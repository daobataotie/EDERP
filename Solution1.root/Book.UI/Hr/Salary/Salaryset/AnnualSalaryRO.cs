using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Hr.Salary.Salaryset
{
    public partial class AnnualSalaryRO : DevExpress.XtraReports.UI.XtraReport
    {
        public AnnualSalaryRO()
        {
            InitializeComponent();
        }

        public AnnualSalaryRO(List<HelperEmp> heList, string annual)
            : this()
        {
            this.lblAnnual.Text = annual;
            this.DataSource = heList;

            this.lblName.DataBindings.Add("Text", DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.lblFYNumber.DataBindings.Add("Text", DataSource, "Employee.FYNumber");
            this.lblQKStandard.DataBindings.Add("Text", DataSource, "Employee.QKStandard");
            this.lblAddress.DataBindings.Add("Text", DataSource, "Employee." + Model.Employee.PROPERTY_CONTACTADDRESS);

            this.lblIDNo1.DataBindings.Add("Text", DataSource, "Employee.IDNo1");
            this.lblIDNo2.DataBindings.Add("Text", DataSource, "Employee.IDNo2");
            this.lblIDNo3.DataBindings.Add("Text", DataSource, "Employee.IDNo3");
            this.lblIDNo4.DataBindings.Add("Text", DataSource, "Employee.IDNo4");
            this.lblIDNo5.DataBindings.Add("Text", DataSource, "Employee.IDNo5");
            this.lblIDNo6.DataBindings.Add("Text", DataSource, "Employee.IDNo6");
            this.lblIDNo7.DataBindings.Add("Text", DataSource, "Employee.IDNo7");
            this.lblIDNo8.DataBindings.Add("Text", DataSource, "Employee.IDNo8");
            this.lblIDNo9.DataBindings.Add("Text", DataSource, "Employee.IDNo9");
            this.lblIDNo10.DataBindings.Add("Text", DataSource, "Employee.IDNo10");

            this.lblMonthSalary1.DataBindings.Add("Text", DataSource, "MonthSalary1");
            this.lblMonthSalary2.DataBindings.Add("Text", DataSource, "MonthSalary2");
            this.lblMonthSalary3.DataBindings.Add("Text", DataSource, "MonthSalary3");
            this.lblMonthSalary4.DataBindings.Add("Text", DataSource, "MonthSalary4");
            this.lblMonthSalary5.DataBindings.Add("Text", DataSource, "MonthSalary5");
            this.lblMonthSalary6.DataBindings.Add("Text", DataSource, "MonthSalary6");
            this.lblMonthSalary7.DataBindings.Add("Text", DataSource, "MonthSalary7");
            this.lblMonthSalary8.DataBindings.Add("Text", DataSource, "MonthSalary8");
            this.lblMonthSalary9.DataBindings.Add("Text", DataSource, "MonthSalary9");
            this.lblMonthSalary10.DataBindings.Add("Text", DataSource, "MonthSalary10");
            this.lblMonthSalary11.DataBindings.Add("Text", DataSource, "MonthSalary11");
            this.lblMonthSalary12.DataBindings.Add("Text", DataSource, "MonthSalary12");
            this.lblMonthSalaryTotal.DataBindings.Add("Text", DataSource, "MonthSalaryTotal");

            this.lblKouKuan1.DataBindings.Add("Text", DataSource, "KouKuan1");
            this.lblKouKuan2.DataBindings.Add("Text", DataSource, "KouKuan2");
            this.lblKouKuan3.DataBindings.Add("Text", DataSource, "KouKuan3");
            this.lblKouKuan4.DataBindings.Add("Text", DataSource, "KouKuan4");
            this.lblKouKuan5.DataBindings.Add("Text", DataSource, "KouKuan5");
            this.lblKouKuan6.DataBindings.Add("Text", DataSource, "KouKuan6");
            this.lblKouKuan7.DataBindings.Add("Text", DataSource, "KouKuan7");
            this.lblKouKuan8.DataBindings.Add("Text", DataSource, "KouKuan8");
            this.lblKouKuan9.DataBindings.Add("Text", DataSource, "KouKuan9");
            this.lblKouKuan10.DataBindings.Add("Text", DataSource, "KouKuan10");
            this.lblKouKuan11.DataBindings.Add("Text", DataSource, "KouKuan11");
            this.lblKouKuan12.DataBindings.Add("Text", DataSource, "KouKuan12");
            this.lblKouKuanTotal.DataBindings.Add("Text", DataSource, "KouKuanTotal");

            this.lblLunchFee1.DataBindings.Add("Text", DataSource, "MonthSalaryClass1.mLunchFee");
            this.lblLunchFee2.DataBindings.Add("Text", DataSource, "MonthSalaryClass2.mLunchFee");
            this.lblLunchFee3.DataBindings.Add("Text", DataSource, "MonthSalaryClass3.mLunchFee");
            this.lblLunchFee4.DataBindings.Add("Text", DataSource, "MonthSalaryClass4.mLunchFee");
            this.lblLunchFee5.DataBindings.Add("Text", DataSource, "MonthSalaryClass5.mLunchFee");
            this.lblLunchFee6.DataBindings.Add("Text", DataSource, "MonthSalaryClass6.mLunchFee");
            this.lblLunchFee7.DataBindings.Add("Text", DataSource, "MonthSalaryClass7.mLunchFee");
            this.lblLunchFee8.DataBindings.Add("Text", DataSource, "MonthSalaryClass8.mLunchFee");
            this.lblLunchFee9.DataBindings.Add("Text", DataSource, "MonthSalaryClass9.mLunchFee");
            this.lblLunchFee10.DataBindings.Add("Text", DataSource, "MonthSalaryClass10.mLunchFee");
            this.lblLunchFee11.DataBindings.Add("Text", DataSource, "MonthSalaryClass11.mLunchFee");
            this.lblLunchFee12.DataBindings.Add("Text", DataSource, "MonthSalaryClass12.mLunchFee");
            this.lblLunchFeeTotal.DataBindings.Add("Text", DataSource, "LunchFeeTotal");

            this.lblOverFee1.DataBindings.Add("Text", DataSource, "OverFee1");
            this.lblOverFee2.DataBindings.Add("Text", DataSource, "OverFee2");
            this.lblOverFee3.DataBindings.Add("Text", DataSource, "OverFee3");
            this.lblOverFee4.DataBindings.Add("Text", DataSource, "OverFee4");
            this.lblOverFee5.DataBindings.Add("Text", DataSource, "OverFee5");
            this.lblOverFee6.DataBindings.Add("Text", DataSource, "OverFee6");
            this.lblOverFee7.DataBindings.Add("Text", DataSource, "OverFee7");
            this.lblOverFee8.DataBindings.Add("Text", DataSource, "OverFee8");
            this.lblOverFee9.DataBindings.Add("Text", DataSource, "OverFee9");
            this.lblOverFee10.DataBindings.Add("Text", DataSource, "OverFee10");
            this.lblOverFee11.DataBindings.Add("Text", DataSource, "OverFee11");
            this.lblOverFee12.DataBindings.Add("Text", DataSource, "OverFee12");
            this.lblOverFeeTotal.DataBindings.Add("Text", DataSource, "OverFeeTotal");
        }
    }
}
