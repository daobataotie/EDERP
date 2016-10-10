using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PronoteHeader
{
    public partial class SheChuRO1 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.PronoteHeaderManager manager = new Book.BL.PronoteHeaderManager();
        BL.PronotedetailsMaterialManager detailManager = new Book.BL.PronotedetailsMaterialManager();
        public SheChuRO1()
        {
            InitializeComponent();

            this.lblPNT.DataBindings.Add("Text", DataSource, Model.PronoteHeader.PRO_PronoteHeaderID);
            this.lblBiaoZhu.DataBindings.Add("Text", DataSource, "InvoiceXO.Customer." + Model.Customer.PRO_CheckedStandard);
            this.lblPO.DataBindings.Add("Text", DataSource, Model.PronoteHeader.PRO_InvoiceCusId);
            this.lblJiaoHuoDate.DataBindings.Add("Text", DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_InvoiceYjrq, "{0:yyyy-MM-dd}");
            this.TCProductName.DataBindings.Add("Text", DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCMaterial.DataBindings.Add("Text", DataSource, "Material.Product." + Model.Product.PRO_ProductName);
            this.TCKG.DataBindings.Add("Text", DataSource, "Material." + Model.PronotedetailsMaterial.PRO_PronoteQuantity);
            this.TCNote.DataBindings.Add("Text", DataSource, Model.PronoteHeader.PRO_Pronotedesc);
        }
        public string PronoteHeaderId { get; set; }
        private void SheChuRO1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = manager.SelectByHeaderId(this.PronoteHeaderId);
            foreach (Model.PronoteHeader item in this.DataSource as IList<Model.PronoteHeader>)
            {
                item.Material = detailManager.GetFirstByHeaderId(item.PronoteHeaderID);
            }
        }

    }
}
