using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.ExcelClass;
using System.Linq;
using Microsoft.Office.Interop.Excel;
namespace Book.UI.CustomsClearance
{
    public partial class BGHandbookForm : Settings.BasicData.BaseEditForm
    {
        private BL.BGHandbookManager bGHandbookManager = new Book.BL.BGHandbookManager();
        private BL.BGHandbookDetail1Manager bGHandbookDetail1Manager = new Book.BL.BGHandbookDetail1Manager();
        private BL.BGHandbookDetail2Manager bGHandbookDetail2Manager = new Book.BL.BGHandbookDetail2Manager();
        private Model.BGHandbook _bGHandbook;
        private int flag = 0;//是否更改转出到手册数量或者手册号
        private IList<Model.BGHandbookDetail2> Detail2ZhjuanCe=new List<Model.BGHandbookDetail2>();
        public BGHandbookForm()
        {
            InitializeComponent();
          
            this.newChooseEmployee0.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseEmployee1.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";
            if (this.action != "view")
            {
                this.barButtonItem1.Enabled = false;
                this.barButtonItem3.Enabled = false;
                this.barButtonItem2.Enabled = false;
            }

            else
            {
                this.barButtonItem1.Enabled = true;
                this.barButtonItem3.Enabled = true;
                this.barButtonItem2.Enabled = true;

            }
        }
        public BGHandbookForm(Model.BGHandbook bGHandbook)
            : this()
        {
            this._bGHandbook = bGHandbook;
            this._bGHandbook.Detail1 = this.bGHandbookDetail1Manager.Select(bGHandbook.BGHandbookId);
            this._bGHandbook.Detail2 = this.bGHandbookDetail2Manager.Select(bGHandbook.BGHandbookId);
            this.action = "view";
        }

        public BGHandbookForm(Model.BGHandbook bGHandbook, string action)
            : this()
        {
            this._bGHandbook = bGHandbook;
            this._bGHandbook.Detail1 = this.bGHandbookDetail1Manager.Select(bGHandbook.BGHandbookId);
            this._bGHandbook.Detail2 = this.bGHandbookDetail2Manager.Select(bGHandbook.BGHandbookId);
            this.action = "view";
        }

        protected override void AddNew()
        {

            if (this._bGHandbook != null)
            {
                this._bGHandbook.BGHandbookId = this.bGHandbookManager.GetId();
                this._bGHandbook.Id = null;
                this._bGHandbook.BGHandbookDate = DateTime.Now;
                this._bGHandbook.Employee = BL.V.ActiveOperator.Employee;
            }
            else
            {

                this._bGHandbook = new Book.Model.BGHandbook();
                this._bGHandbook.BGHandbookId = this.bGHandbookManager.GetId();
                this._bGHandbook.BGHandbookDate = DateTime.Now;
                this._bGHandbook.Employee = BL.V.ActiveOperator.Employee;
                this._bGHandbook.EmployeeId = BL.V.ActiveOperator.EmployeeId;
            }
            this.action = "insert";
        }
        public override void Refresh()
        {


            if (this._bGHandbook == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._bGHandbook = this.bGHandbookManager.GetDetails(_bGHandbook.BGHandbookId);

                }


            }
            this.textId.Text = _bGHandbook.BGHandbookId;
            this.dateDate.EditValue = _bGHandbook.BGHandbookDate;
            this.textEdit1.Text = this._bGHandbook.Id;
            this.newChooseEmployee0.EditValue = this._bGHandbook.Employee;
            this.newChooseEmployee1.EditValue = this._bGHandbook.AuditEmp;
            this.textEditAudit.Text = this.GetAuditName(this._bGHandbook.AuditState);
            this.dateUpdate.EditValue = this._bGHandbook.UpdateTime;
            this.bindingSource1.DataSource = this._bGHandbook.Detail1;
            this.bindingSource2.DataSource = this._bGHandbook.Detail2;


            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    break;

                case "update":

                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;

                    break;

                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.gridView2.OptionsBehavior.Editable = false;
                    break;
                   
            }
            base.Refresh();
        }

        protected override void Save()
        {
            _bGHandbook.BGHandbookId = this.textId.Text;
            _bGHandbook.BGHandbookDate = this.dateDate.DateTime;
            this._bGHandbook.Id = this.textEdit1.Text;
            this._bGHandbook.Employee = this.newChooseEmployee0.EditValue as Model.Employee;
            this._bGHandbook.AuditEmp = this.newChooseEmployee1.EditValue as Model.Employee;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this.bGHandbookManager.Insert(this._bGHandbook);

                    break;

                case "update":
                    this.bGHandbookManager.Update(this._bGHandbook);
                    if (flag == 1)
                    {
                        foreach (Model.BGHandbookDetail2 model in Detail2ZhjuanCe)
                        {
                            this.bGHandbookDetail2Manager.UpdateCeIn(model.ZhuanRuShouCeId, model.Id.ToString(),model.ZhuanCeOutQuantity.HasValue?model.ZhuanCeOutQuantity.Value:0);
                        }
                        flag = 0;
                    }

                    break;
            }

        }
        protected override bool HasRows()
        {
            return this.bGHandbookManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.bGHandbookManager.HasRowsAfter(this._bGHandbook);
        }

        protected override bool HasRowsPrev()
        {
            return this.bGHandbookManager.HasRowsBefore(this._bGHandbook);
        }

        protected override void MoveFirst()
        {
            this._bGHandbook = this.bGHandbookManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._bGHandbook = this.bGHandbookManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.BGHandbook model = this.bGHandbookManager.GetNext(this._bGHandbook);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGHandbook = model;
        }

        protected override void MovePrev()
        {
            Model.BGHandbook model = this.bGHandbookManager.GetPrev(this._bGHandbook);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGHandbook = model;
        }

        protected override void Delete()
        {

            if (this._bGHandbook == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.bGHandbookManager.Delete(this._bGHandbook);
            this._bGHandbook = this.bGHandbookManager.GetNext(this._bGHandbook);
            if (this._bGHandbook == null)
            {
                this._bGHandbook = this.bGHandbookManager.GetLast();
            }
        }


        //导入
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                    ExcelClass1 ec = new ExcelClass1();
                    ec.Open(openFileDialog1.FileName);

                try
                {
                    BL.V.BeginTransaction();
                    Model.BGHandbook bghandbook=null;
                    action = "insert";
                   
                    Model.BGHandbookDetail1 detail1;
                    Model.BGHandbookDetail2 detail2;
                  
                    int m =0;
                    for (int i = 8; i <= ec.wb.Worksheets.Count; i++)
                    {

                        bghandbook = new Book.Model.BGHandbook();
                        bghandbook.Detail1 = new List<Model.BGHandbookDetail1>();
                        bghandbook.Detail2 = new List<Model.BGHandbookDetail2>();
                        bghandbook.BGHandbookId = this.bGHandbookManager.GetId();
                        bghandbook.BGHandbookDate = DateTime.Now;
                        bghandbook.Employee = BL.V.ActiveOperator.Employee;
                        bghandbook.EmployeeId = BL.V.ActiveOperator.EmployeeId;
                        bghandbook.AuditEmpId = BL.V.ActiveOperator.EmployeeId;
                        bghandbook.AuditState = 3;

                        bghandbook.InsertTime = DateTime.Now;

                        m = 0;

                        Microsoft.Office.Interop.Excel.Worksheet ss = (Microsoft.Office.Interop.Excel.Worksheet)ec.wb.Worksheets[i];

                        bghandbook.Id = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[1, 1]).Text.ToString();
                        int flag = 0;
                        for (int j = 3; j < 2500; j++)
                        {


                            if (flag == 0)
                            {
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString().Trim() == "合计")
                                {
                                    flag = 1;
                                    continue;
                                }

                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString() == ""&& ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString() == "")
                                    continue;
                                else if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString() == "" && ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString() != "") //多子件
                                {
                                    m = m+1;


                                    detail1 = new Book.Model.BGHandbookDetail1();
                                    detail1.BGHandbookDetail1Id = Guid.NewGuid().ToString();
                                    detail1.NOId = m;

                                    int c = j;
                                    decimal beeQuantity=decimal.Zero;
                                    for (c=j; c >2; c--)
                                    {
                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[c, 3]).Text.ToString() != "")
                                        {
                                            detail1.Id = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[c, 1]).Text.ToString());
                                            beeQuantity =decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[c, 5]).Text.ToString());
                                            break;
                                        }
                                    }

                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j,7]).Text.ToString() != "")
                                    detail1.LId = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString());
                                    detail1.Column1 = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString() != "")
                                    detail1.Ljingliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString() != "")
                                        detail1.LjingSunliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString() != "")
                                        detail1.LjingQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j,12]).Text.ToString() != "")
                                    detail1.Lsunhaolv =double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString());

                                    detail1.LiLunHaoYongJing = (double)GetDecimal(beeQuantity * decimal.Parse(detail1.LjingQuantity.ToString()), 2);
                                    detail1.LiLunHaoYongJingSun = (double)GetDecimal(beeQuantity * decimal.Parse(detail1.LjingQuantity.ToString()) / (1 - decimal.Parse(detail1.Lsunhaolv.ToString()) * 0.01M), 2);
                                   



                                    bghandbook.Detail1.Add(detail1);
                                }
                                else
                                {
                                    m = m+1;



                                    detail1 = new Book.Model.BGHandbookDetail1();
                                    detail1.BGHandbookDetail1Id = Guid.NewGuid().ToString();
                                    detail1.NOId = m;
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 1]).Text.ToString() != "")
                                    detail1.Id = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 1]).Text.ToString());


                                    detail1.CusProName = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 2]).Text.ToString();
                                    detail1.ProName = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 4]).Text.ToString() != "")
                                    detail1.Quantity = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 4]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 5]).Text.ToString() != "")
                                    {
                                        detail1.BeeQuantity = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 5]).Text.ToString());
                                        detail1.BeeQuantityTemp = detail1.BeeQuantity;
                                    }

                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j,6]).Text.ToString() != "")
                                    detail1.UpQuantity = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 6]).Text.ToString());

                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString() != "")
                                    detail1.LId = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString());
                                    detail1.Column1 = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString() != "")
                                    detail1.Ljingliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j,10]).Text.ToString() != "")
                                    detail1.LjingSunliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString());

                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString() != "")
                                    detail1.LjingQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString());

                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j,12]).Text.ToString() != "")
                                    detail1.Lsunhaolv = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString());
                                    bghandbook.Detail1.Add(detail1);
                                }
                            }

                            if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 6]).Text.ToString().Trim() == "序号")
                            {
                                flag = 2;
                                continue;
                            }
                            if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString().Trim() == "序号")
                            {
                                flag = 3;
                                continue;
                            }
                            if (flag == 2)
                            {
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 6]).Text.ToString().Trim() == "")
                                    continue;
                                detail2 = new Book.Model.BGHandbookDetail2();
                                detail2.BGHandbookDetail2Id = Guid.NewGuid().ToString();
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 6]).Text.ToString() != "")
                                detail2.Id = int.Parse( ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 6]).Text.ToString());
                                detail2.ProName = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString();

                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString() != "")
                                detail2.Ljingliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString());
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString() != "")
                                detail2.LjingSunliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString());
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString()!="")
                                detail2.LLastjingSunliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString());
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 13]).Text.ToString() != "")
                                detail2.Lchazhi = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 13]).Text.ToString());
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 14]).Text.ToString() != "")

                                detail2.LPrice =decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 14]).Text.ToString());


                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 15]).Text.ToString() != "")
                                detail2.Lmoney = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 15]).Text.ToString());
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 16]).Text.ToString() != "")

                                detail2.LbejinQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 16]).Text.ToString());
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 17]).Text.ToString() != "")
                                detail2.LciciMoney = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 17]).Text.ToString());
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 18]).Text.ToString() != "")
                                detail2.JinKouiMoney = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 18]).Text.ToString());
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 19]).Text.ToString() != "")
                                detail2.LastMoney = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 19]).Text.ToString());
                                bghandbook.Detail2.Add(detail2);
                            }
                            if (flag == 3)
                            {
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString() == "")                                   
                                break;
                                foreach (Model.BGHandbookDetail2 detail in bghandbook.Detail2)
                                {
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString() != "")
                                    detail.ZhuanCeInQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString() != "")
                                    detail.UpQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString() != "")
                                    detail.YaoJInQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString() != "")
                                    detail.HaiKeJInQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString());


                                    double a = 0;
                                    double.TryParse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString(),out a);
                                  detail.LilunHaoYong=a;

                                  double b= 0;
                

                                      double.TryParse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 13]).Text.ToString(), out b);
                                      detail.LilunStock = a;
                                   
                                }
                                
                            }
                           
                        }





                        this.bGHandbookManager.Insert(bghandbook);

                    }
                  
                    BL.V.CommitTransaction();
                    this._bGHandbook = this.bGHandbookManager.GetLast();
                    this.action = "view";
                    this.Refresh();
                }
                catch (Exception ex)
                {
                    BL.V.RollbackTransaction();
                    ec.Close();
                    throw ex;

                }
                ec.Close();
                ec.release_xlsObj();


            }
        }
        //导出
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelClass.ExcelClass1 ex = new ExcelClass1();
            ex.Create();
           // ex.AddSheet("130101");
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)ex.app.Application.Worksheets.get_Item(1);

     
            ex.UniteCells(sheet, 1, 1, 1, 4);
            sheet.Cells[1,1]=this._bGHandbook.Id;
              sheet.Cells[2,1]="成品项号";
             sheet.Cells[2,2]="客户型号";
             sheet.Cells[2,3]="成品规格";
             sheet.Cells[2,4]="成品备案数量";
             sheet.Cells[2,5]="已出数量";
             sheet.Cells[2,6]="剩余量";
             sheet.Cells[2,7]="料件项号";
             sheet.Cells[2,8]="原料";
             sheet.Cells[2,9]="净量";
             sheet.Cells[2,10]="净+损";
             sheet.Cells[2,11]="净KG";
             sheet.Cells[2,12]="损耗%";
             int count1 = this._bGHandbook.Detail1.Count;

           // ex.AddSheet("130101");
            for (int i=0;i<  this._bGHandbook.Detail1.Count;i++)
            {
                sheet.Cells[i + 3, 1] = this._bGHandbook.Detail1[i].Id;
                sheet.Cells[i + 3, 2] = this._bGHandbook.Detail1[i].CusProName;
                sheet.Cells[i + 3, 3] = this._bGHandbook.Detail1[i].ProName;
                sheet.Cells[i + 3, 4] =this._bGHandbook.Detail1[i].Quantity.HasValue? this._bGHandbook.Detail1[i].Quantity.Value.ToString("0.####"):"";
                sheet.Cells[i + 3, 5] = this._bGHandbook.Detail1[i].BeeQuantity.HasValue?  this._bGHandbook.Detail1[i].BeeQuantity.Value.ToString("0.####"):"";
                sheet.Cells[i + 3, 6] = this._bGHandbook.Detail1[i].UpQuantity.HasValue? this._bGHandbook.Detail1[i].UpQuantity.Value.ToString("0.####"):"";
                sheet.Cells[i + 3, 7] = this._bGHandbook.Detail1[i].LId;
                sheet.Cells[i + 3,8] = this._bGHandbook.Detail1[i].Column1;
                sheet.Cells[i + 3, 9] =this._bGHandbook.Detail1[i].Ljingliang.HasValue? this._bGHandbook.Detail1[i].Ljingliang.Value.ToString("0.00"):"";
                sheet.Cells[i + 3, 10] = this._bGHandbook.Detail1[i].LjingSunliang.HasValue? this._bGHandbook.Detail1[i].LjingSunliang.Value.ToString("0.00"):"";
                sheet.Cells[i + 3, 11] =this._bGHandbook.Detail1[i].LjingQuantity.HasValue? this._bGHandbook.Detail1[i].LjingQuantity.Value.ToString():"";
                sheet.Cells[i + 3, 12] = this._bGHandbook.Detail1[i].Lsunhaolv.HasValue ? this._bGHandbook.Detail1[i].Lsunhaolv.Value.ToString() : "";        
                
            }

            sheet.Cells[this._bGHandbook.Detail1.Count + 6, 3] = "合计";

            sheet.Cells[this._bGHandbook.Detail1.Count + 6, 4] = this._bGHandbook.Detail1.Sum(a => a.Quantity).ToString();
            sheet.Cells[this._bGHandbook.Detail1.Count + 6,5] = this._bGHandbook.Detail1.Sum(a => a.BeeQuantity).ToString();

            sheet.Cells[this._bGHandbook.Detail1.Count + 9, 8] = "料件表";

            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 6] = "序号";        
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 8] = "原料";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 9] = "总净";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 10] = "总净+损";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 11] = "前次备案的总净+损";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 13] = "差值";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 14] = "单价";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 15] = "金额（差值）";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 16] = "已经进口";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 17] = "此次总额";
            int m = 0;
            int flag = 0;
            for ( m = 0; m< this._bGHandbook.Detail2.Count; m++)
            {
                sheet.Cells[m + count1 + 12, 6] = this._bGHandbook.Detail2[m].Id;
                sheet.Cells[m + count1 + 12, 8] = this._bGHandbook.Detail2[m].ProName;
                sheet.Cells[m + count1 + 12, 9] =this._bGHandbook.Detail2[m].Ljingliang.HasValue ?   this._bGHandbook.Detail2[m].Ljingliang.Value.ToString("0.00"):"";
                sheet.Cells[m + count1 + 12, 10] = this._bGHandbook.Detail2[m].LjingSunliang.HasValue ? this._bGHandbook.Detail2[m].LjingSunliang.Value.ToString("0.00") : "";

                sheet.Cells[m + count1 + 12, 11] = this._bGHandbook.Detail2[m].LLastjingSunliang.HasValue ? this._bGHandbook.Detail2[m].LLastjingSunliang.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 13] = this._bGHandbook.Detail2[m].Lchazhi.HasValue ? this._bGHandbook.Detail2[m].Lchazhi.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 14] = this._bGHandbook.Detail2[m].LPrice.HasValue ? this._bGHandbook.Detail2[m].LPrice.Value.ToString("0.######") : "";
                sheet.Cells[m + count1 + 12, 15] = this._bGHandbook.Detail2[m].Lmoney.HasValue ? this._bGHandbook.Detail2[m].Lmoney.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 16] = this._bGHandbook.Detail2[m].LbejinQuantity.HasValue ? this._bGHandbook.Detail2[m].LbejinQuantity.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 17] = this._bGHandbook.Detail2[m].LciciMoney.HasValue ? this._bGHandbook.Detail2[m].LciciMoney.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 18] = this._bGHandbook.Detail2[m].JinKouiMoney.HasValue ? this._bGHandbook.Detail2[m].JinKouiMoney.Value.ToString("0.######") : "";
                sheet.Cells[m + count1 + 12, 19] = this._bGHandbook.Detail2[m].LastMoney.HasValue ? this._bGHandbook.Detail2[m].LastMoney.Value.ToString("0.######") : ""; 


            }

            flag = m + count1 + 12 + 6;

                 sheet.Cells[ flag, 3] ="序号 ";
                 sheet.Cells[ flag, 4] ="原料";
                 sheet.Cells[ flag, 5] ="1备案量 ";
                 sheet.Cells[ flag,6] ="2已经进口 ";
                 sheet.Cells[ flag, 7] ="3转册";
                 sheet.Cells[ flag,8] ="4剩余量";
                 sheet.Cells[ flag, 9] ="5要进量";
                 sheet.Cells[ flag,10] ="6可进口数量";
                 sheet.Cells[ flag,11] ="7理论耗用";
                 sheet.Cells[ flag, 12] ="8";
                 sheet.Cells[ flag,13] ="9=2-7理论库存";
            
              


            for (int n = 0; n < this._bGHandbook.Detail2.Count; n++)
            {
                sheet.Cells[n + flag+1, 3] = this._bGHandbook.Detail2[n].Id;
                sheet.Cells[n + flag + 1, 4] = this._bGHandbook.Detail2[n].ProName;
                sheet.Cells[n + flag + 1, 5] = this._bGHandbook.Detail2[n].LjingSunliang.HasValue ? this._bGHandbook.Detail2[n].LjingSunliang.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 6] = this._bGHandbook.Detail2[n].LbejinQuantity.HasValue ? this._bGHandbook.Detail2[n].LbejinQuantity.Value.ToString("0.00") : "";

                sheet.Cells[n + flag + 1, 7] = this._bGHandbook.Detail2[n].ZhuanCeInQuantity.HasValue ? this._bGHandbook.Detail2[n].ZhuanCeInQuantity.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 8] = this._bGHandbook.Detail2[n].UpQuantity.HasValue ? this._bGHandbook.Detail2[n].UpQuantity.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 9] = this._bGHandbook.Detail2[n].YaoJInQuantity.HasValue ? this._bGHandbook.Detail2[n].YaoJInQuantity.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 10] = this._bGHandbook.Detail2[n].HaiKeJInQuantity.HasValue ? this._bGHandbook.Detail2[n].HaiKeJInQuantity.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 11] = this._bGHandbook.Detail2[n].LilunHaoYong.HasValue ? this._bGHandbook.Detail2[n].LilunHaoYong.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 13] = this._bGHandbook.Detail2[n].LilunStock.HasValue ? this._bGHandbook.Detail2[n].LilunStock.Value.ToString("0.00") : "";
               
            }



            string y = "L" + (this._bGHandbook.Detail1.Count + this._bGHandbook.Detail2.Count*2 + 200).ToString();

            Range oRange = sheet.get_Range("A1", y);
            oRange.WrapText = true;
            oRange.EntireRow.AutoFit();

            ex.setBorder(sheet, 1, 2, 12, this._bGHandbook.Detail1.Count + 6, 2);
            ex.setBorder(sheet, 6, this._bGHandbook.Detail1.Count + 11, 10, this._bGHandbook.Detail1.Count + 11 + this._bGHandbook.Detail2.Count, 2);
            ex.setBorder(sheet, 3, m + count1 + 12 + 6, 11, m + count1 + 12 + 6 + this._bGHandbook.Detail2.Count, 2);

            //宽度
            ex.SetWidth(sheet, "A:A", 7.13);
            ex.SetWidth(sheet, "B:B", 12.25);
            ex.SetWidth(sheet, "C:C", 12.50);
            ex.SetWidth(sheet, "D:D", 10.50);
            ex.SetWidth(sheet, "E:E", 11.88);
            ex.SetWidth(sheet, "F:F", 10.75);
            ex.SetWidth(sheet, "B:B", 13.38);
            ex.SetWidth(sheet, "G:G", 4.25);
            ex.SetWidth(sheet, "H:H", 17.00);
            ex.SetWidth(sheet, "I:I", 13.88);
            ex.SetWidth(sheet, "J:J", 12.50);
            ex.SetWidth(sheet, "K:K", 16.63);
            ex.SetWidth(sheet, "L:L", 5.75);

            //对齐方式
            ((Range)sheet.Columns["A:B", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["C:C", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Columns["D:G", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["H:H", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Columns["I:L", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Rows["2:2", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


            ex.app.Visible = true;
            ex.release_xlsObj();
            GC.Collect();

       

        }

        //备案变更 复制 数据 到新备案
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            Model.BGHandbook bghand = this._bGHandbook;
            bghand.BGHandbookId = this.bGHandbookManager.GetId(DateTime.Now);
            bghand.BGHandbookDate = DateTime.Now;
            bghand.AuditEmpId = null;
            bghand.AuditState = 1;
                bghand.UpdateTime=null;
                bghand.YouXiaoDate = DateTime.Now.Date.AddMonths(15);                                                           
                foreach (Model.BGHandbookDetail2 detail2 in bghand.Detail2)
                {                  
                    detail2.LLastjingSunliang = detail2.LjingSunliang;
                }
                this.action = "insert";
                this.Refresh();            
        }
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == gridColumnQuantity.Name || e.Column.Name == gridColumnBeeQuantity.Name || e.Column.Name == gridColumn13.Name || e.Column.Name == gridColumn14.Name)
            {

                if (e.Column.Name == gridColumnQuantity.Name)
                {
                    decimal quantity = decimal.Zero;      //备案

                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out quantity);

                    Model.BGHandbookDetail1 detail1 = bindingSource1.Current as Model.BGHandbookDetail1;
                    
                   foreach(Model.BGHandbookDetail1 de in   this._bGHandbook.Detail1.Where(d=>d.Id== detail1.Id &&!string.IsNullOrEmpty( d.Column1)).ToList<Model.BGHandbookDetail1>() )
                   {
                       de.Ljingliang= (double)this.GetDecimal(quantity*decimal.Parse( de.LjingQuantity.ToString()),2);
                       de.LjingSunliang = (double)this.GetDecimal(quantity * decimal.Parse(de.LjingQuantity.ToString()) / (1 - (decimal)de.Lsunhaolv * 0.01M), 2);
                       Detail2((bindingSource1.Current as Model.BGHandbookDetail1).LId);
                   
                   }
                
                }
                if (e.Column.Name == gridColumnBeeQuantity.Name)
                {
                    double beeQuantity = 0;      //已出数量
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out beeQuantity);

                    Model.BGHandbookDetail1 detail1 = bindingSource1.Current as Model.BGHandbookDetail1;
                    detail1.UpQuantity = detail1.Quantity -beeQuantity;

                  
                   IList<Model.BGHandbookDetail1> detail1List = this._bGHandbook.Detail1.Where(d => d.Id == (bindingSource1.Current as Model.BGHandbookDetail1).Id && !string.IsNullOrEmpty(d.Column1)).ToList<Model.BGHandbookDetail1>();
                    
                    Model.BGHandbookDetail2 detail2 = new  Model.BGHandbookDetail2();
                   foreach (Model.BGHandbookDetail1 detail1m in detail1List)
                   {
                     detail2=  this._bGHandbook.Detail2.Where(d => d.Id == detail1m.LId).ToList<Model.BGHandbookDetail2>()[0];
                     double? a = detail2.LilunHaoYong - detail1.LiLunHaoYongJingSun;
                     detail1.LiLunHaoYongJingSun = (double)GetDecimal((decimal)beeQuantity * (decimal)detail1m.LjingQuantity / (1 - (decimal)detail1m.Lsunhaolv * 0.01M), 2);
                     detail2.LilunHaoYong =a+ detail1.LiLunHaoYongJingSun ;
                     detail2.LilunStock = detail2.LbejinQuantity - detail2.LilunHaoYong;
                     detail1m.BeeQuantityTemp = beeQuantity;
                   }
                        
               
                  //  Detail2((bindingSource1.Current as Model.BGHandbookDetail1).LId);
                    ////////////////////////////////////////////////////计算理论损耗
               


                }
                if (e.Column.Name == gridColumn13.Name)  //净重/kg
                {

                    Model.BGHandbookDetail1 detail1 = this._bGHandbook.Detail1.Where(d => d.Id == (bindingSource1.Current as Model.BGHandbookDetail1).Id && !string.IsNullOrEmpty(d.ProName)).ToList<Model.BGHandbookDetail1>()[0];
                    decimal quantity = (decimal)detail1.Quantity;

                  this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn11,GetDecimal( quantity * (decimal)e.Value,2));
                  this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn12, GetDecimal(quantity * decimal.Parse(e.Value.ToString()) / (1 - (decimal)(bindingSource1.Current as Model.BGHandbookDetail1).Lsunhaolv * 0.01M), 2));
                  ////////////////计算理论损耗
                  Model.BGHandbookDetail1 curr= bindingSource1.Current as Model.BGHandbookDetail1;

  

                  double? beeQuantity = detail1.BeeQuantity;
               Model.BGHandbookDetail2   detail2 = this._bGHandbook.Detail2.Where(d => d.Id == curr.LId).ToList<Model.BGHandbookDetail2>()[0];
                  double? a = detail2.LilunHaoYong -curr.LiLunHaoYongJingSun  ;


                  curr.LiLunHaoYongJingSun = (double)GetDecimal((decimal)beeQuantity * (decimal)quantity / (1 - (decimal)curr.Lsunhaolv * 0.01M), 2);
                  detail2.LilunHaoYong = a + curr.LiLunHaoYongJingSun;
                  detail2.LilunStock = detail2.LbejinQuantity - detail2.LilunHaoYong;
                  Detail2((bindingSource1.Current as Model.BGHandbookDetail1).LId);
                }
                if (e.Column.Name == gridColumn14.Name)
                {
                    Model.BGHandbookDetail1 detail1 = this._bGHandbook.Detail1.Where(d => d.Id == (bindingSource1.Current as Model.BGHandbookDetail1).Id && !string.IsNullOrEmpty(d.ProName)).ToList<Model.BGHandbookDetail1>()[0];

                    decimal quantity = (decimal)detail1.Quantity;
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn12, GetDecimal(quantity * (decimal)(bindingSource1.Current as Model.BGHandbookDetail1).LjingQuantity / (1 - (decimal)e.Value * 0.01M), 2));

                    ////////////////计算理论损耗
                    Model.BGHandbookDetail1 curr = bindingSource1.Current as Model.BGHandbookDetail1;

                    double? beeQuantity = detail1.BeeQuantity;
                    Model.BGHandbookDetail2 detail2 = this._bGHandbook.Detail2.Where(d => d.Id == curr.LId).ToList<Model.BGHandbookDetail2>()[0];
                    double? a = detail2.LilunHaoYong - curr.LiLunHaoYongJingSun;

                    curr.LiLunHaoYongJingSun = (double)GetDecimal((decimal)beeQuantity * (decimal)curr.LjingQuantity / (1 - (decimal)curr.Lsunhaolv * 0.01M), 2);
                    detail2.LilunHaoYong = a + curr.LiLunHaoYongJingSun;
                    detail2.LilunStock = detail2.LbejinQuantity - detail2.LilunHaoYong;
                    Detail2((bindingSource1.Current as Model.BGHandbookDetail1).LId);
                } 

                this.gridControl1.RefreshDataSource();
               
            }
        }

        private void Detail2(int? lid)
        {
            IEnumerable<Model.BGHandbookDetail2> detail=this._bGHandbook.Detail2.Where(d => d.Id == lid);
            if(detail==null||detail.Count()==0) return;
            Model.BGHandbookDetail2 detail2 = detail.ToList<Model.BGHandbookDetail2>()[0];
            detail2.Ljingliang = (double)this._bGHandbook.Detail1.Where(d => d.LId == lid).Sum(d => d.Ljingliang);
            detail2.LjingSunliang = (double)this._bGHandbook.Detail1.Where(d => d.LId == lid).Sum(d => d.LjingSunliang);
            detail2.Lchazhi = detail2.LjingSunliang - detail2.LLastjingSunliang;
            detail2.Lmoney =GetDecimal( detail2.LPrice * (decimal)detail2.Lchazhi,2);
            detail2.LciciMoney = GetDecimal(detail2.LPrice * (decimal)detail2.LjingSunliang, 2);
            detail2.JinKouiMoney = GetDecimal(detail2.LPrice * (decimal)detail2.LbejinQuantity, 2);
            detail2.LastMoney = GetDecimal(detail2.LPrice * (decimal)detail2.LLastjingSunliang, 2);
            detail2.UpQuantity = detail2.LjingSunliang - detail2.LbejinQuantity - detail2.ZhuanCeInQuantity;
            detail2.HaiKeJInQuantity = detail2.UpQuantity - detail2.YaoJInQuantity;
          
           
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == gridColumnLast.Name || e.Column.Name == gridColumnPrice.Name || e.Column.Name == gridColumnZhuanCeIn.Name || e.Column.Name == gridColumnZhuanCeOut.Name || e.Column.Name == gridColumnZhuanRuShouCeId.Name)
            {

                if (e.Column.Name == gridColumnLast.Name)
                {

                    double lastquantity = 0;      //上次备案
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out lastquantity);

                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;
                    detail2.Lchazhi=detail2.LjingSunliang-lastquantity;

                    detail2.Lmoney = GetDecimal((decimal)detail2.Lchazhi * detail2.LPrice, 2);
                    detail2.LastMoney = GetDecimal((decimal)lastquantity*detail2.LPrice,2);
                }

                if (e.Column.Name == gridColumnPrice.Name) //修改单价
                {

                    decimal price = decimal.Zero;      //单价
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out price);

                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;
                    detail2.Lmoney = GetDecimal((decimal)detail2.Lchazhi * price, 2);

                    detail2.LciciMoney = GetDecimal((decimal)detail2.LjingSunliang * price, 2);

                    detail2.JinKouiMoney = GetDecimal((decimal)detail2.LbejinQuantity * price, 2);
                    detail2.LastMoney = GetDecimal((decimal)detail2.LLastjingSunliang * price, 2);
                }

                if (e.Column.Name == gridColumnbejinQuantity.Name) //修改已进口
                {

                    double bejinQuantity = 0;      //已进口
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out bejinQuantity);

                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;

                    detail2.UpQuantity = detail2.LjingSunliang - bejinQuantity - detail2.ZhuanCeInQuantity;
                    detail2.HaiKeJInQuantity = detail2.UpQuantity - detail2.YaoJInQuantity;
                    detail2.LilunStock = bejinQuantity - detail2.LilunHaoYong;
                    detail2.JinKouiMoney = GetDecimal((decimal)bejinQuantity * detail2.LPrice, 2);
                }
                if (e.Column.Name == gridColumnYaoJInQuantity.Name) //修改要进量  
                {

                    double yaoJInQuantity = 0;      //要进量
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out yaoJInQuantity);

                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;                
                    detail2.HaiKeJInQuantity = detail2.UpQuantity - detail2.YaoJInQuantity;                 
                 
                }
                if (e.Column.Name == gridColumnZhuanCeOut.Name || e.Column.Name == gridColumnZhuanRuShouCeId.Name) //是否更改转出到手册数量或者手册号
                {
                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;
                    flag = 1;
                    Detail2ZhjuanCe.Add(detail2);
                   
                }
                if (e.Column.Name == gridColumnZhuanCeIn.Name) //修改转入数量
                {
                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;
                    double quantity = 0;      //要进量
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out quantity);              
                
                    detail2.UpQuantity = detail2.LjingSunliang - detail2.LbejinQuantity-quantity;
                    detail2.HaiKeJInQuantity = detail2.UpQuantity - detail2.YaoJInQuantity;
                 
                }
                this.gridControl2.RefreshDataSource();

            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelClass.ExcelClass1 ex = new ExcelClass1();
            ex.Create();
            // ex.AddSheet("130101");
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)ex.app.Application.Worksheets.get_Item(1);
            sheet.Name = "理论耗用";
            ex.UniteCells(sheet, 1, 1, 1, 4);
            sheet.Cells[1, 1] = this._bGHandbook.Id;
        
          
            sheet.Cells[2, 1] = "成品项号";
            sheet.Cells[2, 2] = "客户型号";
            sheet.Cells[2, 3] = "成品规格";
            sheet.Cells[2, 4] = "成品数量";
            sheet.Cells[2, 5] = "已出数量";
            sheet.Cells[2, 6] = "剩余量";
            sheet.Cells[2, 7] = "料件项号";
            sheet.Cells[2, 8] = "原料";
            sheet.Cells[2, 9] = "净量";
            sheet.Cells[2, 10] = "净+损";
            sheet.Cells[2, 11] = "净KG";
            sheet.Cells[2, 12] = "损耗%";
            int count1 = this._bGHandbook.Detail1.Count;

            // ex.AddSheet("130101");
            for (int i =0; i < this._bGHandbook.Detail1.Count; i++)
            {
                sheet.Cells[i +3, 1] = this._bGHandbook.Detail1[i].Id;
                sheet.Cells[i + 3, 2] = this._bGHandbook.Detail1[i].CusProName;
                sheet.Cells[i +3, 3] = this._bGHandbook.Detail1[i].ProName;
                sheet.Cells[i + 3, 4] = this._bGHandbook.Detail1[i].BeeQuantity.HasValue ? this._bGHandbook.Detail1[i].BeeQuantity.Value.ToString("0.####") : "";
                sheet.Cells[i + 3, 5] = this._bGHandbook.Detail1[i].BeeQuantity.HasValue ? this._bGHandbook.Detail1[i].BeeQuantity.Value.ToString("0.####") : "";
                sheet.Cells[i + 3, 6] = 0;
                sheet.Cells[i + 3, 7] = this._bGHandbook.Detail1[i].LId;
                sheet.Cells[i + 3, 8] = this._bGHandbook.Detail1[i].Column1;
                sheet.Cells[i +3, 9] = this._bGHandbook.Detail1[i].LiLunHaoYongJing.HasValue ? this._bGHandbook.Detail1[i].LiLunHaoYongJing.Value.ToString("0.00") : "";
                sheet.Cells[i + 3, 10] = this._bGHandbook.Detail1[i].LiLunHaoYongJingSun.HasValue ? this._bGHandbook.Detail1[i].LiLunHaoYongJingSun.Value.ToString("0.00") : "";
                sheet.Cells[i + 3, 11] = this._bGHandbook.Detail1[i].LjingQuantity.HasValue ? this._bGHandbook.Detail1[i].LjingQuantity.Value.ToString() : "";
                sheet.Cells[i + 3, 12] = this._bGHandbook.Detail1[i].Lsunhaolv.HasValue ? this._bGHandbook.Detail1[i].Lsunhaolv.Value.ToString() : "";

            }
            int a = this._bGHandbook.Detail1.Count+7;

            sheet.Cells[a, 3] = "合计";
            string heji = this._bGHandbook.Detail1.Sum(bee => bee.BeeQuantity).ToString();
            sheet.Cells[a, 4] =
            sheet.Cells[a, 5] = heji;
            sheet.Cells[a + 3, 8] = "料件表";
            sheet.Cells[a + 5,6] = "序号";
            sheet.Cells[a + 5, 8] = "原料";
            sheet.Cells[a + 5, 9] = "总净";
            sheet.Cells[a + 5, 10] = "总净+损";
            sheet.Cells[a + 5, 11] = "理论损耗/废料";

            for (int i = 0; i < this._bGHandbook.Detail2.Count; i++)
            {

                sheet.Cells[a + 6 + i, 6] = this._bGHandbook.Detail2[i].Id;
                sheet.Cells[a + 6 + i, 8] = this._bGHandbook.Detail2[i].ProName;
                double haoyongjing = this._bGHandbook.Detail1.Where(p => p.Id == this._bGHandbook.Detail2[i].Id).Sum(b => b.LiLunHaoYongJing).Value;
                sheet.Cells[a + 6 + i, 9] = haoyongjing.ToString();
                double haoyong= this._bGHandbook.Detail2[i].LilunHaoYong.HasValue? this._bGHandbook.Detail2[i].LilunHaoYong.Value:0;
                sheet.Cells[a + 6 + i, 10] = haoyong.ToString();
                sheet.Cells[a + 6 + i, 11] = haoyong - haoyongjing;
            }

            string y ="L"+ (a + 5 + this._bGHandbook.Detail2.Count + 100).ToString();

            Range   oRange =sheet.get_Range("A1", y);
            oRange.WrapText = true;
            oRange.EntireRow.AutoFit();

            ex.setBorder(sheet, 1, 2, 12, a, 2);
            ex.setBorder(sheet, 6,a + 5,  11,  a + 5 + this._bGHandbook.Detail2.Count,2);

            //宽度
            ex.SetWidth(sheet, "A:A", 5.88);
            ex.SetWidth(sheet, "B:B", 13.38);
            ex.SetWidth(sheet, "C:C", 11.63);
            ex.SetWidth(sheet, "D:D", 10.50);
            ex.SetWidth(sheet, "E:E", 11.88);
            ex.SetWidth(sheet, "F:F", 10.75);
            ex.SetWidth(sheet, "B:B", 13.38);
            ex.SetWidth(sheet, "G:G", 4.25);
            ex.SetWidth(sheet, "H:H", 17.00);
            ex.SetWidth(sheet, "I:I", 13.88);
            ex.SetWidth(sheet, "J:J", 12.50);
            ex.SetWidth(sheet, "K:K", 13.63);
            ex.SetWidth(sheet, "L:L", 5.75);

            //对齐方式
            ((Range)sheet.Columns["A:B", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["C:C", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Columns["D:G", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["H:H", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Columns["I:L", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Rows["2:2", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

           
            ex.app.Visible = true;
            ex.release_xlsObj();
            GC.Collect();

           

        }

    }
}