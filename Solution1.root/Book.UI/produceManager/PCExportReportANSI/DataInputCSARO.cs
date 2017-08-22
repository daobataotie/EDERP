using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DataInputCSARO : DevExpress.XtraReports.UI.XtraReport
    {
        public DataInputCSARO()
        {
            InitializeComponent();
        }

        public DataInputCSARO(Model.PCDataInput pcDataInput, Model.PCExportReportANSI ANSI, Model.PCExportReportANSI CSA,int tag)
            : this()
        {
            if (pcDataInput == null)
                return;

            this.TCTSData.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTSQuantity.Text = pcDataInput.PCPerspectiveList.Count.ToString();
            this.TCTSEmployee.Text = pcDataInput.Employee3 == null ? "" : pcDataInput.Employee3.ToString();

            if (ANSI != null)
                this.xrSubreportANSI.ReportSource = new RO(ANSI, tag);
            if (CSA != null)
                this.xrSubreportCSA.ReportSource = new CSARO(CSA, tag);
            this.xrSubreportProductTest.ReportSource = new ProductTestRO(pcDataInput);
            this.xrSubreportPCOpticalMachine.ReportSource = new PCOpticalMachineRO(pcDataInput.PCOpticalMachineList, pcDataInput);
            this.xrSubreportPCHaze.ReportSource = new PCHazeRO(pcDataInput.PCHazeList, pcDataInput);
        }
    }
}
