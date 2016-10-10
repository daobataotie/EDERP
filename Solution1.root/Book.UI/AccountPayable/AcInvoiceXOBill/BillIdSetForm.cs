using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class BillIdSetForm : Settings.BasicData.BaseEditForm
    {
        Model.BillIdSet _billIdSet;
        IList<Model.BillIdSet> ListBillIdSet;
        BL.BillIdSetManager manager = new Book.BL.BillIdSetManager();

        public BillIdSetForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.BillIdSet.PRO_StartDate, new AA("有效期區間不完整", this.date_StartDate));
            this.invalidValueExceptions.Add(Model.BillIdSet.PRO_EndDate, new AA("有效期區間不完整", this.date_EndDate));

            this.invalidValueExceptions.Add(Model.BillIdSet.PRO_EndBillId, new AA("結束編號不能為空", this.txt_EndBillId));
            this.invalidValueExceptions.Add(Model.BillIdSet.PRO_StartBillId, new AA("起始編號不能為空", this.txt_StartBillId));
            this.invalidValueExceptions.Add(Model.BillIdSet.PRO_EnglishId, new AA("英文字軌不能為空", this.txt_EnglishId));
            this.requireValueExceptions.Add(Model.BillIdSet.PRO_EnglishId + "2", new AA("英文字軌長度有誤", this.txt_EnglishId));
            this.requireValueExceptions.Add(Model.BillIdSet.PRO_EndBillId + "2", new AA("發票編號區間有誤，請檢查", this.txt_EndBillId));

            this.action = "view";
        }

        protected override void AddNew()
        {
            this._billIdSet = new Book.Model.BillIdSet();
            this._billIdSet.BillIdSetId = Guid.NewGuid().ToString();
            this._billIdSet.IdState = true;
            this._billIdSet.IdNumber = 0;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._billIdSet);
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._billIdSet);
        }

        protected override void MoveFirst()
        {
            this._billIdSet = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._billIdSet = this.manager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.BillIdSet p = this.manager.GetPrev(this._billIdSet);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._billIdSet = p;
        }

        protected override void MoveNext()
        {
            Model.BillIdSet p = this.manager.GetNext(this._billIdSet);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._billIdSet = p;
        }

        protected override void Save()
        {
            //this.gridView1.PostEditor();
            //this.gridView1.UpdateCurrentRow();
            this._billIdSet.EnglishId = this.txt_EnglishId.Text;
            this._billIdSet.EndBillId = this.txt_EndBillId.Text;
            this._billIdSet.StartBillId = this.txt_StartBillId.Text;
            if (this.date_StartDate.EditValue == null)
                this._billIdSet.StartDate = null;
            else
                this._billIdSet.StartDate = this.date_StartDate.DateTime;
            if (this.date_EndDate.EditValue == null)
                this._billIdSet.EndDate = null;
            else
                this._billIdSet.EndDate = this.date_EndDate.DateTime;
            this._billIdSet.IdState = this.checkEditIdState.Checked;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._billIdSet);
                    break;
                case "update":
                    this.manager.Update(this._billIdSet);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._billIdSet == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._billIdSet = this.manager.Get(this._billIdSet.BillIdSetId);
            }
            this.txt_EnglishId.Text = this._billIdSet.EnglishId;
            this.txt_StartBillId.Text = this._billIdSet.StartBillId;
            this.txt_EndBillId.Text = this._billIdSet.EndBillId;
            this.date_StartDate.EditValue = this._billIdSet.StartDate;
            this.date_EndDate.EditValue = this._billIdSet.EndDate;
            this.checkEditIdState.Checked = this._billIdSet.IdState.HasValue ? this._billIdSet.IdState.Value : false;

            this.bindingSource1.DataSource = this.ListBillIdSet = this.manager.SelectAll();
            base.Refresh();
            //不能删除，防止发生一些编号生成错误
            this.CannotDelete();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.txt_EndBillId.Enabled = true;
                    this.txt_EnglishId.Enabled = true;
                    this.txt_StartBillId.Enabled = true;
                    break;
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.txt_EndBillId.Enabled = true;
                    this.txt_EnglishId.Enabled = true;
                    this.txt_StartBillId.Enabled = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.txt_EndBillId.Enabled = false;
                    this.txt_EnglishId.Enabled = false;
                    this.txt_StartBillId.Enabled = false;
                    break;
            }

        }

        protected override void Delete()
        {
            if (this._billIdSet == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.BillIdSet model = this.manager.GetNext(this._billIdSet);
                this.manager.Delete(this._billIdSet.BillIdSetId);
                if (model == null)
                    this._billIdSet = this.manager.GetLast();
                else
                    this._billIdSet = model;
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                Model.BillIdSet model = this.bindingSource1.Current as Model.BillIdSet;
                if (model != null)
                {
                    this._billIdSet = model;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

        private void txt_EnglishId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_StartBillId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_EndBillId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}