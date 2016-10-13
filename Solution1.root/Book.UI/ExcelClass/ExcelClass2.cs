using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Book.UI.ExcelClass
{
    class ExcelClass2
    {
        /// <summary>
        /// 把EXCEl中的某工作表中字段值为FieldNameStr的行显示到datagridview中
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="FieldValueStr"></param>
        /// <param name="dataGridView1"></param>
        public void Excel2DBView_SelectFieldValue(string tablename, string FieldName, string FieldNameStr, DataGridView dataGridView1)
        {
            string sExcelFile = mFilename;
            try
            {
                string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExcelFile + ";Extended Properties=\"Microsoft.Office.Interop.Excel 8.0;HDR=Yes;IMEX=1\"";
                OleDbConnection connection = new OleDbConnection(sConnectionString);
                string sql_select_commands = "select * from [" + tablename + "$] where " + FieldName + "=" + FieldNameStr;
                //MessageBox.Show(sql_select_commands);
                OleDbDataAdapter adp = new OleDbDataAdapter(sql_select_commands, connection);
                DataSet ds = new DataSet();
                adp.Fill(ds, tablename);
                //设置DataGridView控件的字段标题
                DataSet ds2 = new DataSet();
                ds2 = GetNewDataSet(tablename);
                for (int j = 0; j < ds2.Tables[tablename].Columns.Count; j++)
                {
                    dataGridView1.Columns.Add(ds2.Tables[tablename].Rows[0][j].ToString(), ds2.Tables[tablename].Rows[0][j].ToString());
                }
                for (int i = 0; i < ds.Tables[tablename].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                }
                for (int i = 0; i < ds.Tables[tablename].Rows.Count; i++)
                    for (int j = 0; j < ds.Tables[tablename].Columns.Count; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = Convert.ToString(ds.Tables[tablename].Rows[i][j]);
                    }
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 返回excel中不把第一行当做标题看待的数据集
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataSet GetNewDataSet(string tablename)
        {
            string sExcelFile = mFilename;
            try
            {
                string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExcelFile + ";Extended Properties=\"Microsoft.Office.Interop.Excel 8.0;HDR=NO;IMEX=1\"";
                OleDbConnection connection = new OleDbConnection(sConnectionString);
                string sql_select_commands = "Select * from [" + tablename + "$]";
                OleDbDataAdapter adp = new OleDbDataAdapter(sql_select_commands, connection);
                DataSet ds = new DataSet();
                adp.Fill(ds, tablename);
                return ds;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); return null; }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //根据字段名，删除EXCEl中的某工作表中的记录(SQL语句未成功执行)
        /// <summary>
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="FieldValueStr"></param>
        /// <param name="dataGridView1"></param>
        public void DeleteExcelFieldValue(string tablename, string FieldName, string FieldNameStr)
        {
            string sExcelFile = mFilename;
            try
            {
                string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExcelFile + ";Extended Properties=\"Microsoft.Office.Interop.Excel 8.0;HDR=Yes;IMEX=1\"";
                OleDbConnection connection = new OleDbConnection(sConnectionString);
                string sql_select_commands = "delete from [" + tablename + "$] where " + FieldName + "=" + FieldNameStr;
                //string sql_select_commands = "delete from [" + tablename + "$]";
                //string sql_select_commands = "drop table [" + tablename + "$]";
                MessageBox.Show(sql_select_commands);
                OleDbDataAdapter adp = new OleDbDataAdapter(sql_select_commands, connection);
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 在工作表中插入行，并调整其他行以留出空间
        /// </summary>
        /// <param name="sheet">当前工作表</param>
        /// <param name="rowIndex">欲插入的行索引</param>
        public void InsertRows(Microsoft.Office.Interop.Excel.Worksheet sheet, int rowIndex)
        {
            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[rowIndex, Type.Missing];
            //object Range.Insert(object shift, object copyorigin);
            //shift: Variant类型，可选。指定单元格的调整方式。可以为下列 XlInsertShiftDirection 常量之一：
            //xlShiftToRight 或 xlShiftDown。如果省略该参数，Microsoft Microsoft.Office.Interop.Excel 将根据区域形状确定调整方式。
            range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
            range = null;
        }
        /// <summary>
        /// 在工作表中删除行
        /// </summary>
        /// <param name="sheet">当前工作表</param>
        /// <param name="rowIndex">欲删除的行索引</param>
        public void DeleteRows(Microsoft.Office.Interop.Excel.Worksheet sheet, int rowIndex)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[rowIndex, Type.Missing];
                range.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
                range = null;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }

        /// <summary>
        /// 在工作表中删除所有行名为rowText的行
        /// </summary>
        /// <param name="sheetName">当前工作表</param>
        /// <param name="FieldName">字段名</param>
        /// <param name="rowText">字段值</param>
        public void DeleteRows(string sheetName, string FieldName, string rowText)
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
            try
            {
                if (worksheet == null)
                {
                    throw new Exception("Worksheet   error ");
                    return;
                }
                //获取工作表行列数
                int rowCounts = rowcount(sheetName);
                int colCounts = colcount(sheetName);
                // MessageBox.Show(rowCounts.ToString() +"|" +colCounts.ToString());
                int colIndex = 0;
                //定位字段索引
                for (int i = 1; i <= colCounts; i++)
                {
                    if (GetCellStr(sheetName, 1, i) == FieldName)
                    {
                        colIndex = i;
                        break;
                    }
                }
                if (colIndex == 0)
                {
                    MessageBox.Show("找不到字段名：" + FieldName);
                    return;
                }
                //int jj = 0;
                //int jjj = 0;
                for (int i = 1; i <= rowCounts; i++)
                {
                    //jj++;
                    if (GetCellStr(sheetName, i, colIndex) == rowText)
                    {
                        DeleteRows(worksheet, i);
                        i--;
                        rowCounts = rowCounts - 1;
                        //jjj++;
                    }
                }
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }

        /// <summary>
        /// 根据字段名，追加记录
        /// </summary>
        /// <param name="sheetName">当前工作表</param>
        /// <param name="FieldName">字段名</param>
        /// <param name="recordText">字段值</param>
        public void AddRecords(string sheetName, string FieldName, string recordText)
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
            try
            {
                if (worksheet == null)
                {
                    throw new Exception("Worksheet   error ");
                    return;
                }
                //获取工作表行列数
                int rowCounts = rowcount(sheetName);
                int colCounts = colcount(sheetName);
                // MessageBox.Show(rowCounts.ToString() +"|" +colCounts.ToString());
                int colIndex = 0;
                //定位字段索引
                for (int i = 1; i <= colCounts; i++)
                {
                    if (GetCellStr(sheetName, 1, i) == FieldName)
                    {
                        colIndex = i;
                        break;
                    }
                }
                if (colIndex == 0)
                {
                    MessageBox.Show("找不到字段名：" + FieldName);
                    return;
                }
                SetCellStr(sheetName, rowCounts + 1, colIndex, recordText);
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 获取字段中的字段名称集
        /// </summary>
        /// <param name="sheetName">当前工作表</param>
        /// <param name="FieldName">字段名</param>
        /// <returns></returns>
        public StringCollection GetFieldValues(string sheetName, string FieldName)
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
            if (worksheet == null)
            {
                throw new Exception("Worksheet   error ");
                return null;
            }
            //获取工作表行列数
            int rowCounts = rowcount(sheetName);
            int colCounts = colcount(sheetName);
            // MessageBox.Show(rowCounts.ToString() +"|" +colCounts.ToString());
            int colIndex = 0;
            //定位字段索引
            for (int i = 1; i <= colCounts; i++)
            {
                if (GetCellStr(sheetName, 1, i) == FieldName)
                {
                    colIndex = i;
                    break;
                }
            }
            if (colIndex == 0)
            {
                MessageBox.Show("找不到字段名：" + FieldName);
                return null;
            }
            StringCollection fieldStrs = new StringCollection();
            string cellStr;
            for (int i = 2; i <= rowCounts; i++)
            {
                cellStr = GetCellStr(sheetName, i, colIndex);
                if (!fieldStrs.Contains(cellStr))
                {
                    fieldStrs.Add(cellStr);
                }
            }
            return fieldStrs;

        }
        /// <summary>
        /// DataGridView导出到Microsoft.Office.Interop.Excel 
        /// ExcelEdit myExcel = new ExcelEdit();
        /// myExcel.Create();
        /// myExcel.DGView2Excel(this.dataGridView1, "d:\\xiao.xls", "xiaobiao");
        /// myExcel.Save();
        /// myExcel.Close();
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="sheetName">当前工作表</param>
        public void DGView2Excel(DataGridView dgv, string sheetName)
        {
            try
            {
                int rowCount = dgv.RowCount;
                int columnCount = dgv.ColumnCount;
                //Microsoft.Office.Interop.Excel.Workbooks workbook=(Microsoft.Office.Interop.Excel.Workbook)app
                //Open(xlsFileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
                int sheet_rowCounts = rowcount(sheetName);
                int sheet_colCounts = colcount(sheetName);
                //int rowIndex = 0;
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
                    //worksheet.Cells[21, ColIndex++] = dHeader.HeaderText;
                }
                ColIndex = 1;
                //获取DataGridView中的所有行和列的数值,填充到一个二维数组中. 
                object[,] myData = new object[rowCount + 1, columnCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        myData[i, j] = dgv[j, i].Value;         //这里的获取注意行列次序 
                    }
                }
                //将填充好的二维数组填充到Microsoft.Office.Interop.Excel对象中. 
                r = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[rowCount + 1, columnCount]);
                r.Value2 = myData;
                r = null;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
    }
}
