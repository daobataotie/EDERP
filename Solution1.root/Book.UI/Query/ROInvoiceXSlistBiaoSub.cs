using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.Query
{
    public partial class ROInvoiceXSlistBiaoSub : DevExpress.XtraReports.UI.XtraReport
    {
        public ROInvoiceXSlistBiaoSub()
        {
            InitializeComponent();
        }
        public ROInvoiceXSlistBiaoSub(DataTable dt)
            : this()
        {
            this.DataSource = dt;

            this.tcCHDH.DataBindings.Add("Text", this.DataSource, "CHDH");
            this.tcCHRQ.DataBindings.Add("Text", this.DataSource, "CHRQ", "{0:yyyy-MM-dd}");
            this.tcProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.tcKHDDBH.DataBindings.Add("Text", this.DataSource, "KHDDBH");
            //this.tcDDSL.DataBindings.Add("Text", this.DataSource, "");
            this.tcBCCHSL.DataBindings.Add("Text", this.DataSource, "BCCHSL", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSSLXiao.Value));
            this.tcDW.DataBindings.Add("Text", this.DataSource, "DanWei");
            this.tcDJ.DataBindings.Add("Text", this.DataSource, "DanJia", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.tcZheRang.DataBindings.Add("Text", this.DataSource, "ZheRang", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.tcJinE.DataBindings.Add("Text", this.DataSource, "JinE");
            this.tcShuiE.DataBindings.Add("Text", this.DataSource, "ShuiE", "{0:0}");
            this.tcYingShou.DataBindings.Add("Text", this.DataSource, "YingShou");

            this.TCZHeJi.Summary.FormatString = "{0:0}";
            this.TCZHeJi.Summary.Func = SummaryFunc.Sum;
            this.TCZHeJi.Summary.IgnoreNullValues = true;
            this.TCZHeJi.Summary.Running = SummaryRunning.Report;
            this.TCZHeJi.DataBindings.Add("Text", this.DataSource, "JinE");

            this.TCZShuiE.Summary.FormatString = "{0:0}";
            this.TCZShuiE.Summary.Func = SummaryFunc.Sum;
            this.TCZShuiE.Summary.IgnoreNullValues = true;
            this.TCZShuiE.Summary.Running = SummaryRunning.Report;
            this.TCZShuiE.DataBindings.Add("Text", this.DataSource, "ShuiE");

            this.TCZZongJi.Summary.FormatString = "{0:0}";
            this.TCZZongJi.Summary.Func = SummaryFunc.Sum;
            this.TCZZongJi.Summary.IgnoreNullValues = true;
            this.TCZZongJi.Summary.Running = SummaryRunning.Report;
            this.TCZZongJi.DataBindings.Add("Text", this.DataSource, "YingShou");
        }
    }
}
