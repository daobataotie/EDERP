using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Query;
using System.Linq;
using System.Reflection;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        BL.AcInvoiceXOBillDetailManager detailManager = new Book.BL.AcInvoiceXOBillDetailManager();

        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.AcInvoiceXOBillManager();

            this.gridView1.GroupPanelText = "默認顯示:" + System.DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + " 到 " + System.DateTime.Now.ToString("yyyy-MM-dd");
        }

        public ListForm(bool ExportExcel)
            : this()
        {
            if (ExportExcel)
            {
                this.gridColumn8.Visible = true;
                this.gridColumn8.VisibleIndex = 0;

                this.bar_ExportExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bar_CheckAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            this.gridView1.OptionsBehavior.Editable = true;
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.AcInvoiceXOBillManager).SelectByDateRange(System.DateTime.Now.AddMonths(-3), System.DateTime.Now);

            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionAChooseForm form = new ConditionAChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ConditionA cond = form.Condition as ConditionA;
                IList<Model.AcInvoiceXOBill> acInvoiceXOBill = (this.manager as BL.AcInvoiceXOBillManager).SelectByDateRange(cond.StartDate, cond.EndDate);
                if (acInvoiceXOBill != null)
                {
                    this.bindingSource1.DataSource = acInvoiceXOBill;
                }
                this.gridView1.GroupPanelText = "查詢日期週期是:" + System.DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + " 到 " + System.DateTime.Now.ToString("yyyy-MM-dd");
            }

            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        private Model.AcInvoiceXOBill _AcInvoiceXOBill;

        public Model.AcInvoiceXOBill AcInvoiceXOBill
        {
            get { return (this.bindingSource1.Current as Model.AcInvoiceXOBill); }
        }

        private void bar_ExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            IList<Model.AcInvoiceXOBill> list = this.bindingSource1.DataSource as IList<Model.AcInvoiceXOBill>;
            if (list != null)
            {
                var source = list.Where(L => L.Checked == true);
                if (source == null || source.Count() == 0)
                {
                    MessageBox.Show("請選擇要導出的數據", "提示", MessageBoxButtons.OK);
                    return;
                }

                List<Model.AcInvoiceXOBill> headers = new List<Model.AcInvoiceXOBill>();
                List<Model.AcInvoiceXOBillDetail> details = new List<Model.AcInvoiceXOBillDetail>();

                foreach (var item in source)
                {
                    headers.Add(item);

                    item.Details = detailManager.SelectByAcInvoiceXOBill(item);
                    if (item.Details != null)
                        foreach (var d in item.Details)
                        {
                            details.Add(d);
                        }
                }

                ExportExcel(headers, details);
            }
        }

        private void bar_CheckAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<Model.AcInvoiceXOBill> list = this.bindingSource1.DataSource as IList<Model.AcInvoiceXOBill>;
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.Checked = bar_CheckAll.Caption == "全選" ? true : false;
                }
            }

            this.gridControl1.RefreshDataSource();

            if (bar_CheckAll.Caption == "全選")
                bar_CheckAll.Caption = "取消全選";
            else
                bar_CheckAll.Caption = "全選";
        }

        //导出Excel
        private void ExportExcel(List<Book.Model.AcInvoiceXOBill> headers, List<Book.Model.AcInvoiceXOBillDetail> details)
        {
            Type objClassType = null;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            if (objClassType == null)
            {
                MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                return;
            }

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = ".xls";
                sfd.Filter = "Excel|*.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

                    #region Invoice

                    Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
                    sheet.Name = "Invoice";
                    sheet.Cells.ColumnWidth = 12;
                    sheet.get_Range(sheet.Cells[1, 11], sheet.Cells[1, 11]).ColumnWidth = 15;

                    sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[headers.Count + 1, 16]).EntireRow.AutoFit();
                    sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[headers.Count + 1, 16]).NumberFormat = "@";

                    #region SetFormat
                    sheet.Cells[1, 1] = "發票號碼";
                    sheet.Cells[1, 2] = "發票日期";
                    sheet.Cells[1, 3] = "發票類別";
                    sheet.Cells[1, 4] = "買方統一編號";
                    sheet.Cells[1, 5] = "課稅別";
                    sheet.Cells[1, 6] = "稅率";
                    sheet.Cells[1, 7] = "通關方式註記";
                    sheet.Cells[1, 8] = "原幣金額";
                    sheet.Cells[1, 9] = "匯率";
                    sheet.Cells[1, 10] = "幣別";
                    sheet.Cells[1, 11] = "營業人角色註記";
                    sheet.Cells[1, 12] = "彙開註記";
                    sheet.Cells[1, 13] = "銷售類別";
                    sheet.Cells[1, 14] = "備註";
                    sheet.Cells[1, 15] = "相關號碼";
                    sheet.Cells[1, 16] = "客戶買方編號";

                    #endregion

                    int invoiceRow = 2;
                    foreach (var item in headers)
                    {
                        sheet.Cells[invoiceRow, 1] = item.Id;
                        sheet.Cells[invoiceRow, 2] = item.AcInvoiceXOBillDate.HasValue ? item.AcInvoiceXOBillDate.Value.ToString("yyyy/MM/dd") : "";
                        sheet.Cells[invoiceRow, 3] = item.InvoiceType;
                        sheet.Cells[invoiceRow, 4] = item.CustomerShouPiao == null ? "" : item.CustomerShouPiao.CustomerNumber;
                        sheet.Cells[invoiceRow, 5] = item.TaxRateType.HasValue ? item.TaxRateType.Value.ToString() : "";
                        sheet.Cells[invoiceRow, 6] = item.TaxRate.HasValue ? item.TaxRate.Value.ToString() : "";
                        sheet.Cells[invoiceRow, 7] = item.ClearanceType;
                        sheet.Cells[invoiceRow, 8] = item.ZongMoney.HasValue ? item.ZongMoney.Value.ToString("f4") : "";
                        sheet.Cells[invoiceRow, 9] = item.ExchangeRate.ToString("f4");
                        sheet.Cells[invoiceRow, 10] = item.Currency;
                        sheet.Cells[invoiceRow, 11] = item.Employee == null ? "" : item.Employee.EmployeeName;
                        sheet.Cells[invoiceRow, 12] = item.HuikaiNote == true ? "*" : "";
                        sheet.Cells[invoiceRow, 13] = item.SalesType;
                        sheet.Cells[invoiceRow, 14] = item.AcInvoiceXOBillDesc;
                        sheet.Cells[invoiceRow, 15] = item.RelatedNumbers;
                        sheet.Cells[invoiceRow, 16] = item.CustomerShouPiao == null ? "" : item.CustomerShouPiao.Id;

                        invoiceRow++;
                    }

                    #endregion

                    #region Invoice_Details

                    excel.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                    Microsoft.Office.Interop.Excel.Worksheet sheet2 = ((Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[excel.Worksheets.Count]);
                    sheet2.Name = "Invoice_Details";

                    sheet2.Cells.ColumnWidth = 12;
                    sheet2.get_Range(sheet2.Cells[1, 3], sheet2.Cells[1, 3]).ColumnWidth = 25;
                    sheet2.get_Range(sheet2.Cells[1, 1], sheet2.Cells[headers.Count + 1, 11]).EntireRow.AutoFit();

                    sheet2.get_Range(sheet2.Cells[1, 1], sheet2.Cells[headers.Count + 1, 11]).NumberFormatLocal = "@";


                    #region SetFormat
                    sheet2.Cells[1, 1] = "發票號碼";
                    sheet2.Cells[1, 2] = "品名編號";
                    sheet2.Cells[1, 3] = "發票品名";
                    sheet2.Cells[1, 4] = "相關號碼";
                    sheet2.Cells[1, 5] = "單價";
                    sheet2.Cells[1, 6] = "單位";
                    sheet2.Cells[1, 7] = "數量";
                    sheet2.Cells[1, 8] = "單價2";
                    sheet2.Cells[1, 9] = "單位2";
                    sheet2.Cells[1, 10] = "數量2";
                    sheet2.Cells[1, 11] = "單一欄位備註2";

                    #endregion

                    int detailRow = 2;
                    foreach (var item in details)
                    {
                        sheet2.Cells[detailRow, 1] = item.AcInvoiceXOBill.Id;
                        sheet2.Cells[detailRow, 2] = item.Product.Id;
                        sheet2.Cells[detailRow, 3] = item.Product.ProductName;
                        sheet2.Cells[detailRow, 4] = item.AcInvoiceXOBill.RelatedNumbers;
                        sheet2.Cells[detailRow, 5] = item.InvoiceXODetailPrice.HasValue ? item.InvoiceXODetailPrice.Value.ToString("f4") : "";
                        sheet2.Cells[detailRow, 6] = item.InvoiceProductUnit;
                        sheet2.Cells[detailRow, 7] = item.InvoiceXODetaiInQuantity.HasValue ? item.InvoiceXODetaiInQuantity.Value.ToString("f4") : "";
                        sheet2.Cells[detailRow, 8] = item.Price2.HasValue ? item.Price2.Value.ToString("f4") : "";
                        sheet2.Cells[detailRow, 9] = item.ProductUnit2;
                        sheet2.Cells[detailRow, 10] = item.Quantity2.HasValue ? item.Quantity2.Value.ToString("f4") : "";
                        sheet2.Cells[detailRow, 11] = item.InvoiceCOIdNote;

                        detailRow++;
                    }

                    #endregion

                    excel.Visible = false;
                    workbook.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    excel.Quit();

                    MessageBox.Show("導出成功！", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
