using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        BL.CustomerProductsManager manager = new Book.BL.CustomerProductsManager();
        public RO()
        {
            InitializeComponent();
        }
        public RO(Model.Customer model)
            : this()
        {

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.CustomerProducts;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.DataSource = manager.Select(model);
            this.TCProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.TCCustomerProductId.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_CustomerProductId);
            this.TCCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerShortName);
            this.TCProducename.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCVersion.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_Version);
            this.TCVersionDate.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_VersionDate, "{0:yyyy-MM-dd}");
            this.TCLong.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_BLong);
            this.TCWide.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_BWide);
            this.TCHeight.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_BHigh);
            this.TCJWeight.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_JWeight);
            this.TCMWeight.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_MWeight);
            this.TCCaiji.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_Caiji);
            this.TCspecification.DataBindings.Add("Text", this.DataSource, Model.CustomerProducts.PRO_PackingSpecification);
            this.xrRichTextProductdesc.DataBindings.Add("Rtf", this.DataSource, Model.CustomerProducts.PRO_CustomerProductDesc);
        }
    }
}
