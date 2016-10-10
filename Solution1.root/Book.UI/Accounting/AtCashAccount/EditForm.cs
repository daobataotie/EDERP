using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Accounting.AtCashAccount
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        BL.AtCashAccountManager manager = new Book.BL.AtCashAccountManager();
        BL.AtCashAccountDetailManager detailManager = new Book.BL.AtCashAccountDetailManager();
        Model.AtCashAccount _atCashAccount;
        IList<Model.AtAccountSubject> subjectList;

        int flag = 0;
        public EditForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.AtCashAccount.PRO_AtCashAccountMonth, new AA("日期不能為空", this.data_Month));
            this.invalidValueExceptions.Add(Model.AtCashAccountDetail.PRO_SubjectId, new AA("科目不能為空", this.gridControl1));
            this.bindingSourceSubject.DataSource = subjectList = new BL.AtAccountSubjectManager().Select();
            this.action = "view";
        }

        protected override void AddNew()
        {
            this._atCashAccount = new Book.Model.AtCashAccount();
            this._atCashAccount.AtCashAccountId = Guid.NewGuid().ToString();
            this._atCashAccount.AtCashAccountMonth = DateTime.Now;

            if (this._atCashAccount.Detail == null)
                this._atCashAccount.Detail = new List<Model.AtCashAccountDetail>();
            Model.AtCashAccountDetail detail = new Book.Model.AtCashAccountDetail();
            detail.AtCashAccountDetailId = Guid.NewGuid().ToString();
            //detail.AtCashAccountDetaiDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM"));
            //Model.AtAccountSubject subject;
            //if (subjectList.Any(d => d.Id == "9999000"))
            //{
            //    subject = subjectList.First(d => d.Id == "9999000");
            //    detail.SubjectId = subject.SubjectId;
            //    if (this.HasRows())
            //        detail.Balance = this.manager.GetBalance();
            //    else
            //        detail.Balance = subject.TheBalance;
            //}
            this._atCashAccount.Detail.Add(detail);
            //this._atCashAccount.BalanceTotal = detail.Balance;
            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._atCashAccount == null)
                this.AddNew();
            else
                if (this.action == "view")
                    this._atCashAccount = this.manager.GetDetail(this._atCashAccount.AtCashAccountId);
            this.data_Month.EditValue = this._atCashAccount.AtCashAccountMonth;
            this.spe_IncomeTotal.EditValue = this._atCashAccount.IncomeTotal;
            this.spe_PayTotal.EditValue = this._atCashAccount.PayTotal;
            this.spe_BalanceTotal.EditValue = this._atCashAccount.BalanceTotal;
            this.bindingSourceDetail.DataSource = this._atCashAccount.Detail;
            base.Refresh();
            this.spe_IncomeTotal.Properties.ReadOnly = true;
            this.spe_PayTotal.Properties.ReadOnly = true;
            this.spe_BalanceTotal.Properties.ReadOnly = true;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();
            if (this.data_Month.EditValue != null)
                this._atCashAccount.AtCashAccountMonth = this.data_Month.DateTime;
            this._atCashAccount.IncomeTotal = this.spe_IncomeTotal.Value;
            this._atCashAccount.PayTotal = this.spe_PayTotal.Value;
            this._atCashAccount.BalanceTotal = this.spe_BalanceTotal.Value;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._atCashAccount);
                    break;
                case "update":
                    this.manager.Update(this._atCashAccount);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._atCashAccount == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            Model.AtCashAccount model = this.manager.GetNext(this._atCashAccount);
            this.manager.Delete(this._atCashAccount.AtCashAccountId);
            if (model == null)
                this._atCashAccount = this.manager.GetLast();
            else
                this._atCashAccount = model;
        }

        protected override void MoveFirst()
        {
            this._atCashAccount = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.flag == 1)
            {
                this.flag = 0;
                return;
            }
            this._atCashAccount = this.manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.AtCashAccount model = this.manager.GetNext(this._atCashAccount);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._atCashAccount = model;
        }

        protected override void MovePrev()
        {
            Model.AtCashAccount model = this.manager.GetPrev(this._atCashAccount);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._atCashAccount = model;
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._atCashAccount);
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._atCashAccount);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._atCashAccount);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Model.AtCashAccountDetail model = new Book.Model.AtCashAccountDetail();
            model.AtCashAccountDetailId = Guid.NewGuid().ToString();
            //model.AtCashAccountDetaiDate = DateTime.Now;

            this.bindingSourceDetail.Add(model);
            this.gridControl1.RefreshDataSource();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            //if (this.bindingSourceDetail.Current != null && this.bindingSourceDetail.Position != 0)
            if (this.bindingSourceDetail.Current != null)
            {
                //this.gridView1.SetRowCellValue(bindingSourceDetail.Count - 1, this.gridColumn7, Convert.ToDecimal(this.spe_BalanceTotal.Value) + Convert.ToDecimal(this.gridView1.GetRowCellValue(bindingSourceDetail.Position, this.gridColumn6)) - Convert.ToDecimal(this.gridView1.GetRowCellValue(bindingSourceDetail.Position, this.gridColumn5)));

                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
            }
            //else
            //{
            //    MessageBox.Show("當前記錄不可刪除", this.Text, MessageBoxButtons.OK);
            //    return;
            //}
            this.CountTotal();
            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle > 0)
            {
                //if (e.Column.Name == "gridColumn5")
                //{
                //    this.gridView1.SetRowCellValue(e.RowHandle, gridColumn7, Convert.ToDecimal(this.gridView1.GetRowCellValue(e.RowHandle, gridColumn5)) + Convert.ToDecimal(this.gridView1.GetRowCellValue(e.RowHandle - 1, gridColumn7)) - Convert.ToDecimal(this.gridView1.GetRowCellValue(e.RowHandle, gridColumn6)));
                //}
                //if (e.Column.Name == "gridColumn6")
                //{
                //    this.gridView1.SetRowCellValue(e.RowHandle, gridColumn7, Convert.ToDecimal(this.gridView1.GetRowCellValue(e.RowHandle, gridColumn5)) + Convert.ToDecimal(this.gridView1.GetRowCellValue(e.RowHandle - 1, gridColumn7)) - Convert.ToDecimal(this.gridView1.GetRowCellValue(e.RowHandle, gridColumn6)));
                //}
                this.CountTotal();
            }
        }

        private void CountTotal()
        {
            decimal income = 0;
            decimal pay = 0;
            foreach (var item in this._atCashAccount.Detail)
            {
                income += Convert.ToDecimal(item.Income);
                pay += Convert.ToDecimal(item.Pay);
            }
            this.spe_IncomeTotal.EditValue = income;
            this.spe_PayTotal.EditValue = pay;
            this.spe_BalanceTotal.EditValue = income - pay;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ROList r = new ROList(f.Condition as Query.ConditionA);
                r.ShowPreviewDialog();
            }
        }
    }
}