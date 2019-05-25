//------------------------------------------------------------------------------
//
// file name：PronoteHeaderAccessor.cs
// author: peidun
// create date：2009-12-29 11:58:39
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
    /// Data accessor of PronoteHeader
    /// </summary>
    public partial class PronoteHeaderAccessor : EntityAccessor, IPronoteHeaderAccessor
    {
        /// <summary>
        /// 生产入库选择加工单
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="customer"></param>
        /// <param name="cusxoid"></param>
        /// <param name="product"></param>
        /// <param name="PronoteHeaderIdStart"></param>
        /// <param name="PronoteHeaderIdEnd"></param>
        /// <param name="sourcetype"></param>
        /// <param name="workhouseIndepot"></param>
        /// <param name="jiean"></param>
        /// <returns></returns>
        public IList<Book.Model.PronoteHeader> GetByDate(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5, bool sourcetype7, DateTime JiaohuoDateStart, DateTime JiaohuoDateEnd, string MachineName)
        {
            SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@xocustomerId", DbType.String), new SqlParameter("@CustomerInvoiceXOId", DbType.String), new SqlParameter("@productid", DbType.String) };
            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (customer != null)
                parames[2].Value = customer.CustomerId;
            else
                parames[2].Value = DBNull.Value;
            if (!string.IsNullOrEmpty(cusxoid))
                parames[3].Value = cusxoid;
            else
                parames[3].Value = DBNull.Value;
            if (product != null)
                parames[4].Value = product.ProductId;
            else
                parames[4].Value = DBNull.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  w.Workhousename as WorkHouseNextName,a.Checkeds,a.IsClose,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId,a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId, a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity");
            //sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee0Id) as Employee0Name, (select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name");
            //sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee2Id) as Employee2Name ");
            //sql.Append(" , (SELECT  Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId =(SELECT TOP 1 WorkHouseId FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  ) ) AS WorkHouseNextName ");
            //   sql.Append(" , (SELECT TOP 1  ProduceTransferQuantity  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  )  AS ProduceTransferQuantity");
            // 本车间合格数量
            //sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and pr.ProduceInDepotId in (select ProduceInDepotid from ProduceInDepot where WorkHouseId='" + workhouseIndepot + "'))  AS HeJiCheckOutSum");
            ////前车间合格数量
            //sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and  WorkHouseId='" + workhouseIndepot + "') AS ProduceTransferQuantity");
            ////当前部门合计生产数量,出自<生产入库详细>
            //sql.Append(", (SELECT sum(isnull(p.ProceduresSum,0)) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderID AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProceduresSum");
            ////当前部门合计转生产数量
            //sql.Append(", (SELECT SUM(HeJiProduceTransferQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceTransferQuantity");
            ////当前部门合计入库数量
            //sql.Append(", (SELECT SUM(HeJiProduceQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceQuantity");
            //PronoteProceduresDate 订单交期
            sql.Append(",  i.CustomerInvoiceXOId,i.InvoiceYjrq as PronoteProceduresDate, (SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard");
            sql.Append(", (SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName");
            //if (!string.IsNullOrEmpty(workhouseIndepot))
            //{
            //    sql.Append(", (select top 1 PronoteProceduresDate from PronoteProceduresDetail u  where  u.PronoteHeaderID=a.PronoteHeaderID and u.WorkHouseId='" + workhouseIndepot + "'  order by PronoteProceduresDate ) as PronoteProceduresDate");
            //}
            //sql.Append(", (SELECT TOP 1 PronoteMachineId FROM PronoteProceduresDetail WHERE PronoteHeaderID=a.PronoteHeaderId ORDER BY ProceduresNo)  as PronoteMachineId");
            sql.Append(", (SELECT TOP 1 PronoteProceduresDate FROM PronoteProceduresDetail WHERE PronoteHeaderID=a.PronoteHeaderId ORDER BY ProceduresNo)  as Shechudata");
            sql.Append(",b.ProductName,b.id, b.CustomerProductName  FROM PronoteHeader a left join   Product b  on a.productid=b.productid  left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");

            sql.Append("  where    PronoteDate between @startdate and @enddate  ");
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and   i.CustomerInvoiceXOId  like '%'+@CustomerInvoiceXOId+'%'");
            if (customer != null)
                sql.Append(" and  i.xocustomerId=@xocustomerId");
            if (product != null)
                sql.Append(" and  a.productid=@productid");
            if (!string.IsNullOrEmpty(PronoteHeaderIdStart) && !string.IsNullOrEmpty(PronoteHeaderIdEnd))
                sql.Append(" and  a.PronoteHeaderID between '" + PronoteHeaderIdStart + "' and '" + PronoteHeaderIdEnd + "'");
            //if (sourcetype != -1)   //全部时为-1
            //    sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType=" + sourcetype + ")");
            if (jiean) // 只显示未结案
                sql.Append(" and  a.IsClose=0");
            if (!string.IsNullOrEmpty(proNameKey)) // 商品名称关键字
                sql.Append(" and b.ProductName like '%" + proNameKey + "%'");
            if (!string.IsNullOrEmpty(proCusNameKey)) //客户型号名称关键字
                sql.Append(" and b.CustomerProductName like '%" + proCusNameKey + "%'");
            if (!string.IsNullOrEmpty(pronoteHeaderIdKey)) // 加工单号关键字
                sql.Append(" and  a.PronoteHeaderID like '%" + pronoteHeaderIdKey + "%'");
            sql.Append(" And i.InvoiceYjrq between '" + JiaohuoDateStart.ToString("yyyy-MM-dd") + "' and '" + JiaohuoDateEnd.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");  //交货日期
            if (!string.IsNullOrEmpty(MachineName))     //机台号
                sql.Append(" and a.PronoteHeaderID in (SELECT PronoteHeaderID FROM PronoteProceduresDetail WHERE PronoteMachineId='" + MachineName + "')");
            //三种自制条件
            if (sourcetype0 && sourcetype4 && !sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4')) and 2=2)");
            else if (sourcetype0 && sourcetype5 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','5')) and 2=2)");
            else if (sourcetype4 && sourcetype5 && !sourcetype0)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4','5')) and 2=2)");
            else if (sourcetype0 && !sourcetype5 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0')) and 2=2)");
            else if (sourcetype4 && !sourcetype0 && !sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4')) and 2=2)");
            else if (sourcetype5 && !sourcetype0 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('5')) and 2=2)");
            else if (sourcetype0 && sourcetype4 && sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4','5')) and 2=2)");
            //在加一种自制条件--仓库(半成品加工)
            if (sourcetype7)
            {
                if (sql.ToString().Contains("and 2=2"))
                    sql.Replace("and 2=2", "or a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('7'))");
                else
                    sql.Append(" and a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('7'))");
            }

            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), parames, CommandType.Text);
        }

        /// <summary>
        /// 领料单选择加工单
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="customer"></param>
        /// <param name="cusxoid"></param>
        /// <param name="product"></param>
        /// <param name="PronoteHeaderIdStart"></param>
        /// <param name="PronoteHeaderIdEnd"></param>
        /// <param name="sourcetype"></param>
        /// <param name="workhouseIndepot"></param>
        /// <param name="jiean"></param>
        /// <param name="proNameKey"></param>
        /// <param name="proCusNameKey"></param>
        /// <param name="pronoteHeaderIdKey"></param>
        /// <returns></returns>
        public IList<Book.Model.PronoteHeader> GetByDateMa(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5, bool sourcetype7)
        {
            SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@xocustomerId", DbType.String), new SqlParameter("@CustomerInvoiceXOId", DbType.String), new SqlParameter("@productid", DbType.String) };
            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (customer != null)
                parames[2].Value = customer.CustomerId;
            else
                parames[2].Value = DBNull.Value;
            if (!string.IsNullOrEmpty(cusxoid))
                parames[3].Value = cusxoid;
            else
                parames[3].Value = DBNull.Value; ;
            if (product != null)
                parames[4].Value = product.ProductId;
            else
                parames[4].Value = DBNull.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  w.Workhousename,a.Checkeds,a.IsClose,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId,a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId, a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity");
            //sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee0Id) as Employee0Name, (select  EmployeeName from employee where employee.employeeid=a.AuditEmpId) as AuditEmpName");
            //  sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee2Id) as Employee2Name ");
            //  sql.Append(" , (SELECT  Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId =(SELECT TOP 1 WorkHouseId FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  ) ) AS WorkHouseNextName ");
            //   sql.Append(" , (SELECT TOP 1  ProduceTransferQuantity  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  )  AS ProduceTransferQuantity");
            // 本车间合格数量
            //  sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and pr.ProduceInDepotId in (select ProduceInDepotid from ProduceInDepot where WorkHouseId='" + workhouseIndepot + "'))  AS HeJiCheckOutSum");
            //前车间合格数量
            //  sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and  WorkHouseId='" + workhouseIndepot + "') AS ProduceTransferQuantity");
            //当前部门合计生产数量,出自<生产入库详细>
            // sql.Append(", (SELECT sum(isnull(p.ProceduresSum,0)) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderID AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProceduresSum");
            //当前部门合计转生产数量
            //   sql.Append(", (SELECT SUM(HeJiProduceTransferQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceTransferQuantity");
            //当前部门合计入库数量
            // sql.Append(", (SELECT SUM(HeJiProduceQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceQuantity");
            //PronoteProceduresDate 订单交期
            sql.Append(",  i.CustomerInvoiceXOId,i.InvoiceYjrq as PronoteProceduresDate, (SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard");
            sql.Append(", (SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName");
            //if (!string.IsNullOrEmpty(workhouseIndepot))
            //{
            //    sql.Append(", (select top 1 PronoteProceduresDate from PronoteProceduresDetail u  where  u.PronoteHeaderID=a.PronoteHeaderID and u.WorkHouseId='" + workhouseIndepot + "'  order by PronoteProceduresDate ) as PronoteProceduresDate");
            //}
            //sql.Append(", (SELECT TOP 1 PronoteMachineId FROM PronoteProceduresDetail WHERE PronoteHeaderID=a.PronoteHeaderId ORDER BY ProceduresNo)  as PronoteMachineId");
            sql.Append(", (SELECT TOP 1 PronoteProceduresDate FROM PronoteProceduresDetail WHERE PronoteHeaderID=a.PronoteHeaderId ORDER BY ProceduresNo)  as Shechudata");
            sql.Append(",b.ProductName,b.id, b.CustomerProductName,a.Chakuang,a.Paihe,a.Moshu,a.Materialprocessum  FROM PronoteHeader a left join   Product b  on a.productid=b.productid  left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");

            sql.Append("  where    PronoteDate between @startdate and @enddate  ");
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and   i.CustomerInvoiceXOId  like '%'+@CustomerInvoiceXOId+'%'");
            if (customer != null)
                sql.Append(" and  i.xocustomerId=@xocustomerId");
            if (product != null)
                sql.Append(" and  a.productid=@productid");
            if (!string.IsNullOrEmpty(PronoteHeaderIdStart) && !string.IsNullOrEmpty(PronoteHeaderIdEnd))
                sql.Append(" and  a.PronoteHeaderID between '" + PronoteHeaderIdStart + "' and '" + PronoteHeaderIdEnd + "'");
            if (sourcetype != -1)   //全部时为-1
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType=" + sourcetype + ")");
            if (jiean) // 只显示未结案
                sql.Append(" and  a.IsClose=0");
            if (!string.IsNullOrEmpty(proNameKey)) // 商品名称关键字
                sql.Append(" and b.ProductName like '%" + proNameKey + "%'");
            if (!string.IsNullOrEmpty(proCusNameKey)) //客户型号名称关键字
                sql.Append(" and b.CustomerProductName like '%" + proCusNameKey + "%'");
            if (!string.IsNullOrEmpty(pronoteHeaderIdKey)) // 加工单号关键字
                sql.Append(" and  a.PronoteHeaderID like '%" + pronoteHeaderIdKey + "%'");
            if (!string.IsNullOrEmpty(workhouseIndepot))   //公司部门
                sql.Append(" and w.WorkHouseId='" + workhouseIndepot + "'");

            //三种自制条件
            if (sourcetype0 && sourcetype4 && !sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4')) and 2=2)");
            else if (sourcetype0 && sourcetype5 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','5')) and 2=2)");
            else if (sourcetype4 && sourcetype5 && !sourcetype0)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4','5')) and 2=2)");
            else if (sourcetype0 && !sourcetype5 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0')) and 2=2)");
            else if (sourcetype4 && !sourcetype0 && !sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4')) and 2=2)");
            else if (sourcetype5 && !sourcetype0 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('5')) and 2=2)");
            else if (sourcetype0 && sourcetype4 && sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4','5')) and 2=2)");
            //在加一种自制条件--仓库(半成品加工)
            if (sourcetype7)
            {
                if (sql.ToString().Contains("and 2=2"))
                    sql.Replace("and 2=2", "or a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('7'))");
                else
                    sql.Append(" and a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('7'))");
            }

            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), parames, CommandType.Text);
        }

        //质检选择加工单      
        public IList<Book.Model.PronoteHeader> GetByDateZJ(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, string sign, bool sourcetype0, bool sourcetype4, bool sourcetype5, bool sourcetype7)
        {
            SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@xocustomerId", DbType.String), new SqlParameter("@CustomerInvoiceXOId", DbType.String), new SqlParameter("@productid", DbType.String) };
            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (customer != null)
                parames[2].Value = customer.CustomerId;
            else
                parames[2].Value = DBNull.Value;
            if (!string.IsNullOrEmpty(cusxoid))
                parames[3].Value = cusxoid;
            else
                parames[3].Value = DBNull.Value; ;
            if (product != null)
                parames[4].Value = product.ProductId;
            else
                parames[4].Value = DBNull.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  w.Workhousename as WorkHouseNextName,a.Checkeds,a.IsClose,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId,a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId, a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity");
            //sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee0Id) as Employee0Name, (select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name");
            //sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee2Id) as Employee2Name ");
            //  sql.Append(" , (SELECT  Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId =(SELECT TOP 1 WorkHouseId FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  ) ) AS WorkHouseNextName ");
            //   sql.Append(" , (SELECT TOP 1  ProduceTransferQuantity  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  )  AS ProduceTransferQuantity");
            // 本车间合格数量
            //  sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and pr.ProduceInDepotId in (select ProduceInDepotid from ProduceInDepot where WorkHouseId='" + workhouseIndepot + "'))  AS HeJiCheckOutSum");
            //前车间合格数量
            //sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and  WorkHouseId = (select WorkHouseId from WorkHouse where WorkHousename = '射出' )) AS ProduceTransferQuantity");
            //当前部门合计生产数量,出自<生产入库详细>
            // sql.Append(", (SELECT sum(isnull(p.ProceduresSum,0)) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderID AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProceduresSum");
            //当前部门合计转生产数量
            //   sql.Append(", (SELECT SUM(HeJiProduceTransferQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceTransferQuantity");
            //当前部门合计入库数量
            // sql.Append(", (SELECT SUM(HeJiProduceQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceQuantity");
            //PronoteProceduresDate 订单交期
            sql.Append(",  i.CustomerInvoiceXOId,i.InvoiceYjrq as PronoteProceduresDate, (SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard");
            sql.Append(", (SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName");
            //if (!string.IsNullOrEmpty(workhouseIndepot))
            //{
            //    sql.Append(", (select top 1 PronoteProceduresDate from PronoteProceduresDetail u  where  u.PronoteHeaderID=a.PronoteHeaderID and u.WorkHouseId='" + workhouseIndepot + "'  order by PronoteProceduresDate ) as PronoteProceduresDate");
            //}
            //sql.Append(", (SELECT TOP 1 PronoteMachineId FROM PronoteProceduresDetail WHERE PronoteHeaderID=a.PronoteHeaderId ORDER BY ProceduresNo)  as PronoteMachineId");
            sql.Append(", (SELECT TOP 1 PronoteProceduresDate FROM PronoteProceduresDetail WHERE PronoteHeaderID=a.PronoteHeaderId ORDER BY ProceduresNo)  as Shechudata");
            sql.Append(",b.ProductName,b.id, b.CustomerProductName FROM PronoteHeader a left join   Product b  on a.productid=b.productid  left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");


            sql.Append("  where    PronoteDate between @startdate and @enddate  ");
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and   i.CustomerInvoiceXOId  like '%'+@CustomerInvoiceXOId+'%'");
            if (customer != null)
                sql.Append(" and  i.xocustomerId=@xocustomerId");
            if (product != null)
                sql.Append(" and  a.productid=@productid");
            if (!string.IsNullOrEmpty(PronoteHeaderIdStart) && !string.IsNullOrEmpty(PronoteHeaderIdEnd))
                sql.Append(" and  a.PronoteHeaderID between '" + PronoteHeaderIdStart + "' and '" + PronoteHeaderIdEnd + "'");
            //if (sourcetype != -1)   //全部时为-1
            //    sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType=" + sourcetype + ")");
            if (jiean) // 只显示未结案
                sql.Append(" and  a.IsClose=0");
            if (!string.IsNullOrEmpty(proNameKey)) // 商品名称关键字
                sql.Append(" and b.ProductName like '%" + proNameKey + "%'");
            if (!string.IsNullOrEmpty(proCusNameKey)) //客户型号名称关键字
                sql.Append(" and b.CustomerProductName like '%" + proCusNameKey + "%'");
            if (!string.IsNullOrEmpty(pronoteHeaderIdKey)) // 加工单号关键字
                sql.Append(" and a.PronoteHeaderID like '%" + pronoteHeaderIdKey + "%'");
            if (sign == "Check")
                sql.Append(" and not exists (SELECT PCFinishCheckID FROM PCFinishCheck WHERE PCFinishCheck.PronoteHeaderID=a.PronoteHeaderID)");

            //三种自制条件
            if (sourcetype0 && sourcetype4 && !sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4')) and 2=2)");
            else if (sourcetype0 && sourcetype5 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','5')) and 2=2)");
            else if (sourcetype4 && sourcetype5 && !sourcetype0)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4','5')) and 2=2)");
            else if (sourcetype0 && !sourcetype5 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0')) and 2=2)");
            else if (sourcetype4 && !sourcetype0 && !sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4')) and 2=2)");
            else if (sourcetype5 && !sourcetype0 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('5')) and 2=2)");
            else if (sourcetype0 && sourcetype4 && sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4','5')) and 2=2)");
            //在加一种自制条件--仓库(半成品加工)
            if (sourcetype7)
            {
                if (sql.ToString().Contains("and 2=2"))
                    sql.Replace("and 2=2", "or a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('7'))");
                else
                    sql.Append(" and a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('7'))");
            }

            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), parames, CommandType.Text);
        }

        //数据输入页 选择加工单
        public IList<Book.Model.PronoteHeader> GetByDateDI(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5, bool sourcetype7)
        {
            SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@xocustomerId", DbType.String), new SqlParameter("@CustomerInvoiceXOId", DbType.String), new SqlParameter("@productid", DbType.String) };
            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (customer != null)
                parames[2].Value = customer.CustomerId;
            else
                parames[2].Value = DBNull.Value;
            if (!string.IsNullOrEmpty(cusxoid))
                parames[3].Value = cusxoid;
            else
                parames[3].Value = DBNull.Value; ;
            if (product != null)
                parames[4].Value = product.ProductId;
            else
                parames[4].Value = DBNull.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  w.Workhousename as WorkHouseNextName,a.Checkeds,a.IsClose,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId,a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId, a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity");
            sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee0Id) as Employee0Name, (select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name");
            sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee2Id) as Employee2Name ");
            sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and  WorkHouseId = (select WorkHouseId from WorkHouse where WorkHousename = '射出' )) AS ProduceTransferQuantity");
            sql.Append(",  i.CustomerInvoiceXOId,i.InvoiceYjrq as PronoteProceduresDate, (SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard");
            sql.Append(", (SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName");
            sql.Append(", (SELECT TOP 1 PronoteMachineId FROM PronoteProceduresDetail WHERE PronoteHeaderID=a.PronoteHeaderId ORDER BY ProceduresNo)  as PronoteMachineId");
            sql.Append(", (SELECT TOP 1 PronoteProceduresDate FROM PronoteProceduresDetail WHERE PronoteHeaderID=a.PronoteHeaderId ORDER BY ProceduresNo)  as Shechudata");
            sql.Append(",b.ProductName,b.id, b.CustomerProductName FROM PronoteHeader a left join   Product b  on a.productid=b.productid  left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");

            sql.Append("  where    PronoteDate between @startdate and @enddate  ");
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and   i.CustomerInvoiceXOId  like '%'+@CustomerInvoiceXOId+'%'");
            if (customer != null)
                sql.Append(" and  i.xocustomerId=@xocustomerId");
            if (product != null)
                sql.Append(" and  a.productid=@productid");
            if (!string.IsNullOrEmpty(PronoteHeaderIdStart) && !string.IsNullOrEmpty(PronoteHeaderIdEnd))
                sql.Append(" and  a.PronoteHeaderID between '" + PronoteHeaderIdStart + "' and '" + PronoteHeaderIdEnd + "'");
            if (jiean) // 只显示未结案
                sql.Append(" and  a.IsClose=0");
            if (!string.IsNullOrEmpty(proNameKey)) // 商品名称关键字
                sql.Append(" and b.ProductName like '%" + proNameKey + "%'");
            if (!string.IsNullOrEmpty(proCusNameKey)) //客户型号名称关键字
                sql.Append(" and b.CustomerProductName like '%" + proCusNameKey + "%'");
            if (!string.IsNullOrEmpty(pronoteHeaderIdKey)) // 加工单号关键字
                sql.Append(" and a.PronoteHeaderID like '%" + pronoteHeaderIdKey + "%'");
            //2017年9月11日00:43:22
            sql.Append(" and a.PronoteHeaderID not in (select PronoteHeaderId from PCDataInput)"); //数据输入也 只能拉一次 PNT

            //三种自制条件
            if (sourcetype0 && sourcetype4 && !sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4')) and 2=2)");
            else if (sourcetype0 && sourcetype5 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','5')) and 2=2)");
            else if (sourcetype4 && sourcetype5 && !sourcetype0)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4','5')) and 2=2)");
            else if (sourcetype0 && !sourcetype5 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0')) and 2=2)");
            else if (sourcetype4 && !sourcetype0 && !sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4')) and 2=2)");
            else if (sourcetype5 && !sourcetype0 && !sourcetype4)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('5')) and 2=2)");
            else if (sourcetype0 && sourcetype4 && sourcetype5)
                sql.Append(" and  (a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4','5')) and 2=2)");
            //在加一种自制条件--仓库(半成品加工)
            if (sourcetype7)
            {
                if (sql.ToString().Contains("and 2=2"))
                    sql.Replace("and 2=2", "or a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('7'))");
                else
                    sql.Append(" and a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('7'))");
            }

            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), parames, CommandType.Text);
        }

        public IList<Book.Model.PronoteHeader> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd, string CusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startcustomerid", customerStart);
            ht.Add("endcustomerid", customerEnd);
            ht.Add("startdate", dateStart);
            ht.Add("enddate", dateEnd);
            ht.Add("cusxoid", CusXOId);
            return sqlmapper.QueryForList<Book.Model.PronoteHeader>("PronoteHeader.select_byCustomerANDdate", ht);
        }

        public IList<Book.Model.PronoteHeader> Select(Model.MRSHeader mrsheader)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT w.Workhousename,a.Checkeds,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId, a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId,a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity,(select  EmployeeName from employee where employee.employeeid=a.Employee0Id) as Employee0Name,(select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name,(select  EmployeeName from employee where employee.employeeid=a.Employee2Id) as Employee2Name,i.CustomerInvoiceXOId,(SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard,(SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName,b.ProductName,b.id,b.CustomerProductName,b.ProductDescription as ProductDesc FROM PronoteHeader a left join   Product b  on a.productid=b.productid left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");
            sql.Append("  where  a.MRSHeaderId= '" + mrsheader.MRSHeaderId + "'");
            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), null, CommandType.Text);

            // return sqlmapper.QueryForList<Book.Model.PronoteHeader>("PronoteHeader.select_bymrsheader", mrsheader.MRSHeaderId);
        }

        public IList<Book.Model.PronoteHeader> Select(IList<string> ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" a.PronoteHeaderID='" + ids[0] + "'");
            for (int i = 1; i < ids.Count; i++)
            {
                sb.Append(" or a.PronoteHeaderID='" + ids[i] + "'");

            }


            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT w.Workhousename,a.Checkeds,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId, a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId,a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity,(select  EmployeeName from employee where employee.employeeid=a.Employee0Id) as Employee0Name,(select  EmployeeName from employee where employee.employeeid=a.AuditEmpId) as AuditEmpName,(select  EmployeeName from employee where employee.employeeid=a.Employee2Id) as Employee2Name,i.CustomerInvoiceXOId,(SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard,(SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName,b.ProductName,b.id,b.CustomerProductName,b.ProductDescription as ProductDesc,a.Chakuang,a.Paihe,a.Moshu FROM PronoteHeader a left join   Product b  on a.productid=b.productid left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");
            sql.Append("  where  " + sb.ToString());
            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), null, CommandType.Text);

            // return sqlmapper.QueryForList<Book.Model.PronoteHeader>("PronoteHeader.select_bymrsheader", mrsheader.MRSHeaderId);
        }

        #region 加工单 生产入库单合格数量数量
        //public void UpdatePronoteIsClose(string pronoteheaderid, double? indepotquantity) 
        //{
        //    Hashtable ht = new Hashtable();
        //    ht.Add("pronoteHeaderid", pronoteheaderid);
        //    ht.Add("inDepotSum", indepotquantity);
        //    sqlmapper.Update("PronoteHeader.update_PronoteHeaderIsClse", ht);
        //}
        #endregion

        //加工单 生产入库单合格数量数量
        public void UpdateHeaderIsClse(string pronoteheaderid, bool isclose)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pronoteHeaderid", pronoteheaderid);
            ht.Add("isclose", isclose);
            sqlmapper.Update("PronoteHeader.update_HeaderIsClse", ht);
        }

        public void UpdateHeaderIsClseByXOId(string InvoiceXOId, bool isclose)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceXOId", InvoiceXOId);
            ht.Add("isclose", isclose);
            sqlmapper.Update("PronoteHeader.update_HeaderIsClseByXOId", ht);
        }

        public IList<Model.PronoteHeader> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.PronoteHeader>("PronoteHeader.SelectByHeaderId", id);
        }

        public string SelectByInvoiceCusID(string ID, string invoiceType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceType", invoiceType);
            ht.Add("CusId", ID);
            return sqlmapper.QueryForObject<string>("PronoteHeader.SelectByInvoiceCusID", ht);
        }

        public Model.PronoteHeader GetLast(string InvoiceType)
        {
            return sqlmapper.QueryForObject<Model.PronoteHeader>("PronoteHeader.mGetLast", InvoiceType);
        }

        public Model.PronoteHeader mGetFirst(string InvoiceType)
        {
            return sqlmapper.QueryForObject<Model.PronoteHeader>("PronoteHeader.mGetFirst", InvoiceType);
        }

        public bool mHasRows(string InvoiceType)
        {
            return sqlmapper.QueryForObject<bool>("PronoteHeader.mHasRows", InvoiceType);
        }

        public bool mHasRowsAfter(Model.PronoteHeader p)
        {
            return sqlmapper.QueryForObject<bool>("PronoteHeader.mHasRowsAfter", p);
        }

        public bool mHasRowsBefore(Model.PronoteHeader p)
        {
            return sqlmapper.QueryForObject<bool>("PronoteHeader.mHasRowsBefore", p);
        }

        public Model.PronoteHeader mGetPrev(Model.PronoteHeader p)
        {
            return sqlmapper.QueryForObject<Model.PronoteHeader>("PronoteHeader.mGetPrev", p);
        }

        public Model.PronoteHeader mGetNext(Model.PronoteHeader p)
        {
            return sqlmapper.QueryForObject<Model.PronoteHeader>("PronoteHeader.mGetNext", p);
        }

        public DataTable GetExcelData(DateTime startDate, DateTime endDate, string workHouseId)
        {
            //string sql = "select ph.PronoteHeaderID,xo.CustomerInvoiceXOId,Convert(varchar(50),xo.InvoiceYjrq,23) as InvoiceYjrq,p.ProductName,ph.DetailsSum,(select sum(ProceduresSum) from ProduceInDepotDetail where PronoteHeaderId=ph.PronoteHeaderID) as TotalProcess,(select sum(CheckOutSum)from ProduceInDepotDetail where PronoteHeaderId=ph.PronoteHeaderID)as TotalPass,ph.ProductUnit,(select top 1 Convert(varchar(50),PronoteProceduresDate,23) from PronoteProceduresDetail where PronoteHeaderId=ph.PronoteHeaderID order by ProceduresNo) as LastDate,(select top 1 PronoteMachineId from PronoteProceduresDetail where PronoteHeaderId=ph.PronoteHeaderID order by ProceduresNo) as MachineId from PronoteHeader ph left join InvoiceXO xo on ph.InvoiceXOId=xo.InvoiceId left join Product p on ph.ProductId=p.ProductId where ph.PronoteDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and ph.InvoiceType=0 and ((select sum(CheckOutSum)from ProduceInDepotDetail where PronoteHeaderId=ph.PronoteHeaderID)<DetailsSum or (select sum(CheckOutSum)from ProduceInDepotDetail where PronoteHeaderId=ph.PronoteHeaderID) is null)";

            //string sql = "select ph.PronoteHeaderID,xo.CustomerInvoiceXOId,Convert(varchar(50),xo.InvoiceYjrq,23) as InvoiceYjrq,p.ProductName,ph.DetailsSum,(select sum(ProceduresSum) from ProduceInDepotDetail where PronoteHeaderId=ph.PronoteHeaderID) as TotalProcess,(select sum(CheckOutSum)from ProduceInDepotDetail where PronoteHeaderId=ph.PronoteHeaderID)as TotalPass,ph.ProductUnit,(select top 1 Convert(varchar(50),PronoteProceduresDate,23) from PronoteProceduresDetail where PronoteHeaderId=ph.PronoteHeaderID order by ProceduresNo) as LastDate,(select top 1 PronoteMachineId from PronoteProceduresDetail where PronoteHeaderId=ph.PronoteHeaderID order by ProceduresNo) as MachineId from PronoteHeader ph left join InvoiceXO xo on ph.InvoiceXOId=xo.InvoiceId left join Product p on ph.ProductId=p.ProductId where ph.PronoteDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and (ph.IsClose=0 or ph.IsClose is null) and (select top 1 PronoteProceduresDate from PronoteProceduresDetail where PronoteHeaderId=ph.PronoteHeaderID order by ProceduresNo)<GETDATE() and (select isnull(sum(CheckOutSum),0) from ProduceInDepotDetail where PronoteHeaderId=ph.PronoteHeaderID)<DetailsSum and (ph.WorkHouseId='" + workHouseId + "')";
            string sql = "select ph.PronoteHeaderID,xo.CustomerInvoiceXOId,Convert(varchar(50),xo.InvoiceYjrq,23) as InvoiceYjrq,p.ProductName,ph.DetailsSum,(select isnull(sum(ProceduresSum),0) from ProduceInDepotDetail pid where PronoteHeaderId=ph.PronoteHeaderID  and ProductId=ph.ProductId and (select WorkHouseId from ProduceInDepot where ProduceInDepotId=pid.ProduceInDepotId)='" + workHouseId + "') as TotalProcess,(select isnull(sum(CheckOutSum),0) from ProduceInDepotDetail pid where PronoteHeaderId=ph.PronoteHeaderID and ProductId=ph.ProductId and (select WorkHouseId from ProduceInDepot where ProduceInDepotId=pid.ProduceInDepotId)='" + workHouseId + "')as TotalPass,ph.ProductUnit,(select top 1 Convert(varchar(50),PronoteProceduresDate,23) from PronoteProceduresDetail where PronoteHeaderId=ph.PronoteHeaderID order by ProceduresNo) as LastDate,(select top 1 PronoteMachineId from PronoteProceduresDetail where PronoteHeaderId=ph.PronoteHeaderID order by ProceduresNo) as MachineId from PronoteHeader ph left join InvoiceXO xo on ph.InvoiceXOId=xo.InvoiceId left join Product p on ph.ProductId=p.ProductId where ph.PronoteDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and (ph.IsClose=0 or ph.IsClose is null) and (select top 1 PronoteProceduresDate from PronoteProceduresDetail where PronoteHeaderId=ph.PronoteHeaderID order by ProceduresNo)<GETDATE() and (select isnull(sum(CheckOutSum),0) from ProduceInDepotDetail pid where PronoteHeaderId=ph.PronoteHeaderID and ProductId=ph.ProductId and (select WorkHouseId from ProduceInDepot where ProduceInDepotId=pid.ProduceInDepotId)='" + workHouseId + "')<DetailsSum ";

            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            return dt;
        }
    }
}
