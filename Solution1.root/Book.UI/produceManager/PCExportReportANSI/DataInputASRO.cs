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

        public DataInputASRO(Model.PCDataInput pcDataInput, Model.PCExportReportANSI AS, int tag, bool isJihe)
            : this()
        {
            if (pcDataInput == null)
                return;

            if (!isJihe)
            {
                this.xrSubreportPCPerspective.ReportSource = new PCPerspective(pcDataInput);
            }

            string customer = string.Empty;
            if (AS != null)
            {
                this.xrSubreportAS.ReportSource = new ASRO(AS, tag);
                customer = AS.Customer.CustomerName;
            }
            if (string.IsNullOrEmpty(customer))
                customer = pcDataInput.CustomerShortName;
            this.xrSubreportProductTest.ReportSource = new ProductTestRO(pcDataInput, customer);

            //if (pcDataInput.PCOpticalMachineList != null && pcDataInput.PCOpticalMachineList.Count != 0)
            //    this.xrSubreportPCOpticalMachine.ReportSource = new PCOpticalMachineRO(pcDataInput.PCOpticalMachineList, pcDataInput);
            if (pcDataInput.PCOpticalMachineList != null)
            {
                if (pcDataInput.PCOpticalMachineList.Count <= 2)
                    this.xrSubreportPCOpticalMachine.ReportSource = new PCOpticalMachineRO(pcDataInput.PCOpticalMachineList, pcDataInput);
                else
                    this.xrSubreportPCOpticalMachine.ReportSource = new PCOpticalMachineRO_2(pcDataInput.PCOpticalMachineList, pcDataInput);
            }

            if (pcDataInput.PCHazeList != null && pcDataInput.PCHazeList.Count != 0)
                this.xrSubreportPCHaze.ReportSource = new PCHazeRO(pcDataInput.PCHazeList, pcDataInput);

            if (pcDataInput.PCDefinitionList != null && pcDataInput.PCDefinitionList.Count != 0)
                this.subReport_PCDefinition.ReportSource = new PCDefinitionRO(pcDataInput.PCDefinitionList);
        }
    }
}
