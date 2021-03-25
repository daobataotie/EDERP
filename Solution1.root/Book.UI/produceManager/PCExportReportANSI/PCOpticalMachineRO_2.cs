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
    public partial class PCOpticalMachineRO_2 : DevExpress.XtraReports.UI.XtraReport
    {
        public PCOpticalMachineRO_2()
        {
            InitializeComponent();
        }

        public PCOpticalMachineRO_2(IList<Model.PCOpticalMachine> PCOpticalMachineList, Model.PCDataInput pcDataInput)
            : this()
        {
            this.TCGroup2.Visible = false;
            this.TCGroup3.Visible = false;
            this.ReportHeader.HeightF = 718;


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
                    this.lbl_Condition.Text = PCOpticalMachineList[0].Condition;
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
                    this.lbl_Condition2.Text = PCOpticalMachineList[1].Condition;
            }


            //第二組 3~4行数据
            if (PCOpticalMachineList != null && PCOpticalMachineList.Count > 2 && PCOpticalMachineList[2] != null)
            {
                this.TCLA3.Text = PCOpticalMachineList[2].LeftA.HasValue ? PCOpticalMachineList[2].LeftA.Value.ToString() : "";
                this.TCLC3.Text = PCOpticalMachineList[2].LeftC.HasValue ? PCOpticalMachineList[2].LeftC.Value.ToString("0.00") : "";
                this.TCLS3.Text = PCOpticalMachineList[2].LeftS.HasValue ? PCOpticalMachineList[2].LeftS.Value.ToString("0.00") : "";
                this.TCLLevelNum3.Text = PCOpticalMachineList[2].LeftLevelNum.HasValue ? PCOpticalMachineList[2].LeftLevelNum.Value.ToString("0.00") : "";
                this.TCLLevelJudge3.Text = PCOpticalMachineList[2].LeftLevelJudge;
                this.TCLVerticalNum3.Text = PCOpticalMachineList[2].LeftVerticalNum.HasValue ? PCOpticalMachineList[2].LeftVerticalNum.Value.ToString("0.00") : "";
                this.TCLVerticalJudge3.Text = PCOpticalMachineList[2].LeftVerticalJudge;

                this.TCRA3.Text = PCOpticalMachineList[2].RightA.HasValue ? PCOpticalMachineList[2].RightA.Value.ToString() : "";
                this.TCRC3.Text = PCOpticalMachineList[2].RightC.HasValue ? PCOpticalMachineList[2].RightC.Value.ToString("0.00") : "";
                this.TCRS3.Text = PCOpticalMachineList[2].RightS.HasValue ? PCOpticalMachineList[2].RightS.Value.ToString("0.00") : "";
                this.TCRLevelNum3.Text = PCOpticalMachineList[2].RightLevelNum.HasValue ? PCOpticalMachineList[2].RightLevelNum.Value.ToString("0.00") : "";
                this.TCRLevelJudge3.Text = PCOpticalMachineList[2].RightLevelJudge;
                this.TCRVerticalNum3.Text = PCOpticalMachineList[2].RightVerticalNum.HasValue ? PCOpticalMachineList[2].RightVerticalNum.Value.ToString("0.00") : "";
                this.TCRVerticalJudge3.Text = PCOpticalMachineList[2].RightVerticalJudge;

                if (!string.IsNullOrEmpty(PCOpticalMachineList[2].Condition))
                    this.lbl_Condition3.Text = PCOpticalMachineList[2].Condition;


                this.TCGroup2.Visible = true;
                this.ReportHeader.HeightF = 1191;
            }

            if (PCOpticalMachineList != null && PCOpticalMachineList.Count > 3 && PCOpticalMachineList[3] != null)
            {
                this.TCLA4.Text = PCOpticalMachineList[3].LeftA.HasValue ? PCOpticalMachineList[3].LeftA.Value.ToString() : "";
                this.TCLC4.Text = PCOpticalMachineList[3].LeftC.HasValue ? PCOpticalMachineList[3].LeftC.Value.ToString("0.00") : "";
                this.TCLS4.Text = PCOpticalMachineList[3].LeftS.HasValue ? PCOpticalMachineList[3].LeftS.Value.ToString("0.00") : "";
                this.TCLLevelNum4.Text = PCOpticalMachineList[3].LeftLevelNum.HasValue ? PCOpticalMachineList[3].LeftLevelNum.Value.ToString("0.00") : "";
                this.TCLLevelJudge4.Text = PCOpticalMachineList[3].LeftLevelJudge;
                this.TCLVerticalNum4.Text = PCOpticalMachineList[3].LeftVerticalNum.HasValue ? PCOpticalMachineList[3].LeftVerticalNum.Value.ToString("0.00") : "";
                this.TCLVerticalJudge4.Text = PCOpticalMachineList[3].LeftVerticalJudge;

                this.TCRA4.Text = PCOpticalMachineList[3].RightA.HasValue ? PCOpticalMachineList[3].RightA.Value.ToString() : "";
                this.TCRC4.Text = PCOpticalMachineList[3].RightC.HasValue ? PCOpticalMachineList[3].RightC.Value.ToString("0.00") : "";
                this.TCRS4.Text = PCOpticalMachineList[3].RightS.HasValue ? PCOpticalMachineList[3].RightS.Value.ToString("0.00") : "";
                this.TCRLevelNum4.Text = PCOpticalMachineList[3].RightLevelNum.HasValue ? PCOpticalMachineList[3].RightLevelNum.Value.ToString("0.00") : "";
                this.TCRLevelJudge4.Text = PCOpticalMachineList[3].RightLevelJudge;
                this.TCRVerticalNum4.Text = PCOpticalMachineList[3].RightVerticalNum.HasValue ? PCOpticalMachineList[3].RightVerticalNum.Value.ToString("0.00") : "";
                this.TCRVerticalJudge4.Text = PCOpticalMachineList[3].RightVerticalJudge;

                if (!string.IsNullOrEmpty(PCOpticalMachineList[3].Condition))
                    this.lbl_Condition4.Text = PCOpticalMachineList[3].Condition;
            }

            //第三組 5~6行数据
            if (PCOpticalMachineList != null && PCOpticalMachineList.Count > 4 && PCOpticalMachineList[4] != null)
            {
                this.TCLA5.Text = PCOpticalMachineList[4].LeftA.HasValue ? PCOpticalMachineList[4].LeftA.Value.ToString() : "";
                this.TCLC5.Text = PCOpticalMachineList[4].LeftC.HasValue ? PCOpticalMachineList[4].LeftC.Value.ToString("0.00") : "";
                this.TCLS5.Text = PCOpticalMachineList[4].LeftS.HasValue ? PCOpticalMachineList[4].LeftS.Value.ToString("0.00") : "";
                this.TCLLevelNum5.Text = PCOpticalMachineList[4].LeftLevelNum.HasValue ? PCOpticalMachineList[4].LeftLevelNum.Value.ToString("0.00") : "";
                this.TCLLevelJudge5.Text = PCOpticalMachineList[4].LeftLevelJudge;
                this.TCLVerticalNum5.Text = PCOpticalMachineList[4].LeftVerticalNum.HasValue ? PCOpticalMachineList[4].LeftVerticalNum.Value.ToString("0.00") : "";
                this.TCLVerticalJudge5.Text = PCOpticalMachineList[4].LeftVerticalJudge;

                this.TCRA5.Text = PCOpticalMachineList[4].RightA.HasValue ? PCOpticalMachineList[4].RightA.Value.ToString() : "";
                this.TCRC5.Text = PCOpticalMachineList[4].RightC.HasValue ? PCOpticalMachineList[4].RightC.Value.ToString("0.00") : "";
                this.TCRS5.Text = PCOpticalMachineList[4].RightS.HasValue ? PCOpticalMachineList[4].RightS.Value.ToString("0.00") : "";
                this.TCRLevelNum5.Text = PCOpticalMachineList[4].RightLevelNum.HasValue ? PCOpticalMachineList[4].RightLevelNum.Value.ToString("0.00") : "";
                this.TCRLevelJudge5.Text = PCOpticalMachineList[4].RightLevelJudge;
                this.TCRVerticalNum5.Text = PCOpticalMachineList[4].RightVerticalNum.HasValue ? PCOpticalMachineList[4].RightVerticalNum.Value.ToString("0.00") : "";
                this.TCRVerticalJudge5.Text = PCOpticalMachineList[4].RightVerticalJudge;

                if (!string.IsNullOrEmpty(PCOpticalMachineList[4].Condition))
                    this.lbl_Condition5.Text = PCOpticalMachineList[4].Condition;


                this.TCGroup3.Visible = true;
                this.ReportHeader.HeightF = 1672;
            }

            if (PCOpticalMachineList != null && PCOpticalMachineList.Count > 5 && PCOpticalMachineList[5] != null)
            {
                this.TCLA6.Text = PCOpticalMachineList[5].LeftA.HasValue ? PCOpticalMachineList[5].LeftA.Value.ToString() : "";
                this.TCLC6.Text = PCOpticalMachineList[5].LeftC.HasValue ? PCOpticalMachineList[5].LeftC.Value.ToString("0.00") : "";
                this.TCLS6.Text = PCOpticalMachineList[5].LeftS.HasValue ? PCOpticalMachineList[5].LeftS.Value.ToString("0.00") : "";
                this.TCLLevelNum6.Text = PCOpticalMachineList[5].LeftLevelNum.HasValue ? PCOpticalMachineList[5].LeftLevelNum.Value.ToString("0.00") : "";
                this.TCLLevelJudge6.Text = PCOpticalMachineList[5].LeftLevelJudge;
                this.TCLVerticalNum6.Text = PCOpticalMachineList[5].LeftVerticalNum.HasValue ? PCOpticalMachineList[5].LeftVerticalNum.Value.ToString("0.00") : "";
                this.TCLVerticalJudge6.Text = PCOpticalMachineList[5].LeftVerticalJudge;

                this.TCRA6.Text = PCOpticalMachineList[5].RightA.HasValue ? PCOpticalMachineList[5].RightA.Value.ToString() : "";
                this.TCRC6.Text = PCOpticalMachineList[5].RightC.HasValue ? PCOpticalMachineList[5].RightC.Value.ToString("0.00") : "";
                this.TCRS6.Text = PCOpticalMachineList[5].RightS.HasValue ? PCOpticalMachineList[5].RightS.Value.ToString("0.00") : "";
                this.TCRLevelNum6.Text = PCOpticalMachineList[5].RightLevelNum.HasValue ? PCOpticalMachineList[5].RightLevelNum.Value.ToString("0.00") : "";
                this.TCRLevelJudge6.Text = PCOpticalMachineList[5].RightLevelJudge;
                this.TCRVerticalNum6.Text = PCOpticalMachineList[5].RightVerticalNum.HasValue ? PCOpticalMachineList[5].RightVerticalNum.Value.ToString("0.00") : "";
                this.TCRVerticalJudge6.Text = PCOpticalMachineList[5].RightVerticalJudge;

                if (!string.IsNullOrEmpty(PCOpticalMachineList[5].Condition))
                    this.lbl_Condition6.Text = PCOpticalMachineList[5].Condition;
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
