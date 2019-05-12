//------------------------------------------------------------------------------
//
// file name：PCEarplugsResilienceCheckDetailManager.cs
// author: mayanjun
// create date：2019/5/10 10:49:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarplugsResilienceCheckDetail.
    /// </summary>
    public partial class PCEarplugsResilienceCheckDetailManager
    {

        /// <summary>
        /// Delete PCEarplugsResilienceCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCEarplugsResilienceCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCEarplugsResilienceCheckDetailId);
        }

        /// <summary>
        /// Insert a PCEarplugsResilienceCheckDetail.
        /// </summary>
        public void Insert(Model.PCEarplugsResilienceCheckDetail pCEarplugsResilienceCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCEarplugsResilienceCheckDetail);
        }

        /// <summary>
        /// Update a PCEarplugsResilienceCheckDetail.
        /// </summary>
        public void Update(Model.PCEarplugsResilienceCheckDetail pCEarplugsResilienceCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCEarplugsResilienceCheckDetail);
        }

        public IList<Model.PCEarplugsResilienceCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId)
        {
            return accessor.SelectByDateRage(startDate, endDate, productId, cusXOId);
        }
    }
}
