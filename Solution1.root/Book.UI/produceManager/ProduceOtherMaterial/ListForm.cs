using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceOtherMaterial
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  裴盾              完成时间:2010-03-2
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        int tag = 0;
        BL.ProduceOtherMaterialDetailManager detailManager = new Book.BL.ProduceOtherMaterialDetailManager();
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.ProduceOtherMaterialManager();
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            list = detailManager.SelectByConditionRange(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, null, null, null, null, invoiceCusId);
            this.bindingSource1.DataSource = list;
            this.gridControl1.RefreshDataSource();
        }
        IList<Model.ProduceOtherMaterialDetail> list = new List<Model.ProduceOtherMaterialDetail>();
        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            list = detailManager.SelectByConditionRange(DateTime.Now.AddMonths(-1), DateTime.Now, null, null, null, null, null, null, null);
            this.bindingSource1.DataSource = list;
            this.gridView1.GroupPanelText = "默認顯示一個月的內容";
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            Model.ProduceOtherMaterial model = (new BL.ProduceOtherMaterialManager()).Get((args[0] as Model.ProduceOtherMaterialDetail).ProduceOtherMaterialId);
            args[0] = model;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current, "view" });
            if (f != null)
                f.ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseForm f = new ChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ChooseCondition condition = f.condition;
                list = detailManager.SelectByConditionRange(condition.StartDate, condition.EndDate, condition.SupplierStartId, condition.SupplierEndId, condition.ProduceOtherCompactStartId, condition.ProduceOtherCompactEndId, condition.ProductStartId, condition.ProductEndId, condition.InvoiceCusID);
                this.bindingSource1.DataSource = list;
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}