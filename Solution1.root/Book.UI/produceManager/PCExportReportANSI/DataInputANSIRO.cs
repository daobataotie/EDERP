using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DataInputANSIRO : DevExpress.XtraReports.UI.XtraReport
    {
        public DataInputANSIRO()
        {
            InitializeComponent();
        }

        public DataInputANSIRO(Model.PCDataInput pcDataInput, Model.PCExportReportANSI pcExportReportANSI, int tag)
            : this()
        {
            if (pcDataInput == null)
                return;

            this.TCTSData.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTSQuantity.Text = pcDataInput.PCPerspectiveList.Count.ToString();
            this.TCTSEmployee.Text = pcDataInput.Employee3 == null ? "" : pcDataInput.Employee3.ToString();

            if (pcExportReportANSI != null)
                this.xrSubreportANSI.ReportSource = new RO(pcExportReportANSI, tag);
            this.xrSubreportProductTest.ReportSource = new ProductTestRO(pcDataInput);

            if (pcDataInput.PCOpticalMachineList != null && pcDataInput.PCOpticalMachineList.Count != 0)
                this.xrSubreportPCOpticalMachine.ReportSource = new PCOpticalMachineRO(pcDataInput.PCOpticalMachineList, pcDataInput);

            if (pcDataInput.PCHazeList != null && pcDataInput.PCHazeList.Count != 0)
                this.xrSubreportPCHaze.ReportSource = new PCHazeRO(pcDataInput.PCHazeList, pcDataInput);
        }

        private void DataInputANSIRO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //RO ro = this.xrSubreportANSI.ReportSource as RO;
            //ro.ANSI = pcExportReportANSI;
        }

    }
}
