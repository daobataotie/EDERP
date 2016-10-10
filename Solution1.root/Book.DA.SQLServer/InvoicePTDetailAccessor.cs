//------------------------------------------------------------------------------
//
// file name:InvoicePTDetailAccessor.cs
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
    /// Data accessor of InvoicePTDetail
    /// </summary>
    public partial class InvoicePTDetailAccessor : EntityAccessor, IInvoicePTDetailAccessor
    {
        #region IInvoicePTDetailAccessor 成员


        public IList<Book.Model.InvoicePTDetail> Select(Book.Model.InvoicePT invoicePT)
        {
            return sqlmapper.QueryForList<Model.InvoicePTDetail>("InvoicePTDetail.select_by_invoiceid", invoicePT.InvoiceId);
        }

        public void Delete(Book.Model.InvoicePT invoice)
        {
            sqlmapper.Delete("InvoicePTDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Book.Model.InvoicePTDetail> SelectByConditon(DateTime startTime, DateTime endTime, string invoiceId, string employeeId, string depot, string depotIn, string productId)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", startTime);
            pars.Add("endTime", endTime);

            StringBuilder sql = new StringBuilder();
            if (invoiceId != null)
                sql.Append(" and i.InvoiceId='" + invoiceId + "'");
            if (employeeId != null)
                sql.Append(" and i.Employee0Id='" + employeeId + "'");
            if (depot != null)
                sql.Append(" and i.DepotId='" + depot + "'");
            if (depotIn != null)
                sql.Append(" and i.DepotInId='" + depotIn + "'");
            if (productId != null)
                sql.Append(" and d.ProductId='" + productId + "'");
            pars.Add("sql", sql);

            return sqlmapper.QueryForList<Model.InvoicePTDetail>("InvoicePTDetail.SelectByConditon", pars);
        }

        #endregion
    }
}
