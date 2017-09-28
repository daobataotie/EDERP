using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class PCOpticalMachineRO : DevExpress.XtraReports.UI.XtraReport
    {
        public PCOpticalMachineRO()
        {
            InitializeComponent();
        }

        public PCOpticalMachineRO(IList<Model.PCOpticalMachine> PCOpticalMachineList, Model.PCDataInput pcDataInput)
            : this()
        {
            this.TCDate.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTestQuantity.Text = PCOpticalMachineList.Count.ToString();
            this.TCEmployee.Text = pcDataInput.Employee == null ? "" : pcDataInput.Employee.ToString();

            if (PCOpticalMachineList != null && PCOpticalMachineList.Count > 0 && PCOpticalMachineList[0] != null)
            {
                this.TCLA.Text = PCOpticalMachineList[0].LeftA.HasValue ? PCOpticalMachineList[0].LeftA.Value.ToString() : "";
                this.TCLC.Text = PCOpticalMachineList[0].LeftC.HasValue ? PCOpticalMachineList[0].LeftC.Value.ToString("0.00") : "";
                this.TCLS.Text = PCOpticalMachineList[0].LeftS.HasValue ? PCOpticalMachineList[0].LeftS.Value.ToString("0.00") : "";
                this.TCLLevelNum.Text = PCOpticalMachineList[0].LeftLevelNum.HasValue ? PCOpticalMachineList[0].LeftLevelNum.Value.ToString("0.00") : "";
                this.TCLLevelJudge.Text = PCOpticalMachineList[0].LeftLevelJudge;
                this.TCLVerticalNum.Text = PCOpticalMachineList[0].LeftVerticalNum.HasValue ? PCOpticalMachineList[0].LeftVerticalNum.Value.ToString("0.00") : "";
                this.TCLVerticalJudge.Text = PCOpticalMachineList[0].LeftVerticalJudge;

                this.TCRA.Text = PCOpticalMachineList[0].RightA.HasValue ? PCOpticalMachineList[0].RightA.Value.ToString() : "";
                this.TCRC.Text = PCOpticalMachineList[0].RightC.HasValue ? PCOpticalMachineList[0].RightC.Value.ToString("0.00") : "";
                this.TCRS.Text = PCOpticalMachineList[0].RightS.HasValue ? PCOpticalMachineList[0].RightS.Value.ToString("0.00") : "";
                this.TCRLevelNum.Text = PCOpticalMachineList[0].RightLevelNum.HasValue ? PCOpticalMachineList[0].RightLevelNum.Value.ToString("0.00") : "";
                this.TCRLevelJudge.Text = PCOpticalMachineList[0].RightLevelJudge;
                this.TCRVerticalNum.Text = PCOpticalMachineList[0].RightVerticalNum.HasValue ? PCOpticalMachineList[0].RightVerticalNum.Value.ToString("0.00") : "";
                this.TCRVerticalJudge.Text = PCOpticalMachineList[0].RightVerticalJudge;

                if (!string.IsNullOrEmpty(PCOpticalMachineList[0].Condition))
                    this.xrLabel4.Text = PCOpticalMachineList[0].Condition;
            }

            if (PCOpticalMachineList != null && PCOpticalMachineList.Count > 1 && PCOpticalMachineList[1] != null)
            {
                this.TCLA2.Text = PCOpticalMachineList[1].LeftA.HasValue ? PCOpticalMachineList[1].LeftA.Value.ToString() : "";
                this.TCLC2.Text = PCOpticalMachineList[1].LeftC.HasValue ? PCOpticalMachineList[1].LeftC.Value.ToString("0.00") : "";
                this.TCLS2.Text = PCOpticalMachineList[1].LeftS.HasValue ? PCOpticalMachineList[1].LeftS.Value.ToString("0.00") : "";
                this.TCLLevelNum2.Text = PCOpticalMachineList[1].LeftLevelNum.HasValue ? PCOpticalMachineList[1].LeftLevelNum.Value.ToString("0.00") : "";
                this.TCLLevelJudge2.Text = PCOpticalMachineList[1].LeftLevelJudge;
                this.TCLVerticalNum2.Text = PCOpticalMachineList[1].LeftVerticalNum.HasValue ? PCOpticalMachineList[1].LeftVerticalNum.Value.ToString("0.00") : "";
                this.TCLVerticalJudge2.Text = PCOpticalMachineList[1].LeftVerticalJudge;

                this.TCRA2.Text = PCOpticalMachineList[1].RightA.HasValue ? PCOpticalMachineList[1].RightA.Value.ToString() : "";
                this.TCRC2.Text = PCOpticalMachineList[1].RightC.HasValue ? PCOpticalMachineList[1].RightC.Value.ToString("0.00") : "";
                this.TCRS2.Text = PCOpticalMachineList[1].RightS.HasValue ? PCOpticalMachineList[1].RightS.Value.ToString("0.00") : "";
                this.TCRLevelNum2.Text = PCOpticalMachineList[1].RightLevelNum.HasValue ? PCOpticalMachineList[1].RightLevelNum.Value.ToString("0.00") : "";
                this.TCRLevelJudge2.Text = PCOpticalMachineList[1].RightLevelJudge;
                this.TCRVerticalNum2.Text = PCOpticalMachineList[1].RightVerticalNum.HasValue ? PCOpticalMachineList[1].RightVerticalNum.Value.ToString("0.00") : "";
                this.TCRVerticalJudge2.Text = PCOpticalMachineList[1].RightVerticalJudge;

                if (!string.IsNullOrEmpty(PCOpticalMachineList[1].Condition))
                    this.xrLabel6.Text = PCOpticalMachineList[1].Condition;
            }
            //this.DataSource = PCOpticalMachineList;
            //this.TCLA.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftA);
            //this.TCLC.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftC);
            //this.TCLS.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftS);
            //this.TCLLevelNum.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftLevelNum);
            //this.TCLLevelJudge.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftLevelJudge);
            //this.TCLVerticalNum.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftVerticalNum);
            //this.TCLVerticalJudge.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_LeftVerticalJudge);

            //this.TCRA.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightA);
            //this.TCRC.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightC);
            //this.TCRS.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightS);
            //this.TCRLevelNum.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightLevelNum);
            //this.TCRLevelJudge.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightLevelJudge);
            //this.TCRVerticalNum.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightVerticalNum);
            //this.TCRVerticalJudge.DataBindings.Add("Text", this.DataSource, Model.PCOpticalMachine.PRO_RightVerticalJudge);
        }
    }
}
