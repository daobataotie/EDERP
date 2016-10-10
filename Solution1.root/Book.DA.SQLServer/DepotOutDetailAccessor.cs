//------------------------------------------------------------------------------
//
// file name：DepotOutDetailAccessor.cs
// author: mayanjun
// create date：2010-10-15 15:41:09
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
    /// Data accessor of DepotOutDetail
    /// </summary>
    public partial class DepotOutDetailAccessor : EntityAccessor, IDepotOutDetailAccessor
    {
        public IList<Model.DepotOutDetail> GetDepotOutDetailByDepotOutId(string depotOutId)
        {
            return sqlmapper.QueryForList<Model.DepotOutDetail>("DepotOutDetail.GetDepotOutDetailByDepotOutId", depotOutId);
        }
        public void Delete(Model.DepotOut depotOut)
        {
            sqlmapper.Delete("DepotOutDetail.deleteByHeader", depotOut.DepotOutId);
        }

        public IList<Model.DepotOutDetail> SelectByCondition(DateTime startDate, DateTime endDate, string DepotOutIdStart, string DepotOutIdEnd, string depotStart, string depotEnd, string ProductId, string invoiceCusId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate);
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(DepotOutIdStart) || !string.IsNullOrEmpty(DepotOutIdEnd))
            {
                if (!string.IsNullOrEmpty(DepotOutIdStart) && !string.IsNullOrEmpty(DepotOutIdEnd))
                    sql.Append("AND DepotOut.DepotOutId BETWEEN '" + DepotOutIdStart + "' AND '" + DepotOutIdEnd + "'");
                else
                    sql.Append("AND DepotOut.DepotOutId = '" + (string.IsNullOrEmpty(DepotOutIdStart) ? DepotOutIdEnd : DepotOutIdStart) + "'");
            }
            if (depotStart != null || depotEnd != null)
            {
                if (depotStart != null && depotEnd != null)
                    sql.Append("AND DepotOut.DepotId BETWEEN '" + depotStart + "' AND '" + depotEnd + "'");
                else
                    sql.Append("AND DepotOut.DepotId='" + (depotStart == null ? depotEnd : depotStart) + "'");
            }
            if (!string.IsNullOrEmpty(ProductId))
            {
                sql.Append(" AND DepotOut.DepotOutId in (select DepotOutId from DepotOutDetail where ProductId='" + ProductId + "')");
            }
            if (!string.IsNullOrEmpty(invoiceCusId))
                sql.Append(" And  (DepotOut.InvioiceId in (select ProduceMaterialID from ProduceMaterial where InvoiceXOId=(select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + invoiceCusId + "'))) or (DepotOut.InvioiceId in (select ProduceOtherMaterialId from ProduceOtherMaterial where ProduceOtherCompactId in (select ProduceOtherCompactId from ProduceOtherCompact where InvoiceXOId=(select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + invoiceCusId + "'))))");
            ht.Add("sql", sql);
            //if (!string.IsNullOrEmpty(InvoiceXOIdStart) || !string.IsNullOrEmpty(InvoiceXOIdEnd))
            //{
            //    if (!string.IsNullOrEmpty(InvoiceXOIdStart) && !string.IsNullOrEmpty(InvoiceXOIdEnd))
            //        sql.Append("");
            //    //else
            //        //sql.Append();
            //}
            return sqlmapper.QueryForList<Model.DepotOutDetail>("DepotOutDetail.SelectByCondition", ht);
        }

        public IList<Model.DepotOutDetail> SelectByDateRange(DateTime startDate, DateTime endDate, string productid, string invoiceCusId)
        {
            StringBuilder sql = new StringBuilder("select d.DepotOutId,d.DepotOutDate,d.SourceType,d.InvioiceId,dd.DepotOutDetailQuantity,e.EmployeeName,p.ProductName,dp.Id,dd.Pihao,(isnull(xo.CustomerInvoiceXOId,'')+isnull(xo2.CustomerInvoiceXOId,'')) as invoiceCusId from DepotOutDetail dd left join DepotOut d on dd.DepotOutId=d.DepotOutId left join Employee e on e.EmployeeId=d.EmployeeId left join DepotPosition dp on dp.DepotPositionId=dd.DepotPositionId left join Product p on p.ProductId=dd.ProductId left join ProduceMaterial pm on d.InvioiceId=pm.ProduceMaterialID left join InvoiceXO xo on pm.InvoiceXOId=xo.InvoiceId Left join ProduceOtherMaterial pom on d.InvioiceId=pom.ProduceOtherMaterialId left join ProduceOtherCompact poc on pom.ProduceOtherCompactId=poc.ProduceOtherCompactId left join InvoiceXO xo2 on poc.InvoiceXOId=xo2.InvoiceId where 1=1");
            sql.Append(" And d.DepotOutDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(productid))
                sql.Append(" And dd.ProductId='" + productid + "'");
            if (!string.IsNullOrEmpty(invoiceCusId))
                sql.Append(" And (isnull(xo.CustomerInvoiceXOId,'')+isnull(xo2.CustomerInvoiceXOId,'')) ='" + invoiceCusId + "'");
            sql.Append(" order by DepotOutId desc");
            return DataReaderBind<Model.DepotOutDetail>(sql.ToString(), null, CommandType.Text);
        }
    }
}
