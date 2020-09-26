using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Employees
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：OnListForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-23
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class OnListForm : Settings.BasicData.BaseListForm
    {
        BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        BL.DepartmentManager deptnamanager = new Book.BL.DepartmentManager();
        public OnListForm()
        {
            InitializeComponent();
            this.barManager1.Bars[0].Visible = false;

        }
        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = this.employeeManager.SelectOnActive();
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditForm._employee = this.bindingSource1.Current as Model.Employee;
            this.DialogResult = DialogResult.OK;
        }

        private void OnListForm_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = deptnamanager.Select();
        }

        private void bar_ExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "請選擇保存路徑";
            sfd.AddExtension = true;
            sfd.DefaultExt = ".xlsx";
            sfd.Filter = "Excel文件(*.xlsx)|*.xlsx";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.gridView1.OptionsPrint.AutoWidth = false;
                this.gridView1.OptionsPrint.PrintFilterInfo = true;        //只導處篩選後的數據

                this.gridView1.ExportToXlsx(sfd.FileName, new DevExpress.XtraPrinting.XlsxExportOptions { SheetName = "在職人員一覽表", ShowGridLines = true });

                MessageBox.Show("導出成功！", "導出結果", MessageBoxButtons.OK);
            }

        }
    }
}