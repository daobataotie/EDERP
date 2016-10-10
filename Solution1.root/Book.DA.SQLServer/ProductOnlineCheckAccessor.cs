//------------------------------------------------------------------------------
//
// file name：ProductOnlineCheckAccessor.cs
// author: mayanjun
// create date：2013-3-25 17:50:57
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
    /// Data accessor of ProductOnlineCheck
    /// </summary>
    public partial class ProductOnlineCheckAccessor : EntityAccessor, IProductOnlineCheckAccessor
    {
        public IList<Model.ProductOnlineCheck> SelectByDate(DateTime startDate, DateTime endDate, string invoiceCusId)
        {
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(invoiceCusId))
                sql.Append(" And InvoiceXOId=(select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + invoiceCusId + "') or (PronoteHeaderId in (select PronoteHeaderID from PronoteHeader where InvoiceXOId =(select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + invoiceCusId + "')))");

            Hashtable ht = new Hashtable();
            ht.Add("StartDate", startDate);
            ht.Add("EndDate", endDate);
            ht.Add("sql", sql.ToString());
            return sqlmapper.QueryForList<Model.ProductOnlineCheck>("ProductOnlineCheck.SelectByDate", ht);
        }

        public string SelectByInvoiceCusID(string ID)
        {
            return sqlmapper.QueryForObject<string>("ProductOnlineCheck.SelectByInvoiceCusID", ID);
        }
    }
}
