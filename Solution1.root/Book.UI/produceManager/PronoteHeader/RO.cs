using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace Book.UI.produceManager.PronoteHeader
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        private BL.PronotedetailsMaterialManager pronotedetailsMaterialManager = new Book.BL.PronotedetailsMaterialManager();

        private Model.PronoteHeader pronoteHeader;
        private BL.PronoteProceduresDetailManager pronoteProceduresDetailManager = new Book.BL.PronoteProceduresDetailManager();
        private System.Collections.Generic.IList<Model.PronoteMachine> machineList = new System.Collections.Generic.List<Model.PronoteMachine>();
        private BL.PronoteMachineManager pronoteMachineManager = new BL.PronoteMachineManager();
        BL.MRSdetailsManager mRSDetailsManager = new BL.MRSdetailsManager();
        public RO(string pronoteHeaderId, int flag)
        {
            InitializeComponent();
            this.pronoteHeader = this.pronoteHeaderManager.GetDetails(pronoteHeaderId);

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            //this.xrLabelDataName.Text = Properties.Resources.Pronotedetails;
            if (flag == 5)
            {
                this.xrLabelDataName.Text = Properties.Resources.GZZhiShi;

            }
            else if (flag == 4)
            {
                this.xrLabelDataName.Text = Properties.Resources.ZZJiaGong;

                this.lbl_PageSign.Text = "QR8-06-09-1";
            }
            this.xrLabelPrintDate.Text = this.xrLabelPrintDate.Text + DateTime.Now.ToShortDateString();
            if (pronoteHeader.WorkHouse != null)
                this.xrLabelWorkHouse.Text = this.pronoteHeader.WorkHouse.Workhousename;

            Model.MRSdetails mrsdetail = this.mRSDetailsManager.Get(this.pronoteHeader.MRSdetailsId);
            if (mrsdetail != null)
                this.xrLabelBeforepPackage.Text = mrsdetail.BeforePackageProduct == null ? string.Empty : (mrsdetail.BeforePackageProduct.IsCustomerProduct.HasValue && mrsdetail.BeforePackageProduct.IsCustomerProduct.Value ? mrsdetail.BeforePackageProduct.ProductName + "{" + mrsdetail.BeforePackageProduct.CustomerProductName + "}" : mrsdetail.BeforePackageProduct.ProductName);
            else
                this.xrLabelBeforepPackage.Text = string.Empty;
            //生產通知
            this.xrLabelPronoteHeaderID.Text = this.pronoteHeader.PronoteHeaderID;
            this.xrLabelPronoteDte.Text = this.pronoteHeader.PronoteDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelMRP.Text = this.pronoteHeader.MRSHeaderId;
            if (this.pronoteHeader.Employee0 != null && flag != 1)
            {
                this.xrLabelEmployee.Text = this.pronoteHeader.Employee0.EmployeeName;
            }
            this.xrLabel14.Text = this.pronoteHeader.AuditEmp == null ? "" : this.pronoteHeader.AuditEmp.EmployeeName;
            if (pronoteHeader.Product != null)
            {

                this.xrLabelProductName.Text = pronoteHeader.Product.ProductName + "    版次:" + pronoteHeader.Product.ProductVersion;
                if (string.IsNullOrEmpty(pronoteHeader.Product.CustomerProductName))
                    this.xrLabelCustomerProductName.Text = new Help().GetCustomerProductNameByPronoteHeaderId(pronoteHeader.PronoteHeaderID, pronoteHeader.ProductId);
                else
                    this.xrLabelCustomerProductName.Text = pronoteHeader.Product.CustomerProductName;
                this.xrRichTextProDesc.Rtf = this.pronoteHeader.Product.ProductDescription;
                if (this.pronoteHeader.Product.AttrZhengMai != null)
                    this.RichTextZhengMai.Rtf = this.pronoteHeader.Product.AttrZhengMai;
                if (this.pronoteHeader.Product.AttrCeMai != null)
                    this.RichTextCeMai.Rtf = this.pronoteHeader.Product.AttrCeMai;

            }
            Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(this.pronoteHeader.InvoiceXOId);
            if (xo != null)
            {
                this.xrLabelCheckedStandard.Text = xo.xocustomer.CheckedStandard;
                this.xrLabelCustomer.Text = xo.xocustomer.CustomerShortName;
                this.xrLabelCustomerXOId.Text = xo.CustomerInvoiceXOId;
                this.xrLabelPiHao.Text = xo.CustomerLotNumber;
                this.xrLabelXOJHDate.Text = xo.InvoiceYjrq.Value.ToString("yyyy-MM-dd");

                if (xo.xocustomer != null && !string.IsNullOrEmpty(xo.xocustomer.CheckedStandard))
                {
                    if (xo.xocustomer.CheckedStandard.ToLower().Contains("jis") && xo.xocustomer.CustomerFullName.ToUpper().Contains("MIDORI"))
                    {
                        //CreateTagLable("JIS");

                        this.lbl_JIS.Text = "JIS";
                    }
                    else if (xo.xocustomer.CheckedStandard.ToLower().Contains("as"))
                    {
                        //CreateTagLable("AS");

                        this.lbl_JIS.Text = "AS";
                    }
                }
            }
            this.xrLabelCount.Text = pronoteHeader.DetailsSum.ToString();
            this.xrLabelUnit.Text = pronoteHeader.ProductUnit;
            this.xrLabelPronotedesc.Text = this.pronoteHeader.Pronotedesc;
            this.lbl_ProduceMaterial.Text = this.pronoteHeader.Materialprocessum == null ? "" : this.pronoteHeader.Materialprocessum.ToString();

            //if (this.pronoteHeader.DetailProcedures != null && this.pronoteHeader.DetailProcedures.Count > 0)
            //{
            //    this.pronoteHeader.DetailProcedures = this.pronoteHeader.DetailProcedures.OrderByDescending(p => p.PronoteProceduresDate).ToList();

            //    if (this.pronoteHeader.DetailProcedures.First().WorkHouse != null)
            //        this.xrLabelhouseId.Text = this.pronoteHeader.DetailProcedures.First().WorkHouse.Workhousename;
            //}
            this.lblInvoiceXODetailQuantity.Text = this.pronoteHeader.InvoiceXODetailQuantity == null ? "0" : this.pronoteHeader.InvoiceXODetailQuantity.ToString();


            this.xrSubreport1.ReportSource = new RO1();
            this.xrSubreport2.ReportSource = new RO2();
        }
        //int flag = 0;
        //public RO(Query.ConditionPronoteHeader condition)
        //{
        //    InitializeComponent();
        //    flag = 1;
        //    IList<Model.PronoteHeader> list = pronoteHeaderManager.GetByDate(condition.StartDate, condition.EndDate, condition.Customer, condition.CusXOId, condition.Product, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd, condition.SourceTpye);
        //    if (list == null || list.Count <= 0)
        //    {
        //         throw new global::Helper.InvalidValueException("無數據");

        //    }
        //    this.DataSource = list;
        //    //CompanyInfo
        //    this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
        //    this.xrLabelDataName.Text = Properties.Resources.Pronotedetails;
        //    //if (flag == 1)
        //    //{
        //    //    this.xrLabelDataName.Text = Properties.Resources.GZZhiShi;

        //    //}
        //    //else if (flag == 2)
        //    //{
        //    //    this.xrLabelDataName.Text = Properties.Resources.ZZJiaGong;
        //    //}
        //    //this.xrLabelPrintDate.Text = this.xrLabelPrintDate.Text + DateTime.Now.ToShortDateString();
        //    //if (pronoteHeader.WorkHouse != null)
        //    //    this.xrLabelWorkHouse.Text = this.pronoteHeader.WorkHouse.Workhousename;

        //    //Model.MRSdetails mrsdetail = this.mRSDetailsManager.Get(this.pronoteHeader.MRSdetailsId);
        //    //if (mrsdetail != null)
        //    //    this.xrLabelBeforepPackage.Text = mrsdetail.BeforePackageProduct == null ? string.Empty : (mrsdetail.BeforePackageProduct.IsCustomerProduct.HasValue && mrsdetail.BeforePackageProduct.IsCustomerProduct.Value ? mrsdetail.BeforePackageProduct.ProductName + "{" + mrsdetail.BeforePackageProduct.CustomerProductName + "}" : mrsdetail.BeforePackageProduct.ProductName);
        //    //else
        //    //    this.xrLabelBeforepPackage.Text = string.Empty;
        //    //生產通知



        //    this.xrLabelPronoteHeaderID.DataBindings.Add("Text",this.DataSource,Model.PronoteHeader.PRO_PronoteHeaderID );  
        //        this.xrLabelPronoteDte.DataBindings.Add("Text",this.DataSource,Model.PronoteHeader.PRO_PronoteDate,"{0:yyyy-MM-dd}" );  
        //    //this.xrLabelMRP.Text = this.pronoteHeader.MRSHeaderId;
        //    //if (this.pronoteHeader.Employee0 != null && flag != 1)
        //    //{
        //    //    this.xrLabelEmployee.Text = this.pronoteHeader.Employee0.EmployeeName;
        //    //}
        //    //if (pronoteHeader.Product != null)
        //    //{

        //    //    this.xrLabelProductName.Text = pronoteHeader.Product.ProductName;
        //    //    this.xrLabelCustomerProductName.Text = pronoteHeader.Product.CustomerProductName;
        //    //}
        //    //Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(this.pronoteHeader.InvoiceXOId);
        //    //if (xo != null)
        //    //{
        //    //    this.xrLabelCheckedStandard.Text = xo.xocustomer.CheckedStandard;
        //    //    this.xrLabelCustomer.Text = xo.xocustomer.CustomerShortName;
        //    //    this.xrLabelCustomerXOId.Text = xo.CustomerInvoiceXOId;
        //    //    this.xrLabelPiHao.Text = xo.CustomerLotNumber;
        //    //    this.xrLabelXOJHDate.Text = xo.InvoiceYjrq.Value.ToString("yyyy-MM-dd");

        //    //}
        //    //this.xrLabelCount.Text = pronoteHeader.DetailsSum.ToString();
        //    //this.xrLabelUnit.Text = pronoteHeader.ProductUnit;
        //    //this.xrLabelPronotedesc.Text = this.pronoteHeader.Pronotedesc;
        //    //this.xrRichTextProDesc.Rtf = this.pronoteHeader.Product.ProductDescription;
        //    //if (this.pronoteHeader.DetailProcedures != null && this.pronoteHeader.DetailProcedures.Count > 0)
        //    //{
        //    //    this.pronoteHeader.DetailProcedures = this.pronoteHeader.DetailProcedures.OrderByDescending(p => p.PronoteProceduresDate).ToList();

        //    //    if (this.pronoteHeader.DetailProcedures.First().WorkHouse != null)
        //    //        this.xrLabelhouseId.Text = this.pronoteHeader.DetailProcedures.First().WorkHouse.Workhousename;
        //    //}



        //    this.xrSubreport1.ReportSource = new RO1();
        //    this.xrSubreport2.ReportSource = new RO2();
        //}

        private void RO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO1 material = this.xrSubreport1.ReportSource as RO1;
            material.PronoteHeader = this.pronoteHeader;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO2 material = this.xrSubreport2.ReportSource as RO2;
            material.PronoteHeader = this.pronoteHeader;
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
