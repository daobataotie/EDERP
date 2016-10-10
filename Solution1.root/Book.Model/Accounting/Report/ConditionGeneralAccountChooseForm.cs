using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;

namespace Book.UI.Accounting.Report
{
    public partial class ConditionGeneralAccountChooseForm : ConditionAChooseForm
    {
        private ConditionGeneralAccount condition;
        public ConditionGeneralAccountChooseForm()
        {
            InitializeComponent();
            this.bindingSource1.DataSource = new BL.AtAccountSubjectManager().Select();
        }
        #region 重写父类方法
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionGeneralAccount();
            this.condition.EndDate = this.dateEditEndDate.DateTime;
            this.condition.StartDate = this.dateEditStartDate.DateTime;
            if (this.lookUpEditDepositSubject.EditValue != null)
            {
                this.condition.StartSubjectId = this.lookUpEditDepositSubject.EditValue.ToString();
            }
            if (this.lookUpEdit1.EditValue != null)
            {
                this.condition.EndSubjectId = this.lookUpEdit1.EditValue.ToString();
            }
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionGeneralAccount;
            }
        }
        #endregion
    }
}