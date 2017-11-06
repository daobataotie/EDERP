//------------------------------------------------------------------------------
//
// file name：PCAssemblyInspectionManager.cs
// author: mayanjun
// create date：2017-11-05 16:28:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCAssemblyInspection.
    /// </summary>
    public partial class PCAssemblyInspectionManager : BaseManager
    {
        private static readonly DA.IPCAssemblyInspectionDetailAccessor accessorDetail = (DA.IPCAssemblyInspectionDetailAccessor)Accessors.Get("PCSamplingDetailAccessor");
		
		/// <summary>
		/// Delete PCAssemblyInspection by primary key.
		/// </summary>
		public void Delete(string pCAssemblyInspectionId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pCAssemblyInspectionId);
		}

        public void Delete(Model.PCAssemblyInspection model)
        {
            if (model != null)
                this.Delete(model.PCAssemblyInspectionId);
        }

		/// <summary>
		/// Insert a PCAssemblyInspection.
		/// </summary>
        public void Insert(Model.PCAssemblyInspection pCAssemblyInspection)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pCAssemblyInspection);
        }
		
		/// <summary>
		/// Update a PCAssemblyInspection.
		/// </summary>
        public void Update(Model.PCAssemblyInspection pCAssemblyInspection)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pCAssemblyInspection);
        }

        protected override string GetInvoiceKind()
        {
            return "PCA";
        }

        protected override string GetSettingId()
        {
            return "PCAssemblyInspection";
        }

        public void Validate(Model.PCAssemblyInspection model)
        {
            if (model.PCAssemblyInspectionDate == null)
                throw new Helper.InvalidValueException(Model.PCAssemblyInspection.PRO_PCAssemblyInspectionDate);
            if (string.IsNullOrEmpty(model.PronoteHeaderId))
                throw new Helper.InvalidValueException(Model.PCAssemblyInspection.PRO_PronoteHeaderId);

        }

        private void TiGuiExists(Model.PCAssemblyInspection model)
        {
            if (this.ExistsPrimary(model.PCAssemblyInspectionId))
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
                model.PCAssemblyInspectionId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCAssemblyInspection GetDetail(string id)
        {
            Model.PCAssemblyInspection model = this.Get(id);
            if (model != null)
                model.Details = accessorDetail.SelectByHeaderId(id);
            return model;
        }
    }
}
