//------------------------------------------------------------------------------
//
// file name：AtSummonAccessor.cs
// author: mayanjun
// create date：2010-11-24 09:40:43
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
    /// Data accessor of AtSummon
    /// </summary>
    public partial class AtSummonAccessor : EntityAccessor, IAtSummonAccessor
    {
        public IList<Book.Model.AtSummon> SelectByDateRage(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.AtSummon>("AtSummon.SelectByDateRage", ht);
        }

        public bool IsExistsId(string Id)
        {
            return sqlmapper.QueryForObject<bool>("AtSummon.IsExistsId", Id);
        }

        public bool IsExistsIdUpdate(Model.AtSummon model)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Id", model.Id);
            ht.Add("SummonId", model.SummonId);
            return sqlmapper.QueryForObject<bool>("AtSummon.IsExistsIdUpdate", ht);
        }

        public IList<Model.AtSummon> SelectByCondition(DateTime startDate, DateTime endDate, string startId, string endId, string StartSubjectId, string EndSubjectId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" and SummonDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(startId) || !string.IsNullOrEmpty(endId))
            {
                if (!string.IsNullOrEmpty(startId) && !string.IsNullOrEmpty(endId))
                    sql.Append(" and Id between '" + startId + "' and '" + endId + "'");
                else
                    sql.Append(" and Id='" + (string.IsNullOrEmpty(startId) ? endId : startId) + "'");
            }
            if (!string.IsNullOrEmpty(StartSubjectId) || !string.IsNullOrEmpty(EndSubjectId))
            {
                if (!string.IsNullOrEmpty(StartSubjectId) && !string.IsNullOrEmpty(EndSubjectId))
                    sql.Append(" and SummonId in (select SummonId from AtSummonDetail where SubjectId in (select SubjectId from AtAccountSubject where Id between '" + StartSubjectId + "' and '" + EndSubjectId + "'))");
                else
                    sql.Append(" and SummonId in (select SummonId from AtSummonDetail where SubjectId in (select SubjectId from AtAccountSubject where Id='" + (string.IsNullOrEmpty(StartSubjectId) ? EndSubjectId : StartSubjectId) + "'))");
            }
            sql.Append(" order by Id");

            return sqlmapper.QueryForList<Model.AtSummon>("AtSummon.SelectByCondition", sql.ToString());
        }

        public Model.AtSummon GetByInvoiceCGId(string invoiceCGId)
        {
            return sqlmapper.QueryForObject<Model.AtSummon>("AtSummon.GetByInvoiceCGId", invoiceCGId);
        }

        public Model.AtSummon GetByInvoiceXSId(string invoiceXSId)
        {
            return sqlmapper.QueryForObject<Model.AtSummon>("AtSummon.GetByInvoiceXSId", invoiceXSId);
        }
    }
}
