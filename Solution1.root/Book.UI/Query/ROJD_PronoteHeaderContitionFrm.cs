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
    public partial class ROJD_PronoteHeaderContitionFrm : ConditionChooseForm
    {
        public ROJD_PronoteHeaderContition condition;

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ROJD_PronoteHeaderContition;
            }
        }

        public ROJD_PronoteHeaderContitionFrm()
        {
            InitializeComponent();

            this.ncc_CustomerChuHuo.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.ncc_EmployeeYeWu.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.ncc_WorkHouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();

        }

        private void ROJD_PronoteHeaderContitionFrm_Load(object sender, EventArgs e)
        {
            this.DateEdit_InvoiceStartDate.DateTime = DateTime.Now.AddDays(-7).Date;
            this.DateEdit_InvoiceEndDate.DateTime = DateTime.Now.AddDays(1).Date;
            this.cmb_JieAn.SelectedIndex = 0;
        }

        protected override void OnOK()
        {
            if (this.condition == null) this.condition = new ROJD_PronoteHeaderContition();
            this.condition.InvoiceDate_Start = this.DateEdit_InvoiceStartDate.EditValue == null ? DateTime.Now.AddDays(-7).Date : this.DateEdit_InvoiceStartDate.DateTime;
            this.condition.InvoiceDate_End = this.DateEdit_InvoiceEndDate.EditValue == null ? DateTime.Now.AddDays(1).Date : this.DateEdit_InvoiceEndDate.DateTime;
            this.condition.CusInvoiceXOId = this.txt_CusInvoiceXOId.Text;
            this.condition.PronoteHeaderId = this.txt_PronoteHeaderId.Text;
            if (this.cmb_JieAn.SelectedIndex == 0)
                this.condition.IsJieAn = null;
            else if (this.cmb_JieAn.SelectedIndex == 1)
                this.condition.IsJieAn = true;
            else
                this.condition.IsJieAn = false;
            this.condition.EmployeeYW = this.ncc_EmployeeYeWu.EditValue as Model.Employee;
            this.condition.ProduceProduct = this.btnEdit_Product.EditValue as Model.Product;
            this.condition.ProduceWorkHouse = this.ncc_WorkHouse.EditValue as Model.WorkHouse;
            this.condition.XSCustomer = this.ncc_CustomerChuHuo.EditValue as Model.Customer;
        }

        private void btnEdit_Product_Click(object sender, EventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm form = new Book.UI.Invoices.ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.btnEdit_Product.EditValue = form.SelectedItem as Model.Product;
            }
        }
    }
}