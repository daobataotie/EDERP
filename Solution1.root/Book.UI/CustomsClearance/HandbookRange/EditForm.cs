using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.CustomsClearance.HandbookRange
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        BL.BGHandbookRangeManager _manager = new Book.BL.BGHandbookRangeManager();
        BL.BGHandbookRangeDetailManager _detailManager = new Book.BL.BGHandbookRangeDetailManager();
        Model.BGHandbookRange _BGHandbookRange;

        public EditForm()
        {
            InitializeComponent();

            this.newChooseContorlEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";
        }

        protected override bool HasRows()
        {
            return this._manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._manager.HasRowsAfter(this._BGHandbookRange);
        }

        protected override bool HasRowsPrev()
        {
            return this._manager.HasRowsBefore(this._BGHandbookRange);
        }

        protected override void MoveFirst()
        {
            this._BGHandbookRange = this._manager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._BGHandbookRange = this._manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.BGHandbookRange model = this._manager.GetNext(this._BGHandbookRange);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._BGHandbookRange = model;
        }

        protected override void MovePrev()
        {
            Model.BGHandbookRange model = this._manager.GetPrev(this._BGHandbookRange);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._BGHandbookRange = model;
        }

        protected override void AddNew()
        {
            this._BGHandbookRange = new Book.Model.BGHandbookRange();
            this._BGHandbookRange.BGHandbookRangeId = Guid.NewGuid().ToString();
            this._BGHandbookRange.BGHandbookRangeDate = DateTime.Now;
            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._BGHandbookRange == null)
                AddNew();
            else
            {
                if (this.action == "view")
                    this._BGHandbookRange = this._manager.Get(this._BGHandbookRange.BGHandbookRangeId);
            }
            this.txt_CompanyNameAndId.EditValue = this._BGHandbookRange.CompanyNameAndId;
            this.date_Invoice.EditValue = this._BGHandbookRange.BGHandbookRangeDate;
            this.newChooseContorlEmployee.EditValue = this._BGHandbookRange.Employee;
            this.txt_Tel.EditValue = this._BGHandbookRange.Tel;
            this.cobProductType.EditValue = this._BGHandbookRange.ProductType;
            this.richTextBoxOpinion.Rtf = this._BGHandbookRange.Opinion;
            this.newChooseContorlAuditEmp.EditValue = this._BGHandbookRange.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._BGHandbookRange.AuditState);

            this._BGHandbookRange.Details = this._detailManager.SelectByBGHandbookId(this._BGHandbookRange.BGHandbookRangeId);
            this.bindingSource1.DataSource = this._BGHandbookRange.Details;
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
        }

        protected override void Save()
        {
            this._BGHandbookRange.CompanyNameAndId = this.txt_CompanyNameAndId.Text;
            this._BGHandbookRange.BGHandbookRangeDate = this.date_Invoice.DateTime;
            this._BGHandbookRange.EmployeeId = this.newChooseContorlEmployee.EditValue == null ? null : (this.newChooseContorlEmployee.EditValue as Model.Employee).EmployeeId;
            this._BGHandbookRange.Tel = this.txt_Tel.Text;
            this._BGHandbookRange.ProductType = this.cobProductType.EditValue == null ? null : this.cobProductType.EditValue.ToString();
            this._BGHandbookRange.Opinion = this.richTextBoxOpinion.Rtf;

            if (!this.gridView1.UpdateCurrentRow() || !this.gridView1.PostEditor())
                return;
            switch (this.action)
            {
                case "insert":
                    this._manager.Insert(this._BGHandbookRange);
                    break;

                case "update":
                    this._manager.Update(this._BGHandbookRange);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._BGHandbookRange == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._manager.Delete(this._BGHandbookRange.BGHandbookRangeId);
            this._BGHandbookRange = this._manager.GetNext(this._BGHandbookRange);
            if (this._BGHandbookRange == null)
            {
                this._BGHandbookRange = this._manager.GetLast();
            }
        }

        private void barSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.BGHandbookRange model = f.SelectItem as Model.BGHandbookRange;
                if (model != null)
                {
                    this._BGHandbookRange = model;
                    this.Refresh();
                }
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.BGHandbookRange.PRO_BGHandbookRangeId;
        }

        protected override int AuditState()
        {
            return this._BGHandbookRange.AuditState.HasValue ? this._BGHandbookRange.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "BGHandbookRange" + "," + this._BGHandbookRange.BGHandbookRangeId;
        }

        #endregion


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Model.BGHandbookRangeDetail detail = new Book.Model.BGHandbookRangeDetail();
            detail.BGHandbookRangeDetailId = Guid.NewGuid().ToString();
            this.bindingSource1.Add(detail);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.bindingSource1.Remove(this.bindingSource1.Current);
            }
            this.gridControl1.RefreshDataSource();
        }

        private void cobProductType_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cobProductType.EditValue == "成品")
                this.gridColumn2.ColumnEditName = "成品名称";
            else
                this.gridColumn2.ColumnEditName = "料件名称";
        }

        #region
        private void barIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExcelClass.ExcelClass1 ec = new Book.UI.ExcelClass.ExcelClass1();
                ec.Open(openFileDialog1.FileName);

                try
                {
                    BL.V.BeginTransaction();
                    Model.BGHandbookRange bGHandbookRange = null;
                    this.action = "insert";

                    Model.BGHandbookRangeDetail detail;
                    for (int i = 1; i < ec.wb.Worksheets.Count; i++)
                    {
                        bGHandbookRange = new Book.Model.BGHandbookRange();
                        bGHandbookRange.Details = new List<Model.BGHandbookRangeDetail>();
                        bGHandbookRange.BGHandbookRangeId = Guid.NewGuid().ToString();
                        bGHandbookRange.BGHandbookRangeDate = DateTime.Now;
                        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)ec.wb.Worksheets[i];
                        bGHandbookRange.CompanyNameAndId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[3, 1]).Text.ToString().Substring(((Microsoft.Office.Interop.Excel.Range)ws.Cells[3, 1]).Text.ToString().IndexOf(':') + 1);

                        if (openFileDialog1.FileName.Contains("成品"))
                        {
                            this.cobProductType.EditValue = "成品";
                            if (ws.Name != "草稿")
                            {
                                for (int j = 6; j < 200; j++)
                                {
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() == "经办关员意见：" || (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() == "" && ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString() == ""))
                                    {
                                        break;
                                    }
                                    detail = new Book.Model.BGHandbookRangeDetail();
                                    detail.BGHandbookRangeDetailId = Guid.NewGuid().ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() != "")
                                        detail.Id = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString() != "")
                                        detail.ProductName = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 3]).Text.ToString() != "")
                                        detail.ProductSpecification = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 3]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 5]).Text.ToString() != "")
                                        detail.ProductUnit = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 5]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 6]).Text.ToString() != "")
                                        detail.CompanyProductId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 6]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 7]).Text.ToString() != "")
                                        detail.CustomProductId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 7]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 9]).Text.ToString() != "")
                                        detail.Note = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 9]).Text.ToString();
                                }
                            }
                        }

                        else if (openFileDialog1.FileName.Contains("料件"))
                        {
                            this.cobProductType.EditValue = "料件";
                        }


                        bGHandbookRange.Employee = (new BL.EmployeeManager()).GetByOperatorName(((Microsoft.Office.Interop.Excel.Range)ws.Cells[4, 1]).Text.ToString().Substring(((Microsoft.Office.Interop.Excel.Range)ws.Cells[4, 1]).Text.ToString().IndexOf(':') + 1, 4).Trim());
                        bGHandbookRange.EmployeeId = bGHandbookRange.Employee.EmployeeId;
                    }

                }
                catch
                {
                    BL.V.RollbackTransaction();
                    ec.Close();
                    throw;
                }
                ec.Close();
            }
        }

        private void barOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion
    }
}


