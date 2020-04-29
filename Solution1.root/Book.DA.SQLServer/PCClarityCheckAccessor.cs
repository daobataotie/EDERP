//------------------------------------------------------------------------------
//
// file name：PCClarityCheckAccessor.cs
// author: mayanjun
// create date：2013-08-19 15:44:12
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
    /// Data accessor of PCClarityCheck
    /// </summary>
    public partial class PCClarityCheckAccessor : EntityAccessor, IPCClarityCheckAccessor
    {
        public IList<Model.PCClarityCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate);
            ht.Add("EndDate", EndDate);
            return sqlmapper.QueryForList<Model.PCClarityCheck>("PCClarityCheck.SelectByDateRage", ht);
        }

        public DataTable SelectByProductName(string productName)
        {
            int length = productName.Length;
            string sql = "select pcc.PCClarityCheckId,pcc.CheckDate,pcc.PronoteHeaderId,p.ProductName,ppd.PronoteMachineId from PCClarityCheck pcc left join PronoteProceduresDetail ppd on ppd.PronoteHeaderID=pcc.PronoteHeaderID left join Product p on pcc.ProductId=p.ProductId where pcc.ProductId in (select ProductId from Product where left(ProductName," + length + ")='" + productName + "') and ppd.PronoteMachineId is not null and ppd.PronoteMachineId<>''";

            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            return dt;
        }
    }
}
