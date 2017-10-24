using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.Settings.BasicData.Products
{
    public partial class ROList1FormReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ROList1FormReport(DataTable dt)
        {
            InitializeComponent();

            this.DataSource = dt;

            this.lblPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.TCID.DataBindings.Add("Text",this.DataSource,"Id");
            this.TCName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TCCustomerProductName.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            this.TCCategory.DataBindings.Add("Text", this.DataSource, "ProductCategoryName");
            this.TCQuantity.DataBindings.Add("Text", this.DataSource, "StocksQuantity");
            this.TCCost.DataBindings.Add("Text", this.DataSource, "ReferenceCost");
            this.TCTotalCost.DataBindings.Add("Text", this.DataSource, "TotalCost");

            this.lblTotalCost.Summary.FormatString = "{0:0}";
            this.lblTotalCost.Summary.Func = SummaryFunc.Sum;
            this.lblTotalCost.Summary.IgnoreNullValues = true;
            this.lblTotalCost.Summary.Running = SummaryRunning.Report;
            this.lblTotalCost.DataBindings.Add("Text", this.DataSource, "TotalCost");
        }

    }
}
