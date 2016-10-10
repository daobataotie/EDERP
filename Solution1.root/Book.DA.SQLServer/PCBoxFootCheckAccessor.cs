//------------------------------------------------------------------------------
//
// file name：PCBoxFootCheckAccessor.cs
// author: mayanjun
// create date：2013-1-28 15:42:34
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
    /// Data accessor of PCBoxFootCheck
    /// </summary>
    public partial class PCBoxFootCheckAccessor : EntityAccessor, IPCBoxFootCheckAccessor
    {
        public IList<Model.PCBoxFootCheck> SelectByRage(DateTime StartDate, DateTime EndDate, string InvoiceXOId, string PronoteHeaderId, Model.Product product)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate.ToString("yyyy-MM-dd"));
            ht.Add("EndDate", EndDate.ToString("yyyy-MM-dd"));

            StringBuilder sql = new StringBuilder();
            if (InvoiceXOId != null)
                sql.Append("AND InvoiceXOId='" + InvoiceXOId + "'");
            if (PronoteHeaderId != null)
                sql.Append(" AND PronoteHeaderId='" + PronoteHeaderId + "'");
            if (product != null)
                sql.Append(" AND ProductId='" + product.ProductId + "'");
            sql.Append(" ORDER BY CheckDate desc");
            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.PCBoxFootCheck>("PCBoxFootCheck.SelectByRage", ht);
        }
    }
}
