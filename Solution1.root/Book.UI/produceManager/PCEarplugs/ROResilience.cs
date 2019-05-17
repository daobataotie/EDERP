using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCEarplugs
{
    public partial class ROResilience : DevExpress.XtraReports.UI.XtraReport
    {
        public ROResilience(Model.PCEarplugsResilienceCheck pCEarplugsResilienceCheck)
        {
            InitializeComponent();

            this.DataSource = pCEarplugsResilienceCheck.Details;

            this.TCTiekuaiyaCondition.Text = pCEarplugsResilienceCheck.TiekuaiyaCondition;
            this.TCShoucuorouCondition.Text = pCEarplugsResilienceCheck.ShoucuorouCondition;

            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lbl_Note.Text = pCEarplugsResilienceCheck.Note;
            this.lbl_Employee.Text = pCEarplugsResilienceCheck.Employee == null ? "" : pCEarplugsResilienceCheck.Employee.ToString();

            this.TCDate.DataBindings.Add("Text", this.DataSource, "PCEarplugsResilienceCheck." + Model.PCEarplugsResilienceCheck.PRO_PCEarplugsResilienceCheckDate, "{0:yyyy-MM-dd}");
            this.TCFromId.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_FromId);
            this.TCCusXOId.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.TCInvoiceQuantity.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_InvoiceXOQuantity);
            this.TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCProductUnit.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_ProductUnit);
            this.TCCheckedStandard.DataBindings.Add("Text", this.DataSource, "InvoiceXO.xocustomer." + Model.Customer.PRO_CheckedStandard);
            this.TCTestQuantity.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_TestQuantity);
            this.TCWaiguan.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_Waiguan);
            this.TCDuise.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_Duise);
            this.TCChicun.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_Chicun);
            this.TCTiekuaiya.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_Tiekuaiya, "{0:0.#}");
            this.TCShoucuorou.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_Shoucuorou, "{0:0.#}");
            this.TCJudge.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsResilienceCheckDetail.PRO_Judge);
        }

    }
}
