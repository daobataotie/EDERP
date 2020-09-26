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
    public partial class ListForm : DevExpress.XtraEditors.XtraForm
    {
        BL.LunchDetailManager manager = new Book.BL.LunchDetailManager();

        public ListForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void bar_MonthSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm(false);
            if (f.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = manager.GetMonthLunch(f.Month);
                this.gridControl1.DataSource = dt;
            }
        }

        private void bar_MonthPersonalSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm(true);
            if (f.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = manager.GetMonthPersonalLunch(f.Month, f.EmployeeId);
                this.gridControl1.DataSource = dt;
            }
        }

        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            this.barStaticItem1.Caption = this.gridView1.RowCount + "項";
        }
    }
}