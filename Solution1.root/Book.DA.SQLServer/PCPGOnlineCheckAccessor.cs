//------------------------------------------------------------------------------
//
// file name：PCPGOnlineCheckAccessor.cs
// author: mayanjun
// create date：2011-12-6 14:19:08
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
    /// Data accessor of PCPGOnlineCheck
    /// </summary>
    public partial class PCPGOnlineCheckAccessor : EntityAccessor, IPCPGOnlineCheckAccessor
    {
        public DataTable SelectDetailByDateRage(DateTime StartDate, DateTime EndDate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId, string StartPronoteHeader, string EndPronoteHeader)
        {
            //SqlParameter[] parames = { 
            //    new SqlParameter("@StartDate", SqlDbType.DateTime), 
            //    new SqlParameter("@EndDate", SqlDbType.DateTime), 
            //    new SqlParameter("@CustomerId", SqlDbType.VarChar), 
            //    new SqlParameter("@InvoiceCusXOId", SqlDbType.VarChar), 
            //    new SqlParameter("@ProductId", SqlDbType.VarChar), 
            //    new SqlParameter("@StartPronoteHeader", SqlDbType.VarChar), 
            //    new SqlParameter("@EndPronoteHeader", SqlDbType.VarChar)
            //};

            //parames[0].Value = StartDate.ToString("yyyy-MM-dd");
            //parames[1].Value = EndDate.ToString("yyyy-MM-dd");
            //if (customer == null)
            //    parames[2].Value = DBNull.Value;
            //else
            //    parames[2].Value = customer.CustomerId;
            //if (string.IsNullOrEmpty(CusXOId))
            //    parames[3].Value = DBNull.Value;
            //else
            //    parames[3].Value = "%" + CusXOId + "%";
            //if (product == null)
            //    parames[4].Value = DBNull.Value;
            //else
            //    parames[4].Value = product.ProductId;
            //if (string.IsNullOrEmpty(StartPronoteHeader))
            //    parames[5].Value = DBNull.Value;
            //else
            //    parames[5].Value = StartPronoteHeader;
            //if (string.IsNullOrEmpty(EndPronoteHeader))
            //    parames[6].Value = DBNull.Value;
            //else
            //    parames[6].Value = EndPronoteHeader;

            //StringBuilder sql = new StringBuilder("SELECT PCPGOnlineCheckId,PCPGOnlineCheckDate,InvoiceCusXOId,(SELECT EmployeeName FROM Employee WHERE Employee.EmployeeId = PCPGOnlineCheck.EmployeeId) AS EmployeeName,(SELECT ProductName FROM Product WHERE Product.ProductId = PCPGOnlineCheck.ProductId) AS ProductName,(SELECT Customer.CustomerShortName FROM Customer WHERE Customer.CustomerId = PCPGOnlineCheck.CustomerId) AS CustomerShortName,(SELECT Convert(varchar(30),PCPGOnlineCheckDetailDate,120)+'.' FROM PCPGOnlineCheckDetail WHERE PCPGOnlineCheckDetail.PCPGOnlineCheckId=PCPGOnlineCheck.PCPGOnlineCheckId FOR xml path('')) AS DescTime FROM PCPGOnlineCheck WHERE 1 = 1 ");
            //sql.Append(" AND PCPGOnlineCheckDate BETWEEN @StartDate AND @EndDate");
            //if (customer != null)
            //    sql.Append(" AND CustomerId = @CustomerId");
            //if (!string.IsNullOrEmpty(CusXOId))
            //    sql.Append(" AND InvoiceCusXOId like @InvoiceCusXOId");
            //if (product != null)
            //    sql.Append(" AND PCPGOnlineCheck.ProductId=@ProductId");
            //if (!string.IsNullOrEmpty(StartPronoteHeader) && !string.IsNullOrEmpty(EndPronoteHeader))
            //    sql.Append(" AND FromPCId BETWEEN @StartPronoteHeader AND @EndPronoteHeader");
            StringBuilder sb = new StringBuilder("select * from (select pd.PCPGOnlineCheckId,p.PCPGOnlineCheckDate,(isnull(xo1.CustomerInvoiceXOId,'')+isnull(xo2.CustomerInvoiceXOId,'')+isnull(xo3.CustomerInvoiceXOId,'')) as InvoiceCusXOId2,pro.ProductName,e.EmployeeName,pd.PCPGOnlineCheckDetailDate as DescTime,pd.FromInvoiceId as FromId,b.BusinessHoursName from PCPGOnlineCheckDetail pd left join PCPGOnlineCheck p on pd.PCPGOnlineCheckId=p.PCPGOnlineCheckId left join Employee e on e.EmployeeId=p.EmployeeId left join product pro on pd.productid=pro.ProductId left join PronoteHeader ph on pd.FromInvoiceId=ph.PronoteHeaderID  left join ProduceOtherCompact pc on pd.FromInvoiceId=pc.ProduceOtherCompactId left join InvoiceCO co on pd.FromInvoiceId=co.InvoiceId  left join InvoiceXO xo1 on ph.InvoiceXOId=xo1.InvoiceId left join InvoiceXO xo2 on pc.InvoiceXOId=xo2.InvoiceId left join InvoiceXO xo3 on co.InvoiceXOId=xo3.InvoiceId left join BusinessHours b on p.BusinessHoursId=b.BusinessHoursId where p.PCPGOnlineCheckDate  BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (customer != null)
                sb.Append(" AND p.CustomerId = '" + customer.CustomerId + "'");
            if (product != null)
                sb.Append(" AND pd.ProductId='" + product.ProductId + "'");
            if (!string.IsNullOrEmpty(StartPronoteHeader) && !string.IsNullOrEmpty(EndPronoteHeader))
                sb.Append(" AND pd.FromInvoiceId BETWEEN '" + StartPronoteHeader + "' AND '" + EndPronoteHeader + "'");
            sb.Append(" ) a");
            if (!string.IsNullOrEmpty(CusXOId))
                sb.Append(" where a.InvoiceCusXOId2 = '" + CusXOId + "'");
            //if (OnlySelfMade)       //JIS出货报告中查询时只查自制部分
            //    sb.Append(" And a.FromId like '%pnt%'");
            sb.Append(" order by PCPGOnlineCheckId desc");

            //return this.DataReaderBind<Model.PCPGOnlineCheck>(sb.ToString(), parames, CommandType.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sb.ToString(), sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.SelectCommand.CommandTimeout = 300;
            sda.Fill(dt);
            return dt;
        }

        public IList<Model.PCPGOnlineCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId, string StartPronoteHeader, string EndPronoteHeader)
        {
            StringBuilder sb = new StringBuilder(" AND p.PCPGOnlineCheckDate  BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (customer != null)
                sb.Append(" AND p.CustomerId = '" + customer.CustomerId + "'");
            if (product != null)
                sb.Append(" AND pd.ProductId='" + product.ProductId + "'");
            if (!string.IsNullOrEmpty(StartPronoteHeader) && !string.IsNullOrEmpty(EndPronoteHeader))
                sb.Append(" AND pd.FromInvoiceId BETWEEN '" + StartPronoteHeader + "' AND '" + EndPronoteHeader + "'");
            if (!string.IsNullOrEmpty(CusXOId))
                sb.Append(" AND (isnull(ph.InvoiceCusId,'')+isnull(pc.CustomerInvoiceXOId,'')) like '" + "%" + CusXOId + "%" + "'");
            sb.Append(" )");
            sb.Append(" order by PCPGOnlineCheckId desc");

            return sqlmapper.QueryForList<Model.PCPGOnlineCheck>("PCPGOnlineCheck.SelectByDateRange", sb.ToString());
        }
    }
}
