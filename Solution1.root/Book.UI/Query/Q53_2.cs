using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.Query
{
    public partial class Q53_2 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.ProduceOtherCompactManager produceOtherCompactManager = new BL.ProduceOtherCompactManager();
        BL.ProduceOtherCompactDetailManager detailManager = new Book.BL.ProduceOtherCompactDetailManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private Model.ProduceOtherCompact produceOtherCompact;
        private ConditionOtherCompact conditionCom;
        public Q53_2(ConditionOtherCompact condition)
        {
            InitializeComponent();
            this.conditionCom = condition;
            DataTable dt = this.detailManager.SelectDetail(condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.StartDate, condition.EndDate, condition.SupplierId1, condition.SupplierId2, condition.ProductId1, condition.ProductId2, condition.InvoiceCusId);

            if (dt == null || dt.Rows.Count <= 0)
                throw new global::Helper.InvalidValueException("無記錄");

            this.DataSource = dt;
            //CompanyInfo
            this.ReportName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.ProduceOtherCompactDetail;
            if (!global::Helper.DateTimeParse.DateTimeEquls(condition.StartDate, global::Helper.DateTimeParse.NullDate))
                this.xrLabelDateRange.Text += "自 " + condition.StartDate.ToString("yyyy-MM-dd");
            this.xrLabelDateRange.Text += "至 " + condition.EndDate.ToString("yyyy-MM-dd");
            this.xrLabelDates.Text += System.DateTime.Now.Date.ToString("yyyy-MM-dd");
            this.xrLabelSupplierId.Text = condition.SupplierName1;

            //Detail
            this.TC_Id.DataBindings.Add("Text", this.DataSource, "ProduceOtherCompactId");
            this.TC_Date.DataBindings.Add("Text", this.DataSource, "ProduceOtherCompactDate");
            this.TC_YJDate.DataBindings.Add("Text", this.DataSource, "JiaoQi");
            this.TC_InvoiceYJDate.DataBindings.Add("Text", this.DataSource, "InvoiceYjrq");
            this.TC_InvoiceCusId.DataBindings.Add("Text", this.DataSource, "CustomerInvoiceXOId");
            this.TC_Product.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TC_Quantity.DataBindings.Add("Text", this.DataSource, "OtherCompactCount");
            this.TC_InQuantity.DataBindings.Add("Text", this.DataSource, "InDepotCount");
            this.TC_CancelQuantity.DataBindings.Add("Text", this.DataSource, "CancelQuantity");
            this.TC_Unit.DataBindings.Add("Text", this.DataSource, "ProductUnit");
        }

    }
}
