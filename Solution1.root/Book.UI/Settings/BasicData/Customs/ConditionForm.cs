using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public Model.Customer customer = new Book.Model.Customer();
        public ConditionForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.newChooseContorlCustomer.Choose = new ChooseCustoms();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.customer = this.newChooseContorlCustomer.EditValue as Model.Customer;
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}