//------------------------------------------------------------------------------
//
// file name：PCEarplugsStayWireCheckDetailManager.cs
// author: mayanjun
// create date：2019/5/14 09:57:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarplugsStayWireCheckDetail.
    /// </summary>
    public partial class PCEarplugsStayWireCheckDetailManager
    {

        /// <summary>
        /// Delete PCEarplugsStayWireCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCEarplugsStayWireCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCEarplugsStayWireCheckDetailId);
        }

        /// <summary>
        /// Insert a PCEarplugsStayWireCheckDetail.
        /// </summary>
        public void Insert(Model.PCEarplugsStayWireCheckDetail pCEarplugsStayWireCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCEarplugsStayWireCheckDetail);
        }

        /// <summary>
        /// Update a PCEarplugsStayWireCheckDetail.
        /// </summary>
        public void Update(Model.PCEarplugsStayWireCheckDetail pCEarplugsStayWireCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCEarplugsStayWireCheckDetail);
        }

        public IList<Model.PCEarplugsStayWireCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId)
        {
            return accessor.SelectByDateRage(startDate, endDate, productId, cusXOId);
        }
    }
}
