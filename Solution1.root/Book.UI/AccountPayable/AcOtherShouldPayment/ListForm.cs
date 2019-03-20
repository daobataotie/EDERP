using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.AccountPayable.AcOtherShouldPayment
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.AcOtherShouldPaymentManager();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new AccountPayable.AcOtherShouldPayment.EditForm();
        }

        protected override void RefreshData()
        {
            //this.bindingSource1.DataSource = (this.manager as BL.AcOtherShouldPaymentManager).SelectByDateRangeAndSupCompany(DateTime.Now.AddMonths(-1), DateTime.Now, null);

            this.bar_Search_ItemClick(null, null);
        }

        public Model.AcOtherShouldPayment SelectItem
        {
            get { return this.bindingSource1.Current as Model.AcOtherShouldPayment; }
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        private void bar_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.bindingSource1.DataSource = (this.manager as BL.AcOtherShouldPaymentManager).SelectByDateRangeAndSupCompany(f.DateStart, f.DateEnd, f.Supplier, null);

                this.gridControl1.RefreshDataSource();
            }
        }
    }
}