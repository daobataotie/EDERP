//------------------------------------------------------------------------------
//
// file name:InvoiceCTAccessor.cs
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
    /// Data accessor of InvoiceCT
    /// </summary>
    public partial class InvoiceCTAccessor : EntityAccessor, IInvoiceCTAccessor
    {
        #region IInvoiceCTAccessor 成员


        public IList<Book.Model.InvoiceCT> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceCT>("InvoiceCT.select_byTime", pars);
        }

        #endregion

        #region IInvoiceCTAccessor Members


        public void OwedIncrement(Book.Model.InvoiceCT invoice, decimal money)
        {
            this.OwedIncrement(invoice.InvoiceId, money);
        }

        public void OwedDecrement(Book.Model.InvoiceCT invoice, decimal money)
        {
            this.OwedDecrement(invoice.InvoiceId, money);
        }

        public void OwedIncrement(Book.Model.InvoiceCT invoice, decimal? money)
        {
            this.OwedDecrement(invoice.InvoiceId, money.Value);
        }

        public void OwedDecrement(Book.Model.InvoiceCT invoice, decimal? money)
        {
            this.OwedDecrement(invoice.InvoiceId, money.Value);
        }

        public void OwedIncrement(string invoiceId, decimal money)
        {
            System.Collections.Hashtable paras = new Hashtable();
            paras.Add("InvoiceOwed", money);
            paras.Add("InvoiceId", invoiceId);
            sqlmapper.Update("InvoiceCT.owedincrement", paras);
        }

        public void OwedDecrement(string invoiceId, decimal money)
        {
            System.Collections.Hashtable paras = new Hashtable();
            paras.Add("InvoiceOwed", money);
            paras.Add("InvoiceId", invoiceId);
            sqlmapper.Update("InvoiceCT.oweddecrement", paras);
        }

        public void OwedIncrement(string invoiceId, decimal? money)
        {
            this.OwedIncrement(invoiceId, money.Value);
        }

        public void OwedDecrement(string invoiceId, decimal? money)
        {
            this.OwedDecrement(invoiceId, money.Value);
        }

        #endregion

        #region IInvoiceCTAccessor 成员


        public IList<Book.Model.InvoiceCT> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceCT>("InvoiceCT.select_byStatus", (int)status);
        }

        #endregion

        public IList<Model.InvoiceCT> SelectByCondition(DateTime dateStart, DateTime dateEnd, string ctStart, string ctEnd, string coStart, string coEnd, string CusId, string supplierid)
        {
            StringBuilder sql = new StringBuilder();
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
            return sqlmapper.QueryForList<Model.InvoiceCT>("InvoiceCT.SelectByCondition", sql.ToString());
        }
    }
}
