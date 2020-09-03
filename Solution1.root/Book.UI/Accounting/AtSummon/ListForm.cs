using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtSummon
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        BL.AtSummonDetailManager detailManager = new Book.BL.AtSummonDetailManager();

        public ListForm()
        {
            InitializeComponent();

            this.manager = new BL.AtSummonManager();
        }

        protected override void RefreshData()
        {
            //this.bindingSource1.DataSource = (this.manager as BL.AtSummonManager).SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate);
            this.bindingSource1.DataSource = detailManager.SelectByDateRangeAndSummary(DateTime.Now.AddMonths(-1), DateTime.Now, "");

            this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
        }

        private void barBtnChangeDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    Query.ConditionA condition = f.Condition as Query.ConditionA;
            //    this.bindingSource1.DataSource = (this.manager as BL.AtSummonManager).SelectByDateRage(condition.StartDate, condition.EndDate);
            //    this.gridControl1.RefreshDataSource();
            //}

            ListConditionForm f = new ListConditionForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var list = detailManager.SelectByDateRangeAndSummary(f.StartDate, f.EndDate, f.Summary);
                this.bindingSource1.DataSource = list;
                this.gridControl1.RefreshDataSource();
                this.barStaticItem1.Caption = list.Count.ToString() + "項";
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
            //Type type = typeof(EditForm);
            //return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);

            Type type = typeof(EditForm);
            Model.AtSummon model = (this.manager as BL.AtSummonManager).Get((args[0] as Model.AtSummonDetail) == null ? null : (args[0] as Model.AtSummonDetail).SummonId);
            args[0] = model;
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