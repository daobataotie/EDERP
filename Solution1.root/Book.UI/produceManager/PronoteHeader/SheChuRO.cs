using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PronoteHeader
{
    public partial class SheChuRO : DevExpress.XtraReports.UI.XtraReport
    {
        public SheChuRO(IList<Model.PronoteHeader> list)
        {
            InitializeComponent();

            this.DataSource = list;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = "Éä³ö¹¤×÷†Î";
            this.lblMachine.Text = list[0].PronoteMachineId;
            this.xrSubreport1.ReportSource = new SheChuRO1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SheChuRO1 ro1 = this.xrSubreport1.ReportSource as SheChuRO1;
            ro1.PronoteHeaderId = (this.GetCurrentRow() as Model.PronoteHeader).PronoteHeaderID;
        }

    }
}
