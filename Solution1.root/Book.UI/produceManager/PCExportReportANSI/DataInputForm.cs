using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DataInputForm : Settings.BasicData.BaseEditForm
    {
        Model.PCDataInput _PCDataInput;
        BL.PCDataInputManager pcDataInputManager = new Book.BL.PCDataInputManager();
        BL.PCExportReportANSIManager pCExportReportANSIManager = new Book.BL.PCExportReportANSIManager();
        int LastFlag = 0;
        public DataInputForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.PCDataInput.PRO_CheckStandard, new AA(Properties.Resources.CheckStandard, this.cob_CheckStandard));
            this.action = "view";
            //this.newChooseContorlEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.ncc_Tester1.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.ncc_Tester2.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.ncc_Tester3.Choose = new Settings.BasicData.Employees.ChooseEmployee();
        }

        public DataInputForm(string PCDataInputId)
            : this()
        {
            this._PCDataInput = this.pcDataInputManager.GetDetails(PCDataInputId);
            this.action = "view";
            this.LastFlag = 1;
        }

        public DataInputForm(Model.PCDataInput PCDataInput)
            : this()
        {
            if (PCDataInput != null)
                this._PCDataInput = this.pcDataInputManager.GetDetails(PCDataInput.PCDataInputId);
            this.action = "view";
            LastFlag = 1;
        }

        public DataInputForm(Model.PCDataInput PCDataInput, string action)
            : this()
        {
            if (PCDataInput != null)
                this._PCDataInput = this.pcDataInputManager.GetDetails(PCDataInput.PCDataInputId);
            this.action = action;
            LastFlag = 1;
        }

        protected override bool HasRows()
        {
            return this.pcDataInputManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.pcDataInputManager.HasRowsAfter(this._PCDataInput);
        }

        protected override bool HasRowsPrev()
        {
            return this.pcDataInputManager.HasRowsBefore(this._PCDataInput);
        }

        protected override void MoveNext()
        {
            Model.PCDataInput pcDataInput = this.pcDataInputManager.GetNext(this._PCDataInput);
            if (pcDataInput == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCDataInput = this.pcDataInputManager.Get(pcDataInput.PCDataInputId);
        }

        protected override void MovePrev()
        {
            Model.PCDataInput pcDataInput = this.pcDataInputManager.GetPrev(this._PCDataInput);
            if (pcDataInput == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCDataInput = this.pcDataInputManager.Get(pcDataInput.PCDataInputId);
        }

        protected override void MoveFirst()
        {
            this._PCDataInput = this.pcDataInputManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._PCDataInput = this.pcDataInputManager.GetLast();
        }

        protected override void AddNew()
        {
            this._PCDataInput = new Book.Model.PCDataInput();
            this._PCDataInput.PCDataInputId = Guid.NewGuid().ToString();
            this._PCDataInput.PCDataInputDate = DateTime.Now;
            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._PCDataInput == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._PCDataInput = this.pcDataInputManager.GetDetails(this._PCDataInput.PCDataInputId);
            }
            this.txt_PronoteHeaderId.Text = this._PCDataInput.PronoteHeaderId;
            this.txt_InvoiceCusId.Text = this._PCDataInput.InvoiceCusId;
            this.date_PCDataInputDate.EditValue = this._PCDataInput.PCDataInputDate;
            this.spe_TestQuantity.EditValue = this._PCDataInput.TestQuantity;
            this.cob_CheckStandard.EditValue = this._PCDataInput.CheckStandard;
            this.txt_ProductName.Text = this._PCDataInput.Product == null ? "" : this._PCDataInput.Product.ToString();
            this.txt_Customer.Text = this._PCDataInput.CustomerShortName;
            //this.newChooseContorlEmployee.EditValue = this._PCDataInput.Employee;
            this.ncc_Tester1.EditValue = this._PCDataInput.Employee;
            this.ncc_Tester2.EditValue = this._PCDataInput.Employee2;
            this.ncc_Tester3.EditValue = this._PCDataInput.Employee3;

            this.bindingSourcePCOpticalMachine.DataSource = this._PCDataInput.PCOpticalMachineList;
            this.bindingSourcePCLaserMachine.DataSource = this._PCDataInput.PCLaserMachineList;
            this.bindingSourcePCDefinition.DataSource = this._PCDataInput.PCDefinitionList;
            this.bindingSourcePCPerspective.DataSource = this._PCDataInput.PCPerspectiveList;
            this.bindingSourcePCHaze.DataSource = this._PCDataInput.PCHazeList;
            this.bindingSourcePCEuropeOptical.DataSource = this._PCDataInput.PCEuropeOpticalList;

            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.gridView2.OptionsBehavior.Editable = false;
                    this.gridView3.OptionsBehavior.Editable = false;
                    this.gridView4.OptionsBehavior.Editable = false;
                    this.gridView5.OptionsBehavior.Editable = false;
                    this.gridView6.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    this.gridView3.OptionsBehavior.Editable = true;
                    this.gridView4.OptionsBehavior.Editable = true;
                    this.gridView5.OptionsBehavior.Editable = true;
                    this.gridView6.OptionsBehavior.Editable = true;
                    break;
            }

            this.txt_PronoteHeaderId.Properties.ReadOnly = true;
            this.txt_InvoiceCusId.Properties.ReadOnly = true;
            this.txt_Customer.Properties.ReadOnly = true;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();
            this.gridView2.PostEditor();
            this.gridView2.UpdateCurrentRow();
            this.gridView3.PostEditor();
            this.gridView3.UpdateCurrentRow();
            this.gridView4.PostEditor();
            this.gridView4.UpdateCurrentRow();
            this.gridView5.PostEditor();
            this.gridView5.UpdateCurrentRow();
            this.gridView6.PostEditor();
            this.gridView6.UpdateCurrentRow();

            this._PCDataInput.PCDataInputDate = this.date_PCDataInputDate.DateTime;
            this._PCDataInput.PronoteHeaderId = this.txt_PronoteHeaderId.Text;
            this._PCDataInput.InvoiceCusId = this.txt_InvoiceCusId.Text;
            this._PCDataInput.TestQuantity = this.spe_TestQuantity.Value;
            this._PCDataInput.CheckStandard = this.cob_CheckStandard.Text;
            this._PCDataInput.CustomerShortName = this.txt_Customer.Text;
            //this._PCDataInput.EmployeeId = this.newChooseContorlEmployee.EditValue == null ? null : (this.newChooseContorlEmployee.EditValue as Model.Employee).EmployeeId;
            this._PCDataInput.EmployeeId = this.ncc_Tester1.EditValue == null ? null : (this.ncc_Tester1.EditValue as Model.Employee).EmployeeId;
            this._PCDataInput.EmployeeId2 = this.ncc_Tester2.EditValue == null ? null : (this.ncc_Tester2.EditValue as Model.Employee).EmployeeId;
            this._PCDataInput.EmployeeId3 = this.ncc_Tester3.EditValue == null ? null : (this.ncc_Tester3.EditValue as Model.Employee).EmployeeId;

            switch (this.action)
            {
                case "insert":
                    this.pcDataInputManager.Insert(this._PCDataInput);
                    break;
                case "update":
                    this.pcDataInputManager.Update(this._PCDataInput);
                    break;
            }
            this.txt_PronoteHeaderId.Properties.ReadOnly = true;
            this.txt_InvoiceCusId.Properties.ReadOnly = true;
        }

        protected override void Delete()
        {
            if (this._PCDataInput == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.pcDataInputManager.Delete(this._PCDataInput.PCDataInputId);
            this._PCDataInput = this.pcDataInputManager.GetNext(this._PCDataInput);
            if (this._PCDataInput == null)
            {
                this._PCDataInput = this.pcDataInputManager.GetLast();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return base.GetReport();
        }

        //光学机
        private void btn_PCOpticalMachineADD_Click(object sender, EventArgs e)
        {
            Model.PCOpticalMachine model = new Book.Model.PCOpticalMachine();
            model.PCOpticalMachineId = Guid.NewGuid().ToString();
            this._PCDataInput.PCOpticalMachineList.Add(model);
            this.gridControl1.RefreshDataSource();
        }

        private void btn_PCOpticalMachineRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourcePCOpticalMachine.Current != null)
            {
                this._PCDataInput.PCOpticalMachineList.Remove(this.bindingSourcePCOpticalMachine.Current as Model.PCOpticalMachine);
                this.gridControl1.RefreshDataSource();
            }
        }

        //雷射仪
        private void btn_PCLaserMachineADD_Click(object sender, EventArgs e)
        {
            Model.PCLaserMachine model = new Book.Model.PCLaserMachine();
            model.PCLaserMachineId = Guid.NewGuid().ToString();
            this._PCDataInput.PCLaserMachineList.Add(model);
            this.gridControl2.RefreshDataSource();
        }

        private void btn_PCLaserMachineRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourcePCLaserMachine.Current != null)
            {
                this._PCDataInput.PCLaserMachineList.Remove(this.bindingSourcePCLaserMachine.Current as Model.PCLaserMachine);
                this.gridControl2.RefreshDataSource();
            }
        }

        //清晰度
        private void btn_PCDefinitionADD_Click(object sender, EventArgs e)
        {
            Model.PCDefinition model = new Book.Model.PCDefinition();
            model.PCDefinitionId = Guid.NewGuid().ToString();
            this._PCDataInput.PCDefinitionList.Add(model);
            this.gridControl3.RefreshDataSource();
        }

        private void btn_PCDefinitionRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourcePCDefinition.Current != null)
            {
                this._PCDataInput.PCDefinitionList.Remove(this.bindingSourcePCDefinition.Current as Model.PCDefinition);
                this.gridControl3.RefreshDataSource();
            }
        }

        //透视率
        private void btn_PCPerspectiveADD_Click(object sender, EventArgs e)
        {
            Model.PCPerspective model = new Book.Model.PCPerspective();
            model.PCPerspectiveId = Guid.NewGuid().ToString();
            this._PCDataInput.PCPerspectiveList.Add(model);
            this.gridControl4.RefreshDataSource();
        }

        private void btn_PCPerspectiveRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourcePCPerspective.Current != null)
            {
                this._PCDataInput.PCPerspectiveList.Remove(this.bindingSourcePCPerspective.Current as Model.PCPerspective);
                this.gridControl4.RefreshDataSource();
            }
        }

        //雾度
        private void btn_PCHazeADD_Click(object sender, EventArgs e)
        {
            Model.PCHaze model = new Book.Model.PCHaze();
            model.PCHazeId = Guid.NewGuid().ToString();
            this._PCDataInput.PCHazeList.Add(model);
            this.gridControl5.RefreshDataSource();
        }

        private void btn_PCHazeRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourcePCHaze.Current != null)
            {
                this._PCDataInput.PCHazeList.Remove(this.bindingSourcePCHaze.Current as Model.PCHaze);
                this.gridControl5.RefreshDataSource();
            }
        }

        //欧规光学测试
        private void btn_PCEuropeOpticalADD_Click(object sender, EventArgs e)
        {
            Model.PCEuropeOptical model = new Book.Model.PCEuropeOptical();
            model.PCEuropeOpticalId = Guid.NewGuid().ToString();
            this._PCDataInput.PCEuropeOpticalList.Add(model);
            this.gridControl6.RefreshDataSource();
        }

        private void btn_PCEuropeOpticalRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourcePCEuropeOptical.Current != null)
            {
                this._PCDataInput.PCEuropeOpticalList.Remove(this.bindingSourcePCEuropeOptical.Current as Model.PCEuropeOptical);
                this.gridControl6.RefreshDataSource();
            }
        }

        //选择加工单
        private void barPronoteHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(0, true);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader pronoteHeader = pronoForm.SelectItem;
                if (pronoteHeader != null)
                {
                    this.txt_PronoteHeaderId.Text = pronoteHeader.PronoteHeaderID;
                    this.txt_InvoiceCusId.Text = pronoteHeader.CustomerInvoiceXOId;
                    this._PCDataInput.ProductId = pronoteHeader.ProductId;
                    this.txt_ProductName.Text = pronoteHeader.ProductName;
                    this.txt_Customer.Text = pronoteHeader.CustomerShortName;
                    this.cob_CheckStandard.EditValue = pronoteHeader.CustomerCheckStandard;

                    //光学机内容拉取该加工单对应的 组装成品检验单的光学测试
                    Model.PCFinishCheck pcFinishCheck = (new BL.PCFinishCheckManager()).SelectByPronoteHeader(pronoteHeader.PronoteHeaderID);
                    if (pcFinishCheck != null)
                    {
                        this.ncc_Tester1.EditValue = pcFinishCheck.Employee0;
                        this.spe_TestQuantity.EditValue = pcFinishCheck.PCFinishCheckCount;

                        IList<Model.OpticsTest> opticsTestList = new BL.OpticsTestManager().FSelect(pcFinishCheck.PCFinishCheckID);
                        foreach (var opticsTest in opticsTestList)
                        {
                            Model.PCOpticalMachine pCOpticalMachine = new Book.Model.PCOpticalMachine();
                            pCOpticalMachine.PCOpticalMachineId = Guid.NewGuid().ToString();
                            pCOpticalMachine.LeftA = Convert.ToDecimal(opticsTest.LattrA);
                            pCOpticalMachine.LeftC = Convert.ToDecimal(opticsTest.LattrC);
                            pCOpticalMachine.LeftS = Convert.ToDecimal(opticsTest.LattrS);
                            pCOpticalMachine.LeftLevelNum = Convert.ToDecimal(opticsTest.LinPSM);
                            pCOpticalMachine.LeftLevelJudge = opticsTest.LeftLevelJudge;
                            pCOpticalMachine.LeftVerticalNum = Convert.ToDecimal(opticsTest.LupPSM);
                            pCOpticalMachine.LeftVerticalJudge = opticsTest.LeftVerticalJudge;

                            pCOpticalMachine.RightA = Convert.ToDecimal(opticsTest.RattrA);
                            pCOpticalMachine.RightC = Convert.ToDecimal(opticsTest.RattrC);
                            pCOpticalMachine.RightS = Convert.ToDecimal(opticsTest.RattrS);
                            pCOpticalMachine.RightLevelNum = Convert.ToDecimal(opticsTest.RinPSM);
                            pCOpticalMachine.RightLevelJudge = opticsTest.RightLevelJudge;
                            pCOpticalMachine.RightVerticalNum = Convert.ToDecimal(opticsTest.RupPSM);
                            pCOpticalMachine.RightVerticalJudge = opticsTest.RightVerticalJudge;

                            this._PCDataInput.PCOpticalMachineList.Add(pCOpticalMachine);
                            this.gridControl1.RefreshDataSource();
                        }
                        //if (opticsTest != null)
                        //{
                        //    Model.PCOpticalMachine pCOpticalMachine = new Book.Model.PCOpticalMachine();
                        //    pCOpticalMachine.PCOpticalMachineId = Guid.NewGuid().ToString();
                        //    pCOpticalMachine.LeftA = Convert.ToDecimal(opticsTest.LattrA);
                        //    pCOpticalMachine.LeftC = Convert.ToDecimal(opticsTest.LattrC);
                        //    pCOpticalMachine.LeftS = Convert.ToDecimal(opticsTest.LattrS);
                        //    pCOpticalMachine.LeftLevelNum = Convert.ToDecimal(opticsTest.LinPSM);
                        //    pCOpticalMachine.LeftLevelJudge = opticsTest.LeftLevelJudge;
                        //    pCOpticalMachine.LeftVerticalNum = Convert.ToDecimal(opticsTest.LupPSM);
                        //    pCOpticalMachine.LeftVerticalJudge = opticsTest.LeftVerticalJudge;

                        //    pCOpticalMachine.RightA = Convert.ToDecimal(opticsTest.RattrA);
                        //    pCOpticalMachine.RightC = Convert.ToDecimal(opticsTest.RattrC);
                        //    pCOpticalMachine.RightS = Convert.ToDecimal(opticsTest.RattrS);
                        //    pCOpticalMachine.RightLevelNum = Convert.ToDecimal(opticsTest.RinPSM);
                        //    pCOpticalMachine.RightLevelJudge = opticsTest.RightLevelJudge;
                        //    pCOpticalMachine.RightVerticalNum = Convert.ToDecimal(opticsTest.RupPSM);
                        //    pCOpticalMachine.RightVerticalJudge = opticsTest.RightVerticalJudge;

                        //    this._PCDataInput.PCOpticalMachineList.Add(pCOpticalMachine);
                        //    this.gridControl1.RefreshDataSource();
                        //}
                    }
                }
            }
        }

        //生成对应安规
        private void barMadeReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (this._PCDataInput.CheckStandard)
            {
                case "ANSI":
                    Model.PCExportReportANSI ansi1 = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "ANSI");
                    DataInputANSIRO ansiro = new DataInputANSIRO(this._PCDataInput, ansi1);
                    ansiro.ShowPreviewDialog();
                    break;
                case "ANSI/CSA":
                    Model.PCExportReportANSI ansi2 = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "ANSI");
                    Model.PCExportReportANSI csa2 = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "CSA");
                    DataInputCSARO csaro = new DataInputCSARO(this._PCDataInput, ansi2, csa2);
                    csaro.ShowPreviewDialog();
                    break;
                case "EN":
                    Model.PCExportReportANSI en = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "CEEN");
                    DataInputENRO enro = new DataInputENRO(this._PCDataInput, en);
                    enro.ShowPreviewDialog();
                    break;
                case "AS/NZS":
                    Model.PCExportReportANSI aso = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "AS");
                    DataInputASRO asro = new DataInputASRO(this._PCDataInput, aso);
                    asro.ShowPreviewDialog();
                    break;

                    //2017年4月23日17:20:39 添加
                case "ANSI2015":
                    Model.PCExportReportANSI ansi20151 = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "ANSI2015");
                    DataInputANSI2015RO ansi2015ro = new DataInputANSI2015RO(this._PCDataInput, ansi20151);
                    ansi2015ro.ShowPreviewDialog();
                    break;
                case "ANSI2015/CSA2015":
                    Model.PCExportReportANSI ansi20152 = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "ANSI2015");
                    Model.PCExportReportANSI csa20152 = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "CSA");
                    DataInputCSA2015RO csa2015ro = new DataInputCSA2015RO(this._PCDataInput, ansi20152, csa20152);
                    csa2015ro.ShowPreviewDialog();
                    break;
                case "AS2017":
                    Model.PCExportReportANSI aso2017 = this.pCExportReportANSIManager.SelectByCusIdAndProduct(this._PCDataInput.InvoiceCusId, this._PCDataInput.ProductId, "AS");
                    DataInputAS2017RO as2017ro = new DataInputAS2017RO(this._PCDataInput, aso2017);
                    as2017ro.ShowPreviewDialog();
                    break;
            }
        }

        protected override string tableCode()
        {
            return "PCDataInput" + "," + this._PCDataInput.PCDataInputId;
        }
    }
}