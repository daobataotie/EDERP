//------------------------------------------------------------------------------
//
// file name：ProduceOtherMaterialDetailAccessor.cs
// author: peidun
// create date：2010-1-5 15:39:19
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
    /// Data accessor of ProduceOtherMaterialDetail
    /// </summary>
    public partial class ProduceOtherMaterialDetailAccessor : EntityAccessor, IProduceOtherMaterialDetailAccessor
    {
        public IList<Book.Model.ProduceOtherMaterialDetail> Select(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.select_byProduceOtherMaterialId", produceOtherMaterial.ProduceOtherMaterialId);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> GetOrderById(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.select_byProduceOtherMaterialIdOrderByCategoryId", produceOtherMaterial.ProduceOtherMaterialId);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> SelectByState(Model.ProduceOtherMaterial produceMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.select_byState", produceMaterial.ProduceOtherMaterialId);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> Select(string houseid, DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("houseid", houseid);
            ht.Add("startDate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.SelectByHouseDates", ht);
        }

        public IList<Model.ProduceOtherMaterialDetail> SelectByCondition(string ProduceOtherMaterialDetailId, string productId1, string productId2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProduceOtherMaterialId", ProduceOtherMaterialDetailId);
            ht.Add("productId1", productId1);
            ht.Add("productId2", productId2);
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.selectByCondition", ht);
        }

        public Model.ProduceOtherMaterialDetail SelectByPidHidPosId(string productId, string produceOtherMaterialId, string depotPositionId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("depotPositionId", depotPositionId);
            ht.Add("ProduceOtherMaterialId", produceOtherMaterialId);
            return sqlmapper.QueryForObject<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.selectByPidHidPosId", ht);
        }

        public IList<Model.ProduceOtherMaterialDetail> SelectForDistributioned(string productid, DateTime InsertTime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productid", productid);
            ht.Add("InsertTime", InsertTime);
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.SelectForDistributioned", ht);
        }

        public IList<Model.ProduceOtherMaterialDetail> SelectByConditionRange(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string cid1, string cid2, string StartpId, string EndpId, string invoiceCusID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select p.Id as pid,p.ProductName as pName,po.ProduceOtherCompactId,po.ProduceOtherMaterialId,po.ProduceOtherMaterialDate,po.ProduceOtherMaterialDesc,(select SupplierShortName from Supplier where SupplierId=po.SupplierId) as SupplierShortName,p2.ProductName as ParentProductName from ProduceOtherMaterialDetail pod left join ProduceOtherMaterial po on pod.ProduceOtherMaterialId=po.ProduceOtherMaterialId left join Product p on pod.ProductId=p.ProductId left join Product p2 on p2.ProductId=pod.ParentProductId where po.ProduceOtherMaterialDate between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(supperId1) || !string.IsNullOrEmpty(supperId2))
            {
                if (!string.IsNullOrEmpty(supperId1) && !string.IsNullOrEmpty(supperId2))
                    sql.Append(" AND po.SupplierId between '" + supperId1 + "' and '" + supperId2 + "'");
                else
                    sql.Append(" AND po.SupplierId='" + (string.IsNullOrEmpty(supperId1) ? supperId2 : supperId1) + "'");
            }
            if (!string.IsNullOrEmpty(cid1) || !string.IsNullOrEmpty(cid2))
            {
                if (!string.IsNullOrEmpty(cid1) && !string.IsNullOrEmpty(cid2))
                    sql.Append(" and po.ProduceOtherCompactId between '" + cid1 + "' and '" + cid2 + "'");
                else
                    sql.Append(" and po.ProduceOtherCompactId='" + (string.IsNullOrEmpty(cid1) ? cid2 : cid1) + "'");
            }
            if (!string.IsNullOrEmpty(StartpId) || !string.IsNullOrEmpty(EndpId))
            {
                if (!string.IsNullOrEmpty(StartpId) && !string.IsNullOrEmpty(EndpId))
                    sql.Append(" and p.Id between '" + StartpId + "' and '" + EndpId + "'");
                else
                    sql.Append(" and p.Id='" + (string.IsNullOrEmpty(StartpId) ? EndpId : StartpId) + "'");
            }
            if (!string.IsNullOrEmpty(invoiceCusID))
            {
                sql.Append(" and po.ProduceOtherCompactId in (select ProduceOtherCompactId from ProduceOtherCompact where InvoiceXOId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + invoiceCusID + "'))");
            }
            sql.Append(" order by po.ProduceOtherMaterialId");
            return this.DataReaderBind<Model.ProduceOtherMaterialDetail>(sql.ToString(), null, CommandType.Text);
        }

        public IList<Model.ProduceOtherMaterialDetail> SelectDetailByCondition(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string cid1, string cid2, string StartpId, string EndpId, string invoiceCusID)
        {
            StringBuilder sb = new StringBuilder("select s.SupplierShortName,pm.ProduceOtherCompactId,pcd.JiaoQi,xo.InvoiceYjrq,pm.ProduceOtherMaterialDate,xo.CustomerInvoiceXOId,p.ProductName,sum(isnull(pmd.InvoiceUseQuantity,0)) as InvoiceUseQuantity,pmd.ProductUnit from ProduceOtherMaterialDetail pmd left join ProduceOtherMaterial pm on pmd.ProduceOtherMaterialId=pm.ProduceOtherMaterialId left join ProduceOtherCompact poc on poc.ProduceOtherCompactId=pm.ProduceOtherCompactId left join InvoiceXO xo on xo.InvoiceId=poc.InvoiceXOId left join ProduceOtherCompactDetail pcd on pmd.ParentProductId=pcd.ProductId and poc.ProduceOtherCompactId=pcd.ProduceOtherCompactId left join Product p on p.ProductId=pmd.ProductId left join Supplier s on s.SupplierId=pm.SupplierId where pm.ProduceOtherMaterialDate  between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(supperId1) || !string.IsNullOrEmpty(supperId2))
            {
                if (!string.IsNullOrEmpty(supperId1) && !string.IsNullOrEmpty(supperId2))
                    sb.Append(" and s.Id between '" + supperId1 + "' and '" + supperId1 + "'");
                else
                    sb.Append(" and s.Id=" + (string.IsNullOrEmpty(supperId1) ? supperId2 : supperId1));
            }
            if (!string.IsNullOrEmpty(cid1) || !string.IsNullOrEmpty(cid2))
            {
                if (!string.IsNullOrEmpty(cid1) && !string.IsNullOrEmpty(cid2))
                    sb.Append(" and pm.ProduceOtherCompactId between '" + cid1 + "' and '" + cid2 + "'");
                else
                    sb.Append(" and pm.ProduceOtherCompactId=" + (string.IsNullOrEmpty(cid1) ? cid2 : cid1));
            }
            if (!string.IsNullOrEmpty(StartpId) || !string.IsNullOrEmpty(EndpId))
            {
                if (!string.IsNullOrEmpty(StartpId) && !string.IsNullOrEmpty(EndpId))
                    sb.Append(" and p.Id between '" + StartpId + "' and '" + EndpId + "'");
                else
                    sb.Append(" and p.Id=" + (string.IsNullOrEmpty(StartpId) ? EndpId : StartpId));
            }
            if (!string.IsNullOrEmpty(invoiceCusID))
            {
                sb.Append(" and xo.CustomerInvoiceXOId='" + invoiceCusID + "'");
            }
            sb.Append(" group by s.SupplierShortName,pm.ProduceOtherCompactId,pcd.JiaoQi,xo.InvoiceYjrq,pm.ProduceOtherMaterialDate,xo.CustomerInvoiceXOId,p.ProductName,pmd.ProductUnit order by pm.ProduceOtherCompactId ");

            return DataReaderBind<Model.ProduceOtherMaterialDetail>(sb.ToString(), null, CommandType.Text);
        }
    }
}
