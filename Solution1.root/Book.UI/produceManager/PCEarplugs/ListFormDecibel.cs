using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.PCEarplugs
{
    public partial class ListFormDecibel : Book.UI.Settings.BasicData.BaseListForm
    {
        BL.PCEarplugsDecibelCheckDetailManager _pCEarplugsDecibelCheckDetailManager = new Book.BL.PCEarplugsDecibelCheckDetailManager();

        public ListFormDecibel()
        {
            InitializeComponent();
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = _pCEarplugsDecibelCheckDetailManager.SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, null);
            barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);

            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.bindingSource1.DataSource = _pCEarplugsDecibelCheckDetailManager.SelectByDateRage(f.StartDate, f.EndDate, f.ProductId, f.CusXOId);
                this.gridControl1.RefreshDataSource();
                barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
            }
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditFormDecibel();
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditFormDecibel);
            Model.PCEarplugsDecibelCheckDetail model = args[0] as Model.PCEarplugsDecibelCheckDetail;
            if (model != null)
                args[0] = model.PCEarplugsDecibelCheckId;

            return (EditFormDecibel)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }
    }
}