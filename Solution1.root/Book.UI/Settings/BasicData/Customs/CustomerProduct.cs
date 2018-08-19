using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class CustomerProduct : DevExpress.XtraEditors.XtraForm
    {
        Model.Customer customer = null;
        IList<Model.Product> customerProductList;
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.CustomerProductsManager customerProductsManager = new Book.BL.CustomerProductsManager();
        public List<Model.Product> SelectProduct { get; set; }

        public CustomerProduct()
        {
            InitializeComponent();
        }

        public CustomerProduct(Model.Customer c)
            : this()
        {
            customer = c;
            this.bindingSource1.DataSource = customerProductList = productManager.SelectAllProductByCustomer(c, true);
        }

        public CustomerProduct(Model.Customer c, IList<Model.Product> productList)
            : this()
        {
            customer = c;
            customerProductList = productManager.SelectAllProductByCustomer(c, true);

            foreach (var item in customerProductList)
            {
                if (productList.Any(d => d.ProductId == item.ProductId))
                    item.Checked = true;
            }
            this.bindingSource1.DataSource = customerProductList;
        }

        private void CustomerProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.gridView2.PostEditor();
            this.gridView2.UpdateCurrentRow();
            SelectProduct = customerProductList.Where(d => d.Checked == true).ToList();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in customerProductList)
            {
                item.Checked = this.checkEdit1.Checked;
            }
            this.gridControl2.RefreshDataSource();
        }

        private void che_IsShowUnuseProduct_CheckedChanged(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = customerProductList = productManager.SelectAllProductByCustomer(customer, !this.che_IsShowUnuseProduct.Checked);
            this.gridControl2.RefreshDataSource();
        }
    }
}