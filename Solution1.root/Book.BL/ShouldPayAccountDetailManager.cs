//------------------------------------------------------------------------------
//
// file name：ShouldPayAccountDetailManager.cs
// author: mayanjun
// create date：2014/7/16 22:02:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ShouldPayAccountDetail.
    /// </summary>
    public partial class ShouldPayAccountDetailManager
    {

        /// <summary>
        /// Delete ShouldPayAccountDetail by primary key.
        /// </summary>
        public void Delete(string shouldPayAccountDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(shouldPayAccountDetailId);
        }

        /// <summary>
        /// Insert a ShouldPayAccountDetail.
        /// </summary>
        public void Insert(Model.ShouldPayAccountDetail shouldPayAccountDetail)
        {
            //
            // todo:add other logic here
            //
            this.ValiDate(shouldPayAccountDetail);
            shouldPayAccountDetail.InsertTime = DateTime.Now;
            shouldPayAccountDetail.UpdateTime = DateTime.Now;
            accessor.Insert(shouldPayAccountDetail);
        }

        /// <summary>
        /// Update a ShouldPayAccountDetail.
        /// </summary>
        public void Update(Model.ShouldPayAccountDetail shouldPayAccountDetail)
        {
            //
            // todo: add other logic here.
            //
            this.ValiDate(shouldPayAccountDetail);
            shouldPayAccountDetail.UpdateTime = DateTime.Now;
            accessor.Update(shouldPayAccountDetail);
        }

        public IList<Model.ShouldPayAccountDetail> GetByDateRangeAndSupplier(DateTime startDate, DateTime endDate, string supplierID)
        {
            return accessor.GetByDateRangeAndSupplier(startDate, endDate, supplierID);
        }

        public IList<Model.ShouldPayAccountDetail> GetByHeaderId(string Id)
        {
            return accessor.GetByHeaderId(Id);
        }

        public void DeleteByHeaderId(string id)
        {
            accessor.DeleteByHeaderId(id);
        }

        public void ValiDate(Model.ShouldPayAccountDetail shouldPayAccountDetail)
        {
            if (string.IsNullOrEmpty(shouldPayAccountDetail.FPId))
                throw new Helper.MessageValueException("發票編號不能為空！");
        }
    }
}
