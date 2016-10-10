//------------------------------------------------------------------------------
//
// file name：AtCashAccountDetailAccessor.cs
// author: mayanjun
// create date：2014/8/11 19:50:50
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
    /// Data accessor of AtCashAccountDetail
    /// </summary>
    public partial class AtCashAccountDetailAccessor : EntityAccessor, IAtCashAccountDetailAccessor
    {
        public IList<Model.AtCashAccountDetail> SelectByHeaderId(string Id)
        {
            return sqlmapper.QueryForList<Model.AtCashAccountDetail>("AtCashAccountDetail.SelectByHeaderId", Id);
        }

        public void DeleteByHeaderId(string Id)
        {
            sqlmapper.Delete("AtCashAccountDetail.DeleteByHeaderId", Id);
        }

        public IList<Model.AtCashAccountDetail> SelectByDate(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            return sqlmapper.QueryForList<Model.AtCashAccountDetail>("AtCashAccountDetail.SelectByDate", ht);
        }
    }
}
