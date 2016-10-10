using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Attendance.Leave
{
    public partial class IntervalLeave : DevExpress.XtraEditors.XtraForm
    {
        private BL.EmployeeManager _EmployeeManager = new Book.BL.EmployeeManager();
        private BL.MonthlySalaryManager _monthlysalarymanager = new Book.BL.MonthlySalaryManager();

        private IList<IntervalLeaveDayHelp> _IntervalLeaveDays = new List<IntervalLeaveDayHelp>();

        public IntervalLeave()
        {
            InitializeComponent();

            //初始化月份
            this.LoadMonthAndDepartment();
            //初始化选中月份假日
            this.LoadHolidayOfMonth();
            //初始化左边员工
            this.LoadEmployeesByMonth();
        }

        private void LoadMonthAndDepartment()
        {
            this.combox_anotherMonth.Properties.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                this.combox_anotherMonth.Properties.Items.Add(DateTime.Now.Date.AddMonths((0 - i)).ToString("yyyy-MM"));
            }
            this.combox_anotherMonth.SelectedIndex = 0;


            //this.chkCombox_Department.Properties.Items.Clear();
            foreach (var item in new BL.DepartmentManager().Select())
            {
                this.chkCombox_Department.Properties.Items.Add(item.DepartmentId, item.DepartmentName);
            }
        }

        private void LoadHolidayOfMonth()
        {
            //构建选中月份的休假日期
            //默认初始化一个不隔周休的选项
            this._IntervalLeaveDays.Clear();
            this._IntervalLeaveDays.Add(new IntervalLeaveDayHelp { IsChecked = false, HolidayDate = null });

            DateTime currentDT = DateTime.Parse(this.combox_anotherMonth.SelectedItem.ToString() + "-01").Date;
            DateTime EndDT = currentDT.AddMonths(1);
            while (currentDT < EndDT)
            {
                if (currentDT.DayOfWeek == DayOfWeek.Saturday)
                {
                    IntervalLeaveDayHelp IntervalLeaveDay = new IntervalLeaveDayHelp
                    {
                        IsChecked = false,
                        HolidayDate = currentDT
                    };
                    this._IntervalLeaveDays.Add(IntervalLeaveDay);
                }
                currentDT = currentDT.AddDays(1);
            }

            this.bs_IntervalHoliday.DataSource = this._IntervalLeaveDays;
            this.grid_MonthDate.RefreshDataSource();

            //如果不是本月则不让修改保存
            if (currentDT.AddMonths(-1).Year == DateTime.Now.Year && currentDT.AddMonths(-1).Month == DateTime.Now.Month)
                this.btn_Save.Enabled = true;
            else
                this.btn_Save.Enabled = false;
        }

        private void LoadEmployeesByMonth()
        {
            DateTime mdate = DateTime.Parse(this.combox_anotherMonth.SelectedItem.ToString() + "-01").Date;
            this.bs_Employees.DataSource = this._EmployeeManager.GetHasThereEmp_ListByDateTime(mdate);

            if (!(mdate.Year == DateTime.Now.Year) && !(mdate.Month == DateTime.Now.Month))
            {
                IList<Model.MonthlySalary> mss = this._monthlysalarymanager.SelectForSpecialMonth(mdate.Year, mdate.Month).ToList();
                (this.bs_Employees.DataSource as List<Model.Employee>).ForEach(d =>
                {
                    try
                    {
                        Model.MonthlySalary curMS = mss.Where(ms => ms.EmployeeId == d.EmployeeId).First();
                        d.HolidayInstitution = curMS.HolidayInstitution;
                        d.HolidayInstitutionDate = curMS.HolidayInstitutionDate;
                    }
                    catch
                    {
                        d.HolidayInstitution = false;
                        d.HolidayInstitutionDate = string.Empty;
                    }
                });
            }
            this.grid_MonthDate.RefreshDataSource();
            this.grid_Employees.RefreshDataSource();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            IList<Model.Employee> CheckedEmps = new List<Model.Employee>();
            CheckedEmps = (this.bs_Employees.DataSource as List<Model.Employee>).Where(d => d.IsChecked).ToList();
            if (CheckedEmps.Count > 0)
            {
                bool hasHoliday;
                string holidayDateStr;

                if (this._IntervalLeaveDays[0].IsChecked)
                {
                    hasHoliday = false;
                    holidayDateStr = string.Empty;
                }
                else
                {
                    try
                    {
                        hasHoliday = true;
                        holidayDateStr = this._IntervalLeaveDays
                                            .Where(d => d.IsChecked)
                                            .Select(d => d.HolidayDateStr)
                                            .Aggregate((current, next) => String.Format("{0},{1}", current, next));
                    }
                    catch
                    {
                        hasHoliday = false;
                        holidayDateStr = string.Empty;
                    }
                }

                foreach (var item in CheckedEmps)
                {
                    item.HolidayInstitution = hasHoliday;
                    item.HolidayInstitutionDate = holidayDateStr;
                    item.EmployeePhoto = new byte[] { };
                    try { this._EmployeeManager.Update(item); }
                    catch { }
                }
            }
            else
            {
                Model.Employee curEmp = this.bs_Employees.Current as Model.Employee;
                string holiydays = string.Empty;
                if (this._IntervalLeaveDays[0].IsChecked)
                {
                    curEmp.HolidayInstitution = false;
                    curEmp.HolidayInstitutionDate = string.Empty;
                }
                else
                {
                    try
                    {
                        curEmp.HolidayInstitution = true;
                        curEmp.HolidayInstitutionDate = this._IntervalLeaveDays
                                            .Where(d => d.IsChecked)
                                            .Select(d => d.HolidayDateStr)
                                            .Aggregate((current, next) => String.Format("{0},{1}", current, next));
                    }
                    catch
                    {
                        curEmp.HolidayInstitution = false;
                        curEmp.HolidayInstitutionDate = string.Empty;
                    }
                }
                try
                { this._EmployeeManager.Update(curEmp); }
                catch { }
            }


            this.grid_Employees.RefreshDataSource();
            this.grid_MonthDate.RefreshDataSource();
            MessageBox.Show("設置成功!", "消息");
        }

        private void combox_anotherMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadHolidayOfMonth();
            this.LoadEmployeesByMonth();
        }

        private void bs_Employees_CurrentChanged(object sender, EventArgs e)
        {
            Model.Employee curEmp = this.bs_Employees.Current as Model.Employee;
            if (curEmp == null)
                return;
            if (curEmp.HolidayInstitution.HasValue && curEmp.HolidayInstitution.Value)
            {
                //隔周休
                this._IntervalLeaveDays[0].IsChecked = false;
                string[] hasCheckHolid = curEmp.HolidayInstitutionDate.Split(',');
                if (hasCheckHolid != null && hasCheckHolid.Length > 0)
                {
                    this._IntervalLeaveDays
                        .Where(d => d.HolidayDate.HasValue).ToList()
                        .ForEach(d =>
                        {
                            if (hasCheckHolid.Contains(d.HolidayDateStr))
                            { d.IsChecked = true; }
                            else
                            { d.IsChecked = false; }
                        });
                }
            }
            else
            {
                //不隔周休
                this._IntervalLeaveDays[0].IsChecked = true;
                this._IntervalLeaveDays
                    .Where(d => d.HolidayDate.HasValue).ToList().
                    ForEach(d => { d.IsChecked = false; });
            }

            this.grid_MonthDate.RefreshDataSource();
        }

        //右边选择休假日期
        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            IntervalLeaveDayHelp curItem = this.bs_IntervalHoliday.Current as IntervalLeaveDayHelp;
            if (curItem.HolidayDate == null)
            {
                foreach (var item in this._IntervalLeaveDays)
                {
                    item.IsChecked = false;
                }
            }
            else
            {
                this._IntervalLeaveDays[0].IsChecked = false;
            }
            this.grid_MonthDate.RefreshDataSource();
        }

        private void chkCombox_Department_EditValueChanged(object sender, EventArgs e)
        {
            IList<Model.Employee> emplist = this.bs_Employees.DataSource as IList<Model.Employee>;
            if (emplist != null && emplist.Count > 0)
            {
                foreach (var item in emplist)
                {
                    item.IsChecked = false;
                }

                var CheckedDeparts = this.chkCombox_Department.Properties.Items.GetCheckedValues();
                if (CheckedDeparts != null && CheckedDeparts.Count > 0)
                {
                    (this.bs_Employees.DataSource as List<Model.Employee>).ForEach(d =>
                    {
                        if (CheckedDeparts.Contains(d.DepartmentId))
                            d.IsChecked = true;
                        else
                            d.IsChecked = false;
                    });
                }
                this.grid_Employees.RefreshDataSource();
            }
        }
    }

    class IntervalLeaveDayHelp
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        private bool _IsChecked;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value; }
        }

        /// <summary>
        /// 周六假日日期
        /// </summary>
        private DateTime? _HolidayDate;

        /// <summary>
        /// 周六假日日期
        /// </summary>
        public DateTime? HolidayDate
        {
            get { return _HolidayDate; }
            set { _HolidayDate = value; }
        }

        /// <summary>
        /// 假日日期字符串显示
        /// </summary>
        public string HolidayDateStr
        {
            get { return this._HolidayDate.HasValue ? this._HolidayDate.Value.ToString("yyyy-MM-dd") : "不隔周休"; }
        }
    }
}