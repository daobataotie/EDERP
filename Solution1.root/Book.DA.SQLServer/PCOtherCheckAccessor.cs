//------------------------------------------------------------------------------
//
// file name：PCOtherCheckAccessor.cs
// author: mayanjun
// create date：2011-11-10 15:05:57
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
    /// Data accessor of PCOtherCheck
    /// </summary>
    public partial class PCOtherCheckAccessor : EntityAccessor, IPCOtherCheckAccessor
    {
        public IList<Book.Model.PCOtherCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", StartDate);
            ht.Add("enddate", EndDate);
            StringBuilder sql = new StringBuilder();
            //if (customer != null)
            //    sql.Append(" and InvoiceCusXOId IN (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE xocustomerId = '" + customer.CustomerId + "')");
            if (!string.IsNullOrEmpty(CusXOId))
                sql.Append(" and PCOtherCheckId in (select PCOtherCheckId from PCOtherCheckDetail where FromInvoiceID in (select InvoiceId from InvoiceCGDetail where InvoiceCOId in (select InvoiceId from InvoiceCO where InvoiceXOId=(select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + CusXOId + "'))) or FromInvoiceID in (select ProduceOtherInDepotId from ProduceOtherInDepotDetail where ProduceOtherCompactId in (select ProduceOtherCompactId from ProduceOtherCompact where InvoiceXOId =(select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + CusXOId + "'))))");
            //if (product != null)
            //    sql.Append(" and ProductId = '" + product.ProductId + "'");
            sql.Append(" ORDER BY PCOtherCheckId desc");
            ht.Add("sql", sql.ToString());
            IList<Model.PCOtherCheck> a = sqlmapper.QueryForList<Model.PCOtherCheck>("PCOtherCheck.SelectByDateRange", ht);
            return a;
        }
    }
}
