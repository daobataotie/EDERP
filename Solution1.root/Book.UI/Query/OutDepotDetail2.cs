using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class OutDepotDetail2 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.DepotOutDetailManager manager = new Book.BL.DepotOutDetailManager();
        public OutDepotDetail2()
        {
            InitializeComponent();
        }
        public OutDepotDetail2(List<Model.DepotOutDetail> list, DateTime StartDate, DateTime EndDate)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.OutDepotDetail;
            this.lblDateRange.Text = "日期區間：" + StartDate.ToString("yyyy-MM-dd") + "-" + EndDate.ToString("yyyy-MM-dd");
            this.lblPrintDate.Text = "列印日期：" + DateTime.Now.ToString("yyyy-MM-dd");

            this.DataSource = list;

            this.xrTableCellDate.DataBindings.Add("Text", this.DataSource, "DepotOutDate", "{0:yyyy-MM-dd}");
            this.xrTableCellOutDepotId.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_DepotOutId);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_ProductName);
            this.xrTableCellPihao.DataBindings.Add("Text", this.DataSource, "Pihao");
            this.xrTableCellHuoWei.DataBindings.Add("Text", this.DataSource, "Id");
            this.xrTableCellOutNum.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_DepotOutDetailQuantity);
            this.xrTableCellInvoiceXOCusid.DataBindings.Add("Text", this.DataSource, "invoiceCusId");

            this.lblTotalNum.Summary.FormatString = "{0:0}";
            this.lblTotalNum.Summary.Func = SummaryFunc.Sum;
            this.lblTotalNum.Summary.IgnoreNullValues = true;
            this.lblTotalNum.Summary.Running = SummaryRunning.Report;
            this.lblTotalNum.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_DepotOutDetailQuantity);
        }
    }
}
