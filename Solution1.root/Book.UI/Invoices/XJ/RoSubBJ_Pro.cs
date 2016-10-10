using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XJ
{
    public partial class RoSubBJ_Pro : DevExpress.XtraReports.UI.XtraReport
    {
        public Model.InvoiceXJ _InvoiceXJ { get; set; }

        double InvoiceXJDetailQuoteTotal = 0;

        public RoSubBJ_Pro()
        {
            InitializeComponent();
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(RoSubCB_Pro_BeforePrint);
            if (this._InvoiceXJ != null)
                this.TCInvoiceXJGuanXiaoPro.Text += this._InvoiceXJ.GuanXiaoPro.ToString();

            //Binding
            this.TCProductName.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_ProductName);
            this.TCInvoiceProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceProductUnit);
            this.TCInvoiceXJDetailQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailQuantity, "{0:0.####}");
            this.TCInvoiceXJDetailQuote.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailQuote, "{0:0.####}");

            //this.TCInvoiceXJDetailQuoteTotal.Summary.FormatString = "{0:0.####}";
            //this.TCInvoiceXJDetailQuoteTotal.Summary.Func = SummaryFunc.Sum;
            //this.TCInvoiceXJDetailQuoteTotal.Summary.IgnoreNullValues = true;
            //this.TCInvoiceXJDetailQuoteTotal.Summary.Running = SummaryRunning.Report;
            //this.TCInvoiceXJDetailQuoteTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailQuote, "{0:0}");
        }

        private void RoSubCB_Pro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this._InvoiceXJ.Details;
            this.TCInvoiceXJGuanXiaoPro.Text += this._InvoiceXJ.GuanXiaoPro.HasValue ? this._InvoiceXJ.GuanXiaoPro.ToString() : "";

            if (this._InvoiceXJ.Details != null)
                foreach (var item in this._InvoiceXJ.Details)
                {
                    if (item.ParentId == "000")
                    {
                        InvoiceXJDetailQuoteTotal += Convert.ToDouble(item.InvoiceXJDetailQuote);
                    }
                }

            this.TCInvoiceXJDetailQuoteTotal.Text = InvoiceXJDetailQuoteTotal.ToString("0.####");
        }
    }
}
