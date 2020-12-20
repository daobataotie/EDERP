﻿//------------------------------------------------------------------------------
//
// file name：ThicknessTestAccessor.cs
// author: mayanjun
// create date：2012-4-24 10:33:14
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
    /// Data accessor of ThicknessTest
    /// </summary>
    public partial class ThicknessTestAccessor : EntityAccessor, IThicknessTestAccessor
    {
        public DataTable SelectByPronoteHeaderId(string pronoteHeaderId)
        {
            string sql = "select * from (select '0' as InvoiceType,pcd.FromInvoiceId as PronoteHeaderId,pcd.PCPGOnlineCheckId  as HeaderId,pcd.PCPGOnlineCheckDetailId  as DetailId,PCPGOnlineCheckDetailDate  as CheckDate,b.BusinessHoursName,(select Count(*) from ThicknessTest where PCPGOnlineCheckDetailId=pcd.PCPGOnlineCheckDetailId) as TTCount from PCPGOnlineCheckDetail pcd left join PCPGOnlineCheck pc on pcd.PCPGOnlineCheckId=pc.PCPGOnlineCheckId left join BusinessHours b on b.BusinessHoursId=pc.BusinessHoursId where pcd.FromInvoiceId='" + pronoteHeaderId + "' union select '1' as InvoiceType,pf.PronoteHeaderId,pf.PCFirstOnlineCheckId as HeaderId,pfd.PCFirstOnlineCheckDetailId as DetailId,pfd.CheckDate,b.BusinessHoursName,(select COUNT(*) from ThicknessTest where PCFirstOnlineCheckDetailId=pfd.PCFirstOnlineCheckDetailId) as TTCount from PCFirstOnlineCheckDetail pfd left join PCFirstOnlineCheck pf on pfd.PCFirstOnlineCheckId=pf.PCFirstOnlineCheckId left join BusinessHours b on b.BusinessHoursId=pfd.BusinessHoursId where pf.PronoteHeaderId='" + pronoteHeaderId + "') a where a.TTCount>0";

            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            return dt;
        }

        public Book.Model.ThicknessTest mGetFirst(string PCPGOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.ThicknessTest>("ThicknessTest.mGetFirst", PCPGOnlineCheckDetailId);
        }

        public Book.Model.ThicknessTest mGetLast(string PCPGOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.ThicknessTest>("ThicknessTest.mGetLast", PCPGOnlineCheckDetailId);
        }

        public Book.Model.ThicknessTest mGetPrev(DateTime InsertDate, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.ThicknessTest>("ThicknessTest.mGetPrev", ht);
        }

        public Book.Model.ThicknessTest mGetNext(DateTime InsertDate, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.ThicknessTest>("ThicknessTest.mGetNext", ht);
        }

        public bool mHasRows(string PCPGOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<bool>("ThicknessTest.mHasRows", PCPGOnlineCheckDetailId);
        }

        public bool mHasRowsBefore(Book.Model.ThicknessTest e, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("ThicknessTest.mHasRowsBefore", ht);
        }

        public bool mHasRowsAfter(Book.Model.ThicknessTest e, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("ThicknessTest.mHasRowsAfter", ht);
        }

        public IList<Book.Model.ThicknessTest> mSelect(string PCPGOnlineCheckDetailId)
        {
            return sqlmapper.QueryForList<Model.ThicknessTest>("ThicknessTest.mSelect", PCPGOnlineCheckDetailId);
        }

        public IList<Book.Model.ThicknessTest> SelectByDateRage(DateTime startdate, DateTime enddate, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);

            return sqlmapper.QueryForList<Model.ThicknessTest>("ThicknessTest.SelectByDateRage", ht);
        }

        public bool ExistsManualId(string ThicknessTestId, string ManualId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ThicknessTestId", ThicknessTestId);
            ht.Add("ManualId", ManualId);
            return sqlmapper.QueryForObject<bool>("ThicknessTest.ExistsManualId", ht);
        }


        #region 适用于首件上线检查表

        public Book.Model.ThicknessTest PFCGetFirst(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.ThicknessTest>("ThicknessTest.PFCGetFirst", PCFirstOnlineCheckDetailId);
        }

        public Book.Model.ThicknessTest PFCGetLast(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.ThicknessTest>("ThicknessTest.PFCGetLast", PCFirstOnlineCheckDetailId);
        }

        public Book.Model.ThicknessTest PFCGetPrev(DateTime InsertDate, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.ThicknessTest>("ThicknessTest.PFCGetPrev", ht);
        }

        public Book.Model.ThicknessTest PFCGetNext(DateTime InsertDate, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.ThicknessTest>("ThicknessTest.PFCGetNext", ht);
        }

        public bool PFCHasRows(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<bool>("ThicknessTest.PFCHasRows", PCFirstOnlineCheckDetailId);
        }

        public bool PFCHasRowsBefore(Book.Model.ThicknessTest e, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("ThicknessTest.PFCHasRowsBefore", ht);
        }

        public bool PFCHasRowsAfter(Book.Model.ThicknessTest e, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("ThicknessTest.PFCHasRowsAfter", ht);
        }

        public IList<Book.Model.ThicknessTest> PFCSelect(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForList<Model.ThicknessTest>("ThicknessTest.PFCSelect", PCFirstOnlineCheckDetailId);
        }

        public IList<Book.Model.ThicknessTest> PFCSelectByDateRage(DateTime startdate, DateTime enddate, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);

            return sqlmapper.QueryForList<Model.ThicknessTest>("ThicknessTest.PFCSelectByDateRage", ht);
        }

        #endregion
    }
}
