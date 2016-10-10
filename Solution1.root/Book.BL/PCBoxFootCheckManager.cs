//------------------------------------------------------------------------------
//
// file name：PCBoxFootCheckManager.cs
// author: mayanjun
// create date：2013-1-28 15:42:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCBoxFootCheck.
    /// </summary>
    public partial class PCBoxFootCheckManager : BaseManager
    {

        /// <summary>
        /// Delete PCBoxFootCheck by primary key.
        /// </summary>
        public void Delete(string pCBoxFootCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(pCBoxFootCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a PCBoxFootCheck.
        /// </summary>
        public void Insert(Model.PCBoxFootCheck pCBoxFootCheck)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                pCBoxFootCheck.InsertTime = DateTime.Now;
                pCBoxFootCheck.UpdateTime = DateTime.Now;
                accessor.Insert(pCBoxFootCheck);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a PCBoxFootCheck.
        /// </summary>  
        public void Update(Model.PCBoxFootCheck pCBoxFootCheck)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                pCBoxFootCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCBoxFootCheck);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        protected override string GetInvoiceKind()
        {
            return "BFC";
        }

        protected override string GetSettingId()
        {
            return "PCBoxFootCheckRule";
        }

        public void TiGuiExists(Model.PCBoxFootCheck model)
        {
            if (this.ExistsPrimary(model.PCBoxFootCheckId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.PCBoxFootCheckId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }
        }

        public IList<Model.PCBoxFootCheck> SelectByRage(DateTime StartDate, DateTime EndDate, string InvoiceXOId, string PronoteHeaderId, Model.Product product)
        {
            return accessor.SelectByRage(StartDate, EndDate, InvoiceXOId, PronoteHeaderId, product);
        }
    }
}

