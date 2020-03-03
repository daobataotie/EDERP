using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherCompact
{

    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherCompactManager produceOtherCompactManager = new Book.BL.ProduceOtherCompactManager();
        private BL.ProduceOtherCompactDetailManager produceOtherCompactdetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();

        private Model.ProduceOtherCompact produceOtherCompact;

        public RO(string produceOtherCompactId)
        {
            InitializeComponent();
            this.produceOtherCompact = this.produceOtherCompactManager.Get(produceOtherCompactId);

            if (this.produceOtherCompact == null)
                return;

            this.produceOtherCompact.Details = this.produceOtherCompactdetailManager.Select(this.produceOtherCompact);


            //foreach (Model.ProduceOtherCompactDetail detail in this.produceOtherCompact.Details)
            //{
            //    Model.MRSdetails mrsdetail = new BL.MRSdetailsManager().Get(detail.MRSdetailsId);
            //    foreach (Model.InvoiceXODetail detail in invoiceXO.Details)
            //    {
            //        if (detail.ProductId == mrsdetail.MadeProductId)
            //            this.xrLabelInvoiceSum.Text = detail.InvoiceXODetailQuantity.Value.ToString("F0");
            //    }
            //}


            this.DataSource = this.produceOtherCompact.Details;
            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceOtherCompactDetail;
            this.xrLabelDate.Text += DateTime.Now.ToShortDateString();

            //外发合同
            this.xrLabelProduceOtherCompactId.Text = this.produceOtherCompact.ProduceOtherCompactId;
            this.xrLabelProduceOtherCompactDate.Text = this.produceOtherCompact.ProduceOtherCompactDate.Value.ToString("yyyy-MM-dd");
            if (this.produceOtherCompact.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.produceOtherCompact.Employee0.EmployeeName;
            }
            if (this.produceOtherCompact.Supplier != null)
            {
                this.xrLabelSupplierId.Text = this.produceOtherCompact.Supplier.SupplierFullName;
            }
            this.xrLabel9.Text = this.produceOtherCompact.AuditEmp == null ? "" : this.produceOtherCompact.AuditEmp.EmployeeName;
            this.xrLabelProduceOtherCompactDesc.Text = this.produceOtherCompact.ProduceOtherCompactDesc;
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.produceOtherCompact.JiaoHuoDate, global::Helper.DateTimeParse.NullDate) || this.produceOtherCompact.JiaoHuoDate==null)
            //{
            //    this.xrLabelJhDate.Text = string.Empty;
            //}
            //else
            //{
            //    this.xrLabelJhDate.Text = this.produceOtherCompact.JiaoHuoDate.Value.ToString("yyyy-MM-dd");
            //}

            //if (!string.IsNullOrEmpty(this.produceOtherCompact.MRSHeaderId))
            //{
            //    Model.MRSHeader mRSHeader = this.mRSHeaderManager.Get(this.produceOtherCompact.MRSHeaderId);
            //    if (mRSHeader != null)
            //    {
            //        Model.MPSheader mPSheader = this.mPSheaderManager.Get(mRSHeader.MPSheaderId);
            //        if (mPSheader != null)
            //        {
            Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(produceOtherCompact.InvoiceXOId);
            if (invoiceXO != null)
            {
                this.xrLabelCustomerXoId.Text = invoiceXO.CustomerInvoiceXOId;
                this.xrLabelCustomer.Text = invoiceXO.xocustomer == null ? null : invoiceXO.xocustomer.CustomerShortName;
                this.xrLabelCheck.Text = invoiceXO.xocustomer == null ? null : invoiceXO.xocustomer.CheckedStandard;
                this.lbl_PiHao.Text = invoiceXO.CustomerLotNumber;
                this.lblFP.Text = invoiceXO.xocustomer == null ? null : invoiceXO.xocustomer.CustomerFP;

                if (invoiceXO.xocustomer != null && !string.IsNullOrEmpty(invoiceXO.xocustomer.CheckedStandard))
                {
                    if (invoiceXO.xocustomer.CheckedStandard.ToLower().Contains("jis") && invoiceXO.xocustomer.CustomerFullName.ToUpper().Contains("MIDORI"))
                    {
                        CreateTagLable("JIS");
                    }
                    else if (invoiceXO.xocustomer.CheckedStandard.ToLower().Contains("as"))
                    {
                        CreateTagLable("AS");
                    }
                }
            }

            this.lbl_MRPId.Text = this.produceOtherCompact.MRSHeaderId;

            //明细
            // this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_ProductUnit);
            this.xrTableCellOtherCompactSum.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_OtherCompactCount);
            // this.xrTableCellStock.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_StocksQuantity,"{0:0.####}");
            //this.xrTableCell5.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableJiaoQi.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_JiaoQi, "{0:yyyy-MM-dd}");
            this.xrTableDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_Description);
            this.TCNextWorkHouse.DataBindings.Add("Text", this.DataSource, "WorkHouseNext." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDesc");
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
