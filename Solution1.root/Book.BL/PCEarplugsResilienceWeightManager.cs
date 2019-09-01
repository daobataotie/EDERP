//------------------------------------------------------------------------------
//
// file name：PCEarplugsResilienceWeightManager.cs
// author: mayanjun
// create date：2019/8/25 22:18:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarplugsResilienceWeight.
    /// </summary>
    public partial class PCEarplugsResilienceWeightManager
    {

        /// <summary>
        /// Delete PCEarplugsResilienceWeight by primary key.
        /// </summary>
        public void Delete(string pCEarplugsResilienceWeightId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(pCEarplugsResilienceWeightId);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Insert a PCEarplugsResilienceWeight.
        /// </summary>
        public void Insert(Model.PCEarplugsResilienceWeight pCEarplugsResilienceWeight)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                pCEarplugsResilienceWeight.InsertTime = DateTime.Now;
                pCEarplugsResilienceWeight.UpdateTime = DateTime.Now;
                accessor.Insert(pCEarplugsResilienceWeight);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Update a PCEarplugsResilienceWeight.
        /// </summary>
        public void Update(Model.PCEarplugsResilienceWeight pCEarplugsResilienceWeight)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                pCEarplugsResilienceWeight.UpdateTime = DateTime.Now;
                accessor.Update(pCEarplugsResilienceWeight);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        private void Validate(Model.PCEarplugsResilienceWeight pCEarplugsResilienceWeight)
        {
            if (string.IsNullOrEmpty(pCEarplugsResilienceWeight.PCEarplugsResilienceCheckId))
            {
                throw new Helper.RequireValueException(Model.PCEarplugsResilienceWeight.PRO_PCEarplugsResilienceCheckId);
            }
        }

        public bool mHasRows(string id)
        {
            return accessor.mHasRows(id);
        }

        public Model.PCEarplugsResilienceWeight mGetLast(string id)
        {
            return accessor.mGetLast(id);
        }
    }
}
