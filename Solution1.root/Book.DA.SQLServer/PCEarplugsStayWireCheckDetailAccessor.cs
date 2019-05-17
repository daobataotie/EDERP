//------------------------------------------------------------------------------
//
// file name：PCEarplugsStayWireCheckDetailAccessor.cs
// author: mayanjun
// create date：2019/5/14 09:57:06
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
    /// Data accessor of PCEarplugsStayWireCheckDetail
    /// </summary>
    public partial class PCEarplugsStayWireCheckDetailAccessor : EntityAccessor, IPCEarplugsStayWireCheckDetailAccessor
    {
        public IList<Model.PCEarplugsStayWireCheckDetail> SelectByHeaderId(string headerId)
        {
            return sqlmapper.QueryForList<Model.PCEarplugsStayWireCheckDetail>("PCEarplugsStayWireCheckDetail.SelectByHeaderId", headerId);
        }

        public void DeleteByHeaderId(string headerId)
        {
            sqlmapper.Delete("PCEarplugsStayWireCheckDetail.DeleteByHeaderId", headerId);
        }

        public IList<Model.PCEarplugsStayWireCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate);
            ht.Add("endDate", endDate);
            ht.Add("productId", productId);
            ht.Add("cusXOId", (string.IsNullOrEmpty(cusXOId) ? null : cusXOId));

            return sqlmapper.QueryForList<Model.PCEarplugsStayWireCheckDetail>("PCEarplugsStayWireCheckDetail.SelectByDateRage", ht);
        }
    }
}
