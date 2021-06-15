using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class PCPerspective : DevExpress.XtraReports.UI.XtraReport
    {
        /// <summary>
        /// 透視率
        /// </summary>
        /// <param name="pcDataInput"></param>
        public PCPerspective(Model.PCDataInput pcDataInput)
        {
            InitializeComponent();
            

            this.TCTSData.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTSQuantity.Text = pcDataInput.PCPerspectiveList.Count.ToString();
            this.TCTSEmployee.Text = pcDataInput.Employee3 == null ? "" : pcDataInput.Employee3.ToString();
        }

    }
}
