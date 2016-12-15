using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.Hr.Attendance.AnnualHoliday
{
    //功能描述：  年假维护    
    //开发人员：   徐彦飞
    //创建时间：  2010-2-3
    //修改时间：

    public partial class AnnualHolidayForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.AnnualHolidayManager _holidayManager = new Book.BL.AnnualHolidayManager();
        private BL.HrSpecificHolidayManager _specificHoliday = new Book.BL.HrSpecificHolidayManager();
        private BL.DepartmentManager _DepartmentManager = new Book.BL.DepartmentManager();
        private DataSet data;
        private DataSet specificData;
        private DataSet Tempdata;
        private DataSet TempspecificData;
        private string _mALLDepart;

        public AnnualHolidayForm()
        {
            InitializeComponent();
        }

        //加载休假的数据
        private void AnnualHolidayForm_Load(object sender, EventArgs e)
        {
            dataGridViewAnnualHoliday.Columns[1].ReadOnly = true;
            for (int i = 0; i < 2; i++)
            {
                this.comboBoxEdit1.Properties.Items.Add(DateTime.Now.AddYears(i).Year);
            }
            this.comboBoxEdit1.SelectedIndex = 0;

            //获取所有部门
            IList<Model.Department> delist = this._DepartmentManager.Select();
            IList<string> sall = (from Model.Department de in delist
                                  select de.DepartmentId).ToList<string>();
            if (sall != null && sall.Count != 0)
                this._mALLDepart = sall.Aggregate((s1, s2) => string.Format("{0},{1}", s1, s2));
            else
                this._mALLDepart = string.Empty;


            if (this._holidayManager.ExistsAutoYear(this.comboBoxEdit1.Text))
                this.btnAutoArrangeHoliday.Enabled = false;

            InitAnnualHolidayInfo();
            //InitSpecificHolidayInfo();

            this.dataGridViewAnnualHoliday.AutoGenerateColumns = false;
        }

        private void InitAnnualHolidayInfo()
        {
            this.data = this._holidayManager.SelectAnnualInfoByYear(Int32.Parse(this.comboBoxEdit1.Text));
            this.Tempdata = this.data.Copy();
            this.bindingSourceAnnualHoliday.DataSource = data.Tables[0];

        }

        //private void InitSpecificHolidayInfo()
        //{
        //    this.specificData = this._specificHoliday.SelectSpecificHolidayInfo();
        //    this.TempspecificData = this.specificData.Copy();
        //    this.bindingSourceSpecificHoliday.DataSource = this.specificData.Tables[0];
        //}

        //对用户的操作进行保存

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //保存所有假日
            try
            {
                BL.V.BeginTransaction();
                comboBoxEdit1.Focus();

                this.dataGridViewAnnualHoliday.EndEdit();
                if (data.HasChanges())
                {
                    this._holidayManager.SaveAnnualInfo(data.Tables[0], this.comboBoxEdit1.Text);
                    InitAnnualHolidayInfo();
                }
                BL.V.CommitTransaction();
                MessageBox.Show("保存成功！", this.Text, MessageBoxButtons.OK);
            }

            catch
            {
                MessageBox.Show("數據庫發生錯誤,請聯繫管理員", this.Text, MessageBoxButtons.OK);
                BL.V.RollbackTransaction();
            }
        }

        //单元格中的值发生变化时
        private void dataGridViewAnnualHoliday_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        DateTime date = Convert.ToDateTime(dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[0].Value);
                        switch (date.DayOfWeek)
                        {
                            case System.DayOfWeek.Monday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期一";
                                break;
                            case System.DayOfWeek.Tuesday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期二";
                                break;
                            case System.DayOfWeek.Wednesday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期三";
                                break;
                            case System.DayOfWeek.Thursday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期四";
                                break;
                            case System.DayOfWeek.Friday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期五";
                                break;
                            case System.DayOfWeek.Saturday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期六";
                                break;
                            case System.DayOfWeek.Sunday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期天";
                                break;
                        }
                    }
                }
            }
        }

        private void dataGridViewAnnualHoliday_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

        }

        //列格式化
        private void dataGridViewAnnualHoliday_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex >= 0)
                {
                    int count = dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells.Count;
                    if (dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[2].Value != null && dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[0].Value != DBNull.Value)
                    {
                        DateTime date = Convert.ToDateTime(dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[0].Value);
                        switch (date.DayOfWeek)
                        {
                            case System.DayOfWeek.Monday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期一";
                                break;
                            case System.DayOfWeek.Tuesday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期二";
                                break;
                            case System.DayOfWeek.Wednesday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期三";
                                break;
                            case System.DayOfWeek.Thursday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期四";
                                break;
                            case System.DayOfWeek.Friday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期五";
                                break;
                            case System.DayOfWeek.Saturday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期六";
                                break;
                            case System.DayOfWeek.Sunday:
                                dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[1].Value = "星期天";
                                break;
                        }

                        if (string.IsNullOrEmpty(dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[4].Value.ToString()))
                            this.dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[3].Value = "所有";
                        else
                            this.dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[3].Value = "部份";
                    }
                }
            }
        }

        //自动排假
        private void btnAutoArrangeHoliday_Click(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == -1)
            {
                MessageBox.Show("請選擇排假類別", "提示", MessageBoxButtons.OK);
                return;
            }
            string sYear = this.comboBoxEdit1.Text;
            data.Tables[0].Clear();
            DateTime date = new DateTime();
            int year = Convert.ToInt32(this.comboBoxEdit1.EditValue);
            DataRow dr = null;
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 1; j <= DateTime.DaysInMonth(date.Year, date.Month); j++)
                {
                    date = new DateTime(year, i, j);

                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        if (date.DayOfWeek == System.DayOfWeek.Sunday)
                        {
                            dr = data.Tables[0].NewRow();
                            dr[Model.AnnualHoliday.PRO_HolidayDate] = date;
                            dr[Model.AnnualHoliday.PRO_HolidayName] = "週日休假";
                            data.Tables[0].Rows.Add(dr);
                        }
                    }
                    else
                    {
                        if (date.DayOfWeek == System.DayOfWeek.Sunday)
                        {
                            dr = data.Tables[0].NewRow();
                            dr[Model.AnnualHoliday.PRO_HolidayDate] = date;
                            dr[Model.AnnualHoliday.PRO_HolidayName] = "週日休假";
                            data.Tables[0].Rows.Add(dr);
                        }
                        if (date.DayOfWeek == System.DayOfWeek.Saturday)
                        {
                            dr = data.Tables[0].NewRow();
                            dr[Model.AnnualHoliday.PRO_AnnualHolidayId] = Guid.NewGuid().ToString();
                            dr[Model.AnnualHoliday.PRO_Years] = year;
                            dr[Model.AnnualHoliday.PRO_HolidayDate] = date;
                            dr[Model.AnnualHoliday.PRO_HolidayName] = "週六休假";
                            data.Tables[0].Rows.Add(dr);
                        }
                    }
                }
            }

            DataView _dv = data.Tables[0].DefaultView;
            _dv.Sort = "HolidayDate ASC";


            this.bindingSourceAnnualHoliday.DataSource = data.Tables[0].DefaultView;
            //if (this._holidayManager.ExistsAutoYear(this.comboBoxEdit1.Text))
            this.btnAutoArrangeHoliday.Enabled = false;
            //else
            //this.btnAutoArrangeHoliday.Enabled = true;
        }

        //下拉框所选值改变事件
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._holidayManager.ExistsAutoYear(this.comboBoxEdit1.Text))
                this.btnAutoArrangeHoliday.Enabled = false;
            else
                this.btnAutoArrangeHoliday.Enabled = true;
            data = this._holidayManager.SelectAnnualInfoByYear(Int32.Parse(this.comboBoxEdit1.Text));
            this.bindingSourceAnnualHoliday.DataSource = data.Tables[0].DefaultView;
        }

        //对单元格编辑数据判断
        private void dataGridViewSpecificHoliday_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //string DefaultValue = this.dataGridViewSpecificHoliday.CurrentCell.Value.ToString();
            //if (DefaultValue == "" || DefaultValue == null)
            //{
            //    MessageBox.Show("輸入不能為空!");
            //}
            //else
            //{
            //    if (this.dataGridViewSpecificHoliday.CurrentCell.ColumnIndex == 0)
            //    {
            //        try
            //        {
            //            string dgs_Year = this.comboBoxEdit1.Text;
            //            DateTime dt = Convert.ToDateTime(dgs_Year + "/" + this.dataGridViewSpecificHoliday.CurrentCell.Value.ToString());
            //            this.dataGridViewSpecificHoliday.CurrentCell.Value = dt.Month.ToString() + "/" + dt.Day.ToString();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("輸入的日期格式有誤,請重新輸入!");
            //            this.dataGridViewSpecificHoliday.CurrentCell.Value = "";
            //        }
            //    }
            //}
        }

        private void dataGridViewAnnualHoliday_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                ChooseDepartments f = new ChooseDepartments(this.dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[4].Value.ToString());
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    //this.dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[e.ColumnIndex] = f.SelectDepartNames;
                    if (string.IsNullOrEmpty(f.SelectDepartIds))
                        this.dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "所有";
                    else
                        this.dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "部份";

                    //重新赋值
                    this.dataGridViewAnnualHoliday.Rows[e.RowIndex].Cells[4].Value = f.SelectDepartIds;
                }
            }
        }

        private void barExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Type objClassType = null;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            if (objClassType == null)
            {
                MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                return;
            }

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Range r = excel.Cells;
            excel.Rows.RowHeight = 20;
            excel.Columns.ColumnWidth = 15;
            excel.get_Range(excel.Cells[2, 1], excel.Cells[dataGridViewAnnualHoliday.Rows.Count, 1]).NumberFormatLocal = "yyyy-MM-dd";
            excel.get_Range(excel.Cells[1, 1], excel.Cells[dataGridViewAnnualHoliday.Rows.Count, 5]).Borders.LineStyle = XlLineStyle.xlContinuous;
            excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 5]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 1]).Value2 = "假日日期";
            ((Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 2]).Value2 = "星期";
            ((Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 3]).Value2 = "假日名稱";
            ((Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 4]).Value2 = "部門";
            ((Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 5]).Value2 = "是否國定假日";
            string date = string.Empty;
            string week = string.Empty;
            string name = string.Empty;
            string department = string.Empty;
            string isHoliday = string.Empty;
            for (int i = 0; i < dataGridViewAnnualHoliday.Rows.Count-1; i++)
            {
                date = dataGridViewAnnualHoliday.Rows[i].Cells["HolidayDate"].Value == null ? "" : dataGridViewAnnualHoliday.Rows[i].Cells["HolidayDate"].Value.ToString();
                week = dataGridViewAnnualHoliday.Rows[i].Cells["DayOfWeek"].Value == null ? "" : dataGridViewAnnualHoliday.Rows[i].Cells["DayOfWeek"].Value.ToString();
                name = dataGridViewAnnualHoliday.Rows[i].Cells["HolidayName"].Value == null ? "" : dataGridViewAnnualHoliday.Rows[i].Cells["HolidayName"].Value.ToString();
                department = dataGridViewAnnualHoliday.Rows[i].Cells["Departs"].Value == null ? "" : dataGridViewAnnualHoliday.Rows[i].Cells["Departs"].Value.ToString();
                isHoliday = (dataGridViewAnnualHoliday.Rows[i].Cells["IsNationalHoliday"].Value == null ? "" : dataGridViewAnnualHoliday.Rows[i].Cells["IsNationalHoliday"].Value.ToString()) == "True" ? "是" : "";


                ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 2, 1]).Value2 = date;
                ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 2, 2]).Value2 = week;
                ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 2, 3]).Value2 = name;
                ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 2, 4]).Value2 = department;
                ((Microsoft.Office.Interop.Excel.Range)excel.Cells[i + 2, 5]).Value2 = isHoliday;
            }
            ((Microsoft.Office.Interop.Excel.Range)excel.Cells[dataGridViewAnnualHoliday.Rows.Count + 1, 1]).Value2 = "如果數據未顯示完整，請先在ERP裡面將滾動條拉至下邊緣以刷新所有數據！";
            excel.Visible = true;
        }
    }
}