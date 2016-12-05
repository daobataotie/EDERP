using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {
        BL.PCPGOnlineCheckManager _pcpgocm = new Book.BL.PCPGOnlineCheckManager();
        BL.PCPGOnlineCheckDetailManager _pcpgcDetailm = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.ProduceOtherCompactManager _pocm = new Book.BL.ProduceOtherCompactManager();
        IList<Model.PCPGOnlineCheckDetail> _DDetails = new List<Model.PCPGOnlineCheckDetail>();
        int tag = 0;

        public Ro(Model.PCPGOnlineCheck pcpgoc)
        {
            InitializeComponent();
            if (pcpgoc == null)
                return;

            this._DDetails = this._pcpgcDetailm.SelectByFromInvoiceId(pcpgoc.PCPGOnlineCheckId);
            this.DataSource = this._DDetails;

            //CompanyInfo
            this.lblCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCPGOnlineCheck;
            this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblPCPGOnlineCheckId.Text = pcpgoc.PCPGOnlineCheckId;
            this.lblDate.Text = pcpgoc.PCPGOnlineCheckDate == null ? "" : pcpgoc.PCPGOnlineCheckDate.Value.ToString("yyyy-MM-dd");
            //this.lblCheckStandard.Text = pcpgoc.Customer == null ? "" : pcpgoc.Customer.CheckedStandard;
            this.lblEmployee.Text = pcpgoc.AuditEmp == null ? "" : pcpgoc.Employee.ToString();
            //this.lblProductName.Text = pcpgoc.Product == null ? "" : pcpgoc.Product.ToString();
            //this.lblInvoiceXOQuantity.Text = pcpgoc.InvoiceXOQuantity.HasValue ? pcpgoc.InvoiceXOQuantity.Value.ToString() : "";
            //if (pcpgoc.PCPGOnlineCheckType.Value > 0)
            //    this.lblSupplier.Text = this._pocm.Get(pcpgoc.FromPCId) == null ? "" : (this._pocm.Get(pcpgoc.FromPCId).Supplier == null ? "" : this._pocm.Get(pcpgoc.FromPCId).Supplier.ToString());
            //else
            //    this.lblSupplier.Text = "";
            //this.lblOrderCount.Text = pcpgoc.Details[0].CheckQuantity.HasValue ? pcpgoc.Details[0].CheckQuantity.Value.ToString() : "0";
            if (pcpgoc.PCPGOnlineCheckType == 1)
                this.lblFromInvoice.Text = "委外合同單";
            else if (pcpgoc.PCPGOnlineCheckType == -1)
                this.lblFromInvoice.Text = "生產加工單";
            else if (pcpgoc.PCPGOnlineCheckType == 0)
                this.lblFromInvoice.Text = "採購單";
            this.lblBusinessHours.Text = pcpgoc.BusinessHours == null ? null : pcpgoc.BusinessHours.BusinessHoursName;
            this.lblWorkHouse.Text = pcpgoc.WorkHouse == null ? null : pcpgoc.WorkHouse.ToString();

            //Detail
            this.TCProduct.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_ProductName);
            this.TCattrDianDuBOLiTest.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrDianDuBOLiTest);
            this.TCattrDianDuPDSLv.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrDianDuPDSLv);
            this.TCattrExterior.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrExterior);
            this.TCattrFangWuMoYingDu.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrFangWuMoYingDu);
            //this.TCattrGaoDiJiaoL.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGaoDiJiaoL);
            //this.TCattrGaoDiJiaoR.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGaoDiJiaoR);
            this.TCattrGuanXue.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGuanXue);
            this.TCattrHouDu.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrHouDu);
            this.TCattrMaoBian.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrMaoBian);
            this.TCattrQiangHuaMo.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrQiangHuaMo);
            this.TCattrTouShiLv.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrTouShiLv);
            this.TCattrUVChengFen.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrUVChengFen);
            this.TCattrZhePian.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrZhePian);
            //this.TCattrZhuangJiaoSJDL.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrZhuangJiaoSJDL);
            this.TCCheckQuantity.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_CheckQuantity);
            this.TCImpactCheck.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_ImpactCheck);
            this.TCPCPGOnlineCheckDetailDate.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_PCPGOnlineCheckDetailDate, "{0:yyyy-MM-dd HH:mm:ss}");
            this.TCFromInvoiceId.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_FromInvoiceId);
            this.TCCheckStandard.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_CheckedStandard);
            this.TCInvoiceCusId.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_InvoiceCusXOId);

            this.subReportGX.ReportSource = new subReportGX();
            this.subReportHD.ReportSource = new subReportHD();
        }

        public Ro(Model.PCPGOnlineCheck pcpgoc, string rowFilter)
        {
            InitializeComponent();
            if (pcpgoc == null)
                return;
            this.tag = 1;

            DataTable dt = this._pcpgcDetailm.SelectByHeaderId(pcpgoc.PCPGOnlineCheckId);
            DataView dv = dt.DefaultView;
            dv.RowFilter = rowFilter;
            this.DataSource = dv;

            //CompanyInfo
            this.lblCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCPGOnlineCheck;
            this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblPCPGOnlineCheckId.Text = pcpgoc.PCPGOnlineCheckId;
            this.lblDate.Text = pcpgoc.PCPGOnlineCheckDate == null ? "" : pcpgoc.PCPGOnlineCheckDate.Value.ToString("yyyy-MM-dd");
            //this.lblCheckStandard.Text = pcpgoc.Customer == null ? "" : pcpgoc.Customer.CheckedStandard;
            this.lblEmployee.Text = pcpgoc.AuditEmp == null ? "" : pcpgoc.Employee.ToString();
            //this.lblProductName.Text = pcpgoc.Product == null ? "" : pcpgoc.Product.ToString();
            //this.lblInvoiceXOQuantity.Text = pcpgoc.InvoiceXOQuantity.HasValue ? pcpgoc.InvoiceXOQuantity.Value.ToString() : "";
            //if (pcpgoc.PCPGOnlineCheckType.Value > 0)
            //    this.lblSupplier.Text = this._pocm.Get(pcpgoc.FromPCId) == null ? "" : (this._pocm.Get(pcpgoc.FromPCId).Supplier == null ? "" : this._pocm.Get(pcpgoc.FromPCId).Supplier.ToString());
            //else
            //    this.lblSupplier.Text = "";
            //this.lblOrderCount.Text = pcpgoc.Details[0].CheckQuantity.HasValue ? pcpgoc.Details[0].CheckQuantity.Value.ToString() : "0";
            if (pcpgoc.PCPGOnlineCheckType == 1)
                this.lblFromInvoice.Text = "委外合同單";
            else if (pcpgoc.PCPGOnlineCheckType == -1)
                this.lblFromInvoice.Text = "生產加工單";
            else if (pcpgoc.PCPGOnlineCheckType == 0)
                this.lblFromInvoice.Text = "採購單";
            this.lblBusinessHours.Text = pcpgoc.BusinessHours == null ? null : pcpgoc.BusinessHours.BusinessHoursName;
            this.lblWorkHouse.Text = pcpgoc.WorkHouse == null ? null : pcpgoc.WorkHouse.ToString();

            //Detail
            this.TCProduct.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_ProductName);
            this.TCattrDianDuBOLiTest.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrDianDuBOLiTest);
            this.TCattrDianDuPDSLv.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrDianDuPDSLv);
            this.TCattrExterior.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrExterior);
            this.TCattrFangWuMoYingDu.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrFangWuMoYingDu);
            //this.TCattrGaoDiJiaoL.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGaoDiJiaoL);
            //this.TCattrGaoDiJiaoR.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGaoDiJiaoR);
            this.TCattrGuanXue.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGuanXue);
            this.TCattrHouDu.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrHouDu);
            this.TCattrMaoBian.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrMaoBian);
            this.TCattrQiangHuaMo.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrQiangHuaMo);
            this.TCattrTouShiLv.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrTouShiLv);
            this.TCattrUVChengFen.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrUVChengFen);
            this.TCattrZhePian.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrZhePian);
            //this.TCattrZhuangJiaoSJDL.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrZhuangJiaoSJDL);
            this.TCCheckQuantity.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_CheckQuantity);
            this.TCImpactCheck.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_ImpactCheck);
            this.TCPCPGOnlineCheckDetailDate.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_PCPGOnlineCheckDetailDate, "{0:yyyy-MM-dd HH:mm:ss}");
            this.TCFromInvoiceId.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_FromInvoiceId);
            this.TCCheckStandard.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_CheckedStandard);
            this.TCInvoiceCusId.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_InvoiceCusXOId);

            this.subReportGX.ReportSource = new subReportGX();
            this.subReportHD.ReportSource = new subReportHD();
        }

        private void subReportGX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportGX subGX = this.subReportGX.ReportSource as subReportGX;
            if (this.tag == 1)
                subGX._PCPGOnlineCheckDetailId = (this.GetCurrentRow() as DataRowView) == null ? "" : (this.GetCurrentRow() as DataRowView)["PCPGOnlineCheckDetailId"].ToString();
            else
                subGX._PCPGOnlineCheckDetailId = (this.GetCurrentRow() as Model.PCPGOnlineCheckDetail) == null ? "" : (this.GetCurrentRow() as Model.PCPGOnlineCheckDetail).PCPGOnlineCheckDetailId;
        }

        private void subReportHD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportHD subHD = this.subReportHD.ReportSource as subReportHD;
            if (this.tag == 1)
                subHD._PCPGOnlineCheckDetailId = (this.GetCurrentRow() as DataRowView) == null ? "" : (this.GetCurrentRow() as DataRowView)["PCPGOnlineCheckDetailId"].ToString();
            else
                subHD._PCPGOnlineCheckDetailId = (this.GetCurrentRow() as Model.PCPGOnlineCheckDetail) == null ? "" : (this.GetCurrentRow() as Model.PCPGOnlineCheckDetail).PCPGOnlineCheckDetailId;
        }
    }
}
