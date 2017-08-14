using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Hr.Salary.Salaryset
{
    public partial class AnnualSalaryForm : DevExpress.XtraEditors.XtraForm
    {
        BL.MonthlySalaryManager monthlySalaryManager = new Book.BL.MonthlySalaryManager();
        private int hryear = 0;
        private IList<Model.Employee> _emplist = new List<Model.Employee>();
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();

        public AnnualSalaryForm()
        {
            InitializeComponent();
        }

        private void PrintMonthSalary_Load(object sender, EventArgs e)
        {
            DateTime date = this.monthlySalaryManager.get_MaxIdentifyDateMonth();

            if (date.Year != 1)
            {
                DateTime strdate = date.AddYears(1);

                for (int i = 0; i < 10; i++)
                {
                    this.comboBoxEdit1.Properties.Items.Add(strdate.AddYears(-1).ToString("yyyy年"));
                    strdate = strdate.AddYears(-1);
                }
                this.comboBoxEdit1.SelectedIndex = 0;
                this.hryear = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
                _emplist = this.employeeManager.SelectHrDailyAttendByMonth(new DateTime(hryear, 1, 1));
                this.bindingSource1.DataSource = _emplist;
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hryear = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
            this._emplist = this.employeeManager.SelectHrDailyAttendByMonth(new DateTime(hryear, 1, 1));
            this.bindingSource1.DataSource = _emplist;
        }

        private void btn_PrintTotal_Click(object sender, EventArgs e)
        {
        }

        private void btn_PrintSelected_Click(object sender, EventArgs e)
        {
            this.gridView2.PostEditor();
            this.gridView2.UpdateCurrentRow();
            IList<Model.Employee> list = this._emplist.Where(d => d.IsChecked == true).ToList();
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("至少選擇一位員工！", this.Text, MessageBoxButtons.OK);
                return;
            }
            else
            {
            }
        }
    }
}