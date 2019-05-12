//------------------------------------------------------------------------------
//
// file name：PCEarplugsDecibelCheckDetailManager.cs
// author: mayanjun
// create date：2019/5/12 14:43:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarplugsDecibelCheckDetail.
    /// </summary>
    public partial class PCEarplugsDecibelCheckDetailManager
    {

        /// <summary>
        /// Delete PCEarplugsDecibelCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCEarplugsDecibelCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCEarplugsDecibelCheckDetailId);
        }

        /// <summary>
        /// Insert a PCEarplugsDecibelCheckDetail.
        /// </summary>
        public void Insert(Model.PCEarplugsDecibelCheckDetail pCEarplugsDecibelCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCEarplugsDecibelCheckDetail);
        }

        /// <summary>
        /// Update a PCEarplugsDecibelCheckDetail.
        /// </summary>
        public void Update(Model.PCEarplugsDecibelCheckDetail pCEarplugsDecibelCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCEarplugsDecibelCheckDetail);
        }

        public IList<Model.PCEarplugsDecibelCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId)
        {
            return accessor.SelectByDateRage(startDate, endDate, productId, cusXOId);
        }
    }
}
