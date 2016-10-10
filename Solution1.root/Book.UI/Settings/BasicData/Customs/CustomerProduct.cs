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
            this.bindingSource1.DataSource = customerProductList = productManager.SelectAllProductByCustomer(c);
        }

        public CustomerProduct(Model.Customer c, IList<Model.Product> productList)
            : this()
        {
            customerProductList = productManager.SelectAllProductByCustomer(c);

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
    }
}