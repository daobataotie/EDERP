using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.StockLimitations
{
    public partial class OutStockReport : DevExpress.XtraReports.UI.XtraReport
    {
        private Model.DepotOut DepotOut;
        private BL.DepotOutManager DepotOutManager = new Book.BL.DepotOutManager();
        private BL.DepotOutDetailManager DepotOutDetailManager = new Book.BL.DepotOutDetailManager();

        BL.PronoteHeaderManager pronoteHeaderManager = new BL.PronoteHeaderManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.ProduceMaterialManager produceMaterialManager = new BL.ProduceMaterialManager();
        BL.ProduceMaterialdetailsManager produceMaterialDetailManager = new BL.ProduceMaterialdetailsManager();
        BL.ProduceOtherCompactManager produceOtherCompactManager = new BL.ProduceOtherCompactManager();
        BL.ProduceOtherMaterialDetailManager produceOtherMaterialDetailManager = new BL.ProduceOtherMaterialDetailManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        public OutStockReport(string DepotOutId)
        {

            InitializeComponent();
            this.DepotOut = this.DepotOutManager.Get(DepotOutId);

            if (this.DepotOut == null)
                return;

            this.DepotOut.Details = this.DepotOutDetailManager.GetDepotOutDetailByDepotOutId(this.DepotOut.DepotOutId);

            this.DataSource = this.DepotOut.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.DepotOut;
            this.xrLabelPrintDate.Text = "列表日期：" + DateTime.Now.ToShortDateString();

            this.xrLabelDepotOutId.Text = this.DepotOut.DepotOutId;
            this.xrLabelDepotOutDate.Text = this.DepotOut.DepotOutDate.Value.ToString("yyyy-MM-dd");
            if (this.DepotOut.Employee != null)
            {
                this.xrLabelEmployeeId.Text = this.DepotOut.Employee.EmployeeName;
            }
            if (this.DepotOut.InvioiceEmployee0 != null)
                this.xrLabelInvoiceEmp0.Text = this.DepotOut.InvioiceEmployee0.EmployeeName;
            if (DepotOut.Depot != null)
            {
                this.xrLabelProduceInDepotId.Text = DepotOut.Depot.DepotName;
            }
            Model.InvoiceXO InvoiceXO = null;

            if (this.DepotOut.SourceType == "領料單")
            {
                Model.ProduceMaterial ProduceMaterial = this.produceMaterialManager.Get(this.DepotOut.InvioiceId);
                if (ProduceMaterial != null)
                {
                    //Model.PronoteHeader PronoteHeader = this.pronoteHeaderManager.Get(ProduceMaterial.InvoiceId);
                    //if (PronoteHeader != null)
                    //{
                    InvoiceXO = this.invoiceXOManager.Get(ProduceMaterial.InvoiceXOId);
                    if (InvoiceXO != null)
                    {
                        this.xrLabelCustomXoId.Text = InvoiceXO.CustomerInvoiceXOId;
                        this.lblPiHao.Text = InvoiceXO.CustomerLotNumber;
                        this.lblXOCustomer.Text = InvoiceXO.xocustomer == null ? null : InvoiceXO.xocustomer.ToString();
                    }
                    //if (ProduceMaterial.Employee0 != null)
                    //    this.xrLabelInvoiceEmp0.Text = ProduceMaterial.Employee0.ToString();
                    if (ProduceMaterial.WorkHouse != null)
                        this.xrLabelWorkHouse.Text = ProduceMaterial.WorkHouse.ToString();
                    // }
                }
            }
            else if (this.DepotOut.SourceType == "委外領料單")
            {
                Model.ProduceOtherMaterial ProduceOtherMaterial = new BL.ProduceOtherMaterialManager().Get(this.DepotOut.InvioiceId);
                if (ProduceOtherMaterial != null)
                {
                    Model.ProduceOtherCompact ProduceOtherCompact = this.produceOtherCompactManager.Get(ProduceOtherMaterial.ProduceOtherCompactId);
                    if (ProduceOtherCompact != null)
                    {
                        if (!string.IsNullOrEmpty(ProduceOtherCompact.MRSHeaderId))
                        {
                            Model.MRSHeader mRSHeader = this.mRSHeaderManager.Get(ProduceOtherCompact.MRSHeaderId);
                            if (mRSHeader != null)
                            {
                                Model.MPSheader mPSheader = this.mPSheaderManager.Get(mRSHeader.MPSheaderId);
                                if (mPSheader != null)
                                {
                                    InvoiceXO = this.invoiceXOManager.Get(mPSheader.InvoiceXOId);
                                    if (InvoiceXO != null)
                                    {
                                        this.xrLabelCustomXoId.Text = InvoiceXO.CustomerInvoiceXOId;
                                        this.lblPiHao.Text = InvoiceXO.CustomerLotNumber;
                                        this.lblXOCustomer.Text = InvoiceXO.xocustomer == null ? null : InvoiceXO.xocustomer.ToString();
                                    }
                                }
                            }

                        }
                    }
                    if (ProduceOtherMaterial.WorkHouse != null)
                        this.xrLabelWorkHouse.Text = ProduceOtherMaterial.WorkHouse.ToString();
                }
            }

            if (InvoiceXO != null && InvoiceXO.xocustomer != null && !string.IsNullOrEmpty(InvoiceXO.xocustomer.CheckedStandard))
            {
                if (InvoiceXO.xocustomer.CheckedStandard.ToLower().Contains("jis") && InvoiceXO.xocustomer.CustomerFullName.ToUpper().Contains("MIDORI"))
                {
                    //CreateTagLable("JIS");

                    this.lbl_JIS.Text = "JIS";
                }
                else if (InvoiceXO.xocustomer.CheckedStandard.ToLower().Contains("as"))
                {
                    //CreateTagLable("AS");

                    this.lbl_JIS.Text = "AS";
                }
            }

            this.txt_ParentProduct.Text = DepotOut.ParentProduct;
            this.xrLabelXqlu.Text = DepotOut.SourceType;
            this.xrLabeldescription.Text = DepotOut.description;
            this.xrLabelXgdj.Text = DepotOut.InvioiceId;
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellXH.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellCount.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_DepotOutDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_ProductUnit);
            this.xrTableCurrentDepotStock.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_CurrentDepotQuantity);
            this.xrTableCell_CurrentStock.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_CurrentStockQuantity);
            this.xrTableCellDepotId.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            //this.xrTableCellSafeStockQuantity.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.Pro_SafeStockQuantity);
            this.TCPihao.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_Pihao);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, Model.DepotOutDetail.Pro_ProductDescription);
            //this.TCPihao.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_Pihao);
            this.TCProductCustomerName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
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
