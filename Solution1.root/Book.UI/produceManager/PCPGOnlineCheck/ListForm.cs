using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        int tag = 0;
        DataTable DT;
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.PCPGOnlineCheckManager();
        }

        public ListForm(string InvoiceCusId)
            : this()
        {
            this.tag = 1;
            DataTable dt = (this.manager as BL.PCPGOnlineCheckManager).SelectDetailByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, InvoiceCusId, null, null);

            this.bindingSource1.DataSource = dt;
            this.DT = dt.Copy();
            this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            //IList<Model.PCPGOnlineCheck> mPCPGlist = (this.manager as BL.PCPGOnlineCheckManager).SelectByDateRage(DateTime.Now.AddDays(-7), global::Helper.DateTimeParse.EndDate, null, null, "", "", "");
            ////BL.PCPGOnlineCheckDetailManager mPCPGDetailM = new Book.BL.PCPGOnlineCheckDetailManager();
            ////foreach (Model.PCPGOnlineCheck item in mPCPGlist)
            ////{
            ////    item.DescTime = mPCPGDetailM.GetTimerListString(item.PCPGOnlineCheckId);
            ////}
            //this.bindingSource1.DataSource = mPCPGlist;
            //this.gridView1.GroupPanelText = "默認顯示七天内的記錄";
            this.barBtnSearch_ItemClick(null, null);
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                DataTable dt = (this.manager as BL.PCPGOnlineCheckManager).SelectDetailByDateRage(condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd);
                
                this.bindingSource1.DataSource = dt;
                this.DT = dt.Copy();
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
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
            Model.PCPGOnlineCheck model = (this.manager as BL.PCPGOnlineCheckManager).Get((args[0] as DataRowView) == null ? null : (args[0] as DataRowView)[0].ToString());
            args[0] = model;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //string id = (this.bindingSource1.Current as DataRowView).Row[0].ToString();
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }

        private void che_HomeMade_CheckedChanged(object sender, EventArgs e)
        {
            //DT = this.bindingSource1.DataSource as DataTable;
            if (DT != null)
            {
                DataView dv = DT.DefaultView;
                if (this.che_HomeMade.Checked)
                {
                    this.che_Out.Checked = false;
                    dv.RowFilter = " FromId like '%pnt%'";
                    this.bindingSource1.DataSource = dv;
                }
                else
                    this.bindingSource1.DataSource = DT.Copy();
                this.gridControl1.RefreshDataSource();
                this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
            }
        }

        private void che_Out_CheckedChanged(object sender, EventArgs e)
        {
            //DT = this.bindingSource1.DataSource as DataTable;
            if (DT != null)
            {
                DataView dv = DT.DefaultView;
                if (this.che_Out.Checked)
                {
                    this.che_HomeMade.Checked = false;
                    dv.RowFilter = " FromId like '%poc%'";
                    this.bindingSource1.DataSource = dv;
                }
                else
                    this.bindingSource1.DataSource = DT.Copy();
                this.gridControl1.RefreshDataSource();
                this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
            }
        }
    }
}
