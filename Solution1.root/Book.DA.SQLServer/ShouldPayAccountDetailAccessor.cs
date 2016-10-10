//------------------------------------------------------------------------------
//
// file name：ShouldPayAccountDetailAccessor.cs
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
    /// Data accessor of ShouldPayAccountDetail
    /// </summary>
    public partial class ShouldPayAccountDetailAccessor : EntityAccessor, IShouldPayAccountDetailAccessor
    {
        public IList<Model.ShouldPayAccountDetail> GetByDateRangeAndSupplier(DateTime startDate, DateTime endDate, string supplierID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("supplierID", supplierID);
            return sqlmapper.QueryForList<Model.ShouldPayAccountDetail>("ShouldPayAccountDetail.GetByDateRangeAndSupplier", ht);
        }

        public IList<Model.ShouldPayAccountDetail> GetByHeaderId(string Id)
        {
            return sqlmapper.QueryForList<Model.ShouldPayAccountDetail>("ShouldPayAccountDetail.GetByHeaderId", Id);
        }

        public void DeleteByHeaderId(string id)
        {
            sqlmapper.Delete("ShouldPayAccountDetail.DeleteByHeaderId", id);
        }
    }
}
