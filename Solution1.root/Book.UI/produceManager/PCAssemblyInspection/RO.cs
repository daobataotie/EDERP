using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCAssemblyInspection
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.PCAssemblyInspection model)
            : this()
        {
            Model.PCAssemblyInspection pCAssemblyInspection = new BL.PCAssemblyInspectionManager().GetDetail(model.PCAssemblyInspectionId);
            this.DataSource = pCAssemblyInspection.Details;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = "品管抽检日报表";
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lbl_PCAssemblyInspectionId.Text = pCAssemblyInspection.PCAssemblyInspectionId;
            this.lbl_PCAssemblyInspectionDate.Text = pCAssemblyInspection.PCAssemblyInspectionDate.Value.ToString("yyyy-MM-dd");
            this.lbl_PronoteHeaderId.Text = pCAssemblyInspection.PronoteHeaderId;
            this.lbl_InvoiceCusId.Text = pCAssemblyInspection.InvoiceCusId;
            this.lbl_Note.Text = pCAssemblyInspection.Note;
            this.lbl_Employee.Text = pCAssemblyInspection.Employee == null ? "" : pCAssemblyInspection.Employee.EmployeeName;
            this.lblEmployee1.Text = pCAssemblyInspection.Employee1 == null ? "" : pCAssemblyInspection.Employee1.EmployeeName;

            TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            TCCheckDate.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_PCAssemblyInspectionDetailDate, "{0:HH:mm}");
            TCCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerShortName);
            TCCheckNum.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_CheckNum, "{0:0}");
            TCWaiguan.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Waiguan);
            TCJiagongbie.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Jiagongbie);
            TCJiao.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Jiao);
            TCShensuojiao.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Shensuojiao);
            TCSuoluosi.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Luosi);
            TCCashang.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Cashang);
            TCKuang.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Kuang);
            TCBidian.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Bidian);
            TCBaozhuangdai.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Baozhuangdai);
            TCTiaomabiao.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Tiaomabiao);
            TCZhengcemai.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Zhengcemai);
            TCChongji.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Chongji);
            TCEmoloyee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            TCNote.DataBindings.Add("Text", this.DataSource, Model.PCAssemblyInspectionDetail.PRO_Note);
        }
    }
}
