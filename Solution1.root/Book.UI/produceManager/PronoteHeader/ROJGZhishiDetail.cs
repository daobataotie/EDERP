using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace Book.UI.produceManager.PronoteHeader
{
    public partial class ROJGZhishiDetail : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.MRSdetailsManager mrsdetailManager = new Book.BL.MRSdetailsManager();
        BL.InvoiceXOManager xomanamager = new Book.BL.InvoiceXOManager();
        public ROJGZhishiDetail(Query.ConditionPronoteHeader condition)
        {
            InitializeComponent();
            IList<Model.PronoteHeader> list = pronoteHeaderManager.GetByDateMa(condition.StartDate, condition.EndDate, condition.Customer, condition.CusXOId, condition.Product, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd, condition.SourceTpye, null, false, condition.ProNameKey, condition.ProCusNameKey, condition.PronoteHeaderIdKey, false, false, true, true);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException("無數據");

            }
            foreach (Model.PronoteHeader detail in list)
            {
                if (!string.IsNullOrEmpty(detail.InvoiceXOId))
                    detail.InvoiceXO = this.xomanamager.Get(detail.InvoiceXOId);
                detail.MRSDetails = this.mrsdetailManager.Get(detail.MRSdetailsId);

                if (string.IsNullOrEmpty(detail.CustomerProductName))
                    detail.CustomerProductName = new Help().GetCustomerProductNameByPronoteHeaderId(detail.PronoteHeaderID, detail.ProductId);
            }
            this.DataSource = list;
            this.xrLabelDataName.Text = Properties.Resources.GZZhiShi;
            this.bind(condition.SourceTpye);

        }
        public ROJGZhishiDetail(IList<Model.PronoteHeader> headerlist, int sourcetype)
        {
            InitializeComponent();
            IList<Model.PronoteHeader> list = headerlist;
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException("無數據");

            }
            foreach (Model.PronoteHeader detail in list)
            {
                if (!string.IsNullOrEmpty(detail.InvoiceXOId))
                    detail.InvoiceXO = this.xomanamager.Get(detail.InvoiceXOId);
                detail.MRSDetails = this.mrsdetailManager.Get(detail.MRSdetailsId);

                if (string.IsNullOrEmpty(detail.CustomerProductName))
                    detail.CustomerProductName = new Help().GetCustomerProductNameByPronoteHeaderId(detail.PronoteHeaderID, detail.ProductId);
            }
            this.DataSource = list;
            this.xrLabelDataName.Text = Properties.Resources.GZZhiShi;
            this.bind(sourcetype);
        }
        private void bind(int type)
        {
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;


            this.xrLabelPrintDate.Text = this.xrLabelPrintDate.Text + DateTime.Now.ToShortDateString();
            this.xrLabelPronoteHeaderID.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_PronoteHeaderID);
            this.xrLabelPronoteDte.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_PronoteDate, "{0:yyyy-MM-dd}");

            this.xrLabelCustomerXOId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.xrLabelCustomer.DataBindings.Add("Text", this.DataSource, "CustomerShortName");
            this.xrLabelCheckedStandard.DataBindings.Add("Text", this.DataSource, "CustomerCheckStandard");
            this.xrLabelCustomerProductName.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            this.xrLabelCount.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_DetailsSum);

            this.xrLabelUnit.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_ProductUnit);
            if (type != 0)
            {
                this.xrLabelXOJHDate.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_InvoiceYjrq, "{0:yyyy-MM-dd}");
                this.xrLabelPiHao.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerLotNumber);
            }
            this.xrLabelProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.xrRichTextProDesc.DataBindings.Add("Rtf", this.DataSource, "ProductDesc");
            this.xrLabelMRP.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_MRSHeaderId);
            this.xrLabelWorkHouse.DataBindings.Add("Text", this.DataSource, "Workhousename");
            this.xrLabelBeforepPackage.DataBindings.Add("Text", this.DataSource, "MRSDetails.BeforePackageProduct." + Model.Product.PRO_ProductName);
            this.GroupHeader1.GroupFields.Add(new GroupField(Model.PronoteHeader.PRO_PronoteHeaderID));
            this.xrLabelPronotedesc.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_Pronotedesc);
            this.xrLabelEmployee.DataBindings.Add("Text", this.DataSource, "Employee0Name");
            this.xrLabel14.DataBindings.Add("Text", this.DataSource, "AuditEmpName");
            this.lbl_MaterialSum.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_Materialprocessum);
            //this.lblChakuang.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_Chakuang);
            //this.lblPaihe.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_Paihe);
            //this.lblMoshu.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_Moshu);
            this.xrSubreport1.ReportSource = new RO1();
            this.xrSubreport2.ReportSource = new RO2();

            this.lbl_JIS.DataBindings.Add("Text", this.DataSource, "Lbl_JIS");
        }

        private void RO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO1 material = this.xrSubreport1.ReportSource as RO1;
            material.PronoteHeader = this.GetCurrentRow() as Model.PronoteHeader;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO2 material = this.xrSubreport2.ReportSource as RO2;
            material.PronoteHeader = this.GetCurrentRow() as Model.PronoteHeader;
        }

    }
}
