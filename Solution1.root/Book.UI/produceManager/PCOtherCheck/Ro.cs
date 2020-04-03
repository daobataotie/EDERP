using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCOtherCheck
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {
        public Ro(Model.PCOtherCheck _PCOC)
        {
            InitializeComponent();

            if (_PCOC == null)
                return;
            this.DataSource = _PCOC.Detail;
            //CompanyInfo
            this.lblCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCOtherCheck;
            this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblPCOtherCheckId.Text = _PCOC.PCOtherCheckId;
            this.lblPCOtherCheckDate.Text = _PCOC.PCOtherCheckDate.Value.ToShortDateString();
            this.lblSupplier.Text = _PCOC.Supplier == null ? "" : _PCOC.Supplier.ToString();
            this.lblEmployee1.Text = _PCOC.Employee1 == null ? "" : _PCOC.Employee1.ToString();
            this.lblEmployee0.Text = _PCOC.Employee0 == null ? "" : _PCOC.Employee0.ToString();
            this.lblPCOtherCheckDesc.Text = _PCOC.PCOtherCheckDesc;

            if (_PCOC.Detail.Any(d => d.PerspectiveRate.ToLower().Contains("jis")))
            {
                //CreateTagLable("JIS");

                this.lbl_JIS.Text = "JIS";
            }
            else if (_PCOC.Detail.Any(d => d.PerspectiveRate.ToLower().Contains("as")))
            {
                //CreateTagLable("AS");

                this.lbl_JIS.Text = "AS";
            }


            //Detail
            #region ×¢ÊÍ
            //foreach (Model.PCOtherCheckDetail d in _PCOC.Detail)
            //{
            //    foreach (var a in d.GetType().GetProperties())
            //    {
            //        if (a.Name != "")
            //        {
            //            switch (a.GetValue().ToString())
            //            {
            //                case "0":
            //                    a.SetValue(d, "¡ð", null); ;
            //                    break;
            //                case "1":
            //                    a.SetValue(d, "¡÷", null);
            //                    break;
            //                case "2":
            //                    a.SetValue(d, "X", null);
            //                    break;
            //            }
            //        }
            //    }
            //}
            #endregion
            this.xrTCPCOtherCheckDetailId.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_FromInvoiceID);
            this.xrTCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTCProceduresId.DataBindings.Add("Text", this.DataSource, "Procedures." + Model.Procedures.PRO_Procedurename);
            this.RTproductDesc.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            this.xrTCPCOtherCheckDetailQuantity.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_PCOtherCheckDetailQuantity);
            this.xrTCProductUnit.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_ProductUnit);
            this.xrTCPerspectiveRate.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_PerspectiveRate);
            this.xrTCDeliveryDate.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_DeliveryDate, "{0:yyyy-MM-dd}");
            //this.xrTCOutQuantity.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_OutQuantity);
            this.xrTCInQuantity.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_InQuantity);
            this.xrTCDeterminant.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_DeterminantDis);
            this.xrTCPCOtherCheckDetailDesc.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_PCOtherCheckDetailDesc1);

            //¿Í‘ôÓ††Î¾ŽÌ–
            this.xrInvoiceCusXoId.DataBindings.Add("Text", this.DataSource, Model.PCOtherCheckDetail.PRO_InvoiceCusXOId);
        }

        private void CreateTagLable(string tag)
        {
            XRLabel lbl_JIS = new XRLabel();
            this.ReportHeader.Controls.Add(lbl_JIS);
            lbl_JIS.BorderWidth = 0;
            lbl_JIS.CanGrow = false;
            lbl_JIS.Name = "lbl_JIS";
            lbl_JIS.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            lbl_JIS.SizeF = new SizeF(200, 85);
            lbl_JIS.Text = tag;
            lbl_JIS.Font = new Font("Times New Roman", 32);
            lbl_JIS.ForeColor = Color.Red;
            lbl_JIS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            lbl_JIS.Visible = true;
            lbl_JIS.LocationF = new PointF(this.PageWidth - lbl_JIS.SizeF.Width - 10 - Margins.Left - Margins.Right, 0);
        }
    }
}
