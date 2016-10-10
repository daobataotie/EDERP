using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

namespace Book.UI.Query
{
    public partial class ROInvoiceCGlistBiao : BaseReport
    {
        private BL.InvoiceCGDetailManager invoicecgmanager = new Book.BL.InvoiceCGDetailManager();

        public ROInvoiceCGlistBiao()
        {
            InitializeComponent();
        }

        public ROInvoiceCGlistBiao(ConditionCO condition)
            : this()
        {
            this.xrLabelReportName.Text = "應付賬款明細表";
            this.lblSupplierName.Text = condition.SupplierStart == null ? "" : condition.SupplierStart.SupplierFullName;
            this.lblInvoiceDateRange.Text += (condition.StartInvoiceDate == null ? null : condition.StartInvoiceDate.Value.ToString("yyyy-MM-dd")) + "  -  " + (condition.EndInvoiceDate == null ? null : condition.EndInvoiceDate.Value.ToString("yyyy-MM-dd"));
            this.lblPayDate.Text += (condition.StartFKDate == null ? null : condition.StartFKDate.Value.ToString("yyyy-MM-dd")) + "  -  " + (condition.EndFKDate == null ? null : condition.EndFKDate.Value.ToString("yyyy-MM-dd"));
            this.lblTel.Text += condition.SupplierStart == null ? "" : condition.SupplierStart.SupplierPhone1;
            this.lblAddress.Text += condition.SupplierStart == null ? "" : condition.SupplierStart.CompanyAddress;
            this.lblFax.Text += condition.SupplierStart == null ? "" : condition.SupplierStart.SupplierFax;
            if (condition.SupplierStart != null)
            {
                this.lblPayMethod.Text += condition.SupplierStart.PayMethod == null ? "" : condition.SupplierStart.PayMethod.PayMethodName;
                this.lblNoId.Text += condition.SupplierStart.NoId;
            }
            //Bind
            this.DataSource = this.invoicecgmanager.SelectByConditionCOBiao(condition.StartInvoiceDate, condition.EndInvoiceDate, condition.StartJHDate, condition.EndJHDate, condition.StartFKDate, condition.EndFKDate, condition.SupplierStart, condition.SupplierEnd, condition.ProductStart, condition.ProductEnd, condition.COStartId, condition.COEndId, condition.CusXOId, condition.EmpStart, condition.EmpEnd);

            if (this.DataSource == null || (this.DataSource as DataTable).Rows.Count == 0)
                throw new Helper.InvalidValueException("查無記錄!");

            string sF;
            decimal jine = 0, shuie = 0, zonge = 0;
            foreach (DataRow dr in (this.DataSource as DataTable).Rows)
            {

                //if (dr[0].ToString().Contains("PID") || dr[0].ToString().Contains("POD"))
                //{
                //    try
                //    {
                //        //string DJ = dr["DanJia"].ToString();
                //        //double Quantity = string.IsNullOrEmpty(dr["JHSL"].ToString()) ? 0 : Convert.ToDouble(dr["JHSL"].ToString());
                //        //dr["DanJia"] = BL.SupplierProductManager.CountPrice(DJ, Quantity);
                //        //dr["JinE"] = Convert.ToDouble(dr["DanJia"]) * Convert.ToDouble(dr["JHSL"]);
                //        //dr["ShuiE"] = Convert.ToDouble(dr["JinE"]) * 0.05;
                //        //dr["Total"] = Convert.ToDouble(dr["JinE"]) + Convert.ToDouble(dr["ShuiE"]);
                //    }
                //    catch (Exception ex)
                //    {
                //        sF = ex.Message;
                //    }
                //}
                jine += Convert.ToDecimal(dr["JinE"]);
                shuie += Convert.ToDecimal(dr["ShuiE"]);
            }

            zonge = jine + shuie;

            if (this.DataSource == null || (this.DataSource as DataTable).Rows.Count == 0)
                throw new global::Helper.InvalidValueException("無記錄");

            this.tcJHDH.DataBindings.Add("Text", this.DataSource, "JHDN");
            this.tcJHRQ.DataBindings.Add("Text", this.DataSource, "JHRQ", "{0:yyyy-MM-dd}");
            this.tcProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.tcJHSL.DataBindings.Add("Text", this.DataSource, "JHSL");
            this.tcDJ.DataBindings.Add("Text", this.DataSource, "DanJia", "{0:0.####}");
            this.tcJinE.DataBindings.Add("Text", this.DataSource, "JinE", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.tcCGorWWDanHao.DataBindings.Add("Text", this.DataSource, "CGorWWDanHao");
            this.tcDW.DataBindings.Add("Text", this.DataSource, "ProductUnit");

            this.TCZJinE.Text = this.invoicecgmanager.GetSiSheWuRu(jine, 0).ToString();
            this.TCTotalShuiE.Text = this.invoicecgmanager.GetSiSheWuRu(shuie, 0).ToString();
            this.TCTotalMoney.Text = this.invoicecgmanager.GetSiSheWuRu(zonge, 0).ToString();

            //this.TCZJinE.Summary.FormatString = "{0:0}";
            //this.TCZJinE.Summary.Func = SummaryFunc.Sum;
            //this.TCZJinE.Summary.IgnoreNullValues = true;
            //this.TCZJinE.Summary.Running = SummaryRunning.Report;
            //this.TCZJinE.DataBindings.Add("Text", this.DataSource, "JinE");

            //this.TCTotalShuiE.Summary.FormatString = "{0:0}";
            //this.TCTotalShuiE.Summary.Func = SummaryFunc.Sum;
            //this.TCTotalShuiE.Summary.IgnoreNullValues = true;
            //this.TCTotalShuiE.Summary.Running = SummaryRunning.Report;
            //this.TCTotalShuiE.DataBindings.Add("Text", this.DataSource, "ShuiE");

            //this.TCTotalMoney.Summary.FormatString = "{0:0}";
            //this.TCTotalMoney.Summary.Func = SummaryFunc.Sum;
            //this.TCTotalMoney.Summary.IgnoreNullValues = true;
            //this.TCTotalMoney.Summary.Running = SummaryRunning.Report;
            //this.TCTotalMoney.DataBindings.Add("Text", this.DataSource, "Total");
        }

        //public ROInvoiceCGlistBiao(Model.Supplier supplier, string InvoiceDate, string FKDate, string Jine, string ShuiE, string ZheRang, string FKZheRang, string Money, DataTable dt, string AtSummonId, string YFId)
        public ROInvoiceCGlistBiao(Model.ShouldPayAccount shouldPayAccount, DataTable dt, string AtSummonId, IList<Model.AtBillsIncome> AtBillsIncomeList)
            : this()
        {
            this.xrLabelReportName.Text = "應付帳款明細表";
            this.lblInvoiceDateRange.Text += shouldPayAccount.InvoiceDate;
            this.lblPayDate.Text += shouldPayAccount.PayDate;
            this.lblAtSummonId.Text += AtSummonId;
            this.lblEmployee.Text += shouldPayAccount.Employee == null ? null : shouldPayAccount.Employee.EmployeeName;
            if (shouldPayAccount.Supplier != null)
            {
                this.lblSupplierName.Text = shouldPayAccount.Supplier.SupplierFullName;
                this.lblTel.Text += shouldPayAccount.Supplier.SupplierPhone1;
                this.lblAddress.Text += shouldPayAccount.Supplier.CompanyAddress;
                this.lblFax.Text += shouldPayAccount.Supplier.SupplierFax;
                this.lblPayMethod.Text += shouldPayAccount.Supplier.PayMethod == null ? "" : shouldPayAccount.Supplier.PayMethod.PayMethodName;
                this.lblNoId.Text += shouldPayAccount.Supplier.NoId;
            }
            //支票编号
            foreach (var item in AtBillsIncomeList)
            {
                this.lblZhiPiaoId.Text += item.Id + ",";
            }
            this.lblZhiPiaoId.Text = this.lblZhiPiaoId.Text.Substring(0, this.lblZhiPiaoId.Text.LastIndexOf(","));
            this.TCZJinE.Text = shouldPayAccount.JinE == null ? "0" : shouldPayAccount.JinE.ToString();
            this.TCTotalShuiE.Text = shouldPayAccount.ShuiE == null ? "0" : shouldPayAccount.ShuiE.ToString();
            this.TCTotalMoney.Text = shouldPayAccount.Total == null ? "0" : shouldPayAccount.Total.ToString();
            this.TCZheRang.Text = shouldPayAccount.ZheRang == null ? "0" : shouldPayAccount.ZheRang.ToString();
            this.TCFKZheRang.Text = shouldPayAccount.PayZheRang == null ? "0" : shouldPayAccount.PayZheRang.ToString();

            this.DataSource = dt;
            this.tcJHDH.DataBindings.Add("Text", this.DataSource, "JHDN");
            this.tcJHRQ.DataBindings.Add("Text", this.DataSource, "JHRQ", "{0:yyyy-MM-dd}");
            this.tcProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.tcJHSL.DataBindings.Add("Text", this.DataSource, "JHSL");
            this.tcDJ.DataBindings.Add("Text", this.DataSource, "DanJia", "{0:0.####}");
            this.tcJinE.DataBindings.Add("Text", this.DataSource, "JinE", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.tcCGorWWDanHao.DataBindings.Add("Text", this.DataSource, "CGorWWDanHao");
            this.tcDW.DataBindings.Add("Text", this.DataSource, "ProductUnit");

            if (shouldPayAccount.Detail != null && shouldPayAccount.Detail.Count > 0)
                this.xrSubreport1.ReportSource = new ROInvoiceCGlistSub(shouldPayAccount.Detail);
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
