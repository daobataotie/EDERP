//------------------------------------------------------------------------------
//
// file name：BillIdDeletedAccessor.cs
// author: mayanjun
// create date：2015/11/20 18:56:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of BillIdDeleted
    /// </summary>
    public partial class BillIdDeletedAccessor : EntityAccessor, IBillIdDeletedAccessor
    {
        public string SelectBillIdByBillIdSetId(string id)
        {
            return sqlmapper.QueryForObject<string>("BillIdDeleted.SelectBillIdByBillIdSetId", id);
        }
    }
}
