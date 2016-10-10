using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
namespace Book.UI.Accounting.AtSummon
{
    public partial class EditForm : BaseEditForm
    {
        Model.AtSummon atSummon = new Book.Model.AtSummon();
        BL.AtSummonManager atSummonManager = new Book.BL.AtSummonManager();

        BL.AtSummonDetailManager atSummonDetailManager = new Book.BL.AtSummonDetailManager();

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtSummon.PRO_Id, new AA(Properties.Resources.RequireDataForId, this.textEditSummonId));
            this.requireValueExceptions.Add(Model.AtSummon.PRO_SummonCategory, new AA("請選擇傳票類別..", this.comboBoxEditSummonCategory));
            //this.requireValueExceptions.Add(Model.AtSummon.PRO_BIllCode, new AA("請輸入傳票詳細資料！", this.gridControl1 as Control));
            //this.invalidValueExceptions.Add(Model.AtSummon.PRO_SummonId, new AA(Properties.Resources.EntityExists, this.textEditSummonId));
            this.action = "insert";

            this.bindingSource2.DataSource = new BL.AtAccountSubjectManager().Select();
        }
        public EditForm(Model.AtSummon atSummon)
            : this()
        {
            this.atSummon = atSummon;
            this.atSummon.Details = this.atSummonDetailManager.Select(atSummon);
            this.action = "update";
        }
        public EditForm(Model.AtSummon atSummon, string action)
            : this()//*--
        {
            this.atSummon = atSummon;
            this.atSummon.Details = this.atSummonDetailManager.Select(atSummon);
            this.action = action;
        }
        protected override void Save()
        {
            this.atSummon.Id = this.textEditSummonId.Text;
            this.atSummon.SummonCategory = this.comboBoxEditSummonCategory.EditValue == null ? null : this.comboBoxEditSummonCategory.EditValue.ToString();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditSummonDate.DateTime, new DateTime()))
            {
                this.atSummon.SummonDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.atSummon.SummonDate = this.dateEditSummonDate.DateTime;
            }
            this.atSummon.TotalDebits = this.spinEditTotalDebits.EditValue == null ? 0 : decimal.Parse(this.spinEditTotalDebits.EditValue.ToString());
            this.atSummon.CreditTotal = this.spinEditCreditTotal.EditValue == null ? 0 : decimal.Parse(this.spinEditCreditTotal.EditValue.ToString());
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (this.comboBoxEditSummonCategory.SelectedIndex == 0)
            {
                if (this.spinEditTotalDebits.EditValue != null)
                {
                    if (this.spinEditTotalDebits.EditValue.ToString() != "0")
                    {
                        throw new global::Helper.MessageValueException("現金收入傳票借方金額必須為零,請檢查輸入是否有誤！");
                    }
                }
            }

            if (this.comboBoxEditSummonCategory.SelectedIndex == 1)
            {
                if (this.spinEditCreditTotal.EditValue != null)
                {
                    if (this.spinEditCreditTotal.EditValue.ToString() != "0")
                    {
                        throw new global::Helper.MessageValueException("現金收入傳票貸方金額必須為零,請檢查輸入是否有誤！");
                    }
                }
            }


            if (this.comboBoxEditSummonCategory.SelectedIndex == 2)
            {
                if (this.spinEditTotalDebits.EditValue != null && this.spinEditCreditTotal.EditValue != null)
                {
                    if (float.Parse(this.spinEditTotalDebits.EditValue.ToString()) != float.Parse(this.spinEditCreditTotal.EditValue.ToString()))
                    {
                        throw new global::Helper.MessageValueException("借貸雙方不平衡,請檢查輸入是否有誤！");
                    }
                }
            }
            switch (this.action)
            {
                case "insert":
                    this.atSummonManager.Insert(this.atSummon);
                    break;

                case "update":
                    this.atSummonManager.Update(this.atSummon);
                    break;
            }

        }
        protected override void Delete()
        {
            this.atSummonManager.Delete(this.atSummon.SummonId);
        }

        public override void Refresh()
        {
            if (this.atSummon == null)
            {
                this.atSummon = new Book.Model.AtSummon();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {

                    this.atSummon = this.atSummonManager.GetDetails(atSummon.SummonId);
                    if (this.atSummon == null)
                    {
                        this.AddNew();
                        this.action = "insert";
                    }

                }

            }
            this.textEditSummonId.Text = this.atSummon.Id;
            this.comboBoxEditSummonCategory.EditValue = this.atSummon.SummonCategory;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.atSummon.SummonDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditSummonDate.EditValue = null;
            }
            else
            {
                this.dateEditSummonDate.EditValue = this.atSummon.SummonDate;
            }
            this.spinEditCreditTotal.EditValue = this.atSummon.CreditTotal;
            this.spinEditTotalDebits.EditValue = this.atSummon.TotalDebits;
            this.bindingSource1.DataSource = this.atSummon.Details;

            switch (this.action)
            {
                case "insert":
                    this.textEditSummonId.Properties.ReadOnly = false;
                    this.comboBoxEditSummonCategory.Properties.ReadOnly = false;
                    this.dateEditSummonDate.Properties.ReadOnly = false;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2.Enabled = true;
                    this.spinEditTotalDebits.Properties.ReadOnly = false;
                    this.spinEditCreditTotal.Properties.ReadOnly = false;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditSummonId.Properties.ReadOnly = false;
                    this.comboBoxEditSummonCategory.Properties.ReadOnly = false;
                    this.dateEditSummonDate.Properties.ReadOnly = false;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.spinEditTotalDebits.Properties.ReadOnly = false;
                    this.spinEditCreditTotal.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditSummonId.Properties.ReadOnly = true;
                    this.comboBoxEditSummonCategory.Properties.ReadOnly = true;
                    this.dateEditSummonDate.Properties.ReadOnly = true;
                    this.simpleButton1.Enabled = false;

                    this.simpleButton2.Enabled = false;
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.spinEditTotalDebits.Properties.ReadOnly = true;
                    this.spinEditCreditTotal.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }

            base.Refresh();
        }
        protected override void MoveNext()
        {
            Model.AtSummon atSummon = this.atSummonManager.GetNext(this.atSummon);
            if (atSummon == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.atSummon = this.atSummonManager.Get(atSummon.SummonId);
        }

        protected override void MovePrev()
        {
            Model.AtSummon atSummon = this.atSummonManager.GetPrev(this.atSummon);
            if (atSummon == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.atSummon = this.atSummonManager.Get(atSummon.SummonId);
        }

        protected override void MoveFirst()
        {
            this.atSummon = this.atSummonManager.Get(this.atSummonManager.GetFirst() == null ? "" : this.atSummonManager.GetFirst().SummonId);
        }

        protected override void MoveLast()
        {
            //if (AtSummon == null)
            {
                this.atSummon = this.atSummonManager.Get(this.atSummonManager.GetLast() == null ? "" : this.atSummonManager.GetLast().SummonId);
            }
        }
        protected override bool HasRows()
        {
            return this.atSummonManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.atSummonManager.HasRowsAfter(this.atSummon);
        }

        protected override bool HasRowsPrev()
        {
            return this.atSummonManager.HasRowsBefore(this.atSummon);
        }

        protected override void AddNew()
        {
            
            this.atSummon = new Model.AtSummon();
            this.atSummon.SummonDate = DateTime.Now;
            this.atSummon.Id = this.atSummonManager.GetId();
            this.atSummon.Details = new List<Model.AtSummonDetail>();
            if (this.action == "insert")
            {
                Model.AtSummonDetail detail = new Model.AtSummonDetail();
                detail.Subject = new Book.Model.AtAccountSubject();
                this.atSummon.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.AtSummonDetail> details = this.bindingSource1.DataSource as IList<Model.AtSummonDetail>;
            //if (details == null || details.Count < 1) return;
            //Model.AtAccountSubject detail = details[e.ListSourceRowIndex].Subject;
            //switch (e.Column.Name)
            //{

            //    case "gridColumn2":
            //        if (detail == null) return;
            //        e.DisplayText = string.IsNullOrEmpty(detail.SubjectId) ? "" : detail.Id;
            //        break;
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Model.AtSummonDetail detail = new Model.AtSummonDetail();
            detail.Subject = new Book.Model.AtAccountSubject();
            this.atSummon.Details.Add(detail);
            this.gridControl1.RefreshDataSource();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.atSummon.Details.Remove(this.bindingSource1.Current as Book.Model.AtSummonDetail);

                if (this.atSummon.Details.Count == 0)
                {
                    Model.AtSummonDetail detail = new Model.AtSummonDetail();
                   
                    detail.Subject = new Book.Model.AtAccountSubject();
                    this.atSummon.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.AtSummonDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.AtSummonDetail;

            if (e.Column == this.gridColumn1)
            {
                if (detail.Lending == "貸" && this.comboBoxEditSummonCategory.SelectedIndex == 0)
                {
                }
                else
                {
                    detail.Lending = "借";
                }
                if (detail.Lending == "借" && this.comboBoxEditSummonCategory.SelectedIndex == 1)
                {

                }
                else 
                {
                    detail.Lending = "貸";
                }
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void comboBoxEditSummonCategory_EditValueChanged(object sender, EventArgs e)
        {

        }
       
    }
}