using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Hr.Attendance.Atten
{
    public partial class HistoryAttenForm : DevExpress.XtraEditors.XtraForm
    {
        //员工管理实例
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        //部门管理实例
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        //当前待处理员工
        private Book.Model.Employee employee = null;
        //出勤记录
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();
        IList<Model.Employee> lstAllEmp;

        public HistoryAttenForm()
        {
            InitializeComponent();
        }

        private void AttenForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceDepartment.DataSource = departmentManager.Select();
            lstAllEmp = employeeManager.SelectAllEmployee();

            this.bindingSourceEmployee.DataSource = lstAllEmp.Where(E => E.EmployeeLeaveDate == null);
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.employee = this.bindingSourceEmployee.Current as Model.Employee;
            if (this.employee == null) return;
            this.txt_EmployeeId.Text = this.employee.IDNo;
            this.txt_EmployeeName.Text = this.employee.EmployeeName;
        }

        //根据员工编号和日期进行查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.employee == null)
            {
                MessageBox.Show(Properties.Resources.EmployeeNotNull, this.Text, MessageBoxButtons.OK);
                return;
            }
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.DateNotNull, this.Text, MessageBoxButtons.OK);
                return;
            }
            this.bindingSource_atten.DataSource = _hrManager.SelectDailyInfoByEmployeeForDoubleDate(this.employee.EmployeeId, this.date_Start.DateTime.Date, this.date_End.DateTime.Date.AddDays(1).AddSeconds(-1)).Tables[0];
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            DataTable hrTable = this.bindingSource_atten.DataSource as DataTable;
            if (hrTable == null || hrTable.Rows.Count < 1) return;
            switch (e.Column.Name)
            {
                case "LateInMinute":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_LateInMinute] == null)
                        e.DisplayText = "0";
                    else
                        e.DisplayText = hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_LateInMinute].ToString();
                    break;
                case "ShouldCheckIn":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn].ToString() == "")
                        e.DisplayText = "--:--";
                    break;
                case "ShouldCheckOut":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut].ToString() == "")
                        e.DisplayText = "--:--";
                    break;
                case "ActualCheckIn":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn].ToString() == "")
                        e.DisplayText = "--:--";
                    break;
                case "ActualCheckOut":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut].ToString() == "")
                        e.DisplayText = "--:--";
                    break;
            }
        }

        private void chk_ShowLeave_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ShowLeave.Checked)
                this.bindingSourceEmployee.DataSource = lstAllEmp;
            else
                this.bindingSourceEmployee.DataSource = lstAllEmp.Where(E => E.EmployeeLeaveDate == null);
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            //if ((gridControl1.DataSource as DataTable) == null)
            //{
            //    MessageBox.Show("無數據，請先查詢","提示",MessageBoxButtons.OK);
            //    return;
            //}

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
           
            saveFileDialog1.Filter = "Excel file|*.xls";
            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                return;

            string file = saveFileDialog1.FileName;
            this.gridControl1.ExportToXls(file);
        }
    }
}

#region Note
//private void loadatten(string empid, DateTime starttime, DateTime endtime)
//{
//    this.bindingSource_atten.DataSource = this._hrManager.Select();

//}
//private void loadatten(DateTime starttime, DateTime endtime)
//{
//    this.bindingSource_atten.DataSource = this._hrManager.Select();
//}
//private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
//{

//}
#endregion