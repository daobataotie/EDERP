//------------------------------------------------------------------------------
//
// file name：PCFinishCheckAccessor.cs
// author: mayanjun
// create date：2011-11-12 15:10:07
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
    /// Data accessor of PCFinishCheck
    /// </summary>
    public partial class PCFinishCheckAccessor : EntityAccessor, IPCFinishCheckAccessor
    {
        public IList<Book.Model.PCFinishCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Book.Model.Product product, string customerProductName, string CusXOId)
        {
            //Hashtable ht = new Hashtable();
            //ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            //ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            //StringBuilder sql = new StringBuilder();
            //if (!string.IsNullOrEmpty(customerProductName))
            //    sql.Append(" and customerProductName='" + customerProductName + "' ");
            //if (!string.IsNullOrEmpty(CusXOId))
            //    sql.Append(" and InvoiceCusXOId like '%" + CusXOId + "%'");
            //if (product != null)
            //    sql.Append(" and ProductId = '" + product.ProductId + "'");
            //ht.Add("sql", sql.ToString());
            //return sqlmapper.QueryForList<Model.PCFinishCheck>("PCFinishCheck.SelectByDateRange", ht);
            StringBuilder sql = new StringBuilder(" select pcf.PCFinishCheckID,pcf.PCFinishCheckDate,wh.Workhousename,pcf.InvoiceCusXOId,p.ProductName,pcf.PCFinishCheckCount,pcf.PCFinishCheckInCoiunt,e0.EmployeeName as Employee0Name,e1.EmployeeName as Employee1Name from PCFinishCheck pcf left join WorkHouse wh on pcf.WorkHouseId=wh.WorkHouseId left join Product p on pcf.ProductId=p.ProductId  left join  Employee e0 on pcf.Employee0Id=e0.EmployeeId left join Employee e1 on pcf.Employee1Id=e1.EmployeeId where pcf.PCFinishCheckDate BETWEEN '" + startdate.Date + "' AND '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd") + "'");
            if (!string.IsNullOrEmpty(customerProductName))
                sql.Append(" and customerProductName='" + customerProductName + "' ");
            if (!string.IsNullOrEmpty(CusXOId))
                sql.Append(" and InvoiceCusXOId like '%" + CusXOId + "%'");
            if (product != null)
                sql.Append(" and p.ProductId = '" + product.ProductId + "'");
            sql.Append("  ORDER BY pcf.PCFinishCheckID desc");
            return this.DataReaderBind<Model.PCFinishCheck>(sql.ToString(), null, CommandType.Text);
        }

        public string SelectByInvoiceCusID(string ID)
        {
            return sqlmapper.QueryForObject<string>("PCFinishCheck.SelectByInvoiceCusID", ID);
        }

        public Model.PCFinishCheck SelectByPronoteHeader(string id)
        {
            return sqlmapper.QueryForObject<Model.PCFinishCheck>("PCFinishCheck.SelectByPronoteHeader", id);
        }
    }
}
