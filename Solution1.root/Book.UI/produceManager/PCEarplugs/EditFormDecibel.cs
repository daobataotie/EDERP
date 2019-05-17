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
    public partial class EditFormDecibel : Settings.BasicData.BaseEditForm
    {
        Model.PCEarplugsDecibelCheck _pCEarplugsDecibelCheck;
        BL.PCEarplugsDecibelCheckManager _pCEarplugsDecibelCheckManager = new Book.BL.PCEarplugsDecibelCheckManager();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();

        public EditFormDecibel()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.PCEarplugsDecibelCheck.PRO_PCEarplugsDecibelCheckDate, new AA("日期不能爲空", this.date_Check));

            this.ncc_Employee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";

            #region LookUpEdit
            //DataTable dt = new DataTable();
            //dt.Columns.Add("id", typeof(string));
            ////dt.Columns.Add("name", typeof(string));
            //DataRow dr;
            //dr = dt.NewRow();
            ////dr[0] = "";
            //dr[0] = " ";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            ////dr[0] = "0";
            //dr[0] = "√";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            ////dr[0] = "1";
            //dr[0] = "×";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            ////dr[0] = "2";
            //dr[0] = "△";
            //dt.Rows.Add(dr);

            //for (int i = 0; i < this.gridView1.Columns.Count; i++)
            //{
            //    if (this.gridView1.Columns[i].Name == "colParameter")
            //    {
            //        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
            //        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
            //        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).NullText = "";
            //        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            //        new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id",25, "标识"),
            //         });
            //        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "id";
            //        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "id";
            //    }
            //}
            #endregion

            this.bindingSourceProduct.DataSource = this._pCEarplugsDecibelCheckManager.Query("select ProductId,Id,ProductName from Product", 30, "Product").Tables[0];

            var testCondition = new BL.SettingManager().SelectByName("EarplugsDecibel");
            if (testCondition != null && testCondition.Count > 0)
            {
                foreach (var item in testCondition)
                {
                    this.cob_TestCondition.Properties.Items.Add(item.SettingCurrentValue);
                }
            }
        }

        int LastFlag = 0; //页面载 入时是否执行 last方法
        public EditFormDecibel(string pCEarplugsDecibelCheckId)
            : this()
        {
            this._pCEarplugsDecibelCheck = this._pCEarplugsDecibelCheckManager.Get(pCEarplugsDecibelCheckId);
            if (this._pCEarplugsDecibelCheck == null)
                throw new ArithmeticException("PCEarplugsDecibelCheckId");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditFormDecibel(Model.PCEarplugsDecibelCheck pCEarplugsDecibelCheck)
            : this()
        {
            if (pCEarplugsDecibelCheck == null)
                throw new ArithmeticException("PCEarplugsDecibelCheckId");
            this._pCEarplugsDecibelCheck = pCEarplugsDecibelCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pCEarplugsDecibelCheck = new Book.Model.PCEarplugsDecibelCheck();
            this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId = this._pCEarplugsDecibelCheckManager.GetId();
            this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckDate = DateTime.Now;

            this._pCEarplugsDecibelCheck.Employee = BL.V.ActiveOperator.Employee;
            this._pCEarplugsDecibelCheck.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            if (this.cob_TestCondition.Properties.Items.Count > 0)
                this._pCEarplugsDecibelCheck.TestCondition = this.cob_TestCondition.Properties.Items[0].ToString();

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this._pCEarplugsDecibelCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pCEarplugsDecibelCheckManager.HasRowsAfter(this._pCEarplugsDecibelCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this._pCEarplugsDecibelCheckManager.HasRowsBefore(this._pCEarplugsDecibelCheck);
        }

        protected override void MoveFirst()
        {
            this._pCEarplugsDecibelCheck = this._pCEarplugsDecibelCheckManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._pCEarplugsDecibelCheck = this._pCEarplugsDecibelCheckManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.PCEarplugsDecibelCheck PCEarplugsDecibelCheck = this._pCEarplugsDecibelCheckManager.GetNext(this._pCEarplugsDecibelCheck);
            if (PCEarplugsDecibelCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCEarplugsDecibelCheck = PCEarplugsDecibelCheck;
        }

        protected override void MovePrev()
        {
            Model.PCEarplugsDecibelCheck PCEarplugsDecibelCheck = this._pCEarplugsDecibelCheckManager.GetPrev(this._pCEarplugsDecibelCheck);
            if (PCEarplugsDecibelCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCEarplugsDecibelCheck = PCEarplugsDecibelCheck;
        }

        public override void Refresh()
        {
            if (this._pCEarplugsDecibelCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._pCEarplugsDecibelCheck = this._pCEarplugsDecibelCheckManager.GetDetail(this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId);
            }

            this.txt_Id.EditValue = this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId;
            this.date_Check.EditValue = this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckDate;
            this.ncc_Employee.EditValue = this._pCEarplugsDecibelCheck.Employee;
            this.cob_TestCondition.Text = this._pCEarplugsDecibelCheck.TestCondition;
            this.txt_Note.EditValue = this._pCEarplugsDecibelCheck.Note;

            this.bindingSourceDetail.DataSource = this._pCEarplugsDecibelCheck.Details;

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


            this.colParameter.Caption = "音頻  " + this._pCEarplugsDecibelCheck.TestCondition;
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId = this.txt_Id.Text;
            if (this.date_Check.EditValue != null)
                this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckDate = this.date_Check.DateTime;
            this._pCEarplugsDecibelCheck.EmployeeId = this.ncc_Employee.EditValue == null ? null : (this.ncc_Employee.EditValue as Model.Employee).EmployeeId;
            this._pCEarplugsDecibelCheck.TestCondition = this.cob_TestCondition.Text;
            this._pCEarplugsDecibelCheck.Note = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this._pCEarplugsDecibelCheckManager.Insert(this._pCEarplugsDecibelCheck);
                    break;
                case "update":
                    this._pCEarplugsDecibelCheckManager.Update(this._pCEarplugsDecibelCheck);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._pCEarplugsDecibelCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pCEarplugsDecibelCheckManager.Delete(this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId);

            this._pCEarplugsDecibelCheck = this._pCEarplugsDecibelCheckManager.GetNext(this._pCEarplugsDecibelCheck);
            if (this._pCEarplugsDecibelCheck == null)
            {
                this._pCEarplugsDecibelCheck = this._pCEarplugsDecibelCheckManager.GetLast();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RODecibel(this._pCEarplugsDecibelCheck);
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListFormDecibel f = new ListFormDecibel();
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
                        Model.PCEarplugsDecibelCheckDetail detail = new Book.Model.PCEarplugsDecibelCheckDetail();
                        detail.PCEarplugsDecibelCheckDetailId = Guid.NewGuid().ToString();
                        detail.Number = (this._pCEarplugsDecibelCheck.Details.Count + 1).ToString();
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


                        this._pCEarplugsDecibelCheck.Details.Add(detail);
                    }

                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCEarplugsDecibelCheck.PRO_PCEarplugsDecibelCheckId;
        }

        protected override int AuditState()
        {
            return this._pCEarplugsDecibelCheck.AuditState.HasValue ? this._pCEarplugsDecibelCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCEarplugsDecibelCheck" + "," + this._pCEarplugsDecibelCheck.PCEarplugsDecibelCheckId;
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
                        Model.PCEarplugsDecibelCheckDetail detail = new Book.Model.PCEarplugsDecibelCheckDetail();
                        detail.PCEarplugsDecibelCheckDetailId = Guid.NewGuid().ToString();
                        detail.Number = (this._pCEarplugsDecibelCheck.Details.Count + 1).ToString();
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        this._pCEarplugsDecibelCheck.Details.Add(detail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    Model.PCEarplugsDecibelCheckDetail detail = new Book.Model.PCEarplugsDecibelCheckDetail();
                    detail.PCEarplugsDecibelCheckDetailId = Guid.NewGuid().ToString();
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    this._pCEarplugsDecibelCheck.Details.Add(detail);
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