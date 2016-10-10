//------------------------------------------------------------------------------
//
// file name:InvoiceCTDetailAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of InvoiceCTDetail
    /// </summary>
    public partial class InvoiceCTDetailAccessor : EntityAccessor, IInvoiceCTDetailAccessor
    {
        #region IInvoiceCTDetailAccessor 成员

        public IList<Book.Model.InvoiceCTDetail> Select(Book.Model.InvoiceCT invoiceCT)
        {
            return sqlmapper.QueryForList<Model.InvoiceCTDetail>("InvoiceCTDetail.select_by_invoiceid", invoiceCT.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceCT invoice)
        {
            sqlmapper.Delete("InvoiceCTDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public DataTable SelectByCondition(DateTime dateStart, DateTime dateEnd, string ctStart, string ctEnd, string coStart, string coEnd, string CusId, string supplierid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ctd.InvoiceId,ct.InvoiceDate,s.SupplierFullName,p.ProductName,ctd.InvoiceCTDetailQuantity,ctd.InvoiceProductUnit,ctd.InvoiceCTDetailPrice,ctd.InvoiceCTDetailMoney0 from InvoiceCTDetail ctd left join InvoiceCT ct on ct.InvoiceId=ctd.InvoiceId left join InvoiceCO co on co.InvoiceId=ctd.InvoiceCOId left join InvoiceXO xo on co.InvoiceXOId=xo.InvoiceId left join Product p on p.ProductId=ctd.ProductId left join Supplier s on ct.SupplierId=s.SupplierId where 1=1");
            sql.Append(" and ct.InvoiceDate between '" + dateStart.Date.ToString("yyyy-MM-dd") + "' and '" + dateEnd.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(ctStart) || !string.IsNullOrEmpty(ctEnd))
            {
                if (!string.IsNullOrEmpty(ctStart) && !string.IsNullOrEmpty(ctEnd))
                    sql.Append(" and ct.InvoiceId between '" + ctStart + "' and '" + ctEnd + "'");
                else
                    sql.Append(" and ct.InvoiceId='" + (string.IsNullOrEmpty(ctStart) ? ctEnd : ctStart) + "'");
            }
            if (!string.IsNullOrEmpty(coStart) || !string.IsNullOrEmpty(coEnd))
            {
                if (!string.IsNullOrEmpty(coStart) && !string.IsNullOrEmpty(coEnd))
                    sql.Append(" and co.InvoiceId between '" + coStart + "' and '" + coEnd + "'");
                else
                    sql.Append(" and co.InvoiceId='" + (string.IsNullOrEmpty(coStart) ? coEnd : coStart) + "'");
            }
            if (!string.IsNullOrEmpty(CusId))
                sql.Append(" and xo.InvoiceId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + CusId + "')");
            if (!string.IsNullOrEmpty(supplierid))
                sql.Append(" and ct.SupplierId='" + supplierid + "'");
            //return sqlmapper.QueryForList<Model.InvoiceCTDetail>("InvoiceCTDetail.SelectByCondition", sql.ToString());
            DataTable dt=new DataTable();
            SqlDataAdapter sda=new SqlDataAdapter(sql.ToString(),sqlmapper.DataSource.ConnectionString);
            sda.Fill(dt);
            return dt;
        }

        #endregion
    }
}
