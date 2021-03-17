using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Query
{
    public partial class ShouPayBySubjectForm : DevExpress.XtraEditors.XtraForm
    {
        IList<Model.Supplier> suppliers = null;    //所有供應商
        IList<Model.Supplier> subjectSuppliers = new List<Model.Supplier>();     //科目對應的供應商
        Dictionary<string, string> dic = new Dictionary<string, string>();

        IList<Model.InvoiceCG> invoiceCGs = null;

        public ShouPayBySubjectForm()
        {
            InitializeComponent();


            dic["原料"] = "原料";
            dic["物料"] = "物料";
            dic["代工加工"] = "代工 加工";
            dic["修繕"] = "修繕";
            dic["客供品"] = "客供品";
            dic["文具用品"] = "文具";
            dic["清潔"] = "清潔用品";
            dic["運費"] = "運費";
            dic["其他"] = "其他";

            List<string> subjectNames = dic.Keys.Distinct().ToList();
            foreach (var item in subjectNames)
            {
                this.cmb_Subject.Properties.Items.Add(item);
            }

            suppliers = new BL.SupplierManager().Select();
        }

        private void cmb_Subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            lue_Supplier.EditValue = null;

            if (this.cmb_Subject.EditValue == null)
            {
                bindingSourceSupplier.DataSource = null;
                return;
            }

            string supplierCategoryName = dic[this.cmb_Subject.EditValue.ToString()];
            bindingSourceSupplier.DataSource = subjectSuppliers = suppliers.Where(s =>
            {
                if (s.SupplierCategory != null && s.SupplierCategory.SupplierCategoryName.Contains(supplierCategoryName))
                    return true;
                else
                    return false;
            }).ToList();
        }

        private void lue_Supplier_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lue_Supplier.EditValue = null;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不完整！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (this.cmb_Subject.EditValue == null)
            {
                MessageBox.Show("請選擇科目！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (subjectSuppliers.Count == 0)
            {
                MessageBox.Show("該科目無對應供應商", "提示", MessageBoxButtons.OK);
                return;
            }

            string supplierIds = "";
            if (this.lue_Supplier.EditValue != null)
            {
                supplierIds = "'" + this.lue_Supplier.EditValue.ToString() + "'";
            }
            else
            {
                foreach (var item in subjectSuppliers)
                {
                    supplierIds += "'" + item.SupplierId + "',";
                }

                supplierIds = supplierIds.TrimEnd(',');
            }

            this.bindingSource1.DataSource = invoiceCGs = new BL.InvoiceCGManager().SelectByDateAndSuppliers(date_Start.DateTime, date_End.DateTime, supplierIds);
            this.gridControl1.RefreshDataSource();

            if (invoiceCGs.Count == 0)
                MessageBox.Show("無資料", "提示", MessageBoxButtons.OK);
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Invoices.CG.EditForm f = new Book.UI.Invoices.CG.EditForm(((HyperLinkEdit)sender).Text);
            f.ShowDialog();
        }

    }
}