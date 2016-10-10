using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.ANSIPCImpactCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        string ForANSIOrJIS = null;
        int _pcFlag = 0;
        int tag = 0;
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.ANSIPCImpactCheckManager();
        }
        public ListForm(string ForANSIOrJIS)
            : this()
        {
            if (ForANSIOrJIS == "JIS")
            {
                this.Text = "JIS衝擊單選擇";
                this.ColANSIImpackID.Caption = "JIS衝擊單編號";
                this._pcFlag = 1;
            }
            this.ForANSIOrJIS = ForANSIOrJIS;
        }

        public ListForm(string InvoiceCusId, int tag)
            : this()
        {
            this._pcFlag = tag;
            if (tag == 1)
            {
                this.Text = "JIS衝擊單選擇";
                this.ColANSIImpackID.Caption = "JIS衝擊單編號";
                this.tag = 1;
                this.bindingSource1.DataSource = (this.manager as BL.ANSIPCImpactCheckManager).SelectByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, InvoiceCusId, "JIS");
            }
            else if (tag == 0)
            {
                this.Text = "ANSI衝擊單選擇";
                this.ColANSIImpackID.Caption = "ANSI衝擊單編號";
                this.tag = 1;
                this.bindingSource1.DataSource = (this.manager as BL.ANSIPCImpactCheckManager).SelectByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, InvoiceCusId, "ANSI");
            }
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = (this.manager as BL.ANSIPCImpactCheckManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, null, "", this.ForANSIOrJIS);
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                this.bindingSource1.DataSource = (this.manager as BL.ANSIPCImpactCheckManager).SelectByDateRage(condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId, this.ForANSIOrJIS);
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
            args = new object[] { args[0], this._pcFlag };
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }
    }
}
