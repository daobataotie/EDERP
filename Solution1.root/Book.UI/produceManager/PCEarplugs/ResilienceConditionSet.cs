using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace Book.UI.produceManager.PCEarplugs
{
    public partial class ResilienceConditionSet : Settings.BasicData.BaseEditForm
    {
        Model.PCEarplugsResilienceConditionSet _pCEarplugsResilienceConditionSet;
        BL.PCEarplugsResilienceConditionSetManager manager = new Book.BL.PCEarplugsResilienceConditionSetManager();
        private string _PCEarplugsResilienceCheckId = string.Empty;

        public ResilienceConditionSet()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.PCEarplugsResilienceConditionSet.PRO_PCEarplugsResilienceCheckId, new AA("必須先保存本測試所依存的頭表", this.label1));

            this.action = "view";
        }

        public ResilienceConditionSet(string PCEarplugsResilienceCheckId)
            : this()
        {
            this._PCEarplugsResilienceCheckId = PCEarplugsResilienceCheckId;
        }

        protected override void AddNew()
        {
            this._pCEarplugsResilienceConditionSet = new Book.Model.PCEarplugsResilienceConditionSet();
            this._pCEarplugsResilienceConditionSet.PCEarplugsResilienceConditionSetId = Guid.NewGuid().ToString();
            this._pCEarplugsResilienceConditionSet.PCEarplugsResilienceCheckId = this._PCEarplugsResilienceCheckId;
        }

        protected override void Delete()
        {
            if (this._pCEarplugsResilienceConditionSet == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.manager.Delete(this._pCEarplugsResilienceConditionSet.PCEarplugsResilienceConditionSetId);
            this._pCEarplugsResilienceConditionSet = this.manager.mGetLast(this._PCEarplugsResilienceCheckId);

        }

        protected override void MoveLast()
        {
            //this._pCEarplugsResilienceConditionSet = this.manager.Get(this._OpticsTestManager.FGetLast(this._PCFinishCheckId) == null ? "" : this._OpticsTestManager.FGetLast(this._PCFinishCheckId).OpticsTestId);
            this._pCEarplugsResilienceConditionSet = this.manager.mGetLast(this._PCEarplugsResilienceCheckId);
        }

        protected override void MoveFirst()
        {
            //if (this._FromPcFinishCheck == 0)
            //{
            //    this._OpticsTest = this._OpticsTestManager.Get(this._OpticsTestManager.mGetFirst(this._PCPGOnlineCheckDetailId) == null ? "" : this._OpticsTestManager.mGetFirst(this._PCPGOnlineCheckDetailId).OpticsTestId);
            //}
            //else
            //{
            //    this._OpticsTest = this._OpticsTestManager.Get(this._OpticsTestManager.FGetFirst(this._PCFinishCheckId) == null ? "" : this._OpticsTestManager.FGetFirst(this._PCFinishCheckId).OpticsTestId);
            //}
        }

        protected override void MovePrev()
        {
            //Model.OpticsTest mOpticsTest = null;
            //if (this._FromPcFinishCheck == 0)
            //{
            //    mOpticsTest = this._OpticsTestManager.mGetPrev(this._OpticsTest.InsertTime.Value, this._PCPGOnlineCheckDetailId);
            //}
            //else
            //{
            //    mOpticsTest = this._OpticsTestManager.FGetPrev(this._OpticsTest.InsertTime.Value, this._PCFinishCheckId);
            //}

            //if (mOpticsTest == null)
            //    throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            //this._OpticsTest = this._OpticsTestManager.Get(mOpticsTest.OpticsTestId);
        }

        protected override void MoveNext()
        {
            //Model.OpticsTest mOpticsTest = null;
            //if (this._FromPcFinishCheck == 0)
            //{
            //    mOpticsTest = this._OpticsTestManager.mGetNext(this._OpticsTest.InsertTime.Value, this._PCPGOnlineCheckDetailId);
            //}
            //else
            //{
            //    mOpticsTest = this._OpticsTestManager.FGetNext(this._OpticsTest.InsertTime.Value, this._PCFinishCheckId);
            //}
            //if (mOpticsTest == null)
            //    throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            //this._OpticsTest = this._OpticsTestManager.Get(mOpticsTest.OpticsTestId);
        }

        protected override bool HasRows()
        {
            return this.manager.mHasRows(this._PCEarplugsResilienceCheckId);
        }

        protected override bool HasRowsNext()
        {
            //if (this._FromPcFinishCheck == 0)
            //{
            //    return this._OpticsTestManager.mHasRowsAfter(this._OpticsTest, this._PCPGOnlineCheckDetailId);
            //}
            //else
            //{
            //    return this._OpticsTestManager.FHasRowsAfter(this._OpticsTest, this._PCFinishCheckId);
            //}
            return false;
        }

        protected override bool HasRowsPrev()
        {
            //if (this._FromPcFinishCheck == 0)
            //{
            //    return this._OpticsTestManager.mHasRowsBefore(this._OpticsTest, this._PCPGOnlineCheckDetailId);
            //}
            //else
            //{
            //    return this._OpticsTestManager.FHasRowsBefore(this._OpticsTest, this._PCFinishCheckId);
            //}
            return false;
        }

        protected override void Save()
        {
            this._pCEarplugsResilienceConditionSet.TKY1 = this.spe_TKY1.Value;
            this._pCEarplugsResilienceConditionSet.TKY2 = this.spe_TKY2.Value;
            this._pCEarplugsResilienceConditionSet.TKY3 = this.spe_TKY3.Value;
            this._pCEarplugsResilienceConditionSet.TKY4 = this.spe_TKY4.Value;
            this._pCEarplugsResilienceConditionSet.TKY5 = this.spe_TKY5.Value;
            this._pCEarplugsResilienceConditionSet.TKY6 = this.spe_TKY6.Value;

            this._pCEarplugsResilienceConditionSet.SCR1 = this.spe_SCR1.Value;
            this._pCEarplugsResilienceConditionSet.SCR2 = this.spe_SCR2.Value;
            this._pCEarplugsResilienceConditionSet.SCR3 = this.spe_SCR3.Value;
            this._pCEarplugsResilienceConditionSet.SCR4 = this.spe_SCR4.Value;
            this._pCEarplugsResilienceConditionSet.SCR5 = this.spe_SCR5.Value;
            this._pCEarplugsResilienceConditionSet.SCR6 = this.spe_SCR6.Value;


            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._pCEarplugsResilienceConditionSet);
                    break;
                case "update":
                    this.manager.Update(this._pCEarplugsResilienceConditionSet);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._pCEarplugsResilienceConditionSet == null)
            {
                this.AddNew();
                this.action = "insert";
            }

            this.spe_TKY1.EditValue = this._pCEarplugsResilienceConditionSet.TKY1;
            this.spe_TKY2.EditValue = this._pCEarplugsResilienceConditionSet.TKY2;
            this.spe_TKY3.EditValue = this._pCEarplugsResilienceConditionSet.TKY3;
            this.spe_TKY4.EditValue = this._pCEarplugsResilienceConditionSet.TKY4;
            this.spe_TKY5.EditValue = this._pCEarplugsResilienceConditionSet.TKY5;
            this.spe_TKY6.EditValue = this._pCEarplugsResilienceConditionSet.TKY6;

            this.spe_SCR1.EditValue = this._pCEarplugsResilienceConditionSet.SCR1;
            this.spe_SCR2.EditValue = this._pCEarplugsResilienceConditionSet.SCR2;
            this.spe_SCR3.EditValue = this._pCEarplugsResilienceConditionSet.SCR3;
            this.spe_SCR4.EditValue = this._pCEarplugsResilienceConditionSet.SCR4;
            this.spe_SCR5.EditValue = this._pCEarplugsResilienceConditionSet.SCR5;
            this.spe_SCR6.EditValue = this._pCEarplugsResilienceConditionSet.SCR6;


            base.Refresh();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            //return new ROOpticsTest(_OpticsTest);

            return null;
        }

    }
}