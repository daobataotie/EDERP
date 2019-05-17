using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCEarplugs
{
    public partial class EditFormStayWire : Settings.BasicData.BaseEditForm
    {
        Model.PCEarplugsStayWireCheck _pCEarplugsStayWireCheck;
        BL.PCEarplugsStayWireCheckManager _pCEarplugsStayWireCheckManager = new Book.BL.PCEarplugsStayWireCheckManager();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();


        public EditFormStayWire()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.PCEarplugsStayWireCheck.PRO_PCEarplugsStayWireCheckDate, new AA("日期不能爲空", this.date_Check));

            this.ncc_Employee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";

            #region LookUpEdit
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            //dt.Columns.Add("name", typeof(string));
            DataRow dr;
            dr = dt.NewRow();
            //dr[0] = "";
            dr[0] = " ";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "0";
            dr[0] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "1";
            dr[0] = "×";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "2";
            dr[0] = "△";
            dt.Rows.Add(dr);

            for (int i = 0; i < this.gridView1.Columns.Count; i++)
            {
                if (this.gridView1.Columns[i].Name == "gridColumn4" || this.gridView1.Columns[i].Name == "gridColumn5" || this.gridView1.Columns[i].Name == "colParameter")
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).NullText = "";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id",25, "标识"),
                     });
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "id";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "id";
                }
            }
            #endregion

            this.bindingSourceProduct.DataSource = this._pCEarplugsStayWireCheckManager.Query("select ProductId,Id,ProductName from Product", 30, "Product").Tables[0];

            var testCondition = new BL.SettingManager().SelectByName("EarplugsStayWire");
            if (testCondition != null && testCondition.Count > 0)
            {
                foreach (var item in testCondition)
                {
                    this.cob_TestCondition.Properties.Items.Add(item.SettingCurrentValue);
                }
            }
        }

        int LastFlag = 0; //页面载 入时是否执行 last方法
        public EditFormStayWire(string pCEarplugsStayWireCheckId)
            : this()
        {
            this._pCEarplugsStayWireCheck = this._pCEarplugsStayWireCheckManager.Get(pCEarplugsStayWireCheckId);
            if (this._pCEarplugsStayWireCheck == null)
                throw new ArithmeticException("PCEarplugsStayWireCheckId");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditFormStayWire(Model.PCEarplugsStayWireCheck pCEarplugsStayWireCheck)
            : this()
        {
            if (pCEarplugsStayWireCheck == null)
                throw new ArithmeticException("PCEarplugsStayWireCheckId");
            this._pCEarplugsStayWireCheck = pCEarplugsStayWireCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pCEarplugsStayWireCheck = new Book.Model.PCEarplugsStayWireCheck();
            this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId = this._pCEarplugsStayWireCheckManager.GetId();
            this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckDate = DateTime.Now;

            this._pCEarplugsStayWireCheck.Employee = BL.V.ActiveOperator.Employee;
            this._pCEarplugsStayWireCheck.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            if (this.cob_TestCondition.Properties.Items.Count > 0)
                this._pCEarplugsStayWireCheck.TestCondition = this.cob_TestCondition.Properties.Items[0].ToString();

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this._pCEarplugsStayWireCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pCEarplugsStayWireCheckManager.HasRowsAfter(this._pCEarplugsStayWireCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this._pCEarplugsStayWireCheckManager.HasRowsBefore(this._pCEarplugsStayWireCheck);
        }

        protected override void MoveFirst()
        {
            this._pCEarplugsStayWireCheck = this._pCEarplugsStayWireCheckManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._pCEarplugsStayWireCheck = this._pCEarplugsStayWireCheckManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.PCEarplugsStayWireCheck PCEarplugsStayWireCheck = this._pCEarplugsStayWireCheckManager.GetNext(this._pCEarplugsStayWireCheck);
            if (PCEarplugsStayWireCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCEarplugsStayWireCheck = PCEarplugsStayWireCheck;
        }

        protected override void MovePrev()
        {
            Model.PCEarplugsStayWireCheck PCEarplugsStayWireCheck = this._pCEarplugsStayWireCheckManager.GetPrev(this._pCEarplugsStayWireCheck);
            if (PCEarplugsStayWireCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCEarplugsStayWireCheck = PCEarplugsStayWireCheck;
        }

        public override void Refresh()
        {
            if (this._pCEarplugsStayWireCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._pCEarplugsStayWireCheck = this._pCEarplugsStayWireCheckManager.GetDetail(this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId);
            }

            this.txt_Id.EditValue = this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId;
            this.date_Check.EditValue = this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckDate;
            this.ncc_Employee.EditValue = this._pCEarplugsStayWireCheck.Employee;
            this.cob_TestCondition.Text = this._pCEarplugsStayWireCheck.TestCondition;
            this.txt_Note.EditValue = this._pCEarplugsStayWireCheck.Note;

            this.bindingSourceDetail.DataSource = this._pCEarplugsStayWireCheck.Details;

            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
            }

            this.txt_Id.Properties.ReadOnly = true;

            this.colParameter.Caption = this._pCEarplugsStayWireCheck.TestCondition;
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId = this.txt_Id.Text;
            if (this.date_Check.EditValue != null)
                this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckDate = this.date_Check.DateTime;
            this._pCEarplugsStayWireCheck.EmployeeId = this.ncc_Employee.EditValue == null ? null : (this.ncc_Employee.EditValue as Model.Employee).EmployeeId;
            this._pCEarplugsStayWireCheck.TestCondition = this.cob_TestCondition.Text;
            this._pCEarplugsStayWireCheck.Note = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this._pCEarplugsStayWireCheckManager.Insert(this._pCEarplugsStayWireCheck);
                    break;
                case "update":
                    this._pCEarplugsStayWireCheckManager.Update(this._pCEarplugsStayWireCheck);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._pCEarplugsStayWireCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pCEarplugsStayWireCheckManager.Delete(this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId);

            this._pCEarplugsStayWireCheck = this._pCEarplugsStayWireCheckManager.GetNext(this._pCEarplugsStayWireCheck);
            if (this._pCEarplugsStayWireCheck == null)
            {
                this._pCEarplugsStayWireCheck = this._pCEarplugsStayWireCheckManager.GetLast();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new ROStayWire(this._pCEarplugsStayWireCheck);
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListFormStayWire f = new ListFormStayWire();
            f.ShowDialog();
        }

        //选择加工单
        private void btn_PronoteHeader_Click(object sender, EventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectItems != null)
                {
                    foreach (var item in f.SelectItems)
                    {
                        Model.PCEarplugsStayWireCheckDetail detail = new Book.Model.PCEarplugsStayWireCheckDetail();
                        detail.PCEarplugsStayWireCheckDetailId = Guid.NewGuid().ToString();
                        detail.Number = (this._pCEarplugsStayWireCheck.Details.Count + 1).ToString();
                        detail.FromId = item.PronoteHeaderID;
                        detail.Product = item.Product;
                        detail.ProductId = item.ProductId;
                        detail.ProductUnit = item.ProductUnit;
                        detail.InvoiceXOId = item.InvoiceXOId;
                        detail.InvoiceXOQuantity = item.InvoiceXODetailQuantity;

                        Model.InvoiceXO xo = invoiceXOManager.Get(detail.InvoiceXOId);
                        if (xo != null)
                        {
                            detail.InvoiceXO = xo;
                        }


                        this._pCEarplugsStayWireCheck.Details.Add(detail);
                    }

                    this.gridControl1.RefreshDataSource();
                }
            }
        }


        //委外加工單
        private void btn_SelectOtherCompact_Click(object sender, EventArgs e)
        {
            ProduceOtherCompact.ChooseOutContract f = new Book.UI.produceManager.ProduceOtherCompact.ChooseOutContract();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.key != null)
                {
                    foreach (var item in f.key)
                    {
                        Model.PCEarplugsStayWireCheckDetail detail = new Book.Model.PCEarplugsStayWireCheckDetail();
                        detail.PCEarplugsStayWireCheckDetailId = Guid.NewGuid().ToString();
                        detail.Number = (this._pCEarplugsStayWireCheck.Details.Count + 1).ToString();
                        detail.FromId = item.ProduceOtherCompactId;
                        detail.Product = item.Product;
                        detail.ProductId = item.ProductId;
                        detail.ProductUnit = item.ProductUnit;
                        if (!string.IsNullOrEmpty(item.ProduceOtherCompactId))
                        {
                            Model.ProduceOtherCompact model = new BL.ProduceOtherCompactManager().Get(item.ProduceOtherCompactId);
                            if (model != null)
                                detail.InvoiceXOId = model.InvoiceXOId;
                        }
                        detail.InvoiceXOQuantity = item.OtherCompactCount.HasValue ? item.OtherCompactCount.Value : 0;

                        Model.InvoiceXO xo = invoiceXOManager.Get(detail.InvoiceXOId);
                        if (xo != null)
                        {
                            detail.InvoiceXO = xo;
                        }

                        this._pCEarplugsStayWireCheck.Details.Add(detail);
                    }

                    this.gridControl1.RefreshDataSource();
                }
                f.Dispose();
                GC.Collect();
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCEarplugsStayWireCheck.PRO_PCEarplugsStayWireCheckId;
        }

        protected override int AuditState()
        {
            return this._pCEarplugsStayWireCheck.AuditState.HasValue ? this._pCEarplugsStayWireCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCEarplugsStayWireCheck" + "," + this._pCEarplugsStayWireCheck.PCEarplugsStayWireCheckId;
        }

        #endregion

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (Invoices.ChooseProductForm.ProductList != null && Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in Invoices.ChooseProductForm.ProductList)
                    {
                        Model.PCEarplugsStayWireCheckDetail detail = new Book.Model.PCEarplugsStayWireCheckDetail();
                        detail.PCEarplugsStayWireCheckDetailId = Guid.NewGuid().ToString();
                        detail.Number = (this._pCEarplugsStayWireCheck.Details.Count + 1).ToString();
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        this._pCEarplugsStayWireCheck.Details.Add(detail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    Model.PCEarplugsStayWireCheckDetail detail = new Book.Model.PCEarplugsStayWireCheckDetail();
                    detail.PCEarplugsStayWireCheckDetailId = Guid.NewGuid().ToString();
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    this._pCEarplugsStayWireCheck.Details.Add(detail);
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);

                this.gridControl1.RefreshDataSource();
            }
        }
    }
}