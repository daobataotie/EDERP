using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.XT
{
    public partial class ListForm : BaseListForm
    {
        BL.InvoiceXTManager manager = new Book.BL.InvoiceXTManager();
        public ListForm()
        {
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceXTManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = manager.SelectByCondition(DateTime.Now.AddMonths(-1), DateTime.Now, null, null);
            this.gridControl1.RefreshDataSource();
        }

        protected override Form GetViewForm()
        {
            Model.InvoiceXT invoice = this.SelectedItem as Model.InvoiceXT;
            if (invoice != null)
                return new EditForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceXT>);
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
            ConditionForm f = new ConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Condition c = f.c;
                this.bindingSource1.DataSource = this.manager.SelectByCondition(c.StartDate, c.EndDate, c.CustomerId, c.InvoiceCusId);
                this.gridControl1.RefreshDataSource();
            }
        }

    }
}