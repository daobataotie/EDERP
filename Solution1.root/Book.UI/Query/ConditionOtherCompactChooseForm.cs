using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;
using Book.UI.produceManager.ProduceOtherCompact;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.Query
{


    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 客戶設置
   // 文 件 名：ConditionOtherCompactChooseForm
   // 编 码 人: 刘永亮                   完成时间:2011-1-01-28
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ConditionOtherCompactChooseForm : ConditionAChooseForm
    {
        private ConditionOtherCompact condition;
        public ConditionOtherCompactChooseForm()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = System.DateTime.Now.Date.AddMonths(-1);
            this.dateEditEndDate.DateTime = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            #region 廠商初始值
            Book.UI.Settings.BasicData.Supplier.ChooseSupplier chooseSupplier = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseSupplier1.Choose = chooseSupplier;
            this.newChooseSupplier2.Choose = chooseSupplier;
            #endregion
        }
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionOtherCompact;
            }
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionOtherCompact();

            this.condition.StartDate = this.dateEditStartDate.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEditEndDate.DateTime;
            this.condition.SupplierId1 = this.newChooseSupplier1.EditValue == null ? null : (this.newChooseSupplier1.EditValue as Model.Supplier).Id;
            this.condition.SupplierId2 = this.newChooseSupplier2.EditValue == null ? null : (this.newChooseSupplier2.EditValue as Model.Supplier).Id;
            this.condition.SupplierName1 = this.newChooseSupplier1.EditValue == null ? null : (this.newChooseSupplier1.EditValue as Model.Supplier).SupplierFullName;
            this.condition.SupplierName2 = this.newChooseSupplier2.EditValue == null ? null : (this.newChooseSupplier2.EditValue as Model.Supplier).SupplierFullName;

            this.condition.ProduceOtherCompactId1 = this.buttonEditProduceOtherCompactId1.EditValue == null ? null : this.buttonEditProduceOtherCompactId1.EditValue.ToString();
            this.condition.ProduceOtherCompactId2 = this.buttonEditProduceOtherCompactId2.EditValue == null ? null : this.buttonEditProduceOtherCompactId2.EditValue.ToString();
            this.condition.ProductId1 = (this.buttonEditProduct1.EditValue as Model.Product) == null ? null : (this.buttonEditProduct1.EditValue as Model.Product).Id;
            this.condition.ProductId2 = (this.buttonEditProduct2.EditValue as Model.Product) == null ? null : (this.buttonEditProduct2.EditValue as Model.Product).Id;
            this.condition.InvoiceCusId = this.txt_InvoiceCusId.Text;
        }

        private void buttonEditProduct1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduct1.EditValue = form.SelectedItem;
                this.buttonEditProduct2.EditValue = form.SelectedItem;
            }
        }

        private void buttonEditProduct2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduct2.EditValue = form.SelectedItem;
        }

        private void buttonEditProduceOtherCompactId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseOutContract form = new ChooseOutContract();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduceOtherCompactId1.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
                this.buttonEditProduceOtherCompactId2.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
            }
        }

        private void buttonEditProduceOtherCompactId2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseOutContract form = new ChooseOutContract();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduceOtherCompactId2.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Type objClassType = null;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            if (objClassType == null)
            {
                MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                return;
            }

            DateTime startTime = global::Helper.DateTimeParse.NullDate;
            DateTime endTime = global::Helper.DateTimeParse.EndDate;
            if (this.dateEditStartDate.EditValue != null)
            {
                startTime = this.dateEditStartDate.DateTime;
            }
            if (this.dateEditEndDate.EditValue != null)
            {
                endTime = this.dateEditEndDate.DateTime;
            }

            System.Data.DataTable dt = new BL.ProduceOtherCompactManager().GetExcelData(startTime, endTime);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("無數據！", "提示！", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);
                excel.Cells.ColumnWidth = 12;
                excel.Rows.RowHeight = 20;

                #region 表頭
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1 + dt.Rows.Count, 7]).Borders.LineStyle = XlLineStyle.xlContinuous;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1 + dt.Rows.Count, 7]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).ColumnWidth = 18;
                excel.get_Range(excel.Cells[1, 3], excel.Cells[1, 3]).ColumnWidth = 18;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 10]).Font.Size = 12;
                excel.get_Range(excel.Cells[1, 4], excel.Cells[1, 4]).ColumnWidth = 50;
                excel.Cells[1, 1] = "編號";
                excel.Cells[1, 2] = "最慢交期";
                excel.Cells[1, 3] = "客戶訂單號";
                excel.Cells[1, 4] = "貨品名稱";
                excel.Cells[1, 5] = "數量";
                excel.Cells[1, 6] = "進貨數量";
                excel.Cells[1, 7] = "單位";
                #endregion

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    excel.Cells[i + 2, 1] = dt.Rows[i]["ProduceOtherCompactId"] == null ? "" : dt.Rows[i]["ProduceOtherCompactId"].ToString();
                    excel.Cells[i + 2, 2] = dt.Rows[i]["LastDate"] == null ? "" : dt.Rows[i]["LastDate"].ToString();
                    excel.Cells[i + 2, 3] = dt.Rows[i]["CustomerInvoiceXOId"] == null ? "" : dt.Rows[i]["CustomerInvoiceXOId"].ToString();
                    excel.Cells[i + 2, 4] = dt.Rows[i]["ProductName"] == null ? "" : dt.Rows[i]["ProductName"].ToString();
                    excel.Cells[i + 2, 5] = dt.Rows[i]["OtherCompactCount"] == null ? "" : dt.Rows[i]["OtherCompactCount"].ToString();
                    excel.Cells[i + 2, 6] = dt.Rows[i]["ArrivalInQuantity"] == null ? "" : dt.Rows[i]["ArrivalInQuantity"].ToString();
                    excel.Cells[i + 2, 7] = dt.Rows[i]["ProductUnit"] == null ? "" : dt.Rows[i]["ProductUnit"].ToString();
                }
                excel.Visible = true;
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }
    }
}