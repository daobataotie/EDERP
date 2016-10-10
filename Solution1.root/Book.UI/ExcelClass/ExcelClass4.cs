using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.ExcelClass
{
    class ExcelClass4
    {

         /// <summary>
        /// 读取指定单元格数据
        /// </summary>
        /// <param name="row">行序号</param>
        /// <param name="column">列序号</param>
        /// <returns>该格的数据</returns>
        public string ReadData(string fileName, string sheetName, int row, int column)
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
            Microsoft.Office.Interop.Excel.Range range = myExcel.get_Range(myExcel.Cells[row, column], myExcel.Cells[row, column]);
            string str = range.Text.ToString();
            myExcel.Quit();
            myWorkBook = null;
            myExcel = null;
            GC.Collect();
            return str;
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void UniteCells(Microsoft.Office.Interop.Excel.Worksheet ws, int x1, int y1, int x2, int y2)
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);
        }
        /// <summary>
        /// 将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置 为在使用模板时控制格式时使用一
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="ws">工作表名</param>
        /// <param name="startX">行</param>
        /// <param name="startY">列</param>
        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX + i, j + startY] = dt.Rows[i][j].ToString();
                }
            }
        }
        /// <summary>
        /// 将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置二
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="ws">工作表</param>
        /// <param name="startX">开始行</param>
        /// <param name="startY">开始列</param>
        public void InsertTable(System.Data.DataTable dt, Microsoft.Office.Interop.Excel.Worksheet ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    ws.Cells[startX + i, j + startY] = dt.Rows[i][j];
                }
            }
        }
        /// <summary>
        /// 将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置一
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="ws">工作表名</param>
        /// <param name="startX">开始行</param>
        /// <param name="startY">开始列</param>
        public void AddTable(System.Data.DataTable dt, string ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[i + startX, j + startY] = dt.Rows[i][j];
                }
            }
        }
        /// <summary>
        /// 将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表（包括字段名）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        public void AddTable(System.Data.DataTable dt, string ws)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                GetSheet(ws).Cells[1, i+1] = dt.Columns[i].ColumnName.ToString();
            }
            AddTable(dt, ws, 2, 1);
        }
        /// <summary>
        /// 将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置二
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void AddTable(System.Data.DataTable dt, Microsoft.Office.Interop.Excel.Worksheet ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    ws.Cells[i + startX, j + startY] = dt.Rows[i][j];
                }
            }
        }
        /// <summary>
        /// 设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        /// </summary>
        /// <param name="ws">工作表</param>
        /// <param name="Startx">开始行</param>
        /// <param name="Starty">开始列</param>
        /// <param name="Endx">结束行</param>
        /// <param name="Endy">结束列</param>
        /// <param name="size">字体大小</param>
        /// <param name="name">字体名</param>
        /// <param name="color">颜色</param>
        /// <param name="HorizontalAlignment">对齐方式</param>
        public void SetCellProperty(Microsoft.Office.Interop.Excel.Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name, Microsoft.Office.Interop.Excel.Constants color, Microsoft.Office.Interop.Excel.Constants HorizontalAlignment)
        {
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }
        /// <summary>
        /// 设置一个单元格的属性
        /// </summary>
        /// <param name="wsn">工作表名</param>
        /// <param name="Startx">开始行</param>
        /// <param name="Starty">开始列</param>
        /// <param name="Endx">结束行</param>
        /// <param name="Endy">结束列</param>
        /// <param name="size">字体大小</param>
        /// <param name="name">字体名</param>
        /// <param name="color">颜色</param>
        /// <param name="HorizontalAlignment">对齐方式</param>
        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name, Microsoft.Office.Interop.Excel.Constants color, Microsoft.Office.Interop.Excel.Constants HorizontalAlignment)
        {
            Microsoft.Office.Interop.Excel.Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }
        /// <summary>
        /// 设置一个工作表的数字全为文本
        /// </summary>
        /// <param name="wsn"></param>
        public void SetNumberFormat(string wsn)
        {
            Microsoft.Office.Interop.Excel.Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[1, 1], ws.Cells[65536, 256]).NumberFormatLocal = "@";
        }
        /// <summary>
        /// 保存Microsoft.Office.Interop.Excel
        /// </summary>
        /// <returns>保存成功返回True</returns>
        public bool Save()
        {
            if (mFilename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();//似乎失效，所以加一下一个语句
                    wb.SaveAs(mFilename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        /// <summary>
        /// Microsoft.Office.Interop.Excel文档另存为
        /// </summary>
        /// <param name="fileName">保存完整路径加文件名</param>
        /// <returns>保存成功返回True</returns>
        public bool SaveAs(object FileName)
        {
            try
            {
                wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// 去掉文件后缀
        /// </summary>
        /// <param name="FileName">文件名（含地址）</param>
        public void RemoveExefilter(string FileName)
        {
            string filename = FileName;
            string substr;
            if (System.IO.File.Exists(FileName))
            {
                System.IO.FileInfo myfile = new System.IO.FileInfo(FileName);
                substr = myfile.Extension;
                filename = filename.Substring(0, filename.Length - substr.Length);
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
                myfile.MoveTo(filename.ToString());
            }
        }
        /// <summary>
        /// 关闭一个Microsoft.Office.Interop.Excel对象，销毁对象
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            app.Quit();
            release_xlsObj();
            //调用window api查找Microsoft.Office.Interop.Excel进程,并用关闭
            IntPtr t = new IntPtr(app.Hwnd);
            int ProcessById;
            GetWindowThreadProcessId(t, out ProcessById);
            System.Diagnostics.Process ExcelProcess = System.Diagnostics.Process.GetProcessById(ProcessById);
            ExcelProcess.Kill();
            app = null;
            wbs = null;
            wb = null;
            wss = null;
            ws = null;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void release_xlsObj()
        {
            //if (app != null)
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            //app = null;
            if (wbs != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wbs);
            wbs = null;
            if (wb != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            wb = null;
            if (wss != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wss);
            wss = null;
            if (ws != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            ws = null;
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(Range);
            // Range=null;
            GC.Collect();
        }
        /// <summary>
        /// 关闭word,excel等进程
        /// </summary>
        public void KillProcess()
        {
            string processName = "EXCEL";
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程 
            try
            {
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(processName))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        
                        thisproc.Kill();
                    }
                }
            }
            catch (Exception e)
            { }
        }
    
    }
}
