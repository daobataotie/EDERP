using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.produceManager.PCFogCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.PCFogCheckManager();

            this.gridColumn10.Visible = false;
            this.btn_OK.Visible = false;
        }

        /// <summary>
        /// 用于入料检验单选择雾都测试单
        /// </summary>
        /// <param name="ShowCheck"></param>
        public ListForm(bool ShowCheck)
            : this()
        {
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.btn_OK.Visible = true;
        }


        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.PCFogCheckManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, null, "");
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                this.bindingSource1.DataSource = (this.manager as BL.PCFogCheckManager).SelectByDateRage(condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId);
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

        public string PCFogCheckId { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            IList<Model.PCFogCheck> list = this.bindingSource1.DataSource as IList<Model.PCFogCheck>;
            if (list != null)
            {
                Model.PCFogCheck pcFogCheck = list.FirstOrDefault(P => P.IsChecked == true);
                if (pcFogCheck != null)
                    this.PCFogCheckId = pcFogCheck.PCFogCheckId;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
