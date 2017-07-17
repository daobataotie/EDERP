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
    public partial class ShouldPayFormList : Settings.BasicData.BaseListForm
    {
        BL.ShouldPayAccountManager manager = new Book.BL.ShouldPayAccountManager();
        public ShouldPayFormList()
        {
            InitializeComponent();
            this.bindingSourceSupplier.DataSource = new BL.SupplierManager().Select();
        }

        protected override void RefreshData()
        {
            this.barButtonItem1_ItemClick(null, null);
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new ShouldPayForm();
        }

        public Model.ShouldPayAccount SelectItem { get { return this.bindingSource1.Current as Model.ShouldPayAccount; } }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(ShouldPayForm);
            return (ShouldPayForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShouldPayFormSearch f = new ShouldPayFormSearch();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.bindingSource1.DataSource = this.manager.SelectByCondition(f.help.StartDate, f.help.EndDate, f.help.SupplierId);
                this.gridControl1.RefreshDataSource();
            }
            else
            {
                this.Dispose();
                this.Close();
            }
            f.Dispose();
            GC.Collect();
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }
    }
}