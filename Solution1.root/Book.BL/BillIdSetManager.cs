//------------------------------------------------------------------------------
//
// file name：BillIdSetManager.cs
// author: mayanjun
// create date：2015/11/20 18:56:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BillIdSet.
    /// </summary>
    public partial class BillIdSetManager
    {

        /// <summary>
        /// Delete BillIdSet by primary key.
        /// </summary>
        public void Delete(string billIdSetId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(billIdSetId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a BillIdSet.
        /// </summary>
        public void Insert(Model.BillIdSet billIdSet)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(billIdSet);
                billIdSet.InsertTime = DateTime.Now;
                billIdSet.UpdatetTime = DateTime.Now;
                //如果这个编号启用，则其他所有失效
                if (billIdSet.IdState.HasValue && billIdSet.IdState.Value)
                    this.DisableAll();

                accessor.Insert(billIdSet);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a BillIdSet.
        /// </summary>
        public void Update(Model.BillIdSet billIdSet)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(billIdSet);
                billIdSet.UpdatetTime = DateTime.Now;
                if (billIdSet.IdState.HasValue && billIdSet.IdState.Value)
                    this.DisableAll();

                accessor.Update(billIdSet);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void UpdateIdnumber(Model.BillIdSet billIdSet)
        {
            accessor.Update(billIdSet);
        }

        private void Validate(Model.BillIdSet billIdSet)
        {
            if (string.IsNullOrEmpty(billIdSet.EnglishId))
                throw new Helper.InvalidValueException(Model.BillIdSet.PRO_EnglishId);
            if (string.IsNullOrEmpty(billIdSet.StartBillId))
                throw new Helper.InvalidValueException(Model.BillIdSet.PRO_StartBillId);
            if (string.IsNullOrEmpty(billIdSet.EndBillId))
                throw new Helper.InvalidValueException(Model.BillIdSet.PRO_EndBillId);
            if (billIdSet.StartDate == null)
                throw new Helper.InvalidValueException(Model.BillIdSet.PRO_StartDate);
            if (billIdSet.EndDate == null)
                throw new Helper.InvalidValueException(Model.BillIdSet.PRO_EndDate);

            if (billIdSet.EnglishId.Length != 2)
                throw new Helper.RequireValueException(Model.BillIdSet.PRO_EnglishId + "2");

            if (Convert.ToInt32(billIdSet.EndBillId) - Convert.ToInt32(billIdSet.StartBillId) <= 0)
                throw new Helper.RequireValueException(Model.BillIdSet.PRO_EndBillId + "2");
        }

        public IList<Model.BillIdSet> SelectAll()
        {
            return accessor.SelectAll();
        }

        public void DisableAll()
        {
            accessor.DisableAll();
        }

        public Model.BillIdSet SelectEnable()
        {
            return accessor.SelectEnable();
        }
    }
}
