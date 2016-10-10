using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class AnnualShipment : DevExpress.XtraEditors.XtraForm
    {
        BL.InvoiceXSDetailManager invoiceXSDetailManager = new Book.BL.InvoiceXSDetailManager();
        public AnnualShipment()
        {
            InitializeComponent();
            this.bindingSourceCustomer.DataSource = new BL.CustomerManager().GetCustomerBaseInfo();
            this.bindingSourceProduct.DataSource = new BL.ProductManager().GetProductBaseInfo();

            this.nccCustomer.Choose = new Customs.ChooseCustoms();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.but_Product.EditValue = form.SelectedItem as Model.Product;
            }
            form.Dispose();
            GC.Collect();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (this.but_Product.EditValue == null)
            {
                MessageBox.Show("客戶商品不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }
            if (this.date_Start.EditValue == null)
            {
                MessageBox.Show("起始日期不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }
            if (this.date_End.EditValue == null)
            {
                MessageBox.Show("結束日期不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }

            this.bindingSourceHeader.DataSource = invoiceXSDetailManager.SelectAnnualShipment((this.but_Product.EditValue as Model.Product).ProductId, this.date_Start.DateTime, this.date_End.DateTime, (this.nccCustomer.EditValue == null ? null : (this.nccCustomer.EditValue as Model.Customer).CustomerId), 0);

            this.gridControl1.RefreshDataSource();
        }

        //private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    if (this.bindingSourceHeader.Current != null)
        //    {
        //        DataRow dr = this.bindingSourceHeader.Current as DataRow;

        //        string productId = dr["ProductId"].ToString();
        //        string customerId = dr["CustomerId"].ToString();

        //        this.bindingSourceDetail.DataSource = "";
        //        this.gridControl2.RefreshDataSource();
        //    }
        //}
    }

}