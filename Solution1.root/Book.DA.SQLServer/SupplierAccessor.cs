//------------------------------------------------------------------------------
//
// file name：SupplierAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:28
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
    /// Data accessor of Supplier
    /// </summary>
    public partial class SupplierAccessor : EntityAccessor, ISupplierAccessor
    {
        #region ISupplierAccessor Members


        //public void Update(Book.Model.Supplier supplier, string newId)
        //{
        //    Hashtable pars = new Hashtable();
        //    pars.Add("supplier", supplier);
        //    pars.Add("newId", newId);
        //    sqlmapper.Update("Supplier.newupdate", pars);
        //    supplier.SupplierId = newId;
        //}
        //public  IList<Model.Supplier> Select(string SupplierStart, string SupplierEnd, DateTime dateStart, DateTime dateEnd)
        // {
        //     Hashtable pars = new Hashtable();
        //     pars.Add("SupplierStart", SupplierStart);
        //     pars.Add("SupplierEnd", SupplierEnd);
        //     pars.Add("dateStart", dateStart);
        //     pars.Add("dateEnd", dateEnd);
        //     return sqlmapper.QueryForList<Model.Supplier>("Supplier.select_byQujianDate", pars);

        // }
        public IList<Book.Model.Supplier> Select(Model.SupplierCategory supplierCategory)
        {
            return sqlmapper.QueryForList<Model.Supplier>("Supplier.select_bySupplierCategoryId", supplierCategory.SupplierCategoryId);
        }

        public IList<Model.Supplier> Zhunshilv(DateTime startDate, DateTime endDate, string supplierCategoryIds)
        {
            //StringBuilder sb = new StringBuilder("select SUM(b.InvoiceQty) as InvoiceQty,SUM(b.OverDay) as OverDay,b.SupplierId,b.SupplierFullName,b.SupplierCategoryName from (");
            ////采购单-进库单
            //sb.Append("select COUNT(*) as InvoiceQty,sum(a.OverDay) as OverDay,a.SupplierId,a.SupplierFullName,a.SupplierCategoryName from (select cg.InvoiceId as cgID,cg.InvoiceDate as cgDate,co.InvoiceId as coID,co.InvoiceDate as coDate,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName,(case when DATEDIFF(d,co.InvoiceDate,cg.InvoiceDate)>'" + overDaySet + "' then 1 else 0 end) as OverDay from InvoiceCGDetail cgd left join InvoiceCG cg on cgd.InvoiceId=cg.InvoiceId left join InvoiceCODetail cod on cgd.InvoiceCODetailId=cod.InvoiceCODetailId left join InvoiceCO co on cod.InvoiceId=co.InvoiceId left join Supplier s on cg.SupplierId=s.SupplierId left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where co.InvoiceId is not null and cg.InvoiceDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(-1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ") group by cg.InvoiceId,cg.InvoiceDate,co.InvoiceId,co.InvoiceDate,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName) a group by a.SupplierId,a.SupplierFullName,a.SupplierCategoryName");
            //sb.Append(" union all ");
            ////加工单-生产入库单
            //sb.Append("select COUNT(*) as InvoiceQty,sum(a.OverDay) as OverDay,a.SupplierId,a.SupplierFullName,a.SupplierCategoryName from (select ph.PronoteHeaderId,ph.PronoteDate,pi.ProduceInDepotId,pi.ProduceInDepotDate,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName,(case when DATEDIFF(d,ph.PronoteDate,pi.ProduceInDepotDate)>'" + overDaySet + "' then 1 else 0 end) as OverDay from ProduceInDepotDetail pid left join ProduceInDepot pi on pid.ProduceInDepotId=pi.ProduceInDepotId left join PronoteHeader ph on ph.PronoteHeaderID=pid.PronoteHeaderId left join WorkHouse wh on wh.WorkHouseId=pi.WorkHouseId left join Supplier s on s.SupplierFullName like  '%'+wh.Workhousename+'%' left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where ph.PronoteHeaderID is not null and wh.Workhousename!=''and s.SupplierFullName is not null and pi.ProduceInDepotDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(-1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ")  group by ph.PronoteHeaderId,ph.PronoteDate,pi.ProduceInDepotId,pi.ProduceInDepotDate,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName) a group by a.SupplierId,a.SupplierFullName,a.SupplierCategoryName");
            //sb.Append(" union all ");
            ////委外合同-委外入库单
            //sb.Append("select COUNT(*) as InvoiceQty,sum(a.OverDay) as OverDay,a.SupplierId,a.SupplierFullName,a.SupplierCategoryName from (select pc.ProduceOtherCompactId,pc.ProduceOtherCompactDate,pi.ProduceOtherInDepotId,pi.ProduceOtherInDepotDate,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName,(case when DATEDIFF(d,pc.ProduceOtherCompactDate,pi.ProduceOtherInDepotDate)>'" + overDaySet + "' then 1 else 0 end) as OverDay  from ProduceOtherInDepotDetail pid left join ProduceOtherInDepot pi on pid.ProduceOtherInDepotId=pi.ProduceOtherInDepotId left join ProduceOtherCompactDetail pcd on pcd.OtherCompactDetailId=pid.ProduceOtherCompactDetailId left join ProduceOtherCompact pc on pc.ProduceOtherCompactId=pcd.ProduceOtherCompactId left join Supplier s on pi.SupplierId=s.SupplierId left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where pc.ProduceOtherCompactId is not null and pi.ProduceOtherInDepotDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(-1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ") group by pc.ProduceOtherCompactId,pc.ProduceOtherCompactDate,pi.ProduceOtherInDepotId,pi.ProduceOtherInDepotDate,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName) a group by a.SupplierId,a.SupplierFullName,a.SupplierCategoryName");
            //sb.Append(") b group by b.SupplierId,b.SupplierFullName,b.SupplierCategoryName");

            //string sqlCO = "select co.InvoiceId as InvoiceID1,co.InvoiceDate as InvoiceDate1,cg.InvoiceId as InvoiceID2,cg.InvoiceDate as InvoiceDate2,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName,DATEDIFF(d,co.InvoiceDate,cg.InvoiceDate)as OverDay from InvoiceCGDetail cgd left join InvoiceCG cg on cgd.InvoiceId=cg.InvoiceId left join InvoiceCODetail cod on cgd.InvoiceCODetailId=cod.InvoiceCODetailId left join InvoiceCO co on cod.InvoiceId=co.InvoiceId left join Supplier s on cg.SupplierId=s.SupplierId left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where co.InvoiceId is not null and cg.InvoiceDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ")";
            string sqlCO = "select co.InvoiceId as InvoiceID1,co.InvoiceYjrq as InvoiceDate1,cg.InvoiceId as InvoiceID2,cg.InvoiceDate as InvoiceDate2,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName,DATEDIFF(d,cg.InvoiceDate,co.InvoiceYjrq)as OverDay from InvoiceCGDetail cgd left join InvoiceCG cg on cgd.InvoiceId=cg.InvoiceId left join InvoiceCODetail cod on cgd.InvoiceCODetailId=cod.InvoiceCODetailId left join InvoiceCO co on cod.InvoiceId=co.InvoiceId left join Supplier s on cg.SupplierId=s.SupplierId left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where co.InvoiceId is not null and cg.InvoiceDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ")";


            //string sqlPNT = "select ph.PronoteHeaderId as InvoiceID1,ph.PronoteDate as InvoiceDate1,pi.ProduceInDepotId as InvoiceID2,pi.ProduceInDepotDate as InvoiceDate2,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName,DATEDIFF(d,ph.PronoteDate,pi.ProduceInDepotDate) as OverDay from ProduceInDepotDetail pid left join ProduceInDepot pi on pid.ProduceInDepotId=pi.ProduceInDepotId left join PronoteHeader ph on ph.PronoteHeaderID=pid.PronoteHeaderId left join WorkHouse wh on wh.WorkHouseId=pi.WorkHouseId left join Supplier s on s.SupplierFullName like  '%'+wh.Workhousename+'%' left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where ph.PronoteHeaderID is not null and wh.Workhousename!=''and s.SupplierFullName is not null and pi.ProduceInDepotDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ")";
            string sqlPNT = "select ph.PronoteHeaderId as InvoiceID1,xo.InvoiceYjrq as InvoiceDate1,pi.ProduceInDepotId as InvoiceID2,pi.ProduceInDepotDate as InvoiceDate2,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName,DATEDIFF(d,pi.ProduceInDepotDate,xo.InvoiceYjrq) as OverDay from ProduceInDepotDetail pid left join ProduceInDepot pi on pid.ProduceInDepotId=pi.ProduceInDepotId left join PronoteHeader ph on ph.PronoteHeaderID=pid.PronoteHeaderId left join WorkHouse wh on wh.WorkHouseId=pi.WorkHouseId left join Supplier s on s.SupplierFullName like  '%'+wh.Workhousename+'%' left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId left join InvoiceXO xo on xo.InvoiceId=ph.InvoiceXOId where ph.PronoteHeaderID is not null and wh.Workhousename!=''and s.SupplierFullName is not null and pi.ProduceInDepotDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ")";

            //string sqlPOC = "select pc.ProduceOtherCompactId as InvoiceID1,pc.ProduceOtherCompactDate as InvoiceDate1,pi.ProduceOtherInDepotId as InvoiceID2,pi.ProduceOtherInDepotDate as InvoiceDate2,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName, DATEDIFF(d,pc.ProduceOtherCompactDate,pi.ProduceOtherInDepotDate) as OverDay from ProduceOtherInDepotDetail pid left join ProduceOtherInDepot pi on pid.ProduceOtherInDepotId=pi.ProduceOtherInDepotId left join ProduceOtherCompactDetail pcd on pcd.OtherCompactDetailId=pid.ProduceOtherCompactDetailId left join ProduceOtherCompact pc on pc.ProduceOtherCompactId=pcd.ProduceOtherCompactId left join Supplier s on pi.SupplierId=s.SupplierId left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where pc.ProduceOtherCompactId is not null and pi.ProduceOtherInDepotDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ")";
            string sqlPOC = "select pc.ProduceOtherCompactId as InvoiceID1,pc.JiaoHuoDate as InvoiceDate1,pi.ProduceOtherInDepotId as InvoiceID2,pi.ProduceOtherInDepotDate as InvoiceDate2,s.SupplierId,s.SupplierFullName,sc.SupplierCategoryName, DATEDIFF(d,pi.ProduceOtherInDepotDate,pc.JiaoHuoDate) as OverDay from ProduceOtherInDepotDetail pid left join ProduceOtherInDepot pi on pid.ProduceOtherInDepotId=pi.ProduceOtherInDepotId left join ProduceOtherCompactDetail pcd on pcd.OtherCompactDetailId=pid.ProduceOtherCompactDetailId left join ProduceOtherCompact pc on pc.ProduceOtherCompactId=pcd.ProduceOtherCompactId left join Supplier s on pi.SupplierId=s.SupplierId left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where pc.ProduceOtherCompactId is not null and pi.ProduceOtherInDepotDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' and sc.Id in (" + supplierCategoryIds + ")";

            string sql = sqlCO + " union all " + sqlPNT + " union all " + sqlPOC;

            return this.DataReaderBind<Model.Supplier>(sql, null, CommandType.Text);
        }

        public IList<Model.Supplier> SelectNameAndCategoryByCategoryId(string supplierCategoryIds)
        {
            string sql = "select s.SupplierFullName,sc.SupplierCategoryName from Supplier s left join SupplierCategory sc on s.SupplierCategoryId=sc.SupplierCategoryId where sc.Id in (" + supplierCategoryIds + ")";

            return this.DataReaderBind<Model.Supplier>(sql, null, CommandType.Text);
        }
        #endregion
    }
}
