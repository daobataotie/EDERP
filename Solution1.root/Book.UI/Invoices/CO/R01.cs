using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CO
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCOManager invoiceCGManager = new Book.BL.InvoiceCOManager();
        private BL.InvoiceCODetailManager invoiceCGDetailManager = new Book.BL.InvoiceCODetailManager();
        private BL.InvoiceXOManager invoiceXoManager = new BL.InvoiceXOManager();
        private Model.InvoiceCO invoice;
        int pp = 0;
        public R01(string invoiceid)
        {
            InitializeComponent();

            this.invoice = this.invoiceCGManager.Get(invoiceid);


            if (this.invoice == null)
                return;

            // this.invoice.Details = this.invoiceCGDetailManager.Select(this.invoice);
            this.DataSource = this.invoice.Details;

            this.xrBarCode1.Text = this.invoice.InvoiceId;
            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelData.Text = Properties.Resources.InvoiceCO;
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            //客户信息
            this.xrLabelCustomName.Text = this.invoice.Supplier.SupplierShortName;
            this.xrLabelLianluoName.Text = this.invoice.Supplier.SupplierContact;
            this.xrLabelCustomFax.Text = this.invoice.Supplier.SupplierFax;
            this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Supplier.SupplierPhone1) ? this.invoice.Supplier.SupplierPhone2 : this.invoice.Supplier.SupplierPhone1;
            this.xrLabelTongYiNo.Text = this.invoice.Supplier.SupplierNumber;
            if (this.invoice.Customer != null)
            {
                this.xrLabelCustomer.Text = this.invoice.Customer.CustomerShortName;
                this.xrLabelJianCe.Text = this.invoice.Customer.CheckedStandard;
                this.lblFP.Text = this.invoice.Customer.CustomerFP;
            }
            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToShortDateString();
            this.xrLabelYJDate.Text = this.invoice.InvoiceYjrq == null ? "" : this.invoice.InvoiceYjrq.Value.ToShortDateString();
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabel25.Text += this.invoice.AuditEmp == null ? "" : this.invoice.AuditEmp.EmployeeName;
            this.xrLabelYeWuName.Text = this.invoice.Employee0 == null ? "" : this.invoice.Employee0.EmployeeName;
            this.xrLabelTotal1.Text = global::Helper.DateTimeParse.GetSiSheWuRu(this.invoice.InvoiceTotal.Value, BL.V.SetDataFormat.CGZJXiao.Value).ToString();
            this.xrLabelNote.Text = this.invoice.InvoiceNote;
            this.lblBiBie.Text = this.invoice.AtCurrencyCategory == null ? "" : this.invoice.AtCurrencyCategory.ToString();
            Model.InvoiceXO temp = this.invoiceXoManager.Get(this.invoice.InvoiceXOId);
            if (temp != null)
            {
                this.xrLabelInvoiceXOId.Text = temp.CustomerInvoiceXOId;
            }

            this.xrLabelPiHao.Text = this.invoice.SupplierLotNumber;
            this.lblMRPId.Text = this.invoice.MRSHeaderId;

            if (this.invoice.Customer != null && !string.IsNullOrEmpty(this.invoice.Customer.CheckedStandard))
            {
                if (this.invoice.Customer.CheckedStandard.ToLower().Contains("jis") && this.invoice.Customer.CustomerFullName.ToUpper().Contains("MIDORI"))
                {
                    CreateTagLable("JIS");
                }
                else if (this.invoice.Customer.CheckedStandard.ToLower().Contains("as"))
                {
                    CreateTagLable("AS");
                }
            }

            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceProductUnit);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_OrderQuantity);
            this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            //this.xrTableCellNetWeight.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_GrossWeight, "{0:0}");
            //this.xrTableCellGrossWeight.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_NetWeight, "{0:0}");
            //this.xrTableCellBulk.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_Bulk, "{0:0}");
            this.xrRichTextProductDescribe.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            this.xrLabelDetailDesc.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailNote);
            this.xrTableCellNextWorkHouse.DataBindings.Add("Text", this.DataSource, "NextWorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
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
