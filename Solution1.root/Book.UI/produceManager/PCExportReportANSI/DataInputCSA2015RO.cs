using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DataInputCSA2015RO : DevExpress.XtraReports.UI.XtraReport
    {
        public DataInputCSA2015RO()
        {
            InitializeComponent();
        }

        public DataInputCSA2015RO(Model.PCDataInput pcDataInput, Model.PCExportReportANSI ANSI, Model.PCExportReportANSI CSA, int tag)
            : this()
        {
            if (pcDataInput == null)
                return;

            this.TCTSData.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTSQuantity.Text = pcDataInput.PCPerspectiveList.Count.ToString();
            this.TCTSEmployee.Text = pcDataInput.Employee3 == null ? "" : pcDataInput.Employee3.ToString();

            string customer = string.Empty;
            if (ANSI != null)
            {
                this.xrSubreportANSI.ReportSource = new ANSI2015RO(ANSI, tag);
                customer = ANSI.Customer.CustomerName;
            }
            if (CSA != null)
            {
                this.xrSubreportCSA.ReportSource = new CSARO(CSA, tag);
                customer = CSA.Customer.CustomerName;
            }
            if (string.IsNullOrEmpty(customer))
                customer = pcDataInput.CustomerShortName;
            this.xrSubreportProductTest.ReportSource = new ProductTestRO(pcDataInput, customer);

            if (pcDataInput.PCOpticalMachineList != null && pcDataInput.PCOpticalMachineList.Count != 0)
                this.xrSubreportPCOpticalMachine.ReportSource = new PCOpticalMachineRO(pcDataInput.PCOpticalMachineList, pcDataInput);

            if (pcDataInput.PCHazeList != null && pcDataInput.PCHazeList.Count != 0)
                this.xrSubreportPCHaze.ReportSource = new PCHazeRO(pcDataInput.PCHazeList, pcDataInput);
        }
    }
}
