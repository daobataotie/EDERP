//------------------------------------------------------------------------------
//
// file name：PCPGOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-12-6 14:34:43
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
    /// Data accessor of PCPGOnlineCheckDetail
    /// </summary>
    public partial class PCPGOnlineCheckDetailAccessor : EntityAccessor, IPCPGOnlineCheckDetailAccessor
    {
        public IList<Book.Model.PCPGOnlineCheckDetail> Select(string pcpgocId)
        {
            return sqlmapper.QueryForList<Model.PCPGOnlineCheckDetail>("PCPGOnlineCheckDetail.select_byPCPGOnlineCheckId", pcpgocId);
        }

        public void DeleteByPCPGOnlineCheckId(string IPCPGOnlineCheckId)
        {
            sqlmapper.Delete("PCPGOnlineCheckDetail.DeleteByPCPGOnlineCheckId", IPCPGOnlineCheckId);
        }

        public IList<Book.Model.PCPGOnlineCheckDetail> SelectByFromInvoiceId(string id)
        {
            return sqlmapper.QueryForList<Model.PCPGOnlineCheckDetail>("PCPGOnlineCheckDetail.SelectByFromInvoiceId", id);
        }

        public string GetTimerListString(string PCPGOnlineCheckId)
        {
            IList<Model.PCPGOnlineCheckDetail> al = sqlmapper.QueryForList<Model.PCPGOnlineCheckDetail>("PCPGOnlineCheckDetail.GetTimerListString", PCPGOnlineCheckId);
            string resultstr = string.Empty;
            foreach (Model.PCPGOnlineCheckDetail item in al)
            {
                resultstr += item.PCPGOnlineCheckDetailDate.Value.ToString() + ",";
            }
            return resultstr;
        }

        public string SelectByInvoiceCusID(string ID)
        {
            //return sqlmapper.QueryForObject<string>("PCPGOnlineCheckDetail.SelectByInvoiceCusID", ID);
            string sql = " select distinct Cast(pd.PCPGOnlineCheckId as varchar) + ' ' from PCPGOnlineCheckDetail pd left join PCPGOnlineCheck p on pd.PCPGOnlineCheckId=p.PCPGOnlineCheckId  where ISNULL(p.InvoiceCusXOId,'')= '" + ID + " '  or pd.FromInvoiceId in (select PronoteHeaderID from PronoteHeader where InvoiceXOId =(select InvoiceId from InvoiceXO where CustomerInvoiceXOId= '" + ID + " ' ))  or pd.FromInvoiceId in (select ProduceOtherCompactId from ProduceOtherCompact where InvoiceXOId =(select InvoiceId from InvoiceXO where CustomerInvoiceXOId= '" + ID + " ' ))   or pd.FromInvoiceId in (select InvoiceId from InvoiceCO where InvoiceXOId =(select InvoiceId from InvoiceXO where CustomerInvoiceXOId= '" + ID + " ' )) for xml path('')";

            if (SQLDB.SqlHelper.ExecuteScalar(sqlmapper.DataSource.ConnectionString, CommandType.Text, sql, null) != DBNull.Value && SQLDB.SqlHelper.ExecuteScalar(sqlmapper.DataSource.ConnectionString, CommandType.Text, sql, null) != null)
                return SQLDB.SqlHelper.ExecuteScalar(sqlmapper.DataSource.ConnectionString, CommandType.Text, sql, null).ToString();
            else
                return null;
        }

        public DataTable SelectByHeaderId(string id)
        {
            string sql = "SELECT pc.*,p.ProductName FROM PCPGOnlineCheckDetail pc left join Product p on pc.ProductId=p.ProductId WHERE pc.PCPGOnlineCheckId = '" + id + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            sda.Fill(dt);
            return dt;
        }
    }
}
