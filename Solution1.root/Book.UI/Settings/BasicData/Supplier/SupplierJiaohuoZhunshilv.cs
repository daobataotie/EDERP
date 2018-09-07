using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.Settings.BasicData.Supplier
{
    public partial class SupplierJiaohuoZhunshilv : DevExpress.XtraEditors.XtraForm
    {
        BL.SupplierManager supplierManager = new Book.BL.SupplierManager();
        BL.SupplierCategoryManager supplierCategoryManager = new Book.BL.SupplierCategoryManager();

        public SupplierJiaohuoZhunshilv()
        {
            InitializeComponent();

            this.cob_SupplierCategory.Properties.DataSource = supplierCategoryManager.Select();
            this.cob_SupplierCategory.Properties.DisplayMember = "SupplierCategoryName";
            this.cob_SupplierCategory.Properties.ValueMember = "Id";
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不完整", this.Text, MessageBoxButtons.OK);
                return;
            }
            if (this.spe_OverDays.EditValue == null)
            {
                MessageBox.Show("延遲天數不能為空", this.Text, MessageBoxButtons.OK);
                return;
            }
            string supplierCategoryIds = "";
            foreach (CheckedListBoxItem item in this.cob_SupplierCategory.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                    supplierCategoryIds += "'" + item.Value.ToString() + "',";
            }
            if (string.IsNullOrEmpty(supplierCategoryIds))
            {
                MessageBox.Show("廠商類別不能為空", this.Text, MessageBoxButtons.OK);
                return;
            }
            supplierCategoryIds = supplierCategoryIds.TrimEnd(',');

            List<Model.Supplier> supplierList = supplierManager.SelectNameAndCategoryByCategoryId(supplierCategoryIds).ToList();
            IList<Model.Supplier> sourceData = supplierManager.Zhunshilv(this.date_Start.DateTime, this.date_End.DateTime, supplierCategoryIds);

            var group = sourceData.GroupBy(S => S.InvoiceID1);          //根据采购单/加工单/委外合同号去重，每个单据只算一次
            List<Model.Supplier> list = new List<Book.Model.Supplier>();

            foreach (var item in group)
            {
                if (item.Count() > 1)
                {
                    Model.Supplier model = item.OrderBy(D => D.OverDay).First();
                    model.OverTime = (model.OverDay > (int)this.spe_OverDays.Value) ? 1 : 0;
                    list.Add(model);
                }
                else
                {
                    Model.Supplier model = item.First();
                    model.OverTime = (model.OverDay > (int)this.spe_OverDays.Value) ? 1 : 0;
                    list.Add(model);
                }
            }

            var sGroup = list.GroupBy(S => new { S.SupplierFullName, S.SupplierCategoryName }).Select(D => new { D.Key, TotalTime = D.Count(), OverTime = D.Sum(O => O.OverTime) });        //根据厂商汇总.ToList<Model.Supplier>()
            //List<Model.Supplier> sList = new List<Book.Model.Supplier>();
            foreach (var item in sGroup)
            {
                if (supplierList.Any(S => S.SupplierFullName == item.Key.SupplierFullName && S.SupplierCategoryName == item.Key.SupplierCategoryName))
                {
                    Model.Supplier model = supplierList.First(S => S.SupplierFullName == item.Key.SupplierFullName && S.SupplierCategoryName == item.Key.SupplierCategoryName);
                    model.TotalTime = item.TotalTime;
                    model.OverTime = item.OverTime;
                }
            }

            var scGroup = supplierList.GroupBy(S => S.SupplierCategoryName);         //根据厂商类别分类
            int row = 5;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Range r1 = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 7]);
            r1.MergeCells = true;//合并单元格
            Microsoft.Office.Interop.Excel.Range r2 = excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 7]);
            r2.MergeCells = true;
            Microsoft.Office.Interop.Excel.Range r3 = excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 2]);
            r3.MergeCells = true;

            excel.Cells.ColumnWidth = 12;
            excel.Cells.Font.Size = 12;
            excel.get_Range(excel.Cells[4, 1], excel.Cells[4, 1]).ColumnWidth = 5;
            excel.get_Range(excel.Cells[4, 2], excel.Cells[4, 2]).ColumnWidth = 30;
            excel.get_Range(excel.Cells[4, 4], excel.Cells[4, 4]).ColumnWidth = 15;
            excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
            excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;

            excel.Cells[1, 1] = "亦達光學股份有限公司";
            excel.Cells[2, 1] = "廠商交易排行";
            excel.Cells[3, 1] = "日期區間:" + this.date_Start.DateTime.ToString("yyyy-MM-dd") + " ~ " + this.date_End.DateTime.ToString("yyyy-MM-dd");
            excel.Cells[2, 1] = "廠商交易排行";

            int number = 1;
            foreach (var item in scGroup)
            {
                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 7]);
                r.MergeCells = true;
                excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 1]).Font.Size = 20;
                excel.Cells[row, 1] = item.Key;

                row++;

                excel.Cells[row, 1] = "序號";
                excel.Cells[row, 2] = "公司名稱";
                excel.Cells[row, 3] = "採購單數量";
                excel.Cells[row, 4] = "延遲交貨次數";
                excel.Cells[row, 5] = "百分比";
                excel.Cells[row, 6] = "退貨次數";
                excel.Cells[row, 7] = "異常單";

                row++;

                int i = 1;
                foreach (var s in item)
                {
                    excel.Cells[row, 1] = number;
                    excel.Cells[row, 2] = s.SupplierFullName;
                    excel.Cells[row, 3] = s.TotalTime;
                    excel.Cells[row, 4] = s.OverTime;
                    if (s.TotalTime != 0)
                        excel.Cells[row, 5] = (s.OverTime / s.TotalTime * 100).ToString() + "%";

                    i++;
                    row++;
                    number++;
                }

                excel.get_Range(excel.Cells[row - i, 1], excel.Cells[row - 1, 7]).Borders.LineStyle = 1;
                excel.get_Range(excel.Cells[row - i, 1], excel.Cells[row - 1, 7]).Borders.ColorIndex = 1;
                //excel.get_Range(excel.Cells[row - i, 1], excel.Cells[row - 1, 7]).BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, "#000000");

                row++;
            }
            excel.get_Range(excel.Cells[1, 1], excel.Cells[row, 7]).HorizontalAlignment = -4108;

            excel.Visible = true;//是否打开该Excel文件
            excel.WindowState = XlWindowState.xlMaximized;
        }

        private void SetExcelLittleTitle()
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}