using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCFirstOnlineCheck
{
    public partial class ROImpactCheck : DevExpress.XtraReports.UI.XtraReport
    {
        BL.PCImpactCheckManager manager = new Book.BL.PCImpactCheckManager();

        public ROImpactCheck()
        {
            InitializeComponent();

            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(ROImpactCheck_BeforePrint);

            //Details
            this.TCattrDate.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrDate, "{0:yyyy-MM-dd HH:mm}");
            this.TCattrBanbie.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrBanBie);
            this.TCattrGlassUpL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassUpLDis);
            this.TCattrGlassUpR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassUpRDis);
            this.TCattrGlassDownL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassDownLDis);
            this.TCattrGlassDownR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassDownRDis);
            this.lblattrGlassLeftL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassLeftLDis);
            this.lblattrGlassLeftR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassLeftRDis);
            this.lblattrGlassRightL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassRightLDis);
            this.lblattrGlassRightR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassRightRDis);
            this.lblattrCentralL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrCentralLDis);
            this.lblattrCentralR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrCentralRDis);
            this.TCattrNoseCentral.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrNoseCentralDis);
            this.TCattrGuanZui.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGuanZuiDis);
            this.lblattr_15L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr_15LDis);
            this.lblattr_15R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr_15RDis);
            this.TCattr0L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr0LDis);
            this.TCattr0R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr0RDis);
            this.TCattr15L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr15LDis);
            this.TCattr15R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr15RDis);
            this.TCattr30L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr30LDis);
            this.TCattr30R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr30RDis);
            this.lblattr45L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr45LDis);
            //this.lblattr45R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr45RDis);
            this.TCattr60L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr60LDis);
            this.TCattr60R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr60RDis);
            this.TCattr75L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr75LDis);
            this.TCattr75R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr75RDis);
            this.TCattr90L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr90LDis);
            this.TCattr90R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr90RDis);
            this.lblattr90T.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr90TDis);
            this.lblattr90B.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr90BDis);
            this.TCattrJieHenL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrJieHenLDis);
            this.TCattrJieHenR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrJieHenRDis);
            this.TCattrWingL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrWingLDis);
            this.TCattrWingR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrWingRDis);
            this.TCattrFootL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrFootLDis);
            //this.TCattrFootR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrFootRDis);
            //this.lblBanbie.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrBanBie);
            this.RT_retest.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrRetest);

            this.TCPronoteHeaderId.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_PronoteHeaderId);
            this.TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCUnit.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_ProductUnitId);
            this.TCInvoiceCusID.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_InvoiceCusXOId);
            this.TCCheckStandard.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_mCheckStandard);
            this.TCInvoiceXOQuantity.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_InvoiceXOQuantity);
            this.TCCheckQuantity.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_PCImpactCheckQuantity);
            this.TCSecondTestTime.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_SecondTestTime, "{0:HH:mm}");
        }

        public string PCFirstOnlineCheckDetailId { get; set; }

        void ROImpactCheck_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.DataSource = manager.PFCSelect(PCFirstOnlineCheckDetailId);   //一般对应一张冲击测试单据

            //ROImpactCheckSub sub = this.ImpackCheckBub.ReportSource as ROImpactCheckSub;
            //sub.PCFirstOnlineCheckDetailId = PCFirstOnlineCheckDetailId;

            Model.PCImpactCheck _pcic = manager.PFCGetFirst(PCFirstOnlineCheckDetailId);
            if (_pcic != null)
            {
                _pcic = manager.GetDetail(_pcic.PCImpactCheckId);
                this.DataSource = _pcic.Details.OrderBy(d => d.attrDate).ToList();

            }
            else
            {
                _pcic = new Book.Model.PCImpactCheck();
                this.DataSource = this.DataSource = new List<Model.PCImpactCheckDetail>();
            }
        }

    }
}
