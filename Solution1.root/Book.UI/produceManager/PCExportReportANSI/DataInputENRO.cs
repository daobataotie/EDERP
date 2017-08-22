using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DataInputENRO : DevExpress.XtraReports.UI.XtraReport
    {
        public DataInputENRO()
        {
            InitializeComponent();
        }

        public DataInputENRO(Model.PCDataInput pcDataInput, Model.PCExportReportANSI EN,int tag)
            : this()
        {
            if (pcDataInput == null)
                return;

            this.TCTSData.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTSQuantity.Text = pcDataInput.PCPerspectiveList.Count.ToString();
            this.TCTSEmployee.Text = pcDataInput.Employee3 == null ? "" : pcDataInput.Employee3.ToString();

            if (EN != null)
                this.xrSubreportEN.ReportSource = new CEENRO (EN, tag);
            this.xrSubreportProductTest.ReportSource = new ProductTestRO(pcDataInput);
            this.xrSubreportPCOpticalMachine.ReportSource = new PCOpticalMachineRO(pcDataInput.PCOpticalMachineList, pcDataInput);
        }
    }
}
