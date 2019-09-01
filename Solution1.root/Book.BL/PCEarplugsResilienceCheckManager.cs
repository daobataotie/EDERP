//------------------------------------------------------------------------------
//
// file name：PCEarplugsResilienceCheckManager.cs
// author: mayanjun
// create date：2019/5/10 10:49:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarplugsResilienceCheck.
    /// </summary>
    public partial class PCEarplugsResilienceCheckManager : BL.BaseManager
    {
        private static readonly DA.IPCEarplugsResilienceCheckDetailAccessor accessorDetail = (DA.IPCEarplugsResilienceCheckDetailAccessor)Accessors.Get("PCEarplugsResilienceCheckDetailAccessor");

        /// <summary>
        /// Delete PCEarplugsResilienceCheck by primary key.
        /// </summary>
        public void Delete(string pCEarplugsResilienceCheckId)
        {
            //
            // todo:add other logic here
            //

            try
            {
                BL.V.BeginTransaction();

                accessorDetail.DeleteByHeaderId(pCEarplugsResilienceCheckId);
                accessor.Delete(pCEarplugsResilienceCheckId);

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();

                throw ex;
            }
        }

        /// <summary>
        /// Insert a PCEarplugsResilienceCheck.
        /// </summary>
        public void Insert(Model.PCEarplugsResilienceCheck pCEarplugsResilienceCheck)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();

                pCEarplugsResilienceCheck.InsertTime = DateTime.Now;
                pCEarplugsResilienceCheck.UpdateTime = DateTime.Now;


                Validate(pCEarplugsResilienceCheck);
                this.TiGuiExists(pCEarplugsResilienceCheck);

                accessor.Insert(pCEarplugsResilienceCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCEarplugsResilienceCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCEarplugsResilienceCheck.InsertTime.Value.Year, pCEarplugsResilienceCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCEarplugsResilienceCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                foreach (var item in pCEarplugsResilienceCheck.Details)
                {
                    item.PCEarplugsResilienceCheckId = pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId;

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
        /// Update a PCEarplugsResilienceCheck.
        /// </summary>
        public void Update(Model.PCEarplugsResilienceCheck pCEarplugsResilienceCheck)
        {
            //
            // todo: add other logic here.
            //

                try
                {
                    BL.V.BeginTransaction();

                    Validate(pCEarplugsResilienceCheck);
                    pCEarplugsResilienceCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCEarplugsResilienceCheck);

                accessorDetail.DeleteByHeaderId(pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId);
                foreach (var item in pCEarplugsResilienceCheck.Details)
                {
                    item.PCEarplugsResilienceCheckId = pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId;

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
            return "PCER";
        }

        protected override string GetSettingId()
        {
            return "PCEarplugsResilienceCheckRule";
        }

        public void Validate(Model.PCEarplugsResilienceCheck model)
        {
            if (model.PCEarplugsResilienceCheckDate == null)
                throw new Helper.InvalidValueException(Model.PCEarplugsResilienceCheck.PRO_PCEarplugsResilienceCheckDate);
        }

        private void TiGuiExists(Model.PCEarplugsResilienceCheck model)
        {
            if (this.ExistsPrimary(model.PCEarplugsResilienceCheckId))
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
                model.PCEarplugsResilienceCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCEarplugsResilienceCheck GetDetail(string pCEarplugsResilienceCheckId)
        {
            Model.PCEarplugsResilienceCheck model = this.Get(pCEarplugsResilienceCheckId);
            if (model != null)
                model.Details = accessorDetail.SelectByHeaderId(pCEarplugsResilienceCheckId);
            return model;
        }
    }
}
