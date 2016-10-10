using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.StockLimitations
{
    public partial class StockCheckConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public StockCheckCondition model;
        public StockCheckConditionForm()
        {
            InitializeComponent();

            this.dateEditStart.EditValue = DateTime.Now.AddMonths(-1);
            this.dateEditEnd.EditValue = DateTime.Now;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (model == null)
                model = new StockCheckCondition();
            model.StartDate = this.dateEditStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStart.DateTime;
            model.EndDate = this.dateEditEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEditEnd.DateTime;
            model.Invoiceid = this.txt_Invoiceid.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            GC.Collect();
        }
    }
}