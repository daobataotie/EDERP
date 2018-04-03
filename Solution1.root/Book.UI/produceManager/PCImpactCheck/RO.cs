using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.produceManager.PCImpactCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.PCImpactCheck _pcic)
        {
            InitializeComponent();
            if (_pcic == null)
                return;
            this.DataSource = _pcic.Details;

            //CompanyInfo
            this.lblCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCImpactCheck;
            this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblPCImpactCheckId.Text = _pcic.PCImpactCheckId;
            this.lblPCImpactCheckDate.Text = _pcic.PCImpactCheckDate.Value.ToShortDateString();
            //this.lblInvoiceCusXOId.Text = _pcic.InvoiceCusXOId;
            //this.lblPronoteHeaderId.Text = _pcic.PronoteHeaderId;
            this.lblPCImpactCheckDesc.Text = _pcic.PCImpactCheckDesc;
            //this.lblPCImpactCheckQuantity.Text = _pcic.PCImpactCheckQuantity.Value.ToString();
            this.lblEmployeeId.Text = _pcic.Employee == null ? "" : _pcic.Employee.ToString();
            this.lblWorkHouseId.Text = _pcic.WorkHouse == null ? "" : _pcic.WorkHouse.ToString();
            //this.lblProductName.Text = _pcic.Product == null ? "" : _pcic.Product.ToString();
            //this.lblDanJuTest.Text = _pcic.PCFromType > 0 ? "委外單據編號:" : "加工單據編號:";
            //this.lblCheckStandard.Text = _pcic.mCheckStandard;      //质检标准
            //this.lblInvoiceXOQuantity.Text = _pcic.InvoiceXOQuantity.HasValue ? _pcic.InvoiceXOQuantity.Value.ToString() : "";
            //this.lbl_ProuductUnit.Text = _pcic.ProductUnit == null ? "" : _pcic.ProductUnit.ToString();
            //Details
            this.TCattrDate.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrDate, "{0:yyyy-MM-dd}");
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
            this.TCSecondTestTime.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_SecondTestTime, "{0:yyyy-MM-dd}");
        }

        public RO(Model.PCImpactCheck _pcic, string RowFilter)
        {
            InitializeComponent();
            if (_pcic == null)
                return;
            try
            {
                DataTable dt = new BL.PCImpactCheckDetailManager().SelectByHeaderId(_pcic.PCImpactCheckId);
                DataView dv = dt.DefaultView;
                dv.RowFilter = RowFilter;
                this.DataSource = dv;
            }
            catch
            {
                throw new Helper.MessageValueException("不能以此列作為排序條件打印");
            }

            //CompanyInfo
            this.lblCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCImpactCheck;
            this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblPCImpactCheckId.Text = _pcic.PCImpactCheckId;
            this.lblPCImpactCheckDate.Text = _pcic.PCImpactCheckDate.Value.ToShortDateString();
            //this.lblInvoiceCusXOId.Text = _pcic.InvoiceCusXOId;
            //this.lblPronoteHeaderId.Text = _pcic.PronoteHeaderId;
            this.lblPCImpactCheckDesc.Text = _pcic.PCImpactCheckDesc;
            //this.lblPCImpactCheckQuantity.Text = _pcic.PCImpactCheckQuantity.Value.ToString();
            this.lblEmployeeId.Text = _pcic.Employee == null ? "" : _pcic.Employee.ToString();
            this.lblWorkHouseId.Text = _pcic.WorkHouse == null ? "" : _pcic.WorkHouse.ToString();
            //this.lblProductName.Text = _pcic.Product == null ? "" : _pcic.Product.ToString();
            //this.lblDanJuTest.Text = _pcic.PCFromType > 0 ? "委外單據編號:" : "加工單據編號:";
            //this.lblCheckStandard.Text = _pcic.mCheckStandard;      //质检标准
            //this.lblInvoiceXOQuantity.Text = _pcic.InvoiceXOQuantity.HasValue ? _pcic.InvoiceXOQuantity.Value.ToString() : "";
            //this.lbl_ProuductUnit.Text = _pcic.ProductUnit == null ? "" : _pcic.ProductUnit.ToString();
            //Details

            this.TCattrDate.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrDate, "{0:yyyy-MM-dd}");
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
            this.TCProduct.DataBindings.Add("Text", this.DataSource, "Product");
            this.TCUnit.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_ProductUnitId);
            this.TCInvoiceCusID.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_InvoiceCusXOId);
            this.TCCheckStandard.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_mCheckStandard);
            this.TCInvoiceXOQuantity.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_InvoiceXOQuantity);
            this.TCCheckQuantity.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_PCImpactCheckQuantity);
        }
    }
}
