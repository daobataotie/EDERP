using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.XT
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionForm()
        {
            InitializeComponent();
            this.nccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }

        public Condition c { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (c == null)
                c = new Condition();
            c.StartDate = this.dateEditStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStart.DateTime;
            c.EndDate = this.dateEditEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEditEnd.DateTime;
            c.CustomerId = (this.nccCustomer.EditValue as Model.Customer) == null ? null : (this.nccCustomer.EditValue as Model.Customer).CustomerId;
            c.InvoiceCusId = string.IsNullOrEmpty(this.txt_InvoiceCusXOId.Text) ? null : this.txt_InvoiceCusXOId.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}