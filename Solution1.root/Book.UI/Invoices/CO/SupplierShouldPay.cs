using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.Invoices.CO
{
    public partial class SupplierShouldPay : DevExpress.XtraEditors.XtraForm
    {
        BL.InvoiceCGDetailManager manager = new Book.BL.InvoiceCGDetailManager();

        public SupplierShouldPay()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不完整！", this.Text, MessageBoxButtons.OK);
                return;
            }

            System.Data.DataTable dt = manager.SelectAllSupplierShouldPay(this.date_Start.DateTime, this.date_End.DateTime);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("無數據！");
                return;
            }

            Type objClassType = null;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            if (objClassType == null)
            {
                MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                return;
            }

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Workbooks.Add(true);

            Worksheet sheet = (Worksheet)excel.Worksheets[1];
            sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 4]).MergeCells = true;
            sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[2, 4]).MergeCells = true;
            sheet.get_Range(sheet.Cells[3, 1], sheet.Cells[3, 1]).ColumnWidth = 20;
            sheet.Cells.HorizontalAlignment = -4108;

            sheet.Cells[1, 1] = "應付賬款一覽表";
            sheet.Cells[2, 1] = string.Format("單據日期：{0} ~ {1}", this.date_Start.DateTime.ToString("yyyy-MM-dd"), this.date_End.DateTime.ToString("yyyy-MM-dd"));
            sheet.Cells[3, 1] = "廠商";
            sheet.Cells[3, 2] = "金額";
            sheet.Cells[3, 3] = "稅額";
            sheet.Cells[3, 4] = "總額";

            int row = 4;
            foreach (DataRow dr in dt.Rows)
            {
                sheet.Cells[row, 1] = dr["SupplierShortName"].ToString();
                sheet.Cells[row, 2] = dr["JinE"].ToString();
                sheet.Cells[row, 3] = dr["ShuiE"].ToString();
                sheet.Cells[row, 4] = dr["Total"].ToString();

                row++;
            }

            excel.Visible = true;
            excel.WindowState = XlWindowState.xlMaximized;
        }
    }
}