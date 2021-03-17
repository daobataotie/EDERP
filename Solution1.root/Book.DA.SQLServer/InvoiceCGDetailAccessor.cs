//------------------------------------------------------------------------------
//
// file name:InvoiceCGDetailAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of InvoiceCGDetail
    /// </summary>
    public partial class InvoiceCGDetailAccessor : EntityAccessor, IInvoiceCGDetailAccessor
    {
        public IList<Book.Model.InvoiceCGDetail> Select(Book.Model.InvoiceCG invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceCG invoice)
        {
            sqlmapper.Delete("InvoiceCGDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Book.Model.InvoiceCGDetail> Select(DateTime startDate, DateTime endDate, string startId, string endId)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startDate", startDate);
            pars.Add("endDate", endDate);
            pars.Add("startId", startId);
            pars.Add("endId", endId);
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.selectbyDateReangeAndIdReange", pars);
        }

        public IList<Book.Model.InvoiceCGDetail> Select(DateTime startDate, DateTime endDate, string csid, string ceid, string psid, string peid)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startDate", startDate);
            pars.Add("endDate", endDate);
            pars.Add("csid", csid);
            pars.Add("ceid", ceid);
            pars.Add("psid", psid);
            pars.Add("peid", peid);
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.selectbyDateReangeAndProductReangeCompanyReange", pars);
        }
        public IList<Book.Model.InvoiceCGDetail> SelectCount(Book.Model.InvoiceCO invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.select_count", invoice.InvoiceId);
        }

        public IList<Model.InvoiceCGDetail> SelectbyinvoiceIdfz(Model.InvoiceCG invoicecg)
        {
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.selectbyinvoiceIdfz", invoicecg.InvoiceId);
        }

        public IList<Model.InvoiceCGDetail> Select(Model.InvoiceCG invoice, Model.Product productStart, Model.Product productEnd)
        {
            IList<Book.Model.InvoiceCGDetail> invoicecg = null;
            Hashtable ht = new Hashtable();

            ht.Add("invoiceId", invoice.InvoiceId);
            ht.Add("productStart", productStart == null ? null : productStart.ProductName);
            ht.Add("productEnd", productEnd == null ? null : productEnd.ProductName);
            return sqlmapper.QueryForList<Book.Model.InvoiceCGDetail>("InvoiceCGDetail.selectByProductIdQuJian", ht);

        }

        public Model.InvoiceCGDetail SelectByProductIdAndHeadIdAndPositionId(string productId, string invoiceId, string positionId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("invoiceId", invoiceId);
            ht.Add("depotpositionId", positionId);
            return sqlmapper.QueryForObject<Model.InvoiceCGDetail>("InvoiceCGDetail.selectByProductIdAndHeadIdAndPositionId", ht);
        }

        public double GetSumByProductIdAndInvoiceId(string productId, string invoiceId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("invoiceId", invoiceId);
            return sqlmapper.QueryForObject<double>("InvoiceCGDetail.GetSumByProductIdAndInvoiceId", ht);
        }

        public IList<Book.Model.InvoiceCGDetail> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.SelectByDateRange", ht);
        }

        public void UpdateInvoiceCGDetailFPQuantityById(string id, string InvoiceCGDetailFPQuantity)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", id);
            ht.Add("InvoiceCGDetailFPQuantity", InvoiceCGDetailFPQuantity);
            sqlmapper.Update("InvoiceCGDetail.UpdateInvoiceCGDetailFPQuantityById", ht);
        }

        public DateTime SelectLastInvoiceCGDate(string productId, string depotpositionId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("depotpositionId", depotpositionId);
            return sqlmapper.QueryForObject<DateTime>("InvoiceCGDetail.SelectLastInvoiceCGDate", ht);
        }

        //应付账款明细表
        public DataTable SelectByConditionCOBiao(DateTime? startdate, DateTime? enddate, DateTime JHstartdate, DateTime JHenddate, DateTime? FKStartDate, DateTime? FKEndDate, Book.Model.Supplier startSupplier, Book.Model.Supplier endSupplier, Book.Model.Product productStart, Book.Model.Product productEnd, string coidStart, string coidEnd, string CusXOid, Book.Model.Employee empstart, Book.Model.Employee empend)
        {
            StringBuilder sb_cg = new StringBuilder("SELECT InvoiceId AS JHDN,(SELECT InvoiceDate FROM InvoiceCG WHERE InvoiceId = InvoiceCGDetail.InvoiceId) AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE Product.ProductId = InvoiceCGDetail.ProductId) AS ProductName,InvoiceCGDetailQuantity AS JHSL,InvoiceProductUnit AS ProductUnit,cast(ISNULL(InvoiceCGDetailPrice,0) as decimal(18,4)) AS DanJia,ISNULL(InvoiceCGDetailMoney,0) AS JinE,InvoiceCOId AS CGorWWDanHao,Inumber AS Inumber,isnull(InvoiceCGDetailTax,0) AS ShuiE,ISNULL(InvoiceCGDetailMoney,0)+isnull(InvoiceCGDetailTax,0) as Total FROM InvoiceCGDetail WHERE 1=1 ");            //进货
            //StringBuilder sb_ct = new StringBuilder("SELECT InvoiceId AS JHDN,(SELECT InvoiceDate FROM InvoiceCT WHERE InvoiceId = InvoiceCTDetail.InvoiceId) AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE ProductId = InvoiceCTDetail.ProductId) AS ProductName,InvoiceCTDetailQuantity AS JHSL,InvoiceProductUnit AS ProductUnit,cast(ISNULL(InvoiceCTDetailPrice,0) as decimal(18,4)) AS DanJia,ISNULL((0-InvoiceCTDetailMoney1) ,0) AS JinE,InvoiceCOId AS CGorWWDanHao,Inumber AS Inumber,ISNULL((0-InvoiceCTDetailMoney1) ,0)*0.05 AS ShuiE,ISNULL((0-InvoiceCTDetailMoney1) ,0)+ ISNULL((0-InvoiceCTDetailMoney1) ,0)*0.05 as Total  FROM InvoiceCTDetail WHERE 1=1 ");            //退货
            StringBuilder sb_ct = new StringBuilder("SELECT ctd.InvoiceId AS JHDN,ct.InvoiceDate AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE ProductId = ctd.ProductId) AS ProductName,InvoiceCTDetailQuantity AS JHSL,InvoiceProductUnit AS ProductUnit,cast(ISNULL(InvoiceCTDetailPrice,0) as decimal(18,4)) AS DanJia,ISNULL((0-InvoiceCTDetailMoney1) ,0) AS JinE,ctd.InvoiceCOId AS CGorWWDanHao,Inumber AS Inumber,ISNULL((0-InvoiceCTDetailMoney1) ,0)*((case ct.InvoiceKslb when '外加' then ct.InvoiceTaxRate else 0 end)/100) AS ShuiE,ISNULL((0-InvoiceCTDetailMoney1) ,0)*(1+((case ct.InvoiceKslb when '外加' then ct.InvoiceTaxRate else 0 end)/100)) as Total  FROM InvoiceCTDetail ctd left join InvoiceCT ct on ctd.InvoiceId=ct.InvoiceId WHERE 1=1 ");
            //StringBuilder sb_ww = new StringBuilder("SELECT ProduceOtherCompactId AS JHDN,(SELECT ProduceOtherCompactDate FROM ProduceOtherCompact WHERE ProduceOtherCompactId = ProduceOtherCompactDetail.ProduceOtherCompactId) AS JHRQ,(SELECT ProductName FROM Product WHERE ProductId = ProduceOtherCompactDetail.ProductId) AS ProudctName,OtherCompactCount AS JHSL,ProductUnit AS ProductUnit,cast(ISNULL(OtherCompactPrice,0) as decimal(10,4)) AS DanJia,ISNULL(OtherCompactMoney,0) AS JinE,'' AS CGorWWDanHao FROM ProduceOtherCompactDetail WHERE 1=1 ");  //委外加工
            //StringBuilder sb_sr = new StringBuilder("SELECT ProduceInDepotId AS JHDN,(SELECT ProduceInDepotDate FROM ProduceInDepot WHERE ProduceInDepot.ProduceInDepotId = ProduceInDepotDetail.ProduceInDepotId) AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE Product.ProductId = ProduceInDepotDetail.ProductId) AS ProudctName,CheckOutSum AS JHSL,ProductUnit AS ProductUnit,cast(ISNULL(ProduceInDepotPrice,0) as decimal(18,4)) AS DanJia,isnull(ProduceMoney,0) AS JinE,PronoteHeaderId AS CGorWWDanHao,Inumber AS Inumber,isnull(ProduceMoney,0)*0.05 AS ShuiE,isnull(ProduceMoney,0)*0.05+ isnull(ProduceMoney,0) as Total  FROM ProduceInDepotDetail WHERE 1 = 1 ");  //生产入库
            StringBuilder sb_sr = new StringBuilder("SELECT pid.ProduceInDepotId AS JHDN,pi.ProduceInDepotDate AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE Product.ProductId = pid.ProductId) AS ProudctName,CheckOutSum AS JHSL,ProductUnit AS ProductUnit,cast(ISNULL(ProduceInDepotPrice,0) as decimal(18,4)) AS DanJia,isnull(ProduceMoney,0) AS JinE,PronoteHeaderId AS CGorWWDanHao,Inumber AS Inumber,isnull(ProduceMoney,0)*(cast(ISNULL(pi.InvoiceTaxrate,0) as decimal(18,2))/100) AS ShuiE,isnull(ProduceMoney,0)*(1+(cast(ISNULL(pi.InvoiceTaxrate,0) as decimal(18,2))/100)) as Total  FROM ProduceInDepotDetail pid left join ProduceInDepot pi on pid.ProduceInDepotId=pi.ProduceInDepotId WHERE 1 = 1 ");  //生产入库
            //StringBuilder sb_wr = new StringBuilder("SELECT ProduceOtherInDepotId AS JHDN,(SELECT ProduceOtherInDepotDate FROM ProduceOtherInDepot WHERE ProduceOtherInDepotDetail.ProduceOtherInDepotId = ProduceOtherInDepot.ProduceOtherInDepotId) AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE Product.ProductId = ProduceOtherInDepotDetail.ProductId) AS ProudctName,isnull(ProduceQuantity,0) AS JHSL,ProductUnit AS ProductUnit,cast(ISNULL(ProcessPrice,0) as decimal(18,4)) AS DanJia,isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4)) AS JinE,ProduceOtherCompactId AS CGorWWDanHao,'' AS Inumber,isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4))*0.05 AS ShuiE,isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4))+ isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4))*0.05 as Total FROM ProduceOtherInDepotDetail WHERE 1 = 1");//委外入库
            StringBuilder sb_wr = new StringBuilder("SELECT pod.ProduceOtherInDepotId AS JHDN,po.ProduceOtherInDepotDate AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE Product.ProductId = pod.ProductId) AS ProudctName,isnull(ProduceQuantity,0) AS JHSL,ProductUnit AS ProductUnit,cast(ISNULL(ProcessPrice,0) as decimal(18,4)) AS DanJia,isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4)) AS JinE,pod.ProduceOtherCompactId AS CGorWWDanHao,'' AS Inumber,isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4))*cast(ISNULL(InvoiceTaxrate,0) as decimal(18,2))/100 AS ShuiE,isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4))*(1+cast(ISNULL(InvoiceTaxrate,0) as decimal(18,2))/100) as Total FROM ProduceOtherInDepotDetail pod left join ProduceOtherInDepot po on pod.ProduceOtherInDepotId=po.ProduceOtherInDepotId WHERE 1 = 1");  //2020年12月10日23:20:42：委外入库,改left join

            //StringBuilder sb_wt = new StringBuilder("SELECT ProduceOtherReturnMaterialId  AS JHDN,(SELECT ProduceOtherReturnMaterialDate FROM ProduceOtherReturnMaterial WHERE ProduceOtherReturnMaterial.ProduceOtherReturnMaterialId=pd.ProduceOtherReturnMaterialId) AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE Product.ProductId=pd.ProductId) AS ProductName,Quantity AS JHSL,ProductUnit AS ProductUnit,cast(isnull(Price,0) AS decimal(18,4)) AS DanJia,isnull((0-Amount),0) AS JinE,ProduceOtherCompactId AS CGorWWDanHao,'' AS Inumber,isnull((0-Amount),0)*0.05 AS ShuiE,isnull((0-Amount),0)*0.05+isnull((0-Amount),0) AS Total FROM ProduceOtherReturnDetail  pd WHERE 1=1");//委外退库
            StringBuilder sb_wt = new StringBuilder("SELECT pd.ProduceOtherReturnMaterialId  AS JHDN,pm.ProduceOtherReturnMaterialDate AS JHRQ,(SELECT ProductName+'{'+ISNULL(CustomerProductName,'')+'}' FROM Product WHERE Product.ProductId=pd.ProductId) AS ProductName,Quantity AS JHSL,ProductUnit AS ProductUnit,cast(isnull(Price,0) AS decimal(18,4)) AS DanJia,isnull((0-Amount),0) AS JinE,ProduceOtherCompactId AS CGorWWDanHao,'' AS Inumber,isnull((0-Amount),0)*(CAST(ISNULL(pm.InvoiceTaxrate,0) as decimal(18,2))/100) AS ShuiE,isnull((0-Amount),0)*(1+(CAST(ISNULL(pm.InvoiceTaxrate,0) as decimal(18,2))/100)) AS Total FROM ProduceOtherReturnDetail  pd left join ProduceOtherReturnMaterial pm on pd.ProduceOtherReturnMaterialId=pm.ProduceOtherReturnMaterialId WHERE 1=1 ");//委外退库
            StringBuilder sb_qf = new StringBuilder("SELECT AcOtherShouldPaymentId AS JHDN,(SELECT AcOtherShouldPaymentDate FROM AcOtherShouldPayment WHERE AcOtherShouldPayment.AcOtherShouldPaymentId = AcOtherShouldPaymentDetail.AcOtherShouldPaymentId) AS JHRQ,LoanName AS ProudctName,AcQuantity AS JHSL,'' AS ProductUnit,cast(ISNULL(AcItemPrice,0) as decimal(18,4)) AS DanJia,AcMoney AS JinE,'' AS CGorWWDanHao,'' AS Inumber,CONVERT(float,(SELECT TOP 1 InvoiceTaxrate  FROM AcOtherShouldPayment WHERE AcOtherShouldPayment.AcOtherShouldPaymentId = AcOtherShouldPaymentDetail.AcOtherShouldPaymentId))*AcMoney / 100 AS ShuiE,AcMoney+CONVERT(float,(SELECT TOP 1 InvoiceTaxrate  FROM AcOtherShouldPayment WHERE AcOtherShouldPayment.AcOtherShouldPaymentId = AcOtherShouldPaymentDetail.AcOtherShouldPaymentId))*AcMoney / 100 AS Total FROM AcOtherShouldPaymentDetail WHERE 1 = 1 "); //其它应付款
            //日期
            if (startdate.HasValue && enddate.HasValue)
            {
                sb_cg.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE InvoiceDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' )");
                //sb_ct.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCT WHERE InvoiceDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_ct.Append(" AND ct.InvoiceDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //sb_ww.Append(" AND ProduceOtherCompactId IN (SELECT ProduceOtherCompactId FROM ProduceOtherCompact WHERE ProduceOtherCompactDate BETWEEN '" + startdate.ToString("yyyy-MM-dd") + "' AND '" + enddate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
                //sb_sr.Append(" AND ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE ProduceInDepotDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_sr.Append(" AND pi.ProduceInDepotDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //sb_wr.Append(" AND ProduceOtherInDepotId IN (SELECT ProduceOtherInDepotId FROM ProduceOtherInDepot WHERE ProduceOtherInDepotDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_wr.Append(" AND po.ProduceOtherInDepotDate  BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                sb_qf.Append(" AND AcOtherShouldPaymentId IN (SELECT AcOtherShouldPaymentId FROM AcOtherShouldPayment WHERE AcOtherShouldPaymentDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                //sb_wt.Append(" AND ProduceOtherReturnMaterialId IN (SELECT ProduceOtherReturnMaterialId FROM ProduceOtherReturnMaterial WHERE ProduceOtherReturnMaterialDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_wt.Append(" AND pm.ProduceOtherReturnMaterialDate BETWEEN '" + startdate.Value.ToString("yyyy-MM-dd") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            //交货日期
            if (JHstartdate != global::Helper.DateTimeParse.NullDate.Date && JHenddate != global::Helper.DateTimeParse.EndDate.Date.AddDays(1).AddSeconds(-1))
            {
                //使用采购进货单-- 进货日期
                sb_cg.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE InvoiceHisDate BETWEEN '" + JHstartdate.ToString("yyyy-MM-dd") + "' AND '" + JHenddate.AddDays(1).ToString("yyyy-MM-dd") + "')");
                //使用采购订单---交货日期
                //sb_cg.Append(" AND InvoiceCOId IN (SELECT InvoiceCO.InvoiceId FROM InvoiceCO WHERE InvoiceCO.InvoiceYjrq BETWEEN '" + JHstartdate.ToString("yyyy-MM-dd") + "' AND '" + JHenddate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
            }

            //付款日期
            if (FKStartDate.HasValue && FKEndDate.HasValue)
            {
                sb_cg.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE InvoicePaymentDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_qf.Append(" AND AcOtherShouldPaymentId IN (SELECT AcOtherShouldPaymentId FROM AcOtherShouldPayment WHERE AdvancePayableDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                //sb_ct.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCT WHERE PayDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_ct.Append(" AND ct.PayDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //sb_sr.Append(" AND ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE PayDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_sr.Append(" AND pi.PayDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //sb_wr.Append(" AND ProduceOtherInDepotId IN (SELECT ProduceOtherInDepotId FROM ProduceOtherInDepot WHERE PayDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_wr.Append(" AND po.PayDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //sb_wt.Append(" AND ProduceOtherReturnMaterialId in (SELECT ProduceOtherReturnMaterialId FROM ProduceOtherReturnMaterial WHERE PayDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
                sb_wt.Append(" AND pm.PayDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            //供应商
            if (startSupplier != null && endSupplier != null)
            {
                sb_cg.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE SupplierId IN (SELECT Supplier.SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "'))");
                //sb_ct.Append(" AND InvoiceId IN (SELECT InvoiceCT.InvoiceId FROM InvoiceCT WHERE SupplierId IN (SELECT Supplier.SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "'))");
                sb_ct.Append(" AND ct.SupplierId IN (SELECT Supplier.SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "')");
                //sb_ww.Append(" AND ProduceOtherCompactId IN (SELECT ProduceOtherCompactId FROM ProduceOtherCompact WHERE SupplierId IN (SELECT SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "'))");
                //sb_sr.Append(" AND (SELECT WorkHouse.Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId = (SELECT ProduceInDepot.WorkHouseId FROM ProduceInDepot WHERE ProduceInDepotDetail.ProduceInDepotId = ProduceInDepot.ProduceInDepotId)) IN ('" + startSupplier.SupplierShortName.Trim() + "','" + endSupplier.SupplierShortName.Trim() + "')");
                //sb_sr.Append(" and EXISTS (select SupplierFullName from supplier where SupplierFullName like (SELECT '%'+WorkHouse.Workhousename+'%' FROM WorkHouse WHERE WorkHouse.WorkHouseId = (SELECT ProduceInDepot.WorkHouseId FROM ProduceInDepot WHERE ProduceInDepotDetail.ProduceInDepotId = ProduceInDepot.ProduceInDepotId)) AND SupplierFullName in ('" + startSupplier.SupplierFullName.Trim() + "','" + endSupplier.SupplierFullName.Trim() + "'))");
                sb_sr.Append("  and (EXISTS (select SupplierFullName from supplier where SupplierFullName like (SELECT '%'+WorkHouse.Workhousename+'%' FROM WorkHouse WHERE WorkHouse.WorkHouseId = pi.WorkHouseId) AND SupplierFullName in ('" + startSupplier.SupplierFullName.Trim() + "','" + endSupplier.SupplierFullName.Trim() + "')) or EXISTS (SELECT WorkHouse.Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId = pi.WorkHouseId and (Workhousename  like '%" + startSupplier.SupplierFullName.Trim() + "%' or  Workhousename like '%" + endSupplier.SupplierFullName.Trim() + "%')))");
                //sb_wr.Append(" AND ProduceOtherInDepotId IN (SELECT ProduceOtherInDepotId FROM ProduceOtherInDepot WHERE SupplierId IN (SELECT SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "'))");
                sb_wr.Append(" AND po.SupplierId IN (SELECT SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "')");
                sb_qf.Append(" AND AcOtherShouldPaymentId IN (SELECT AcOtherShouldPaymentId FROM AcOtherShouldPayment WHERE SupplierId IN (SELECT SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "'AND '" + endSupplier.Id + "'))");
                //sb_wt.Append(" AND ProduceOtherReturnMaterialId IN (SELECT ProduceOtherReturnMaterialId FROM ProduceOtherReturnMaterial WHERE SupplierId IN (SELECT SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "'))");
                sb_wt.Append(" AND pm.SupplierId IN (SELECT SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "')");
            }

            //商品
            if (productStart != null && productEnd != null)
            {
                sb_cg.Append(" AND ProductId IN (SELECT ProductId FROM Product WHERE Id BETWEEN '" + productStart.Id + "' AND '" + productEnd.Id + "')");
                sb_ct.Append(" AND ctd.ProductId IN (SELECT ProductId FROM Product WHERE Id BETWEEN '" + productStart.Id + "' AND '" + productEnd.Id + "')");
                //sb_ww.Append(" AND ProductId IN (SELECT ProductId FROM Product WHERE Id BETWEEN '" + productStart.Id + "' AND '" + productEnd.Id + "')");
                sb_sr.Append(" AND pid.ProductId IN (SELECT ProductId FROM Product WHERE Id BETWEEN '" + productStart.Id + "' AND '" + productEnd.Id + "')");
                sb_wr.Append(" AND ProductId IN (SELECT ProductId FROM Product WHERE Id BETWEEN '" + productStart.Id + "' AND '" + productEnd.Id + "')");
                sb_wt.Append(" AND pd.ProductId IN (SELECT ProductId FROM product WHERE id BETWEEN '" + productStart.Id + "' AND '" + productEnd.Id + "')");
            }

            //采购单号
            if (!string.IsNullOrEmpty(coidStart) && !string.IsNullOrEmpty(coidEnd))
            {
                sb_cg.Append(" AND InvoiceCOId BETWEEN '" + coidStart + "' AND '" + coidEnd + "'");
                sb_ct.Append(" AND ctd.InvoiceCOId BETWEEN '" + coidStart + "' AND '" + coidEnd + "'");
            }

            //客户订单编号
            if (!string.IsNullOrEmpty(CusXOid))
            {
                sb_cg.Append(" AND InvoiceCOId IN (SELECT InvoiceCO.InvoiceId FROM InvoiceCO WHERE InvoiceCustomXOId LIKE '%" + CusXOid + "%')");
                sb_ct.Append(" AND ctd.InvoiceCOId IN (SELECT InvoiceCO.InvoiceId FROM InvoiceCO WHERE InvoiceCustomXOId LIKE '%" + CusXOid + "%')");
                //sb_ww.Append(" AND CustomInvoiceXOId LIKE '%" + CusXOid + "%'");
            }

            //员工
            if (empstart != null && empend != null)
            {
                sb_cg.Append(" AND InvoiceId IN (SELECT InvoiceCG.InvoiceId FROM InvoiceCG WHERE Employee0Id IN (SELECT Employee.EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "'))");
                //sb_ct.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCT WHERE Employee0Id IN (SELECT Employee0Id FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "'))");
                sb_ct.Append("  AND ct.Employee0Id IN (SELECT Employee0Id FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "')");
                //sb_ww.Append(" AND ProduceOtherCompactId IN (SELECT ProduceOtherCompactId FROM ProduceOtherCompact WHERE Employee0Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "'))");
                //sb_sr.Append(" AND ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE Employee0Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "'))");
                sb_sr.Append(" AND pi.Employee0Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "')");
                //sb_wr.Append(" AND ProduceOtherInDepotId IN (SELECT ProduceOtherInDepotId FROM ProduceOtherInDepot WHERE Employee0Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "'))");
                sb_wr.Append(" AND po.Employee0Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "')");
                sb_qf.Append(" AND AcOtherShouldPaymentId IN (SELECT AcOtherShouldPaymentId FROM AcOtherShouldPayment WHERE Employee1Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "'))");
                //sb_wt.Append("  AND ProduceOtherReturnMaterialId IN (SELECT ProduceOtherReturnMaterialId FROM ProduceOtherReturnMaterial WHERE Employee0Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "'))");
                sb_wt.Append(" AND pm.Employee0Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empstart.IDNo + "' AND '" + empend.IDNo + "')");
            }

            string sql = sb_cg.ToString() + " UNION ALL " + sb_ct.ToString() + " UNION ALL " + sb_sr.ToString() + " UNION ALL " + sb_wr.ToString() + " UNION ALL " + sb_qf.ToString() + " UNION ALL " + sb_wt.ToString();
            sql = " SELECT * FROM ( " + sql + " ) a ORDER BY a.JHRQ ASC";

            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }

            return null;
        }

        public IList<Model.InvoiceCGDetail> SelectByConditionCO(DateTime startdate, DateTime enddate, DateTime JHstartdate, DateTime JHenddate, DateTime? FKStartDate, DateTime? FKEndDate, Book.Model.Supplier startSupplier, Book.Model.Supplier endSupplier, Book.Model.Product productStart, Book.Model.Product productEnd, string coidStart, string coidEnd, string CusXOid, Book.Model.Employee empstart, Book.Model.Employee empend)
        {
            StringBuilder sb = new StringBuilder();
            if (startdate != global::Helper.DateTimeParse.NullDate.Date && enddate != global::Helper.DateTimeParse.EndDate.Date.AddDays(1).AddDays(-1))
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE InvoiceDate BETWEEN '" + startdate.ToString("yyyy-MM-dd") + "' AND '" + enddate.AddDays(1).ToString("yyyy-MM-dd") + "')");
            }
            if (!string.IsNullOrEmpty(coidStart) && !string.IsNullOrEmpty(coidEnd))
            {
                sb.Append(" AND InvoiceCOId WHERE BETWEEN '" + coidStart + "' AND '" + coidEnd + "'");
            }
            if (JHstartdate != global::Helper.DateTimeParse.NullDate.Date && JHenddate != global::Helper.DateTimeParse.EndDate.Date.AddDays(1).AddSeconds(-1))
            {
                //使用进货日期
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE InvoiceHisDate BETWEEN '" + JHstartdate.ToString("yyyy-MM-dd") + "' AND '" + JHenddate.AddDays(1).ToString("yyyy-MM-dd") + "')");
                //使用采购订单--交货日期
                //sb.Append(" AND InvoiceCOId IN (SELECT InvoiceCO.InvoiceId FROM InvoiceCO WHERE InvoiceCO.InvoiceYjrq BETWEEN '" + JHstartdate.ToString("yyyy-MM-dd") + "' AND '" + JHenddate.ToString("yyyy-MM-dd") + "')");
            }
            if (FKStartDate.HasValue && FKEndDate.HasValue)
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE InvoicePaymentDate BETWEEN '" + FKStartDate.Value.ToString("yyyy-MM-dd") + "' AND '" + FKEndDate.Value.AddDays(1).ToString("yyyy-MM-dd") + "')");
            }
            if (!string.IsNullOrEmpty(CusXOid))
            {
                sb.Append(" AND InvoiceCOId IN (SELECT InvoiceCO.InvoiceId FROM InvoiceCO WHERE InvoiceCustomXOId LIKE '%" + CusXOid + "%')");
            }
            if (startSupplier != null && endSupplier != null)
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE SupplierId IN (SELECT Supplier.SupplierId FROM Supplier WHERE Id BETWEEN '" + startSupplier.Id + "' AND '" + endSupplier.Id + "'))");
            }
            if (productStart != null && productEnd != null)
            {
                sb.Append(" AND ProductId IN (SELECT Product.ProductId FROM Product WHERE Id BETWEEN '" + productStart.Id + "' AND '" + productEnd.Id + "')");
            }
            if (empstart != null && empend != null)
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceCG.InvoiceId FROM InvoiceCG WHERE Employee0Id IN (SELECT Employee.EmployeeId FROM Employee WHERE IDNo BETWEEN '' AND ''))");
            }

            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.SelectByConditionCO", sb.ToString());
        }

        public decimal? SelectLatelyProductPrice(string productid)
        {
            return sqlmapper.QueryForObject<decimal?>("InvoiceCGDetail.SelectLatelyProductPrice", productid);
        }

        public string SelectByInvoiceId(string invoiceid)
        {
            return sqlmapper.QueryForObject<string>("InvoiceCGDetail.SelectByInvoiceId", invoiceid);
        }

        public double CountInDepotQuantity(string id)
        {
            return sqlmapper.QueryForObject<double>("InvoiceCGDetail.CountInDepotQuantity", id);
        }

        public Model.InvoiceCGDetail SelectByCODetailId(string Id)
        {
            return sqlmapper.QueryForObject<Model.InvoiceCGDetail>("InvoiceCGDetail.SelectByCODetailId", Id);
        }

        //查詢所有廠商指定日期的應付賬款，金額為0不顯示
        public DataTable SelectAllSupplierShouldPay(DateTime startdate, DateTime enddate)
        {
            StringBuilder sb_cg = new StringBuilder("SELECT ISNULL(InvoiceCGDetailMoney,0) AS JinE,isnull(InvoiceCGDetailTax,0) AS ShuiE,ISNULL(InvoiceCGDetailMoney,0)+isnull(InvoiceCGDetailTax,0) as Total,cg.SupplierId FROM InvoiceCGDetail cgd left join InvoiceCG cg on cgd.InvoiceId=cg.InvoiceId WHERE InvoiceCGDetailMoney<>0");            //进货
            StringBuilder sb_ct = new StringBuilder("SELECT ISNULL((0-InvoiceCTDetailMoney1) ,0) AS JinE,ISNULL((0-InvoiceCTDetailMoney1) ,0)*0.05 AS ShuiE,ISNULL((0-InvoiceCTDetailMoney1) ,0)+ ISNULL((0-InvoiceCTDetailMoney1) ,0)*0.05 as Total ,ct.SupplierId FROM InvoiceCTDetail ctd left join InvoiceCT ct on ctd.InvoiceId=ct.InvoiceId WHERE InvoiceCTDetailMoney1<>0 ");            //退货
            StringBuilder sb_sr = new StringBuilder("SELECT isnull(ProduceMoney,0) AS JinE,isnull(ProduceMoney,0)*0.05 AS ShuiE,isnull(ProduceMoney,0)*0.05+ isnull(ProduceMoney,0) as Total ,s.SupplierId FROM ProduceInDepotDetail pid left join ProduceInDepot pi on pid.ProduceInDepotId=pi.ProduceInDepotId left join WorkHouse wh on pi.WorkHouseId=wh.WorkHouseId left join Supplier s on s.SupplierFullName like '%'+wh.Workhousename+'%' WHERE pid.ProduceMoney<>0 and s.SupplierId is not null");      //生产入库
            StringBuilder sb_wr = new StringBuilder("SELECT isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4)) AS JinE,isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4))*0.05 AS ShuiE,isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4))+ isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4))*0.05 as Total ,po.SupplierId FROM ProduceOtherInDepotDetail pod left join ProduceOtherInDepot po on pod.ProduceOtherInDepotId=po.ProduceOtherInDepotId  WHERE (isnull(ProduceQuantity,0)*cast(ISNULL(ProcessPrice,0) as decimal(18,4)))<>0");    //委外入库
            StringBuilder sb_wt = new StringBuilder("SELECT isnull((0-Amount),0) AS JinE,isnull((0-Amount),0)*0.05 AS ShuiE,isnull((0-Amount),0)*0.05+isnull((0-Amount),0) AS Total ,pm.SupplierId FROM ProduceOtherReturnDetail  pd left join ProduceOtherReturnMaterial pm on pd.ProduceOtherReturnMaterialId=pm.ProduceOtherReturnMaterialId WHERE Amount<>0");    //委外退库
            StringBuilder sb_qf = new StringBuilder("SELECT AcMoney AS JinE,CONVERT(float,ap.InvoiceTaxrate)*AcMoney / 100 AS ShuiE,AcMoney+CONVERT(float,ap.InvoiceTaxrate)*AcMoney / 100 AS Total ,ap.SupplierId FROM AcOtherShouldPaymentDetail apd left join AcOtherShouldPayment ap on apd.AcOtherShouldPaymentId=ap.AcOtherShouldPaymentId WHERE AcMoney<>0");     //其它应付款
            //日期

            sb_cg.Append(" and cg.InvoiceDate between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sb_ct.Append("and ct.InvoiceDate between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sb_sr.Append(" and pi.ProduceInDepotDate between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sb_wr.Append(" and po.ProduceOtherInDepotDate between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sb_wt.Append(" and pm.ProduceOtherReturnMaterialDate between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sb_qf.Append(" and ap.AcOtherShouldPaymentDate between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");


            string sql = sb_cg.ToString() + " UNION ALL " + sb_ct.ToString() + " UNION ALL " + sb_sr.ToString() + " UNION ALL " + sb_wr.ToString() + " UNION ALL " + sb_qf.ToString() + " UNION ALL " + sb_wt.ToString();
            sql = "select b.*,Supplier.SupplierShortName from ( SELECT a.SupplierId,SUM(a.JinE) as JinE,SUM(a.ShuiE) as ShuiE,SUM(a.Total) as Total FROM ( " + sql + " ) a group by a.SupplierId ) b left join Supplier on b.SupplierId=Supplier.SupplierId";

            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }

            return null;
        }
    }
}
