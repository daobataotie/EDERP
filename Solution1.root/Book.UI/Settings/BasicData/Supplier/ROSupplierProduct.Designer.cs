namespace Book.UI.Settings.BasicData.Supplier
{
    partial class ROSupplierProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ROSupplierProduct));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.TCProductType = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCProductIDNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCProduct = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCCustomerProductName = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCProductDesc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblCompanyName = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lblSupplier = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportName = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCProductVersion = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreport1,
            this.xrTable2,
            this.xrTable1});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // xrSubreport1
            // 
            resources.ApplyResources(this.xrSubreport1, "xrSubreport1");
            this.xrSubreport1.Name = "xrSubreport1";
            this.xrSubreport1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreport1_BeforePrint);
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTable2, "xrTable2");
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.TCProductType,
            this.TCProductIDNo,
            this.TCProduct,
            this.TCCustomerProductName,
            this.TCProductVersion,
            this.TCProductDesc});
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1;
            // 
            // TCProductType
            // 
            resources.ApplyResources(this.TCProductType, "TCProductType");
            this.TCProductType.Name = "TCProductType";
            this.TCProductType.Weight = 0.57865785299392525;
            // 
            // TCProductIDNo
            // 
            resources.ApplyResources(this.TCProductIDNo, "TCProductIDNo");
            this.TCProductIDNo.Name = "TCProductIDNo";
            this.TCProductIDNo.Weight = 1.3936999296067933;
            // 
            // TCProduct
            // 
            resources.ApplyResources(this.TCProduct, "TCProduct");
            this.TCProduct.Name = "TCProduct";
            this.TCProduct.Weight = 2.3851684244915994;
            // 
            // TCCustomerProductName
            // 
            resources.ApplyResources(this.TCCustomerProductName, "TCCustomerProductName");
            this.TCCustomerProductName.Name = "TCCustomerProductName";
            this.TCCustomerProductName.Weight = 1.2632465854458692;
            // 
            // TCProductDesc
            // 
            this.TCProductDesc.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1});
            resources.ApplyResources(this.TCProductDesc, "TCProductDesc");
            this.TCProductDesc.Name = "TCProductDesc";
            this.TCProductDesc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 254F);
            this.TCProductDesc.StylePriority.UsePadding = false;
            this.TCProductDesc.Weight = 1.1350690326273976;
            // 
            // xrRichText1
            // 
            this.xrRichText1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrRichText1, "xrRichText1");
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.StylePriority.UseBorders = false;
            this.xrRichText1.StylePriority.UseFont = false;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell6,
            this.xrTableCell5});
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1;
            // 
            // xrTableCell1
            // 
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Weight = 0.57865809328982409;
            // 
            // xrTableCell2
            // 
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Weight = 1.3937001699026919;
            // 
            // xrTableCell3
            // 
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Weight = 2.385168905083396;
            // 
            // xrTableCell4
            // 
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Weight = 1.2632456242622743;
            // 
            // xrTableCell5
            // 
            resources.ApplyResources(this.xrTableCell5, "xrTableCell5");
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Weight = 1.1350685520355996;
            // 
            // TopMargin
            // 
            resources.ApplyResources(this.TopMargin, "TopMargin");
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // BottomMargin
            // 
            resources.ApplyResources(this.BottomMargin, "BottomMargin");
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCompanyName});
            resources.ApplyResources(this.ReportHeader, "ReportHeader");
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblCompanyName
            // 
            resources.ApplyResources(this.lblCompanyName, "lblCompanyName");
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCompanyName.StylePriority.UseFont = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblSupplier,
            this.lblReportDate,
            this.lblReportName});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // lblSupplier
            // 
            resources.ApplyResources(this.lblSupplier, "lblSupplier");
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 5, 0, 0, 254F);
            this.lblSupplier.StylePriority.UseFont = false;
            this.lblSupplier.StylePriority.UsePadding = false;
            this.lblSupplier.StylePriority.UseTextAlignment = false;
            // 
            // lblReportDate
            // 
            resources.ApplyResources(this.lblReportDate, "lblReportDate");
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            // 
            // lblReportName
            // 
            resources.ApplyResources(this.lblReportName, "lblReportName");
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblReportName.StylePriority.UseFont = false;
            // 
            // ReportFooter
            // 
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Expanded = false;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            resources.ApplyResources(this.PageFooter, "PageFooter");
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            resources.ApplyResources(this.xrPageInfo1, "xrPageInfo1");
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            // 
            // xrTableCell6
            // 
            resources.ApplyResources(this.xrTableCell6, "xrTableCell6");
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Weight = 0.5100688914008582;
            // 
            // TCProductVersion
            // 
            resources.ApplyResources(this.TCProductVersion, "TCProductVersion");
            this.TCProductVersion.Name = "TCProductVersion";
            this.TCProductVersion.Weight = 0.5100688914008582;
            // 
            // ROSupplierProduct
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.PageFooter});
            resources.ApplyResources(this, "$this");
            this.Margins = new System.Drawing.Printing.Margins(150, 150, 80, 80);
            this.PageHeight = 2794;
            this.PageWidth = 2159;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel lblCompanyName;
        private DevExpress.XtraReports.UI.XRLabel lblReportName;
        private DevExpress.XtraReports.UI.XRLabel lblSupplier;
        private DevExpress.XtraReports.UI.XRLabel lblReportDate;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport1;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell TCProductType;
        private DevExpress.XtraReports.UI.XRTableCell TCProductIDNo;
        private DevExpress.XtraReports.UI.XRTableCell TCProduct;
        private DevExpress.XtraReports.UI.XRTableCell TCCustomerProductName;
        private DevExpress.XtraReports.UI.XRTableCell TCProductDesc;
        private DevExpress.XtraReports.UI.XRRichText xrRichText1;
        private DevExpress.XtraReports.UI.XRTableCell TCProductVersion;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
    }
}
