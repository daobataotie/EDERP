using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceOtherInDepot
{
    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人:  裴盾              完成时间:2010-02-28
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        BL.ProduceOtherInDepotDetailManager detailManager = new Book.BL.ProduceOtherInDepotDetailManager();
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.ProduceOtherInDepotManager();
        }

        protected override void RefreshData()
        {
            //this.bindingSource1.DataSource = (this.manager as BL.ProduceOtherInDepotManager).SelectByCondition(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, null, null, null, null, null, null, null, null);
            this.bindingSource1.DataSource = detailManager.SelectByCondition(DateTime.Now.AddMonths(-1), DateTime.Now, null, null, null, null, null, null, null, null);
            this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
        }
        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new Editform();
        }
        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(Editform);
            Model.ProduceOtherInDepot model = new BL.ProduceOtherInDepotManager().Get((args[0] as Model.ProduceOtherInDepotDetail) == null ? null : (args[0] as Model.ProduceOtherInDepotDetail).ProduceOtherInDepotId);
            args[0] = model;
            return (Editform)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);

        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionOtherInDepotChooseForm form = new Book.UI.Query.ConditionOtherInDepotChooseForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                Query.ConditionOtherInDepot condition = form.Condition as Query.ConditionOtherInDepot;

                //this.bindingSource1.DataSource = (this.manager as BL.ProduceOtherInDepotManager).SelectByCondition(condition.StartDate, condition.EndDate, condition.Supplier1, condition.Supplier2, condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.Product1, condition.Product2, condition.InvouceCusIdStart, condition.InvoiceCusIdEnd);
                this.bindingSource1.DataSource = detailManager.SelectByCondition(condition.StartDate, condition.EndDate, condition.Supplier1, condition.Supplier2, condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.Product1, condition.Product2, condition.InvouceCusIdStart, condition.InvoiceCusIdEnd);
                this.gridControl1.RefreshDataSource();
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
            }
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Form f = this.GetEditForm(new object[] { this.bindingSource1.Current == null ? null : (this.bindingSource1.Current as Model.ProduceOtherInDepotDetail).ProduceOtherInDepotId, "view" });
            //if (f != null)
            //    f.ShowDialog();
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }
    }
}