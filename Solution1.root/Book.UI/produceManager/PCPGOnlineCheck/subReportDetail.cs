using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class subReportDetail : DevExpress.XtraReports.UI.XtraReport
    {
        string sign = string.Empty;
        BL.PCPGOnlineCheckDetailManager _pcpgcDetail = new Book.BL.PCPGOnlineCheckDetailManager();

        public subReportDetail()
        {
            InitializeComponent();
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(subReportDetail_BeforePrint);

            //Detail
            this.TCProduct.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_ProductName);
            this.TCattrDianDuBOLiTest.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrDianDuBOLiTest);
            this.TCattrDianDuPDSLv.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrDianDuPDSLv);
            this.TCattrExterior.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrExterior);
            this.TCattrFangWuMoYingDu.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrFangWuMoYingDu);
            this.TCattrGaoDiJiaoL.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGaoDiJiaoL);
            this.TCattrGaoDiJiaoR.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGaoDiJiaoR);
            this.TCattrGuanXue.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGuanXue);
            this.TCattrHouDu.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrHouDu);
            this.TCattrMaoBian.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrMaoBian);
            this.TCattrQiangHuaMo.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrQiangHuaMo);
            this.TCattrTouShiLv.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrTouShiLv);
            this.TCattrUVChengFen.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrUVChengFen);
            this.TCattrZhePian.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrZhePian);
            this.TCattrZhuangJiaoSJDL.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrZhuangJiaoSJDL);
            this.TCCheckQuantity.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_CheckQuantity);
            this.TCImpactCheck.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_ImpactCheck);
            this.TCPCPGOnlineCheckDetailDate.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_PCPGOnlineCheckDetailDate, "{0:yyyy-MM-dd HH:mm:ss}");
            this.TCFromInvoiceId.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_FromInvoiceId);
            this.TCCheckStandard.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_CheckedStandard);
            this.TCInvoiceCusId.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_InvoiceCusXOId);
            this.lblSecondTestTime.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_SecondTestTime, "{0:yyyy-MM-dd}");

            this.subReportGX.ReportSource = new subReportGX();
            this.subReportHD.ReportSource = new subReportHD();
        }

        public string PCPGOnlineCheckId { set; get; }

        private void subReportGX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportGX subGX = this.subReportGX.ReportSource as subReportGX;
            subGX._PCPGOnlineCheckDetailId = (this.GetCurrentRow() as Model.PCPGOnlineCheckDetail).PCPGOnlineCheckDetailId;
        }

        private void subReportHD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportHD subHD = this.subReportHD.ReportSource as subReportHD;
            subHD._PCPGOnlineCheckDetailId = (this.GetCurrentRow() as Model.PCPGOnlineCheckDetail).PCPGOnlineCheckDetailId;
        }

        private void subReportDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this._pcpgcDetail.Select(PCPGOnlineCheckId);
        }
    }
}