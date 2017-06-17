//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactAccessor.cs
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
    /// Data accessor of ProduceOtherCompact
    /// </summary>
    public partial class ProduceOtherCompactAccessor : EntityAccessor, IProduceOtherCompactAccessor
    {

        public IList<Model.ProduceOtherCompact> SelectIsInDepot()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.selectIsInDepot", null);
        }

        public IList<Model.ProduceOtherCompact> SelectIsInDepotMaterial()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.selectIsInDepotMaterial", null);
        }

        public IList<Model.ProduceOtherCompact> SelectByMRSHeaderId(string MrsHeaderId)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.selectByMRSHeaderId", MrsHeaderId);
        }

        public IList<Book.Model.ProduceOtherCompact> SelectThreeMonth()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.Select_ThreeMaths", null);
        }

        public IList<Model.ProduceOtherCompact> Select(string StartCompactId, string EndCompactId, DateTime Startdate, DateTime EndDate, string StartSupplierId, string EndSupplierId, string StartPid, string EndPid, string InvoiceCusId)
        {
            StringBuilder sb = new StringBuilder("SELECT *,(SELECT Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId = ProduceOtherCompact.NextWorkHouseId) AS NextWorkHouseName,(SELECT EmployeeName FROM Employee WHERE Employee.EmployeeId = ProduceOtherCompact.Employee0Id) AS EmployeeName0,(SELECT SupplierFullName FROM Supplier WHERE Supplier.SupplierId = ProduceOtherCompact.SupplierId) AS SupplierName,(SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE InvoiceId = (SELECT MPSheader.InvoiceXOId FROM MPSheader WHERE MPSheaderId = (SELECT MRSHeader.MPSheaderId FROM MRSHeader WHERE MRSHeader.MRSHeaderId = ProduceOtherCompact.MRSHeaderId))) AS RPCustomerInvoiceXOId FROM ProduceOtherCompact WHERE 1 = 1");
            if (!string.IsNullOrEmpty(StartCompactId) && !string.IsNullOrEmpty(EndCompactId))
            {
                sb.Append(" AND ProduceOtherCompactId BETWEEN '" + StartCompactId + "' AND '" + EndCompactId + "'");
            }
            sb.Append(" AND ProduceOtherCompactDate BETWEEN '" + Startdate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.AddDays(1).Date.ToString("yyyy-MM-dd") + "'");
            if (!string.IsNullOrEmpty(StartSupplierId) && !string.IsNullOrEmpty(EndSupplierId))
            {
                sb.Append(" AND SupplierId IN (SELECT Supplier.SupplierId FROM Supplier WHERE Id BETWEEN '" + StartSupplierId + "' AND '" + EndSupplierId + "')");
            }
            if (!string.IsNullOrEmpty(StartPid) && !string.IsNullOrEmpty(EndPid))
            {
                sb.Append(" AND ProduceOtherCompactId IN (SELECT ProduceOtherCompactDetail.ProduceOtherCompactId FROM ProduceOtherCompactDetail WHERE ProductId IN (SELECT Product.ProductId FROM Product WHERE Id BETWEEN '" + StartPid + "' AND '" + EndPid + "'))");
            }
            if (!string.IsNullOrEmpty(InvoiceCusId))
                sb.Append(" AND InvoiceXOId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + InvoiceCusId + "')");

            sb.Append(" AND InvoiceStatus<>2 ORDER BY ProduceOtherCompactId DESC");

            return this.DataReaderBind<Model.ProduceOtherCompact>(sb.ToString(), null, CommandType.Text);

            #region 注释
            //Hashtable ht = new Hashtable();
            //ht.Add("StartCompactId", StartCompactId);
            //ht.Add("EndCompactId", EndCompactId);
            //ht.Add("StartDate", Startdate);
            //ht.Add("EndDate", EndDate);
            //ht.Add("StartsId", StartSupplierId);
            //ht.Add("EndsId", EndSupplierId);
            //ht.Add("StartpId", StartPid);
            //ht.Add("EndpId", EndPid);
            //return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.selectByCondition", ht);
            #endregion
        }

        public IList<Book.Model.ProduceOtherCompact> GetByDate(DateTime startDate, DateTime endDate, Book.Model.Product sendProduct, string CustomerInvoiceXOId, string customerid, string supplierid, string ProduceOtherCompactId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("sendProductId", sendProduct == null ? null : sendProduct.ProductId);
            ht.Add("CustomerInvoiceXOId", string.IsNullOrEmpty(CustomerInvoiceXOId) ? null : CustomerInvoiceXOId);
            ht.Add("customerid", string.IsNullOrEmpty(customerid) ? null : customerid);
            ht.Add("supplierid", string.IsNullOrEmpty(supplierid) ? null : supplierid);
            ht.Add("ProduceOtherCompactId", string.IsNullOrEmpty(ProduceOtherCompactId) ? null : ProduceOtherCompactId);
            //if (!string.IsNullOrEmpty(InvoiceCusId))
            //    ht.Add("CustomerInvoiceXOId", InvoiceCusId);
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.select_GetToDate", ht);
        }

        public string SelectByInvoiceCusID(string Id)
        {
            return sqlmapper.QueryForObject<string>("ProduceOtherCompact.SelectByInvoiceCusID", Id);
        }

        public DataTable GetExcelData(DateTime startDate, DateTime endDate)
        {
            //string sql = "select po.ProduceOtherCompactId,Convert(varchar(50),po.ProduceOtherCompactDate,23) as LastDate,xo.CustomerInvoiceXOId,p.ProductName,pod.OtherCompactCount,pod.ArrivalInQuantity,pod.ProductUnit from ProduceOtherCompactDetail pod left join ProduceOtherCompact po on pod.ProduceOtherCompactId =po.ProduceOtherCompactId left join InvoiceXO xo on xo.InvoiceId=po.InvoiceXOId left join Product p on pod.ProductId=p.ProductId where po.ProduceOtherCompactDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and (pod.ArrivalInQuantity<pod.OtherCompactCount or pod.ArrivalInQuantity is null)";
            string sql = "select po.ProduceOtherCompactId,Convert(varchar(50),po.ProduceOtherCompactDate,23) as LastDate,Convert(varchar(50),xo.InvoiceYjrq,23) as InvoiceYjrq,xo.CustomerInvoiceXOId,s.SupplierFullName,p.ProductName,pod.OtherCompactCount,pod.ArrivalInQuantity,pod.ProductUnit from ProduceOtherCompactDetail pod left join ProduceOtherCompact po on pod.ProduceOtherCompactId =po.ProduceOtherCompactId left join InvoiceXO xo on xo.InvoiceId=po.InvoiceXOId left join Product p on pod.ProductId=p.ProductId left join Supplier s on po.SupplierId=s.SupplierId where po.ProduceOtherCompactDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and (pod.ArrivalInQuantity<pod.OtherCompactCount or pod.ArrivalInQuantity is null)";

            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            return dt;
        }
    }
}
