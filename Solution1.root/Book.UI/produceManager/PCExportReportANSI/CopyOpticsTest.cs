using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class CopyOpticsTest : DevExpress.XtraEditors.XtraForm
    {
        public CopyOpticsTest(string invoiceCusXOId)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            this.bindingSource1.DataSource = new BL.OpticsTestManager().SelectByInvoiceCusXOId(invoiceCusXOId);
            this.gridControl1.RefreshDataSource();
        }

        public List<Model.OpticsTest> List { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            var source = this.bindingSource1.DataSource as IList<Model.OpticsTest>;

            if (source != null && source.Count > 0)
            {
                List = source.Where(l => l.IsChecked).ToList();
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}