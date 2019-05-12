using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCEarplugs
{
    public partial class RODecibel : DevExpress.XtraReports.UI.XtraReport
    {
        public RODecibel(Model.PCEarplugsDecibelCheck pCEarplugsDecibelCheck)
        {
            InitializeComponent();

            this.DataSource = pCEarplugsDecibelCheck.Details;

            var columnCaption = new BL.SettingManager().SelectByName("EarplugsDecibel");
            if (columnCaption != null && columnCaption.Count > 0)
                this.lbl_Parameter.Text = columnCaption[0].SettingCurrentValue;
            else
                this.lbl_Parameter.Text = "";

            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lbl_Note.Text = pCEarplugsDecibelCheck.Note;
            this.lbl_Employee.Text = pCEarplugsDecibelCheck.Employee == null ? "" : pCEarplugsDecibelCheck.Employee.ToString();

            this.TCDate.DataBindings.Add("Text", this.DataSource, "PCEarplugsDecibelCheck." + Model.PCEarplugsDecibelCheck.PRO_PCEarplugsDecibelCheckDate, "{0:yyyy-MM-dd}");
            this.TCFromId.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsDecibelCheckDetail.PRO_FromId);
            this.TCCusXOId.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.TCInvoiceQuantity.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsDecibelCheckDetail.PRO_InvoiceXOQuantity);
            this.TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCProductUnit.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsDecibelCheckDetail.PRO_ProductUnit);
            this.TCCheckedStandard.DataBindings.Add("Text", this.DataSource, "InvoiceXO.xocustomer." + Model.Customer.PRO_CheckedStandard);
            this.TCTestQuantity.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsDecibelCheckDetail.PRO_TestQuantity);
            this.TCYinpin.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsDecibelCheckDetail.PRO_Yinpin);
            this.TCJudge.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsDecibelCheckDetail.PRO_Judge);
        }

    }
}
