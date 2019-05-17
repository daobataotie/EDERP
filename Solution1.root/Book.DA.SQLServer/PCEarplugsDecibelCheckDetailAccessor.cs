//------------------------------------------------------------------------------
//
// file name：PCEarplugsDecibelCheckDetailAccessor.cs
// author: mayanjun
// create date：2019/5/12 14:43:54
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
    /// Data accessor of PCEarplugsDecibelCheckDetail
    /// </summary>
    public partial class PCEarplugsDecibelCheckDetailAccessor : EntityAccessor, IPCEarplugsDecibelCheckDetailAccessor
    {
        public IList<Model.PCEarplugsDecibelCheckDetail> SelectByHeaderId(string headerId)
        {
            return sqlmapper.QueryForList<Model.PCEarplugsDecibelCheckDetail>("PCEarplugsDecibelCheckDetail.SelectByHeaderId", headerId);
        }

        public void DeleteByHeaderId(string headerId)
        {
            sqlmapper.Delete("PCEarplugsDecibelCheckDetail.DeleteByHeaderId", headerId);
        }

        public IList<Model.PCEarplugsDecibelCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate);
            ht.Add("endDate", endDate);
            ht.Add("productId", productId);
            ht.Add("cusXOId", (string.IsNullOrEmpty(cusXOId) ? null : cusXOId));

            return sqlmapper.QueryForList<Model.PCEarplugsDecibelCheckDetail>("PCEarplugsDecibelCheckDetail.SelectByDateRage", ht);
        }
    }
}
