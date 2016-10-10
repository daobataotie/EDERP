namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    partial class FPRO1
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.TCProduct = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCQuantity = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblUnit = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCPrice = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCMoney = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 52.91667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.Dpi = 254F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(206.375F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1180.042F, 52.91667F);
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.TCProduct,
            this.TCQuantity,
            this.lblUnit,
            this.TCPrice,
            this.TCMoney});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableRow1.Weight = 1;
            // 
            // TCProduct
            // 
            this.TCProduct.Dpi = 254F;
            this.TCProduct.Name = "TCProduct";
            this.TCProduct.Text = "品名规格";
            this.TCProduct.Weight = 1.0226942364302321;
            // 
            // TCQuantity
            // 
            this.TCQuantity.Dpi = 254F;
            this.TCQuantity.Name = "TCQuantity";
            this.TCQuantity.StylePriority.UseTextAlignment = false;
            this.TCQuantity.Text = "数量";
            this.TCQuantity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.TCQuantity.Weight = 0.28638622658378238;
            // 
            // lblUnit
            // 
            this.lblUnit.Dpi = 254F;
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Text = "单位";
            this.lblUnit.Weight = 0.23545807939064603;
            // 
            // TCPrice
            // 
            this.TCPrice.Dpi = 254F;
            this.TCPrice.Name = "TCPrice";
            this.TCPrice.StylePriority.UseTextAlignment = false;
            this.TCPrice.Text = "单价";
            this.TCPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.TCPrice.Weight = 0.64060058185234614;
            // 
            // TCMoney
            // 
            this.TCMoney.Dpi = 254F;
            this.TCMoney.Name = "TCMoney";
            this.TCMoney.StylePriority.UseTextAlignment = false;
            this.TCMoney.Text = "金额";
            this.TCMoney.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.TCMoney.Weight = 0.65408676186033932;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 79F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 79F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // FPRO1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(79, 79, 79, 79);
            this.PageHeight = 2794;
            this.PageWidth = 2159;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell TCProduct;
        private DevExpress.XtraReports.UI.XRTableCell TCQuantity;
        private DevExpress.XtraReports.UI.XRTableCell TCPrice;
        private DevExpress.XtraReports.UI.XRTableCell TCMoney;
        private DevExpress.XtraReports.UI.XRTableCell lblUnit;
    }
}
