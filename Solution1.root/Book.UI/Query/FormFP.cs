using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class FormFP : ConditionChooseForm
    {
        private ConditionFP condition;
        public FormFP()
        {
            InitializeComponent();
            this.dateEditEnd.EditValue = DateTime.Now;
            this.dateEditStart.EditValue = DateTime.Now.AddMonths(-1);
            this.nccSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
        }


        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionFP;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionFP();
            this.condition.Supplier = this.nccSupplier.EditValue as Model.Supplier;
            //if (condition.Supplier == null)
            //{
            //    //MessageBox.Show(Properties.Resources.Supplier, this.Text, MessageBoxButtons.OK);
            //    //return;
            //    throw new Helper.MessageValueException(Properties.Resources.Supplier);
            //}
            this.condition.StartDate = this.dateEditStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStart.DateTime;
            this.condition.EndDate = this.dateEditEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEditEnd.DateTime;
            this.DialogResult = DialogResult.OK;
        }
    }
}