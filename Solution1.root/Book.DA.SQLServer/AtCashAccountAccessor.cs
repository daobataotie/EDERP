//------------------------------------------------------------------------------
//
// file name：AtCashAccountAccessor.cs
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
    /// Data accessor of AtCashAccount
    /// </summary>
    public partial class AtCashAccountAccessor : EntityAccessor, IAtCashAccountAccessor
    {
        public bool ISExistsMonth(Model.AtCashAccount model)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Date", model.AtCashAccountMonth);
            ht.Add("Id", model.AtCashAccountId);
            return sqlmapper.QueryForObject<bool>("AtCashAccount.ISExistsMonth", ht);
        }

        public decimal GetBalance()
        {
            return sqlmapper.QueryForObject<decimal>("AtCashAccount.GetBalance", null);
        }

        public decimal GetBalanceByDate(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForObject<decimal>("AtCashAccount.GetBalanceByDate", ht);
        }

        public DataTable CountIncomeAndPay(DateTime startdate, DateTime enddate)
        {
            string sql = "select isnull(sum(IncomeTotal),0)as Income,isnull(sum(PayTotal),0) as Pay from AtCashAccount where AtCashAccountMonth between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            sda.Fill(dt);
            return dt;
        }
    }
}
