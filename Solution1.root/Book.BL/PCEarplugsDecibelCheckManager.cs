//------------------------------------------------------------------------------
//
// file name：PCEarplugsDecibelCheckManager.cs
// author: mayanjun
// create date：2019/5/12 14:43:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarplugsDecibelCheck.
    /// </summary>
    public partial class PCEarplugsDecibelCheckManager : BL.BaseManager
    {
        private static readonly DA.IPCEarplugsDecibelCheckDetailAccessor accessorDetail = (DA.IPCEarplugsDecibelCheckDetailAccessor)Accessors.Get("PCEarplugsDecibelCheckDetailAccessor");

        /// <summary>
        /// Delete PCEarplugsDecibelCheck by primary key.
        /// </summary>
        public void Delete(string pCEarplugsDecibelCheckId)
        {
            //
            // todo:add other logic here
            // 
            try
            {
                BL.V.BeginTransaction();

                accessorDetail.DeleteByHeaderId(pCEarplugsDecibelCheckId);
                accessor.Delete(pCEarplugsDecibelCheckId);

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();

                throw ex;
            }
        }

        /// <summary>
        /// Insert a PCEarplugsDecibelCheck.
        /// </summary>
        public void Insert(Model.PCEarplugsDecibelCheck pCEarplugsDecibelCheck)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();

                Validate(pCEarplugsDecibelCheck);
                this.TiGuiExists(pCEarplugsDecibelCheck);

                pCEarplugsDecibelCheck.InsertTime = DateTime.Now;
                pCEarplugsDecibelCheck.UpdateTime = DateTime.Now;

                accessor.Insert(pCEarplugsDecibelCheck);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCEarplugsDecibelCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCEarplugsDecibelCheck.InsertTime.Value.Year, pCEarplugsDecibelCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCEarplugsDecibelCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                foreach (var item in pCEarplugsDecibelCheck.Details)
                {
                    item.PCEarplugsDecibelCheckId = pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId;

                    accessorDetail.Insert(item);
                }

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();

                throw ex;
            }
        }

        /// <summary>
        /// Update a PCEarplugsDecibelCheck.
        /// </summary>
        public void Update(Model.PCEarplugsDecibelCheck pCEarplugsDecibelCheck)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();

                Validate(pCEarplugsDecibelCheck);
                pCEarplugsDecibelCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCEarplugsDecibelCheck);

                accessorDetail.DeleteByHeaderId(pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId);
                foreach (var item in pCEarplugsDecibelCheck.Details)
                {
                    item.PCEarplugsDecibelCheckId = pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId;

                    accessorDetail.Insert(item);
                }

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();

                throw ex;
            }
        }

        protected override string GetInvoiceKind()
        {
            return "PCED";
        }

        protected override string GetSettingId()
        {
            return "PCEarplugsDecibelCheckRule";
        }

        public void Validate(Model.PCEarplugsDecibelCheck model)
        {
            if (model.PCEarplugsDecibelCheckDate == null)
                throw new Helper.InvalidValueException(Model.PCEarplugsDecibelCheck.PRO_PCEarplugsDecibelCheckDate);
        }

        private void TiGuiExists(Model.PCEarplugsDecibelCheck model)
        {
            if (this.ExistsPrimary(model.PCEarplugsDecibelCheckId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.InsertTime.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.PCEarplugsDecibelCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCEarplugsDecibelCheck GetDetail(string pCEarplugsDecibelCheckId)
        {
            Model.PCEarplugsDecibelCheck model = this.Get(pCEarplugsDecibelCheckId);
            if (model != null)
                model.Details = accessorDetail.SelectByHeaderId(pCEarplugsDecibelCheckId);
            return model;
        }
    }
}
