using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class ShouldPayFormSearch : DevExpress.XtraEditors.XtraForm
    {
        public Help help = new Help();
        public ShouldPayFormSearch()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.nccSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.date_Start.EditValue = DateTime.Now.AddMonths(-3);
            this.date_End.EditValue = DateTime.Now;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            help.StartDate = this.date_Start.EditValue == null ? DateTime.Now.AddMonths(-1) : this.date_Start.DateTime;
            help.EndDate = this.date_End.EditValue == null ? DateTime.Now : this.date_End.DateTime;
            help.SupplierId = (this.nccSupplier.EditValue as Model.Supplier) == null ? null : (this.nccSupplier.EditValue as Model.Supplier).SupplierId;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }

    public class Help
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SupplierId { get; set; }
    }
}