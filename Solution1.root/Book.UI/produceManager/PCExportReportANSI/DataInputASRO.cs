using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DataInputASRO : DevExpress.XtraReports.UI.XtraReport
    {
        public DataInputASRO()
        {
            InitializeComponent();
        }

        public DataInputASRO(Model.PCDataInput pcDataInput, Model.PCExportReportANSI AS)
            : this()
        {
            if (pcDataInput == null)
                return;

            this.TCTSData.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTSQuantity.Text = pcDataInput.PCPerspectiveList.Count.ToString();
            this.TCTSEmployee.Text = pcDataInput.Employee2 == null ? "" : pcDataInput.Employee2.ToString();

            if (AS != null)
                this.xrSubreportAS.ReportSource = new ASRO (AS, 0,false);
            this.xrSubreportProductTest.ReportSource = new ProductTestRO(pcDataInput);
            this.xrSubreportPCOpticalMachine.ReportSource = new PCOpticalMachineRO(pcDataInput.PCOpticalMachineList, pcDataInput);
            this.xrSubreportPCHaze.ReportSource = new PCHazeRO(pcDataInput.PCHazeList, pcDataInput);
        }
    }
}
