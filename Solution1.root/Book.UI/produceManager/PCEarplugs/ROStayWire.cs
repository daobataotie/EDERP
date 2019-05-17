using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCEarplugs
{
    public partial class ROStayWire : DevExpress.XtraReports.UI.XtraReport
    {
        public ROStayWire(Model.PCEarplugsStayWireCheck pCEarplugsStayWireCheck)
        {
            InitializeComponent();

            this.DataSource = pCEarplugsStayWireCheck.Details;

            this.TCParameter.Text = pCEarplugsStayWireCheck.TestCondition;

            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lbl_Note.Text = pCEarplugsStayWireCheck.Note;
            this.lbl_Employee.Text = pCEarplugsStayWireCheck.Employee == null ? "" : pCEarplugsStayWireCheck.Employee.ToString();

            this.TCDate.DataBindings.Add("Text", this.DataSource, "pCEarplugsStayWireCheck." + Model.PCEarplugsStayWireCheck.PRO_PCEarplugsStayWireCheckDate, "{0:yyyy-MM-dd}");
            this.TCFromId.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsStayWireCheckDetail.PRO_FromId);
            this.TCCusXOId.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.TCInvoiceQuantity.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsStayWireCheckDetail.PRO_InvoiceXOQuantity);
            this.TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCProductUnit.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsStayWireCheckDetail.PRO_ProductUnit);
            this.TCCheckedStandard.DataBindings.Add("Text", this.DataSource, "InvoiceXO.xocustomer." + Model.Customer.PRO_CheckedStandard);
            this.TCTestQuantity.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsStayWireCheckDetail.PRO_TestQuantity);
            this.TCWaiguan.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsStayWireCheckDetail.PRO_Waiguan);
            this.TCDuise.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsStayWireCheckDetail.PRO_Duise);
            this.TCFama.DataBindings.Add("Text", this.DataSource, Model.PCEarplugsStayWireCheckDetail.PRO_Fama);
        }

    }
}
