using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Accounting.AtBillsIncome
{
    public partial class ROList : DevExpress.XtraReports.UI.XtraReport
    {
        BL.CustomerManager customerManager = new Book.BL.CustomerManager();
        BL.SupplierManager supplierManager = new Book.BL.SupplierManager();
        decimal money = 0;
        public ROList()
        {
            InitializeComponent();
        }
        public ROList(IList<Model.AtBillsIncome> list, string category)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            if (category == "0")
                this.lblReportName.Text = "應收票據作業";
            if (string.IsNullOrEmpty(category))
                this.lblReportName.Text = "應收/付票據作業";

            foreach (var item in list)
            {
                if (item.IncomeCategory == "0")
                {
                    item.IncomeCategory = "收入";
                    item.Customer = customerManager.Get(item.PassingObject);
                }
                else
                {
                    item.IncomeCategory = "支出";
                    item.Supplier = supplierManager.Get(item.PassingObject);
                }
                //if (item.BillsOften == "0")
                //    item.BillsOften = "兌現";
                //else if (item.BillsOften == "1")
                //    item.BillsOften = "作廢";
                //else if (item.BillsOften == "2")
                //    item.BillsOften = "退票";
                //else
                //    item.BillsOften = "";

                this.money += Convert.ToDecimal(item.NotesMoney);
            }
            this.lblTotalMoney.Text = this.money.ToString("F2");
            this.DataSource = list;
            //this.lblCategory.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_IncomeCategory);
            //this.lblKPDate.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_TheOpenDate, "{0:yyyy-MM-dd}");
            //this.lblId.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_Id);
            //this.lblDQDate.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_Maturity, "{0:yyyy-MM-dd}");
            //this.lblPMMoney.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_NotesMoney, "{0:F2}");
            //this.lblPMId.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_NotesAccount);
            //this.lblBank.DataBindings.Add("Text", this.DataSource, "Bank." + Model.Bank.PROPERTY_BANKNAME);
            //this.lblSubject.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_SubjectName);
            //this.lblCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerFullName);
            //this.lblSupplier.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERFULLNAME);
            //this.lblYDDate.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_TheJpy, "{0:yyyy-MM-dd}");
            //this.lblChangeDate.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_MoveDay, "{0:yyyy-MM-dd}");
            //this.lblInvoiceState.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_BillsOften);
            //this.lblNote.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_Mark);

            this.TCID.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_Id);
            this.TCDQDate.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_Maturity, "{0:yyyy-MM-dd}");
            this.TCPMMoney.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_NotesMoney, "{0:F2}");
            this.TCBank.DataBindings.Add("Text", this.DataSource, "Bank." + Model.Bank.PROPERTY_BANKNAME);
            this.TCSupplier.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.TCCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerShortName);
            this.TCChuanPiaoId.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_ChuanPiaoId);
        }
    }
}
