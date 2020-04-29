using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Customs;
using System.Linq;

namespace Book.UI.Query
{
    public partial class CustomerShipment : DevExpress.XtraEditors.XtraForm
    {
        List<Model.Customer> Customers = new List<Book.Model.Customer>();
        BL.InvoiceXODetailManager invoiceXODetailManager = new Book.BL.InvoiceXODetailManager();

        private string CustomerNames
        {
            get
            {
                if (Customers == null || Customers.Count == 0)
                    return null;
                else
                {
                    string names = "";
                    Customers.ForEach(C =>
                    {
                        names += C.CustomerShortName + ",";
                    });
                    names = names.TrimEnd(',');

                    return names;
                }
            }
        }

        private string CustomerIds
        {
            get
            {
                if (Customers == null || Customers.Count == 0)
                    return null;
                else
                {
                    string ids = "";
                    Customers.ForEach(C =>
                    {
                        ids += "'" + C.CustomerId + "',";
                    });
                    ids = ids.TrimEnd(',');

                    return ids;
                }
            }
        }

        public CustomerShipment()
        {
            InitializeComponent();
        }

        private void btn_Customer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseCustomsForm2 f = new ChooseCustomsForm2(Customers);
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.Customers = f.Customers.Where(C => C.IsChecked == true).ToList();

                this.btn_Customer.Text = this.CustomerNames;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerIds))
            {
                MessageBox.Show("客戶不能為空", "提示", MessageBoxButtons.OK);
                return;
            }
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不能為空", "提示", MessageBoxButtons.OK);
                return;
            }


            DataTable dt = invoiceXODetailManager.ShipmentTable(CustomerIds, date_Start.DateTime, date_End.DateTime);


            //導出Excel
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("無數據", "提示", MessageBoxButtons.OK);
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
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
                sheet.Cells.ColumnWidth = 15;
                sheet.Cells.RowHeight = 20;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 3]).ColumnWidth = 20;
                excel.get_Range(excel.Cells[1, 8], excel.Cells[1, 11]).ColumnWidth = 9;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1 + dt.Rows.Count, 13]).Borders.LineStyle = 1;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1 + dt.Rows.Count, 13]).HorizontalAlignment = -4108;

                excel.Cells[1, 1] = "出货客戶名稱";
                excel.Cells[1, 2] = "訂單號碼";
                excel.Cells[1, 3] = "產品型號";
                excel.Cells[1, 4] = "數量";
                //excel.Cells[1, 5] = "單位PCS/PRS";
                excel.Cells[1, 5] = "單位";
                excel.Cells[1, 6] = "訂單交貨日";
                excel.Cells[1, 7] = "盒裝/箱裝";
                excel.Cells[1, 8] = "淨重KG";
                excel.Cells[1, 9] = "毛重KG";
                excel.Cells[1, 10] = "才積";
                excel.Cells[1, 11] = "箱數";
                excel.Cells[1, 12] = "外箱編號";
                excel.Cells[1, 13] = "生產站";


                int row = 2;
                double startNumber = 0;

                excel.get_Range(excel.Cells[2, 2], excel.Cells[1 + dt.Rows.Count, 2]).NumberFormatLocal = "@";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0 && dt.Rows[i]["CustomerInvoiceXOId"].ToString() != dt.Rows[i - 1]["CustomerInvoiceXOId"].ToString())   //新的訂單編號，箱號重新開始排
                        startNumber = 0;

                    double boxCount = Math.Ceiling(Convert.ToDouble(dt.Rows[i]["BoxCount"]));

                    dt.Rows[i]["BoxNumber"] = string.Format("{0} ~ {1}", startNumber + 1, startNumber + boxCount);

                    startNumber += boxCount;

                    excel.Cells[row, 1] = dt.Rows[i]["CustomerFullName"] == null ? "" : dt.Rows[i]["CustomerFullName"].ToString();
                    excel.Cells[row, 2] = dt.Rows[i]["CustomerInvoiceXOId"] == null ? "" : dt.Rows[i]["CustomerInvoiceXOId"].ToString();
                    excel.Cells[row, 3] = dt.Rows[i]["CustomerProductName"] == null ? "" : dt.Rows[i]["CustomerProductName"].ToString();
                    excel.Cells[row, 4] = dt.Rows[i]["InvoiceXODetailQuantity"] == null ? "" : dt.Rows[i]["InvoiceXODetailQuantity"].ToString();
                    excel.Cells[row, 5] = dt.Rows[i]["SellUnit"] == null ? "" : dt.Rows[i]["SellUnit"].ToString();
                    excel.Cells[row, 6] = dt.Rows[i]["InvoiceYjrq"] == null ? "" : dt.Rows[i]["InvoiceYjrq"].ToString();
                    excel.Cells[row, 7] = dt.Rows[i]["Guige"] == null ? "" : dt.Rows[i]["Guige"].ToString();
                    excel.Cells[row, 8] = dt.Rows[i]["NetWeight"] == null ? "" : dt.Rows[i]["NetWeight"].ToString();
                    excel.Cells[row, 9] = dt.Rows[i]["GrossWeight"] == null ? "" : dt.Rows[i]["GrossWeight"].ToString();
                    excel.Cells[row, 10] = dt.Rows[i]["Volume"] == null ? "" : dt.Rows[i]["Volume"].ToString();
                    excel.Cells[row, 11] = dt.Rows[i]["BoxCount"] == null ? "" : dt.Rows[i]["BoxCount"].ToString();
                    excel.Cells[row, 12] = dt.Rows[i]["BoxNumber"] == null ? "" : dt.Rows[i]["BoxNumber"].ToString();
                    excel.Cells[row, 13] = dt.Rows[i]["Workhousename"] == null ? "" : dt.Rows[i]["Workhousename"].ToString();

                    row++;
                }

                excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
                excel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}