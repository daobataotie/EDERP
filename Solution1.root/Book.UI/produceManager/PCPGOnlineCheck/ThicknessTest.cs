using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class ThicknessTest : Settings.BasicData.BaseEditForm
    {
        public Model.ThicknessTest _ThicknessTest;
        public BL.ThicknessTestManager _ThicknessTestManager = new Book.BL.ThicknessTestManager();
        private string _PCPGOnlineCheckDetailId = string.Empty;

        public ThicknessTest()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.ThicknessTest.PRO_ThicknessTestId, new AA(Properties.Resources.NumsIsNotNull, this.txtThicknessTestId));
            this.requireValueExceptions.Add(Model.ThicknessTest.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee));
            this.requireValueExceptions.Add(Model.ThicknessTest.PRO_ThicknessTestDate, new AA(Properties.Resources.DateIsNull, this.deThicknessTestDate));
            //this.requireValueExceptions.Add(Model.ThicknessTest.PRO_manualId, new AA("手動編號不能為空!", this.txtManualid));
            //this.invalidValueExceptions.Add(Model.ThicknessTest.PRO_manualId, new AA("手動編號重複,請重新輸入!", this.txtManualid));

            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";
        }

        public ThicknessTest(string PCPGOnlineCheckDetailId)
            : this()
        {
            this._PCPGOnlineCheckDetailId = PCPGOnlineCheckDetailId;
        }

        protected override void AddNew()
        {
            this._ThicknessTest = new Model.ThicknessTest();
            this._ThicknessTest.ThicknessTestId = this._ThicknessTestManager.GetId();
            this._ThicknessTest.PCPGOnlineCheckDetailId = this._PCPGOnlineCheckDetailId;
            this._ThicknessTest.ThicknessTestDate = DateTime.Now;
            this._ThicknessTest.Employee = BL.V.ActiveOperator.Employee;
            this._ThicknessTest.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            this._ThicknessTest.Details = new List<Model.ThicknessTestDetails>();
            for (int i = 1; i < 9; i++)
            {
                Model.ThicknessTestDetails d = new Book.Model.ThicknessTestDetails();
                d.ThicknessTestDetailsId = Guid.NewGuid().ToString();
                d.ThicknessTestId = this._ThicknessTest.ThicknessTestId;
                d.HouduBiao = "#" + i.ToString() + " 號片";

                this._ThicknessTest.Details.Add(d);
            }
        }

        protected override void Delete()
        {
            if (this._ThicknessTest == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._ThicknessTestManager.DeleteByPCPGOnlineCheckDetailId(this._ThicknessTest.ThicknessTestId);
            this._ThicknessTest = this._ThicknessTestManager.mGetNext(this._ThicknessTest.InsertTime.Value, this._PCPGOnlineCheckDetailId);
            if (this._ThicknessTest == null)
            {
                this._ThicknessTest = this._ThicknessTestManager.mGetLast(this._PCPGOnlineCheckDetailId);
            }
        }

        protected override void MoveLast()
        {
            this._ThicknessTest = this._ThicknessTestManager.Get(this._ThicknessTestManager.mGetLast(this._PCPGOnlineCheckDetailId) == null ? "" : this._ThicknessTestManager.mGetLast(this._PCPGOnlineCheckDetailId).ThicknessTestId);
        }

        protected override void MoveFirst()
        {
            this._ThicknessTest = this._ThicknessTestManager.Get(this._ThicknessTestManager.mGetFirst(this._PCPGOnlineCheckDetailId) == null ? "" : this._ThicknessTestManager.mGetFirst(this._PCPGOnlineCheckDetailId).ThicknessTestId);
        }

        protected override void MovePrev()
        {
            Model.ThicknessTest mThicknessTest = this._ThicknessTestManager.mGetPrev(this._ThicknessTest.InsertTime.Value, this._PCPGOnlineCheckDetailId);
            if (mThicknessTest == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._ThicknessTest = this._ThicknessTestManager.Get(mThicknessTest.ThicknessTestId);
        }

        protected override void MoveNext()
        {
            Model.ThicknessTest mThicknessTest = this._ThicknessTestManager.mGetNext(this._ThicknessTest.InsertTime.Value, this._PCPGOnlineCheckDetailId);
            if (mThicknessTest == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._ThicknessTest = this._ThicknessTestManager.Get(mThicknessTest.ThicknessTestId);
        }

        protected override bool HasRows()
        {
            return this._ThicknessTestManager.mHasRows(this._PCPGOnlineCheckDetailId);
        }

        protected override bool HasRowsNext()
        {
            return this._ThicknessTestManager.mHasRowsAfter(this._ThicknessTest, this._PCPGOnlineCheckDetailId);
        }

        protected override bool HasRowsPrev()
        {
            return this._ThicknessTestManager.mHasRowsBefore(this._ThicknessTest, this._PCPGOnlineCheckDetailId);
        }

        protected override void Save()
        {
            this._ThicknessTest.ThicknessTestId = this.txtThicknessTestId.Text;
            //this._ThicknessTest.manualId = this.txtManualid.Text;
            this._ThicknessTest.ThicknessTestDate = this.deThicknessTestDate.DateTime;
            this._ThicknessTest.Perspectiverate = this.spinPerspectiverate.EditValue == null ? 0 : double.Parse(this.spinPerspectiverate.EditValue.ToString());
            this._ThicknessTest.ThicknessDescript = this.txtThicknessDescript.Text;
            this._ThicknessTest.Employee = this.nccEmployee.EditValue as Model.Employee;
            if (this._ThicknessTest.Employee != null)
            {
                this._ThicknessTest.EmployeeId = this._ThicknessTest.Employee.EmployeeId;
            }

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._ThicknessTestManager.Insert(this._ThicknessTest);
                    break;
                case "update":
                    this._ThicknessTestManager.Update(this._ThicknessTest);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._ThicknessTest == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this._ThicknessTest = this._ThicknessTestManager.GetDetail(this._ThicknessTest.ThicknessTestId);
            }

            this.txtThicknessTestId.Text = this._ThicknessTest.ThicknessTestId;
            //this.txtManualid.Text = this._ThicknessTest.manualId;
            this.deThicknessTestDate.EditValue = this._ThicknessTest.ThicknessTestDate.Value;
            this.txtThicknessDescript.Text = this._ThicknessTest.ThicknessDescript;
            this.nccEmployee.EditValue = this._ThicknessTest.Employee;
            this.spinPerspectiverate.EditValue = this._ThicknessTest.Perspectiverate.HasValue ? this._ThicknessTest.Perspectiverate.Value : 0;

            this.bindingSource1.DataSource = this._ThicknessTest.Details;

            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.barBtn_Search.Enabled = false;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.barBtn_Search.Enabled = false;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.barBtn_Search.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }
            this.txtThicknessTestId.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new ROThicknessTest(_ThicknessTest);
        }


        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThicknessTestList f = new ThicknessTestList(this._PCPGOnlineCheckDetailId);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.ThicknessTest currentModel = f.SelectItem as Model.ThicknessTest;
                if (currentModel != null)
                {
                    this._ThicknessTest = currentModel;
                    this.Refresh();
                }
            }

            f.Dispose();
            GC.Collect();
        }
    }
}