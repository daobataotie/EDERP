using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Accounting.AtBillsIncome
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        BL.AtBillsIncomeManager manager = new Book.BL.AtBillsIncomeManager();
        public ListForm()
        {
            InitializeComponent();
            this.bindingSourceSupplier.DataSource = new BL.SupplierManager().Select();
            this.bindingSourceCustomer.DataSource = new BL.CustomerManager().Select();
        }

        public Model.AtBillsIncome SelectItem { get { return this.bindingSource1.Current as Model.AtBillsIncome; } }

        private void barSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Condition c = f.condition;
                this.bindingSource1.DataSource = manager.SelectByCondition(c.KPStart, c.KPEnd, c.DQStart, c.DQEnd, c.YDStart, c.YDEnd, c.IdStart, c.IdEnd, c.Category, c.InvoiceState, c.BankNameStart, c.BankNameEnd, c.SupplierIdStart, c.SupplierIdEnd, c.CustomerIdStart, c.CustomerIdEnd);
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

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm2();
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm2);
            return (EditForm2)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        protected override void RefreshData()
        {
            this.barSearch_ItemClick(null, null);
            this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
            if (this.bindingSource1.DataSource != null)
                this.labelControl1.Text += (this.bindingSource1.DataSource as IList<Model.AtBillsIncome>).Sum(d => d.NotesMoney).Value.ToString("F2");
        }
    }
}