using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.AccountPayable.AcOtherShouldPayment
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionForm()
        {
            InitializeComponent();

            this.date_Start.EditValue = DateTime.Now.AddMonths(-1);
            this.date_End.EditValue = DateTime.Now;
            this.ncc_Supplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();

            this.StartPosition = FormStartPosition.CenterParent;
        }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public Model.Supplier Supplier { get; set; }


        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不能為空", "提示", MessageBoxButtons.OK);
                return;
            }

            this.DateStart = this.date_Start.DateTime.Date;
            this.DateEnd = this.date_End.DateTime.Date.AddDays(1).AddSeconds(-1);
            this.Supplier = this.ncc_Supplier.EditValue as Model.Supplier;

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}