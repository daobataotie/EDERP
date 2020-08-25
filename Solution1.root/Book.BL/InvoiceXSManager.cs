//------------------------------------------------------------------------------
//
// file name：InvoiceXSManager.cs
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
    /// Business logic for dbo.InvoiceXS.
    /// </summary>
    public partial class InvoiceXSManager : InvoiceManager
    {
        private static readonly DA.IInvoiceXSDetailAccessor invoiceXSDetailAccessor = (DA.IInvoiceXSDetailAccessor)Accessors.Get("InvoiceXSDetailAccessor");
        private static readonly DA.IInvoiceXOAccessor invoiceXOAccessor = (DA.IInvoiceXOAccessor)Accessors.Get("InvoiceXOAccessor");
        private static readonly DA.ICustomerAccessor customerAccessor = (DA.ICustomerAccessor)Accessors.Get("CustomerAccessor");
        private static readonly DA.ICustomerProductsAccessor customerProductsAccessor = (DA.ICustomerProductsAccessor)Accessors.Get("CustomerProductsAccessor");
        private static readonly DA.IInvoiceXODetailAccessor invoiceXODetailAccessor = (DA.IInvoiceXODetailAccessor)Accessors.Get("InvoiceXODetailAccessor");
        private BL.InvoiceXOManager invoiceXOManager = new InvoiceXOManager();
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private ProductManager productManager = new ProductManager();
        BL.AtSummonManager atSummonManager = new Book.BL.AtSummonManager();
        private static readonly DA.IAtSummonAccessor atSummonAccessor = (DA.IAtSummonAccessor)Accessors.Get("AtSummonAccessor");
        BL.AtSummonDetailManager atSummonDetailManager = new Book.BL.AtSummonDetailManager();

        #region Select

        public IList<Model.InvoiceXS> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceXS> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceXS Get(string invoiceId)
        {
            Model.InvoiceXS invoice = accessor.Get(invoiceId);
            if (invoice != null)
                invoice.Details = invoiceXSDetailAccessor.Select(invoice);
            return invoice;
        }

        public Model.InvoiceXS GetDetails(string invoiceId)
        {
            Model.InvoiceXS invoice = accessor.Get(invoiceId);
            if (invoice != null)
                invoice.Details = invoiceXSDetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion



        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXS))
                throw new ArgumentException();
            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            _Insert((Model.InvoiceXS)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXS))
                throw new ArgumentException();
            _Update((Model.InvoiceXS)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXS))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceXS)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXS))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceXS)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceXS invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceXS invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "XS";
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
            Validate((Model.InvoiceXS)invoice);
            //
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);

            Validate((Model.InvoiceXS)invoice);
        }

        #endregion

        #endregion

        private void _Insert(Model.InvoiceXS invoice)
        {
            invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.Employee1Id = invoice.Employee1 == null ? null : invoice.Employee1.EmployeeId;
            invoice.Employee2Id = invoice.Employee2 == null ? null : invoice.Employee2.EmployeeId;
            accessor.Insert(invoice);
            foreach (Model.InvoiceXSDetail detail in invoice.Details)
            {
                //  if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId)) continue;    
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    continue;
                Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(detail.ProductId, detail.DepotPositionId);
                if (stock == null || Convert.ToDecimal(stock.StockQuantity1) < Convert.ToDecimal(detail.InvoiceXSDetailQuantity))
                    throw new Helper.MessageValueException("" + detail.Product + "\r出貨數量不能大於貨位庫存");

                detail.InvoiceId = invoice.InvoiceId;
                invoiceXSDetailAccessor.Insert(detail);
                Model.InvoiceXODetail xodetail = invoiceXODetailAccessor.Get(detail.InvoiceXODetailId);
                if (xodetail != null)
                {
                    if (detail.InvoiceXSDetailQuantity >= xodetail.InvoiceXODetailQuantity0)
                    {
                        xodetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                    }
                    else
                    {
                        xodetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;
                    }


                    if (!xodetail.InvoiceXODetailBeenQuantity.HasValue)
                        xodetail.InvoiceXODetailBeenQuantity = 0;
                    if (!xodetail.InvoiceXTDetailQuantity.HasValue)
                        xodetail.InvoiceXTDetailQuantity = 0;
                    xodetail.InvoiceXODetailBeenQuantity += detail.InvoiceXSDetailQuantity;
                    xodetail.InvoiceXODetailQuantity0 = xodetail.InvoiceXODetailQuantity - xodetail.InvoiceXODetailBeenQuantity + xodetail.InvoiceXTDetailQuantity;
                    if (xodetail.InvoiceXODetailQuantity0 < 0)
                        xodetail.InvoiceXODetailQuantity0 = 0;
                    invoiceXODetailAccessor.Update(xodetail);
                    //改变客户订单和生产加工单的结案状态
                    invoiceXOManager.UpdateInvoiceFlag(xodetail.Invoice);

                }

                //更新产品表库存
                // customerProductsAccessor.Update(p);
                //更新销售订单出货量和未出货量             
                //单位转化过程
                //1
                //2
                //3

                if (detail.DepotPosition != null)
                {
                    // 更新库存
                    stockAccessor.Decrement(detail.DepotPosition, detail.Product, detail.InvoiceXSDetailQuantity.Value);
                    //更新产品库存
                    //this.productManager.UpdateProduct_Stock(detail.Product);
                    detail.Product.StocksQuantity = stockAccessor.GetTheCountByProduct(detail.Product);
                    detail.Product.ProductNearXSDate = DateTime.Now;
                    this.productManager.update(detail.Product);
                }
            }



            //客户最近交易日
            invoice.Customer.LastTransactionDate = DateTime.Now;
            customerAccessor.Update(invoice.Customer);
        }

        private void _Update(Model.InvoiceXS invoice)
        {
            _ValidateForUpdate(invoice);
            invoice.UpdateTime = DateTime.Now;
            invoice.CustomerId = invoice.Customer.CustomerId;
            invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.Employee1Id = invoice.Employee1 == null ? null : invoice.Employee1.EmployeeId;
            invoice.Employee2Id = invoice.Employee2 == null ? null : invoice.Employee2.EmployeeId;
            Model.InvoiceXS invoiceOriginal = this.Get(invoice.InvoiceId);
            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
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

                            foreach (Model.InvoiceXSDetail detail in invoice.Details)
                            {
                                Model.InvoiceXODetail xodetail = invoiceXODetailAccessor.Get(detail.InvoiceXODetailId);
                                if (xodetail != null)
                                {
                                    xodetail.InvoiceXODetailBeenQuantity = Convert.ToDouble(xodetail.InvoiceXODetailBeenQuantity) - detail.InvoiceXSDetailQuantity;
                                    if (!xodetail.InvoiceXTDetailQuantity.HasValue)
                                        xodetail.InvoiceXTDetailQuantity = 0;
                                    xodetail.InvoiceXODetailQuantity0 = xodetail.InvoiceXODetailQuantity - xodetail.InvoiceXODetailBeenQuantity + xodetail.InvoiceXTDetailQuantity;
                                    if (xodetail.InvoiceXODetailQuantity0 < 0)
                                        xodetail.InvoiceXODetailQuantity0 = 0;

                                    if (xodetail.InvoiceXODetailQuantity0 == 0)
                                        xodetail.DetailsFlag = 2;
                                    else if (xodetail.InvoiceXODetailQuantity0 == xodetail.InvoiceXODetailQuantity)
                                        xodetail.DetailsFlag = 0;
                                    else
                                        xodetail.DetailsFlag = 1;

                                    invoiceXODetailAccessor.Update(xodetail);
                                    invoiceXOManager.UpdateInvoiceFlag(xodetail.Invoice);

                                }
                                if (detail.DepotPosition != null)
                                {
                                    stockAccessor.Increment(detail.DepotPosition, detail.Product, detail.InvoiceXSDetailQuantity.Value);
                                    //更新产品库存                         

                                    this.productManager.UpdateProduct_Stock(detail.Product);
                                }
                                //临时注销客户产品
                                //Model.CustomerProducts p = detail.PrimaryKey;
                                //p.PrimaryKeyId = detail.PrimaryKey.PrimaryKeyId;
                                //if (p.DepotQuantity == null)
                                //    p.DepotQuantity = 0;
                                //if (p.OrderQuantity == null)
                                //    p.OrderQuantity = 0;
                                //p.DepotQuantity -= detail.InvoiceXSDetailQuantity;
                                //p.OrderQuantity += detail.InvoiceXSDetailQuantity;
                                //customerProductsAccessor.Update(p);
                            }
                            if (invoice.InvoiceXO != null)
                                this.invoiceXOManager.UpdateInvoiceFlag(invoice.InvoiceXO);
                            break;
                    }
                    break;
            }
        }

        private void Validate(Model.InvoiceXS invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoice.Customer == null)
                throw new Helper.RequireValueException("Company");

            if (invoice.Depot == null)
                throw new Helper.RequireValueException("Depot");

            foreach (Model.InvoiceXSDetail detail in invoice.Details)
            {
                if (detail.DepotPositionId == null)
                    throw new Helper.RequireValueException(Model.InvoiceXSDetail.PRO_DepotPositionId);
                //if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId)) continue;                
                //if (detail.DepotPosition == null || string.IsNullOrEmpty(detail.DepotPositionId)) 
                //{
                //    throw new Helper.RequireValueException(Model.InvoiceXSDetail.PROPERTY_DEPOTPOSITIONID);
                //}
            }

        }

        public IList<Model.InvoiceXS> Select(DateTime start, DateTime end, Model.Employee employee)
        {
            return accessor.Select(start, end, employee);
        }
        public IList<Book.Model.InvoiceXS> Select(DateTime start, DateTime end, string startId, string endId)
        {
            return accessor.Select(start, end, startId, endId);
        }
        public IList<Book.Model.InvoiceXS> Select1(DateTime start, DateTime end)
        {
            return accessor.Select1(start, end);
        }
        public IList<Book.Model.InvoiceXS> Select(Model.InvoiceXO invoicexo)
        {
            return accessor.Select(invoicexo);
        }
        public IList<Book.Model.InvoiceXS> Select(Model.Customer customer)
        {
            return accessor.Select(customer);
        }
        public IList<Book.Model.InvoiceXS> Select(string customerStart, string customerEnd, string productStart, string productEnd, DateTime dateStart, DateTime dateEnd)
        {
            return accessor.Select(customerStart, customerEnd, productStart, productEnd, dateStart, dateEnd);
        }
        public IList<Model.InvoiceXS> SelectInvoice(Model.Customer customer)
        {
            return accessor.SelectInvoice(customer);
        }
        public IList<Model.InvoiceXS> SelectCustomerInfo(string xoid)
        {
            return accessor.SelectCustomerInfo(xoid);
        }
        public IList<Book.Model.InvoiceXS> SelectDateRangAndWhere(Model.Customer customer, DateTime? dateStart, DateTime? dateEnd, string cusxoid, Model.Product product, string invoicexoid, string FreightedCompanyId, string ConveyanceMethodId)
        {
            return accessor.SelectDateRangAndWhere(customer, dateStart, dateEnd, cusxoid, product, invoicexoid, FreightedCompanyId, ConveyanceMethodId);
        }

        public string SelectByInvoiceCusID(string ID)
        {
            return accessor.SelectByInvoiceCusID(ID);
        }


        #region 生成对应的会计传票

        public void InsertAtSummon(Model.InvoiceXS invoice, Dictionary<string, string> dic)
        {
            Model.AtSummon atSummon = new Book.Model.AtSummon();
            atSummon.SummonId = Guid.NewGuid().ToString();
            atSummon.SummonDate = DateTime.Now;
            atSummon.SummonCategory = "轉帳傳票";
            atSummon.InsertTime = DateTime.Now;
            atSummon.UpdateTime = DateTime.Now;
            atSummon.Id = this.atSummonManager.GetId();
            atSummon.InvoiceXSId = invoice.InvoiceId;

            atSummon.Details = new List<Model.AtSummonDetail>();

            Model.AtSummonDetail detail1 = new Model.AtSummonDetail();
            detail1.SummonDetailId = Guid.NewGuid().ToString();
            detail1.SummonCatetory = atSummon.SummonCategory;
            detail1.Lending = "借";
            detail1.AMoney = invoice.InvoiceTotal;
            detail1.SubjectId = dic[string.Format("應收賬款-{0}", invoice.Customer.CustomerShortName)];
            detail1.InsertTime = DateTime.Now;
            detail1.UpdateTime = DateTime.Now;
            atSummon.Details.Add(detail1);

            Model.AtSummonDetail detail2 = new Model.AtSummonDetail();
            detail2.SummonDetailId = Guid.NewGuid().ToString();
            detail2.SummonCatetory = atSummon.SummonCategory;
            detail2.Lending = "貸";
            detail2.AMoney = invoice.InvoiceHeji;
            detail2.SubjectId = dic["銷貨收入"];
            detail2.InsertTime = DateTime.Now;
            detail2.UpdateTime = DateTime.Now;
            atSummon.Details.Add(detail2);

            Model.AtSummonDetail detail3 = new Model.AtSummonDetail();
            detail3.SummonDetailId = Guid.NewGuid().ToString();
            detail3.SummonCatetory = atSummon.SummonCategory;
            detail3.Lending = "貸";
            detail3.AMoney = invoice.InvoiceTax;
            detail3.SubjectId = dic["銷項稅額"];
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

        public void UpdateAtSummon(Model.InvoiceXS invoice, Dictionary<string, string> dic)
        {
            Model.AtSummon atSummon = atSummonManager.GetByInvoiceXSId(invoice.InvoiceId);
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
                            if (item.Subject.SubjectName == "銷貨收入")
                            {
                                if (item.AMoney != invoice.InvoiceHeji)
                                {
                                    item.AMoney = invoice.InvoiceHeji;
                                    item.UpdateTime = DateTime.Now;

                                    atSummonDetailManager.Update(item);
                                }
                            }
                            else       //銷項稅額
                            {
                                if (item.AMoney != invoice.InvoiceTax)
                                {
                                    item.AMoney = invoice.InvoiceTax;
                                    item.UpdateTime = DateTime.Now;

                                    atSummonDetailManager.Update(item);
                                }
                            }
                        }
                        else          //應收賬款-客戶
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

                atSummon.TotalDebits = atSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
                atSummon.CreditTotal = atSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

                atSummonAccessor.Update(atSummon);
            }
        }

        public void DeleteAtSummon(Model.InvoiceXS invoice)
        {
            Model.AtSummon atSummon = atSummonManager.GetByInvoiceXSId(invoice.InvoiceId);
            if (atSummon != null)
            {
                atSummonManager.Delete(atSummon);
            }
        }
        #endregion
    }
}

