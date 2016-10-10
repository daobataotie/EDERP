using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class ROCustomerProductPrice : DevExpress.XtraReports.UI.XtraReport
    {
        public ROCustomerProductPrice()
        {
            InitializeComponent();
        }

        public ROCustomerProductPrice(string CustomerName, List<Model.CustomerProductPrice> list)
            : this()
        {
            this.lblCompany.Text = BL.Settings.CompanyChineseName;
            this.lblCustomer.Text = CustomerName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.CustomerProductPriceRage))
                {
                    if (item.CustomerProductPriceRage.Contains("/"))
                        item.Price = item.CustomerProductPriceRage.Substring(item.CustomerProductPriceRage.LastIndexOf('/') + 1);
                }
            }

            this.DataSource = list;

            this.TCProductId.DataBindings.Add("Text", this.DataSource, "ProductIDNo");
            this.TCProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TCCustomerProduct.DataBindings.Add("Text", this.DataSource, "CustomerProductId");
            this.TCProductVersion.DataBindings.Add("Text", this.DataSource, "ProductVersion");
            this.TCPrice.DataBindings.Add("Text", this.DataSource, "Price");

        }
    }
}
