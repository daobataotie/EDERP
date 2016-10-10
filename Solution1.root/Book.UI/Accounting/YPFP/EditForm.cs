using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.YPFP
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.ShouldPayAccountDetail _shouldPayAccountDetail;
        BL.ShouldPayAccountDetailManager shouldPayAccountDetailManager = new Book.BL.ShouldPayAccountDetailManager();
        BL.CompanyManager companyManager = new Book.BL.CompanyManager();

        public EditForm()
        {
            InitializeComponent();
            this.action = "view";
            this.nccSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.bindingSourceFPHeader.DataSource = companyManager.Select();
        }

        protected override void AddNew()
        {
            this._shouldPayAccountDetail = new Book.Model.ShouldPayAccountDetail();
            this._shouldPayAccountDetail.ShouldPayAccountDetailId = Guid.NewGuid().ToString();
            this._shouldPayAccountDetail.FPDate = DateTime.Now;
            this.action = "insert";
        }

        protected override void Save()
        {
            this._shouldPayAccountDetail.FPDate = this.date_YFFP.EditValue == null ? DateTime.Now : this.date_YFFP.DateTime;
            this._shouldPayAccountDetail.FPId = this.txt_FPId.Text;
            this._shouldPayAccountDetail.FPSupplierId = (this.nccSupplier.EditValue as Model.Supplier) == null ? null : (this.nccSupplier.EditValue as Model.Supplier).SupplierId;
            this._shouldPayAccountDetail.FPHeader = this.lookUpEdit1.EditValue == null ? null : this.lookUpEdit1.EditValue.ToString();
            this._shouldPayAccountDetail.FPMoney = this.spe_Money.Value;
            this._shouldPayAccountDetail.FPTax = this.spe_Tax.Value;
            this._shouldPayAccountDetail.FPTotalMoney = this.spe_TotalMoney.Value;

            switch (this.action)
            {
                case "insert":
                    this.shouldPayAccountDetailManager.Insert(this._shouldPayAccountDetail);
                    break;
                case "update":
                    this.shouldPayAccountDetailManager.Update(this._shouldPayAccountDetail);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._shouldPayAccountDetail == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.shouldPayAccountDetailManager.Delete(this._shouldPayAccountDetail.ShouldPayAccountDetailId);
                MessageBox.Show("刪除成功!");
            }
            Model.ShouldPayAccountDetail model = this.shouldPayAccountDetailManager.GetNext(this._shouldPayAccountDetail);
            if (model == null)
                this._shouldPayAccountDetail = this.shouldPayAccountDetailManager.GetLast();
            else
                this._shouldPayAccountDetail = model;
        }

        public override void Refresh()
        {
            if (this._shouldPayAccountDetail == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._shouldPayAccountDetail = this.shouldPayAccountDetailManager.Get(this._shouldPayAccountDetail.ShouldPayAccountDetailId);
            }

            this.date_YFFP.EditValue = this._shouldPayAccountDetail.FPDate;
            this.txt_FPId.EditValue = this._shouldPayAccountDetail.FPId;
            this.nccSupplier.EditValue = this._shouldPayAccountDetail.FPSupplier;
            this.lookUpEdit1.EditValue = this._shouldPayAccountDetail.FPHeader;
            this.spe_Money.EditValue = this._shouldPayAccountDetail.FPMoney == null ? 0 : this._shouldPayAccountDetail.FPMoney.Value;
            this.spe_Tax.EditValue = this._shouldPayAccountDetail.FPTax == null ? 0 : this._shouldPayAccountDetail.FPTax.Value;
            this.spe_TotalMoney.EditValue = this._shouldPayAccountDetail.FPTotalMoney == null ? 0 : this._shouldPayAccountDetail.FPTotalMoney.Value;
            base.Refresh();
        }

        protected override void MoveNext()
        {
            Model.ShouldPayAccountDetail model = this.shouldPayAccountDetailManager.GetNext(this._shouldPayAccountDetail);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._shouldPayAccountDetail = this.shouldPayAccountDetailManager.Get(model.ShouldPayAccountDetailId);
        }

        protected override void MovePrev()
        {
            Model.ShouldPayAccountDetail model = this.shouldPayAccountDetailManager.GetPrev(this._shouldPayAccountDetail);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._shouldPayAccountDetail = this.shouldPayAccountDetailManager.Get(model.ShouldPayAccountDetailId);
        }

        protected override void MoveFirst()
        {
            this._shouldPayAccountDetail = this.shouldPayAccountDetailManager.Get(this.shouldPayAccountDetailManager.GetFirst() == null ? "" : this.shouldPayAccountDetailManager.GetFirst().ShouldPayAccountDetailId);
        }

        protected override void MoveLast()
        {
            this._shouldPayAccountDetail = this.shouldPayAccountDetailManager.Get(this.shouldPayAccountDetailManager.GetLast() == null ? "" : this.shouldPayAccountDetailManager.GetLast().ShouldPayAccountDetailId);
        }

        protected override bool HasRows()
        {
            return this.shouldPayAccountDetailManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.shouldPayAccountDetailManager.HasRowsAfter(this._shouldPayAccountDetail);
        }

        protected override bool HasRowsPrev()
        {
            return this.shouldPayAccountDetailManager.HasRowsBefore(this._shouldPayAccountDetail);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Query.ROFP(this._shouldPayAccountDetail);
        }

        private void spe_Money_EditValueChanged(object sender, EventArgs e)
        {
            this.spe_Tax.EditValue = Convert.ToDouble(this.spe_Money.Value) * 0.05;
            this.spe_TotalMoney.EditValue = Convert.ToDouble(this.spe_Money.Value) * 1.05;
        }

        private void spe_Tax_EditValueChanged(object sender, EventArgs e)
        {
            this.spe_TotalMoney.EditValue = Convert.ToDouble(this.spe_Money.Value) + Convert.ToDouble(this.spe_Tax.Value);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._shouldPayAccountDetail = f.SelectItem;
                this.Refresh();
            }
        }
    }
}