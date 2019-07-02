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
    public partial class EditFormResilience : Settings.BasicData.BaseEditForm
    {
        Model.PCEarplugsResilienceCheck _pCEarplugsResilienceCheck;
        BL.PCEarplugsResilienceCheckManager _pCEarplugsResilienceCheckManager = new Book.BL.PCEarplugsResilienceCheckManager();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();


        public EditFormResilience()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.PCEarplugsResilienceCheck.PRO_PCEarplugsResilienceCheckDate, new AA("日期不能爲空", this.date_Check));

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
                if (this.gridView1.Columns[i].Name == "gridColumn4" || this.gridView1.Columns[i].Name == "gridColumn5" || this.gridView1.Columns[i].Name == "gridColumn6")
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

            this.bindingSourceProduct.DataSource = this._pCEarplugsResilienceCheckManager.Query("select ProductId,Id,ProductName from Product", 30, "Product").Tables[0];

            var tiekuaiya = new BL.SettingManager().SelectByName("EarplugsResilience");
            if (tiekuaiya != null && tiekuaiya.Count > 0)
            {
                foreach (var item in tiekuaiya)
                {
                    this.cob_Tiekuaiya.Properties.Items.Add(item.SettingCurrentValue);
                }
            }
            var shoucuorou = new BL.SettingManager().SelectByName("EarplugsResilience2");
            if (shoucuorou != null && shoucuorou.Count > 0)
            {
                foreach (var item in shoucuorou)
                {
                    this.cob_Shoucuorou.Properties.Items.Add(item.SettingCurrentValue);
                }
            }
        }

        int LastFlag = 0; //页面载 入时是否执行 last方法
        public EditFormResilience(string pCEarplugsResilienceCheckId)
            : this()
        {
            this._pCEarplugsResilienceCheck = this._pCEarplugsResilienceCheckManager.Get(pCEarplugsResilienceCheckId);
            if (this._pCEarplugsResilienceCheck == null)
                throw new ArithmeticException("PCEarplugsResilienceCheckId");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditFormResilience(Model.PCEarplugsResilienceCheck pCEarplugsResilienceCheck)
            : this()
        {
            if (pCEarplugsResilienceCheck == null)
                throw new ArithmeticException("PCEarplugsResilienceCheckId");
            this._pCEarplugsResilienceCheck = pCEarplugsResilienceCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pCEarplugsResilienceCheck = new Book.Model.PCEarplugsResilienceCheck();
            this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId = this._pCEarplugsResilienceCheckManager.GetId();
            this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckDate = DateTime.Now;

            this._pCEarplugsResilienceCheck.Employee = BL.V.ActiveOperator.Employee;
            this._pCEarplugsResilienceCheck.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            if (this.cob_Tiekuaiya.Properties.Items.Count > 0)
                this._pCEarplugsResilienceCheck.TiekuaiyaCondition = this.cob_Tiekuaiya.Properties.Items[0].ToString();
            if (this.cob_Shoucuorou.Properties.Items.Count > 0)
                this._pCEarplugsResilienceCheck.ShoucuorouCondition = this.cob_Shoucuorou.Properties.Items[0].ToString();

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this._pCEarplugsResilienceCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pCEarplugsResilienceCheckManager.HasRowsAfter(this._pCEarplugsResilienceCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this._pCEarplugsResilienceCheckManager.HasRowsBefore(this._pCEarplugsResilienceCheck);
        }

        protected override void MoveFirst()
        {
            this._pCEarplugsResilienceCheck = this._pCEarplugsResilienceCheckManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._pCEarplugsResilienceCheck = this._pCEarplugsResilienceCheckManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.PCEarplugsResilienceCheck PCEarplugsResilienceCheck = this._pCEarplugsResilienceCheckManager.GetNext(this._pCEarplugsResilienceCheck);
            if (PCEarplugsResilienceCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCEarplugsResilienceCheck = PCEarplugsResilienceCheck;
        }

        protected override void MovePrev()
        {
            Model.PCEarplugsResilienceCheck PCEarplugsResilienceCheck = this._pCEarplugsResilienceCheckManager.GetPrev(this._pCEarplugsResilienceCheck);
            if (PCEarplugsResilienceCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCEarplugsResilienceCheck = PCEarplugsResilienceCheck;
        }

        public override void Refresh()
        {
            if (this._pCEarplugsResilienceCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._pCEarplugsResilienceCheck = this._pCEarplugsResilienceCheckManager.GetDetail(this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId);
            }

            this.txt_Id.EditValue = this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId;
            this.date_Check.EditValue = this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckDate;
            this.ncc_Employee.EditValue = this._pCEarplugsResilienceCheck.Employee;
            this.cob_Tiekuaiya.Text = this._pCEarplugsResilienceCheck.TiekuaiyaCondition;
            this.cob_Shoucuorou.Text = this._pCEarplugsResilienceCheck.ShoucuorouCondition;
            this.txt_Note.EditValue = this._pCEarplugsResilienceCheck.Note;

            this.bindingSourceDetail.DataSource = this._pCEarplugsResilienceCheck.Details;

            base.Refresh();

            switch (this.action)
            {
                case "view":
                    //this.gridView1.OptionsBehavior.Editable = false;
                    gridColumn3.OptionsColumn.AllowEdit = false;
                    gridColumn13.OptionsColumn.AllowEdit = false;
                    gridColumn14.OptionsColumn.AllowEdit = false;
                    gridColumn15.OptionsColumn.AllowEdit = false;
                    gridColumn9.OptionsColumn.AllowEdit = false;
                    gridColumn4.OptionsColumn.AllowEdit = false;
                    gridColumn5.OptionsColumn.AllowEdit = false;
                    gridColumn6.OptionsColumn.AllowEdit = false;
                    gridColumn10.OptionsColumn.AllowEdit = false;
                    break;
                default:
                    //this.gridView1.OptionsBehavior.Editable = true;
                    gridColumn3.OptionsColumn.AllowEdit = true;
                    gridColumn13.OptionsColumn.AllowEdit = true;
                    gridColumn14.OptionsColumn.AllowEdit = true;
                    gridColumn15.OptionsColumn.AllowEdit = true;
                    gridColumn9.OptionsColumn.AllowEdit = true;
                    gridColumn4.OptionsColumn.AllowEdit = true;
                    gridColumn5.OptionsColumn.AllowEdit = true;
                    gridColumn6.OptionsColumn.AllowEdit = true;
                    gridColumn10.OptionsColumn.AllowEdit = true;
                    break;
            }

            this.txt_Id.Properties.ReadOnly = true;

            this.gcTiekuaiya.Caption = this._pCEarplugsResilienceCheck.TiekuaiyaCondition;
            this.gcShoucuorou.Caption = this._pCEarplugsResilienceCheck.ShoucuorouCondition;
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId = this.txt_Id.Text;
            if (this.date_Check.EditValue != null)
                this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckDate = this.date_Check.DateTime;
            this._pCEarplugsResilienceCheck.EmployeeId = this.ncc_Employee.EditValue == null ? null : (this.ncc_Employee.EditValue as Model.Employee).EmployeeId;
            this._pCEarplugsResilienceCheck.TiekuaiyaCondition = this.cob_Tiekuaiya.Text;
            this._pCEarplugsResilienceCheck.ShoucuorouCondition = this.cob_Shoucuorou.Text;
            this._pCEarplugsResilienceCheck.Note = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this._pCEarplugsResilienceCheckManager.Insert(this._pCEarplugsResilienceCheck);
                    break;
                case "update":
                    this._pCEarplugsResilienceCheckManager.Update(this._pCEarplugsResilienceCheck);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._pCEarplugsResilienceCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pCEarplugsResilienceCheckManager.Delete(this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId);

            this._pCEarplugsResilienceCheck = this._pCEarplugsResilienceCheckManager.GetNext(this._pCEarplugsResilienceCheck);
            if (this._pCEarplugsResilienceCheck == null)
            {
                this._pCEarplugsResilienceCheck = this._pCEarplugsResilienceCheckManager.GetLast();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new ROResilience(this._pCEarplugsResilienceCheck);
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListFormResilience f = new ListFormResilience();
            f.ShowDialog();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    this._pCFlameRetardant = f.SelectItem as Model.PCFlameRetardant;
            //    this.action = "view";
            //    this.Refresh();
            //}
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
                        Model.PCEarplugsResilienceCheckDetail detail = new Book.Model.PCEarplugsResilienceCheckDetail();
                        detail.PCEarplugsResilienceCheckDetailId = Guid.NewGuid().ToString();
                        detail.Number = (this._pCEarplugsResilienceCheck.Details.Count + 1).ToString();
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


                        this._pCEarplugsResilienceCheck.Details.Add(detail);
                    }

                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        #region 审核

        //protected override string AuditKeyId()
        //{
        //    return Model.PCFlameRetardant.PRO_PCFlameRetardantId;
        //}

        //protected override int AuditState()
        //{
        //    return this._pCEarplugsResilienceCheck.AuditState.HasValue ? this._pCFlameRetardant.AuditState.Value : 0;
        //}

        protected override string tableCode()
        {
            return "PCEarplugsResilienceCheck" + "," + this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId;
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
                        Model.PCEarplugsResilienceCheckDetail detail = new Book.Model.PCEarplugsResilienceCheckDetail();
                        detail.PCEarplugsResilienceCheckDetailId = Guid.NewGuid().ToString();
                        detail.Number = (this._pCEarplugsResilienceCheck.Details.Count + 1).ToString();
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        this._pCEarplugsResilienceCheck.Details.Add(detail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    Model.PCEarplugsResilienceCheckDetail detail = new Book.Model.PCEarplugsResilienceCheckDetail();
                    detail.PCEarplugsResilienceCheckDetailId = Guid.NewGuid().ToString();
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    this._pCEarplugsResilienceCheck.Details.Add(detail);
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

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            ResilienceConditionSet f = new ResilienceConditionSet(this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId);
            f.ShowDialog();
        }

        private void repositoryItemHyperLinkEdit3_Click(object sender, EventArgs e)
        {
            ResilienceConditionSet f = new ResilienceConditionSet(this._pCEarplugsResilienceCheck.PCEarplugsResilienceCheckId);
            f.ShowDialog();
        }
    }
}