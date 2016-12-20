using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Office.Interop.Excel;
using System.Linq;

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
                helpLeave.JoinDate = item.EmployeeJoinDate.HasValue ? item.EmployeeJoinDate.Value.ToString("yyyy-MM-dd") : "";
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (helpLeaveList != null && helpLeaveList.Count != 0)
            {
                IList<string> leaveType = manager.SelectLeaveName();

                Type objClassType = null;
                objClassType = Type.GetTypeFromProgID("Excel.Application");
                if (objClassType == null)
                {
                    MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);
                excel.Rows.RowHeight = 15;
                excel.Columns.ColumnWidth = 8;
                ((Microsoft.Office.Interop.Excel.Range)excel.get_Range(excel.Cells[2, 1], excel.Cells[helpLeaveList.Count + 2, 1])).ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 1]).Value2 = "年份：" + this.comboBoxEditYear.Text;
                ((Microsoft.Office.Interop.Excel.Range)excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 2])).MergeCells = true;
                ((Microsoft.Office.Interop.Excel.Range)excel.Cells[2, 1]).Value2 = "員工姓名(到職日期)";
                ((Microsoft.Office.Interop.Excel.Range)excel.get_Range(excel.Cells[2, 1], excel.Cells[2, leaveType.Count + 1])).Interior.ColorIndex = 15;
                //((Microsoft.Office.Interop.Excel.Range)excel.get_Range(excel.Cells[2, 2], excel.Cells[helpLeaveList.Count + 2, 2])).WrapText = true;
                ((Microsoft.Office.Interop.Excel.Range)excel.get_Range(excel.Cells[1, 1], excel.Cells[helpLeaveList.Count + 2, leaveType.Count + 1])).Borders.LineStyle = XlLineStyle.xlContinuous;

                //for (int i = 0; i < helpLeaveList.Count; i++)
                //{
                //    ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 3, 1]).Value2 = helpLeaveList[i].EmployeeName;
                //    ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 3, 2]).Value2 = helpLeaveList[i].LeaveNote;
                //}

                //表头
                for (int i = 0; i < leaveType.Count; i++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)excel.Cells[2, i + 2]).Value2 = leaveType[i];
                }

                for (int i = 0; i < helpLeaveList.Count; i++)
                {
                    HelpLeave helpLeave = helpLeaveList[i];
                    ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 3, 1]).Value2 = helpLeave.EmployeeName + string.Format("({0})", helpLeave.JoinDate);
                    string[] leaveArray = helpLeave.LeaveNote.Split(',');

                    for (int j = 0; j < leaveType.Count; j++)
                    {
                        string leave = leaveArray.FirstOrDefault(l => l.StartsWith(leaveType[j]));
                        string days = new string(leave.ToCharArray().Where(c => char.IsDigit(c) || c == '.').ToArray());
                        ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 3, j + 2]).Value2 = days;
                    }
                }


                excel.Visible = true;
            }
            else
            {
                MessageBox.Show("請先進行查詢！", "提示", MessageBoxButtons.OK);
                return;
            }
        }
    }

    public class HelpLeave
    {
        public string EmployeeId { get; set; }

        public string JoinDate { get; set; }

        public string EmployeeName { get; set; }

        public string LeaveNote { get; set; }
    }
}