using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;

namespace Book.UI.Hr.Attendance.ClockData
{
    public partial class NewClockForm : DevExpress.XtraEditors.XtraForm
    {
        BL.ClockDataManager clockDataManager = new Book.BL.ClockDataManager();
        BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();

        public NewClockForm()
        {
            InitializeComponent();

            this.date_Start.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.date_End.EditValue = DateTime.Now;
        }

        private void btn_SearchClockRecord_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不完整", "提示", MessageBoxButtons.OK);
                return;
            }

            DateTime dateStart = this.date_Start.DateTime.Date;
            DateTime dateEnd = this.date_End.DateTime.Date.AddDays(1).AddSeconds(-1);

            this.gridControl1.DataSource = clockDataManager.getbydate(dateStart, dateEnd);
        }

        private void btn_UpdateClockRecord_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不完整", "提示", MessageBoxButtons.OK);
                return;
            }
            DateTime dateStart = this.date_Start.DateTime.Date;
            DateTime dateEnd = this.date_End.DateTime.Date.AddDays(1).AddSeconds(-1);

            DataTable dt = new DataTable();
            try
            {
                string fileName = System.Configuration.ConfigurationManager.AppSettings["AccessPath"];
                string conn = string.Format("Provider=Microsoft.ACE.OLEDB.15.0;Data Source={0};Persist Security Info=False;Jet OLEDB:Database Password=!@Stan888@!", fileName);
                OleDbConnection oconn = new OleDbConnection(conn);
                string sql = string.Format("select c.CheckTime,u.Name,u.SSN from CHECKINOUT c left join USERINFO u on c.USERID=u.USERID where c.CheckTime between #{0}# and #{1}# order by c.CheckTime ", dateStart, dateEnd.ToString("yyyy-MM-dd HH:mm:ss"));

                OleDbDataAdapter oda = new OleDbDataAdapter(sql, oconn);
                oda.Fill(dt);
            }
            catch
            {
                MessageBox.Show("導入只支持64位操作系統配合64位ERP", "提示", MessageBoxButtons.OK);
                return;
            }

            int erpClockCount = clockDataManager.CountClockByDateRange(dateStart, dateEnd);

            if (dt.Rows.Count <= erpClockCount)
            {
                MessageBox.Show("已經導入過打卡資料", "提示", MessageBoxButtons.OK);
                return;
            }

            foreach (DataRow dr in dt.Rows)
            {
                string date = dr[0].ToString();
                string name = dr[1].ToString().Trim();
                string id = dr[2].ToString();
                if (name.Contains("\\0"))
                    name = name.Substring(0, name.IndexOf('\0'));

                Model.Employee emp = this.employeeManager.SelectIdByNameAnId(name, id);
                if (emp == null)
                    continue;

                Model.ClockData clockdata = new Book.Model.ClockData();
                clockdata.ClockDataId = Guid.NewGuid().ToString();
                clockdata.EmployeeId = emp.EmployeeId;
                clockdata.InsertTime = DateTime.Now;
                clockdata.Empclockdate = DateTime.Parse(date).Date;
                clockdata.Clocktime = DateTime.Parse(date);
                clockdata.CardNo = emp.CardNo;
                new BL.ClockDataManager().Insert(clockdata);
            }

            MessageBox.Show("導入完成", "提示", MessageBoxButtons.OK);
        }
    }
}