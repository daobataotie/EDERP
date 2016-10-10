//------------------------------------------------------------------------------
//
// file name：BillIdDeletedManager.cs
// author: mayanjun
// create date：2015/11/20 18:56:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BillIdDeleted.
    /// </summary>
    public partial class BillIdDeletedManager
    {

        /// <summary>
        /// Delete BillIdDeleted by primary key.
        /// </summary>
        public void Delete(string billId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(billId);
        }

        /// <summary>
        /// Insert a BillIdDeleted.
        /// </summary>
        public void Insert(Model.BillIdDeleted billIdDeleted)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(billIdDeleted);
        }

        /// <summary>
        /// Update a BillIdDeleted.
        /// </summary>
        public void Update(Model.BillIdDeleted billIdDeleted)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(billIdDeleted);
        }

        public string SelectBillIdByBillIdSetId(string id)
        {
            return accessor.SelectBillIdByBillIdSetId(id);
        }
    }
}
