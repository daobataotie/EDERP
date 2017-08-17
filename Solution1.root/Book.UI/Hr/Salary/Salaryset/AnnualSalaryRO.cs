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
        }
    }
}
