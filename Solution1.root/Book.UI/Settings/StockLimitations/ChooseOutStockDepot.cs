using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using System.Linq;

namespace Book.UI.Settings.StockLimitations
{
    public partial class ChooseOutStockDepot : BaseChooseForm
    {

        #region
        private BL.DepotOutDetailManager depotOutDetailManager = new BL.DepotOutDetailManager();

        #endregion
        IList<Model.DepotOutDetail> list = new List<Model.DepotOutDetail>();
        int tag = 0;
        public ChooseOutStockDepot()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = System.DateTime.Now.Date.AddDays(-30);
            this.dateEditEndate.DateTime = System.DateTime.Now.Date;
            //this.manager = new BL.DepotOutManager();
        }

        public ChooseOutStockDepot(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = this.depotOutDetailManager.SelectByDateRange(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, invoiceCusId);
            this.gridControl1.RefreshDataSource();
        }

        protected override void LoadData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = list = this.depotOutDetailManager.SelectByDateRange(this.dateEditStartDate.EditValue == null ? DateTime.Now.AddMonths(-1) : this.dateEditStartDate.DateTime, this.dateEditEndate.EditValue == null ? DateTime.Now : this.dateEditEndate.DateTime, (this.buttonEditProduct.EditValue as Model.Product) == null ? "" : (this.buttonEditProduct.EditValue as Model.Product).ProductId, this.txt_InvoiceCusID.Text);
            this.gridControl1.RefreshDataSource();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new OutStockEditForm();
        }

        private void Button_SearCh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void buttonEditProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.buttonEditProduct.EditValue = f.SelectedItem as Model.Product;
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Model.DepotOutDetail detail = this.bindingSource1.Current as Model.DepotOutDetail;
            if (detail != null)
            {
                OutStockEditForm f = new OutStockEditForm(detail.DepotOutId);
                f.Show(this);
            }
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in this.list)
            {
                item.IsChecked = this.checkAll.Checked;
            }
            this.gridControl1.RefreshDataSource();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            Query.OutDepotDetail2 f = new Book.UI.Query.OutDepotDetail2(list.Where(d => d.IsChecked == true).ToList(), this.dateEditStartDate.DateTime, this.dateEditEndate.DateTime);
            f.ShowPreviewDialog();
        }
    }
}