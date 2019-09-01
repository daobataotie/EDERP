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
    public partial class ResilienceConditionWeight : Settings.BasicData.BaseEditForm
    {
        Model.PCEarplugsResilienceWeight _pCEarplugsResilienceWeight;
        BL.PCEarplugsResilienceWeightManager manager = new Book.BL.PCEarplugsResilienceWeightManager();
        private string _PCEarplugsResilienceCheckDetailId = string.Empty;

        public ResilienceConditionWeight()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.PCEarplugsResilienceConditionSet.PRO_PCEarplugsResilienceCheckId, new AA("必須先保存本測試所依存的頭表", this.label1));

            this.action = "view";
        }

        public ResilienceConditionWeight(string PCEarplugsResilienceCheckDetailId)
            : this()
        {
            this._PCEarplugsResilienceCheckDetailId = PCEarplugsResilienceCheckDetailId;
        }

        protected override void AddNew()
        {
            this._pCEarplugsResilienceWeight = new Book.Model.PCEarplugsResilienceWeight();
            this._pCEarplugsResilienceWeight.PCEarplugsResilienceWeightId = Guid.NewGuid().ToString();
            this._pCEarplugsResilienceWeight.PCEarplugsResilienceCheckId = this._PCEarplugsResilienceCheckDetailId;
        }

        protected override void Delete()
        {
            if (this._pCEarplugsResilienceWeight == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.manager.Delete(this._pCEarplugsResilienceWeight.PCEarplugsResilienceWeightId);
            this._pCEarplugsResilienceWeight = this.manager.mGetLast(this._PCEarplugsResilienceCheckDetailId);

        }

        protected override void MoveLast()
        {
            //this._pCEarplugsResilienceConditionSet = this.manager.Get(this._OpticsTestManager.FGetLast(this._PCFinishCheckId) == null ? "" : this._OpticsTestManager.FGetLast(this._PCFinishCheckId).OpticsTestId);
            this._pCEarplugsResilienceWeight = this.manager.mGetLast(this._PCEarplugsResilienceCheckDetailId);
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
            return this.manager.mHasRows(this._PCEarplugsResilienceCheckDetailId);
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
            this._pCEarplugsResilienceWeight.Weight1 = this.spe_Weight1.Value;
            this._pCEarplugsResilienceWeight.Weight2 = this.spe_Weight2.Value;
            this._pCEarplugsResilienceWeight.Weight3 = this.spe_Weight3.Value;
            this._pCEarplugsResilienceWeight.Weight4 = this.spe_Weight4.Value;
            this._pCEarplugsResilienceWeight.Weight5 = this.spe_Weight5.Value;
            this._pCEarplugsResilienceWeight.Weight6 = this.spe_Weight6.Value;



            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._pCEarplugsResilienceWeight);
                    break;
                case "update":
                    this.manager.Update(this._pCEarplugsResilienceWeight);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._pCEarplugsResilienceWeight == null)
            {
                this.AddNew();
                this.action = "insert";
            }

            this.spe_Weight1.EditValue = this._pCEarplugsResilienceWeight.Weight1;
            this.spe_Weight2.EditValue = this._pCEarplugsResilienceWeight.Weight2;
            this.spe_Weight3.EditValue = this._pCEarplugsResilienceWeight.Weight3;
            this.spe_Weight4.EditValue = this._pCEarplugsResilienceWeight.Weight4;
            this.spe_Weight5.EditValue = this._pCEarplugsResilienceWeight.Weight5;
            this.spe_Weight6.EditValue = this._pCEarplugsResilienceWeight.Weight6;

            base.Refresh();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            //return new ROOpticsTest(_OpticsTest);

            return null;
        }

    }
}