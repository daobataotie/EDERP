using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtSummon
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public Condition condition = new Condition();
        public ConditionForm()
        {
            InitializeComponent();
            this.dateEditStart.EditValue = DateTime.Now.AddDays(-30);
            this.dateEditEnd.EditValue = DateTime.Now;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.nccStartSubject.Choose = new Accounting.AtAccountSubject.ChooseAccountSubject();
            this.nccEndSubject.Choose = new Accounting.AtAccountSubject.ChooseAccountSubject();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            condition.StartDate = this.dateEditStart.EditValue == null ? DateTime.Now.AddDays(-30) : this.dateEditStart.DateTime;
            condition.EndDate = this.dateEditEnd.EditValue == null ? DateTime.Now : this.dateEditEnd.DateTime;
            condition.StartId = this.textEditStartId.Text;
            condition.EndId = this.textEditEndId.Text;
            condition.StartSubjectId = this.nccStartSubject.EditValue == null ? "" : (this.nccStartSubject.EditValue as Model.AtAccountSubject).Id;
            condition.EndSubjectId = this.nccEndSubject.EditValue == null ? "" : (this.nccEndSubject.EditValue as Model.AtAccountSubject).Id;

            this.DialogResult = DialogResult.OK;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void nccStartSubject_EditValueChanged(object sender, EventArgs e)
        {
            if (this.nccStartSubject.EditValue != null)
                this.nccEndSubject.EditValue = this.nccStartSubject.EditValue;
        }
    }
}