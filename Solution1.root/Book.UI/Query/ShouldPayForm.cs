﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Query
{
    public partial class ShouldPayForm : Settings.BasicData.BaseEditForm
    {
        Model.ShouldPayAccount shouldPayAccount = null;
        Model.AtBillsIncome AtBillsIncome = new Book.Model.AtBillsIncome();
        Model.AtSummon atSummon = new Book.Model.AtSummon();
        Model.AtSummonDetail atSummondetail;
        BL.ShouldPayAccountManager manager = new Book.BL.ShouldPayAccountManager();
        BL.AtBillsIncomeManager AtBillsIncomeManager = new Book.BL.AtBillsIncomeManager();
        BL.AtSummonManager atSummonManager = new Book.BL.AtSummonManager();
        BL.AtSummonDetailManager atSummonDetailManager = new Book.BL.AtSummonDetailManager();
        BL.ShouldPayAccountDetailManager detailManager = new Book.BL.ShouldPayAccountDetailManager();
        private BL.InvoiceCGDetailManager invoicecgmanager = new Book.BL.InvoiceCGDetailManager();
        DataTable dt = new DataTable();
        IList<Model.AtAccountSubject> subjectList;
        IList<Model.AtBillsIncome> atBillsIncomeList = new List<Model.AtBillsIncome>();
        Model.ShouldPayAccountCondition shouldPayAccountCondition;
        BL.AtAccountSubjectManager atAccountSubjectManager = new Book.BL.AtAccountSubjectManager();
        bool isNeedBills = true;   //是否需要支票數據(若會計傳票科目名稱有xxx銀行存款，則不需要，否則需要支票)
        IList<Model.AtSummon> listAtSummon = new List<Model.AtSummon>();
        BL.BankManager bankManager = new Book.BL.BankManager();
        Model.AtSummon atSummon2 = new Book.Model.AtSummon();

        int flag = 0;
        public ShouldPayForm()
        {
            InitializeComponent();

            this.bindingSourceSubject.DataSource = this.subjectList = new BL.AtAccountSubjectManager().SelectIdAndName();
            this.bindingSourceCompany.DataSource = new BL.CompanyManager().Select();

            this.nccSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            //this.nccYFBank.Choose = new Settings.BasicData.Bank.ChooseBank();
            //this.nccYFSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            //this.nccYFSubject.Choose = new Accounting.AtAccountSubject.ChooseAccountSubject();
            this.bindingSourceSupplier.DataSource = new BL.SupplierManager().Select();
            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.bindingSourceBank.DataSource = bankManager.Select();

            this.action = "view";
        }

        public ShouldPayForm(Model.ShouldPayAccount model)
            : this()
        {
            this.shouldPayAccount = model;
            this.flag = 1;
        }

        #region  override
        protected override void AddNew()
        {
            //会计传票
            this.atSummon = new Model.AtSummon();
            this.atSummon.SummonDate = DateTime.Parse(string.Format("{0}-{1}-25", DateTime.Now.Year, DateTime.Now.Month));
            this.atSummon.Id = this.atSummonManager.GetConsecutiveId(this.atSummon.SummonDate.Value);
            this.atSummon.SummonId = Guid.NewGuid().ToString();
            this.atSummon.SummonCategory = "轉帳傳票";
            this.atSummon.Details = new List<Model.AtSummonDetail>();

            this.atSummon2 = new Book.Model.AtSummon();
            this.atSummon2.SummonCategory = "轉帳傳票";
            this.atSummon2.Details = new List<Model.AtSummonDetail>();

            this.shouldPayAccount = new Book.Model.ShouldPayAccount();
            this.shouldPayAccount.ShouldPayAccountId = Guid.NewGuid().ToString();
            this.shouldPayAccount.AtSummon = this.atSummon;
            this.shouldPayAccount.AtSummonId = this.atSummon.SummonId;
            //this.shouldPayAccount.AtBillsIncome = this.AtBillsIncome;
            //this.shouldPayAccount.AtBillsIncomeId = this.AtBillsIncome.BillsId;
            this.shouldPayAccount.Employee = BL.V.ActiveOperator.Employee;
            if (this.shouldPayAccount.Employee != null)
                this.shouldPayAccount.EmployeeId = this.shouldPayAccount.Employee.EmployeeId;

            //应付票据作业
            this.atBillsIncomeList = new List<Model.AtBillsIncome>();

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this.shouldPayAccount);
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this.shouldPayAccount);
        }

        protected override void MoveFirst()
        {
            this.shouldPayAccount = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.flag == 1)
            {
                this.flag = 0;
                return;
            }
            this.shouldPayAccount = this.manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.ShouldPayAccount model = this.manager.GetNext(this.shouldPayAccount);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.shouldPayAccount = model;
        }

        protected override void MovePrev()
        {
            Model.ShouldPayAccount model = this.manager.GetPrev(this.shouldPayAccount);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.shouldPayAccount = model;
        }

        public override void Refresh()
        {
            if (this.shouldPayAccount == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this.shouldPayAccount = this.manager.GetDetail(this.shouldPayAccount.ShouldPayAccountId);
            }

            this.atSummon = this.shouldPayAccount.AtSummon;
            if (this.atSummon != null)
            {
                if (this.action != "insert")
                    this.atSummon = this.atSummonManager.GetDetails(this.atSummon.SummonId);
                this.txt_AtSummonId.EditValue = this.atSummon.Id;
                this.date_AtSummonDate.EditValue = this.atSummon.SummonDate;
                this.cobAtSummonCategory.EditValue = this.atSummon.SummonCategory;
                this.txt_JieTaotal.EditValue = this.atSummon.TotalDebits;
                this.txt_DaiTotal.EditValue = this.atSummon.CreditTotal;
                this.bindingSourceAtDetail.DataSource = this.atSummon.Details;
            }
            else
            {
                this.txt_AtSummonId.EditValue = null;
                this.date_AtSummonDate.EditValue = null;
                this.cobAtSummonCategory.EditValue = null;
                this.txt_JieTaotal.EditValue = null;
                this.txt_DaiTotal.EditValue = null;
                this.bindingSourceAtDetail.DataSource = null;
            }

            if (this.action != "insert")
            {
                listAtSummon = atSummonManager.GetByShouldPayAccountId(shouldPayAccount.ShouldPayAccountId);
                if (listAtSummon.Count() > 0)
                {
                    this.atSummon2 = listAtSummon[0];
                    this.atSummon2.Details = atSummonDetailManager.Select(atSummon2);
                }
                else
                {
                    this.atSummon2 = null;
                }
            }

            if (this.atSummon2 != null)
            {
                this.txt_AtSummonId_P1.EditValue = atSummon2.Id;
                this.date_AtSummonDate_P1.EditValue = atSummon2.SummonDate;
                this.cob_AtSummonCategory_P1.EditValue = atSummon2.SummonCategory;

                this.bindingSourceAtSummonDetail1.DataSource = this.atSummon2.Details;
            }
            else
            {
                this.txt_AtSummonId_P1.EditValue = null;
                this.date_AtSummonDate_P1.EditValue = null;
                this.cob_AtSummonCategory_P1.EditValue = null;

                this.bindingSourceAtSummonDetail1.DataSource = null;
            }
            this.gridControl4.RefreshDataSource();

            if (this.action != "insert")
                this.atBillsIncomeList = this.AtBillsIncomeManager.SelectByShouldPayAccountId(this.shouldPayAccount.ShouldPayAccountId);
            foreach (var item in this.atBillsIncomeList)
            {
                if (item.BillsOften == "0")
                    item.BillsOften = "未托收";
                else if (item.BillsOften == "1")
                    item.BillsOften = "已托收";
                else if (item.BillsOften == "2")
                    item.BillsOften = "已兑现";
                else
                    item.BillsOften = null;
            }
            this.bindingSourceAtBill.DataSource = this.atBillsIncomeList;


            this.txt_InvoiceDate.Text = this.shouldPayAccount.InvoiceDate;
            this.txt_FKDate.Text = this.shouldPayAccount.PayDate;
            this.nccSupplier.EditValue = this.shouldPayAccount.Supplier;
            this.txt_PayMethod.EditValue = this.shouldPayAccount.Supplier == null ? null : this.shouldPayAccount.Supplier.PayMethod;
            this.spe_JinE.EditValue = this.shouldPayAccount.JinE == null ? 0 : this.shouldPayAccount.JinE;
            this.spe_ShuiE.EditValue = this.shouldPayAccount.ShuiE == null ? 0 : this.shouldPayAccount.ShuiE;
            this.spe_ZheRang.EditValue = this.shouldPayAccount.ZheRang == null ? 0 : this.shouldPayAccount.ZheRang;
            this.spe_FKZheRang.EditValue = this.shouldPayAccount.PayZheRang == null ? 0 : this.shouldPayAccount.PayZheRang;
            this.spe_Total.EditValue = this.shouldPayAccount.Total == null ? 0 : this.shouldPayAccount.Total;
            this.nccEmployee.EditValue = this.shouldPayAccount.Employee;

            this.spe_FPMoneyTotal.EditValue = this.shouldPayAccount.FPMoney;
            this.spe_FPTaxTotal.EditValue = this.shouldPayAccount.FPTax;
            this.spe_FPTotalHeji.EditValue = this.shouldPayAccount.FPTotal;
            this.spe_PMTotal.EditValue = this.shouldPayAccount.PMTotal;

            this.bindingSourceDetail.DataSource = this.shouldPayAccount.Detail;

            //2020年9月13日17:39:54：
            //現金日期：
            this.date_Cash.EditValue = this.shouldPayAccount.CashDate;

            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.gridView2.OptionsBehavior.Editable = false;
                    this.gridView3.OptionsBehavior.Editable = false;
                    break;

                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    this.gridView3.OptionsBehavior.Editable = true;
                    break;
            }

            this.txt_PayMethod.Enabled = false;
            this.nccSupplier.Enabled = false;

            this.btn_Search.Enabled = true;
            this.spe_Total.Enabled = false;

            this.nccEmployee.Enabled = false;

            //只有新增时，因付票据才能增减，修改不行
            //if (this.action == "insert")
            //{
            //    btn_YFAdd.Enabled = true;
            //    btn_YFRemove.Enabled = true;
            //}
            //else
            //{
            //    btn_YFAdd.Enabled = false;
            //    btn_YFRemove.Enabled = false;
            //}

            this.txt_AtSummonId_P1.Enabled = false;
            this.cob_AtSummonCategory_P1.Enabled = false;
            this.txt_AtSummonId.Enabled = false;
            this.cobAtSummonCategory.Enabled = false;

            //编辑状态下可以打印，为了预览打印效果，没啥问题再保存
            barButtonItemPrint.Enabled = true;
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            if (!this.gridView3.PostEditor() || !this.gridView3.UpdateCurrentRow())
                return;
            if (!this.gridView4.PostEditor() || !this.gridView4.UpdateCurrentRow())
                return;

            //会计传票
            this.atSummon.Id = this.txt_AtSummonId.Text;
            this.atSummon.SummonCategory = this.cobAtSummonCategory.EditValue == null ? null : this.cobAtSummonCategory.EditValue.ToString();
            this.atSummon.EmployeeDSId = BL.V.ActiveOperator.EmployeeId;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_AtSummonDate.DateTime, new DateTime()))
            {
                this.atSummon.SummonDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.atSummon.SummonDate = this.date_AtSummonDate.DateTime;
            }
            if (string.IsNullOrEmpty(atSummon.Id))
            {
                throw new Helper.MessageValueException("編號不能為空");
            }
            if (string.IsNullOrEmpty(atSummon.SummonCategory))
            {
                throw new Helper.MessageValueException("請選擇傳票類別");
            }
            TimeSpan ts = atSummon.SummonDate.Value - DateTime.Now;
            if (Math.Abs(ts.Days) > 365)
            {
                if (MessageBox.Show("傳票日期與當前日期相差一年以上，是否繼續？", this.Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                    throw null;
            }
            if (atSummon.Details.Count == 0 || (atSummon.Details[0].Lending == null || atSummon.Details[0].SubjectId == null))
            {
                throw new Helper.MessageValueException("請輸入傳票詳細資料！");
            }

            this.atSummon.TotalDebits = this.txt_JieTaotal.Text == null ? 0 : decimal.Parse(this.txt_JieTaotal.Text);
            this.atSummon.CreditTotal = this.txt_DaiTotal.Text == null ? 0 : decimal.Parse(this.txt_DaiTotal.Text);

            //借貸平衡
            if (this.txt_DaiTotal.EditValue != null && this.txt_JieTaotal.EditValue != null)
            {
                if (decimal.Parse(this.txt_JieTaotal.Text) != decimal.Parse(this.txt_DaiTotal.Text))
                {
                    throw new Helper.MessageValueException("會計傳票借貸雙方不平衡,請檢查輸入是否有誤！");
                }
            }
            else
            {
                throw new Helper.MessageValueException("會計傳票借貸金額須相等,借貸數據不完整");
            }

            //亦达会计换人，这块来回变，现在取消校验
            //if (atSummon.Details.Any(a => this.subjectList.First(s => s.SubjectId == a.SubjectId).SubjectName.Contains("銀行存款")) && atSummon.Details.Any(a => this.subjectList.First(s => s.SubjectId == a.SubjectId).SubjectName.Contains("應付票據")))
            //{
            //    throw new Helper.MessageValueException("會計傳票中應付票據和銀行存款不可同時存在");
            //}

            foreach (var item in atSummon.Details)
            {
                if (item.Lending == "借")
                    item.Id = "A" + atSummon.Details.IndexOf(item);
                else
                    item.Id = "B" + atSummon.Details.IndexOf(item);
            }


            if (this.atSummon2 != null)
            {
                this.atSummon2.EmployeeDSId = BL.V.ActiveOperator.EmployeeId;
                this.atSummon2.TotalDebits = this.atSummon2.Details.Where(d => d.Lending == "借").ToList().Sum(d => d.AMoney);
                this.atSummon2.CreditTotal = this.atSummon2.Details.Where(d => d.Lending == "貸").ToList().Sum(d => d.AMoney);

                foreach (var item in atSummon2.Details)
                {
                    if (item.Lending == "借")
                        item.Id = "A" + atSummon2.Details.IndexOf(item);
                    else
                        item.Id = "B" + atSummon2.Details.IndexOf(item);
                }
            }

            string bankName = "";
            if (atBillsIncomeList != null && atBillsIncomeList.Count > 0)
            {
                foreach (var item in this.atBillsIncomeList)
                {
                    item.IncomeCategory = "1";
                    if (item.BillsOften == "未托收")
                        item.BillsOften = "0";
                    else if (item.BillsOften == "已托收")
                        item.BillsOften = "1";
                    else if (item.BillsOften == "已兑现")
                        item.BillsOften = "2";
                    else
                        item.BillsOften = "-1";
                    if (string.IsNullOrEmpty(item.Id))
                        throw new Helper.MessageValueException("請輸入支票編號");
                    if (item.Maturity == null)
                        throw new Helper.MessageValueException("請輸入到期日期");

                    if ((this.bindingSourceBank.DataSource as List<Model.Bank>) != null)
                    {
                        item.Bank = (this.bindingSourceBank.DataSource as List<Model.Bank>).FirstOrDefault(d => d.BankId == item.BankId);

                        bankName = item.Bank == null ? "" : item.Bank.BankName;
                    }
                    item.NotesAccount = item.Bank == null ? null : item.Bank.Id;
                }
            }
            else if (this.action == "insert")
            {
                if (this.isNeedBills == true)
                {
                    throw new Helper.MessageValueException("請輸入支票信息");
                }
                else if (this.date_Cash.EditValue == null)
                {
                    throw new Helper.MessageValueException("請輸入現金日期");
                }
            }

            //应付账款明细表
            this.shouldPayAccount.InvoiceDate = this.txt_InvoiceDate.Text;
            this.shouldPayAccount.PayDate = this.txt_FKDate.Text;
            this.shouldPayAccount.SupplierId = (this.nccSupplier.EditValue as Model.Supplier) == null ? null : (this.nccSupplier.EditValue as Model.Supplier).SupplierId;
            this.shouldPayAccount.JinE = this.spe_JinE.Value;
            this.shouldPayAccount.ShuiE = this.spe_ShuiE.Value;
            this.shouldPayAccount.ZheRang = this.spe_ZheRang.Value;
            this.shouldPayAccount.PayZheRang = this.spe_FKZheRang.Value;
            this.shouldPayAccount.Total = this.spe_Total.Value;
            this.shouldPayAccount.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            this.shouldPayAccount.FPMoney = this.spe_FPMoneyTotal.Value;
            this.shouldPayAccount.FPTax = this.spe_FPTaxTotal.Value;
            this.shouldPayAccount.FPTotal = this.spe_FPTotalHeji.Value;
            this.shouldPayAccount.PMTotal = this.spe_PMTotal.Value;

            if (this.shouldPayAccount.ShouldPayAccountCondition == null && this.action == "insert")
                throw new Helper.MessageValueException("請先查詢應付賬款明細表！");

            if (this.date_Cash.EditValue != null)
                this.shouldPayAccount.CashDate = this.date_Cash.DateTime;

            switch (this.action)
            {
                case "insert":
                    this.atSummonManager.Insert(this.atSummon);
                    new BL.ShouldPayAccountConditionManager().Insert(this.shouldPayAccount.ShouldPayAccountCondition);
                    this.manager.Insert(this.shouldPayAccount);
                    this.AtBillsIncomeManager.Insert(this.atBillsIncomeList);

                    //第二筆會計傳票必須放在這裡保存，因為它保存了應付賬款的主鍵
                    if (this.atSummon2 != null && this.atSummon2.Details.Count > 0)
                    {
                        this.atSummon2.ShouldPayAccountId = this.shouldPayAccount.ShouldPayAccountId;
                        this.atSummonManager.Insert(this.atSummon2);
                    }

                    //InsertAtSummon(bankName);
                    break;
                case "update":

                    //UpdateAtSummon();

                    this.atSummonManager.Update(this.atSummon);

                    if (this.atSummon2 != null)
                        this.atSummonManager.Update(this.atSummon2);

                    new BL.ShouldPayAccountConditionManager().Update(this.shouldPayAccount.ShouldPayAccountCondition);
                    this.AtBillsIncomeManager.Update(this.atBillsIncomeList, this.shouldPayAccount.ShouldPayAccountId);
                    this.manager.Update(this.shouldPayAccount);

                    break;
            }
        }

        private void InsertAtSummon(string bankName)
        {
            if (this.isNeedBills == true)
            {
                this.shouldPayAccount.CashDate = null;
                foreach (var item in this.atBillsIncomeList)
                {
                    Model.AtSummon newAtSummon = new Book.Model.AtSummon();
                    newAtSummon.SummonId = Guid.NewGuid().ToString();
                    newAtSummon.SummonDate = item.Maturity;
                    newAtSummon.SummonCategory = "轉帳傳票";
                    newAtSummon.InsertTime = DateTime.Now;
                    newAtSummon.UpdateTime = DateTime.Now;
                    newAtSummon.Id = this.atSummonManager.GetConsecutiveId(newAtSummon.SummonDate.Value);
                    newAtSummon.ShouldPayAccountId = shouldPayAccount.ShouldPayAccountId;
                    newAtSummon.EmployeeDSId = BL.V.ActiveOperator.EmployeeId;
                    newAtSummon.Details = new List<Model.AtSummonDetail>();

                    Model.AtSummonDetail detail1 = new Model.AtSummonDetail();
                    detail1.SummonDetailId = Guid.NewGuid().ToString();
                    detail1.SummonCatetory = newAtSummon.SummonCategory;
                    detail1.Lending = "借";
                    detail1.AMoney = item.NotesMoney;
                    detail1.InsertTime = DateTime.Now;
                    detail1.UpdateTime = DateTime.Now;
                    detail1.SubjectId = atSummon.Details.First(a => a.Lending == "借").SubjectId;
                    detail1.Id = "A0";
                    newAtSummon.Details.Add(detail1);

                    //Model.AtSummonDetail detail2 = new Model.AtSummonDetail();
                    //detail2.SummonDetailId = Guid.NewGuid().ToString();
                    //detail2.SummonCatetory = newAtSummon.SummonCategory;
                    //detail2.Lending = "貸";
                    //detail2.AMoney = item.NotesMoney;
                    //detail2.InsertTime = DateTime.Now;
                    //detail2.UpdateTime = DateTime.Now;
                    //detail2.SubjectId = "6389198a-ab4d-401c-8ec9-2865a727d0e6";    //台幣銀行存款
                    Model.AtSummonDetail detail2 = this.atSummon.Details.FirstOrDefault(d => d.SummonDetailId == item.AtSummonDetailId);
                    if (detail2 == null)
                    {
                        detail2.SummonDetailId = Guid.NewGuid().ToString();
                        detail2.SummonCatetory = newAtSummon.SummonCategory;
                        detail2.Lending = "貸";
                        detail2.AMoney = item.NotesMoney;
                        detail2.InsertTime = DateTime.Now;

                        detail2.UpdateTime = DateTime.Now;

                        if (bankName.Contains("銀行"))
                            bankName = bankName.Substring(0, bankName.IndexOf("銀行"));

                        detail2.Summary = string.Format("{0}{1}票款", bankName, (this.nccSupplier.EditValue as Model.Supplier) == null ? null : (this.nccSupplier.EditValue as Model.Supplier).SupplierShortName);

                        string subjectName = string.Format("應付票據-{0}", bankName);
                        detail2.SubjectId = subjectList.First(s => s.SubjectName == subjectName).SubjectId;

                        item.AtSummonDetailId = detail2.SummonDetailId;
                    }

                    detail2.Id = "B0";
                    newAtSummon.Details.Add(detail2);

                    newAtSummon.TotalDebits = newAtSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
                    newAtSummon.CreditTotal = newAtSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

                    this.atSummonManager.Insert(newAtSummon);
                }
            }
            else
            {
                var group = this.atSummon.Details.Where(a => a.Lending == "貸" && a.Subject.SubjectName.Contains("銀行存款")).GroupBy(g => g);
                foreach (var item in group)
                {
                    Model.AtSummon newAtSummon = new Book.Model.AtSummon();
                    newAtSummon.SummonId = Guid.NewGuid().ToString();
                    newAtSummon.SummonDate = this.date_Cash.DateTime;
                    newAtSummon.SummonCategory = "轉帳傳票";
                    newAtSummon.InsertTime = DateTime.Now;
                    newAtSummon.UpdateTime = DateTime.Now;
                    newAtSummon.Id = this.atSummonManager.GetConsecutiveId(newAtSummon.SummonDate.Value);
                    newAtSummon.ShouldPayAccountId = shouldPayAccount.ShouldPayAccountId;
                    newAtSummon.EmployeeDSId = BL.V.ActiveOperator.EmployeeId;
                    newAtSummon.Details = new List<Model.AtSummonDetail>();

                    Model.AtSummonDetail detail1 = new Model.AtSummonDetail();
                    detail1.SummonDetailId = Guid.NewGuid().ToString();
                    detail1.SummonCatetory = newAtSummon.SummonCategory;
                    detail1.Lending = "借";
                    detail1.AMoney = item.Key.AMoney;
                    detail1.InsertTime = DateTime.Now;
                    detail1.UpdateTime = DateTime.Now;
                    detail1.SubjectId = atSummon.Details.First(a => a.Lending == "借").SubjectId;
                    detail1.Id = "A0";
                    newAtSummon.Details.Add(detail1);

                    Model.AtSummonDetail detail2 = new Model.AtSummonDetail();
                    detail2.SummonDetailId = Guid.NewGuid().ToString();
                    detail2.SummonCatetory = newAtSummon.SummonCategory;
                    detail2.Lending = "貸";
                    detail2.AMoney = item.Key.AMoney;
                    detail2.InsertTime = DateTime.Now;
                    detail2.UpdateTime = DateTime.Now;
                    detail2.SubjectId = item.Key.SubjectId;
                    detail2.Summary = item.Key.Summary;
                    detail2.Id = "B0";
                    newAtSummon.Details.Add(detail2);

                    newAtSummon.TotalDebits = newAtSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
                    newAtSummon.CreditTotal = newAtSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

                    this.atSummonManager.Insert(newAtSummon);
                }
            }
        }

        private void UpdateAtSummon()
        {
            if (this.listAtSummon.Count == 0)
                return;

            var updatedAtSummon = this.listAtSummon[0];
            updatedAtSummon.EmployeeDSId = BL.V.ActiveOperator.EmployeeId;

            this.gridView4.PostEditor();
            this.gridView4.UpdateCurrentRow();

            if (this.date_AtSummonDate_P1.EditValue == null)
                throw new Exception("傳票日期不能為空");

            if (updatedAtSummon.SummonDate.Value.Date != this.date_AtSummonDate_P1.DateTime.Date)
                updatedAtSummon.Id = this.atSummonManager.GetConsecutiveId(this.date_AtSummonDate_P1.DateTime);

            updatedAtSummon.SummonDate = this.date_AtSummonDate_P1.DateTime;

            updatedAtSummon.Details = this.bindingSourceAtSummonDetail1.DataSource as IList<Model.AtSummonDetail>;

            updatedAtSummon.TotalDebits = updatedAtSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
            updatedAtSummon.CreditTotal = updatedAtSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

            if (updatedAtSummon.TotalDebits != updatedAtSummon.CreditTotal)
                throw new Exception("傳票借貸金額須相等");

            foreach (var item in listAtSummon)
            {
                this.atSummonManager.TiGuiExistsForUpdate(item);
                this.atSummonManager.Update(item);
            }
        }

        public void DeleteAtSummon()
        {
            IList<Model.AtSummon> atSummons = atSummonManager.GetByShouldPayAccountId(shouldPayAccount.ShouldPayAccountId);
            foreach (var item in atSummons)
            {
                atSummonManager.Delete(item);
            }
        }

        protected override void Delete()
        {
            if (this.shouldPayAccount == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //删除对应的会计传票
                DeleteAtSummon();

                Model.ShouldPayAccount model = this.manager.GetNext(this.shouldPayAccount);
                //this.manager.Delete(this.shouldPayAccount.ShouldPayAccountId);
                this.manager.Delete(this.shouldPayAccount);
                if (model == null)
                    this.shouldPayAccount = this.manager.GetLast();
                else
                    this.shouldPayAccount = model;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            if (this.action != "view")  //编辑状态下打印，数据先赋值一下
            {
                //应付账款明细表
                this.shouldPayAccount.InvoiceDate = this.txt_InvoiceDate.Text;
                this.shouldPayAccount.PayDate = this.txt_FKDate.Text;

                this.shouldPayAccount.Supplier = this.nccSupplier.EditValue as Model.Supplier;
                this.shouldPayAccount.SupplierId = this.shouldPayAccount.Supplier == null ? null : this.shouldPayAccount.Supplier.SupplierId;

                this.shouldPayAccount.JinE = this.spe_JinE.Value;
                this.shouldPayAccount.ShuiE = this.spe_ShuiE.Value;
                this.shouldPayAccount.ZheRang = this.spe_ZheRang.Value;
                this.shouldPayAccount.PayZheRang = this.spe_FKZheRang.Value;
                this.shouldPayAccount.Total = this.spe_Total.Value;
                this.shouldPayAccount.EmployeeId = BL.V.ActiveOperator.EmployeeId;
            }

            Model.ShouldPayAccountCondition model = this.shouldPayAccount.ShouldPayAccountCondition;
            if (model != null)
                dt = this.invoicecgmanager.SelectByConditionCOBiao(model.StartInvoiceDate, model.EndInvoiceDate, model.StartJHDate == null ? global::Helper.DateTimeParse.NullDate : (DateTime)model.StartJHDate, model.EndJHDate == null ? global::Helper.DateTimeParse.EndDate : (DateTime)model.EndJHDate, model.StartFKDate, model.EndFKDate, model.SupplierStart, model.SupplierEnd, model.ProductStart, model.ProductEnd, model.COStartId, model.COEndId, model.CusXOId, model.EmpStart, model.EmpEnd);
            else
                dt = null;

            //return new ROInvoiceCGlistBiao(this.shouldPayAccount, dt, this.txt_AtSummonId.Text, this.atBillsIncomeList);

            string atSummonIds = string.Empty;
            foreach (var item in listAtSummon)
            {
                atSummonIds += item.Id + ',';
            }

            return new ROInvoiceCGlistBiao(this.shouldPayAccount, dt, atSummonIds.TrimEnd(','), this.atBillsIncomeList);
        }
        #endregion

        //增加傳票
        private void btnAtSummonAdd_Click(object sender, EventArgs e)
        {
            Model.AtSummonDetail mdetail = new Book.Model.AtSummonDetail();
            mdetail.SummonDetailId = Guid.NewGuid().ToString();
            mdetail.Subject = new Book.Model.AtAccountSubject();
            mdetail.SummonId = this.atSummon.SummonId;
            mdetail.InsertTime = DateTime.Now;
            mdetail.UpdateTime = DateTime.Now;
            mdetail.AMoney = 0;

            //switch (this.cobAtSummonCategory.SelectedIndex)
            //{
            //    case 0:
            //        mdetail.Lending = "貸";
            //        break;
            //    case 1:
            //        mdetail.Lending = "借";
            //        break;
            //    case 3:
            //        mdetail.Lending = "";
            //        break;
            //}

            this.atSummon.Details.Add(mdetail);
            this.bindingSourceAtDetail.DataSource = this.atSummon.Details;
            this.bindingSourceAtDetail.Position = this.bindingSourceAtDetail.IndexOf(mdetail);
            this.gridControl1.RefreshDataSource();
            this.CountJieDaiTotal(this.atSummon.Details);
        }

        private void btnAtSummonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceAtDetail.Current != null)
            {
                //删除对应的应付票据
                Model.AtSummonDetail mdetail = this.bindingSourceAtDetail.Current as Model.AtSummonDetail;

                if (mdetail != null && !string.IsNullOrEmpty(mdetail.SubjectId))
                {
                    Model.AtBillsIncome bill = this.atBillsIncomeList.FirstOrDefault(d => d.AtSummonDetailId == mdetail.SummonDetailId);
                    if (bill != null)
                    {
                        if (this.atSummon2 != null)
                        {
                            Model.AtSummonDetail mapAtd2 = this.atSummon2.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId2);
                            Model.AtSummonDetail mapAtd3 = this.atSummon2.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId3);
                            if (mapAtd2 != null || mapAtd3 != null)
                            {
                                if (mapAtd2 != null)
                                    this.atSummon2.Details.Remove(mapAtd2);

                                if (mapAtd3 != null)
                                    this.atSummon2.Details.Remove(mapAtd3);

                                this.bindingSourceAtSummonDetail1.DataSource = this.atSummon2.Details;
                                this.gridControl4.RefreshDataSource();
                            }
                        }

                        atBillsIncomeList.Remove(bill);
                        this.gridControl3.RefreshDataSource();
                    }
                }


                this.atSummon.Details.Remove(this.bindingSourceAtDetail.Current as Book.Model.AtSummonDetail);

                this.gridControl1.RefreshDataSource();

                this.bindingSourceAtDetail.Position = this.atSummon.Details.Count - 1;
                this.CountJieDaiTotal(this.atSummon.Details);

            }
        }

        //会计科目变化 以更新 应付票据作业 的 票面金额
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            IList<Model.AtSummonDetail> atDetails = this.bindingSourceAtDetail.DataSource as IList<Model.AtSummonDetail>;

            if (e.Column == this.colJinE || e.Column == this.colJieorDai)
            {
                if (e.Column == this.colJinE)
                {
                    Model.AtSummonDetail detail = this.bindingSourceAtDetail.Current as Model.AtSummonDetail;
                    if (detail != null && detail.SubjectId != null)
                    {
                        Model.AtBillsIncome bill = this.atBillsIncomeList.FirstOrDefault(d => d.AtSummonDetailId == detail.SummonDetailId);
                        if (bill != null)
                        {
                            bill.NotesMoney = detail.AMoney;
                            this.gridControl3.RefreshDataSource();

                            if (this.atSummon2 != null)
                            {
                                //對應第二張會計傳票中的 “應付票據-銀行”
                                Model.AtSummonDetail mapAtd2 = this.atSummon2.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId2);
                                //對應第二張會計傳票中與 “應付票據-銀行”關聯的“台幣銀行存款”
                                Model.AtSummonDetail mapAtd3 = this.atSummon2.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId3);

                                if (mapAtd2 != null || mapAtd3 != null)
                                {
                                    if (mapAtd2 != null)
                                    {
                                        mapAtd2.AMoney = bill.NotesMoney;
                                    }
                                    if (mapAtd3 != null)
                                    {
                                        mapAtd3.AMoney = bill.NotesMoney;
                                    }

                                    this.bindingSourceAtSummonDetail1.DataSource = this.atSummon2.Details;
                                    this.gridControl4.RefreshDataSource();
                                }
                            }
                        }
                    }
                }

                this.CountJieDaiTotal(atDetails);
            }
            else if (e.Column == this.colKeMuMingCheng || e.Column == this.colKemuBianHao)
            {
                Model.AtSummonDetail mdetail = this.bindingSourceAtDetail.Current as Model.AtSummonDetail;
                if (mdetail != null && !string.IsNullOrEmpty(mdetail.SubjectId))
                    mdetail.Subject = this.subjectList.First(s => s.SubjectId == mdetail.SubjectId); //atAccountSubjectManager.Get(mdetail.SubjectId);
            }

            //亦达会计换人，这块来回变，现在取消校验，银行存款和支票可以同时存在
            //傳票選取”xxxxx銀行存款”  則必定沒有支票資料  可是現金日期(必填)  此現金日期 為傳票日期
            if (atDetails.Any(a => !string.IsNullOrEmpty(a.SubjectId) && this.subjectList.First(s => s.SubjectId == a.SubjectId).SubjectName.Contains("銀行存款")))
            {
                //this.btn_YFAdd.Enabled = false;

                //this.atBillsIncomeList.Clear();
                //this.spe_PMTotal.Value = 0;
                //this.gridControl3.RefreshDataSource();

                //if (this.atSummon2 != null)
                //{
                //    this.atSummon2.Details.Clear();
                //    this.bindingSourceAtSummonDetail1.DataSource = this.atSummon2.Details;
                //    this.gridControl4.RefreshDataSource();
                //}

                isNeedBills = false;
            }
            else
            {
                //this.btn_YFAdd.Enabled = true;

                isNeedBills = true;
            }
        }

        private void CountJieDaiTotal(IList<Model.AtSummonDetail> list)
        {
            this.txt_JieTaotal.EditValue = list.Where(d => d.Lending == "借").ToList().Sum(d => d.AMoney);
            this.txt_DaiTotal.EditValue = list.Where(d => d.Lending == "貸").ToList().Sum(d => d.AMoney);

            //应付票据作业
            if (this.atBillsIncomeList.Count > 0)
            {
                decimal pmtotal = 0;
                foreach (var item in this.atBillsIncomeList)
                {
                    pmtotal += Convert.ToDecimal(item.NotesMoney);
                }
                this.spe_PMTotal.EditValue = pmtotal;
            }
        }

        //查询应付账款明细表
        private void btn_Search_Click(object sender, EventArgs e)
        {
            Book.UI.Query.ConditionCOChooseForm f = new ConditionCOChooseForm(1);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ConditionCO condition = f.Condition as ConditionCO;
                if (this.action == "view")
                {
                    try
                    {
                        ROInvoiceCGlistBiao ro = new ROInvoiceCGlistBiao(condition);
                        ro.ShowPreviewDialog();

                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    dt = this.invoicecgmanager.SelectByConditionCOBiao(condition.StartInvoiceDate, condition.EndInvoiceDate, condition.StartJHDate, condition.EndJHDate, condition.StartFKDate, condition.EndFKDate, condition.SupplierStart, condition.SupplierEnd, condition.ProductStart, condition.ProductEnd, condition.COStartId, condition.COEndId, condition.CusXOId, condition.EmpStart, condition.EmpEnd);

                    //查询条件
                    this.shouldPayAccountCondition = this.shouldPayAccount.ShouldPayAccountCondition;
                    if (this.shouldPayAccountCondition == null)
                    {
                        this.shouldPayAccountCondition = new Book.Model.ShouldPayAccountCondition();
                        this.shouldPayAccountCondition.ShouldPayAccountConditionId = Guid.NewGuid().ToString();
                    }
                    this.shouldPayAccountCondition.StartInvoiceDate = condition.StartInvoiceDate;
                    this.shouldPayAccountCondition.EndInvoiceDate = condition.EndInvoiceDate;
                    this.shouldPayAccountCondition.StartJHDate = condition.StartJHDate;
                    this.shouldPayAccountCondition.EndJHDate = condition.EndJHDate;
                    this.shouldPayAccountCondition.StartFKDate = condition.StartFKDate;
                    this.shouldPayAccountCondition.EndFKDate = condition.EndFKDate;
                    this.shouldPayAccountCondition.SupplierStart = condition.SupplierStart;
                    this.shouldPayAccountCondition.SupplierStartId = condition.SupplierStart == null ? null : condition.SupplierStart.SupplierId;
                    this.shouldPayAccountCondition.SupplierEnd = condition.SupplierEnd;
                    this.shouldPayAccountCondition.SupplierEndId = condition.SupplierEnd == null ? null : condition.SupplierEnd.SupplierId;
                    this.shouldPayAccountCondition.ProductStart = condition.ProductStart;
                    this.shouldPayAccountCondition.ProductStartId = condition.ProductStart == null ? null : condition.ProductStart.ProductId;
                    this.shouldPayAccountCondition.ProductEnd = condition.ProductEnd;
                    this.shouldPayAccountCondition.ProductEndId = condition.ProductEnd == null ? null : condition.ProductEnd.ProductId;
                    this.shouldPayAccountCondition.COStartId = condition.COStartId;
                    this.shouldPayAccountCondition.COEndId = condition.COEndId;
                    this.shouldPayAccountCondition.CusXOId = condition.CusXOId;
                    this.shouldPayAccountCondition.EmpStart = condition.EmpStart;
                    this.shouldPayAccountCondition.EmpStartId = condition.EmpStart == null ? null : condition.EmpStart.EmployeeId;
                    this.shouldPayAccountCondition.EmpEnd = condition.EmpEnd;
                    this.shouldPayAccountCondition.EmpEndId = condition.EmpEnd == null ? null : condition.EmpEnd.EmployeeId;

                    this.shouldPayAccount.ShouldPayAccountCondition = this.shouldPayAccountCondition;
                    this.shouldPayAccount.ShouldPayAccountConditionId = this.shouldPayAccountCondition == null ? null : this.shouldPayAccountCondition.ShouldPayAccountConditionId;

                    if (condition.SupplierStart != null && condition.SupplierEnd != null)
                        if (condition.SupplierStart.SupplierId != condition.SupplierEnd.SupplierId)
                        {
                            this.nccSupplier.EditValue = null;
                            this.txt_PayMethod.EditValue = null;
                        }
                        else
                        {
                            this.nccSupplier.EditValue = condition.SupplierStart == null ? null : condition.SupplierStart;
                            this.txt_PayMethod.EditValue = condition.SupplierStart == null ? null : condition.SupplierStart.PayMethod;
                        }
                    this.txt_InvoiceDate.EditValue = (condition.StartInvoiceDate == null ? null : condition.StartInvoiceDate.Value.ToString("yyyy-MM-dd")) + "  -  " + (condition.EndInvoiceDate == null ? null : condition.EndInvoiceDate.Value.ToString("yyyy-MM-dd"));
                    this.txt_FKDate.EditValue = (condition.StartFKDate == null ? null : condition.StartFKDate.Value.ToString("yyyy-MM-dd")) + "  -  " + (condition.EndFKDate == null ? null : condition.EndFKDate.Value.ToString("yyyy-MM-dd"));
                    if (dt.Rows.Count > 0)
                    {
                        this.spe_JinE.EditValue = Math.Round(Convert.ToDouble(dt.Compute("Sum(JinE)", "1=1")), 0);
                        this.spe_ShuiE.EditValue = Math.Round(Convert.ToDouble(dt.Compute("Sum(ShuiE)", "")), 0);
                        this.spe_Total.EditValue = Math.Round(Convert.ToDouble(this.spe_JinE.Value) + Convert.ToDouble(this.spe_ShuiE.Value), 0);
                    }

                    #region 添加会计传票详细 以及 對應的應付票據
                    this.atSummon.Details.Clear();

                    //借
                    atSummondetail = new Book.Model.AtSummonDetail();
                    atSummondetail.SummonDetailId = Guid.NewGuid().ToString();
                    atSummondetail.Lending = "借";
                    atSummondetail.AMoney = Convert.ToDecimal(this.spe_Total.EditValue);

                    string subjectName = string.Format("應付帳款-{0}", (this.nccSupplier.EditValue as Model.Supplier) == null ? null : (this.nccSupplier.EditValue as Model.Supplier).SupplierShortName);
                    var matchSub = subjectList.FirstOrDefault(s => s.SubjectName == subjectName);
                    if (matchSub != null)
                    {
                        atSummondetail.SubjectId = matchSub.SubjectId;
                    }
                    else
                    {
                        try
                        {
                            BL.V.BeginTransaction();
                            string subjectId = Guid.NewGuid().ToString();
                            string insertSql = string.Format("insert into AtAccountSubject values('{0}','{1}','',null,'31c7baf9-c21d-4075-8738-ebbaedd1c000','借','0',null,null,null,null,GETDATE(),GETDATE(),(select cast((select top 1 cast(Id as int) from AtAccountSubject where left(Id,4)='2144' order by Id desc )+1 as varchar(20))),null,null)", subjectId, subjectName);

                            this.manager.UpdateSql(insertSql);

                            BL.V.CommitTransaction();

                            atSummondetail.SubjectId = subjectId;

                            this.bindingSourceSubject.DataSource = this.subjectList = new BL.AtAccountSubjectManager().SelectIdAndName();
                        }
                        catch
                        {
                            BL.V.RollbackTransaction();
                            throw new Exception(string.Format("添加會計科目‘{0}’時出現錯誤，請聯繫管理員", subjectName));
                        }
                    }

                    this.atSummon.Details.Add(atSummondetail);

                    //貸
                    atSummondetail = new Book.Model.AtSummonDetail();
                    atSummondetail.SummonDetailId = Guid.NewGuid().ToString();
                    atSummondetail.Lending = "貸";
                    atSummondetail.AMoney = 0;
                    this.atSummon.Details.Add(atSummondetail);

                    this.bindingSourceAtDetail.DataSource = this.atSummon.Details;
                    this.gridControl1.RefreshDataSource();

                    CountJieDaiTotal(this.atSummon.Details);

                    //如果是修改，并且之前已经存在第二笔传票，编号不能变，否则删除的时候根据编号删不掉原数据
                    //第二筆會計傳票
                    if (this.atSummon2 == null)
                    {
                        this.atSummon2 = new Book.Model.AtSummon();
                        this.atSummon2.SummonCategory = "轉帳傳票";
                        this.atSummon2.Details = new List<Model.AtSummonDetail>();
                    }
                    this.atSummon2.Details.Clear();
                    Model.AtSummonDetail atd2_1 = new Book.Model.AtSummonDetail();
                    atd2_1.SummonDetailId = Guid.NewGuid().ToString();
                    atd2_1.Lending = "借";
                    atd2_1.AMoney = 0;
                    this.atSummon2.Details.Add(atd2_1);

                    Model.AtSummonDetail atd2_2 = new Book.Model.AtSummonDetail();
                    atd2_2.SummonDetailId = Guid.NewGuid().ToString();
                    atd2_2.Lending = "貸";
                    atd2_2.AMoney = 0;
                    atd2_2.SubjectId = "6389198a-ab4d-401c-8ec9-2865a727d0e6";    //台幣銀行存款
                    this.atSummon2.Details.Add(atd2_2);

                    this.bindingSourceAtSummonDetail1.DataSource = this.atSummon2.Details;
                    this.gridControl4.RefreshDataSource();


                    //对应的 应付票据
                    this.atBillsIncomeList.Clear();
                    Model.AtBillsIncome atbill = new Book.Model.AtBillsIncome();
                    atbill.BillsId = Guid.NewGuid().ToString();
                    atbill.TheOpenDate = DateTime.Now;
                    atbill.ShouldPayAccountId = this.shouldPayAccount.ShouldPayAccountId;
                    atbill.NotesMoney = atSummondetail.AMoney;
                    atbill.PassingObject = this.shouldPayAccountCondition == null ? null : this.shouldPayAccountCondition.SupplierStartId;
                    atbill.AtSummonDetailId = atSummondetail.SummonDetailId;
                    atbill.AtSummonDetailId2 = atd2_1.SummonDetailId;
                    atbill.AtSummonDetailId3 = atd2_2.SummonDetailId;
                    this.atBillsIncomeList.Add(atbill);
                    this.gridControl3.RefreshDataSource();

                    this.spe_PMTotal.EditValue = 0;

                    #endregion

                    this.spe_FKZheRang.EditValue = 0;
                    this.spe_ZheRang.EditValue = 0;
                }

                #region 自动增加发票。规则：自动拉取 上月 对应的厂商的，修改会双向同时修改。比如本月是6月，应付账款明细表日期为4.26-5.25，拉取5.1-5.31号所有该厂商的发票
                if (this.nccSupplier.EditValue == null)
                    MessageBox.Show("廠商為空不能自動拉取發票", this.Text, MessageBoxButtons.OK);
                else
                {
                    shouldPayAccount.Detail.Clear();
                    DateTime startDate = DateTime.Parse(string.Format("{0}-{1}-{2}", DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1));
                    DateTime endDate = DateTime.Parse(string.Format("{0}-{1}-{2}", DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month)));
                    var fpList = detailManager.GetByDateRangeAndSupplier(startDate, endDate.AddDays(1).AddSeconds(-1), (this.nccSupplier.EditValue as Model.Supplier).SupplierId);
                    if (fpList != null)
                    {
                        fpList.ToList().ForEach(fp =>
                            {
                                Model.ShouldPayAccountDetail detail = new Book.Model.ShouldPayAccountDetail();
                                detail.ShouldPayAccountId = shouldPayAccount.ShouldPayAccountId;
                                detail.ShouldPayAccountDetailId = Guid.NewGuid().ToString();
                                detail.FPDate = fp.FPDate;
                                detail.FPHeader = fp.FPHeader;
                                detail.FPId = fp.FPId;
                                detail.FPSupplier = fp.FPSupplier;
                                detail.FPSupplierId = fp.FPSupplierId;
                                detail.FPMoney = fp.FPMoney;
                                detail.FPTax = fp.FPTax;
                                detail.FPTotalMoney = fp.FPTotalMoney;

                                shouldPayAccount.Detail.Add(detail);
                            });
                        this.gridControl2.RefreshDataSource();
                        this.CountFP();
                    }
                }
                #endregion

            }
        }

        private void UpdateAtSummonDetailMoney()
        {
            foreach (var item in this.atSummon.Details)
            {
                item.AMoney = Convert.ToDecimal(this.spe_Total.Text);
            }

            this.gridControl1.RefreshDataSource();
        }

        private void spe_JinE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action == "view")
                return;
            this.spe_Total.EditValue = Convert.ToDouble(this.spe_JinE.EditValue) + Convert.ToDouble(this.spe_ShuiE.EditValue) - Convert.ToDouble(this.spe_ZheRang.EditValue) - Convert.ToDouble(this.spe_FKZheRang.EditValue);

            UpdateAtSummonDetailMoney();

            this.CountJieDaiTotal(this.atSummon.Details);
            this.gridControl1.RefreshDataSource();
        }

        private void spe_ZheRang_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action == "view")
                return;
            this.spe_Total.EditValue = Convert.ToDouble(this.spe_JinE.EditValue) + Convert.ToDouble(this.spe_ShuiE.EditValue) - Convert.ToDouble(this.spe_ZheRang.EditValue) - Convert.ToDouble(this.spe_FKZheRang.EditValue);

            UpdateAtSummonDetailMoney();

            this.CountJieDaiTotal(this.atSummon.Details);
            this.gridControl1.RefreshDataSource();
        }

        private void spe_FKZheRang_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action == "view")
                return;
            this.spe_Total.EditValue = Convert.ToDouble(this.spe_JinE.EditValue) + Convert.ToDouble(this.spe_ShuiE.EditValue) - Convert.ToDouble(this.spe_ZheRang.EditValue) - Convert.ToDouble(this.spe_FKZheRang.EditValue);

            UpdateAtSummonDetailMoney();

            this.CountJieDaiTotal(this.atSummon.Details);
            this.gridControl1.RefreshDataSource();
        }

        private void spe_ShuiE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action == "view")
                return;
            this.spe_Total.EditValue = Convert.ToDouble(this.spe_JinE.EditValue) + Convert.ToDouble(this.spe_ShuiE.EditValue) - Convert.ToDouble(this.spe_ZheRang.EditValue) - Convert.ToDouble(this.spe_FKZheRang.EditValue);

            UpdateAtSummonDetailMoney();

            this.CountJieDaiTotal(this.atSummon.Details);
            this.gridControl1.RefreshDataSource();
        }

        //应付票据
        private void btn_YFAdd_Click(object sender, EventArgs e)
        {
            if (this.atBillsIncomeList == null)
                this.atBillsIncomeList = new List<Model.AtBillsIncome>();
            Model.AtBillsIncome model = new Book.Model.AtBillsIncome();
            model.BillsId = Guid.NewGuid().ToString();
            model.TheOpenDate = DateTime.Now;
            model.ShouldPayAccountId = this.shouldPayAccount.ShouldPayAccountId;

            Model.AtSummonDetail atd = new Book.Model.AtSummonDetail();
            atd.SummonDetailId = Guid.NewGuid().ToString();
            atd.Lending = "貸";
            atd.AMoney = 0;
            this.atSummon.Details.Add(atd);
            this.bindingSourceAtDetail.DataSource = this.atSummon.Details;
            this.gridControl1.RefreshDataSource();

            //第二筆會計傳票
            if (this.atSummon2 != null)
            {
                Model.AtSummonDetail atd2_1 = new Book.Model.AtSummonDetail();
                atd2_1.SummonDetailId = Guid.NewGuid().ToString();
                atd2_1.SummonId = this.atSummon2.SummonId;
                atd2_1.Lending = "借";
                atd2_1.AMoney = 0;
                this.atSummon2.Details.Add(atd2_1);

                Model.AtSummonDetail atd2_2 = new Book.Model.AtSummonDetail();
                atd2_2.SummonDetailId = Guid.NewGuid().ToString();
                atd2_2.SummonId = this.atSummon2.SummonId;
                atd2_2.Lending = "貸";
                atd2_2.AMoney = 0;
                atd2_2.SubjectId = "6389198a-ab4d-401c-8ec9-2865a727d0e6";    //台幣銀行存款
                this.atSummon2.Details.Add(atd2_2);

                this.bindingSourceAtSummonDetail1.DataSource = this.atSummon2.Details;
                this.gridControl4.RefreshDataSource();

                model.AtSummonDetailId2 = atd2_1.SummonDetailId;
                model.AtSummonDetailId3 = atd2_2.SummonDetailId;
            }


            model.AtSummonDetailId = atd.SummonDetailId;
            this.atBillsIncomeList.Add(model);
            this.bindingSourceAtBill.Position = this.bindingSourceAtBill.IndexOf(model);
            this.gridControl3.RefreshDataSource();
        }

        private void btn_YFRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceAtBill.Current != null)
            {
                Model.AtBillsIncome bill = bindingSourceAtBill.Current as Model.AtBillsIncome;
                //對應第一張會計傳票中的 “應付票據-銀行”
                Model.AtSummonDetail mapAtd1 = this.atSummon.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId);

                Model.AtSummonDetail mapAtd2 = null;
                Model.AtSummonDetail mapAtd3 = null;
                if (this.atSummon2 != null)
                {
                    //對應第二張會計傳票中的 “應付票據-銀行”
                    mapAtd2 = this.atSummon2.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId2);

                    //對應第二張會計傳票中與 “應付票據-銀行”關聯的“台幣銀行存款”
                    mapAtd3 = this.atSummon2.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId3);
                }

                if (mapAtd1 != null)
                {
                    this.atSummon.Details.Remove(mapAtd1);
                    this.gridControl1.RefreshDataSource();
                }
                if (mapAtd2 != null || mapAtd3 != null)
                {
                    if (mapAtd2 != null)
                        this.atSummon2.Details.Remove(mapAtd2);

                    if (mapAtd3 != null)
                        this.atSummon2.Details.Remove(mapAtd3);

                    this.bindingSourceAtSummonDetail1.DataSource = this.atSummon2.Details;
                    this.gridControl4.RefreshDataSource();
                }

                this.atBillsIncomeList.Remove(bill);
                this.gridControl3.RefreshDataSource();
            }

            CountJieDaiTotal(this.atSummon.Details);
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.AtBillsIncome bill = bindingSourceAtBill.Current as Model.AtBillsIncome;
            //對應第一張會計傳票中的 “應付票據-銀行”
            Model.AtSummonDetail mapAtd1 = this.atSummon.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId);

            Model.AtSummonDetail mapAtd2 = null;
            Model.AtSummonDetail mapAtd3 = null;
            if (this.atSummon2 != null)
            {
                //對應第二張會計傳票中的 “應付票據-銀行”
                mapAtd2 = this.atSummon2.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId2);

                //對應第二張會計傳票中與 “應付票據-銀行”關聯的“台幣銀行存款”
                mapAtd3 = this.atSummon2.Details.FirstOrDefault(d => d.SummonDetailId == bill.AtSummonDetailId3);
            }

            if (e.Column.Name == "gridColumn11")            //到期日期
            {
                if (e.RowHandle == 0 && this.atSummon2 != null)
                {
                    this.atSummon2.SummonDate = Convert.ToDateTime(e.Value);

                    //如果是修改，并且之前已经存在第二笔传票，编号不能变，否则删除的时候根据编号删不掉原数据
                    if (this.action == "insert" || (this.action == "update" && string.IsNullOrEmpty(this.txt_AtSummonId_P1.Text)))
                    {
                        this.atSummon2.Id = this.atSummonManager.GetConsecutiveId(this.atSummon2.SummonDate.Value);
                        this.txt_AtSummonId_P1.EditValue = this.atSummon2.Id;
                    }

                    this.date_AtSummonDate_P1.EditValue = this.atSummon2.SummonDate;
                }
            }
            else if (e.Column.Name == "gridColumn12")       //票面金額
            {
                decimal pmtotal = 0;
                foreach (var item in this.atBillsIncomeList)
                {
                    pmtotal += Convert.ToDecimal(item.NotesMoney);
                }
                this.spe_PMTotal.EditValue = pmtotal;

                if (mapAtd1 != null)
                {
                    mapAtd1.AMoney = bill.NotesMoney;
                    this.gridControl1.RefreshDataSource();

                    this.txt_JieTaotal.EditValue = this.atSummon.Details.Where(d => d.Lending == "借").ToList().Sum(d => d.AMoney);
                    this.txt_DaiTotal.EditValue = this.atSummon.Details.Where(d => d.Lending == "貸").ToList().Sum(d => d.AMoney);
                }

                if (mapAtd2 != null || mapAtd3 != null)
                {
                    if (mapAtd2 != null)
                    {
                        mapAtd2.AMoney = bill.NotesMoney;
                    }
                    if (mapAtd3 != null)
                    {
                        mapAtd3.AMoney = bill.NotesMoney;
                    }

                    this.bindingSourceAtSummonDetail1.DataSource = this.atSummon2.Details;
                    this.gridControl4.RefreshDataSource();
                }
            }
            else if (e.Column.Name == "gridColumn13" || e.Column.Name == "gridColumn14")   //開票銀行
            {
                Model.Bank bank = bankManager.Get(bill.BankId);
                string bankName = bank == null ? "" : bank.BankName;
                if (bankName.Contains("銀行"))
                    bankName = bankName.Substring(0, bankName.IndexOf("銀行"));

                string subjectName = string.Format("應付票據-{0}", bankName);
                var matchSub = subjectList.FirstOrDefault(s => s.SubjectName == subjectName);
                string subjectId = null;
                if (matchSub != null)
                {
                    subjectId = matchSub.SubjectId;
                }
                else
                {
                    try
                    {
                        BL.V.BeginTransaction();
                        subjectId = Guid.NewGuid().ToString();
                        string insertSql = string.Format("insert into AtAccountSubject values('{0}','{1}','',null,'31c7baf9-c21d-4075-8738-ebbaedd1c000','借','0',null,null,null,null,GETDATE(),GETDATE(),(select cast((select top 1 cast(Id as int) from AtAccountSubject where left(Id,5)='21417' order by Id desc )+1 as varchar(20))),null,null)", subjectId, subjectName);

                        this.manager.UpdateSql(insertSql);

                        BL.V.CommitTransaction();

                        this.bindingSourceSubject.DataSource = this.subjectList = new BL.AtAccountSubjectManager().SelectIdAndName();
                    }
                    catch
                    {
                        BL.V.RollbackTransaction();
                        throw new Exception(string.Format("添加會計科目‘{0}’時出現錯誤，請聯繫管理員", subjectName));
                    }
                }

                if (mapAtd1 != null)
                {
                    mapAtd1.SubjectId = subjectId;
                    this.gridControl1.RefreshDataSource();
                }
                if (mapAtd2 != null)
                {
                    mapAtd2.SubjectId = subjectId;
                    this.gridControl4.RefreshDataSource();
                }

            }
        }


        //增加发票
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this.shouldPayAccount.Detail == null)
                this.shouldPayAccount.Detail = new List<Model.ShouldPayAccountDetail>();
            Model.ShouldPayAccountDetail model = new Book.Model.ShouldPayAccountDetail();
            model.ShouldPayAccountDetailId = Guid.NewGuid().ToString();
            model.ShouldPayAccountId = this.shouldPayAccount.ShouldPayAccountId;
            model.FPSupplier = this.nccSupplier.EditValue as Model.Supplier;
            model.FPSupplierId = model.FPSupplier == null ? null : model.FPSupplier.SupplierId;
            if ((this.bindingSourceCompany.DataSource as List<Model.Company>) != null)
            {
                if ((this.bindingSourceCompany.DataSource as List<Model.Company>).Any(d => d.CompanyName.Contains("亦達")))
                {
                    model.FPHeader = (this.bindingSourceCompany.DataSource as List<Model.Company>).First(d => d.CompanyName.Contains("亦達")).CompanyId;
                }
            }
            this.shouldPayAccount.Detail.Add(model);
            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(model);
            this.gridControl2.RefreshDataSource();

        }

        //移除發票
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.shouldPayAccount.Detail.Remove(this.bindingSourceDetail.Current as Model.ShouldPayAccountDetail);
                this.gridControl2.RefreshDataSource();
                this.CountFP();
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumn5" || e.Column.Name == "gridColumn6")
            {
                if (e.Column.Name == "gridColumn5")
                {
                    this.gridView2.SetRowCellValue(e.RowHandle, this.gridColumn6, global::Helper.DateTimeParse.GetSiSheWuRu(Convert.ToDouble(this.gridView2.GetRowCellValue(e.RowHandle, this.gridColumn5)) * 0.05, 0));
                    this.gridView2.SetRowCellValue(e.RowHandle, this.gridColumn7, global::Helper.DateTimeParse.GetSiSheWuRu(Convert.ToDouble(this.gridView2.GetRowCellValue(e.RowHandle, this.gridColumn5)) * 1.05, 0));
                }
                if (e.Column.Name == "gridColumn6")
                {
                    this.gridView2.SetRowCellValue(e.RowHandle, this.gridColumn7, Convert.ToDouble(this.gridView2.GetRowCellValue(e.RowHandle, this.gridColumn5)) + Convert.ToDouble(this.gridView2.GetRowCellValue(e.RowHandle, this.gridColumn6)));
                }
                this.gridControl2.RefreshDataSource();
                this.CountFP();
            }
        }

        private void CountFP()
        {
            this.spe_FPMoneyTotal.EditValue = global::Helper.DateTimeParse.GetSiSheWuRu(Convert.ToDecimal(this.shouldPayAccount.Detail.Sum(d => d.FPMoney)), 0);
            this.spe_FPTaxTotal.EditValue = global::Helper.DateTimeParse.GetSiSheWuRu(Convert.ToDecimal(this.shouldPayAccount.Detail.Sum(d => d.FPTax)), 0);
            //this.spe_FPTotalHeji.EditValue = this.shouldPayAccount.Detail.Sum(d => d.FPTotalMoney);
            this.spe_FPTotalHeji.EditValue = this.spe_FPMoneyTotal.Value + this.spe_FPTaxTotal.Value;
        }


        private void nccYFBank_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.nccYFBank.EditValue as Model.Bank != null && this.action == "insert")
            //{
            //    this.txt_YFPMId.Text = (this.nccYFBank.EditValue as Model.Bank).Id;

            //    //添加、删除会计传票详细
            //    int count = this.atSummon.Details.Count;
            //    for (int i = 0; i < count; i++)
            //    {
            //        if (this.atSummon.Details.Any(d => d.Lending == "貸"))
            //            this.atSummon.Details.Remove(this.atSummon.Details.First(d => d.Lending == "貸"));
            //    }

            //    this.atSummondetail = new Book.Model.AtSummonDetail();
            //    atSummondetail.SummonDetailId = Guid.NewGuid().ToString();
            //    atSummondetail.Lending = "貸";
            //    if (this.subjectList.Any(d => d.SubjectName.Contains((this.nccYFBank.EditValue as Model.Bank).BankName.Substring(0, (this.nccYFBank.EditValue as Model.Bank).BankName.IndexOf("銀行")))))
            //        atSummondetail.SubjectId = this.subjectList.First(d => d.SubjectName.Contains((this.nccYFBank.EditValue as Model.Bank).BankName.Substring(0, (this.nccYFBank.EditValue as Model.Bank).BankName.IndexOf("銀行")))).SubjectId;
            //    atSummondetail.AMoney = Convert.ToDecimal(this.spe_Total.EditValue);
            //    this.atSummon.Details.Add(atSummondetail);

            //    this.atSummondetail = new Book.Model.AtSummonDetail();
            //    atSummondetail.SummonDetailId = Guid.NewGuid().ToString();
            //    atSummondetail.Lending = "貸";
            //    atSummondetail.SubjectId = this.subjectList.First(d => d.Id == "7101000").SubjectId;
            //    atSummondetail.AMoney = Convert.ToDecimal(this.spe_FKZheRang.EditValue);
            //    this.atSummon.Details.Add(atSummondetail);
            //    this.CountJieDaiTotal(this.atSummon.Details);
            //    this.gridControl1.RefreshDataSource();
            //}
        }

        private void nccSupplier_EditValueChanged(object sender, EventArgs e)
        {
            if (nccSupplier.EditValue == null)
                this.lbl_SupplierNote.Text = "";
            else
            {
                if (string.IsNullOrEmpty((nccSupplier.EditValue as Model.Supplier).NoId))
                    this.lbl_SupplierNote.Text = "";
                else
                    this.lbl_SupplierNote.Text = "注意事項：" + (nccSupplier.EditValue as Model.Supplier).NoId;
            }
        }

        //搜索
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShouldPayFormList f = new ShouldPayFormList();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.shouldPayAccount = f.SelectItem as Model.ShouldPayAccount;
                this.Refresh();
            }
        }

        private void date_AtSummonDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action != "view" && date_AtSummonDate.EditValue != null)
            {
                //this.textEditSummonId.Text = this.atSummonManager.GetId(this.dateEditSummonDate.DateTime);
                this.txt_AtSummonId.Text = this.atSummonManager.GetConsecutiveId(this.date_AtSummonDate.DateTime);
            }
        }

    }

}
