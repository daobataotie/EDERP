//------------------------------------------------------------------------------
//
// file name：InvoiceCGManager.cs
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
    /// 采购单
    /// </summary>
    public partial class InvoiceCGManager : InvoiceManager
    {
        private static readonly DA.IInvoiceCGDetailAccessor invoiceCGDetailAccessor = (DA.IInvoiceCGDetailAccessor)Accessors.Get("InvoiceCGDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoiceCODetailAccessor invoiceCODetailAccessor = (DA.IInvoiceCODetailAccessor)Accessors.Get("InvoiceCODetailAccessor");
        private static readonly DA.IInvoiceCOAccessor invoiceCOAccessor = (DA.IInvoiceCOAccessor)Accessors.Get("InvoiceCOAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly ProductManager productManager = new ProductManager();
        private BL.InvoiceCOManager invoiceCOManager = new InvoiceCOManager();
        BL.AtSummonManager atSummonManager = new Book.BL.AtSummonManager();
        private static readonly DA.IAtSummonAccessor atSummonAccessor = (DA.IAtSummonAccessor)Accessors.Get("AtSummonAccessor");
        BL.AtSummonDetailManager atSummonDetailManager = new Book.BL.AtSummonDetailManager();

        #region Select

        public IList<Model.InvoiceCG> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }


        public IList<Model.InvoiceCG> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceCG Get(string invoiceId)
        {
            Model.InvoiceCG invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceCGDetailAccessor.Select(invoice);
            return invoice;
        }

        public Model.InvoiceCG GetDetails(Model.InvoiceCG invoicecg)
        {
            Model.InvoiceCG invoice = accessor.Get(invoicecg.InvoiceId);
            if (invoice != null)
            {
                invoice.Details = invoiceCGDetailAccessor.Select(invoice);
            }
            return invoice;
        }

        //public Model.InvoiceCG GetDetail(Model.InvoiceCG invoiceCG)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    invoiceCG.Details = invoiceCGDetailAccessor.Select(invoiceCG);
        //    invoiceCG.PositionAndNumsSet.Clear();
        //    foreach (Model.InvoiceCGDetail item in invoiceCG.Details)
        //    {
        //        if (!dic.ContainsKey(item.InvoiceCODetailId))
        //        {
        //            invoiceCG.PositionAndNumsSet.Add(item);
        //            dic.Add(item.InvoiceCODetailId, item.ProductId);
        //        }
        //        else
        //        {
        //            if (item.ProductId != dic[item.InvoiceCODetailId])
        //                dic.Add(item.InvoiceCODetailId, item.ProductId);
        //            else
        //            {
        //                foreach (Model.InvoiceCGDetail temp in invoiceCG.PositionAndNumsSet)
        //                {
        //                    if (temp.ProductId == item.ProductId)
        //                        temp.InvoiceCGDetailQuantity += item.InvoiceCGDetailQuantity;
        //                }
        //            }
        //        }
        //    }

        //    return invoiceCG;
        //}
        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCG))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            // invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceCG)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCG))
                throw new ArgumentException();

            _Update((Model.InvoiceCG)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCG))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceCG)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCG))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceCG)invoice);
        }

        #endregion

        #region Other

        protected override string GetInvoiceKind()
        {
            return "CG";
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
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoiceCG)
        {
            base._ValidateForInsert(invoiceCG);

            Model.InvoiceCG invoice = invoiceCG as Model.InvoiceCG;

            if (invoice.Supplier == null)
                throw new Helper.RequireValueException("Company");

            //if (invoice.Depot == null)
            //    throw new Helper.RequireValueException(Model.InvoiceCG.PRO_DepotId);

            if (invoice.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            //int count = 0;
            foreach (Model.InvoiceCGDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;

                if (string.IsNullOrEmpty(detail.DepotPositionId) && Convert.ToDouble(detail.ProduceTransferQuantity) <= 0)
                {
                    //throw new Helper.RequireValueException(Model.InvoiceCGDetail.PRO_DepotPositionId);
                    throw new Helper.MessageValueException("必須選擇入庫貨位或填寫轉生產數量");
                }
                //count++;
            }
            //if (count == 0)
            //    throw new Helper.RequireValueException("InvoiceCGDetailQuantityIsZero");

        }

        #endregion

        #endregion

        #region Helpers

        private void _TurnNormal(Model.InvoiceCG invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        private void _TurnNull(Model.InvoiceCG invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _Insert(Model.InvoiceCG invoice)
        {
            //   Model.InvoiceCO invoiceCO = invoice.InvoiceCO; //修改订单状态      
            _ValidateForInsert(invoice);
            if (this.HasRows(invoice.InvoiceId))
                throw new Helper.InvalidValueException(Model.InvoiceCG.PRO_InvoiceCOId);
            // int flag = 0;
            //foreach (Model.InvoiceCGDetail detail in invoice.Details)
            //{
            //    if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
            //    if (!string.IsNullOrEmpty( detail.DepotPositionId))
            //        flag=1;
            //}
            //if(flag==0)
            //    throw new Helper.RequireValueException(Model.InvoiceCGDetail.PRO_DepotPositionId);

            if (invoice.Employee0 == null)
                throw new Helper.MessageValueException("操作員不能為空！");

            invoice.SupplierId = invoice.Supplier.SupplierId;
            if (invoice.Depot != null)
                invoice.DepotId = invoice.Depot.DepotId;

            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.Employee1Id = invoice.Employee1 == null ? null : invoice.Employee1.EmployeeId;
            //过账人
            invoice.Employee2Id = invoice.Employee2 == null ? null : invoice.Employee2.EmployeeId;
            //过账时间
            invoice.InvoiceGZTime = DateTime.Now;
            invoice.InvoiceLRTime = DateTime.Now;

            accessor.Insert(invoice);

            foreach (Model.InvoiceCGDetail detail in invoice.Details)
            {
                //if (detail.InvoiceCGDetaiInQuantity + invoiceCGDetailAccessor.CountInDepotQuantity(detail.InvoiceCODetailId) > detail.OrderQuantity)
                //    throw new Helper.MessageValueException("序號: " + (detail.Inumber == null ? "" : detail.Inumber.ToString()) + " 總入庫數量大于訂貨數量！");
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                Model.Product p = productAccessor.Get(detail.ProductId);

                detail.InvoiceId = invoice.InvoiceId;
                detail.DepotPositionId = detail.DepotPositionId;
                invoiceCGDetailAccessor.Insert(detail);
                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);

                if (codetail != null)
                {
                    //if (detail.InvoiceCGDetailQuantity > codetail.NoArrivalQuantity)
                    //{
                    //    throw new Helper.InvalidValueException("CGQgtCOQ");
                    //}
                    //进货量大于订单数量 修改订单数量
                    //if (detail.InvoiceCGDetailQuantity >= codetail.NoArrivalQuantity)
                    //{
                    //    //invoiceCO.InvoiceHeji-=codetail.InvoiceCODetailMoney;
                    //    codetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                    //    // codetail.OrderQuantity = detail.InvoiceCGDetailQuantity + codetail.ArrivalQuantity;
                    //    // codetail.InvoiceCODetailMoney = codetail.InvoiceCODetailPrice *decimal.Parse(codetail.OrderQuantity.Value.ToString());
                    //    // codetail.TotalMoney = codetail.InvoiceCODetailMoney;
                    //    // invoiceCO.InvoiceHeji += codetail.InvoiceCODetailMoney;
                    //    //暂时 未考虑税率
                    //    // invoiceCO.InvoiceTotal = invoiceCO.InvoiceHeji;
                    //}
                    //else
                    //{
                    //    if (codetail.ArrivalQuantity == 0)
                    //    { codetail.DetailsFlag = (int)Helper.DetailsFlag.OnTheWay; }
                    //    else
                    //        codetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;
                    //}

                    codetail.ArrivalQuantity = Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(detail.InvoiceCGDetailQuantity);

                    codetail.NoArrivalQuantity = Convert.ToDouble(codetail.OrderQuantity) - Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(codetail.InvoiceCTQuantity);
                    codetail.NoArrivalQuantity = codetail.NoArrivalQuantity < 0 ? 0 : codetail.NoArrivalQuantity;

                    if (codetail.NoArrivalQuantity == 0)
                    {
                        codetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                    }
                    else if (codetail.ArrivalQuantity == 0)
                    {
                        codetail.DetailsFlag = (int)Helper.DetailsFlag.OnTheWay;
                    }
                    else
                        codetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;

                    invoiceCODetailAccessor.Update(codetail);
                    this.invoiceCOManager.UpdateInvoiceFlag(codetail.Invoice);
                }

                //更新产品信息00

                if (!string.IsNullOrEmpty(detail.InvoiceCODetailId))
                    p.OrderOnWayQuantity = Convert.ToDouble(p.OrderOnWayQuantity) - Convert.ToDouble(detail.InvoiceCGDetailQuantity);
                //2017年7月24日00:07:10： 可以为负,否则会导致不准
                //p.OrderOnWayQuantity = p.OrderOnWayQuantity < 0 ? 0 : p.OrderOnWayQuantity;
                p.ProductNearCGDate = DateTime.Now;
                if (codetail != null)//单价 以后 在进库单 保存
                    p.NewestCost = codetail.InvoiceCODetailPrice.HasValue ? codetail.InvoiceCODetailPrice.Value : 0;
                if (!string.IsNullOrEmpty(detail.DepotPositionId) && detail.InvoiceCGDetaiInQuantity != 0)
                {
                    p.StocksQuantity = Convert.ToDouble(p.StocksQuantity) + Convert.ToDouble(detail.InvoiceCGDetaiInQuantity);
                    //修改货位库存。
                    stockAccessor.Increment(detail.DepotPosition, p, detail.InvoiceCGDetaiInQuantity);
                }
                // 成本
                // productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice,cgQuantity);
                productManager.update(p);
            }




            //应收应付
            //companyAccessor.IncrementP(invoice.Company, invoice.InvoiceOwed.Value);
        }

        private void _Update(Model.InvoiceCG invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;

            Model.InvoiceCG invoiceOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:

                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            invoice.UpdateTime = DateTime.Now;
                            invoice.SupplierId = invoice.Supplier.SupplierId;
                            invoice.DepotId = invoice.Depot.DepotId;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            accessor.Update(invoice);

                            invoiceCGDetailAccessor.Delete(invoiceOriginal);
                            foreach (Model.InvoiceCGDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceCGDetailAccessor.Insert(detail);
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

                            foreach (Model.InvoiceCGDetail detail in invoice.Details)
                            {
                                //修改订单未到和实际到数量   
                                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);
                                if (codetail != null)
                                {
                                    codetail.ArrivalQuantity = Convert.ToDouble(codetail.ArrivalQuantity) - Convert.ToDouble(detail.InvoiceCGDetailQuantity);

                                    codetail.NoArrivalQuantity = Convert.ToDouble(codetail.OrderQuantity) - Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(codetail.InvoiceCTQuantity);
                                    codetail.NoArrivalQuantity = codetail.NoArrivalQuantity < 0 ? 0 : codetail.NoArrivalQuantity;
                                    if (codetail.NoArrivalQuantity < 0)
                                        codetail.NoArrivalQuantity = 0;
                                    if (codetail.NoArrivalQuantity == 0)
                                        codetail.DetailsFlag = 2;
                                    else if (codetail.NoArrivalQuantity == codetail.OrderQuantity)
                                        codetail.DetailsFlag = 0;
                                    else
                                        codetail.DetailsFlag = 1;
                                    invoiceCODetailAccessor.Update(codetail);
                                    this.invoiceCOManager.UpdateInvoiceFlag(codetail.Invoice);
                                }

                                //更新产品信息
                                //if (detail.DepotPosition != null)
                                //{
                                Model.Product pro = detail.Product;
                                if (!string.IsNullOrEmpty(detail.InvoiceCODetailId))
                                    pro.OrderOnWayQuantity = Convert.ToDouble(pro.OrderOnWayQuantity) + Convert.ToDouble(detail.InvoiceCGDetailQuantity);
                                pro.ProductNearCGDate = DateTime.Now;
                                if (!string.IsNullOrEmpty(detail.DepotPositionId) && detail.InvoiceCGDetaiInQuantity != 0)
                                {
                                    pro.StocksQuantity = Convert.ToDouble(pro.StocksQuantity) - Convert.ToDouble(detail.InvoiceCGDetaiInQuantity);

                                    //修改货位库存。
                                    stockAccessor.Decrement(detail.DepotPosition, pro, detail.InvoiceCGDetaiInQuantity);
                                }
                                productManager.update(pro);
                                //}
                                // 成本
                                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice,cgQuantity);
                            }
                            //  this.invoiceCOManager.UpdateInvoiceFlag(invoice.InvoiceCO);
                            //应收应付
                            //companyAccessor.IncrementP(invoice.Company, invoice.InvoiceOwed.Value);
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }
        #endregion

        public IList<Book.Model.InvoiceCG> Select(DateTime start, DateTime end, string startID, string endID)
        {
            return accessor.Select(start, end, startID, endID);
        }

        public IList<Book.Model.InvoiceCG> Select1(DateTime start, DateTime end)
        {
            return accessor.Select1(start, end);
        }
        public IList<Book.Model.InvoiceCG> Select(Model.Supplier supplier)
        {
            return accessor.Select(supplier);
        }
        public IList<Book.Model.InvoiceCG> Select(string costartid, string coendid, Model.Supplier SupplierStart, Model.Supplier SupplierEnd, DateTime dateStart, DateTime dateEnd, Model.Product productStart, Model.Product productEnd, string cusxoid, DateTime dateJHStart, DateTime dateJHEnd, string InvoiceCGIdStart, string InvoiceCGIdEnd)
        {
            return accessor.Select(costartid, coendid, SupplierStart, SupplierEnd, dateStart, dateEnd, productStart, productEnd, cusxoid, dateJHStart, dateJHEnd, InvoiceCGIdStart, InvoiceCGIdEnd);
        }


        #region 生成对应的会计传票

        public void InsertAtSummon(Model.InvoiceCG invoice, Dictionary<string, string> dic)
        {
            Model.AtSummon atSummon = new Book.Model.AtSummon();
            atSummon.SummonId = Guid.NewGuid().ToString();
            atSummon.SummonDate = DateTime.Now;
            atSummon.SummonCategory = "轉帳傳票";
            atSummon.InsertTime = DateTime.Now;
            atSummon.UpdateTime = DateTime.Now;
            //atSummon.Id = this.atSummonManager.GetId();
            atSummon.Id = this.atSummonManager.GetConsecutiveId(DateTime.Now);
            atSummon.InvoiceCGId = invoice.InvoiceId;

            atSummon.Details = new List<Model.AtSummonDetail>();

            Model.AtSummonDetail detail1 = new Model.AtSummonDetail();
            detail1.SummonDetailId = Guid.NewGuid().ToString();
            detail1.SummonCatetory = atSummon.SummonCategory;
            detail1.Lending = "借";
            detail1.AMoney = invoice.InvoiceHeji;
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
                detail2.Lending = "借";
                detail2.AMoney = invoice.InvoiceTax;
                detail2.SubjectId = dic["進項稅額"];
                detail2.InsertTime = DateTime.Now;
                detail2.UpdateTime = DateTime.Now;
                atSummon.Details.Add(detail2);
            }

            Model.AtSummonDetail detail3 = new Model.AtSummonDetail();
            detail3.SummonDetailId = Guid.NewGuid().ToString();
            detail3.SummonCatetory = atSummon.SummonCategory;
            detail3.Lending = "貸";
            detail3.AMoney = invoice.InvoiceTotal;
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

        public void UpdateAtSummon(Model.InvoiceCG invoice, Dictionary<string, string> dic)
        {
            Model.AtSummon atSummon = atSummonManager.GetByInvoiceCGId(invoice.InvoiceId);
            if (atSummon != null)
            {
                atSummon.UpdateTime = DateTime.Now;

                atSummon.Details = atSummonDetailManager.Select(atSummon);

                foreach (var item in atSummon.Details)
                {
                    if (dic.Values.Contains(item.SubjectId))
                    {
                        if (item.Lending == "借")
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
                                if (item.AMoney != invoice.InvoiceHeji)
                                {
                                    item.AMoney = invoice.InvoiceHeji;
                                    item.UpdateTime = DateTime.Now;

                                    atSummonDetailManager.Update(item);
                                }
                            }
                        }
                        else          //應付賬款-廠商
                        {
                            if (item.AMoney != invoice.InvoiceTotal)
                            {
                                item.AMoney = invoice.InvoiceTotal;
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
                    detail2.Lending = "借";
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

        public void DeleteAtSummon(Model.InvoiceCG invoice)
        {
            Model.AtSummon atSummon = atSummonManager.GetByInvoiceCGId(invoice.InvoiceId);
            if (atSummon != null)
            {
                atSummonManager.Delete(atSummon);
            }
        }
        #endregion
    }
}

