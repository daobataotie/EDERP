using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProductOnlineCheck
{
    public partial class List : Settings.BasicData.BaseListForm
    {
        int tag = 0;
        public List()
        {
            InitializeComponent();
            this.manager = new BL.ProductOnlineCheckManager();
        }

        public List(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = ((BL.ProductOnlineCheckManager)this.manager).SelectByDate(DateTime.Now.AddDays(-30), DateTime.Now, invoiceCusId);
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = ((BL.ProductOnlineCheckManager)this.manager).SelectByDate(DateTime.Now.AddDays(-30), DateTime.Now, null);
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new Editform();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(Editform);
            return (Editform)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condition = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = ((BL.ProductOnlineCheckManager)this.manager).SelectByDate(condition.StartDate, condition.EndDate, null);
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}