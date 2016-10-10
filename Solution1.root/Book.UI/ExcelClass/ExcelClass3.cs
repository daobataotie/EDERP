using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Book.UI.ExcelClass
{
    class ExcelClass3
    {
        /// <summary>
        /// DataGridView追加到Microsoft.Office.Interop.Excel指定表格中 
        /// ExcelEdit myExcel = new ExcelEdit();
        /// myExcel.Create();
        /// myExcel.DGViewAdd2Excel(this.dataGridView1, "d:\\xiao.xls", "xiaobiao");
        /// myExcel.Save();
        /// myExcel.Close();
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sheetName"></param>
        public void DGViewAdd2Excel(DataGridView dgv, string sheetName)
        {
            try
            {
                //DataGridView控件行列数
                int rowCount = dgv.RowCount;
                int columnCount = dgv.ColumnCount;
                //工作表行列数
                int sheet_row_count = rowcount(sheetName);
                int sheet_column_count = colcount(sheetName);
                //        MessageBox.Show(sheet_row_count.ToString() + " | "+sheet_column_count.ToString());
                //Microsoft.Office.Interop.Excel.Workbooks workbook=(Microsoft.Office.Interop.Excel.Workbook)app
                //Open(xlsFileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
                if (worksheet == null)
                {
                    throw new Exception("Worksheet   error ");
                }
                // 
                Microsoft.Office.Interop.Excel.Range r = worksheet.get_Range("A1 ", Missing.Value);
                if (r == null)
                {
                    MessageBox.Show("Range无法启动 ");
                    throw new Exception("Range   error ");
                }
                //以上是一些例行的初始化工作,下面进行具体的信息填充 
                //填充标题 
                int ColIndex = 1;
                foreach (DataGridViewColumn dHeader in dgv.Columns)
                {
                    worksheet.Cells[1, ColIndex++] = dHeader.HeaderText;
                }
                ColIndex = 1;
                //获取DataGridView中的所有行和列的数值,填充到一个二维数组中. 
                object[,] myData = new object[rowCount + 1, columnCount];
                for (int i = 1; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        myData[i - 1, j] = dgv[j, i - 1].Value;         //这里的获取注意行列次序 
                    }
                }
                //将填充好的二维数组填充到Microsoft.Office.Interop.Excel对象中. 
                //r = worksheet.get_Range(worksheet.Cells[sheet_row_count + 1, 1], worksheet.Cells[sheet_row_count + rowCount, columnCount]);
                r = worksheet.get_Range(worksheet.Cells[sheet_row_count + 1, 1], worksheet.Cells[sheet_row_count + rowCount, columnCount]);
                r.Value2 = myData;
                r = null;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }

        //_________________________________________________________________________________
        /// <summary>
        /// 返回工作表名
        /// </summary>
        /// <param name="_filename">文件名（含地址）</param>
        /// <returns>工作表名集</returns>
        public StringCollection countexcel(string _filename) //
        {
            if (System.IO.File.Exists(_filename))
            {
                Microsoft.Office.Interop.Excel.ApplicationClass myExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook excel_wb;
                excel_wb = myExcelApp.Workbooks.Open(_filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                StringCollection a = new StringCollection();
                for (int i = 1; i <= excel_wb.Worksheets.Count; i++)
                {
                    a.Add(((Microsoft.Office.Interop.Excel.Worksheet)excel_wb.Worksheets[i]).Name);
                }
                myExcelApp.Workbooks.Close();
                myExcelApp.Quit();
                myExcelApp = null;
                return a;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 用datset返回整个excel
        /// </summary>
        /// <param name="_filename">文件名（含地址）</param>
        /// <returns></returns>
        public DataSet proces(string _filename) //
        {
            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;";
            strConn += "data source=" + _filename + ";";
            strConn += "Extended Properties=Microsoft.Office.Interop.Excel 8.0;";
            strConn += "HDR=Yes;IMEX=1";
            OleDbConnection objConn = new OleDbConnection(strConn);
            DataSet ds = new DataSet();
            OleDbDataAdapter oldda = new OleDbDataAdapter();
            foreach (string sheetname in countexcel(_filename))
            {
                string a = "select * from ";
                a += sheetname;
                oldda.SelectCommand.CommandText = a;
                oldda.Fill(ds, sheetname);
            }
            return ds;
        }
        /// <summary>
        /// 行数
        /// </summary>
        /// <param name="_filename">文件名（含地址）</param>
        /// <param name="sheetname">工作表</param>
        /// <returns></returns>
        public int rowcount(string _filename, string sheetname) //行数
        {
            Microsoft.Office.Interop.Excel.ApplicationClass myExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook excel_wb;
            excel_wb = myExcelApp.Workbooks.Open(_filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            int counts = ((Microsoft.Office.Interop.Excel.Worksheet)excel_wb.Worksheets.get_Item(sheetname)).UsedRange.Rows.Count;
            myExcelApp.Workbooks.Close();
            myExcelApp.Quit();
            myExcelApp = null;
            return counts;
        }
        /// <summary>
        /// 当前打开的表的行数
        /// </summary>
        /// <param name="sheetname">当前工作表</param>
        /// <returns></returns>
        public int rowcount(string sheetname) //行数
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetname);
            if (worksheet == null)
            {
                throw new Exception("Worksheet   error ");
            }
            int counts = worksheet.UsedRange.Rows.Count;
            worksheet = null;
            return counts;
        }
        /// <summary>
        /// 列数
        /// </summary>
        /// <param name="_filename">文件名（含地址）</param>
        /// <param name="sheetname">工作表名</param>
        /// <returns></returns>
        public int colcount(string _filename, string sheetname) //列数
        {
            Microsoft.Office.Interop.Excel.ApplicationClass myExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook excel_wb;
            excel_wb = myExcelApp.Workbooks.Open(_filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            int counts = ((Microsoft.Office.Interop.Excel.Worksheet)excel_wb.Worksheets.get_Item(sheetname)).UsedRange.Columns.Count;
            myExcelApp.Workbooks.Close();
            myExcelApp.Quit();
            myExcelApp = null;
            return counts;
        }
        /// <summary>
        /// 当前工作表列数
        /// </summary>
        /// <param name="sheetname">当前工作表</param>
        /// <returns></returns>
        public int colcount(string sheetname) //列数
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetname);
            if (worksheet == null)
            {
                throw new Exception("Worksheet   error ");
            }
            int counts = worksheet.UsedRange.Columns.Count;
            worksheet = null;
            return counts;
        }
        /// <summary>
        /// 返回指定单元格的文本
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <returns></returns>
        public string GetCellStr(string sheetName, int row, int col) //返回指定单元格的文本
        {
            string str = "";
            try
            {
                //Open(xlsFileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
                Microsoft.Office.Interop.Excel.Range r = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[row, col]);
                str = r.Text.ToString();
                r = null;
                worksheet = null;
                //Close();
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
            return str;
            //Microsoft.Office.Interop.Excel.Range rng3 = xSheet.get_Range("C6", System.Reflection.Missing.Value);     
            //rng3.Cells.FormulaR1C1   =   txtCellText.Text;         
            // rng3.Interior.ColorIndex = 6;   //设置Range的背景色 
        }
        /// <summary>
        /// 将字符串写入指定单元格
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="writeStr">值</param>
        public void SetCellStr(string sheetName, int row, int col, string writeStr)
        {
            try
            {
                //Open(xlsFileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
                Microsoft.Office.Interop.Excel.Range r = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[row, col]);
                //r.Text.FormulaR1C1;
                r.Value2 = writeStr;
                worksheet = null;
                r = null;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
            //Save();
            // Close();
            //Microsoft.Office.Interop.Excel.Range rng3 = xSheet.get_Range("C6", System.Reflection.Missing.Value);     
            //rng3.Cells.FormulaR1C1   =   txtCellText.Text;         
            // rng3.Interior.ColorIndex = 6;   //设置Range的背景色 
        }

        //_______________________________________添加自定义函数_________________________________________________________________
        /// <summary>
        /// 将数据写入Microsoft.Office.Interop.Excel
        /// </summary>
        /// <param name="data">要写入的二维数组数据</param>
        /// <param name="startRow">Microsoft.Office.Interop.Excel中的起始行</param>
        /// <param name="startColumn">Microsoft.Office.Interop.Excel中的起始列</param>
        public void WriteData(string[,] data, string fileName, string sheetName, int startRow, int startColumn)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application myExcel;
                Microsoft.Office.Interop.Excel.Workbook myWorkBook;
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                //myWorkBook = myExcel.Application.Workbooks.Add(true);
                myWorkBook = myExcel.Workbooks.Add(fileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                worksheet.Name = sheetName;
                //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcel.Worksheets[sheetName];
                worksheet.Activate();
                int rowNumber = data.GetLength(0);
                int columnNumber = data.GetLength(1);
                for (int i = 0; i < rowNumber; i++)
                {
                    for (int j = 0; j < columnNumber; j++)
                    {
                        //在Microsoft.Office.Interop.Excel中，如果某单元格以单引号“'”开头，表示该单元格为纯文本，因此，我们在每个单元格前面加单引号。 
                        myExcel.Cells[startRow + i, startColumn + j] = "'" + data[i, j];
                    }
                }
                myExcel.Quit();
                myWorkBook = null;
                myExcel = null;
                GC.Collect();
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 将数据写入Microsoft.Office.Interop.Excel
        /// </summary>
        /// <param name="data">要写入的字符串</param>
        /// <param name="starRow">写入的行</param>
        /// <param name="startColumn">写入的列</param>
        public void WriteData(string data, string fileName, string sheetName, int row, int column)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application myExcel;
                Microsoft.Office.Interop.Excel.Workbook myWorkBook;
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                //myWorkBook = myExcel.Application.Workbooks.Add(true);
                myWorkBook = myExcel.Workbooks.Add(fileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                worksheet.Name = sheetName;
                //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcel.Worksheets[sheetName];
                worksheet.Activate();
                myExcel.Cells[row, column] = data;
                myExcel.Quit();
                myWorkBook = null;
                myExcel = null;
                GC.Collect();
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
            //Microsoft.Office.Interop.Excel.Range rng3 = xSheet.get_Range("C6", System.Reflection.Missing.Value);     
            //rng3.Cells.FormulaR1C1   =   txtCellText.Text;         
            // rng3.Interior.ColorIndex = 6;   //设置Range的背景色   
        }
    }
}
