using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Query
{
    public partial class ROInvoiceCGlistSub : DevExpress.XtraReports.UI.XtraReport
    {
        BL.SupplierManager supplierManager = new Book.BL.SupplierManager();
        BL.CompanyManager companyManager = new Book.BL.CompanyManager();
        public ROInvoiceCGlistSub()
        {
            InitializeComponent();
        }

        public ROInvoiceCGlistSub(IList<Model.ShouldPayAccountDetail> list)
            : this()
        {
            this.DataSource = list;
            foreach (var item in list)
            {
                //item.FPSupplier = supplierManager.Get(item.FPSupplierId);
                item.HeaderName=companyManager.SelectCompanyName(item.FPHeader);
            }
            this.TCFPDate.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPDate, "{0:yyyy-MM-dd}");
            this.TCFPId.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPId);
            this.TCFPSupplier.DataBindings.Add("Text", this.DataSource, "FPSupplier.SupplierFullName");
            //this.TCFPHeader.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPHeader);
            this.TCFPHeader.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_HeaderName);
            this.TCFPMoney.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPMoney, "{0:0.##}");
            this.TCFPTax.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPTax, "{0:0.##}");
            this.TCFPTotalMoney.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPTotalMoney, "{0:0.##}");
        }

        private void ROInvoiceCGlistSub_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
