using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCExportReportANSI
{
    /// <summary>
    /// 光学机
    /// </summary>
    public partial class PCOpticalMachineRO_2_Sub : DevExpress.XtraReports.UI.XtraReport
    {
        public PCOpticalMachineRO_2_Sub()
        {
            InitializeComponent();
        }

        public PCOpticalMachineRO_2_Sub(IList<Model.PCOpticalMachine> PCOpticalMachineList)
            : this()
        {
            this.DataSource = PCOpticalMachineList;

            this.TCLA.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftA);
            this.TCLC.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftC);
            this.TCLS.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftS);
            this.TCLLevelNum.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftLevelNum, "{0:0.00}");
            this.TCLLevelJudge.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftLevelJudge);
            this.TCLVerticalNum.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftVerticalNum, "{0:0.00}");
            this.TCLVerticalJudge.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftVerticalJudge);

            this.TCRA.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightA);
            this.TCRC.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightC);
            this.TCRS.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightS);
            this.TCRLevelNum.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightLevelNum, "{0:0.00}");
            this.TCRLevelJudge.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightLevelJudge);
            this.TCRVerticalNum.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightVerticalNum, "{0:0.00}");
            this.TCRVerticalJudge.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightVerticalJudge);

            this.lbl_Condition.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_Condition);
        }

    }
}
