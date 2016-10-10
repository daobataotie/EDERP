using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.CT
{
    public partial class ConditionForm : Query.ConditionChooseForm
    {
        public ConditionForm()
        {
            InitializeComponent();
            this.date_End.EditValue = DateTime.Now;
            this.date_Start.EditValue = DateTime.Now.AddMonths(-1);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.newChooseContorl1.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
        }
        public Condition condition;

        public override Book.UI.Query.Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as Condition;
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (this.condition == null)
                this.condition = new Condition();
            condition.StartDate = this.date_Start.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.date_Start.DateTime;
            condition.EndDate = this.date_End.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.date_End.DateTime;
            condition.StartCTId = this.txt_CTStartId.Text;
            condition.EndCTId = this.txt_CTEndId.Text;
            condition.StartCOId = this.txt_COStartId.Text;
            condition.EndCOId = this.txt_COEndId.Text;
            condition.CusId = this.txt_CusId.Text;
            condition.SupplierId = this.newChooseContorl1.EditValue == null ? null : (this.newChooseContorl1.EditValue as Model.Supplier).SupplierId;
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}