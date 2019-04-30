using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCFinishCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        string _PCFinishCheckId;
        IList<Model.OpticsTest> listOpticsTest = new List<Model.OpticsTest>();

        public RO(Model.PCFinishCheck _pcfc)
        {
            InitializeComponent();
            if (_pcfc == null)
                return;
            //CompanyInfo
            this._PCFinishCheckId = _pcfc.PCFinishCheckID;
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCFinishCheck;
            this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Details Controls
            this.lblPCFinishCheckID.Text = _pcfc.PCFinishCheckID;
            this.lblPCFinishCheckDate.Text = _pcfc.PCFinishCheckDate.HasValue ? _pcfc.PCFinishCheckDate.Value.ToShortDateString() : "";
            this.lblProductName.Text = _pcfc.Product == null ? "" : _pcfc.Product.ToString();
            this.lblBuMen.Text = _pcfc.WorkHouse == null ? "" : _pcfc.WorkHouse.ToString();
            //this.lblInvoiceCusXOId.Text = _pcfc.InvoiceCusXOId;
            this.lblPCFinishCheckCount.Text = _pcfc.PCFinishCheckCount.ToString();
            this.lblPCFinishCheckInCoiunt.Text = _pcfc.PCFinishCheckInCoiunt.HasValue ? _pcfc.PCFinishCheckInCoiunt.Value.ToString() : "";
            this.lblPCFinishCheckDesc.Text = _pcfc.PCFinishCheckDesc;
            this.lblEmployee0.Text = _pcfc.Employee0 == null ? "" : _pcfc.Employee0.ToString();
            this.lblEmployee1.Text = _pcfc.Employee1 == null ? "" : _pcfc.Employee1.ToString();
            this.lblCustomerProductName.Text = _pcfc.Product == null ? "" : _pcfc.Product.CustomerProductName;
            this.lblPronoteHeardId.Text = _pcfc.PronoteHeaderID;
            this.lbl_proudctunit.Text = _pcfc.ProductUnit == null ? "" : _pcfc.ProductUnit.ToString();
            this.lbl_AnnualRing.Text = _pcfc.AnnualRing;
            this.checkEdit1.Checked = _pcfc.IsMuShiJianYan.HasValue ? _pcfc.IsMuShiJianYan.Value : false;
            this.lbl_Pihao.Text = _pcfc.Pihao;

            if (_pcfc.PronoteHeader != null)
            {
                Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(_pcfc.PronoteHeader.InvoiceXOId);
                if (xo != null)
                {
                    this.lbl_TestStandard.Text = xo.xocustomer.CheckedStandard;
                    this.lbl_CustomerName.Text = xo.Customer.CustomerFullName;
                    this.lbl_InvoiceXOCustomer.Text = xo.xocustomer.CustomerFullName;
                    this.lbl_JHDate.Text = xo.InvoiceYjrq.Value.ToString("yyyy-MM-dd");
                    this.lblInvoiceCusXOId.Text = xo.CustomerInvoiceXOId;
                }
            }

            //details
            this.lblDZDWQDW.Text = Trans(_pcfc.AttrDZDWQDW);
            this.lblJWYHWRL.Text = Trans(_pcfc.AttrJWYHWRL);
            this.lblGZBKYRL.Text = Trans(_pcfc.AttrGZBKYRL);
            this.lblZZWBXGJ.Text = Trans(_pcfc.AttrZZWBXGJ);
            this.lblJPBKGCS.Text = Trans(_pcfc.AttrJPBKGS);
            this.lblJPJJHZQ.Text = Trans(_pcfc.AttrJPJHZQ);
            this.lblJPJSX.Text = Trans(_pcfc.AttrJPSX);
            this.lblJJSFTSYH.Text = Trans(_pcfc.AttrJJSFTSYH);
            this.lblGX.Text = Trans(_pcfc.AttrGX);
            this.lblTSL.Text = Trans(_pcfc.AttrTSL);
            this.lblCJBZ.Text = Trans(_pcfc.AttrCJBZ);
            this.lblWXTB.Text = Trans(_pcfc.AttrWXTB);
            this.lblZMCM.Text = Trans(_pcfc.AttrZMCM);
            this.lblSLDKSFMF.Text = Trans(_pcfc.AttrSLDSFMF);
            this.lblNHDQSFZQ.Text = Trans(_pcfc.AttrNHDQSFZQ);
            this.lblNHTB.Text = Trans(_pcfc.AttrNHTB);
            this.lblJSSFZQ.Text = Trans(_pcfc.AttrJSSFZQ);
            this.lblJDZRFS.Text = Trans(_pcfc.AttrJDZRFS);
            this.lblPKZRFS.Text = Trans(_pcfc.AttrPKZRFS);
            this.lblSLDNHWXTMTBSFZQ.Text = Trans(_pcfc.AttrSLDNHWXTMSFZQ);
            this.TCAttrDGBLTest.Text = Trans(_pcfc.AttrDGBLTest);
            this.lblESSSFZH.Text = Trans(_pcfc.AttrESSSFZH);
            this.lblESSFYGZTZ.Text = Trans(_pcfc.AttrESSFYGZTZ);

            listOpticsTest = new BL.OpticsTestManager().FSelect(this._PCFinishCheckId);
            if (listOpticsTest != null && listOpticsTest.Count > 0)
                this.xrSubreportGX.ReportSource = new PCPGOnlineCheck.subReportGX("PCFinishCheck");
            else
                this.Detail.Visible = false;

        }

        private string Trans(int? a)
        {
            string str = string.Empty;
            switch (a.HasValue ? a.Value : -1)
            {
                case -1:
                    str = "";
                    break;
                case 0:
                    str = "¡Ì";
                    break;
                case 1:
                    str = "X";
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

        private void subReportGX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (listOpticsTest != null && listOpticsTest.Count > 0)
            {
                PCPGOnlineCheck.subReportGX subGX = this.xrSubreportGX.ReportSource as PCPGOnlineCheck.subReportGX;
                subGX._PCPGOnlineCheckDetailId = this._PCFinishCheckId;
            }
        }
    }
}
