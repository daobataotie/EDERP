using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Attendance.Leave
{
    public partial class YearLeaveList : DevExpress.XtraEditors.XtraForm
    {
        IList<Model.Employee> emplist;
        BL.LeaveManager manager = new Book.BL.LeaveManager();
        HelpLeave helpLeave;
        IList<HelpLeave> helpLeaveList = new List<HelpLeave>();
        public YearLeaveList()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            for (int i = 0; i < 10; i++)
            {
                this.comboBoxEditYear.Properties.Items.Add(DateTime.Now.Year - i);
            }
        }

        public YearLeaveList(IList<Model.Employee> list)
            : this()
        {
            this.emplist = list;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {

            if (this.comboBoxEditYear.EditValue == null)
            {
                MessageBox.Show("年份不能為空！", "提示", MessageBoxButtons.OK);
                return;
            }
            this.helpLeaveList.Clear();
            foreach (var item in emplist)
            {
                this.helpLeave = new HelpLeave();
                helpLeave.EmployeeName = item.EmployeeName;
                helpLeave.LeaveNote = this.manager.SelectYearLeaveCount(item.EmployeeId, Convert.ToInt32(this.comboBoxEditYear.EditValue));
                if (helpLeave.LeaveNote != null)
                {
                    helpLeave.LeaveNote = helpLeave.LeaveNote.Substring(0, helpLeave.LeaveNote.Length - 1);
                    //helpLeave.LeaveNote = helpLeave.LeaveNote.Replace(",", "\r");
                }
                this.helpLeaveList.Add(this.helpLeave);
            }

            this.bindingSource1.DataSource = this.helpLeaveList;
            this.gridControl1.RefreshDataSource();
        }
    }

    public class HelpLeave
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string LeaveNote { get; set; }
    }
}