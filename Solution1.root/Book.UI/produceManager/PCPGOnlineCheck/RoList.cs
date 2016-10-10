using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class RoList : DevExpress.XtraReports.UI.XtraReport
    {
        BL.PCPGOnlineCheckManager _pcpgocm = new Book.BL.PCPGOnlineCheckManager();
        BL.PCPGOnlineCheckDetailManager _pcpgcDetailm = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.ProduceOtherCompactManager _pocm = new Book.BL.ProduceOtherCompactManager();
        IList<Model.PCPGOnlineCheckDetail> _DDetails = new List<Model.PCPGOnlineCheckDetail>();

        public RoList(IList<Model.PCPGOnlineCheck> list)
        {
            InitializeComponent();

            //CompanyInfo
            this.lblCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCPGOnlineCheck;
            this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            this.DataSource = list;

            foreach (var item in list)
            {
                //if (item.PCPGOnlineCheckType.Value > 0)
                //    item.Supplier = this._pocm.Get(item.FromPCId) == null ? null : this._pocm.Get(item.FromPCId).Supplier;

                if (item.PCPGOnlineCheckType == 1)
                    item.FromType = "委外合同單";
                else if (item.PCPGOnlineCheckType == -1)
                    item.FromType = "生產加工單";
                else if (item.PCPGOnlineCheckType == 0)
                    item.FromType = "採購單";
            }

            //Control
            this.lblPCPGOnlineCheckId.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheck.PRO_PCPGOnlineCheckId);
            this.lblDate.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheck.PRO_PCPGOnlineCheckDate, "{0:yyyy-MM-dd}");
            this.lblEmployee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            //this.lblSupplier.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERFULLNAME);
            this.lblFromInvoice.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheck.PRO_FromType);
            this.lblBusinessHours.DataBindings.Add("Text", this.DataSource, "BusinessHours." + Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
            this.lblWorkHouse.DataBindings.Add("Text", this.DataSource, "WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);

            this.xrSubreportDetail.ReportSource = new subReportDetail();
        }

        private void xrSubreportDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportDetail detail = this.xrSubreportDetail.ReportSource as subReportDetail;
            detail.PCPGOnlineCheckId = (this.GetCurrentRow() as Model.PCPGOnlineCheck).PCPGOnlineCheckId;
        }
    }
}
