using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.Accounting.Report
{
    public partial class CashCreditSlipRO : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtSummonDetailManager manager = new Book.BL.AtSummonDetailManager();
        DataTable dt = new DataTable();
        public CashCreditSlipRO()
        {
            InitializeComponent();
        }
        Query.ConditionA condition = new Book.UI.Query.ConditionA();
        public CashCreditSlipRO(Query.ConditionA condition)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.Date.ToString("yyyy-MM-dd");
            this.lblDateRange.Text = condition.StartDate.ToString("yyyy-MM-dd") + " - " + condition.EndDate.ToString("yyyy-MM-dd");
            decimal d = manager.SelectYEByDate(condition.StartDate);
            this.TCBeforeYE.Text = d.ToString("0.#");
            this.dt = manager.GetByDate(condition.StartDate, condition.EndDate);

            decimal a = d;
            foreach (DataRow item in dt.Rows)
            {
                if (item["YE"] == DBNull.Value)
                    item["YE"] = 0;
                item["YE"] = a = Convert.ToDecimal(item["YE"]) + a;
            }

            this.DataSource = dt;

            this.TCDate.DataBindings.Add("Text", this.DataSource, "SummonDate", "{0:yyyy-MM-dd}");
            this.TCSummonId.DataBindings.Add("Text", this.DataSource, "Id");
            this.TCSummonName.DataBindings.Add("Text", this.DataSource, "SubjectName");
            this.TCNote.DataBindings.Add("Text", this.DataSource, "Summary");
            this.TCInCome.DataBindings.Add("Text", this.DataSource, "Income", "{0:0}");
            this.TCPay.DataBindings.Add("Text", this.DataSource, "Pay", "{0:0}");
            this.TCYE.DataBindings.Add("Text", this.DataSource, "YE", "{0:0}");

            if (dt.Rows.Count > 1)
                this.lblYE.Text = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["YE"]).ToString("0.#");

            //加总：
            this.lblTotalIncome.Summary.FormatString = "{0:0}";
            this.lblTotalIncome.Summary.Func = SummaryFunc.Sum;
            this.lblTotalIncome.Summary.IgnoreNullValues = true;
            this.lblTotalIncome.Summary.Running = SummaryRunning.Report;
            this.lblTotalIncome.DataBindings.Add("Text", this.DataSource, "Income");

            this.lblTotalPay.Summary.FormatString = "{0:0}";
            this.lblTotalPay.Summary.Func = SummaryFunc.Sum;
            this.lblTotalPay.Summary.IgnoreNullValues = true;
            this.lblTotalPay.Summary.Running = SummaryRunning.Report;
            this.lblTotalPay.DataBindings.Add("Text", this.DataSource, "Pay");

            //this.lblYE.Summary.FormatString = "{0:0}";
            //this.lblYE.Summary.Func = SummaryFunc.Sum;
            //this.lblYE.Summary.IgnoreNullValues = true;
            //this.lblYE.Summary.Running = SummaryRunning.Report;
            //this.lblYE.DataBindings.Add("Text", this.DataSource, "YE");

        }
    }
}
