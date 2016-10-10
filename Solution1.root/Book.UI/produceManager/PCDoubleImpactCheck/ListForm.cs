using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.PCDoubleImpactCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        private int _pcFlag = 0;     //表示检测单据类型
        int tag = 0;

        public ListForm(int pcFlag, string text)
        {
            InitializeComponent();
            this.Text = text;
            this._pcFlag = pcFlag;
            this.manager = new BL.PCDoubleImpactCheckManager();
        }

        public ListForm(string InvoiceCusId, int pcFlag)
        {
            InitializeComponent();
            this.manager = new BL.PCDoubleImpactCheckManager();
            tag = 1;
            if (pcFlag == 0)
            {
                this.Text = "CSA衝擊測試單";
                this._pcFlag = pcFlag;
            }
            else if (pcFlag == 1)
            {
                this.Text = "BS/EN衝擊測試單";
                this._pcFlag = pcFlag;
            }
            else if (pcFlag == 2)
            {
                this.Text = "AS/NZS衝擊測試單";
                this._pcFlag = pcFlag;
            }
            this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, this._pcFlag, null, null, InvoiceCusId);
            this.manager = new BL.PCDoubleImpactCheckManager();
            this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
        }

        protected override void RefreshData()
        {
            if (tag == 1)
            {
                tag = 0;
                return;
            }
            this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, this._pcFlag, null, null, "");
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(condition.StartDate, condition.EndDate, this._pcFlag, condition.Product, condition.Customer, condition.CusXOId);
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            //if (tag == 1)
            //{
            //    tag = 0;
            //    return;
            //}
            //this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, this._pcFlag, null, null, "");
            //this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
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
