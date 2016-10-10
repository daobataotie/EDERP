//------------------------------------------------------------------------------
//
// file name：BillIdSetAccessor.cs
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
    /// Data accessor of BillIdSet
    /// </summary>
    public partial class BillIdSetAccessor : EntityAccessor, IBillIdSetAccessor
    {
        public IList<Model.BillIdSet> SelectAll()
        {
            return sqlmapper.QueryForList<Model.BillIdSet>("BillIdSet.SelectAll", null);
        }

        public void DisableAll()
        {
            sqlmapper.Update("BillIdSet.DisableAll", null);
        }

        public Model.BillIdSet SelectEnable()
        {
            return sqlmapper.QueryForObject<Model.BillIdSet>("BillIdSet.SelectEnable", null);
        }
    }
}
