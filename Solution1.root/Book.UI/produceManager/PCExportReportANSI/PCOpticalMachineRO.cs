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

            if (PCOpticalMachineList[0] != null)
            {
                this.TCLA.Text = PCOpticalMachineList[0].LeftA.HasValue ? PCOpticalMachineList[0].LeftA.ToString() : "";
                this.TCLC.Text = PCOpticalMachineList[0].LeftC.HasValue ? PCOpticalMachineList[0].LeftC.ToString() : "";
                this.TCLS.Text = PCOpticalMachineList[0].LeftS.HasValue ? PCOpticalMachineList[0].LeftS.ToString() : "";
                this.TCLLevelNum.Text = PCOpticalMachineList[0].LeftLevelNum.HasValue ? PCOpticalMachineList[0].LeftLevelNum.ToString() : "";
                this.TCLLevelJudge.Text = PCOpticalMachineList[0].LeftLevelJudge;
                this.TCLVerticalNum.Text = PCOpticalMachineList[0].LeftVerticalNum.HasValue ? PCOpticalMachineList[0].LeftVerticalNum.ToString() : "";
                this.TCLVerticalJudge.Text = PCOpticalMachineList[0].LeftVerticalJudge;

                this.TCRA.Text = PCOpticalMachineList[0].RightA.HasValue ? PCOpticalMachineList[0].RightA.ToString() : "";
                this.TCRC.Text = PCOpticalMachineList[0].RightC.HasValue ? PCOpticalMachineList[0].RightC.ToString() : "";
                this.TCRS.Text = PCOpticalMachineList[0].RightS.HasValue ? PCOpticalMachineList[0].RightS.ToString() : "";
                this.TCRLevelNum.Text = PCOpticalMachineList[0].RightLevelNum.HasValue ? PCOpticalMachineList[0].RightLevelNum.ToString() : "";
                this.TCRLevelJudge.Text = PCOpticalMachineList[0].RightLevelJudge;
                this.TCRVerticalNum.Text = PCOpticalMachineList[0].RightVerticalNum.HasValue ? PCOpticalMachineList[0].RightVerticalNum.ToString() : "";
                this.TCRVerticalJudge.Text = PCOpticalMachineList[0].RightVerticalJudge;
            }

            if (PCOpticalMachineList[1] != null)
            {
                this.TCLA2.Text = PCOpticalMachineList[1].LeftA.HasValue ? PCOpticalMachineList[1].LeftA.ToString() : "";
                this.TCLC2.Text = PCOpticalMachineList[1].LeftC.HasValue ? PCOpticalMachineList[1].LeftC.ToString() : "";
                this.TCLS2.Text = PCOpticalMachineList[1].LeftS.HasValue ? PCOpticalMachineList[1].LeftS.ToString() : "";
                this.TCLLevelNum2.Text = PCOpticalMachineList[1].LeftLevelNum.HasValue ? PCOpticalMachineList[1].LeftLevelNum.ToString() : "";
                this.TCLLevelJudge2.Text = PCOpticalMachineList[1].LeftLevelJudge;
                this.TCLVerticalNum2.Text = PCOpticalMachineList[1].LeftVerticalNum.HasValue ? PCOpticalMachineList[1].LeftVerticalNum.ToString() : "";
                this.TCLVerticalJudge2.Text = PCOpticalMachineList[1].LeftVerticalJudge;

                this.TCRA2.Text = PCOpticalMachineList[1].RightA.HasValue ? PCOpticalMachineList[1].RightA.ToString() : "";
                this.TCRC2.Text = PCOpticalMachineList[1].RightC.HasValue ? PCOpticalMachineList[1].RightC.ToString() : "";
                this.TCRS2.Text = PCOpticalMachineList[1].RightS.HasValue ? PCOpticalMachineList[1].RightS.ToString() : "";
                this.TCRLevelNum2.Text = PCOpticalMachineList[1].RightLevelNum.HasValue ? PCOpticalMachineList[1].RightLevelNum.ToString() : "";
                this.TCRLevelJudge2.Text = PCOpticalMachineList[1].RightLevelJudge;
                this.TCRVerticalNum2.Text = PCOpticalMachineList[1].RightVerticalNum.HasValue ? PCOpticalMachineList[1].RightVerticalNum.ToString() : "";
                this.TCRVerticalJudge2.Text = PCOpticalMachineList[1].RightVerticalJudge;
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
