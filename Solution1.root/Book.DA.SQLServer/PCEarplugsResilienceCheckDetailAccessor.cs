//------------------------------------------------------------------------------
//
// file name：PCEarplugsResilienceCheckDetailAccessor.cs
// author: mayanjun
// create date：2019/5/10 10:49:43
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
    /// Data accessor of PCEarplugsResilienceCheckDetail
    /// </summary>
    public partial class PCEarplugsResilienceCheckDetailAccessor : EntityAccessor, IPCEarplugsResilienceCheckDetailAccessor
    {
        public IList<Model.PCEarplugsResilienceCheckDetail> SelectByHeaderId(string headerId)
        {
            return sqlmapper.QueryForList<Model.PCEarplugsResilienceCheckDetail>("PCEarplugsResilienceCheckDetail.SelectByHeaderId", headerId);
        }

        public void DeleteByHeaderId(string headerId)
        {
            sqlmapper.Delete("PCEarplugsResilienceCheckDetail.DeleteByHeaderId", headerId);
        }

        public IList<Model.PCEarplugsResilienceCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate);
            ht.Add("endDate", endDate);
            ht.Add("productId", productId);
            ht.Add("cusXOId", cusXOId);

            return sqlmapper.QueryForList<Model.PCEarplugsResilienceCheckDetail>("PCEarplugsResilienceCheckDetail.SelectByDateRage", ht);
        }
    }
}
