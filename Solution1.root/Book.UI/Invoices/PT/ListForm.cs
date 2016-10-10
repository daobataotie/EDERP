using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.PT
{
    public partial class ListForm : BaseListForm
    {
        BL.InvoicePTDetailManager detailManager = new Book.BL.InvoicePTDetailManager();
        public ListForm()
        {
            InitializeComponent();
            this.invoiceManager = new BL.InvoicePTManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        protected override Form GetViewForm()
        {
            Model.InvoicePTDetail invoice = this.bindingSource1.Current as Model.InvoicePTDetail;
            if (invoice != null)
                return new EditForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoicePTDetail>);
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }

        protected override void ShowSearchForm()
        {
            ConditionPT f = new ConditionPT();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //this.bindingSource1.DataSource = (new BL.InvoicePTManager()).Select(f.startdate, f.enddate, f.invoiceId, f.employeeId, f.depot, f.depotIn, f.productId);
                this.bindingSource1.DataSource = detailManager.SelectByConditon(f.startdate, f.enddate, f.invoiceId, f.employeeId, f.depot, f.depotIn, f.productId);
            }
        }
    }
}