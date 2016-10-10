using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Query
{
    public partial class ROFP : DevExpress.XtraReports.UI.XtraReport
    {
        BL.ShouldPayAccountDetailManager manager = new Book.BL.ShouldPayAccountDetailManager();
        BL.CompanyManager companyManager = new Book.BL.CompanyManager();
        public ROFP()
        {
            InitializeComponent();
        }

        public ROFP(Model.ShouldPayAccountDetail shouldPayAccountDetail)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = "應付賬款發票";
            //this.lblDateRange.Text = fp.StartDate.ToString("yyyy-MM-dd") + " - " + fp.EndDate.ToString("yyyy-MM-dd");
            //this.lblSupplier.Text = fp.Supplier.SupplierFullName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            IList<Model.ShouldPayAccountDetail> list = new List<Model.ShouldPayAccountDetail>();
            list.Add(shouldPayAccountDetail);
            this.DataSource = list;
            foreach (var item in list)
            {
                //item.FPSupplier = supplierManager.Get(item.FPSupplierId);
                item.HeaderName = companyManager.SelectCompanyName(item.FPHeader);
            }
            this.TCFPDate.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPDate, "{0:yyyy-MM-dd}");
            this.TCFPId.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPId);
            this.TCFPSupplier.DataBindings.Add("Text", this.DataSource, "FPSupplier.SupplierFullName");
            this.TCFPHeader.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_HeaderName);
            this.TCFPMoney.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPMoney, "{0:0.##}");
            this.TCFPTax.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPTax, "{0:0.##}");
            this.TCFPTotalMoney.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPTotalMoney, "{0:0.##}");
        }

        public ROFP(IList<Model.ShouldPayAccountDetail> list)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = "應付賬款發票";
            //this.lblDateRange.Text = fp.StartDate.ToString("yyyy-MM-dd") + " - " + fp.EndDate.ToString("yyyy-MM-dd");
            //this.lblSupplier.Text = fp.Supplier.SupplierFullName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.DataSource = list;
            foreach (var item in list)
            {
                //item.FPSupplier = supplierManager.Get(item.FPSupplierId);
                item.HeaderName = companyManager.SelectCompanyName(item.FPHeader);
            }
            this.TCFPDate.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPDate, "{0:yyyy-MM-dd}");
            this.TCFPId.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPId);
            this.TCFPSupplier.DataBindings.Add("Text", this.DataSource, "FPSupplier.SupplierFullName");
            this.TCFPHeader.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_HeaderName);
            this.TCFPMoney.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPMoney, "{0:0.##}");
            this.TCFPTax.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPTax, "{0:0.##}");
            this.TCFPTotalMoney.DataBindings.Add("Text", this.DataSource, Model.ShouldPayAccountDetail.PRO_FPTotalMoney, "{0:0.##}");
        }
    }
}
