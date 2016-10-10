using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

namespace Book.UI.Accounting.AtCashAccount
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtCashAccountManager manager = new Book.BL.AtCashAccountManager();
        BL.AtCashAccountDetailManager detailManager = new Book.BL.AtCashAccountDetailManager();
        IList<Model.AtCashAccountDetail> detailList = new List<Model.AtCashAccountDetail>();
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.AtCashAccount model)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lblMonth.Text = model.AtCashAccountMonth.Value.ToString("yyyy-MM-dd");

            if (model.Detail.Count > 0)
                //this.lblStartBalance.Text = model.Detail[0].Subject.TheBalance == null ? "" : model.Detail[0].Subject.TheBalance.Value.ToString("F2");
            this.lblIncome.Text = model.IncomeTotal == null ? "" : model.IncomeTotal.Value.ToString("F2");
            this.lblPay.Text = model.PayTotal == null ? "" : model.PayTotal.Value.ToString("F2");
            this.lblBalance.Text = model.BalanceTotal == null ? "" : model.BalanceTotal.Value.ToString("F2");

            this.DataSource = model.Detail;

            //this.TCTime.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_AtCashAccountDetaiDate, "{0:yyyy-MM-dd}");
            //this.TCId.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_Id);
            //this.TCName.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_SubjectName);
            this.TCNote.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_Note);
            this.TCIncome.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_Income, "{0:F2}");
            this.TCPay.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_Pay, "{0:F2}");
            //this.TCBalance.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_Balance, "{0:F2}");
        }

        //public RO(Query.ConditionA condition)
        //    : this()
        //{
        //    this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
        //    this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");


        //    this.lblMonth.Text = condition.StartDate == condition.EndDate ? condition.StartDate.ToString("yyyy-MM") : condition.StartDate.ToString("yyyy-MM") + " - " + condition.EndDate.ToString("yyyy-MM");
        //    DataTable dt = this.manager.CountIncomeAndPay(condition.StartDate, condition.EndDate);
        //    this.lblIncome.Text = dt.Rows[0]["Income"].ToString();
        //    this.lblPay.Text = dt.Rows[0]["Pay"].ToString();
        //    this.lblBalance.Text = this.manager.GetBalanceByDate(condition.StartDate, condition.EndDate).ToString("F2");

        //    this.DataSource =this.detailList= this.detailManager.SelectByDate(condition.StartDate, condition.EndDate);
        //    if (detailList.Count > 0)
        //        this.lblStartBalance.Text = this.detailList.First(d => d.Subject.Id == "9999000").Subject.TheBalance == null ? "" : this.lblStartBalance.Text = this.detailList.First(d => d.Subject.Id == "9999000").Subject.TheBalance.Value.ToString("F2");
        //    this.TCTime.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_AtCashAccountDetaiDate, "{0:yyyy-MM-dd}");
        //    this.TCId.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_Id);
        //    this.TCName.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_SubjectName);
        //    this.TCNote.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_Note);
        //    this.TCIncome.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_Income, "{0:F2}");
        //    this.TCPay.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_Pay, "{0:F2}");
        //    this.TCBalance.DataBindings.Add("Text", this.DataSource, Model.AtCashAccountDetail.PRO_Balance, "{0:F2}");
        //}
    }
}
