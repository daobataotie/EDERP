//------------------------------------------------------------------------------
//
// file name：PCOtherCheckManager.cs
// author: mayanjun
// create date：2011-11-10 15:05:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCOtherCheck.
    /// </summary>
    public partial class PCOtherCheckManager : BaseManager
    {
        private static readonly DA.IPCOtherCheckDetailAccessor PCOtherCheckDetailAccessor = (DA.IPCOtherCheckDetailAccessor)Accessors.Get("PCOtherCheckDetailAccessor");
        BL.InvoiceCGDetailManager cgdetailManager = new InvoiceCGDetailManager();
        BL.ProduceOtherInDepotDetailManager podetailManager = new ProduceOtherInDepotDetailManager();
        /// <summary>
        /// Delete PCOtherCheck by primary key.
        /// </summary>
        public void Delete(string pCOtherCheckId)
        {
            accessor.Delete(pCOtherCheckId);
        }
        public void Delete(Model.PCOtherCheck model)
        {
            try
            {
                BL.V.BeginTransaction();

                //排單狀態修改
                Model.InvoiceCGDetail cgDetail;
                Model.ProduceOtherInDepotDetail po;
                foreach (var item in model.Detail)
                {
                    if (item.PCOtherCheckDetailFromPC == "1")
                    {
                        cgDetail = cgdetailManager.Get(item.FromInvoiceDetailID);
                        if (cgDetail != null)
                        {
                            cgDetail.GenerateInputCheck = "";
                            cgdetailManager.Update(cgDetail);
                        }
                    }
                    if (item.PCOtherCheckDetailFromPC == "0")
                    {
                        po = podetailManager.Get(item.FromInvoiceDetailID);
                        if (po != null)
                        {
                            po.GenerateInputCheck = "";
                            podetailManager.Update(po);
                        }
                    }
                }


                PCOtherCheckDetailAccessor.DeleteByPCOCId(model.PCOtherCheckId);

                this.Delete(model.PCOtherCheckId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a PCOtherCheck.
        /// </summary>
        public void Insert(Model.PCOtherCheck pCOtherCheck)
        {
            Validate(pCOtherCheck);
            try
            {
                BL.V.BeginTransaction();
                TiGuiExists(pCOtherCheck);
                pCOtherCheck.InsertTime = DateTime.Now;
                pCOtherCheck.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCOtherCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCOtherCheck.InsertTime.Value.Year, pCOtherCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCOtherCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCOtherCheck);

                Model.InvoiceCGDetail cgDetail;
                Model.ProduceOtherInDepotDetail po;
                foreach (Model.PCOtherCheckDetail PcotherCheckDetail in pCOtherCheck.Detail)
                {
                    if (PcotherCheckDetail.Product == null || string.IsNullOrEmpty(PcotherCheckDetail.Product.ProductId)) continue;
                    PcotherCheckDetail.PCOtherCheckId = pCOtherCheck.PCOtherCheckId;
                    PCOtherCheckDetailAccessor.Insert(PcotherCheckDetail);

                    //排單狀態修改
                    if (PcotherCheckDetail.PCOtherCheckDetailFromPC == "1")
                    {
                        cgDetail = cgdetailManager.Get(PcotherCheckDetail.FromInvoiceDetailID);
                        if (cgDetail != null)
                        {
                            cgDetail.GenerateInputCheck = "已生成進料檢驗單";
                            cgdetailManager.Update(cgDetail);
                        }
                    }
                    if (PcotherCheckDetail.PCOtherCheckDetailFromPC == "0")
                    {
                        po = podetailManager.Get(PcotherCheckDetail.FromInvoiceDetailID);
                        if (po != null)
                        {
                            po.GenerateInputCheck = "已生成進料檢驗單";
                            podetailManager.Update(po);
                        }
                    }
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a PCOtherCheck.
        /// </summary>
        public void Update(Model.PCOtherCheck pCOtherCheck)
        {
            Validate(pCOtherCheck);
            if (pCOtherCheck != null)
            {
                //修改头
                pCOtherCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCOtherCheck);

                IList<Model.PCOtherCheckDetail> oldDetail = PCOtherCheckDetailAccessor.Selct(pCOtherCheck);
                //排單狀態修改
                Model.InvoiceCGDetail cgDetail;
                Model.ProduceOtherInDepotDetail po;
                foreach (var item in oldDetail)
                {
                    if (item.PCOtherCheckDetailFromPC == "1")
                    {
                        cgDetail = cgdetailManager.Get(item.FromInvoiceDetailID);
                        if (cgDetail != null)
                        {
                            cgDetail.GenerateInputCheck = "";
                            cgdetailManager.Update(cgDetail);
                        }
                    }
                    if (item.PCOtherCheckDetailFromPC == "0")
                    {
                        po = podetailManager.Get(item.FromInvoiceDetailID);
                        if (po != null)
                        {
                            po.GenerateInputCheck = "";
                            podetailManager.Update(po);
                        }
                    }
                }
                //删除详细
                PCOtherCheckDetailAccessor.DeleteByPCOCId(pCOtherCheck.PCOtherCheckId);

                //添加详细
                Model.InvoiceCGDetail cgDetail2;
                Model.ProduceOtherInDepotDetail po2;
                foreach (Model.PCOtherCheckDetail details in pCOtherCheck.Detail)
                {
                    details.PCOtherCheckId = pCOtherCheck.PCOtherCheckId;
                    PCOtherCheckDetailAccessor.Insert(details);

                    //排單狀態修改
                    if (details.PCOtherCheckDetailFromPC == "1")
                    {
                        cgDetail2 = cgdetailManager.Get(details.FromInvoiceDetailID);
                        if (cgDetail2 != null)
                        {
                            cgDetail2.GenerateInputCheck = "已生成進料檢驗單";
                            cgdetailManager.Update(cgDetail2);
                        }
                    } if (details.PCOtherCheckDetailFromPC == "0")
                    {
                        po2 = podetailManager.Get(details.FromInvoiceDetailID);
                        if (po2 != null)
                        {
                            po2.GenerateInputCheck = "已生成進料檢驗單";
                            podetailManager.Update(po2);
                        }
                    }
                }
            }
        }

        private void Validate(Model.PCOtherCheck _pcoc)
        {
            if (string.IsNullOrEmpty(_pcoc.PCOtherCheckId))
                throw new Helper.RequireValueException(Model.PCOtherCheck.PRO_PCOtherCheckId);
            if (string.IsNullOrEmpty(_pcoc.PCOtherCheckDate.ToString()))
                throw new Helper.RequireValueException(Model.PCOtherCheck.PRO_PCOtherCheckDate);
            if (string.IsNullOrEmpty(_pcoc.Employee0Id))
                throw new Helper.RequireValueException(Model.PCOtherCheck.PRO_Employee0Id);
        }

        private void TiGuiExists(Model.PCOtherCheck model)
        {
            if (this.ExistsPrimary(model.PCOtherCheckId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.PCOtherCheckDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.PCOtherCheckDate.Value.Year, model.PCOtherCheckDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.PCOtherCheckDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.PCOtherCheckId = this.GetId(model.PCOtherCheckDate.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        public Model.PCOtherCheck GetDetails(string PCOtherCheckId)
        {
            Model.PCOtherCheck _Pcoc = accessor.Get(PCOtherCheckId);
            _Pcoc.Detail = PCOtherCheckDetailAccessor.Selct(_Pcoc);
            return _Pcoc;
        }

        public IList<Model.PCOtherCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string CusXOId)
        {
            return accessor.SelectByDateRage(StartDate, EndDate, product, customer, CusXOId);
        }

        protected override string GetSettingId()
        {
            return "pcocRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcoc";
        }
    }
}

