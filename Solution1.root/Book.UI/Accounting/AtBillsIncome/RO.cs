using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Accounting.AtBillsIncome
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }
        public RO(Model.AtBillsIncome atBillsIncome)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            if (atBillsIncome.IncomeCategory == "0")
                this.lblReportName.Text = "應收票據作業";

            this.lblCategory.Text = atBillsIncome.IncomeCategory == "0" ? "收入" : "支出";
            this.lblKPDate.Text = atBillsIncome.TheOpenDate == null ? "" : atBillsIncome.TheOpenDate.Value.ToString("yyyy-MM-dd");
            this.lblId.Text = atBillsIncome.Id;
            this.lblDQDate.Text = atBillsIncome.Maturity == null ? "" : atBillsIncome.Maturity.Value.ToString("yyyy-MM-dd");
            this.lblPMMoney.Text = atBillsIncome.NotesMoney.ToString();
            this.lblPMId.Text = atBillsIncome.NotesAccount;
            this.lblBank.Text = atBillsIncome.Bank == null ? "" : atBillsIncome.Bank.BankName;
            this.lblSubject.Text = atBillsIncome.Subject == null ? "" : atBillsIncome.Subject.SubjectName;
            if (atBillsIncome.IncomeCategory == "0")
                this.lblCustomer.Text = new BL.CustomerManager().Get(atBillsIncome.PassingObject) == null ? "" : new BL.CustomerManager().Get(atBillsIncome.PassingObject).CustomerShortName;
            else
                this.lblSupplier.Text = new BL.SupplierManager().Get(atBillsIncome.PassingObject) == null ? "" : new BL.SupplierManager().Get(atBillsIncome.PassingObject).SupplierFullName;
            this.lblYDDate.Text = atBillsIncome.TheJpy == null ? "" : atBillsIncome.TheJpy.Value.ToString("yyyy-MM-dd");
            this.lblChangeDate.Text = atBillsIncome.MoveDay == null ? "" : atBillsIncome.MoveDay.Value.ToString("yyyy-MM-dd");
            if (atBillsIncome.BillsOften == "0")
                this.lblInvoiceState.Text = "兌現";
            else if (atBillsIncome.BillsOften == "1")
                this.lblInvoiceState.Text = "作廢";
            else if (atBillsIncome.BillsOften == "2")
                this.lblInvoiceState.Text = "退票";
            this.lblNote.Text = atBillsIncome.Mark;
        }
    }
}
