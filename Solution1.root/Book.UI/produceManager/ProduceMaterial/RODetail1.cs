using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceMaterial
{
    public partial class RODetail1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceMaterialdetailsManager produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();
        public RODetail1()
        {
            InitializeComponent();
            //明细
            this.xrTableCell5.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_Inumber);
            //this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.Pro_ProductNameWithVersion);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_Materialprocessum);
            //this.xrTableHasOutDepot.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_Distributioned, "{0:0.####}");        
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, "ProductUnit");
            this.xrTableCellProductSpecification.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_StocksQuantity, "{0:0.####}");
            this.xrTableCellPihao.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_Pihao);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDescription");
            this.xrTableCusProName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.TC_NextWorkStation.DataBindings.Add("Text", this.DataSource, "NextWorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
        }

        private Model.ProduceMaterial _produceMaterial;

        public Model.ProduceMaterial ProduceMaterial
        {
            get { return _produceMaterial; }
            set { _produceMaterial = value; }
        }
        private void RODetail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.ProduceMaterial == null)
                return;
            ProduceMaterial.Details = this.produceMaterialdetailsManager.Select(ProduceMaterial);
            this.DataSource = ProduceMaterial.Details;
        }

    }
}
