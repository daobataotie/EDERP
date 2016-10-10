using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.ProduceOtherExitMaterial
{
    public partial class ChooseOtherExitMaterial : DevExpress.XtraEditors.XtraForm
    {
        private BL.ProduceOtherExitMaterialManager headerManager = new Book.BL.ProduceOtherExitMaterialManager();
        private BL.ProduceOtherExitDetailManager detailManager = new Book.BL.ProduceOtherExitDetailManager();
        public IList<Model.ProduceOtherExitDetail> detailList;
        public IList<Model.ProduceOtherExitMaterial> HeaderList;

        private IList<Model.ProduceOtherExitDetail> selectItems = new List<Model.ProduceOtherExitDetail>();

        public IList<Model.ProduceOtherExitDetail> SelectItems
        {
            get { return selectItems; }
            set { selectItems = value; }
        }

        public ChooseOtherExitMaterial()
        {
            InitializeComponent();
            this.dateEditstartdate.DateTime = DateTime.Now.AddMonths(-1);
            this.dateEditenddate.DateTime = DateTime.Now;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void spb_search_Click(object sender, EventArgs e)
        {
            HeaderList = headerManager.SelectByDateRange(this.dateEditstartdate.EditValue == null ? DateTime.Now.AddMonths(-1) : this.dateEditstartdate.DateTime, this.dateEditenddate.EditValue == null ? DateTime.Now : this.dateEditenddate.DateTime);


            this.bindingSourceProduceOtherMaterial.DataSource = HeaderList;
            if (this.bindingSourceProduceOtherMaterial.Current != null)
            {
                detailList = this.detailManager.Select(this.bindingSourceProduceOtherMaterial.Current as Model.ProduceOtherExitMaterial);
                this.bindingSourceProduceOtherMaterialDetails.DataSource = detailList;
            }
        }

        private void sbtn_sure_Click(object sender, EventArgs e)
        {
            this.gridView2.PostEditor();
            this.gridView2.UpdateCurrentRow();
            if (this.SelectItems.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }


        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumnCheck")
            {
                Model.ProduceOtherExitDetail detail = this.gridView2.GetRow(e.RowHandle) as Model.ProduceOtherExitDetail;

                if ((bool)e.Value)
                {
                    this.SelectItems.Add(detail);
                }
                if (!(bool)e.Value && selectItems.Any(d=>d.ProduceOtherExitDetailId==detail.ProduceOtherExitDetailId))
                {
                    this.SelectItems.Remove(selectItems.First<Model.ProduceOtherExitDetail>(d=>d.ProduceOtherExitDetailId==detail.ProduceOtherExitDetailId));
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (this.bindingSourceProduceOtherMaterial.Current != null)
            {
                detailList = this.detailManager.Select(this.bindingSourceProduceOtherMaterial.Current as Model.ProduceOtherExitMaterial);
                foreach (Model.ProduceOtherExitDetail detail in detailList)
                {
                    if (selectItems.Count != 0 && SelectItems.Any(d => d.ProduceOtherExitDetailId == detail.ProduceOtherExitDetailId))
                        detail.IsChecked = true;
                }
                this.bindingSourceProduceOtherMaterialDetails.DataSource = detailList;
            }
        }
        private void ChooseProduceMaterialExit_Load(object sender, EventArgs e)
        {
            HeaderList = this.headerManager.SelectByDateRange(DateTime.Now.AddMonths(-1), DateTime.Now);
            this.bindingSourceProduceOtherMaterial.DataSource = HeaderList;
        }
    }
}