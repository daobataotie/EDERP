using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtSummon
{
    public partial class ListConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public ListConditionForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            this.dateEditStart.EditValue = DateTime.Now.AddDays(-30);
            this.dateEditEnd.EditValue = DateTime.Now;
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Summary { get; set; }


        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.dateEditStart.EditValue == null || this.dateEditEnd.EditValue == null)
            {
                MessageBox.Show("日期區間不能為空", "提示", MessageBoxButtons.OK);
                return;
            }

            StartDate = this.dateEditStart.DateTime;
            EndDate = this.dateEditEnd.DateTime;
            Summary = this.txt_Summary.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}