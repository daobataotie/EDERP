using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.produceManager.PCImpactCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        int tag = 0;
        IList<Model.PCImpactCheckDetail> detailList = new List<Model.PCImpactCheckDetail>();
        BL.PCImpactCheckDetailManager detailManager = new Book.BL.PCImpactCheckDetailManager();


        public ListForm()
        {
            InitializeComponent();
            //this.manager = new BL.PCImpactCheckManager();
        }

        public ListForm(string InvoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = this.detailList = this.detailManager.SelectByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, InvoiceCusId);
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = this.detailList = this.detailManager.SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, "");
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                this.bindingSource1.DataSource = this.detailList = this.detailManager.SelectByDateRage(condition.StartDate, condition.EndDate, condition.Product, condition.CusXOId);
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
            Model.PCImpactCheck model = new BL.PCImpactCheckManager().Get((args[0] as Model.PCImpactCheckDetail) == null ? null : (args[0] as Model.PCImpactCheckDetail).PCImpactCheckId);
            args[0] = model;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }

        private void che_HomeMade_CheckedChanged(object sender, EventArgs e)
        {
            if (this.detailList.Count > 0)
            {
                if (this.che_HomeMade.Checked)
                {
                    this.che_Out.Checked = false;
                    this.bindingSource1.DataSource = this.detailList.Where(d => d.PronoteHeaderId.Contains("PNT"));
                }
                else
                    this.bindingSource1.DataSource = this.detailList;
                this.gridControl1.RefreshDataSource();
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
            }
        }

        private void che_Out_CheckedChanged(object sender, EventArgs e)
        {
            if (this.detailList.Count > 0)
            {
                if (this.che_Out.Checked)
                {
                    this.che_HomeMade.Checked = false;
                    this.bindingSource1.DataSource = this.detailList.Where(d => d.PronoteHeaderId.Contains("POC"));
                }
                else
                    this.bindingSource1.DataSource = this.detailList;
                this.gridControl1.RefreshDataSource();
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //if (e.Column.Name == "gridColumn8")
            //{
            //    IList<Model.PCImpactCheckDetail> detail = this.bindingSource1.DataSource as List<Model.PCImpactCheckDetail>;
            //    if (detail != null && detail.Count > 0)
            //    {
            //        e.DisplayText = this.detailManager.SelectChecker(detailList[e.ListSourceRowIndex].PCImpactCheckId);
            //    }
            //}
        }
    }
}
