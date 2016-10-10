using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProductOnlineCheck
{
    public partial class Editform : Settings.BasicData.BaseEditForm
    {
        BL.ProductOnlineCheckManager _ProductOnlineCheckManager = new Book.BL.ProductOnlineCheckManager();
        BL.ProductOnlineCheckDetailManager _detailManager = new Book.BL.ProductOnlineCheckDetailManager();
        Model.ProductOnlineCheck _ProductOnlineCheck;

        int flag = 0;

        public Editform()
        {
            InitializeComponent();
            this.newCCEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.requireValueExceptions.Add(Model.ProductOnlineCheck.PRO_ProductOnlineCheckDate, new AA(Properties.Resources.InvoiceDateNotNull, this.date_ProductOnlineCheck));
            this.requireValueExceptions.Add(Model.ProductOnlineCheck.PRO_OnlineDate, new AA(Properties.Resources.OnlineDateNotNull, this.date_Online));
            this.requireValueExceptions.Add(Model.ProductOnlineCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.txt_ProductName));

            DataTable dt = new DataTable();
            DataColumn dc;
            dc = new DataColumn("Id", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("Name", typeof(string));
            dt.Columns.Add(dc);
            DataRow dr;
            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "√";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "×";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "△";
            dt.Rows.Add(dr);
            this.repositoryItemLookUpEdit1.DataSource = dt;

            this.action = "view";
        }

        public Editform(string ProductOnlineCheckId)
            : this()
        {
            this._ProductOnlineCheck = this._ProductOnlineCheckManager.Get(ProductOnlineCheckId);
            if (this._ProductOnlineCheck != null)
                this._ProductOnlineCheck.Detail = this._detailManager.SelectByProductOnlineCheckId(this._ProductOnlineCheck.ProductOnlineCheckId);
            this.action = "view";
            this.flag = 1;
        }

        protected override bool HasRows()
        {
            return this._ProductOnlineCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._ProductOnlineCheckManager.HasRowsAfter(this._ProductOnlineCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this._ProductOnlineCheckManager.HasRowsBefore(this._ProductOnlineCheck);
        }

        protected override void MoveFirst()
        {
            this._ProductOnlineCheck = this._ProductOnlineCheckManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.flag == 1)
            {
                this.flag = 0;
                return;
            }
            this._ProductOnlineCheck = this._ProductOnlineCheckManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.ProductOnlineCheck model = this._ProductOnlineCheckManager.GetNext(this._ProductOnlineCheck);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._ProductOnlineCheck = model;
        }

        protected override void MovePrev()
        {
            Model.ProductOnlineCheck model = this._ProductOnlineCheckManager.GetPrev(this._ProductOnlineCheck);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._ProductOnlineCheck = model;
        }

        protected override void Delete()
        {
            if (this._ProductOnlineCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.ProductOnlineCheck model = this._ProductOnlineCheckManager.GetNext(this._ProductOnlineCheck);
                this._ProductOnlineCheckManager.Delete(this._ProductOnlineCheck.ProductOnlineCheckId);
                if (model == null)
                    this._ProductOnlineCheck = this._ProductOnlineCheckManager.GetLast();
                else
                    this._ProductOnlineCheck = model;
            }
        }

        protected override void AddNew()
        {
            this._ProductOnlineCheck = new Book.Model.ProductOnlineCheck();
            this._ProductOnlineCheck.ProductOnlineCheckId = this._ProductOnlineCheckManager.GetId();
            this._ProductOnlineCheckManager.TiGuiExists(this._ProductOnlineCheck);
            this._ProductOnlineCheck.ProductOnlineCheckDate = DateTime.Now;

            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._ProductOnlineCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._ProductOnlineCheck = this._ProductOnlineCheckManager.Get(this._ProductOnlineCheck.ProductOnlineCheckId);
            }
            this.txt_Id.EditValue = this._ProductOnlineCheck.ProductOnlineCheckId;
            this.date_ProductOnlineCheck.EditValue = this._ProductOnlineCheck.ProductOnlineCheckDate;
            this.date_Online.EditValue = this._ProductOnlineCheck.OnlineDate;
            this.newCCEmployee.EditValue = this._ProductOnlineCheck.Employee;
            this.txt_ProductName.EditValue = this._ProductOnlineCheck.Product == null ? null : this._ProductOnlineCheck.Product.ToString();
            this.txt_InvoiceXOId.EditValue = this._ProductOnlineCheck.InvoiceXOId;
            this.txt_PronoteHeaderId.EditValue = this._ProductOnlineCheck.PronoteHeaderId;
            this.richTextBoxRemark.Rtf = this._ProductOnlineCheck.Remark;

            this.newChooseContorlAuditEmp.EditValue = this._ProductOnlineCheck.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._ProductOnlineCheck.AuditState);

            this._ProductOnlineCheck.Detail = this._detailManager.SelectByProductOnlineCheckId(this._ProductOnlineCheck.ProductOnlineCheckId);
            this.bindingSource1.DataSource = this._ProductOnlineCheck.Detail;
            this.gridControl1.RefreshDataSource();

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
            this.txt_Id.Enabled = true;
            this.txt_Id.Properties.ReadOnly = true;
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (this.date_ProductOnlineCheck.EditValue != null)
                this._ProductOnlineCheck.ProductOnlineCheckDate = this.date_ProductOnlineCheck.DateTime;
            if (this.date_Online.EditValue != null)
                this._ProductOnlineCheck.OnlineDate = this.date_Online.DateTime;
            //this._ProductOnlineCheck.ProductId = (this.txt_ProductName.EditValue as Model.ProductOnlineCheck).ProductId;
            this._ProductOnlineCheck.EmployeeId = this.newCCEmployee.EditValue == null ? null : (this.newCCEmployee.EditValue as Model.Employee).EmployeeId;
            this._ProductOnlineCheck.InvoiceXOId = this.txt_InvoiceXOId.EditValue == null ? null : this.txt_InvoiceXOId.EditValue.ToString();
            this._ProductOnlineCheck.PronoteHeaderId = this.txt_PronoteHeaderId.EditValue == null ? null : this.txt_PronoteHeaderId.EditValue.ToString();
            this._ProductOnlineCheck.Remark = this.richTextBoxRemark.Rtf;

            //this._ProductOnlineCheck.Detail = (IList<Model.ProductOnlineCheckDetail>)this.bindingSource1.DataSource;

            switch (this.action)
            {
                case "insert":
                    this._ProductOnlineCheckManager.Insert(this._ProductOnlineCheck);
                    break;
                case "update":
                    this._ProductOnlineCheckManager.Update(this._ProductOnlineCheck);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._ProductOnlineCheck);
        }

        private void barInvoiceXO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Invoices.XS.SearcharInvoiceXSForm f = new Book.UI.Invoices.XS.SearcharInvoiceXSForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.key != null && f.key.Count > 0)
                {
                    this.txt_InvoiceXOId.EditValue = f.key[0].InvoiceId;
                    this.txt_PronoteHeaderId.EditValue = null;
                    if (f.key[0].Product != null)
                    {
                        this.txt_ProductName.EditValue = f.key[0].Product.ToString();
                        this._ProductOnlineCheck.ProductId = f.key[0].Product.ProductId;
                        this.richTextBoxRemark.Rtf = f.key[0].Product.ProductDescription;
                    }
                }
            }
        }

        private void barPronoteHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectItem != null)
                {
                    this.txt_PronoteHeaderId.EditValue = f.SelectItem.PronoteHeaderID;
                    this.txt_InvoiceXOId.EditValue = null;
                    this.txt_ProductName.EditValue = f.SelectItem.ProductName;
                    this._ProductOnlineCheck.ProductId = f.SelectItem.ProductId;
                    this.richTextBoxRemark.Rtf = f.SelectItem.Product == null ? null : f.SelectItem.Product.ProductDescription;
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Model.ProductOnlineCheckDetail model = new Book.Model.ProductOnlineCheckDetail();
            model.ProductOnlineCheckDetailId = Guid.NewGuid().ToString();
            model.ProductOnlineCheckId = this._ProductOnlineCheck.ProductOnlineCheckId;
            model.CheckDate = DateTime.Now;

            this.bindingSource1.Add(model);
            this.gridControl1.RefreshDataSource();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
                this.bindingSource1.Remove(this.bindingSource1.Current);
            this.gridControl1.RefreshDataSource();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List f = new List();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProductOnlineCheck model = f.SelectItem as Model.ProductOnlineCheck;
                if (model != null)
                {
                    this._ProductOnlineCheck = model;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.ProductOnlineCheck.PRO_ProductOnlineCheckId;
        }

        protected override int AuditState()
        {
            return this._ProductOnlineCheck.AuditState.HasValue ? this._ProductOnlineCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProductOnlineCheck" + "," + this._ProductOnlineCheck.ProductOnlineCheckId;
        }

        #endregion
    }
}