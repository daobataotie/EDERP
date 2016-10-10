using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCBoxFootCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.PCBoxFootCheck model)
            : this()
        {
            if (model != null)
            {
                this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
                this.lblReportName.Text = Properties.Resources.PCBoxFootCheck;
                this.lblPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

                this.lblId.Text = model.PCBoxFootCheckId;
                this.lblCheckDate.Text = model.CheckDate == null ? null : model.CheckDate.Value.ToString("yyyy-MM-dd");
                this.lblProductName.Text = model.Product == null ? null : model.Product.ProductName;
                this.lblInvoiceXO.Text = model.InvoiceXO == null ? null : model.InvoiceXO.CustomerInvoiceXOId;
                this.lblPronoteHeader.Text = model.PronoteHeaderId;
                this.lblEmployee.Text = model.Employee == null ? null : model.Employee.EmployeeName;
                this.xrRichTextNote.Rtf = model.Note;
                this.lbl_quyangshuliang.Text = model.GetNum.HasValue ? model.GetNum.ToString() : string.Empty;
                this.lbl_shouceshuliang.Text = model.CheckNum.HasValue ? model.CheckNum.ToString() : string.Empty;
                this.lbl_hegeshuliang.Text = model.PassNum.HasValue ? model.PassNum.ToString() : string.Empty;
                this.lbl_buhegeshuliang.Text = model.NoPassNum.HasValue ? model.NoPassNum.ToString() : string.Empty;

                //this.DataSource = model;
                //this.TCFlap.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheck.PRO_Flap);
                //this.TCExterior.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheck.PRO_Exterior);
                this.TCFlap.Text = Trans(model.Flap);
                this.TCExterior.Text = Trans(model.Exterior);
                this.TCOfColor.Text = Trans(model.OfColor);
                this.TCHeightFootL.Text = Trans(model.HeightFootL);
                this.TCHeightFootR.Text = Trans(model.HeightFootR);
                this.TCFootElasticL.Text = Trans(model.HeightFootL);
                this.TCFootElasticR.Text = Trans(model.HeightFootR);
                this.TCImpactTest.Text = Trans(model.ImpactTest);
                this.TCAceticacidTest.Text = Trans(model.AceticacidTest);
            }

        }

        private string Trans(int? i)
        {
            string str = string.Empty;
            switch (i)
            {
                case 0:
                    str = "¡Ì";
                    break;
                case 1:
                    str = "¡Á";
                    break;
                case 2:
                    str = "¡÷";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
    }
}
