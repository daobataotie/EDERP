using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Employees
{
    public partial class AttendanceJJDays : DevExpress.XtraEditors.XtraForm
    {
        public AttendanceJJDays()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public AttendanceJJDays(double value, double value2)
            : this()
        {
            this.spinEdit1.EditValue = value;
            this.spinEdit2.EditValue = value2;
        }
        BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();

        //出勤獎金天數
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.spinEdit1.EditValue != null)
            {
                string str = "UPDATE Employee SET AttendanceJJDays='" + Convert.ToDouble(this.spinEdit1.EditValue) + "'";

                if (employeeManager.UpdateSql(str) > 1)
                    MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text);
            }
        }

        //底薪天數
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.spinEdit2.EditValue != null)
            {
                string sql = "update Employee set AttendanceDays='" + Convert.ToDouble(this.spinEdit2.EditValue) + "'";

                if (employeeManager.UpdateSql(sql) > 1)
                    MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text);
            }
        }

        //時數補貼天數
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (this.spinEdit3.EditValue != null)
            {
                string sql = "update Employee set TimeBonusDays='" + Convert.ToDecimal(this.spinEdit3.EditValue) + "' ";
                if (employeeManager.UpdateSql(sql) > 1)
                    MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text);
            }
        }

        //時數補貼小時
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (this.spinEdit4.EditValue != null)
            {
                string sql = "update Employee set TimeBonusTimes='" + Convert.ToDecimal(this.spinEdit4.EditValue) + "' ";
                if (employeeManager.UpdateSql(sql) > 1)
                    MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text);
            }
        }
    }
}