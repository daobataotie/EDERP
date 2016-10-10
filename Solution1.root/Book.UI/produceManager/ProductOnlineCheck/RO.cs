using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProductOnlineCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.ProductOnlineCheck model)
            : this()
        {
            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = Properties.Resources.ProductOnlineCheck;
            this.lbl_PrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.lbl_Id.Text = model.ProductOnlineCheckId;
            this.lbl_ProductOnlineCheckDate.Text = model.ProductOnlineCheckDate.Value.ToString("yyyy-MM-dd");
            this.lbl_OnlineDate.Text = model.OnlineDate.Value.ToString("yyyy-MM-dd");
            this.lbl_InvoiceXOId.Text = model.InvoiceXOId;
            this.lbl_PronoteHeaderId.Text = model.PronoteHeaderId;
            this.lbl_ProductName.Text = model.Product == null ? null : model.Product.ProductName;
            this.lbl_Employee.Text = model.Employee == null ? null : model.Employee.EmployeeName;
            this.xrRichText1.Rtf = model.Remark;

            foreach (Model.ProductOnlineCheckDetail detail in model.Detail)
            {
                detail.MaoBian = this.Scaler(detail.MaoBian);
                detail.CaShang = this.Scaler(detail.CaShang);
                detail.SuoShui = this.Scaler(detail.SuoShui);
                detail.DuiSe = this.Scaler(detail.DuiSe);
                detail.Zhepian = this.Scaler(detail.Zhepian);
            }
            this.DataSource = model.Detail;

            this.TC_CheckDate.DataBindings.Add("Text", DataSource, Model.ProductOnlineCheckDetail.PRO_CheckDate, "{0:yyyy-MM-dd}");
            this.TC_CaShang.DataBindings.Add("Text", DataSource, Model.ProductOnlineCheckDetail.PRO_CaShang);
            this.TC_MaoBian.DataBindings.Add("Text", DataSource, Model.ProductOnlineCheckDetail.PRO_MaoBian);
            this.TC_SuoShui.DataBindings.Add("Text", DataSource, Model.ProductOnlineCheckDetail.PRO_SuoShui);
            this.TC_DuiSe.DataBindings.Add("Text", DataSource, Model.ProductOnlineCheckDetail.PRO_DuiSe);
            this.TC_ZhePian.DataBindings.Add("Text", DataSource, Model.ProductOnlineCheckDetail.PRO_Zhepian);
            this.TC_Exception.DataBindings.Add("Text", DataSource, Model.ProductOnlineCheckDetail.PRO_Remark);
        }

        private string Scaler(string str)
        {
            switch (str)
            {
                case "0":
                    str = "¡Ì";
                    break;
                case "1":
                    str = "X";
                    break;
                case "2":
                    str = "¡÷";
                    break;
            }
            return str;
        }
    }

}
