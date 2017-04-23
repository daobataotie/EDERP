using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class PCHazeRO : DevExpress.XtraReports.UI.XtraReport
    {
        public PCHazeRO()
        {
            InitializeComponent();
        }

        public PCHazeRO(IList<Model.PCHaze> pcHazeList, Model.PCDataInput pcDataInput)
            : this()
        {
            this.TCDate.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTestQuantity.Text = pcHazeList.Count.ToString();
            this.TCEmployee.Text = pcDataInput.Employee3 == null ? "" : pcDataInput.Employee3.ToString();

            foreach (var item in pcHazeList)
            {
                item.HelpNoid = "Sample." + item.NoId;
                if (item.LeftHaze < 2 && item.RightHaze < 2)
                    item.Judge = "PASS";
                else
                    item.Judge = "FAIL";
            }
            this.DataSource = pcHazeList;

            this.TCSample.DataBindings.Add("Text", this.DataSource, "HelpNoid");
            this.TCLHaze.DataBindings.Add("Text", this.DataSource, Model.PCHaze.PRO_LeftHaze);
            this.TCRHaze.DataBindings.Add("Text", this.DataSource, Model.PCHaze.PRO_RightHaze);
            this.TCJudge.DataBindings.Add("Text", this.DataSource, Model.PCHaze.PRO_Judge);
        }
    }
}
