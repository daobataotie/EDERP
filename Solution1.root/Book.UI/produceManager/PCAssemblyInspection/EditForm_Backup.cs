using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCAssemblyInspection
{
    public partial class EditForm_Backup : Settings.BasicData.BaseEditForm
    {
        Model.PCAssemblyInspection _pCAssemblyInspection = null;
        BL.PCAssemblyInspectionManager manager = new Book.BL.PCAssemblyInspectionManager();

        int LastFlag = 0;
        public EditForm_Backup()
        {
            InitializeComponent();

            this.action = "view";

            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccEmployee1.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            this.invalidValueExceptions.Add(Model.PCAssemblyInspection.PRO_PCAssemblyInspectionDate, new AA("日期不能为空", this.date_PCAssemblyInspectionDate));
            this.invalidValueExceptions.Add(Model.PCAssemblyInspection.PRO_PronoteHeaderId, new AA("加工单不能为空", this.txt_PronoteHeaderId));

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
                if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit && this.gridView1.Columns[i].Name != "gridColumn2" && this.gridView1.Columns[i].Name != "gridColumn12" && this.gridView1.Columns[i].Name != "gridColumn17")
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

            this.bindingSourceCustomer.DataSource = new BL.CustomerManager().Select();
            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectOnActive();
            this.bindingSourceZuzhuangProduct.DataSource = new BL.ProductManager().SelectProductByProductCategoryId(new Book.Model.ProductCategory() { ProductCategoryId = "756c936b-4643-4963-ad11-4e63b86bed2f" });   //查詢所有塑膠袋
        }

        public EditForm_Backup(string PCAssemblyInspectionId)
            : this()
        {
            this._pCAssemblyInspection = this.manager.Get(PCAssemblyInspectionId);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm_Backup(Model.PCAssemblyInspection model)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._pCAssemblyInspection = model;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm_Backup(Model.PCAssemblyInspection model, string action)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._pCAssemblyInspection = model;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pCAssemblyInspection = new Book.Model.PCAssemblyInspection();
            this._pCAssemblyInspection.PCAssemblyInspectionId = this.manager.GetId();
            this._pCAssemblyInspection.PCAssemblyInspectionDate = DateTime.Now;
            this._pCAssemblyInspection.Employee = BL.V.ActiveOperator.Employee;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._pCAssemblyInspection);
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._pCAssemblyInspection);
        }

        protected override void MoveFirst()
        {
            this._pCAssemblyInspection = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._pCAssemblyInspection = this.manager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.PCAssemblyInspection p = this.manager.GetPrev(this._pCAssemblyInspection);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCAssemblyInspection = p;
        }

        protected override void MoveNext()
        {
            Model.PCAssemblyInspection p = this.manager.GetNext(this._pCAssemblyInspection);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCAssemblyInspection = p;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            this._pCAssemblyInspection.PCAssemblyInspectionId = this.txt_PCAssemblyInspectionId.Text;
            if (this.date_PCAssemblyInspectionDate.EditValue != null)
                this._pCAssemblyInspection.PCAssemblyInspectionDate = this.date_PCAssemblyInspectionDate.DateTime;
            else
                this._pCAssemblyInspection.PCAssemblyInspectionDate = null;
            this._pCAssemblyInspection.PronoteHeaderId = this.txt_PronoteHeaderId.Text;
            this._pCAssemblyInspection.InvoiceCusId = this.txt_InvoiceCusId.Text;
            this._pCAssemblyInspection.EmployeeId = this.nccEmployee.EditValue == null ? null : (this.nccEmployee.EditValue as Model.Employee).EmployeeId;

            this._pCAssemblyInspection.Note = this.txt_Note.Text;

            this._pCAssemblyInspection.EmployeeId1 = (this.nccEmployee1.EditValue as Model.Employee) == null ? null : (this.nccEmployee1.EditValue as Model.Employee).EmployeeId;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._pCAssemblyInspection);
                    break;
                case "update":
                    this.manager.Update(this._pCAssemblyInspection);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._pCAssemblyInspection == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._pCAssemblyInspection = this.manager.GetDetail(this._pCAssemblyInspection.PCAssemblyInspectionId);
            }

            this.txt_PCAssemblyInspectionId.Text = this._pCAssemblyInspection.PCAssemblyInspectionId;
            this.date_PCAssemblyInspectionDate.EditValue = this._pCAssemblyInspection.PCAssemblyInspectionDate;
            this.txt_PronoteHeaderId.Text = this._pCAssemblyInspection.PronoteHeaderId;
            this.txt_InvoiceCusId.Text = this._pCAssemblyInspection.InvoiceCusId;
            this.nccEmployee.EditValue = this._pCAssemblyInspection.Employee;
            this.txt_Note.Text = this._pCAssemblyInspection.Note;
            this.nccEmployee1.EditValue = this._pCAssemblyInspection.Employee1;
            this.bindingSourceDetail.DataSource = this._pCAssemblyInspection.Details;

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

            this.txt_PronoteHeaderId.Properties.ReadOnly = true;
            this.txt_PCAssemblyInspectionId.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._pCAssemblyInspection);
        }

        protected override void Delete()
        {
            if (this._pCAssemblyInspection == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.PCAssemblyInspection model = this.manager.GetNext(this._pCAssemblyInspection);
                this.manager.Delete(this._pCAssemblyInspection.PCAssemblyInspectionId);
                if (model == null)
                    this._pCAssemblyInspection = this.manager.GetLast();
                else
                    this._pCAssemblyInspection = model;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this._pCAssemblyInspection.Details == null || this._pCAssemblyInspection.Details.Count == 0)
            {
                MessageBox.Show("请先从加工单拉取第一条测试内容。", this.Text, MessageBoxButtons.OK);
                return;
            }
            else
            {
                Model.PCAssemblyInspectionDetail detail = new Book.Model.PCAssemblyInspectionDetail();
                detail.PCAssemblyInspectionDetailId = Guid.NewGuid().ToString();
                detail.PCAssemblyInspectionId = this._pCAssemblyInspection.PCAssemblyInspectionId;
                detail.PCAssemblyInspectionDetailDate = DateTime.Now;
                detail.Product = this._pCAssemblyInspection.Details[0].Product;
                detail.ProductId = this._pCAssemblyInspection.Details[0].ProductId;

                if (detail.Product != null)
                {
                    if (detail.Product.IsQiangHua == true)
                        detail.Jiagongbie = "強化";
                    else if (detail.Product.IsFangWu == true)
                        detail.Jiagongbie = "防霧";
                    else if (detail.Product.IsNoQiangFang == true)
                        detail.Jiagongbie = "無強化防霧";
                }

                if (this._pCAssemblyInspection.PronoteHeader != null)
                    detail.CustomerId = (this._pCAssemblyInspection.PronoteHeader.InvoiceXO == null ? null : this._pCAssemblyInspection.PronoteHeader.InvoiceXO.CustomerId);

                this._pCAssemblyInspection.Details.Add(detail);
                this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);

            }

            this.gridControl1.RefreshDataSource();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            f.ShowDialog(this);
            this.Refresh();
        }

        private void barPronoteHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(null, 0);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                if (pronoForm.SelectItems == null || pronoForm.SelectItems.Count == 0)
                {
                    Model.PronoteHeader currentModel = pronoForm.SelectItem;
                    if (currentModel != null)
                    {
                        this.txt_PronoteHeaderId.Text = currentModel.PronoteHeaderID;
                        this._pCAssemblyInspection.PronoteHeader = currentModel;
                        Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(currentModel.InvoiceXOId);
                        if (xo != null)
                        {
                            currentModel.InvoiceXO = xo;
                            this.txt_InvoiceCusId.Text = xo.CustomerInvoiceXOId;
                        }
                        //this.txt_Model.Text = (currentModel.Product) == null ? "" : (currentModel.Product).CustomerProductName;
                        Model.Product p = new BL.ProductManager().Get(currentModel.ProductId);
                        Model.PCAssemblyInspectionDetail detail = new Book.Model.PCAssemblyInspectionDetail();
                        detail.PCAssemblyInspectionDetailId = Guid.NewGuid().ToString();
                        detail.PCAssemblyInspectionDetailDate = DateTime.Now;
                        detail.PCAssemblyInspectionId = this._pCAssemblyInspection.PCAssemblyInspectionId;
                        detail.Product = p;
                        detail.ProductId = currentModel.ProductId;
                        detail.CustomerId = xo == null ? null : xo.CustomerId;

                        if (p.IsQiangHua == true)
                            detail.Jiagongbie = "強化";
                        else if (p.IsFangWu == true)
                            detail.Jiagongbie = "防霧";
                        else if (p.IsNoQiangFang == true)
                            detail.Jiagongbie = "無強化防霧";

                        this._pCAssemblyInspection.Details.Add(detail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                else
                {
                    foreach(var item in pronoForm.SelectItems)
                    {
                        Model.Product p = new BL.ProductManager().Get(item.ProductId);
                        Model.PCAssemblyInspectionDetail detail = new Book.Model.PCAssemblyInspectionDetail();
                        detail.PCAssemblyInspectionDetailId = Guid.NewGuid().ToString();
                        detail.PCAssemblyInspectionDetailDate = DateTime.Now;
                        detail.PCAssemblyInspectionId = this._pCAssemblyInspection.PCAssemblyInspectionId;
                        detail.Product = p;
                        detail.ProductId = item.ProductId;
                        //detail.CustomerId = xo == null ? null : xo.CustomerId;

                        if (p.IsQiangHua == true)
                            detail.Jiagongbie = "強化";
                        else if (p.IsFangWu == true)
                            detail.Jiagongbie = "防霧";
                        else if (p.IsNoQiangFang == true)
                            detail.Jiagongbie = "無強化防霧";

                        this._pCAssemblyInspection.Details.Add(detail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                this.gridControl1.RefreshDataSource();

            }
            pronoForm.Dispose();
            GC.Collect();
        }

        protected override string tableCode()
        {
            return "PCAssemblyInspection" + "," + this._pCAssemblyInspection.PCAssemblyInspectionId;
        }


    }
}