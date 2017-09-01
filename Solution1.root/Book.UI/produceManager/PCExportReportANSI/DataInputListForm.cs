using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DataInputListForm : Settings.BasicData.BaseListForm
    {
        BL.PCDataInputManager pcDataInputManager = new BL.PCDataInputManager();
        public DataInputListForm()
        {
            InitializeComponent();
        }

        protected override void RefreshData()
        {


        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new DataInputForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(DataInputForm);
            return (DataInputForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataInputConditionForm f = new DataInputConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.bindingSource1.DataSource = this.pcDataInputManager.SelectByCondition(f.DataInputCondition.StartDate, f.DataInputCondition.EndDate, f.DataInputCondition.PNTId, f.DataInputCondition.CusXOId, f.DataInputCondition.ProductId);
                this.gridControl1.RefreshDataSource();
            }
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }
    }
}