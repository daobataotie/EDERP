using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.produceManager.PronoteHeader
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  裴盾            完成时间:2010-03-6
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChoosePronoteHeaderDetailsForm : DevExpress.XtraEditors.XtraForm
    {
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        Model.PronoteHeader pronoteHeader = new Book.Model.PronoteHeader();
        public static IList<Model.PronoteHeader> _pronoteHeaderList;
        private IList<Model.PronoteHeader> DetailList;
        private Model.WorkHouse workHouseIndepot;
        private string sign = string.Empty;
        private int flag = 0;
        private int type = -1;   //0，非生產入库，1生产入库 生产入库时查询前部门转入 前部门合计等数量
        //默认能多选

        public ChoosePronoteHeaderDetailsForm()
        {
            InitializeComponent();
            // this.pronoteHeader.Details = PronotedetailsManager.Select();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddDays(-15).Date;
            this.dateEditEndDate.DateTime = DateTime.Now;
            _pronoteHeaderList = new List<Model.PronoteHeader>();
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.checkEdit1.Checked = true;
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.newChooseWorkHorse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.bindingSourceMachine.DataSource = new BL.PronoteMachineManager().Select();

            System.Data.DataTable dt = this.GetExcelData();
            if (dt.Rows != null && dt.Rows.Count > 0)
            {
                this.lbl_Status.ForeColor = Color.Red;
            }
            else
                this.lbl_Status.ForeColor = Color.Green;
        }

        public Model.PronoteHeader SelectItem
        {
            get { return this.bindingSource1.Current as Model.PronoteHeader; }
        }

        public IList<Model.PronoteHeader> SelectItems;

        /// <summary>
        /// 1:单选
        /// </summary>
        /// <param name="tag"></param>
        public ChoosePronoteHeaderDetailsForm(int tag)
            : this()
        {
            flag = tag;
            this.newChooseWorkHorse.Enabled = false;
            // this.pronoteHeader.Details = PronotedetailsManager.Select();

        }

        public ChoosePronoteHeaderDetailsForm(int type, bool IsZuzhuang)
            : this()
        {
            this.type = type;
            this.newChooseWorkHorse.Enabled = false;
            this.checkEditZizhiZZ.Checked = IsZuzhuang;
            // this.pronoteHeader.Details = PronotedetailsManager.Select();

        }
        //i=0，非生產入库，1生产入库 生产入库时查询前部门转入 前部门合计等数量
        public ChoosePronoteHeaderDetailsForm(Model.WorkHouse workHouseIndepot, int i)
            : this()
        {
            this.workHouseIndepot = workHouseIndepot;
            this.type = i;
            this.newChooseWorkHorse.Enabled = false;
            this.newChooseWorkHorse.EditValue = workHouseIndepot;
        }

        public ChoosePronoteHeaderDetailsForm(int i, string sign)
            : this()
        {
            this.sign = sign;
            this.type = i;
        }

        private void ChoosePronoteHeaderDetailsForm_Load(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                gridView1.Columns[0].Visible = false;
            }
            if (type == 0)
            {
                gridColumn17.Visible = false;
                gridColumn14.Visible = false;
                gridColumn15.Visible = false;
                gridColumn16.Visible = false;
            }
            if (type == -1)
            {
                gridColumn17.Visible = false;
                gridColumn14.Visible = false;
                gridColumn15.Visible = false;
                gridColumn16.Visible = false;
                gridColumn7.Visible = false;
            }
            if (type == 1)
            {
                this.date_JiaohuoStart.Visible = true; ;
                this.date_JiaohuoEnd.Visible = true; ;
                this.lookUpEditMachine.Visible = true;
            }
            //DetailList = this.pronoteHeaderManager.GetByDate(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, null, null, null, null, null, -1, this.workHouseIndepot == null ? null : this.workHouseIndepot.WorkHouseId, true);

            //this.bindingSource1.DataSource = DetailList;
        }

        //确定
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //foreach (Model.Pronotedetails Pronotedetails in pronoteHeader.Details)
            //{
            //    if (Pronotedetails.Checkeds == true)
            //    {
            //        produceManager.ProduceInDepot.EditForm._pronotedetails.Add(Pronotedetails);
            //    }
            //}
            if (DetailList != null)
                this.SelectItems = DetailList.Where(d => d.Checkeds == true).ToList();
            this.DialogResult = DialogResult.OK;
        }

        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.PronoteHeader> details = this.bindingSource1.DataSource as IList<Model.PronoteHeader>;
            //if (details == null || details.Count < 1) return;
            //Model.Product products = details[e.ListSourceRowIndex].Product;
            ////Model.InvoiceXODetail invoiceXODetail = new BL.InvoiceXODetailManager().Get(details[e.ListSourceRowIndex].InvoiceXODetailId);
            ////Model.CustomerProducts cusProducts = details[e.ListSourceRowIndex].PrimaryKey;
            //switch (e.Column.Name)
            //{
            //    //case "gridColumn2":
            //    //    if (detail == null) return;
            //    //    e.DisplayText = detail.PronoteDate.Value.ToString("yyyy-MM-dd");
            //    //    break;
            //    case "gridColumnProId":
            //        if (products == null) return;
            //        e.DisplayText = string.IsNullOrEmpty(products.Id) ? "" : products.Id; ;
            //        break;
            //    //case "gridColumn7":
            //    //    if (invoiceXODetail == null) return;
            //    //    e.DisplayText = invoiceXODetail.InvoiceXODetailQuantity.ToString(); ;
            //    //    break;
            //    case "gridColumnCusProName":
            //        if (products == null) return;
            //        e.DisplayText = string.IsNullOrEmpty(products.CustomerProductName) ? "" : products.CustomerProductName;
            //        break;
            //}
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_pronoteHeaderList == null) return;
            if (e.Column.Name == "gridColumnChecked")
            {
                Model.PronoteHeader header = this.gridView1.GetRow(e.RowHandle) as Model.PronoteHeader;
                if (header != null)
                {

                    if ((bool)e.Value)
                    {
                        _pronoteHeaderList.Add(header);
                    }
                    if (!(bool)e.Value)
                    {
                        for (int i = 0; i < _pronoteHeaderList.Count; i++)
                        {
                            if (_pronoteHeaderList[i].PronoteHeaderID == header.PronoteHeaderID)
                            {
                                _pronoteHeaderList.RemoveAt(i);
                                break;
                            }
                        }

                    }
                }
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            DateTime startTime = global::Helper.DateTimeParse.NullDate;
            DateTime endTime = global::Helper.DateTimeParse.EndDate;

            DateTime JiaohuoDateStart = global::Helper.DateTimeParse.NullDate;
            DateTime JiaohuoDateEnd = global::Helper.DateTimeParse.EndDate;
            if (this.dateEditStartDate.EditValue != null)
            {
                startTime = this.dateEditStartDate.DateTime;
            }
            if (this.dateEditEndDate.EditValue != null)
            {
                endTime = this.dateEditEndDate.DateTime;
            }
            if (this.date_JiaohuoStart.EditValue != null)
                JiaohuoDateStart = this.date_JiaohuoStart.DateTime;
            if (this.date_JiaohuoEnd.EditValue != null)
                JiaohuoDateEnd = this.date_JiaohuoEnd.DateTime;

            if (type == 0) //质检
                DetailList = this.pronoteHeaderManager.GetByDateZJ(startTime, endTime, this.newChooseCustomer.EditValue as Model.Customer, this.textEditCusXOId.Text, this.buttonEditPro1.EditValue as Model.Product, null, null, this.workHouseIndepot == null ? null : this.workHouseIndepot.WorkHouseId, this.checkEdit1.Checked, this.TXTproNameKey.Text, this.TXTproCusNameKey.Text, this.txtpronoteHeaderIdKey.Text, this.sign, this.checkEditZizhi.Checked, this.checkEditZizhiZZ.Checked, this.checkEditZizhiBCP.Checked);
            else if (type == 1) //生产入库
                DetailList = this.pronoteHeaderManager.GetByDate(startTime, endTime, this.newChooseCustomer.EditValue as Model.Customer, this.textEditCusXOId.Text, this.buttonEditPro1.EditValue as Model.Product, null, null, this.workHouseIndepot == null ? null : this.workHouseIndepot.WorkHouseId, this.checkEdit1.Checked, this.TXTproNameKey.Text, this.TXTproCusNameKey.Text, this.txtpronoteHeaderIdKey.Text, this.checkEditZizhi.Checked, this.checkEditZizhiZZ.Checked, this.checkEditZizhiBCP.Checked, JiaohuoDateStart, JiaohuoDateEnd, this.lookUpEditMachine.EditValue == null ? "" : this.lookUpEditMachine.EditValue.ToString());
            else
            {
                this.workHouseIndepot = this.newChooseWorkHorse.EditValue as Model.WorkHouse;
                DetailList = this.pronoteHeaderManager.GetByDateMa(startTime, endTime, this.newChooseCustomer.EditValue as Model.Customer, this.textEditCusXOId.Text, this.buttonEditPro1.EditValue as Model.Product, null, null, -1, this.workHouseIndepot == null ? null : this.workHouseIndepot.WorkHouseId, this.checkEdit1.Checked, this.TXTproNameKey.Text, this.TXTproCusNameKey.Text, this.txtpronoteHeaderIdKey.Text, this.checkEditZizhi.Checked, this.checkEditZizhiZZ.Checked, this.checkEditZizhiBCP.Checked);
            }


            if (DetailList != null)
            {
                int flag = 0;
                for (int i = 0; i < _pronoteHeaderList.Count; i++)
                {
                    foreach (Model.PronoteHeader detail in DetailList)
                    {
                        if (_pronoteHeaderList[i].PronoteHeaderID == detail.PronoteHeaderID)
                        {
                            detail.Checkeds = true;
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                        break;
                }
            }
            this.bindingSource1.DataSource = DetailList;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Model.Pronotedetails Pronotedetails = this.bindingSource1.Current as Model.Pronotedetails;
            //if (Pronotedetails != null)
            //{
            //    produceManager.ProduceInDepot.EditForm._pronotedetails.Add(Pronotedetails);
            //}
            this.DialogResult = DialogResult.OK;
        }


        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            if ((this.bindingSource1.Current as Model.PronoteHeader).IsClose.Value)
                return;
            if (MessageBox.Show("結案后,此加工單將不能做領料,退料與入庫動作,是否繼續?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.gridView1.SetRowCellValue(DetailList.IndexOf(this.bindingSource1.Current as Model.PronoteHeader), this.gridColumn13, true);
            this.pronoteHeaderManager.UpdateHeaderIsClse((this.bindingSource1.Current as Model.PronoteHeader).PronoteHeaderID, true);
            //从当前集合删除该项（2012.12.7）
            this.bindingSource1.Remove(this.bindingSource1.Current);
        }

        //加工单查看
        private void ItemHyperLinkPronoteHeaderID_Click(object sender, EventArgs e)
        {
            Model.PronoteHeader d = (this.bindingSource1.Current as Model.PronoteHeader);
            if (d != null)
            {
                PronoteHeader.EditForm f = new EditForm(d);
                f.ShowDialog();
            }
        }

        private void buttonEditPro1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro1.EditValue = form.SelectedItem as Model.Product;
            }
            form.Dispose();
            GC.Collect();
        }

        /// <summary>
        /// 射出工作单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (this.DetailList == null || this.DetailList.Count == 0)
            {
                MessageBox.Show("無數據！", this.Text, MessageBoxButtons.OK);
                return;
            }
            SheChuRO ro = new SheChuRO(this.DetailList);
            ro.ShowPreviewDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (this.DetailList == null || this.DetailList.Count == 0)
            {
                MessageBox.Show("無數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
            ROProductIndepot ro = new ROProductIndepot(this.DetailList);
            ro.ShowPreviewDialog();
        }

        private System.Data.DataTable GetExcelData()
        {
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

            System.Data.DataTable dt = pronoteHeaderManager.GetExcelData(startTime, endTime);
            return dt;
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            Type objClassType = null;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            if (objClassType == null)
            {
                MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                return;
            }

            System.Data.DataTable dt = this.GetExcelData();
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
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1 + dt.Rows.Count, 10]).Borders.LineStyle = XlLineStyle.xlContinuous;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1 + dt.Rows.Count, 10]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).ColumnWidth = 18;
                excel.get_Range(excel.Cells[1, 2], excel.Cells[1, 2]).ColumnWidth = 18;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 10]).Font.Size = 12;
                excel.get_Range(excel.Cells[1, 5], excel.Cells[1, 5]).ColumnWidth = 50;
                excel.Cells[1, 1] = "加工單號";
                excel.Cells[1, 2] = "客戶訂單號";
                excel.Cells[1, 3] = "最慢交期";
                excel.Cells[1, 4] = "交貨日期";
                excel.Cells[1, 5] = "貨品名稱";
                excel.Cells[1, 6] = "生產數量";
                excel.Cells[1, 7] = "當前合計生產";
                excel.Cells[1, 8] = "當前合格";
                excel.Cells[1, 9] = "單位";
                excel.Cells[1, 10] = "機台";
                #endregion

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    excel.Cells[i + 2, 1] = dt.Rows[i]["PronoteHeaderID"] == null ? "" : dt.Rows[i]["PronoteHeaderID"].ToString();
                    excel.Cells[i + 2, 2] = dt.Rows[i]["CustomerInvoiceXOId"] == null ? "" : dt.Rows[i]["CustomerInvoiceXOId"].ToString();
                    excel.Cells[i + 2, 3] = dt.Rows[i]["LastDate"] == null ? "" : dt.Rows[i]["LastDate"].ToString();
                    excel.Cells[i + 2, 4] = dt.Rows[i]["InvoiceYjrq"] == null ? "" : dt.Rows[i]["InvoiceYjrq"].ToString();
                    excel.Cells[i + 2, 5] = dt.Rows[i]["ProductName"] == null ? "" : dt.Rows[i]["ProductName"].ToString();
                    excel.Cells[i + 2, 6] = dt.Rows[i]["DetailsSum"] == null ? "" : dt.Rows[i]["DetailsSum"].ToString();
                    excel.Cells[i + 2, 7] = dt.Rows[i]["TotalProcess"] == null ? "" : dt.Rows[i]["TotalProcess"].ToString();
                    excel.Cells[i + 2, 8] = dt.Rows[i]["TotalPass"] == null ? "" : dt.Rows[i]["TotalPass"].ToString();
                    excel.Cells[i + 2, 9] = dt.Rows[i]["ProductUnit"] == null ? "" : dt.Rows[i]["ProductUnit"].ToString();
                    excel.Cells[i + 2, 10] = dt.Rows[i]["MachineId"] == null ? "" : dt.Rows[i]["MachineId"].ToString();
                }
                excel.Visible = true;
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void dateEditStartDate_EditValueChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dt = this.GetExcelData();
            if (dt.Rows != null && dt.Rows.Count > 0)
            {
                this.lbl_Status.ForeColor = Color.Red;
            }
            else
                this.lbl_Status.ForeColor = Color.Green;
        }
    }
}