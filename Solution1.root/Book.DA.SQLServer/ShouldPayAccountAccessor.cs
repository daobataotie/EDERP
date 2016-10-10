//------------------------------------------------------------------------------
//
// file name：ShouldPayAccountAccessor.cs
// author: mayanjun
// create date：2014/7/16 22:02:39
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
    /// Data accessor of ShouldPayAccount
    /// </summary>
    public partial class ShouldPayAccountAccessor : EntityAccessor, IShouldPayAccountAccessor
    {
        public IList<Model.ShouldPayAccount> SelectByCondition(DateTime startdate, DateTime enddate, string supplierid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" and InsertTime between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(supplierid))
                sql.Append(" and SupplierId='" + supplierid + "'");
            return sqlmapper.QueryForList<Model.ShouldPayAccount>("ShouldPayAccount.SelectByCondition", sql.ToString());
        }
    }
}
