//------------------------------------------------------------------------------
//
// file name：InvoiceCTManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCT.
    /// </summary>
    public partial class InvoiceCTManager : InvoiceManager
    {
        private static readonly DA.IInvoiceCTDetailAccessor invoiceCTDetailAccessor = (DA.IInvoiceCTDetailAccessor)Accessors.Get("InvoiceCTDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        //private static readonly DA.ICompanyAccessor companyAccessor = (DA.ICompanyAccessor)Accessors.Get("CompanyAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IInvoiceCODetailAccessor invoiceCODetailAccessor = (DA.IInvoiceCODetailAccessor)Accessors.Get("InvoiceCODetailAccessor");
        private BL.InvoiceCOManager invoiceCOManager = new InvoiceCOManager();
        //private static readonly DA.ITransactionController transactionController = (DA.ITransactionController)Accessors.Get("TransactionController");
        BL.AtSummonManager atSummonManager = new Book.BL.AtSummonManager();
        private static readonly DA.IAtSummonAccessor atSummonAccessor = (DA.IAtSummonAccessor)Accessors.Get("AtSummonAccessor");
        BL.AtSummonDetailManager atSummonDetailManager = new Book.BL.AtSummonDetailManager();


        #region Select

        public IList<Model.InvoiceCT> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceCT> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceCT Get(string invoiceId)
        {
            Model.InvoiceCT invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceCTDetailAccessor.Select(invoice);
            return invoice;
        }

        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCT))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceCT)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCT))
                throw new ArgumentException();

            _Update((Model.InvoiceCT)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCT))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceCT)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCT))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceCT)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceCT invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceCT invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "CT";
        }

        protected override Book.DA.IInvoiceAccessor GetAccessor()
        {
            return accessor;
        }

        #endregion

        #region Validation

        protected override void _ValidateForUpdate(Book.Model.Invoice invoice)
        {
            base._ValidateForUpdate(invoice);
            //
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);

            Validate((Model.InvoiceCT)invoice);

        }

        #endregion

        #endregion

        #region Helpers

        private void _Insert(Model.InvoiceCT invoice)
        {
            _ValidateForInsert(invoice);

            invoice.SupplierId = invoice.Supplier.SupplierId;

            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            invoice.Employee1Id = invoice.Employee1 == null ? null : invoice.Employee1.EmployeeId;


            if (invoice.InvoiceStatus == (int)Helper.InvoiceStatus.Normal)
            {
                invoice.InvoiceGZTime = DateTime.Now; ;
                //过账人
                invoice.Employee2Id = invoice.Employee2 == null ? null : invoice.Employee2.EmployeeId;
            }
            accessor.Insert(invoice);


            //Model.Depot depot = invoice.Depot;

            // 库存
            foreach (Model.InvoiceCTDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                Model.Product p = productAccessor.Get(detail.ProductId);

                detail.InvoiceId = invoice.InvoiceId;

                invoiceCTDetailAccessor.Insert(detail);
                // p.ProductNearCGDate = DateTime.Now;            
                if (!string.IsNullOrEmpty(detail.DepotPositionId))
                {
                    stockAccessor.Decrement(new DepotPositionManager().Get(detail.DepotPositionId), detail.Product, detail.InvoiceCTDetailQuantity.Value);
                    p.StocksQuantity = Convert.ToDouble(p.StocksQuantity) - Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                    // productAccessor.UpdateProduct_Stock(detail.Product);
                }
                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);

                if (codetail != null)
                {
                    //if (detail.InvoiceCGDetailQuantity > codetail.NoArrivalQuantity)
                    //{
                    //    throw new Helper.InvalidValueException("CGQgtCOQ");
                    //}
                    //进货量大于订单数量 修改订单数量
                    if (detail.InvoiceCTDetailQuantity >= codetail.NoArrivalQuantity)
                    {
                        codetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                    }
                    else
                    {
                        if (codetail.ArrivalQuantity == 0)
                        { codetail.DetailsFlag = (int)Helper.DetailsFlag.OnTheWay; }
                        else
                            codetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;
                    }
                    codetail.InvoiceCTQuantity = Convert.ToDouble(codetail.InvoiceCTQuantity) + Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                    codetail.NoArrivalQuantity = Convert.ToDouble(codetail.OrderQuantity) - Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(codetail.InvoiceCTQuantity);
                    codetail.NoArrivalQuantity = codetail.NoArrivalQuantity < 0 ? 0 : codetail.NoArrivalQuantity;
                    invoiceCODetailAccessor.Update(codetail);
                    this.invoiceCOManager.UpdateInvoiceFlag(codetail.Invoice);
                    if (!p.OrderOnWayQuantity.HasValue) p.OrderOnWayQuantity = 0;
                    p.OrderOnWayQuantity = Convert.ToDouble(p.OrderOnWayQuantity) + Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                }
                productAccessor.Update(p);
                // 成本
                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice, -cgQuantity);

                // 库存
                //stockAccessor.Decrement(invoice.Depot, detail.Product, cgQuantity.Value);
            }
            // 应付
            //companyAccessor.DecrementP(invoice.Company, invoice.InvoiceOwed.Value);
        }

        private void _Update(Model.InvoiceCT invoice)
        {
            _ValidateForUpdate(invoice);

            Model.InvoiceCT invoiceOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            invoice.UpdateTime = DateTime.Now;
                            invoice.SupplierId = invoice.Supplier.SupplierId;
                            //invoice.DepotId = invoice.Depot.DepotId;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            accessor.Update(invoice);

                            invoiceCTDetailAccessor.Delete(invoice);
                            foreach (Model.InvoiceCTDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceCTDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceCTDetailAccessor.Insert(detail);
                            }
                            break;


                        case Helper.InvoiceStatus.Normal:
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;

                        case Helper.InvoiceStatus.Null:
                            throw new InvalidOperationException();
                    }
                    break;

                case Helper.InvoiceStatus.Normal:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            throw new InvalidOperationException();

                        case Helper.InvoiceStatus.Normal:
                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;

                        case Helper.InvoiceStatus.Null:
                            foreach (Model.InvoiceCTDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                Model.Product p = productAccessor.Get(detail.ProductId);
                                if (!string.IsNullOrEmpty(detail.DepotPositionId))
                                {
                                    stockAccessor.Increment(new DepotPositionManager().Get(detail.DepotPositionId), detail.Product, detail.InvoiceCTDetailQuantity.Value);
                                    p.StocksQuantity = Convert.ToDouble(p.StocksQuantity) + Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                                    // productAccessor.UpdateProduct_Stock(detail.Product);
                                }
                                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);

                                if (codetail != null)
                                {
                                    codetail.InvoiceCTQuantity = Convert.ToDouble(codetail.InvoiceCTQuantity) - Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                                    codetail.NoArrivalQuantity = Convert.ToDouble(codetail.OrderQuantity) - Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(codetail.InvoiceCTQuantity);
                                    codetail.NoArrivalQuantity = codetail.NoArrivalQuantity < 0 ? 0 : codetail.NoArrivalQuantity;
                                    //进货量大于订单数量 修改订单数量
                                    if (detail.InvoiceCTDetailQuantity >= codetail.NoArrivalQuantity)
                                    {
                                        codetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                                    }
                                    else
                                    {
                                        if (codetail.ArrivalQuantity == 0)
                                        {
                                            codetail.DetailsFlag = (int)Helper.DetailsFlag.OnTheWay;
                                        }
                                        else
                                            codetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;
                                    }
                                    invoiceCODetailAccessor.Update(codetail);
                                    if (!p.OrderOnWayQuantity.HasValue) p.OrderOnWayQuantity = 0;
                                    p.OrderOnWayQuantity = Convert.ToDouble(p.OrderOnWayQuantity) - Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                                    // this.UpdateSql("update product set OrderOnWayQuantity=OrderOnWayQuantity-" + detail.InvoiceCTDetailQuantity + " where productid='" + codetail.ProductId + "'");
                                    this.invoiceCOManager.UpdateInvoiceFlag(codetail.Invoice);
                                }

                                productAccessor.Update(p);


                                //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceCTDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value) / Convert.ToDecimal(p.ProductInnerUnitRelationship.Value);
                                //    cgQuantity = detail.InvoiceCTDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceCTDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value);
                                //    cgQuantity = detail.InvoiceCTDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceCTDetailPrice;
                                //    cgQuantity = detail.InvoiceCTDetailQuantity;
                                //}
                                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice, cgQuantity);
                                //stockAccessor.Increment(invoice.Depot, p,z.Value);
                            }
                            //companyAccessor.IncrementP(invoice.Company, invoice.InvoiceOwed.Value);
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }


        private void Validate(Model.InvoiceCT invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoice.Supplier == null)
                throw new Helper.RequireValueException("Company");

            //if (invoice.Depot == null)
            //    throw new Helper.RequireValueException("Depot");

            if (invoice.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            //foreach (Model.InvoiceCTDetail detail in invoice.Details)
            //{
            //    if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;                
            //    //if (detail.InvoiceCTDetailMoney0 == 0)
            //{
            //    throw new Helper.RequireValueException("Details");
            //}


            //if (string.IsNullOrEmpty(detail.DepotPositionId) || detail.DepotPosition == null)
            //{
            //    throw new Helper.RequireValueException(Model.InvoiceCGDetail.PRO_DepotPositionId);
            //}
            // }
        }

        #endregion

        public IList<Model.InvoiceCT> SelectByCondition(DateTime dateStart, DateTime dateEnd, string ctStart, string ctEnd, string coStart, string coEnd, string CusId, string supplierid)
        {
            return accessor.SelectByCondition(dateStart, dateEnd, ctStart, ctEnd, coStart, coEnd, CusId, supplierid);
        }


        #region 生成对应的会计传票

        public void InsertAtSummon(Model.InvoiceCT invoice, Dictionary<string, string> dic)
        {
            Model.AtSummon atSummon = new Book.Model.AtSummon();
            atSummon.SummonId = Guid.NewGuid().ToString();
            atSummon.SummonDate = DateTime.Now;
            atSummon.SummonCategory = "轉帳傳票";
            atSummon.InsertTime = DateTime.Now;
            atSummon.UpdateTime = DateTime.Now;
            //atSummon.Id = this.atSummonManager.GetId();
            atSummon.Id = this.atSummonManager.GetConsecutiveId(DateTime.Now);
            atSummon.InvoiceCTId = invoice.InvoiceId;

            atSummon.Details = new List<Model.AtSummonDetail>();

            Model.AtSummonDetail detail1 = new Model.AtSummonDetail();
            detail1.SummonDetailId = Guid.NewGuid().ToString();
            detail1.SummonCatetory = atSummon.SummonCategory;
            detail1.Lending = "貸";
            detail1.AMoney = invoice.InvoiceHeJi;
            //detail1.SubjectId = dic["進貨"];
            detail1.SubjectId = dic[this.GetSubjectNameBySupplier(invoice.Supplier)];
            detail1.InsertTime = DateTime.Now;
            detail1.UpdateTime = DateTime.Now;
            atSummon.Details.Add(detail1);

            if (invoice.InvoiceTax > 0)
            {
                Model.AtSummonDetail detail2 = new Model.AtSummonDetail();
                detail2.SummonDetailId = Guid.NewGuid().ToString();
                detail2.SummonCatetory = atSummon.SummonCategory;
                detail2.Lending = "貸";
                detail2.AMoney = invoice.InvoiceTax;
                detail2.SubjectId = dic["進項稅額"];
                detail2.InsertTime = DateTime.Now;
                detail2.UpdateTime = DateTime.Now;
                atSummon.Details.Add(detail2);
            }

            Model.AtSummonDetail detail3 = new Model.AtSummonDetail();
            detail3.SummonDetailId = Guid.NewGuid().ToString();
            detail3.SummonCatetory = atSummon.SummonCategory;
            detail3.Lending = "借";
            detail3.AMoney = invoice.InvoiceZongJi;
            detail3.SubjectId = dic[string.Format("應付帳款-{0}", invoice.Supplier.SupplierShortName)];
            detail3.InsertTime = DateTime.Now;
            detail3.UpdateTime = DateTime.Now;
            atSummon.Details.Add(detail3);


            foreach (var item in atSummon.Details)
            {
                if (item.Lending == "借")
                    item.Id = "A" + atSummon.Details.IndexOf(item);
                else
                    item.Id = "B" + atSummon.Details.IndexOf(item);
            }

            atSummonManager.TiGuiExists(atSummon);

            //插入
            string invoiceKind = "ats";
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atSummon.SummonDate.Value.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atSummon.SummonDate.Value.Year, atSummon.SummonDate.Value.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atSummon.SummonDate.Value.ToString("yyyy-MM-dd"));
            string sequencekey = string.Format(invoiceKind);

            SequenceManager.Increment(sequencekey_y);
            SequenceManager.Increment(sequencekey_m);
            SequenceManager.Increment(sequencekey_d);
            SequenceManager.Increment(sequencekey);

            atSummon.TotalDebits = atSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
            atSummon.CreditTotal = atSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

            atSummonAccessor.Insert(atSummon);

            foreach (Model.AtSummonDetail atSummonDetail in atSummon.Details)
            {
                atSummonDetail.SummonId = atSummon.SummonId;
                atSummonDetailManager.Insert(atSummonDetail);
            }
        }

        public void UpdateAtSummon(Model.InvoiceCT invoice, Dictionary<string, string> dic)
        {
            Model.AtSummon atSummon = atSummonManager.GetByInvoiceCTId(invoice.InvoiceId);
            if (atSummon != null)
            {
                atSummon.UpdateTime = DateTime.Now;

                atSummon.Details = atSummonDetailManager.Select(atSummon);

                foreach (var item in atSummon.Details)
                {
                    if (dic.Values.Contains(item.SubjectId))
                    {
                        if (item.Lending == "貸")
                        {
                            if (item.Subject.SubjectName == "進項稅額")
                            {
                                if (invoice.InvoiceTax > 0)
                                {
                                    if (item.AMoney != invoice.InvoiceTax)
                                    {
                                        item.AMoney = invoice.InvoiceTax;
                                        item.UpdateTime = DateTime.Now;

                                        atSummonDetailManager.Update(item);
                                    }
                                }
                                else
                                    atSummonDetailManager.Delete(item.SummonDetailId);
                            }
                            else       //供應商分類對應的科目
                            {
                                if (item.AMoney != invoice.InvoiceHeJi)
                                {
                                    item.AMoney = invoice.InvoiceHeJi;
                                    item.UpdateTime = DateTime.Now;

                                    atSummonDetailManager.Update(item);
                                }
                            }
                        }
                        else          //應付賬款-廠商
                        {
                            if (item.AMoney != invoice.InvoiceZongJi)
                            {
                                item.AMoney = invoice.InvoiceZongJi;
                                item.UpdateTime = DateTime.Now;

                                atSummonDetailManager.Update(item);
                            }
                        }
                    }
                }

                if (invoice.InvoiceTax > 0 && atSummon.Details.Count == 2 && !atSummon.Details.Any(d => d.Subject.SubjectName == "進項稅額"))
                {
                    Model.AtSummonDetail detail2 = new Model.AtSummonDetail();
                    detail2.SummonId = atSummon.SummonId;
                    detail2.SummonDetailId = Guid.NewGuid().ToString();
                    detail2.SummonCatetory = atSummon.SummonCategory;
                    detail2.Lending = "貸";
                    detail2.Id = "A2";
                    detail2.AMoney = invoice.InvoiceTax;
                    detail2.SubjectId = dic["進項稅額"];
                    detail2.InsertTime = DateTime.Now;
                    detail2.UpdateTime = DateTime.Now;
                    atSummon.Details.Add(detail2);

                    atSummonDetailManager.Insert(detail2);
                }

                atSummon.TotalDebits = atSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
                atSummon.CreditTotal = atSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

                atSummonAccessor.Update(atSummon);
            }
        }

        public void DeleteAtSummon(Model.InvoiceCT invoice)
        {
            Model.AtSummon atSummon = atSummonManager.GetByInvoiceCTId(invoice.InvoiceId);
            if (atSummon != null)
            {
                atSummonManager.Delete(atSummon);
            }
        }
        #endregion
    }
}

