﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class ConditionCOChooseForm : ConditionAChooseForm
    {
        private ConditionCO condition;

        public ConditionCOChooseForm()
        {
            InitializeComponent();
            this.newChooseSupStart.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseSupEnd.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.nccEmployeeStart.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccEmployeeEnd.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            //this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1);
            //this.dateEditEndDate.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            this.dateEditStartDate.DateTime = DateTime.Parse(string.Format("{0}-{1}-{2}", DateTime.Now.AddMonths(-2).Year, DateTime.Now.AddMonths(-2).Month, 26));
            this.dateEditEndDate.DateTime = DateTime.Parse(string.Format("{0}-{1}-{2}",DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 25));
        }

        protected override void Init()
        {
            this.dateEditStartDate.DateTime = DateTime.Parse(string.Format("{0}-{1}-{2}", DateTime.Now.AddMonths(-2).Year, DateTime.Now.AddMonths(-2).Month, 26));
            this.dateEditEndDate.DateTime = DateTime.Parse(string.Format("{0}-{1}-{2}", DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 25));
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionCO;
            }
        }

        protected override void OnOK1()
        {
            if (this.condition == null)
                this.condition = new ConditionCO();

            if (this.dateEditStartDate.EditValue != null)
            {
                this.condition.StartInvoiceDate = this.dateEditStartDate.DateTime;
            }
            else
                this.condition.StartInvoiceDate = global::Helper.DateTimeParse.NullDate;

            if (this.dateEditEndDate.EditValue != null)
            {
                this.condition.EndInvoiceDate = this.dateEditEndDate.DateTime;
            }
            else
                this.condition.EndInvoiceDate = global::Helper.DateTimeParse.EndDate;

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditJHDate1.DateTime, new DateTime()))
            {
                this.condition.StartJHDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.condition.StartJHDate = this.dateEditJHDate1.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditJHDate2.DateTime, new DateTime()))
            {
                this.condition.EndJHDate = global::Helper.DateTimeParse.EndDate;
            }
            else
            {
                this.condition.EndJHDate = this.dateEditJHDate2.DateTime;
            }

            if (this.dateEditFKStart.EditValue != null)
                this.condition.StartFKDate = this.dateEditFKStart.DateTime;
            if (this.dateEditFKEnd.EditValue != null)
                this.condition.EndFKDate = this.dateEditFKEnd.DateTime;

            this.condition.ProductStart = this.buttonEditPro1.EditValue as Model.Product;
            this.condition.ProductEnd = this.buttonEditPro2.EditValue as Model.Product;
            this.condition.SupplierStart = this.newChooseSupStart.EditValue as Model.Supplier;
            this.condition.SupplierEnd = this.newChooseSupEnd.EditValue as Model.Supplier;
            this.condition.COStartId = string.IsNullOrEmpty(this.textEditCOID1.Text) ? null : this.textEditCOID1.Text;
            this.condition.COEndId = string.IsNullOrEmpty(this.textEditCOID2.Text) ? null : this.textEditCOID2.Text;
            this.condition.InvoiceFlag = this.checkEdit1.Checked == true ? 1 : 0;
            this.condition.CusXOId = this.textEditCusXOId.EditValue == null ? null : this.textEditCusXOId.Text;
            this.condition.EmpStart = this.nccEmployeeStart.EditValue as Model.Employee;
            this.condition.EmpEnd = this.nccEmployeeEnd.EditValue as Model.Employee;

            if ((condition.EndInvoiceDate.HasValue && !condition.StartInvoiceDate.HasValue) || (!condition.EndInvoiceDate.HasValue && condition.StartInvoiceDate.HasValue))
                throw new Exception("單據日期區間不完整！");
            if ((condition.StartFKDate.HasValue && !condition.EndFKDate.HasValue) || (!condition.StartFKDate.HasValue && condition.EndFKDate.HasValue))
                throw new Exception("付款日期區間不完整！");

            this.condition.InvoiceCGIdStart = this.txt_InvoiceCGIdStart.EditValue == null ? null : this.txt_InvoiceCGIdStart.EditValue.ToString();
            this.condition.InvoiceCGIdEnd = this.txt_InvoiceCGIdEnd.EditValue == null ? null : this.txt_InvoiceCGIdEnd.EditValue.ToString();
        }

        private void buttonEditPro1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro1.EditValue = form.SelectedItem as Model.Product;
                this.buttonEditPro2.EditValue = form.SelectedItem as Model.Product;

            }
            form.Dispose();
            GC.Collect();
        }

        private void buttonEditPro2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro2.EditValue = form.SelectedItem as Model.Product;

            }
            form.Dispose();
            GC.Collect();
        }

        private void newChooseSupStart_EditValueChanged(object sender, EventArgs e)
        {
            this.newChooseSupEnd.EditValue = this.newChooseSupStart.EditValue;
        }

        private void buttonEditPro2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void nccEmployeeStart_EditValueChanged(object sender, EventArgs e)
        {
            this.nccEmployeeEnd.EditValue = this.nccEmployeeStart.EditValue;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
        }
    }
}