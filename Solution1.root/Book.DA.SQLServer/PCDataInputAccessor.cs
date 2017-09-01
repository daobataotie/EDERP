//------------------------------------------------------------------------------
//
// file name：PCDataInputAccessor.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
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
    /// Data accessor of PCDataInput
    /// </summary>
    public partial class PCDataInputAccessor : EntityAccessor, IPCDataInputAccessor
    {
        public IList<Model.PCDataInput> SelectByCondition(DateTime startDate, DateTime endDate, string PNTId, string CusXOId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", startDate.Date);
            ht.Add("EndDate", endDate.Date.AddDays(1).AddSeconds(-1));
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(PNTId))
                sb.Append(" and PronoteHeaderId='" + PNTId + "'");
            if (!string.IsNullOrEmpty(CusXOId))
                sb.Append(" and InvoiceCusId='" + CusXOId + "'");
            if (!string.IsNullOrEmpty(ProductId))
                sb.Append(" and ProductId='" + ProductId + "'");
            ht.Add("SQL", sb.ToString());
            return sqlmapper.QueryForList<Model.PCDataInput>("PCDataInput.SelectByCondition", ht);
        }
    }
}
