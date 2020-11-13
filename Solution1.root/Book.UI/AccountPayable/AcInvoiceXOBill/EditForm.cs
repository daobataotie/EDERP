using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.BasicData.Customs;
using System.Linq;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class EditForm : BaseEditForm
    {
        private BL.AcInvoiceXOBillManager _acInvoiceXoBillManager = new BL.AcInvoiceXOBillManager();
        private Model.AcInvoiceXOBill _acInvoiceXoBill;
        private BL.InvoiceXTDetailManager invoiceXTDetailManager = new Book.BL.InvoiceXTDetailManager();
        BL.BillIdSetManager BillIdSetManager = new Book.BL.BillIdSetManager();
        BL.BillIdDeletedManager BillIdDeletedManager = new Book.BL.BillIdDeletedManager();

        int billIdIsDeleted = 0;

        int IsCancel = 0;
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AcInvoiceXOBill.PRO_Id, new AA(Properties.Resources.AcInvoiceXOBillfpbh, this.XoId));
            this.requireValueExceptions.Add(Model.AcInvoiceXOBill.PRO_CustomerId, new AA(Properties.Resources.RequireDataForComtomer, this.newChooseCustomerId));
            this.requireValueExceptions.Add("AcInvoiceXOBill.Details", new AA(Properties.Resources.AcInvoiceHasDetails, this.simbtnAppend));
            this.invalidValueExceptions.Add(Model.AcInvoiceXOBill.PRO_AcInvoiceXOBillDate, new AA("發票日期不能小於當前日期！", this.dateAcInvoiceXOBillDate));
            this.newChooseEmployee0Id.Choose = new ChooseEmployee();
            //   this.newChooseEmployee1Id.Choose = new ChooseEmployee();
            this.newChooseEmployeeId.Choose = new ChooseEmployee();
            this.newChooseCustomerId.Choose = new ChooseCustoms();
            this.newChooseCustomer2.Choose = new ChooseCustoms();
            this.action = "view";

            #region LookUpEdit

            #region 發票類別

            DataTable dtInvoiceType = new DataTable();
            dtInvoiceType.Columns.Add("Key", typeof(string));
            dtInvoiceType.Columns.Add("Value", typeof(string));
            DataRow dr = null;
            dr = dtInvoiceType.NewRow();
            dr[0] = "01";
            dr[1] = "三聯式發票";
            dtInvoiceType.Rows.Add(dr);
            dr = dtInvoiceType.NewRow();
            dr[0] = "02";
            dr[1] = "二聯式發票";
            dtInvoiceType.Rows.Add(dr);
            dr = dtInvoiceType.NewRow();
            dr[0] = "03";
            dr[1] = "二聯式收銀機發票";
            dtInvoiceType.Rows.Add(dr);
            dr = dtInvoiceType.NewRow();
            dr[0] = "04";
            dr[1] = "特種稅額發票";
            dtInvoiceType.Rows.Add(dr);
            dr = dtInvoiceType.NewRow();
            dr[0] = "05";
            dr[1] = "電子計算機發票";
            dtInvoiceType.Rows.Add(dr);
            dr = dtInvoiceType.NewRow();
            dr[0] = "06";
            dr[1] = "三聯式收銀機發票";
            dtInvoiceType.Rows.Add(dr);
            dr = dtInvoiceType.NewRow();
            dr[0] = "07";
            dr[1] = "一般稅額計算電子發票";
            dtInvoiceType.Rows.Add(dr);
            dr = dtInvoiceType.NewRow();
            dr[0] = "08";
            dr[1] = "特種稅額計算電子發票";
            dtInvoiceType.Rows.Add(dr);

            this.lue_InvoiceType.Properties.DataSource = dtInvoiceType;
            this.lue_InvoiceType.Properties.ValueMember = "Key";
            this.lue_InvoiceType.Properties.DisplayMember = "Key";

            #endregion

            #region 通關方式註記
            DataTable dtClearanceType = new DataTable();
            dtClearanceType.Columns.Add("Key", typeof(string));
            dtClearanceType.Columns.Add("Value", typeof(string));
            DataRow drClearanceType = null;
            drClearanceType = dtClearanceType.NewRow();
            drClearanceType[0] = "1";
            drClearanceType[1] = "非經海關出口";
            dtClearanceType.Rows.Add(drClearanceType);
            drClearanceType = dtClearanceType.NewRow();
            drClearanceType[0] = "2";
            drClearanceType[1] = "經海關出口";
            dtClearanceType.Rows.Add(drClearanceType);

            this.lue_ClearanceType.Properties.DataSource = dtClearanceType;
            this.lue_ClearanceType.Properties.ValueMember = "Key";
            this.lue_ClearanceType.Properties.DisplayMember = "Key";
            #endregion

            #region 幣別

            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Key", typeof(string));
            dtCurrency.Columns.Add("Value", typeof(string));
            DataRow drCurrency = null;
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "TWD";
            drCurrency[1] = "新台幣";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "USD";
            drCurrency[1] = "美金";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "GBP";
            drCurrency[1] = "英鎊";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "DEM";
            drCurrency[1] = "德國馬克";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "AUD";
            drCurrency[1] = "澳大利亞幣";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "HKD";
            drCurrency[1] = "港幣";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "SGD";
            drCurrency[1] = "新加坡幣";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "CAD";
            drCurrency[1] = "加拿大幣";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "CHF";
            drCurrency[1] = "瑞士法郎";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "MYR";
            drCurrency[1] = "馬來西亞幣";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "FRF";
            drCurrency[1] = "法國法郎";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "BEF";
            drCurrency[1] = "比利時法郎";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "SEK";
            drCurrency[1] = "瑞典幣";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "JPY";
            drCurrency[1] = "日圓";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "ITL";
            drCurrency[1] = "義大利里拉";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "THB";
            drCurrency[1] = "泰銖";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "NTD";
            drCurrency[1] = "CURRENCY_NTD";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "EUR";
            drCurrency[1] = "歐洲共同貨幣";
            dtCurrency.Rows.Add(drCurrency);
            drCurrency = dtCurrency.NewRow();
            drCurrency[0] = "NZD";
            drCurrency[1] = "紐西蘭幣";
            dtCurrency.Rows.Add(drCurrency);

            this.lue_Currency.Properties.DataSource = dtCurrency;
            this.lue_Currency.Properties.ValueMember = "Key";
            this.lue_Currency.Properties.DisplayMember = "Key";
            #endregion

            #region 銷售類別

            DataTable dtSalesType = new DataTable();
            dtSalesType.Columns.Add("Key", typeof(string));
            dtSalesType.Columns.Add("Value", typeof(string));
            DataRow drSalesType = null;
            drSalesType = dtSalesType.NewRow();
            drSalesType[0] = "0";
            drSalesType[1] = "一般銷售";
            dtSalesType.Rows.Add(drSalesType);
            drSalesType = dtSalesType.NewRow();
            drSalesType[0] = "1";
            drSalesType[1] = "洋煙酒類";
            dtSalesType.Rows.Add(drSalesType);
            drSalesType = dtSalesType.NewRow();
            drSalesType[0] = "2";
            drSalesType[1] = "固定資產";
            dtSalesType.Rows.Add(drSalesType);
            drSalesType = dtSalesType.NewRow();
            drSalesType[0] = "3";
            drSalesType[1] = "土地";
            dtSalesType.Rows.Add(drSalesType);

            this.lue_SalesType.Properties.DataSource = dtSalesType;
            this.lue_SalesType.Properties.ValueMember = "Key";
            this.lue_SalesType.Properties.DisplayMember = "Key";

            #endregion

            #endregion

        }

        #region override
        protected override void MoveFirst()
        {
            this._acInvoiceXoBill = this._acInvoiceXoBillManager.Get(this._acInvoiceXoBillManager.GetFirst() == null ? "" : this._acInvoiceXoBillManager.GetFirst().AcInvoiceXOBillId);
        }

        /// <summary>
        /// 尾笔
        /// </summary>
        protected override void MoveLast()
        {
            // if (_acbeginAccountPayble == null)
            {
                this._acInvoiceXoBill = this._acInvoiceXoBillManager.Get(this._acInvoiceXoBillManager.GetLast() == null ? "" : this._acInvoiceXoBillManager.GetLast().AcInvoiceXOBillId);
            }
        }

        protected override void MovePrev()
        {
            Model.AcInvoiceXOBill temp = this._acInvoiceXoBillManager.GetPrev(this._acInvoiceXoBill);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acInvoiceXoBill = this._acInvoiceXoBillManager.Get(temp.AcInvoiceXOBillId);
        }

        protected override void MoveNext()
        {
            Model.AcInvoiceXOBill temp = this._acInvoiceXoBillManager.GetNext(this._acInvoiceXoBill);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acInvoiceXoBill = this._acInvoiceXoBillManager.Get(temp.AcInvoiceXOBillId);
        }

        /// <summary>
        /// 是否有返回行
        /// </summary>
        /// <returns></returns>
        protected override bool HasRows()
        {
            return this._acInvoiceXoBillManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._acInvoiceXoBillManager.HasRowsAfter(this._acInvoiceXoBill);
        }

        protected override bool HasRowsPrev()
        {
            return this._acInvoiceXoBillManager.HasRowsBefore(this._acInvoiceXoBill);
        }

        protected override void AddNew()
        {
            this._acInvoiceXoBill = new Model.AcInvoiceXOBill();
            this._acInvoiceXoBill.AcInvoiceXOBillId = this._acInvoiceXoBillManager.GetId(DateTime.Now);
            this._acInvoiceXoBill.AcInvoiceXOBillDate = DateTime.Now;
            this._acInvoiceXoBill.Employee = BL.V.ActiveOperator.Employee;
            this._acInvoiceXoBill.TaxRateType = 1;
            this._acInvoiceXoBill.TaxRate = 5;
            this._acInvoiceXoBill.IsCancel = 0;
            this._acInvoiceXoBill.ClearanceType = "1";
            this._acInvoiceXoBill.SalesType = "0";
            this._acInvoiceXoBill.Details = new List<Model.AcInvoiceXOBillDetail>();


            //生成發票編號
            GenerateBillId();
        }

        /// <summary>
        /// 生成發票編號
        /// </summary>
        public void GenerateBillId()
        {
            Model.BillIdSet billIdSet = this.BillIdSetManager.SelectEnable();
            if (billIdSet != null)
            {
                if (DateTime.Now.Date > billIdSet.EndDate.Value.Date || DateTime.Now.Date < billIdSet.StartDate.Value.Date)
                {
                    throw new Helper.MessageValueException("當前日期已超出發票編號使用日期！");
                    //MessageBox.Show("當前日期已超出發票編號使用日期！", this.Text, MessageBoxButtons.OK);
                    //return;
                }
                //this._acInvoiceXoBill.Id = this.BillIdDeletedManager.SelectBillIdByBillIdSetId(billIdSet.BillIdSetId);
                //this.billIdIsDeleted = 1;        //发票编号不能回收
                //if (string.IsNullOrEmpty(this._acInvoiceXoBill.Id))
                //{
                //    this.billIdIsDeleted = 0;
                int id = Convert.ToInt32(billIdSet.StartBillId) + (billIdSet.IdNumber.HasValue ? billIdSet.IdNumber.Value : 0);
                if (id > Convert.ToInt32(billIdSet.EndBillId))
                {
                    throw new Helper.MessageValueException("發票編號已超出編號使用範圍！");
                    //MessageBox.Show("發票編號已超出編號使用範圍！", this.Text, MessageBoxButtons.OK);
                    //return;
                }
                this._acInvoiceXoBill.Id = billIdSet.EnglishId + id.ToString("00000000"); //发票采用八位编码，如1显示为00000001
                //}
            }
            else
            {
                throw new Helper.MessageValueException("請先設置發票編碼！");
                //MessageBox.Show("請先設置發票編碼！", this.Text, MessageBoxButtons.OK);
                //return;
            }
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this._acInvoiceXoBillManager.Delete(this._acInvoiceXoBill);
                this._acInvoiceXoBill = this._acInvoiceXoBillManager.GetNext(this._acInvoiceXoBill);
                if (this._acInvoiceXoBill == null)
                {
                    this._acInvoiceXoBill = this._acInvoiceXoBillManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            UpdateMoneyFields();       //2019年8月6日22:56:45 ，可能存在总金额计算为0，所以保存时再算一次

            this._acInvoiceXoBill.AcInvoiceXOBillId = this.AcInvoiceXOBillId.Text;
            this._acInvoiceXoBill.Id = this.XoId.Text;
            this._acInvoiceXoBill.Customer = this.newChooseCustomerId.EditValue as Model.Customer;
            if (this._acInvoiceXoBill.Customer != null)
                this._acInvoiceXoBill.CustomerId = this._acInvoiceXoBill.Customer.CustomerId;
            this._acInvoiceXoBill.TaxRate = Convert.ToDouble(this.calcTaxRate.Value);
            this._acInvoiceXoBill.TaxRateMoney = this.calcTaxRateMoney.Value;
            //this._acInvoiceXoBill.TaxRateType = this.TaxType.SelectedIndex;
            this._acInvoiceXoBill.TaxRateType = this.TaxType.SelectedIndex + 1;
            this._acInvoiceXoBill.HeJiMoney = this.calcHeJiMoney.Value;
            this._acInvoiceXoBill.ZongMoney = this.calcZongMoney.Value;
            this._acInvoiceXoBill.mHeXiaoJingE = this.calcmHeXiaoJingE.Value;
            this._acInvoiceXoBill.CustomerShouPiao = this.newChooseCustomer2.EditValue as Model.Customer;
            if (this.newChooseCustomer2.EditValue != null)
                this._acInvoiceXoBill.CustomerShouPiaoId = this._acInvoiceXoBill.CustomerShouPiao.CustomerId;
            this._acInvoiceXoBill.NoHeXiaoTotal = this.calcNoHeXiaoTotal.Value;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateAcInvoiceXOBillDate.DateTime, new DateTime()))
                this._acInvoiceXoBill.AcInvoiceXOBillDate = global::Helper.DateTimeParse.NullDate;
            else
                this._acInvoiceXoBill.AcInvoiceXOBillDate = this.dateAcInvoiceXOBillDate.DateTime;

            this._acInvoiceXoBill.InvoiceType = this.lue_InvoiceType.EditValue == null ? null : this.lue_InvoiceType.EditValue.ToString();
            this._acInvoiceXoBill.ClearanceType = this.lue_ClearanceType.EditValue == null ? null : this.lue_ClearanceType.EditValue.ToString();
            this._acInvoiceXoBill.ExchangeRate = this.spe_ExchangeRate.Value;
            this._acInvoiceXoBill.Currency = this.lue_Currency.EditValue == null ? null : this.lue_Currency.EditValue.ToString();
            this._acInvoiceXoBill.HuikaiNote = this.che_HuikaiNote.Checked;
            this._acInvoiceXoBill.SalesType = this.lue_SalesType.EditValue == null ? null : this.lue_SalesType.EditValue.ToString();
            this._acInvoiceXoBill.RelatedNumbers = this.txt_RelatedNumbers.Text;

            if (this.action == "insert")
            {
                Model.AcInvoiceXOBill last = this._acInvoiceXoBillManager.GetLast();
                if (this._acInvoiceXoBill.AcInvoiceXOBillDate < last.AcInvoiceXOBillDate.Value.Date)
                {
                    throw new Helper.MessageValueException("發票日期不能小於上笔日期");
                }
            }
            else if (this.action == "update")
            {
                if (this._acInvoiceXoBill.AcInvoiceXOBillDate < this._acInvoiceXoBillManager.SelectLastDate(this._acInvoiceXoBill.InsertTime.Value).Date)
                    throw new Helper.MessageValueException("發票日期不能小於上笔日期");
            }
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.dateForYSDate.DateTime, new DateTime()))
            //    this._acInvoiceXoBill.YSDate = global::Helper.DateTimeParse.NullDate;
            //else
            //    this._acInvoiceXoBill.YSDate = this.dateForYSDate.DateTime;
            this._acInvoiceXoBill.AcInvoiceXOBillDesc = this.memoAcInvoiceXOBillDesc.Text;
            this._acInvoiceXoBill.Employee = this.newChooseEmployeeId.EditValue as Model.Employee;
            if (this._acInvoiceXoBill.Employee != null)
                this._acInvoiceXoBill.EmployeeId = this._acInvoiceXoBill.Employee.EmployeeId;
            this._acInvoiceXoBill.Employee0 = this.newChooseEmployee0Id.EditValue as Model.Employee;
            if (this._acInvoiceXoBill.Employee0 != null)
                this._acInvoiceXoBill.Employee0Id = this._acInvoiceXoBill.Employee0.EmployeeId;
            //    this._acInvoiceXoBill.Employee1 = this.newChooseEmployee1Id.EditValue as Model.Employee;
            //  if (this._acInvoiceXoBill.Employee1 != null)
            //    this._acInvoiceXoBill.Employee1Id = this._acInvoiceXoBill.Employee1.EmployeeId;

            this._acInvoiceXoBill.InvoiceStatus = 1;

            switch (this.action)
            {
                case "insert":
                    this._acInvoiceXoBillManager.Insert(this._acInvoiceXoBill);
                    break;

                case "update":
                    this._acInvoiceXoBillManager.Update(this._acInvoiceXoBill);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._acInvoiceXoBill == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._acInvoiceXoBill = this._acInvoiceXoBillManager.GetDetails(this._acInvoiceXoBill);
                    if (this._acInvoiceXoBill == null)
                    {
                        this._acInvoiceXoBill = new Book.Model.AcInvoiceXOBill();
                    }
                }

            }
            this.newChooseCustomer2.EditValue = this._acInvoiceXoBill.CustomerShouPiao;
            this.AcInvoiceXOBillId.Text = this._acInvoiceXoBill.AcInvoiceXOBillId;
            this.XoId.Text = this._acInvoiceXoBill.Id;
            this.memoAcInvoiceXOBillDesc.Text = this._acInvoiceXoBill.AcInvoiceXOBillDesc;
            this.dateAcInvoiceXOBillDate.DateTime = this._acInvoiceXoBill.AcInvoiceXOBillDate.Value;
            this.calcHeJiMoney.Value = this._acInvoiceXoBill.HeJiMoney == null ? 0 : this._acInvoiceXoBill.HeJiMoney.Value;
            //this.TaxType.SelectedIndex = Convert.ToInt32(this._acInvoiceXoBill.TaxRateType);
            this.TaxType.SelectedIndex = Convert.ToInt32(this._acInvoiceXoBill.TaxRateType) - 1;
            this.calcTaxRate.Value = Convert.ToDecimal(this._acInvoiceXoBill.TaxRate == null ? 0 : this._acInvoiceXoBill.TaxRate.Value);
            this.calcTaxRateMoney.Value = this._acInvoiceXoBill.TaxRateMoney == null ? 0 : this._acInvoiceXoBill.TaxRateMoney.Value;
            this.calcZongMoney.Value = this._acInvoiceXoBill.ZongMoney == null ? 0 : this._acInvoiceXoBill.ZongMoney.Value;
            this.calcmHeXiaoJingE.Value = this._acInvoiceXoBill.mHeXiaoJingE == null ? 0 : this._acInvoiceXoBill.mHeXiaoJingE.Value;
            this.calcNoHeXiaoTotal.Value = this._acInvoiceXoBill.NoHeXiaoTotal == null ? 0 : this._acInvoiceXoBill.NoHeXiaoTotal.Value;
            //  this.dateForYSDate.DateTime = this._acInvoiceXoBill.YSDate == null ? DateTime.Now : this._acInvoiceXoBill.YSDate.Value;
            this.newChooseEmployee0Id.EditValue = this._acInvoiceXoBill.AuditEmp;
            //  this.newChooseEmployee1Id.EditValue = this._acInvoiceXoBill.Employee1;
            this.newChooseEmployeeId.EditValue = this._acInvoiceXoBill.Employee;
            this.newChooseCustomerId.EditValue = this._acInvoiceXoBill.Customer;

            this.txt_AuditState.EditValue = this.GetAuditName(this._acInvoiceXoBill.AuditState);

            this.lue_InvoiceType.EditValue = this._acInvoiceXoBill.InvoiceType;
            this.lue_ClearanceType.EditValue = this._acInvoiceXoBill.ClearanceType;
            this.spe_ExchangeRate.Value = this._acInvoiceXoBill.ExchangeRate;
            this.lue_Currency.EditValue = this._acInvoiceXoBill.Currency;
            this.che_HuikaiNote.Checked = this._acInvoiceXoBill.HuikaiNote;
            this.lue_SalesType.EditValue = this._acInvoiceXoBill.SalesType;
            this.txt_RelatedNumbers.Text = this._acInvoiceXoBill.RelatedNumbers;

            this.bindingSourceDetails.DataSource = this._acInvoiceXoBill.Details;

            base.Refresh();
            //if (this.action == "view")
            //{
            //    this.barBtnChuHuoDan.Enabled = false;
            //}
            //else
            //{
            //    this.barBtnChuHuoDan.Enabled = true;
            //}

            this.newChooseEmployeeId.Enabled = false;
            // this.calcHeJiMoney.Enabled = false;
            this.calcZongMoney.Enabled = false;
            this.calcTaxRateMoney.Enabled = false;
            this.calcmHeXiaoJingE.Enabled = false;
            this.calcNoHeXiaoTotal.Enabled = false;
            this.XoId.Enabled = false;

            //作废
            if (this._acInvoiceXoBill.IsCancel == 1)
            {
                this.IsCancel = 1;
                this.barCancel.Caption = "取消作廢";
                this.pictureEditIsCancel.Visible = true;
                if (this.action == "view")
                    FPCancel(false);
                this.barPrintFP.Enabled = false;
            }
            else
            {
                this.IsCancel = 0;
                this.barCancel.Caption = "作廢";
                this.pictureEditIsCancel.Visible = false;
                if (this.action == "view")
                    FPCancel(true);
                this.barPrintFP.Enabled = true;
            }

        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Ro(this._acInvoiceXoBill);
        }

        #endregion

        //选择出货单
        private void simbtnAppend_Click(object sender, EventArgs e)
        {
            //SelectInvoiceXOListForm form = new SelectInvoiceXOListForm();
            //if (form.ShowDialog(this) == DialogResult.OK)
            //{
            //    if (form.SelectItems == null || form.SelectItems.Count == 0)
            //        form.SelectItems.Add(form.SelectItem);
            //    this._acInvoiceXoBill.Details = (this._acInvoiceXoBill.Details.Union(from i in form.SelectItems
            //                                                                         select new Model.AcInvoiceXOBillDetail()
            //                                                                         {
            //                                                                             AcInvoiceXOBillDetailId = Guid.NewGuid().ToString(),
            //                                                                             AcInvoiceXOBillId = this._acInvoiceXoBill.AcInvoiceXOBillId,
            //                                                                             InvoiceId = i.InvoiceId,
            //                                                                             InvoiceXODetailMoney = i.InvoiceHeji,
            //                                                                             InvoiceXODetailTaxMoney = i.InvoiceTotal,
            //                                                                             InvoiceXODetailTax = i.InvoiceTax,
            //                                                                         }).ToList<Model.AcInvoiceXOBillDetail>());
            //}
            //this.calcTaxRateMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTax.Value).Sum();
            //this.calcHeJiMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailMoney.Value).Sum();
            //this.calcZongMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTaxMoney.Value).Sum();

            //this.bindingSourceDetails.DataSource = this._acInvoiceXoBill.Details;
            //this.gridControl1.RefreshDataSource();
            //form.Dispose();
            //GC.Collect();
            Invoices.XS.SearchXSDetail f = new Book.UI.Invoices.XS.SearchXSDetail();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.selectItems.Count > 0)
                {
                    List<string> idlist = this._acInvoiceXoBill.Details.Select(d => d.InvoiceXODetailId).ToList();
                    this.newChooseCustomer2.EditValue = f.selectItems[0].Invoice.Customer;
                    this.newChooseCustomerId.EditValue = f.selectItems[0].Invoice.Customer;

                    foreach (Model.InvoiceXSDetail item in f.selectItems)
                    {
                        if (!idlist.Contains(item.InvoiceXSDetailId))
                        {
                            Model.AcInvoiceXOBillDetail detail = new Book.Model.AcInvoiceXOBillDetail();
                            detail.AcInvoiceXOBillDetailId = Guid.NewGuid().ToString();
                            detail.InvoiceXODetailId = item.InvoiceXSDetailId;
                            detail.ProductId = item.ProductId;
                            detail.Product = item.Product;
                            detail.Invoice = item.Invoice;
                            detail.InvoiceId = item.InvoiceId;
                            detail.AcInvoiceXOBillId = this._acInvoiceXoBill.AcInvoiceXOBillId;
                            detail.InvoiceProductUnit = item.InvoiceProductUnit;
                            detail.InvoiceAllowance = decimal.Parse((item.InvoiceAllowance == null ? 0 : item.InvoiceAllowance.Value).ToString());
                            //if (item.XTquantity > 0)
                            //{
                            //    detail.InvoiceXODetaiInQuantity = (item.InvoiceXSDetailQuantity - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value) - item.XTquantity) < 0 ? 0 : (item.InvoiceXSDetailQuantity - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value) - item.XTquantity);
                            //}
                            //else
                            detail.InvoiceXODetaiInQuantity = (item.InvoiceXSDetailQuantity - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value)) < 0 ? 0 : (item.InvoiceXSDetailQuantity - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value));
                            detail.InvoiceXODetailPrice = item.InvoiceXSDetailPrice == null ? 0 : item.InvoiceXSDetailPrice.Value;
                            detail.InvoiceXODetailMoney = global::Helper.DateTimeParse.GetSiSheWuRu(decimal.Parse(detail.InvoiceXODetaiInQuantity.ToString()) * detail.InvoiceXODetailPrice.Value - detail.InvoiceAllowance.Value, 0);

                            //detail.InvoiceXODetailTax = item.InvoiceXSDetailTax == null ? 0 : item.InvoiceXSDetailTax.Value;
                            //detail.InvoiceXODetailTaxMoney = item.InvoiceXSDetailTaxMoney == null ? 0 : item.InvoiceXSDetailTaxMoney.Value;
                            //detail.InvoiceXODetailTaxPrice = item.InvoiceXSDetailTaxPrice == null ? 0 : item.InvoiceXSDetailTaxPrice.Value;

                            //detail.InvoiceCOIdNote = new BL.InvoiceCOManager().SelectCOIdByXOId(item.InvoiceXOId);

                            detail.InvoiceCOIdNote = new BL.InvoiceXOManager().SelectCusXOIdByPrimaryId(item.InvoiceXOId);
                            this._acInvoiceXoBill.Details.Add(detail);
                        }
                    }

                    ///  this.calcTaxRateMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTax.Value).Sum();
                    //this.calcHeJiMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailMoney.Value).Sum();
                    //this.calcZongMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTaxMoney.Value).Sum();
                    //this.calcTaxRate.Value = f.selectItems[0].Invoice.InvoiceTaxrate == null ? 0 : decimal.Parse(f.selectItems[0].Invoice.InvoiceTaxrate.ToString());
                    //this.TaxType.SelectedIndex = f.selectItems[0].Invoice.TaxCaluType == null ? 0 : f.selectItems[0].Invoice.TaxCaluType.Value;
                    //this.bindingSourceDetails.DataSource = this._acInvoiceXoBill.Details;
                    this.gridControl1.RefreshDataSource();
                    this.UpdateMoneyFields();
                }
            }
            f.Dispose();
            GC.Collect();
        }

        private void simBtnAdd_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (Invoices.ChooseProductForm.ProductList != null && Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in Invoices.ChooseProductForm.ProductList)
                    {
                        Model.AcInvoiceXOBillDetail detail = new Book.Model.AcInvoiceXOBillDetail();
                        detail.AcInvoiceXOBillDetailId = Guid.NewGuid().ToString();
                        //detail.InvoiceXODetailId = item.InvoiceXSDetailId;
                        detail.ProductId = product.ProductId;
                        detail.Product = product;
                        //detail.Invoice = item.Invoice;
                        //detail.InvoiceId = item.InvoiceId;
                        detail.AcInvoiceXOBillId = this._acInvoiceXoBill.AcInvoiceXOBillId;
                        detail.InvoiceProductUnit = detail.Product.SellUnit == null ? null : detail.Product.SellUnit.CnName;
                        //detail.InvoiceAllowance = decimal.Parse((item.InvoiceAllowance == null ? 0 : item.InvoiceAllowance.Value).ToString());
                        //detail.InvoiceXODetaiInQuantity = (item.InvoiceXSDetailQuantity - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value)) < 0 ? 0 : (item.InvoiceXSDetailQuantity - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value));
                        //detail.InvoiceXODetailPrice = item.InvoiceXSDetailPrice == null ? 0 : item.InvoiceXSDetailPrice.Value;
                        //detail.InvoiceXODetailMoney = global::Helper.DateTimeParse.GetSiSheWuRu(decimal.Parse(detail.InvoiceXODetaiInQuantity.ToString()) * detail.InvoiceXODetailPrice.Value - detail.InvoiceAllowance.Value, BL.V.SetDataFormat.XSJEXiao.Value);

                        //detail.InvoiceCOIdNote = new BL.InvoiceXOManager().SelectCusXOIdByPrimaryId(item.InvoiceXOId);
                        this._acInvoiceXoBill.Details.Add(detail);
                    }
                }
                if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    Model.AcInvoiceXOBillDetail detail = new Book.Model.AcInvoiceXOBillDetail();
                    detail.AcInvoiceXOBillDetailId = Guid.NewGuid().ToString();
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.AcInvoiceXOBillId = this._acInvoiceXoBill.AcInvoiceXOBillId;
                    detail.InvoiceProductUnit = detail.Product.SellUnit == null ? null : detail.Product.SellUnit.CnName;

                    this._acInvoiceXoBill.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.UpdateMoneyFields();
            }
        }

        private void simBtnRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this._acInvoiceXoBill.Details.Remove(this.bindingSourceDetails.Current as Model.AcInvoiceXOBillDetail);
                this.gridView1.RefreshData();

                //this.calcTaxRateMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTax.Value).Sum();
                //this.calcHeJiMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailMoney.Value).Sum();
                //this.calcZongMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTaxMoney.Value).Sum();
                UpdateMoneyFields();
            }
        }

        private void barButtonSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm lf = new ListForm();
            if (lf.ShowDialog(this) == DialogResult.OK)
            {
                this._acInvoiceXoBill = (Model.AcInvoiceXOBill)lf.SelectItem;
                this.action = "view";
                this.Refresh();
            }
            lf.Dispose();
            GC.Collect();
        }

        //选择出货单 作廢
        private void barBtnChuHuoDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Invoices.XS.SearchXSDetail f = new Book.UI.Invoices.XS.SearchXSDetail();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    if (f.selectItems.Count > 0)
            //    {
            //        foreach (Model.InvoiceXSDetail item in f.selectItems)
            //        {
            //            Model.AcInvoiceXOBillDetail detail = new Book.Model.AcInvoiceXOBillDetail();
            //            detail.AcInvoiceXOBillDetailId = Guid.NewGuid().ToString();
            //            detail.InvoiceXODetailId = item.InvoiceXSDetailId;
            //            detail.ProductId = item.ProductId;
            //            detail.Product = item.Product;
            //            detail.Invoice = item.Invoice;
            //            detail.InvoiceId = item.InvoiceId;
            //            detail.AcInvoiceXOBillId = this._acInvoiceXoBill.AcInvoiceXOBillId;
            //            detail.InvoiceAllowance = decimal.Parse((item.InvoiceAllowance == null ? 0 : item.InvoiceAllowance.Value).ToString());
            //            detail.InvoiceXODetaiInQuantity = ((item.InvoiceXODetailQuantity == null ? 0 : item.InvoiceXODetailQuantity.Value) - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value)) < 0 ? 0 : ((item.InvoiceXODetailQuantity == null ? 0 : item.InvoiceXODetailQuantity.Value) - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value));
            //            detail.InvoiceXODetailMoney = item.InvoiceXSDetailMoney == null ? 0 : item.InvoiceXSDetailMoney.Value;
            //            detail.InvoiceXODetailPrice = item.InvoiceXSDetailPrice == null ? 0 : item.InvoiceXSDetailPrice.Value;
            //            detail.InvoiceXODetailTax = item.InvoiceXSDetailTax == null ? 0 : item.InvoiceXSDetailTax.Value;
            //            detail.InvoiceXODetailTaxMoney = item.InvoiceXSDetailTaxMoney == null ? 0 : item.InvoiceXSDetailTaxMoney.Value;
            //            detail.InvoiceXODetailTaxPrice = item.InvoiceXSDetailTaxPrice == null ? 0 : item.InvoiceXSDetailTaxPrice.Value;

            //            this._acInvoiceXoBill.Details.Add(detail);
            //        }

            //        this.calcTaxRateMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTax.Value).Sum();
            //        this.calcHeJiMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailMoney.Value).Sum();
            //        this.calcZongMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTaxMoney.Value).Sum();
            //        this.calcTaxRate.Value = f.selectItems[0].Invoice.InvoiceTaxrate == null ? 0 : decimal.Parse(f.selectItems[0].Invoice.InvoiceTaxrate.ToString());
            //        this.TaxType.SelectedIndex = f.selectItems[0].Invoice.TaxCaluType == null ? 0 : f.selectItems[0].Invoice.TaxCaluType.Value;
            //        this.bindingSourceDetails.DataSource = this._acInvoiceXoBill.Details;
            //        this.gridControl1.RefreshDataSource();
            //    }
            //    f.Dispose();
            //    GC.Collect();
            //}
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();
            foreach (Model.AcInvoiceXOBillDetail item in this._acInvoiceXoBill.Details)
            {
                item.InvoiceXODetailMoney = (item.InvoiceXODetaiInQuantity == null ? 0 : decimal.Parse(item.InvoiceXODetaiInQuantity.Value.ToString())) * (item.InvoiceXODetailPrice == null ? 0 : decimal.Parse(item.InvoiceXODetailPrice.Value.ToString())) - (item.InvoiceAllowance == null ? 0 : decimal.Parse(item.InvoiceAllowance.Value.ToString()));
                item.InvoiceXODetailTaxMoney = item.InvoiceXODetailMoney + (item.InvoiceXODetailTax == null ? 0 : item.InvoiceXODetailTax.Value);
                //按照设置位数 四舍五入
                item.InvoiceXODetailMoney = this.GetDecimal(item.InvoiceXODetailMoney.Value, 0);
                item.InvoiceXODetailTaxMoney = this.GetDecimal(item.InvoiceXODetailTaxMoney.Value, 0);
            }
            this.gridControl1.RefreshDataSource();
            this.UpdateMoneyFields();
        }

        private int flag = 0;

        private void UpdateMoneyFields()
        {
            decimal yse = 0;//茼彶塗

            foreach (Model.AcInvoiceXOBillDetail detail in this._acInvoiceXoBill.Details)
            {
                if (detail.InvoiceXODetailMoney == null)
                    detail.InvoiceXODetailMoney = 0;
                yse += detail.InvoiceXODetailMoney.Value;
            }
            //yse = global::Helper.DateTimeParse.GetSiSheWuRu(yse, 0);
            yse = this.GetDecimal(yse, 0);
            if (this.action != "view")
            {
                if (flag == 0)
                {
                    this.calcHeJiMoney.EditValue = yse;
                    this.calcTaxRate.EditValue = 0;
                    this.calcTaxRateMoney.EditValue = 0;
                    this.calcZongMoney.EditValue = yse;
                    this.calcNoHeXiaoTotal.Value = this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value;
                }
                else if (flag == 1)
                {
                    this.calcHeJiMoney.EditValue = yse;
                    //this.calcTaxRateMoney.EditValue = global::Helper.DateTimeParse.GetSiSheWuRu(yse * this.calcTaxRate.Value / 100, 0);
                    this.calcTaxRateMoney.EditValue = this.GetDecimal((yse * this.calcTaxRate.Value / 100), 0);
                    //this.calcZongMoney.EditValue = yse + decimal.Parse(this.calcTaxRateMoney.Text);
                    this.calcZongMoney.EditValue = this.GetDecimal(yse + decimal.Parse(this.calcTaxRateMoney.Text), 0);
                    //this.calcNoHeXiaoTotal.Value = this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value;
                    this.calcNoHeXiaoTotal.Value = this.GetDecimal(this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value, 0);
                }
                else
                {
                    //this.calcHeJiMoney.EditValue = yse;
                    //this.calcTaxRateMoney.EditValue = 0;
                    //this.calcZongMoney.EditValue = yse;
                    //this.calcNoHeXiaoTotal.Value = this.GetDecimal(this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value, 0);


                    //this.calcEditInvoiceTotal.EditValue = tol;
                    //this.calcEditInvoiceHeji.EditValue = yse;
                    //// this.calcEditInvoiceHeji.EditValue = yse * 100 / (100 + this.spinEditInvoiceTaxRate.Value);
                    //this.calcEditInvoiceTax.EditValue = tol - yse;
                    //this.comboBoxEditInvoiceKslb.SelectedIndex = 0;
                }
            }
        }

        private void UpdateZongMoney()
        {
            if (flag == 0)
            {
                this.calcTaxRateMoney.EditValue = 0;
                this.calcZongMoney.EditValue = this.calcHeJiMoney.EditValue;
            }
            else if (flag == 1)
            {
                //this.calcTaxRateMoney.EditValue = global::Helper.DateTimeParse.GetSiSheWuRu(this.calcHeJiMoney.Value * this.calcTaxRate.Value / 100, 0);
                this.calcTaxRateMoney.EditValue = this.GetDecimal(this.calcHeJiMoney.Value * this.calcTaxRate.Value / 100, 0);
                //this.calcZongMoney.EditValue = this.calcHeJiMoney.Value + this.calcTaxRateMoney.Value;
                this.calcZongMoney.EditValue = this.GetDecimal(this.calcHeJiMoney.Value + this.calcTaxRateMoney.Value, 0);
            }
            else
            {
                //this.calcEditInvoiceTotal.EditValue = tol;
                //this.calcEditInvoiceHeji.EditValue = yse;
                //// this.calcEditInvoiceHeji.EditValue = yse * 100 / (100 + this.spinEditInvoiceTaxRate.Value);
                //this.calcEditInvoiceTax.EditValue = tol - yse;
                //this.comboBoxEditInvoiceKslb.SelectedIndex = 0;
            }
            this.calcNoHeXiaoTotal.Value = this.GetDecimal(this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value, 0);
        }

        private void calcTaxRate_EditValueChanged(object sender, EventArgs e)
        {
            UpdateZongMoney();
        }

        private void TaxType_EditValueChanged(object sender, EventArgs e)
        {

            int index = TaxType.SelectedIndex;
            switch (index)
            {
                case 0:
                    flag = 1;
                    break;
                case 1:
                    flag = 2;
                    break;
                default:
                    flag = 0;
                    break;
            }
            UpdateZongMoney();
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            UpdateMoneyFields();
        }

        private void calcHeJiMoney_EditValueChanged(object sender, EventArgs e)
        {
            UpdateZongMoney();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcInvoiceXOBill.PRO_AcInvoiceXOBillId;
        }

        protected override int AuditState()
        {
            return this._acInvoiceXoBill.AuditState.HasValue ? this._acInvoiceXoBill.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcInvoiceXOBill" + "," + this._acInvoiceXoBill.AcInvoiceXOBillId;
        }

        #endregion

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.AcInvoiceXOBillDetail> details = this.bindingSourceDetails.DataSource as IList<Model.AcInvoiceXOBillDetail>;
            if (details == null || details.Count < 1) return;
            string XTId = this.invoiceXTDetailManager.SelectByXODetailId(details[e.ListSourceRowIndex].InvoiceXODetail == null ? "" : details[e.ListSourceRowIndex].InvoiceXODetail.InvoiceXODetailId);
            switch (e.Column.Name)
            {
                case "gridColumn1":
                    e.DisplayText = XTId;
                    break;
            }
        }

        private void barPrintFP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FPRO ro = new FPRO(this._acInvoiceXoBill);
            ro.ShowPreviewDialog();
        }

        private void barCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sql = "";
            if (this.IsCancel == 0)
            {
                this.IsCancel = 1;
                this.barCancel.Caption = "取消作廢";

                sql = "update AcInvoiceXOBill set IsCancel=1 where AcInvoiceXOBillId='" + this._acInvoiceXoBill.AcInvoiceXOBillId + "'";
            }
            else
            {
                this.IsCancel = 0;
                this.barCancel.Caption = "作廢";

                sql = "update AcInvoiceXOBill set IsCancel=0 where AcInvoiceXOBillId='" + this._acInvoiceXoBill.AcInvoiceXOBillId + "'";
            }
            try
            {
                BL.V.BeginTransaction();
                this._acInvoiceXoBillManager.UpdateSql(sql);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK);
                return;
            }
            this.Refresh();
        }

        //导出Excel
        private void bar_ExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm(true);
            f.ShowDialog(this);
        }

    }
}