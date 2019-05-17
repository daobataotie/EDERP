//------------------------------------------------------------------------------
//
// file name：PCEarplugsStayWireCheckManager.cs
// author: mayanjun
// create date：2019/5/14 09:57:05
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarplugsStayWireCheck.
    /// </summary>
    public partial class PCEarplugsStayWireCheckManager : BL.BaseManager
    {
        private static readonly DA.IPCEarplugsStayWireCheckDetailAccessor accessorDetail = (DA.IPCEarplugsStayWireCheckDetailAccessor)Accessors.Get("PCEarplugsStayWireCheckDetailAccessor");

        /// <summary>
        /// Delete PCEarplugsStayWireCheck by primary key.
        /// </summary>
        public void Delete(string pCEarplugsStayWireCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();

                accessorDetail.DeleteByHeaderId(pCEarplugsStayWireCheckId);
                accessor.Delete(pCEarplugsStayWireCheckId);

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();

                throw ex;
            }
        }

        /// <summary>
        /// Insert a PCEarplugsStayWireCheck.
        /// </summary>
        public void Insert(Model.PCEarplugsStayWireCheck pCEarplugsStayWireCheck)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();

                Validate(pCEarplugsStayWireCheck);
                this.TiGuiExists(pCEarplugsStayWireCheck);

                pCEarplugsStayWireCheck.InsertTime = DateTime.Now;
                pCEarplugsStayWireCheck.UpdateTime = DateTime.Now;

                accessor.Insert(pCEarplugsStayWireCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCEarplugsStayWireCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCEarplugsStayWireCheck.InsertTime.Value.Year, pCEarplugsStayWireCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCEarplugsStayWireCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                foreach (var item in pCEarplugsStayWireCheck.Details)
                {
                    item.PCEarplugsStayWireCheckId = pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId;

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
        /// Update a PCEarplugsStayWireCheck.
        /// </summary>
        public void Update(Model.PCEarplugsStayWireCheck pCEarplugsStayWireCheck)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();

                Validate(pCEarplugsStayWireCheck);
                pCEarplugsStayWireCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCEarplugsStayWireCheck);

                accessorDetail.DeleteByHeaderId(pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId);
                foreach (var item in pCEarplugsStayWireCheck.Details)
                {
                    item.PCEarplugsStayWireCheckId = pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId;

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
            return "PCES";
        }

        protected override string GetSettingId()
        {
            return "PCEarplugsStayWireCheckRule";
        }

        public void Validate(Model.PCEarplugsStayWireCheck model)
        {
            if (model.PCEarplugsStayWireCheckDate == null)
                throw new Helper.InvalidValueException(Model.PCEarplugsStayWireCheck.PRO_PCEarplugsStayWireCheckDate);
        }

        private void TiGuiExists(Model.PCEarplugsStayWireCheck model)
        {
            if (this.ExistsPrimary(model.PCEarplugsStayWireCheckId))
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
                model.PCEarplugsStayWireCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCEarplugsStayWireCheck GetDetail(string pCEarplugsStayWireCheckId)
        {
            Model.PCEarplugsStayWireCheck model = this.Get(pCEarplugsStayWireCheckId);
            if (model != null)
                model.Details = accessorDetail.SelectByHeaderId(pCEarplugsStayWireCheckId);
            return model;
        }
    }
}
