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

        public AnnualSalaryRO(List<HelperEmp> heList,string annual)
            : this()
        {
            this.lblAnnual.Text = annual;
            this.DataSource = heList;


        }
    }
}
