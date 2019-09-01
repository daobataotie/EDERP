//------------------------------------------------------------------------------
//
// file name：PCEarplugsResilienceConditionSetManager.cs
// author: mayanjun
// create date：2019/6/15 19:29:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarplugsResilienceConditionSet.
    /// </summary>
    public partial class PCEarplugsResilienceConditionSetManager
    {

        /// <summary>
        /// Delete PCEarplugsResilienceConditionSet by primary key.
        /// </summary>
        public void Delete(string pCEarplugsResilienceConditionSetId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(pCEarplugsResilienceConditionSetId);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Insert a PCEarplugsResilienceConditionSet.
        /// </summary>
        public void Insert(Model.PCEarplugsResilienceConditionSet pCEarplugsResilienceConditionSet)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                pCEarplugsResilienceConditionSet.InsertTime = DateTime.Now;
                pCEarplugsResilienceConditionSet.UpdateTime = DateTime.Now;
                accessor.Insert(pCEarplugsResilienceConditionSet);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Update a PCEarplugsResilienceConditionSet.
        /// </summary>
        public void Update(Model.PCEarplugsResilienceConditionSet pCEarplugsResilienceConditionSet)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                pCEarplugsResilienceConditionSet.UpdateTime = DateTime.Now;
                accessor.Update(pCEarplugsResilienceConditionSet);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        private void Validate(Model.PCEarplugsResilienceConditionSet pCEarplugsResilienceConditionSet)
        {
            if (string.IsNullOrEmpty(pCEarplugsResilienceConditionSet.PCEarplugsResilienceCheckId))
            {
                throw new Helper.RequireValueException(Model.PCEarplugsResilienceConditionSet.PRO_PCEarplugsResilienceCheckId);
            }
        }

        public bool mHasRows(string id)
        {
            return accessor.mHasRows(id);
        }

        public Model.PCEarplugsResilienceConditionSet mGetLast(string id)
        {
            return accessor.mGetLast(id);
        }
    }
}
