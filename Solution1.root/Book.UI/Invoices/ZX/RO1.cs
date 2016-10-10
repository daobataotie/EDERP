using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZX
{
    public partial class RO1 : DevExpress.XtraReports.UI.XtraReport
    {
        public RO1()
        {
            InitializeComponent();
        }

        public RO1(Model.InvoicePacking model)
            : this()
        {
            //this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            //this.lblReportName.Text = Properties.Resources.InvoicePacking;
            //this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            //this.lblInvoicePackingId.Text = model.InvoicePackingId;
            this.lblInvoicePackingDate.Text = model.InvoicePackingDate == null ? null : model.InvoicePackingDate.Value.ToString("yyyy-MM-dd");
            this.lblInvoiceOF.Text = model.XOCustomer == null ? null : model.XOCustomer.ToString();
            this.lblInvoiceTO_1.Text = model.To1;
            this.lblInvoiceTO_2.Text = model.To2;
            this.lblShippedBy.Text = model.ShippedBy == null ? null : model.ShippedBy.CompanyName;
            this.lblPerSS.Text = model.PerSS;
            this.lblS_O.Text = model.SorO;
            this.lblShippedON_About.Text = model.ShippedOnAbout;
            this.lblArrivel_ONAbout.Text = model.ArrivelOnAbout;
            this.lblInvoice_AddFrom.Text = model.AddressFrom;
            this.lblInvoice_AddTo.Text = model.AddressTo;
            //this.RichTextNote.Rtf = model.InvoicePackingNote;
            //this.lblEmployee.Text = model.Employee == null ? "0" : model.Employee.EmployeeName;
            //this.lblJWeight.Text = model.Jweight == null ? "0" : model.Jweight.ToString();
            //this.lblMWeight.Text = model.Mweight == null ? "0" : model.Mweight.ToString();
            //this.lblCaiji.Text = model.Caiji == null ? null : model.Caiji.ToString();

            this.DataSource = model.Details;
            TC_ShippingMarks.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_HandPackingId);
            this.TC_InvoicePackingDetailNO.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_InvoicePackingDetailId);
            this.TC_Description.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_Note);
            this.TC_Quantity.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_PackingNum);
            this.TC_NewWeight.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_AllJweight);
            this.TC_CrossWeight.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_AllMWeight);
            this.TC_UnitPrice.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_UnitPrice);
        }
    }
}
