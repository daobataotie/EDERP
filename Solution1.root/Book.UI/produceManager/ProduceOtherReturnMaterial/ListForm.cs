using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;

namespace Book.UI.produceManager.ProduceOtherReturnMaterial
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.ProduceOtherReturnMaterialManager();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionAChooseForm condition = new ConditionAChooseForm();
            if (condition.ShowDialog(this) == DialogResult.OK)
            {
                ConditionA con = condition.Condition as ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.ProduceOtherReturnMaterialManager).Select(con.StartDate, con.EndDate);
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
                this.gridControl1.RefreshDataSource();
            }
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }
        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current, "view" });
            if (f != null)
                f.ShowDialog();
        }
    }
}