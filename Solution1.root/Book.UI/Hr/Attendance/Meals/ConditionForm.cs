using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Attendance.Meals
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 是否為個人月度查詢
        /// </summary>
        bool IsPersonal = false;

        public ConditionForm(bool isPersonal)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            newChooseContorl1.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            this.IsPersonal = isPersonal;

            if (!IsPersonal)
                this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            else
                this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            this.dateEdit1.EditValue = DateTime.Now;
        }

        public DateTime Month { get; set; }

        public string EmployeeId { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show("月份不能為空", "提示", MessageBoxButtons.OK);
                return;
            }
            if (IsPersonal)
            {
                if (this.newChooseContorl1.EditValue == null)
                {
                    MessageBox.Show("員工不能為空", "提示", MessageBoxButtons.OK);
                    return;
                }

                EmployeeId = (newChooseContorl1.EditValue as Model.Employee).EmployeeId;
            }
            Month = this.dateEdit1.DateTime;

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}