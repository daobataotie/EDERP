using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.YPFP
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        BL.ShouldPayAccountDetailManager shouldPayAccountDetailManager = new Book.BL.ShouldPayAccountDetailManager();
        IList<Model.ShouldPayAccountDetail> list = new List<Model.ShouldPayAccountDetail>();
        public ListForm()
        {
            InitializeComponent();

            this.bindingSourceSupplier.DataSource = new BL.SupplierManager().Select();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        protected override void RefreshData()
        {
            this.barButtonItem1_ItemClick(null, null);
            //this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Query.FormFP f = new Book.UI.Query.FormFP();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    Query.ConditionFP c = f.Condition as Query.ConditionFP;
                    this.bindingSource1.DataSource = this.list = this.shouldPayAccountDetailManager.GetByDateRangeAndSupplier(c.StartDate, c.EndDate, c.Supplier == null ? null : c.Supplier.SupplierId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK);

                this.barButtonItem1_ItemClick(null, null);
            }
        }

        public Model.ShouldPayAccountDetail SelectItem { get { return this.bindingSource1.Current as Model.ShouldPayAccountDetail; } }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.list == null || this.list.Count == 0)
            {
                MessageBox.Show("無記錄！", this.Text, MessageBoxButtons.OK);
                return;
            }
            Query.ROFP ro = new Book.UI.Query.ROFP(this.list);
            ro.ShowPreviewDialog();
        }

    }
}