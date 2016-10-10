//------------------------------------------------------------------------------
//
// file name：ANSIPCImpactCheckAccessor.cs
// author: mayanjun
// create date：2011-11-23 09:49:54
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
    /// Data accessor of ANSIPCImpactCheck
    /// </summary>
    public partial class ANSIPCImpactCheckAccessor : EntityAccessor, IANSIPCImpactCheckAccessor
    {
        public IList<Book.Model.ANSIPCImpactCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Book.Model.Product product, Book.Model.Customer customer, string cusXOId, string ForANSIOrJIS)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate);
            ht.Add("EndDate", EndDate);
            StringBuilder sql = new StringBuilder();
            if (customer != null)
                sql.Append(" and InvoiceCusXOId IN (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE xocustomerId = '" + customer.CustomerId + "')");
            if (!string.IsNullOrEmpty(cusXOId))
                //sql.Append(" and InvoiceCusXOId like '%" + cusXOId + "%'");
                sql.Append(" and PronoteHeaderId in (select PronoteHeaderID from PronoteHeader where InvoiceXOId =(select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + cusXOId + "'))");
            if (product != null)
                sql.Append(" and ProductId = '" + product.ProductId + "'");
            if (!String.IsNullOrEmpty(ForANSIOrJIS))
                sql.Append("  AND ForANSIOrJIS='" + ForANSIOrJIS + "'");
            ht.Add("sql", sql.ToString());
            return sqlmapper.QueryForList<Model.ANSIPCImpactCheck>("ANSIPCImpactCheck.SelectByDateRange", ht);
        }

        public Model.ANSIPCImpactCheck GetLastByForANSIOrJIS(string ForANSIOrJIS)
        {
            return sqlmapper.QueryForObject<Model.ANSIPCImpactCheck>("ANSIPCImpactCheck.GetLastByForANSIOrJIS", ForANSIOrJIS);
        }

        public Model.ANSIPCImpactCheck GetFirstByForANSIOrJIS(string ForANSIOrJIS)
        {
            return sqlmapper.QueryForObject<Model.ANSIPCImpactCheck>("ANSIPCImpactCheck.GetFirstByForANSIOrJIS", ForANSIOrJIS);
        }

        public bool HasRowsByForANSIOrJIS(string ForANSIOrJIS)
        {
            return sqlmapper.QueryForObject<bool>("ANSIPCImpactCheck.HasRowsByForANSIOrJIS", ForANSIOrJIS);
        }

        public bool HasRowsBeforeByForANSIOrJIS(Model.ANSIPCImpactCheck e)
        {
            return sqlmapper.QueryForObject<bool>("ANSIPCImpactCheck.HasRowsBeforeByForANSIOrJIS", e);
        }

        public bool HasRowsAfterByForANSIOrJIS(Model.ANSIPCImpactCheck e)
        {
            return sqlmapper.QueryForObject<bool>("ANSIPCImpactCheck.HasRowsAfterByForANSIOrJIS", e);
        }

        public Model.ANSIPCImpactCheck GetNextByForANSIOrJIS(Model.ANSIPCImpactCheck e)
        {
            return sqlmapper.QueryForObject<Model.ANSIPCImpactCheck>("ANSIPCImpactCheck.GetNextByForANSIOrJIS", e);
        }

        public Model.ANSIPCImpactCheck GetPrevByForANSIOrJIS(Model.ANSIPCImpactCheck e)
        {
            return sqlmapper.QueryForObject<Model.ANSIPCImpactCheck>("ANSIPCImpactCheck.GetPrevByForANSIOrJIS", e);
        }

        public string SelectByInvoiceCusID(string ID)
        {
            return sqlmapper.QueryForObject<string>("ANSIPCImpactCheck.SelectByInvoiceCusID", ID);
        }

        public bool IsExistsPNTForInsert(string PNTId, string ForANSIOrJIS)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderId", PNTId);
            ht.Add("ForANSIOrJIS", ForANSIOrJIS);
            return sqlmapper.QueryForObject<bool>("ANSIPCImpactCheck.IsExistsPNTForInsert", ht);
        }

        public bool IsExistsPNTForUpdate(string PNTId, string ForANSIOrJIS, string ANSIPCImpactCheckID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderId", PNTId);
            ht.Add("ForANSIOrJIS", ForANSIOrJIS);
            ht.Add("ANSIPCImpactCheckID", ANSIPCImpactCheckID);
            return sqlmapper.QueryForObject<bool>("ANSIPCImpactCheck.IsExistsPNTForUpdate", ht);
        }
    }
}
