using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherCompact
{
    public partial class RoSub_Zijian : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherCompactMaterialManager produceOtherCompactMaterialManager = new Book.BL.ProduceOtherCompactMaterialManager();

        public RoSub_Zijian(string produceOtherCompactId)
        {
            InitializeComponent();

            this.DataSource = produceOtherCompactMaterialManager.Select(produceOtherCompactId);

            this.TC_ProId.DataBindings.Add("Text", this.DataSource, "Product.Id");
            this.TC_ProName.DataBindings.Add("Text", this.DataSource, "Product.ProductName");
            this.TC_Qty.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactMaterial.PRO_ProduceQuantity);
            this.TC_Unit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactMaterial.PRO_ProductUnit);
            this.TC_ParentPro.DataBindings.Add("Text", this.DataSource, "ParentProduct.ProductName");
        }

    }
}
