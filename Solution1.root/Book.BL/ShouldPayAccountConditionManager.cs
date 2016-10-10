//------------------------------------------------------------------------------
//
// file name：ShouldPayAccountConditionManager.cs
// author: mayanjun
// create date：2014/11/19 18:29:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ShouldPayAccountCondition.
    /// </summary>
    public partial class ShouldPayAccountConditionManager
    {

        /// <summary>
        /// Delete ShouldPayAccountCondition by primary key.
        /// </summary>
        public void Delete(string shouldPayAccountConditionId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(shouldPayAccountConditionId);
        }

        /// <summary>
        /// Insert a ShouldPayAccountCondition.
        /// </summary>
        public void Insert(Model.ShouldPayAccountCondition shouldPayAccountCondition)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Insert(shouldPayAccountCondition);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a ShouldPayAccountCondition.
        /// </summary>
        public void Update(Model.ShouldPayAccountCondition shouldPayAccountCondition)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Update(shouldPayAccountCondition);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
    }
}
