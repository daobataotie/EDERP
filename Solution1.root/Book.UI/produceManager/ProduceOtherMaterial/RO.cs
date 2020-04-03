﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherMaterial
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherMaterialManager produceOtherMaterialManager = new Book.BL.ProduceOtherMaterialManager();
        private BL.ProduceOtherMaterialDetailManager ProduceOtherMaterialDetailManager = new Book.BL.ProduceOtherMaterialDetailManager();
        private BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        private BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        private BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private Model.ProduceOtherMaterial produceOtherMaterial;
        public RO(string produceOtherMaterialId)
        {
            InitializeComponent();
            this.produceOtherMaterial = this.produceOtherMaterialManager.GetDetails(produceOtherMaterialId);

            if (this.produceOtherMaterial == null)
                return;

            //   this.produceOtherMaterial.Details = this.ProduceOtherMaterialDetailManager.GetOrderById(this.produceOtherMaterial);

            this.DataSource = this.produceOtherMaterial.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceOtherMaterialDetail;
            this.xrLabelDepot.Text = this.produceOtherMaterial.Depot == null ? "" : this.produceOtherMaterial.Depot.ToString();
            this.xrLabelDate.Text += DateTime.Now.ToShortDateString();

            xrLabelSup.Text = this.produceOtherMaterial.Supplier == null ? null : this.produceOtherMaterial.Supplier.SupplierShortName;
            //外包領料
            this.xrLabelProduceOtherMaterialId.Text = this.produceOtherMaterial.ProduceOtherMaterialId;
            this.xrLabelProduceOtherMaterialDate.Text = this.produceOtherMaterial.ProduceOtherMaterialDate.Value.ToString("yyyy-MM-dd");
            if (this.produceOtherMaterial.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.produceOtherMaterial.Employee0.EmployeeName;
            }
            if (this.produceOtherMaterial.Employee1 != null)
            {
                this.xrLabelEmployee1.Text = this.produceOtherMaterial.Employee1.EmployeeName;
            }
            if (this.produceOtherMaterial.WorkHouse != null)
            {
                this.xrLabelDepartment.Text = this.produceOtherMaterial.WorkHouse.Workhousename;
            }
            this.xrLabelOtherCam.Text = this.produceOtherMaterial.ProduceOtherCompactId;
            this.xrLabelProduceOtherMaterialDesc.Text = this.produceOtherMaterial.ProduceOtherMaterialDesc;

            if (!string.IsNullOrEmpty(produceOtherMaterial.ProduceOtherCompactId))
            {
                Model.ProduceOtherCompact OtherCompact = new BL.ProduceOtherCompactManager().Get(produceOtherMaterial.ProduceOtherCompactId);
                if (OtherCompact != null)
                {
                    //Model.MRSHeader mrsHeader = this.mRSHeaderManager.Get(OtherCompact.MRSHeaderId);
                    //if (mrsHeader != null)
                    //{
                    //    Model.MPSheader mPSheader = this.mPSheaderManager.Get(mrsHeader.MPSheaderId);
                    //    if (mPSheader != null)
                    //    {
                    //        Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mPSheader.InvoiceXOId);
                    //        this.xrLabelCustomerXOId.Text = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
                    //    }
                    //}
                    Model.InvoiceXO invoiceXO = OtherCompact.InvoiceXO;
                    if (invoiceXO != null)
                    {
                        this.xrLabelCustomerXOId.Text = invoiceXO.CustomerInvoiceXOId;

                        if (invoiceXO.xocustomer != null && !string.IsNullOrEmpty(invoiceXO.xocustomer.CheckedStandard))
                        {
                            if (invoiceXO.xocustomer.CheckedStandard.ToLower().Contains("jis") && invoiceXO.xocustomer.CustomerFullName.ToUpper().Contains("MIDORI"))
                            {
                                //CreateTagLable("JIS");

                                this.lbl_JIS.Text = "JIS";
                            }
                            else if (invoiceXO.xocustomer.CheckedStandard.ToLower().Contains("as"))
                            {
                                //CreateTagLable("AS");

                                this.lbl_JIS.Text = "AS";
                            }
                        }
                    }
                }
            }
            //this.xrLabelCustomerXOId.Text = this.produceOtherMaterial.InvoiceCusId;
            //明细
            //this.xrTableCell1ProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //if (produceOtherMaterial.WorkHouse!=null)
            //{
            //    if(produceOtherMaterial.WorkHouse.Workhousename != null)
            //    this.xrTableCellDepartment.DataBindings.Add("Text", this.DataSource, "ProduceOtherMaterialWorkHouse.Workhousename");
            //}
            // this.xrTableCellProduceOtherMaterialDate.DataBindings.Add("Text", this.DataSource, "ProduceOtherMaterial." + Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialDate, "{0:yyyy-MM-dd}");
            this.xrTableCellOtherMaterialQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_OtherMaterialQuantity);
            this.xrTableCellDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_Description);
            this.xrTableCell4.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_ProductUnit);
            //this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            this.xrTableCellParent.DataBindings.Add("Text", this.DataSource, "ParentProduct." + Model.Product.PRO_ProductName);
            this.xrTableInumber.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_Inumber);
            this.xrTableStock.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_ProductStock);
            this.xrTableCellNum.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_PiHao);
            this.TCKouliaoDate.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_KouliaoDate, "{0:yyyy-MM-dd}");
            this.TCInvoiceUseQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_InvoiceUseQuantity);
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
