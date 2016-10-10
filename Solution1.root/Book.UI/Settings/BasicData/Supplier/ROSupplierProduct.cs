using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace Book.UI.Settings.BasicData.Supplier
{
    public partial class ROSupplierProduct : DevExpress.XtraReports.UI.XtraReport
    {
        public ROSupplierProduct()
        {
            InitializeComponent();
        }

        public ROSupplierProduct(Model.Supplier sup, List<Model.SupplierProduct> list)
            : this()
        {
            //Controls
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lblSupplier.Text += " " + sup.SupplierFullName;

            this.DataSource = list;

            this.TCProductType.DataBindings.Add("Text", this.DataSource, "ProductType");
            this.TCProductIDNo.DataBindings.Add("Text", this.DataSource, "ProductIDNo");
            this.TCProduct.DataBindings.Add("Text", this.DataSource, "Product.ProductName");
            this.TCCustomerProductName.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            this.TCProductVersion.DataBindings.Add("Text", this.DataSource, "ProductVersion");
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDesc");

            xrSubreport1.ReportSource = new RO1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO1 r = this.xrSubreport1.ReportSource as RO1;
            r.priceR = (this.GetCurrentRow() as Model.SupplierProduct).SupplierProductPriceRange;
        }

    }
}
