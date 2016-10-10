using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Book.UI.Query
{
    public partial class ROJD_PronoteHeader : DevExpress.XtraReports.UI.XtraReport
    {

        private BL.ProduceInDepotDetailManager _produceindepotdetailmanager = new Book.BL.ProduceInDepotDetailManager();

        public ROJD_PronoteHeader()
        {
            InitializeComponent();
        }

        public ROJD_PronoteHeader(ROJD_PronoteHeaderContition condition)
            : this()
        {
            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = Properties.Resources.ROJDPronoteHeader;
            this.lbl_ReportDate.Text += DateTime.Now.ToShortDateString();
            DataTable dtSource = this._produceindepotdetailmanager.ProducePronotePlanReport(condition.InvoiceDate_Start, condition.InvoiceDate_End, condition.PronoteHeaderId, condition.XSCustomer == null ? "" : condition.XSCustomer.CustomerId, condition.CusInvoiceXOId, condition.EmployeeYW == null ? "" : condition.EmployeeYW.EmployeeId, condition.PronoteHeaderType, condition.ProduceWorkHouse == null ? "" : condition.ProduceWorkHouse.WorkHouseId, condition.ProduceProduct == null ? "" : condition.ProduceProduct.ProductId, condition.IsJieAn);

            dtSource.DefaultView.Sort = "Date DESC";
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add("JGDN", typeof(System.String));
            dt.Columns.Add("KHDH", typeof(System.String));
            dt.Columns.Add("HPMC", typeof(System.String));
            dt.Columns.Add("DDSL", typeof(System.String));
            dt.Columns.Add("SCSL", typeof(System.String));
            dt.Columns.Add("KHJQ", typeof(System.DateTime));
            dt.Columns.Add("SCJQ", typeof(System.DateTime));
            dt.Columns.Add("SCHJSCSL", typeof(System.String));
            dt.Columns.Add("SCHJHGSL", typeof(System.String));
            dt.Columns.Add("PGJQ", typeof(System.DateTime));
            dt.Columns.Add("PGHJSCSL", typeof(System.String));
            dt.Columns.Add("PGHJHGSL", typeof(System.String));
            dt.Columns.Add("JQSB", typeof(System.String));
            dt.Columns.Add("YL", typeof(System.String));
            dt.Columns.Add("SPMS", typeof(System.String));
            dt.Columns.Add("Date", typeof(System.DateTime));

            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                if (i > 0 && dtSource.Rows[i]["JGDN"].ToString() == dtSource.Rows[i - 1]["JGDN"].ToString())
                    continue;
                dr = dt.NewRow();
                dr["JGDN"] = dtSource.Rows[i]["JGDN"];
                dr["KHDH"] = dtSource.Rows[i]["KHDH"];
                dr["HPMC"] = dtSource.Rows[i]["HPMC"];
                dr["DDSL"] = dtSource.Rows[i]["DDSL"];
                dr["SCSL"] = dtSource.Rows[i]["SCSL"];
                dr["KHJQ"] = dtSource.Rows[i]["KHJQ"];
                dr["SCJQ"] = dtSource.Rows[i]["SCJQ"];
                dr["SCHJSCSL"] = dtSource.Rows[i]["SCHJSCSL"];
                dr["SCHJHGSL"] = dtSource.Rows[i]["SCHJHGSL"];
                dr["PGJQ"] = dtSource.Rows[i]["PGJQ"];
                dr["PGHJSCSL"] = dtSource.Rows[i]["PGHJSCSL"];
                dr["PGHJHGSL"] = dtSource.Rows[i]["PGHJHGSL"];
                dr["JQSB"] = dtSource.Rows[i]["JQSB"];
                dr["YL"] = dtSource.Rows[i]["YL"];
                dr["SPMS"] = dtSource.Rows[i]["SPMS"];
                dr["Date"] = dtSource.Rows[i]["Date"];

                dt.Rows.Add(dr);
            }

            this.DataSource = dt;
            this.TC_JGDN.DataBindings.Add("Text", this.DataSource, "JGDN");
            this.TC_KHDH.DataBindings.Add("Text", this.DataSource, "KHDH");
            this.TC_HPMC.DataBindings.Add("Text", this.DataSource, "HPMC");
            this.TC_DDSL.DataBindings.Add("Text", this.DataSource, "DDSL");
            this.TC_SCSL.DataBindings.Add("Text", this.DataSource, "SCSL");
            this.TC_KHJQ.DataBindings.Add("Text", this.DataSource, "KHJQ", "{0:yyyy-MM-dd}");
            this.TC_SCJQ.DataBindings.Add("Text", this.DataSource, "SCJQ", "{0:yyyy-MM-dd}");
            this.TC_SCHJSCSL.DataBindings.Add("Text", this.DataSource, "SCHJSCSL");
            this.TC_SCHJHGSL.DataBindings.Add("Text", this.DataSource, "SCHJHGSL");
            this.TC_PGJQ.DataBindings.Add("Text", this.DataSource, "PGJQ", "{0:yyyy-MM-dd}");
            this.TC_PGHJSCSL.DataBindings.Add("Text", this.DataSource, "PGHJSCSL");
            this.TC_PGHJHGSL.DataBindings.Add("Text", this.DataSource, "PGHJHGSL");
            this.TC_JQSB.DataBindings.Add("Text", this.DataSource, "JQSB");
            this.TC_YL.DataBindings.Add("Text", this.DataSource, "YL");
            this.RT_SPMS.DataBindings.Add("Rtf", this.DataSource, "SPMS");

        }
    }
}
