using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Query
{
    public partial class Q54Update : DevExpress.XtraReports.UI.XtraReport
    {
        BL.ProduceOtherMaterialDetailManager otherDetailManager = new Book.BL.ProduceOtherMaterialDetailManager();
        private Model.ProduceOtherMaterial produceOtherMaterial;

        public Model.ProduceOtherMaterial ProduceOtherMaterial
        {
            get { return produceOtherMaterial; }
            set { produceOtherMaterial = value; }
        }

        public Q54Update(ConditionOtherMaterial condition)
        {
            InitializeComponent();

            IList<Model.ProduceOtherMaterialDetail> list = otherDetailManager.SelectDetailByCondition(condition.StartDate, condition.EndDate, condition.SupplierId1, condition.SupplierId2, condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.ProductId1, condition.ProductId2, condition.InvoiceCusId);
            if (list == null || list.Count <= 0)
                throw new global::Helper.MessageValueException("無記錄！");

            this.RepotName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.ProduceOtherMaterialDetail;
            this.xrLabelDates.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.DataSource = list;
            this.TCSupplier.DataBindings.Add("Text", this.DataSource, "SupplierShortName");
            this.TCOtherCompactID.DataBindings.Add("Text", this.DataSource, "ProduceOtherCompactId");
            this.TCJHR.DataBindings.Add("Text", this.DataSource, "JiaoQi", "{0:yyyy-MM-dd}");
            this.TCYJRQ.DataBindings.Add("Text", this.DataSource, "InvoiceYjrq", "{0:yyyy-MM-dd}");
            this.TC_InvoiceDate.DataBindings.Add("Text", this.DataSource, "ProduceOtherMaterialDate", "{0:yyyy-MM-dd}");
            this.TCInvoiceCusID.DataBindings.Add("Text", this.DataSource, "CustomerInvoiceXOId");
            this.xrTableProName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.xrTableQuanTity.DataBindings.Add("Text", this.DataSource, "InvoiceUseQuantity", "{0:0.##}");
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, "ProductUnit");
        }
    }
}
