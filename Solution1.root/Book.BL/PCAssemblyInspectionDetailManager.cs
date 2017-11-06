//------------------------------------------------------------------------------
//
// file name：PCAssemblyInspectionDetailManager.cs
// author: mayanjun
// create date：2017-11-05 16:28:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCAssemblyInspectionDetail.
    /// </summary>
    public partial class PCAssemblyInspectionDetailManager
    {

        /// <summary>
        /// Delete PCAssemblyInspectionDetail by primary key.
        /// </summary>
        public void Delete(string pCAssemblyInspectionDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCAssemblyInspectionDetailId);
        }

        /// <summary>
        /// Insert a PCAssemblyInspectionDetail.
        /// </summary>
        public void Insert(Model.PCAssemblyInspectionDetail pCAssemblyInspectionDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCAssemblyInspectionDetail);
        }

        /// <summary>
        /// Update a PCAssemblyInspectionDetail.
        /// </summary>
        public void Update(Model.PCAssemblyInspectionDetail pCAssemblyInspectionDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCAssemblyInspectionDetail);
        }

        public IList<Model.PCAssemblyInspectionDetail> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }

        public void DeleteByHeaderId(string id)
        {
            accessor.DeleteByHeaderId(id);
        }

        public IList<Model.PCAssemblyInspectionDetail> SelectByCondition(DateTime startDate, DateTime endDate, string startPId, string endPId, string invoiceCusId)
        {
            return accessor.SelectByCondition(startDate, endDate, startPId, endPId, invoiceCusId);
        }
    }
}
