using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DataInputConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public DataInputConditionForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.date_Start.EditValue = DateTime.Now.AddMonths(-1);
            this.date_End.EditValue = DateTime.Now;
        }

        public DataInputCondition DataInputCondition { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DataInputCondition = new DataInputCondition();
            DataInputCondition.StartDate = (this.date_Start.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.date_Start.DateTime);
            DataInputCondition.EndDate = (this.date_End.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.date_End.DateTime);
            DataInputCondition.PNTId = this.txt_PNTID.Text;
            DataInputCondition.CusXOId = this.txt_CusXOId.Text;
            DataInputCondition.ProductId = (this.btn_Product.EditValue == null ? null : (this.btn_Product.EditValue as Model.Product).ProductId);

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Product_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_Product.EditValue = form.SelectedItem as Model.Product;

            }
            form.Dispose();
            GC.Collect();
        }
    }

    public class DataInputCondition
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PNTId { get; set; }

        public string CusXOId { get; set; }

        public string ProductId { get; set; }
    }
}