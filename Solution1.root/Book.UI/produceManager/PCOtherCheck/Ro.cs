using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

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

            ////Control
            this.lblPCOtherCheckId.Text = _PCOC.PCOtherCheckId;
            this.lblPCOtherCheckDate.Text = _PCOC.PCOtherCheckDate.Value.ToShortDateString();
            this.lblSupplier.Text = _PCOC.Supplier == null ? "" : _PCOC.Supplier.ToString();
            this.lblEmployee1.Text = _PCOC.Employee1 == null ? "" : _PCOC.Employee1.ToString();
            this.lblEmployee0.Text = _PCOC.Employee0 == null ? "" : _PCOC.Employee0.ToString();
            this.lblPCOtherCheckDesc.Text = _PCOC.PCOtherCheckDesc;

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
    }
}
