using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
using System.Linq;
using System.Collections;

namespace Book.UI.produceManager.ProduceOtherInDepot
{
    public partial class Editform : Settings.BasicData.BaseEditForm
    {
        public static IList<Model.ProduceOtherCompactDetail> _produceOtherCompactDetail = new List<Model.ProduceOtherCompactDetail>();
        Model.ProduceOtherInDepot _produceOtherInDepot;
        BL.ProduceOtherInDepotManager produceOtherInDepotManager = new Book.BL.ProduceOtherInDepotManager();
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        BL.ProduceOtherInDepotDetailManager produceOtherInDepotDetailManager = new Book.BL.ProduceOtherInDepotDetailManager();
        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.ProduceOtherCompactDetailManager OtherCompactDetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        private BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        private BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        private BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private BL.ProduceOtherCompactManager produceOtherCompactManager = new BL.ProduceOtherCompactManager();
        private BL.PCOtherCheckDetailManager pcOtherCheckDetailManager = new Book.BL.PCOtherCheckDetailManager();
        BL.AtAccountSubjectManager atAccountSubjectManager = new Book.BL.AtAccountSubjectManager();

        int flag = 0;
        public Editform()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotId, new AA(Properties.Resources.RequireDataForId, this.textEditProduceOtherInDepotId));
            //this.requireValueExceptions.Add(Model.ProduceOtherInDepot.PROPERTY_WORKHOUSEID, new AA(Properties.Resources.WorkHouse, this.newChooseWorkHorseId));
            this.invalidValueExceptions.Add(Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotId, new AA(Properties.Resources.EntityExists, this.textEditProduceOtherInDepotId));
            this.invalidValueExceptions.Add(Model.ProduceOtherInDepot.PRO_SupplierId, new AA(Properties.Resources.Supplier, this.newChooseContorlSipu));
            this.invalidValueExceptions.Add(Model.ProduceOtherInDepotDetail.PRO_ProduceQuantity, new AA("進貨數量不能為空", this.gridControl1));
            this.invalidValueExceptions.Add(Model.ProduceOtherInDepotDetail.PRO_ProduceTransferQuantity, new AA("轉生產數量或入庫數量必須填寫一項", this.gridControl1));

            this.action = "view";
            this.newChooseEmployee0.Choose = new ChooseEmployee();
            this.newChooseEmployee1.Choose = new ChooseEmployee();
            this.newChooseEmployeeUpdate.Choose = new ChooseEmployee();
            this.newChooseContorlSipu.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlDepot.Choose = new ChooseDepot();
            this.EmpAudit.Choose = new ChooseEmployee();
            // this.newChooseWorkHorseId.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();

            //if (BL.Settings.authorityOperationId.Contains(BL.V.ActiveOperator.OperatorsId))
            //{
            //    this.gridColProcessPrice.Visible = true;
            //    this.ColProduceMoney.Visible = true;
            //}
            //else
            //{
            //    this.gridColProcessPrice.Visible = false;
            //    this.ColProduceMoney.Visible = false;
            //}
            this.gridColProcessPrice.Visible = false;
            this.ColProduceMoney.Visible = false;

            IList<Model.Role> roleList = BL.V.RoleList;
            if (roleList != null && roleList.Count > 0)
            {
                for (int i = 0; i < roleList.Count; i++)
                {
                    if (roleList[i].IsPODPrice == true)
                    {
                        this.gridColProcessPrice.Visible = true;
                        this.ColProduceMoney.Visible = true;

                        break;
                    }
                }
            }
        }

        public Editform(string produceOtherInDepotId, string action)
            : this()
        {
            this._produceOtherInDepot = this.produceOtherInDepotManager.Get(produceOtherInDepotId);
            this._produceOtherInDepot.Details = this.produceOtherInDepotDetailManager.Select(this._produceOtherInDepot);
            this.action = action;
            this.flag = 1;
        }

        public Editform(Model.ProduceOtherInDepot produceOtherInDepot)
            : this()
        {
            this._produceOtherInDepot = produceOtherInDepot;
            this._produceOtherInDepot.Details = this.produceOtherInDepotDetailManager.Select(produceOtherInDepot);
            this.action = "view";
            this.flag = 1;
        }

        public Editform(Model.ProduceOtherInDepot produceOtherInDepot, string action)
            : this()
        {
            this._produceOtherInDepot = produceOtherInDepot;
            this._produceOtherInDepot.Details = this.produceOtherInDepotDetailManager.Select(produceOtherInDepot);
            this.action = action;
            this.flag = 1;
        }

        protected override void AddNew()
        {
            this.newChooseContorlDepot.EditValue = null;
            this.bindingSourceDepotPositionId.DataSource = null;
            this._produceOtherInDepot = new Model.ProduceOtherInDepot();
            this._produceOtherInDepot.ProduceOtherInDepotDate = DateTime.Now;
            this._produceOtherInDepot.PayDate = DateTime.Now;
            this._produceOtherInDepot.ProduceOtherInDepotId = this.produceOtherInDepotManager.GetId();// Guid.NewGuid().ToString();
            this._produceOtherInDepot.Employee0 = BL.V.ActiveOperator.Employee;
            this._produceOtherInDepot.Details = new List<Model.ProduceOtherInDepotDetail>();
            if (this.action == "insert")
            {
                Model.ProduceOtherInDepotDetail detail = new Model.ProduceOtherInDepotDetail();
                detail.ProduceOtherInDepotDetailId = Guid.NewGuid().ToString();
                detail.ProduceQuantity = 0;
                detail.ProducePrice = 0;
                detail.ProductGuige = "";
                detail.ProduceMoney = 0;
                detail.ProcessPrice = 0;
                detail.ProductUnit = "";
                detail.Product = new Book.Model.Product();
                this._produceOtherInDepot.Details.Add(detail);
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            }
        }

        protected override void Save()
        {
            //保存前吧鼠标焦点置于 GridControl中，目的是触发一些控件的更改事件，比如说  供应商更改事件
            //如果控件修改完直接点击保存，对应的修改事件不触发
            this.gridControl1.Focus();

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._produceOtherInDepot.ProduceOtherInDepotId = this.textEditProduceOtherInDepotId.Text;
            this._produceOtherInDepot.ProduceOtherInDepotDesc = this.textEditProduceOtherInDepotDesc.Text;
            //this.produceOtherInDepot.WorkHouse = this.newChooseWorkHorseId.EditValue as Model.WorkHouse;
            //if (this.produceOtherInDepot.WorkHouse != null)
            //{
            //    this.produceOtherInDepot.WorkHouseId = this.produceOtherInDepot.WorkHouse.WorkHouseId;
            //}
            this._produceOtherInDepot.Supplier = this.newChooseContorlSipu.EditValue as Model.Supplier;
            if (this._produceOtherInDepot.Supplier != null)
                this._produceOtherInDepot.SupplierId = this._produceOtherInDepot.Supplier.SupplierId;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceOtherInDepotDate.DateTime, new DateTime()))
            {
                this._produceOtherInDepot.ProduceOtherInDepotDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._produceOtherInDepot.ProduceOtherInDepotDate = this.dateEditProduceOtherInDepotDate.DateTime;
            }
            this._produceOtherInDepot.Employee0 = BL.V.ActiveOperator.Employee;
            if (this._produceOtherInDepot.Employee0 != null)
            {
                this._produceOtherInDepot.Employee0Id = this._produceOtherInDepot.Employee0.EmployeeId;
            }
            this._produceOtherInDepot.Employee1 = (this.newChooseEmployee1.EditValue as Model.Employee);
            if (this._produceOtherInDepot.Employee1 != null)
            {
                this._produceOtherInDepot.Employee1Id = this._produceOtherInDepot.Employee1.EmployeeId;
            }
            this._produceOtherInDepot.Depot = this.newChooseContorlDepot.EditValue as Model.Depot;
            if (this._produceOtherInDepot.Depot != null)
                this._produceOtherInDepot.DepotId = this._produceOtherInDepot.Depot.DepotId;
            //this._produceOtherInDepot.InvoiceCusId = this.textEditCusXOId.Text;
            if (this.newChooseEmployeeUpdate.EditValue != null)
            {
                this._produceOtherInDepot.EmployeeUpdate = this.newChooseEmployeeUpdate.EditValue as Model.Employee;
                this._produceOtherInDepot.EmployeeUpdateId = this._produceOtherInDepot.EmployeeUpdate.EmployeeId;

            }
            this._produceOtherInDepot.Employee0 = this.newChooseEmployee0.EditValue as Model.Employee;
            this._produceOtherInDepot.Employee0Id = this._produceOtherInDepot.Employee0.EmployeeId;
            if (this.newChooseEmployee1.EditValue != null)
            {
                this._produceOtherInDepot.Employee1 = this.newChooseEmployee1.EditValue as Model.Employee;
                this._produceOtherInDepot.Employee1Id = this._produceOtherInDepot.Employee1.EmployeeId;
            }
            this._produceOtherInDepot.AuditState = this.saveAuditState;
            this._produceOtherInDepot.ProduceAmount = Convert.ToDouble(this.txt_Sum.Text == "" ? null : this.txt_Sum.Text);
            this._produceOtherInDepot.ProduceTax = Convert.ToDouble(this.txt_Tax.Text == "" ? null : this.txt_Tax.Text);
            this._produceOtherInDepot.ProduceTotal = Convert.ToDouble(this.txt_Total.Text == "" ? null : this.txt_Total.Text);

            this._produceOtherInDepot.PayDate = this.PayDate.EditValue == null ? DateTime.Now : this.PayDate.DateTime;
            this._produceOtherInDepot.InvoiceTaxrate = (int)this.spe_InvoiceTaxrate.Value;

            Dictionary<string, string> dicSubject = new Dictionary<string, string>();
            //dicSubject.Add("進貨", null);
            dicSubject.Add(this.produceOtherInDepotManager.GetSubjectNameBySupplier(_produceOtherInDepot.Supplier), null);
            dicSubject.Add("進項稅額", null);
            dicSubject.Add(string.Format("應付帳款-{0}", this._produceOtherInDepot.Supplier.SupplierShortName), null);

            for (int i = 0; i < dicSubject.Count; i++)
            {
                string key = dicSubject.Keys.ToArray()[i];
                string value = atAccountSubjectManager.GetSubjectIdByName(key);
                if (string.IsNullOrEmpty(value))
                {
                    if (i != 2)
                        throw new Exception(string.Format("會計科目中無此科目：{0}，請先添加。", key));

                    try
                    {
                        BL.V.BeginTransaction();
                        //Model.AtAccountSubject atAccountSubject = new Model.AtAccountSubject();
                        //atAccountSubject.SubjectId = Guid.NewGuid().ToString();
                        //atAccountSubject.SubjectName = key;
                        //atAccountSubject.AccountingCategoryId = "31c7baf9-c21d-4075-8738-ebbaedd1c000";
                        //atAccountSubject.TheLending = "貸";
                        //atAccountSubject.TheBalance = 0;
                        //atAccountSubject.InsertTime = DateTime.Now;
                        //atAccountSubject.UpdateTime = DateTime.Now;
                        //atAccountSubject.Id = "select cast((select top 1 cast(Id as int) from AtAccountSubject where left(Id,4)='2144' order by Id desc )+1 as varchar(20))";
                        string subjectId = Guid.NewGuid().ToString();
                        string insertSql = string.Format("insert into AtAccountSubject values('{0}','{1}','',null,'31c7baf9-c21d-4075-8738-ebbaedd1c000','貸','0',null,null,null,null,GETDATE(),GETDATE(),(select cast((select top 1 cast(Id as int) from AtAccountSubject where left(Id,4)='2144' order by Id desc )+1 as varchar(20))),null,null)", subjectId, key);

                        this.produceOtherInDepotManager.UpdateSql(insertSql);
                        value = subjectId;

                        BL.V.CommitTransaction();
                    }
                    catch
                    {
                        BL.V.RollbackTransaction();
                        throw new Exception(string.Format("添加會計科目‘{0}’時出現錯誤，請聯繫管理員", key));
                    }
                }

                dicSubject[key] = value;
            }


            switch (this.action)
            {
                case "insert":
                    this.produceOtherInDepotManager.Insert(this._produceOtherInDepot);

                    this.produceOtherInDepotManager.InsertAtSummon(_produceOtherInDepot, dicSubject);
                    break;

                case "update":
                    this._produceOtherInDepot.EmployeeUpdate = BL.V.ActiveOperator.Employee;
                    this._produceOtherInDepot.EmployeeUpdateId = BL.V.ActiveOperator.EmployeeId;
                    this.produceOtherInDepotManager.Update(this._produceOtherInDepot);

                    this.produceOtherInDepotManager.UpdateAtSummon(_produceOtherInDepot, dicSubject);
                    break;
            }

        }

        protected override void Delete()
        {
            foreach (var item in this._produceOtherInDepot.Details)
            {
                if (this.pcOtherCheckDetailManager.IsHasByFromInvoiceDetailID(item.ProduceOtherInDepotDetailId))
                {
                    MessageBox.Show("已在進料檢驗單使用，請勿刪除！", this.Text, MessageBoxButtons.OK);
                    return;
                }
            }
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.produceOtherInDepotManager.Delete(this._produceOtherInDepot);

                this.produceOtherInDepotManager.DeleteAtSummon(this._produceOtherInDepot);

                this._produceOtherInDepot = this.produceOtherInDepotManager.GetNext(this._produceOtherInDepot);
                if (this._produceOtherInDepot == null)
                {
                    this._produceOtherInDepot = this.produceOtherInDepotManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        public override void Refresh()
        {

            if (this._produceOtherInDepot == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._produceOtherInDepot = this.produceOtherInDepotManager.GetDetails(_produceOtherInDepot.ProduceOtherInDepotId);
                }
            }
            if (this._produceOtherInDepot == null)
            {
                this._produceOtherInDepot = new Book.Model.ProduceOtherInDepot();
                this.AddNew();
                this.action = "insert";
            }

            this.textEditProduceOtherInDepotId.Text = this._produceOtherInDepot.ProduceOtherInDepotId;
            this.textEditProduceOtherInDepotDesc.Text = this._produceOtherInDepot.ProduceOtherInDepotDesc;
            //this.newChooseWorkHorseId.EditValue = this.produceOtherInDepot.WorkHouse;
            this.newChooseContorlSipu.EditValue = this._produceOtherInDepot.Supplier;
            if (global::Helper.DateTimeParse.DateTimeEquls(this._produceOtherInDepot.ProduceOtherInDepotDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceOtherInDepotDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceOtherInDepotDate.EditValue = this._produceOtherInDepot.ProduceOtherInDepotDate;
            }

            //this.textEditCusXOId.Text = string.Empty;
            //if (!string.IsNullOrEmpty(this._produceOtherInDepot.ProduceOtherCompactId))
            //{
            //    Model.ProduceOtherCompact produceOtherCompact = this.produceOtherCompactManager.Get(this._produceOtherInDepot.ProduceOtherCompactId);

            //    if (produceOtherCompact != null)
            //    {
            //        Model.MRSHeader mrsHeader = this.mRSHeaderManager.Get(produceOtherCompact.MRSHeaderId);
            //        if (mrsHeader != null)
            //        {
            //            Model.MPSheader mPSheader = this.mPSheaderManager.Get(mrsHeader.MPSheaderId);
            //            if (mPSheader != null)
            //            {
            //                Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mPSheader.InvoiceXOId);
            //                this.textEditCusXOId.Text = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
            //            }
            //        }
            //    }
            //}
            //this.textEditCusXOId.Text = this._produceOtherInDepot.InvoiceCusId;

            this.newChooseEmployee0.EditValue = this._produceOtherInDepot.Employee0;
            this.newChooseEmployee1.EditValue = this._produceOtherInDepot.Employee1;
            this.newChooseEmployeeUpdate.EditValue = this._produceOtherInDepot.EmployeeUpdate;
            this.newChooseContorlDepot.EditValue = this._produceOtherInDepot.Depot;
            this.txt_Sum.EditValue = this._produceOtherInDepot.ProduceAmount;
            this.txt_Tax.EditValue = this._produceOtherInDepot.ProduceTax;
            this.txt_Total.EditValue = this._produceOtherInDepot.ProduceTotal;

            this.EmpAudit.EditValue = this._produceOtherInDepot.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this._produceOtherInDepot.AuditState);

            this.PayDate.EditValue = this._produceOtherInDepot.PayDate;
            this.spe_InvoiceTaxrate.Value = this._produceOtherInDepot.InvoiceTaxrate;

            this.bindingSourceDetails.DataSource = this._produceOtherInDepot.Details;

            base.Refresh();
            this.newChooseEmployee0.Enabled = false;

            switch (this.action)
            {
                case "insert":
                case "update":
                    //this.gridView1.OptionsBehavior.Editable = true;
                    this.ColProductId.OptionsColumn.AllowEdit = true;
                    this.gridColumn2.OptionsColumn.AllowEdit = true;
                    this.gridColumnCustomPro.OptionsColumn.AllowEdit = true;
                    this.gridColumn14.OptionsColumn.AllowEdit = true;
                    this.gridColProduceQuantity.OptionsColumn.AllowEdit = true;
                    this.gridColProduceTransferQuantity.OptionsColumn.AllowEdit = true;
                    this.gridColProduceInDepotQuantity.OptionsColumn.AllowEdit = true;
                    this.gridColProcessPrice.OptionsColumn.AllowEdit = true;
                    this.gridColumn4.OptionsColumn.AllowEdit = true;
                    this.ColProduceMoney.OptionsColumn.AllowEdit = true;
                    this.gridColumn8.OptionsColumn.AllowEdit = true;
                    this.gridColumn5.OptionsColumn.AllowEdit = true;
                    this.gridColumn9.OptionsColumn.AllowEdit = true;
                    this.gridColumn13.OptionsColumn.AllowEdit = true;
                    this.gridColumn7.OptionsColumn.AllowEdit = true;
                    this.barButtonItem1.Enabled = true;
                    break;
                case "view":
                    //this.gridView1.OptionsBehavior.Editable = false;
                    this.ColProductId.OptionsColumn.AllowEdit = false;
                    this.gridColumn2.OptionsColumn.AllowEdit = false;
                    this.gridColumnCustomPro.OptionsColumn.AllowEdit = false;
                    this.gridColumn14.OptionsColumn.AllowEdit = false;
                    this.gridColProduceQuantity.OptionsColumn.AllowEdit = false;
                    this.gridColProduceTransferQuantity.OptionsColumn.AllowEdit = false;
                    this.gridColProduceInDepotQuantity.OptionsColumn.AllowEdit = false;
                    this.gridColProcessPrice.OptionsColumn.AllowEdit = false;
                    this.gridColumn4.OptionsColumn.AllowEdit = false;
                    this.ColProduceMoney.OptionsColumn.AllowEdit = false;
                    this.gridColumn8.OptionsColumn.AllowEdit = false;
                    this.gridColumn5.OptionsColumn.AllowEdit = false;
                    this.gridColumn9.OptionsColumn.AllowEdit = false;
                    this.gridColumn13.OptionsColumn.AllowEdit = false;
                    this.gridColumn7.OptionsColumn.AllowEdit = false;
                    this.barButtonItem1.Enabled = false;
                    break;
            }
            this.simpleButton3.Enabled = true;
            this.newChooseEmployeeUpdate.ButtonReadOnly = true;
            this.newChooseEmployeeUpdate.Enabled = false;
            this.textEditProduceOtherInDepotId.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.ProduceOtherInDepot produceOtherInDepot = this.produceOtherInDepotManager.GetNext(this._produceOtherInDepot);
            if (produceOtherInDepot == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceOtherInDepot = this.produceOtherInDepotManager.Get(produceOtherInDepot.ProduceOtherInDepotId);
        }

        protected override void MovePrev()
        {
            Model.ProduceOtherInDepot produceOtherInDepot = this.produceOtherInDepotManager.GetPrev(this._produceOtherInDepot);
            if (produceOtherInDepot == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceOtherInDepot = this.produceOtherInDepotManager.Get(produceOtherInDepot.ProduceOtherInDepotId);
        }

        protected override void MoveFirst()
        {
            this._produceOtherInDepot = this.produceOtherInDepotManager.Get(this.produceOtherInDepotManager.GetFirst() == null ? "" : this.produceOtherInDepotManager.GetFirst().ProduceOtherInDepotId);
        }

        protected override void MoveLast()
        {
            //if (this._produceOtherInDepot == null)
            //{
            if (this.flag == 1)
            {
                this.flag = 0; return;
            }
            this._produceOtherInDepot = this.produceOtherInDepotManager.Get(this.produceOtherInDepotManager.GetLast() == null ? "" : this.produceOtherInDepotManager.GetLast().ProduceOtherInDepotId);
            //}
        }

        protected override bool HasRows()
        {
            return this.produceOtherInDepotManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceOtherInDepotManager.HasRowsAfter(this._produceOtherInDepot);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceOtherInDepotManager.HasRowsBefore(this._produceOtherInDepot);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(_produceOtherInDepot.ProduceOtherInDepotId);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ProduceOtherCompact.ChooseOtherCompactDetailForm f = new Book.UI.produceManager.ProduceOtherCompact.ChooseOtherCompactDetailForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (_produceOtherCompactDetail.Count != 0)
            {
                this._produceOtherInDepot.Details.Clear();

                if (_produceOtherCompactDetail != null)
                {
                    foreach (Model.ProduceOtherCompactDetail ProduceOtherCompactDetail in _produceOtherCompactDetail)
                    {
                        Model.ProduceOtherInDepotDetail produceOtherInDepotDetail = new Book.Model.ProduceOtherInDepotDetail();
                        produceOtherInDepotDetail.ProduceOtherInDepotDetailId = Guid.NewGuid().ToString();

                        produceOtherInDepotDetail.Product = ProduceOtherCompactDetail.Product;
                        produceOtherInDepotDetail.ProductId = ProduceOtherCompactDetail.ProductId;
                        produceOtherInDepotDetail.ProductUnit = ProduceOtherCompactDetail.Product.MainUnit == null ? string.Empty : ProduceOtherCompactDetail.Product.MainUnit.CnName;

                        produceOtherInDepotDetail.ProduceOtherCompactDetailId = ProduceOtherCompactDetail.ProduceOtherCompactId;
                        produceOtherInDepotDetail.ProduceQuantity = ProduceOtherCompactDetail.OtherCompactCount;
                        produceOtherInDepotDetail.ProducePrice = ProduceOtherCompactDetail.OtherCompactPrice;
                        produceOtherInDepotDetail.ProduceMoney = ProduceOtherCompactDetail.OtherCompactMoney;
                        produceOtherInDepotDetail.ProduceOtherInDepot = this._produceOtherInDepot;
                        produceOtherInDepotDetail.ProduceOtherInDepotId = this._produceOtherInDepot.ProduceOtherInDepotId;

                        produceOtherInDepotDetail.InvoiceXOId = ProduceOtherCompactDetail.InvoiceXOId;
                        produceOtherInDepotDetail.InvoiceXODetailId = ProduceOtherCompactDetail.InvoiceXODetailId;
                        this._produceOtherInDepot.Details.Add(produceOtherInDepotDetail);

                    }
                }
                this.gridControl1.RefreshDataSource();
                _produceOtherCompactDetail.Clear();

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this._produceOtherInDepot.Details.Count > 0 && this._produceOtherInDepot.Details[0] != null && string.IsNullOrEmpty(this._produceOtherInDepot.Details[0].ProductId))
                    this._produceOtherInDepot.Details.RemoveAt(0);
                Model.ProduceOtherInDepotDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.ProduceOtherInDepotDetail();
                        detail.ProduceOtherInDepotDetailId = Guid.NewGuid().ToString();
                        // detail.Inumber = this._produceMaterial.Details.Count + 1;
                        detail.Product = this.productManager.Get(product.ProductId);
                        detail.ProductId = detail.Product.ProductId;
                        detail.ProductGuige = (f.SelectedItem as Model.Product).ProductSpecification;
                        detail.ProduceQuantity = 0;
                        detail.ProducePrice = 0;
                        detail.ProduceMoney = 0;
                        detail.ProcessPrice = 0;
                        detail.ProduceInDepotQuantity = 0;
                        detail.ProduceTransferQuantity = 0;
                        if (detail.Product.DepotUnit != null)
                            detail.ProductUnit = detail.Product.DepotUnit.CnName;
                        this._produceOtherInDepot.Details.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.ProduceOtherInDepotDetail();
                    Model.Product product = f.SelectedItem as Model.Product;
                    //  detail.Inumber = this._produceOtherInDepot.Details.Count + 1;
                    detail.ProduceOtherInDepotDetailId = Guid.NewGuid().ToString();
                    detail.Product = this.productManager.Get(product.ProductId);
                    detail.ProductId = detail.Product.ProductId;
                    detail.ProductGuige = detail.Product.ProductSpecification;
                    detail.ProduceQuantity = 0;
                    detail.ProducePrice = 0;
                    detail.ProduceMoney = 0;
                    detail.ProcessPrice = 0;
                    detail.ProduceInDepotQuantity = 0;
                    detail.ProduceTransferQuantity = 0;
                    if (detail.Product.DepotUnit != null)
                        detail.ProductUnit = detail.Product.DepotUnit.CnName;
                    this._produceOtherInDepot.Details.Add(detail);
                    //this.bindingSourceProductId.DataSource = productManager.Select();
                }
                this.gridControl1.RefreshDataSource();
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                f.Dispose();
                GC.Collect();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                if (this.pcOtherCheckDetailManager.IsHasByFromInvoiceDetailID((this.bindingSourceDetails.Current as Book.Model.ProduceOtherInDepotDetail).ProduceOtherInDepotDetailId))
                {
                    MessageBox.Show("已在進料檢驗單使用，請勿刪除！", this.Text, MessageBoxButtons.OK);
                    return;
                }
                this._produceOtherInDepot.Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceOtherInDepotDetail);

                if (this._produceOtherInDepot.Details.Count == 0)
                {
                    Model.ProduceOtherInDepotDetail detail = new Model.ProduceOtherInDepotDetail();
                    detail.ProduceOtherInDepotDetailId = Guid.NewGuid().ToString();
                    detail.ProduceQuantity = 0;
                    detail.ProducePrice = 0;
                    detail.ProductGuige = "";
                    detail.ProduceMoney = 0;
                    detail.ProcessPrice = 0;
                    detail.ProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this._produceOtherInDepot.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }

                decimal d = 0;
                foreach (Model.ProduceOtherInDepotDetail model in this._produceOtherInDepot.Details)
                {
                    d += Convert.ToDecimal(model.ProduceMoney);
                }
                this.txt_Sum.Text = d.ToString();
                this.txt_Tax.Text = (Convert.ToDouble(d) * 0.05).ToString();
                this.txt_Total.Text = (Convert.ToDouble(d) * (1.05)).ToString();

                this.gridControl1.RefreshDataSource();
            }
        }

        private void Editform_Load(object sender, EventArgs e)
        {
            this.bindingSourcDepot.DataSource = depotManager.Select();
            //this.bindingSourceDepotPositionId.DataSource = depotPositionManager.Select();
            //string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            //this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColProduceQuantity || e.Column == this.gridColProcessPrice)
            {
                decimal price = decimal.Zero;
                decimal quantity = decimal.Zero;

                if (e.Column == this.gridColProcessPrice)
                {
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out price);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColProduceQuantity).ToString(), out quantity);
                }
                if (e.Column == this.gridColProduceQuantity)
                {
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out quantity);
                    string PriceRange = string.Empty;
                    Model.ProduceOtherInDepotDetail detail = this.bindingSourceDetails.Current as Model.ProduceOtherInDepotDetail;

                    if ((this.newChooseContorlSipu.EditValue as Model.Supplier) == null)
                        MessageBox.Show("廠商為空，不能計算商品單價！", this.Text);
                    else
                        PriceRange = (new BL.SupplierProductManager()).GetPriceRangeForSupAndProduct((this.newChooseContorlSipu.EditValue as Model.Supplier).SupplierId, detail.ProductId);
                    if (string.IsNullOrEmpty(PriceRange))
                    {
                        decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColProcessPrice).ToString(), out price);
                    }
                    else
                    {
                        price = BL.SupplierProductManager.CountPrice(PriceRange, Convert.ToDouble(quantity));
                        this.gridView1.SetRowCellValue(e.RowHandle, this.gridColProcessPrice, price);
                    }
                }
                this.gridView1.SetRowCellValue(e.RowHandle, this.ColProduceMoney, price * quantity);

                UpdateMoneyFields();
            }

        }

        private void UpdateMoneyFields()
        {
            double taxRate = (double)this.spe_InvoiceTaxrate.Value / 100;

            decimal d = 0;
            foreach (Model.ProduceOtherInDepotDetail model in this._produceOtherInDepot.Details)
            {
                d += Convert.ToDecimal(model.ProduceMoney);
                model.ProduceTaxMoney = model.ProduceMoney * (decimal)taxRate;
            }

            this.txt_Sum.Text = d.ToString();
            this.txt_Tax.Text = (Convert.ToDouble(d) * taxRate).ToString();
            this.txt_Total.Text = (Convert.ToDouble(d) * (1 + taxRate)).ToString();

            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProduceOtherInDepotDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceOtherInDepotDetail;

            if (e.Column == this.ColProductId || e.Column == this.gridColumnCustomPro || e.Column == this.gridColumn2)
            {
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.ProduceOtherInDepotDetailId = Guid.NewGuid().ToString();
                    detail.ProduceQuantity = 0;
                    detail.ProducePrice = 0;
                    detail.ProduceMoney = 0;
                    detail.ProcessPrice = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;

                    detail.ProductUnit = p.MainUnit == null ? "" : p.MainUnit.CnName;
                    detail.ProductGuige = p.ProductSpecification;

                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            //if (e.Column == this.gridColumn7)
            //{
            //    detail.DepotPosition = null;
            //}
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherInDepotDetail> details = this.bindingSourceDetails.DataSource as IList<Model.ProduceOtherInDepotDetail>;
            if (details == null || details.Count < 1) return;

            Model.ProduceOtherInDepotDetail detail = details[e.ListSourceRowIndex];
            switch (e.Column.Name)
            {
                case "ColProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Product.Id) ? "" : detail.Product.Id;
                    break;
                //case "gridColumn15":    //未到货数量，订单数量-截止到该入库单所有已进货数量
                //    double hasInQty = produceOtherInDepotDetailManager.SelectHasInQty(detail.ProduceOtherCompactDetailId, detail.ProduceOtherInDepotId);
                //    double noInQty = detail.OrderQuantity.HasValue ? detail.OrderQuantity.Value - hasInQty : 0;

                //    e.DisplayText = (noInQty < 0 ? 0 : noInQty).ToString();
                //break;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                //if (this.gridView1.FocusedColumn.Name == "gridColumn8")
                //{
                //    Model.Depot tem = this.newChooseContorlDepot.EditValue as Model.Depot;
                //    if (tem != null)
                //    {
                //        this.repositoryItemComboBox2.Items.Clear();
                //        if (tem.DepotId != null)
                //        {
                //            IList<Model.DepotPosition> unitList = depotPositionManager.Select(tem.DepotId);
                //            foreach (Model.DepotPosition item in unitList)
                //            {
                //                this.repositoryItemComboBox2.Items.Add(item.Id);
                //            }

                //        }
                //        //this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                //    }

                //}
                if (this.gridView1.FocusedColumn.Name == "gridColumn4")
                {

                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceOtherInDepotDetail).Product;

                        this.repositoryItemComboBox1.Items.Clear();
                        if (p != null)
                        {
                            if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                            {
                                BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                                Model.UnitGroup ug = new BL.UnitGroupManager().Get(p.BasedUnitGroupId);
                                IList<Model.ProductUnit> unitList = manager.Select(ug);
                                foreach (Model.ProductUnit item in unitList)
                                {
                                    this.repositoryItemComboBox1.Items.Add(item.CnName);
                                }
                            }

                        }
                    }
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        public static Model.ProduceOtherCompact _compact = new Book.Model.ProduceOtherCompact();

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Model.ProduceOtherInDepotDetail detail;
            ChooseProduceOtherCompactForm form = new ChooseProduceOtherCompactForm("product");
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this._produceOtherInDepot.Details.Clear();
                foreach (var item in OtherCompactDetailManager.SelectIsInDepot(_compact))
                {
                    detail = new Book.Model.ProduceOtherInDepotDetail();
                    detail.ProduceOtherInDepotDetailId = Guid.NewGuid().ToString();
                    detail.ProduceQuantity = item.NoInDepotCount;
                    detail.ProducePrice = item.OtherCompactPrice;
                    detail.ProductGuige = "";
                    detail.ProduceMoney = item.OtherCompactMoney;
                    detail.ProduceOtherCompactDetailId = item.OtherCompactDetailId;
                    detail.ProcessPrice = 0;
                    detail.ProductUnit = item.ProductUnit;
                    detail.ProductId = item.ProductId;
                    detail.Product = item.Product;
                    this._produceOtherInDepot.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseContorlDepot.EditValue != null)
            {
                this.bindingSourceDepotPositionId.DataSource = depotPositionManager.Select(this.newChooseContorlDepot.EditValue as Model.Depot);
                this.gridControl1.RefreshDataSource();
            }
        }

        //选择委外合同
        private void simpleButtonOther_Click_1(object sender, EventArgs e)
        {
            ProduceOtherCompact.ChooseOutContract f = new ProduceOtherCompact.ChooseOutContract();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (f.key == null || f.key.Count == 0) return;
            Model.ProduceOtherCompact OtherCompact = f.key[0].ProduceOtherCompact;

            if (this._produceOtherInDepot.Details.Count > 0 && string.IsNullOrEmpty(this._produceOtherInDepot.Details[0].ProductId))
                this._produceOtherInDepot.Details.RemoveAt(0);

            this.newChooseContorlSipu.EditValue = OtherCompact.Supplier;
            //this.textEditCusXOId.Text = OtherCompact.CustomerInvoiceXOId;
            //if (!string.IsNullOrEmpty(OtherCompact.MRSHeaderId))
            //{
            //    Model.MRSHeader mrsHeader = this.mRSHeaderManager.Get(OtherCompact.MRSHeaderId);
            //    if (mrsHeader != null)
            //    {
            //        Model.MPSheader mPSheader = this.mPSheaderManager.Get(mrsHeader.MPSheaderId);
            //        if (mPSheader != null)
            //        {
            //            Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mPSheader.InvoiceXOId);
            //            this.textEditCusXOId.Text = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
            //        }
            //    }
            //}
            foreach (Model.ProduceOtherCompactDetail item in f.key)
            {
                Model.ProduceOtherInDepotDetail detail = new Model.ProduceOtherInDepotDetail();
                detail.ProduceOtherInDepotDetailId = Guid.NewGuid().ToString();
                detail.ProduceOtherCompactDetailId = item.OtherCompactDetailId;
                detail.CustomerId = item.ProduceOtherCompact.CustomerId;
                //客户订单编号
                //detail.InvoiceCusId = item.CustomInvoiceXOId;
                if (item.ProduceOtherCompact.InvoiceXO == null)
                    detail.InvoiceCusId = item.CustomInvoiceXOId;
                else
                {
                    detail.InvoiceCusId = item.ProduceOtherCompact.InvoiceXO.CustomerInvoiceXOId;
                }
                detail.OrderQuantity = item.OtherCompactCount;
                detail.NotArriveQuantity = item.OtherCompactCount - item.InDepotCount;
                detail.Product = item.Product;
                detail.ProductId = item.ProductId;
                detail.ProductUnit = item.ProductUnit;
                //detail.ProduceQuantity = item.OtherCompactCount - Convert.ToDouble(item.InDepotCount);
                detail.ProduceQuantity = 0;
                detail.ProduceInDepotQuantity = 0;
                detail.ProduceTransferQuantity = 0;
                detail.ProcessPrice = item.OtherCompactPrice;
                detail.Description = item.Description;
                detail.ProduceOtherCompactId = item.ProduceOtherCompactId;
                detail.ProduceMoney = detail.ProcessPrice * Convert.ToDecimal(detail.ProduceQuantity);
                this._produceOtherInDepot.Details.Add(detail);
            }
            this.gridControl1.RefreshDataSource();

            decimal d = 0;
            foreach (Model.ProduceOtherInDepotDetail model in this._produceOtherInDepot.Details)
            {
                d += Convert.ToDecimal(model.ProduceMoney);
            }
            this.txt_Sum.Text = d.ToString();
            this.txt_Tax.Text = (Convert.ToDouble(d) * 0.05).ToString();
            this.txt_Total.Text = (Convert.ToDouble(d) * (1.05)).ToString();
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm lf = new ListForm();
            if (lf.ShowDialog() == DialogResult.OK)
            {
                this._produceOtherInDepot = lf.SelectItem as Model.ProduceOtherInDepot;
                this.action = "view";
                this.Refresh();
            }
            GC.Collect();
            lf.Dispose();
        }

        private void textEditProduceOtherInDepotDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditProduceOtherInDepotDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核
        protected override string AuditKeyId()
        {
            return Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotId;
        }

        protected override int AuditState()
        {
            return this._produceOtherInDepot.AuditState.HasValue ? this._produceOtherInDepot.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProduceOtherInDepot" + "," + this._produceOtherInDepot.ProduceOtherInDepotId;
        }
        #endregion

        //生成進料檢驗單
        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            if (this._produceOtherInDepot.Details.Where(d => d.IsCheck == true).ToList().Count == 0)
            {
                MessageBox.Show("請選擇要生成的商品！", this.Text, MessageBoxButtons.OK);
                return;
            }
            if (_produceOtherInDepot.Details.Any(d => d.IsCheck == true && !string.IsNullOrEmpty(d.GenerateInputCheck)))
            {
                MessageBox.Show("選中的商品已經生成過進料檢驗單！", this.Text, MessageBoxButtons.OK);
                return;
            }
            IList<Model.ProduceOtherInDepotDetail> list = _produceOtherInDepot.Details.Where(d => d.IsCheck == true).ToList();
            if (list == null || list.Count == 0)
                return;
            produceManager.PCOtherCheck.EditForm f = new Book.UI.produceManager.PCOtherCheck.EditForm(list);
            f.ShowDialog(this);
            this._produceOtherInDepot.Details = this.produceOtherInDepotDetailManager.Select(_produceOtherInDepot);
            this.bindingSourceDetails.DataSource = _produceOtherInDepot.Details;
            this.gridControl1.RefreshDataSource();
        }

        //税率变化
        private void spe_InvoiceTaxrate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action != "view")
                UpdateMoneyFields();
        }

        //委外厂商变更，税率变更
        private void newChooseContorlSipu_EditValueChanged(object sender, EventArgs e)
        {
            if (newChooseContorlSipu.EditValue != null && this.action != "view")
            {
                Model.Supplier supplier = newChooseContorlSipu.EditValue as Model.Supplier;
                if (supplier.TaxRateP5.HasValue && supplier.TaxRateP5.Value && supplier.TaxCaluType == 1)
                {
                    this.spe_InvoiceTaxrate.Value = 5;
                }
                else
                    this.spe_InvoiceTaxrate.Value = 0;
            }
        }
    }
}