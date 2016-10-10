//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepotDetailAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:37
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
    /// Data accessor of ProduceOtherInDepotDetail
    /// </summary>
    public partial class ProduceOtherInDepotDetailAccessor : EntityAccessor, IProduceOtherInDepotDetailAccessor
    {
        public IList<Book.Model.ProduceOtherInDepotDetail> Select(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherInDepotDetail>("ProduceOtherInDepotDetail.select_byProduceOtherInDepotId", produceOtherInDepot.ProduceOtherInDepotId);
        }
        public IList<Book.Model.ProduceOtherInDepotDetail> Select(Model.ProduceOtherCompact startPronoteHeader, Model.ProduceOtherCompact endPronoteHeader, DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startpronoteid", startPronoteHeader == null ? null : startPronoteHeader.ProduceOtherCompactId);
            ht.Add("endpronoteid", endPronoteHeader == null ? null : endPronoteHeader.ProduceOtherCompactId);
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceOtherInDepotDetail>("ProduceOtherInDepotDetail.select_byProduceInDateAndPronote", ht);
        }

        public IList<Model.ProduceOtherInDepotDetail> SelectByCondition(string indepotId, string productId1, string productId2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("indepotId", indepotId);
            ht.Add("productId1", productId1);
            ht.Add("productId2", productId2);
            return sqlmapper.QueryForList<Model.ProduceOtherInDepotDetail>("ProduceOtherInDepotDetail.selectByCondition", ht);
        }
        public void Delete(Model.ProduceOtherInDepot indepot)
        {
            sqlmapper.Delete("ProduceOtherInDepotDetail.deletebyheader", indepot.ProduceOtherInDepotId);
        }

        /// <summary>
        /// 用于委外加工入库——搜索
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="supper1"></param>
        /// <param name="supper2"></param>
        /// <param name="ProduceOtherCompactId1"></param>
        /// <param name="ProduceOtherCompactId2"></param>
        /// <param name="startPro"></param>
        /// <param name="endPro"></param>
        /// <param name="invouceCusidStart"></param>
        /// <param name="invouceCusidEnd"></param>
        /// <returns></returns>
        public IList<Model.ProduceOtherInDepotDetail> SelectByCondition(DateTime startdate, DateTime enddate, Book.Model.Supplier supper1, Book.Model.Supplier supper2, string ProduceOtherCompactId1, string ProduceOtherCompactId2, Book.Model.Product startPro, Book.Model.Product endPro, string invouceCusidStart, string invouceCusidEnd)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT po.ProduceOtherInDepotId,po.ProduceOtherInDepotDate,pc.ProduceOtherCompactId,p.ProductName,pod.ProductUnit,pod.ProduceQuantity,pod.ProduceTransferQuantity,pod.ProduceInDepotQuantity,s.SupplierShortName,pod.InvoiceCusId,pc.OtherCompactCount,pc.JiaoQi FROM ProduceOtherInDepotDetail pod LEFT JOIN ProduceOtherInDepot po ON po.ProduceOtherInDepotId = pod.ProduceOtherInDepotId LEFT JOIN product p ON p.ProductId = pod.ProductId LEFT JOIN Supplier s ON s.SupplierId = po.SupplierId LEFT JOIN ProduceOtherCompactDetail pc ON pod.ProduceOtherCompactDetailId=pc.OtherCompactDetailId WHERE po.ProduceOtherInDepotDate BETWEEN '" + startdate.ToString("yyyy-MM-dd") + "' AND '" + enddate.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (supper1 != null || supper2 != null)
            {
                if (supper1 != null && supper2 != null)
                    sql.Append(" and  s.Id between '" + supper1.Id + "' and '" + supper2.Id + "' ");
                else
                    sql.Append(" and  s.Id='" + (supper1 == null ? supper2.Id : supper1.Id) + "'");
            }
            if (!string.IsNullOrEmpty(ProduceOtherCompactId1) || !string.IsNullOrEmpty(ProduceOtherCompactId2))
            {
                if (!string.IsNullOrEmpty(ProduceOtherCompactId1) && !string.IsNullOrEmpty(ProduceOtherCompactId2))
                    sql.Append(" and  pod.ProduceOtherCompactId between '" + ProduceOtherCompactId2 + "' and '" + ProduceOtherCompactId2 + "' ");
                else
                    sql.Append(" AND pod.ProduceOtherCompactId='" + (string.IsNullOrEmpty(ProduceOtherCompactId1) ? ProduceOtherCompactId2 : ProduceOtherCompactId1) + "'");
            }
            if (startPro != null || endPro != null)
            {
                if (startPro != null && endPro != null)
                    sql.Append(" and  pod.ProductId in(select productId from product where ProductName between '" + startPro.ProductName + "' and '" + endPro.ProductName + "') ");
                else
                    sql.Append(" AND pod.ProductId IN (SELECT ProductId FROM Product where ProductName between '" + (startPro == null ? endPro.ProductName : startPro.ProductName) + "')");
            }
            if (!string.IsNullOrEmpty(invouceCusidStart) || !string.IsNullOrEmpty(invouceCusidEnd))
            {
                if (!string.IsNullOrEmpty(invouceCusidStart) && !string.IsNullOrEmpty(invouceCusidEnd))
                    sql.Append(" AND pod.InvoiceCusId BETWEEN '" + invouceCusidStart + "' and '" + invouceCusidEnd + "'");
                else
                    sql.Append(" AND pod.InvoiceCusId='" + (string.IsNullOrEmpty(invouceCusidStart) ? invouceCusidEnd : invouceCusidStart) + "'");
            }
            //SqlDataAdapter sda = new SqlDataAdapter(sql.ToString(), sqlmapper.DataSource.ConnectionString);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //return dt;

            return this.DataReaderBind<Model.ProduceOtherInDepotDetail>(sql.ToString(), null, CommandType.Text);
        }

        #region IProduceOtherInDepotDetailAccessor 成员


        public IList<Book.Model.ProduceOtherInDepotDetail> SelectByProduceotherInDepotId(string id)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherInDepotDetail>("ProduceOtherInDepotDetail.SelectByProduceotherInDepotId", id);
        }

        #endregion
    }

}
