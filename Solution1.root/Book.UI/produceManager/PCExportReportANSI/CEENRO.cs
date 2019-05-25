using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class CEENRO : DevExpress.XtraReports.UI.XtraReport
    {
        public CEENRO()
        {
            InitializeComponent();
            this.xrPictureBox1.Visible = false;
        }

        public CEENRO(Model.PCExportReportANSI _PCExportReportANSI, int tag)
            : this()
        {
            if (tag == 1)
            {
                this.xrLabel1.Text = "ALAN    SAFETY    COMPANY.";
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ALANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ALANSignature")[0].SettingCurrentValue;
                this.xrPictureBox1.Visible = true;
            }
            else if (tag == 2)
            {
                this.xrLabel1.Text = "PPE   SAFETY   INC.";
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("PPESignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("PPESignature")[0].SettingCurrentValue;
            }
            else
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ASWANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ASWANSignature")[0].SettingCurrentValue;
            this.LbCustomer.Text = _PCExportReportANSI.Customer == null ? null : _PCExportReportANSI.Customer.CustomerName;
            this.LbOrderId.Text = _PCExportReportANSI.InvoiceCusXOId == null ? null : _PCExportReportANSI.InvoiceCusXOId.ToString();
            this.LbProduct.Text = _PCExportReportANSI.Product.CustomerProductName == null ? null : _PCExportReportANSI.Product.CustomerProductName.ToString();
            this.LbOrderAmount.Text = (_PCExportReportANSI.Amount.HasValue ? _PCExportReportANSI.Amount.ToString() : "0") + "PCS";
            this.LbTestAmount.Text = (_PCExportReportANSI.AmountTest.HasValue ? _PCExportReportANSI.AmountTest.ToString() : "0") + "PCS";
            this.LbTesrPerson.Text = (_PCExportReportANSI.Employee == null ? null : _PCExportReportANSI.Employee.ToString()) + (_PCExportReportANSI.Employee2 == null ? null : " / " + _PCExportReportANSI.Employee2.ToString()) + (_PCExportReportANSI.Employee3 == null ? null : " / " + _PCExportReportANSI.Employee3.ToString()) + (_PCExportReportANSI.Employee4 == null ? null : " / " + _PCExportReportANSI.Employee4.ToString());
            //this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToShortDateString();
            this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToString("dd/MM/yyyy");
            this.lblTraceabilityMarking.Text = _PCExportReportANSI.TraceMarking;
            this.LbClearlens.Text = _PCExportReportANSI.Clearlens == null ? null : _PCExportReportANSI.Clearlens.ToString();
            this.LbClearlens.Multiline = true;
            this.LbTestTS.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.LbJudgeTS.Borders = DevExpress.XtraPrinting.BorderSide.None;

            //this.xrTableCell37.DataBindings("Text",);
            //this.xrTableCell37.Text = (_PCExportReportANSI.ShouCeShu6.HasValue ? _PCExportReportANSI.ShouCeShu6.ToString() : "0") + "OF";
            //this.xrTableCell39.Text = (_PCExportReportANSI.PanDing6.HasValue ? _PCExportReportANSI.PanDing6.ToString() : "0") + "PASS";
            //this.lbTraceability.Text = _PCExportReportANSI.TraceMarking == null ? null : _PCExportReportANSI.TraceMarking.ToString();
            this.xrLabel31.Text = "6mm dia. Steel ball at " + _PCExportReportANSI.Protectionone + " + 1.5/-0 m/s or " + _PCExportReportANSI.Protectiontwo + "(+5)FPS. " + this.xrLabel31.Text;

            this.LbTestCS.Text = (_PCExportReportANSI.ShouCeShu1.HasValue ? _PCExportReportANSI.ShouCeShu1.ToString() : "0");
            this.LbTestSp.Text = (_PCExportReportANSI.ShouCeShu2.HasValue ? _PCExportReportANSI.ShouCeShu2.ToString() : "0");
            this.LbTestAR.Text = (_PCExportReportANSI.ShouCeShu3.HasValue ? _PCExportReportANSI.ShouCeShu3.ToString() : "0");
            this.LbTestPR.Text = (_PCExportReportANSI.ShouCeShu4.HasValue ? _PCExportReportANSI.ShouCeShu4.ToString() : "0");
            this.LbTestDP.Text = (_PCExportReportANSI.ShouCeShu5.HasValue ? _PCExportReportANSI.ShouCeShu5.ToString() : "0");
            this.LbTestDP2.Text = (_PCExportReportANSI.ShouCeShu5.HasValue ? _PCExportReportANSI.ShouCeShu5.ToString() : "0");
            this.LbTestTS.Text = (_PCExportReportANSI.ShouCeShu6.HasValue ? _PCExportReportANSI.ShouCeShu6.ToString() : "0");
            this.LbTestSQ.Text = (_PCExportReportANSI.ShouCeShu7.HasValue ? _PCExportReportANSI.ShouCeShu7.ToString() : "0");
            this.LbTestIR.Text = (_PCExportReportANSI.ShouCeShu8.HasValue ? _PCExportReportANSI.ShouCeShu8.ToString() : "0");
            this.LbTestHS.Text = (_PCExportReportANSI.ShouCeShu9.HasValue ? _PCExportReportANSI.ShouCeShu9.ToString() : "0");
            this.LbTestMK.Text = (_PCExportReportANSI.ShouCeShu10.HasValue ? _PCExportReportANSI.ShouCeShu10.ToString() : "0");
            this.LbTestUT.Text = (_PCExportReportANSI.ShouCeShu12.HasValue ? _PCExportReportANSI.ShouCeShu12.ToString() : "0");
            this.LbTestPackaging.Text = (_PCExportReportANSI.ShouCeShu13.HasValue ? _PCExportReportANSI.ShouCeShu13.ToString() : "0");

            this.LbJudgeCS.Text = "PASS";
            this.LbJudgeSP.Text = "PASS";
            this.LbJudgeAR.Text = "PASS";
            this.LbJudgePR.Text = "PASS";
            this.LbJudgeDP.Text = "PASS";
            this.LbJudgeDP2.Text = "PASS";
            this.LbJudgeTS.Text = "PASS";
            this.LbJudgeSQ.Text = "PASS";
            this.LbJudgeIR.Text = "PASS";
            this.LbJudgeHS.Text = "PASS";
            this.LbJudgeMK.Text = "PASS";
            this.LbJudgeUT.Text = "PASS";
            this.LbJudgePackaging.Text = "PASS";
        }

    }
}
