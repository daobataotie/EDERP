//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactDetailAccessor.cs
// author: peidun
// create date：2010-1-4 15:32:39
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
    /// Data accessor of ProduceOtherCompactDetail
    /// </summary>
    public partial class ProduceOtherCompactDetailAccessor : EntityAccessor, IProduceOtherCompactDetailAccessor
    {
        public IList<Book.Model.ProduceOtherCompactDetail> Select(Model.ProduceOtherCompact produceOtherCompact)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_byProduceOtherCompactId", produceOtherCompact.ProduceOtherCompactId);
        }

        public IList<Book.Model.ProduceOtherCompactDetail> Select(string produceOtherCompactId)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_byProduceOtherCompactId", produceOtherCompactId);
        }

        public IList<Book.Model.ProduceOtherCompactDetail> SelectCompactDetailAndFlag(Model.ProduceOtherCompact produceOtherCompact)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_byProduceOtherCompactIdAndFlag", produceOtherCompact.ProduceOtherCompactId);
        }

        public IList<Book.Model.ProduceOtherCompactDetail> SelectIsInDepot(Model.ProduceOtherCompact produceOtherCompact)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.selectbyCompactIdIsInDepot", produceOtherCompact.ProduceOtherCompactId);
        }

        public double GetByMPSdetail(string mPSDetailId)
        {
            return sqlmapper.QueryForObject<double>("ProduceOtherCompactDetail.select_byMPSdetail", mPSDetailId);
        }

        public IList<Book.Model.ProduceOtherCompactDetail> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startcustomerid", customerStart);
            ht.Add("endcustomerid", customerEnd);
            ht.Add("startdate", dateStart);
            ht.Add("enddate", dateEnd);
            return sqlmapper.QueryForList<Book.Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_byCustomerANDdate", ht);
        }

        public IList<Model.ProduceOtherCompactDetail> SelectByConditoin(string cid1, string cid2, DateTime startdate, DateTime enddate, string pid0, string pid1, string sid0, string sid1)
        {
            Hashtable ht = new Hashtable();
            ht.Add("cid1", cid1);
            ht.Add("cid2", cid2);
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            ht.Add("pid0", pid0);
            ht.Add("pid1", pid1);
            ht.Add("sid0", sid0);
            ht.Add("sid1", sid1);
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.selectBycondition", ht);
        }

        public IList<Book.Model.ProduceOtherCompactDetail> GetThreeMaths()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.Select_ThreeMaths", null);
        }

        public IList<Book.Model.ProduceOtherCompactDetail> GetDate(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();

            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_GetToDate", ht);
        }

        public IList<Model.ProduceOtherCompactDetail> Select(string CompactId, string StartpId, string EndpId)
        {
            StringBuilder sb = new StringBuilder("select *,(SELECT ProductName FROM Product WHERE Product.ProductId = ProduceOtherCompactDetail.ProductId) AS ProductName,(SELECT CustomerProductName FROM Product WHERE Product.ProductId = ProduceOtherCompactDetail.ProductId) AS CustomerProductName from ProduceOtherCompactDetail where 1 = 1 ");

            sb.Append(" AND ProduceOtherCompactId = '" + CompactId + "'");
            if (!string.IsNullOrEmpty(StartpId) && !string.IsNullOrEmpty(EndpId))
            {
                sb.Append(" AND ProductId IN (SELECT Product.ProductId FROM Product WHERE Id BETWEEN '" + StartpId + "' AND '" + EndpId + "')");
            }
            return this.DataReaderBind<Model.ProduceOtherCompactDetail>(sb.ToString(), null, CommandType.Text);

            #region 注释
            //Hashtable ht = new Hashtable();
            //ht.Add("ProduceOtherCompactDetail", CompactId);
            //ht.Add("StartpId", StartpId);
            //ht.Add("EndpId", EndpId);
            //return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.selectByHeaderIdAndPid", ht);
            #endregion
        }


        public IList<Model.ProduceOtherCompactDetail> SelectByDateRangeAndProductId(DateTime dateStart, DateTime dateEnd, string p)
        {
            Hashtable ht = new Hashtable();
            ht.Add("dateStart", dateStart);
            ht.Add("dateEnd", dateEnd);
            ht.Add("productid", p);
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.SelectByDateRangeAndProductId", ht);
        }

        public DataTable SelectDetail(string StartCompactId, string EndCompactId, DateTime Startdate, DateTime EndDate, string StartSupplierId, string EndSupplierId, string StartPid, string EndPid, string InvoiceCusId)
        {
            StringBuilder sb = new StringBuilder("select pc.ProduceOtherCompactId,Convert(varchar(50),pc.ProduceOtherCompactDate,111) as ProduceOtherCompactDate,Convert(varchar(50),pcd.JiaoQi,111) as JiaoQi,Convert(varchar(50),x.InvoiceYjrq,111) as InvoiceYjrq,p.ProductName,x.CustomerInvoiceXOId,pcd.OtherCompactCount,pcd.InDepotCount,pcd.CancelQuantity,pcd.ProductUnit,(select isnull(sum(ProduceInDepotQuantity),0) from ProduceOtherInDepotDetail where ProduceOtherCompactId=pc.ProduceOtherCompactId and ProductId=pcd.ProductId) as ProduceInDepotQuantity from ProduceOtherCompactDetail pcd left join ProduceOtherCompact pc on pcd.ProduceOtherCompactId=pc.ProduceOtherCompactId left join Product p on pcd.ProductId=p.ProductId left join InvoiceXO x on pc.InvoiceXOId=x.InvoiceId");
            sb.Append(" where pc.ProduceOtherCompactDate BETWEEN '" + Startdate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.AddDays(1).Date.ToString("yyyy-MM-dd") + "'");
            if (!string.IsNullOrEmpty(StartCompactId) && !string.IsNullOrEmpty(EndCompactId))
            {
                sb.Append(" AND pc.ProduceOtherCompactId BETWEEN '" + StartCompactId + "' AND '" + EndCompactId + "'");
            }
            if (!string.IsNullOrEmpty(StartSupplierId) && !string.IsNullOrEmpty(EndSupplierId))
            {
                sb.Append(" AND pc.SupplierId IN (SELECT Supplier.SupplierId FROM Supplier WHERE Id BETWEEN '" + StartSupplierId + "' AND '" + EndSupplierId + "')");
            }
            if (!string.IsNullOrEmpty(StartPid) && !string.IsNullOrEmpty(EndPid))
            {
                sb.Append(" AND pcd.ProductId IN (SELECT Product.ProductId FROM Product WHERE Id BETWEEN '" + StartPid + "' AND '" + EndPid + "'))");
            }
            if (!string.IsNullOrEmpty(InvoiceCusId))
                sb.Append(" AND pc.InvoiceXOId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + InvoiceCusId + "')");

            sb.Append(" AND pc.InvoiceStatus<>2 ORDER BY ProduceOtherCompactId DESC");

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sb.ToString(), sqlmapper.DataSource.ConnectionString);
            sda.Fill(dt);
            return dt;
        }
    }
}
