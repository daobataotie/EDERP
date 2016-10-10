using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtBillsIncome
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.date_KPStart.EditValue = DateTime.Now.AddMonths(-1);
            this.date_KPEnd.EditValue = DateTime.Now;
            this.nccBackStart.Choose = new Settings.BasicData.Bank.ChooseBank();
            this.nccBackEnd.Choose = new Settings.BasicData.Bank.ChooseBank();
            this.nccSupplierStart.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.nccSupplierEnd.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.nccCustomerStart.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.nccCustomerEnd.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.nccSupplierStart.Enabled = false;
            this.nccSupplierEnd.Enabled = false;
            this.nccCustomerStart.Enabled = false;
            this.nccCustomerEnd.Enabled = false;
        }

        public Condition condition { get; set; }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (condition == null)
                condition = new Condition();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_KPStart.DateTime, new DateTime()))
            {
                this.condition.KPStart = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.condition.KPStart = this.date_KPStart.DateTime;
            }
            this.condition.KPEnd = this.date_KPEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.date_KPEnd.DateTime;
            this.condition.DQStart = this.date_DQStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.date_DQStart.DateTime;
            this.condition.DQEnd = this.date_DQEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.date_DQEnd.DateTime;
            this.condition.YDStart = this.date_YDStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.date_YDStart.DateTime;
            this.condition.YDEnd = this.date_YDEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.date_YDEnd.DateTime;
            this.condition.IdStart = this.txt_IdStart.Text;
            this.condition.IdEnd = this.txt_IdEnd.Text;
            this.condition.Category = this.cobCategory.SelectedIndex.ToString();
            this.condition.InvoiceState = this.cobInvoiceState.SelectedIndex.ToString();
            this.condition.BankNameStart = this.nccBackStart.EditValue == null ? null : (this.nccBackStart.EditValue as Model.Bank).BankName;
            this.condition.BankNameEnd = this.nccBackEnd.EditValue == null ? null : (this.nccBackEnd.EditValue as Model.Bank).BankName;
            this.condition.SupplierIdStart = this.nccSupplierStart.EditValue == null ? null : (this.nccSupplierStart.EditValue as Model.Supplier).Id;
            this.condition.SupplierIdEnd = this.nccSupplierEnd.EditValue == null ? null : (this.nccSupplierEnd.EditValue as Model.Supplier).Id;
            this.condition.CustomerIdStart = this.nccCustomerStart.EditValue == null ? null : (this.nccCustomerStart.EditValue as Model.Customer).Id;
            this.condition.CustomerIdEnd = this.nccCustomerEnd.EditValue == null ? null : (this.nccCustomerEnd.EditValue as Model.Customer).Id;

            this.DialogResult = DialogResult.OK;
        }

        private void nccBackStart_EditValueChanged(object sender, EventArgs e)
        {
            this.nccCustomerEnd.EditValue = this.nccCustomerStart.EditValue;
        }

        private void nccSupplierStart_EditValueChanged(object sender, EventArgs e)
        {
            this.nccSupplierEnd.EditValue = this.nccSupplierStart.EditValue;
        }

        private void nccCustomerStart_EditValueChanged(object sender, EventArgs e)
        {
            this.nccCustomerEnd.EditValue = this.nccCustomerStart.EditValue;
        }

        private void txt_IdStart_EditValueChanged(object sender, EventArgs e)
        {
            this.txt_IdEnd.EditValue = this.txt_IdStart.EditValue;
        }

        private void cobCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobCategory.SelectedIndex == 0)
            {
                this.nccSupplierStart.Enabled = false;
                this.nccSupplierEnd.Enabled = false;
                this.nccCustomerStart.Enabled = true;
                this.nccCustomerEnd.Enabled = true;
            }
            else if (cobCategory.SelectedIndex == 1)
            {
                this.nccCustomerStart.Enabled = false;
                this.nccCustomerEnd.Enabled = false;
                this.nccSupplierStart.Enabled = true;
                this.nccSupplierEnd.Enabled = true;
            }
        }

    }
}