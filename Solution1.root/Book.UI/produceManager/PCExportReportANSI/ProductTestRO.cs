using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class ProductTestRO : DevExpress.XtraReports.UI.XtraReport
    {
        public ProductTestRO()
        {
            InitializeComponent();
        }

        public ProductTestRO(Model.PCDataInput pcDataInput)
            : this()
        {
            this.TCCheckStandard.Text = pcDataInput.CheckStandard;
            this.TCCustomerShortName.Text = pcDataInput.CustomerShortName;
            this.TCProduct.Text = pcDataInput.Product == null ? "" : pcDataInput.Product.ToString();
            this.TCInvoiceCusId.Text = pcDataInput.InvoiceCusId;
            //this.TCTotalQuantity.Text = pcDataInput.OrderQuantity.HasValue ? pcDataInput.OrderQuantity.ToString() : "";
            this.TCTotalQuantity.Text = pcDataInput.PronoteHeader.DetailsSum.HasValue ? pcDataInput.PronoteHeader.DetailsSum.ToString() : "";
            this.TCTestQuantity.Text = pcDataInput.TestQuantity.HasValue ? pcDataInput.TestQuantity.ToString() : "";
            this.TCDate.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
        }
    }
}
