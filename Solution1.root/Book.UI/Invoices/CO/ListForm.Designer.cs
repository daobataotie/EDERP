namespace Book.UI.Invoices.CO
{
    partial class ListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colInvoiceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee0Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceAbstract = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_ListPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPrintList = new DevExpress.XtraBars.BarButtonItem();
            this.checkEditAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnPrintList});
            this.barManager1.MaxItemId = 13;
            // 
            // bar1
            // 
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrintList)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.colInvoiceId,
            this.colCompanyId,
            this.colEmployee0Id,
            this.colInvoiceDate,
            this.colInvoiceStatus,
            this.colInvoiceAbstract,
            this.colInvoiceNote,
            this.colInvoiceTotal,
            this.gridColumn1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.ColumnEdit = this.repositoryItemCheckEdit2;
            this.gridColumn2.FieldName = "IsChecked";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // repositoryItemCheckEdit2
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit2, "repositoryItemCheckEdit2");
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colInvoiceId
            // 
            this.colInvoiceId.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceId.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceId, "colInvoiceId");
            this.colInvoiceId.FieldName = "InvoiceId";
            this.colInvoiceId.Name = "colInvoiceId";
            this.colInvoiceId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colCompanyId
            // 
            this.colCompanyId.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompanyId.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colCompanyId, "colCompanyId");
            this.colCompanyId.FieldName = "Supplier";
            this.colCompanyId.Name = "colCompanyId";
            this.colCompanyId.OptionsColumn.AllowEdit = false;
            // 
            // colEmployee0Id
            // 
            this.colEmployee0Id.AppearanceCell.Options.UseTextOptions = true;
            this.colEmployee0Id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployee0Id.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployee0Id.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colEmployee0Id, "colEmployee0Id");
            this.colEmployee0Id.FieldName = "Employee0";
            this.colEmployee0Id.Name = "colEmployee0Id";
            this.colEmployee0Id.OptionsColumn.AllowEdit = false;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInvoiceDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colInvoiceDate, "colInvoiceDate");
            this.colInvoiceDate.FieldName = "InvoiceDate";
            this.colInvoiceDate.Name = "colInvoiceDate";
            this.colInvoiceDate.OptionsColumn.AllowEdit = false;
            // 
            // colInvoiceStatus
            // 
            this.colInvoiceStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInvoiceStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colInvoiceStatus, "colInvoiceStatus");
            this.colInvoiceStatus.FieldName = "InvoiceStatusDesc";
            this.colInvoiceStatus.Name = "colInvoiceStatus";
            // 
            // colInvoiceAbstract
            // 
            this.colInvoiceAbstract.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceAbstract.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceAbstract.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceAbstract.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceAbstract, "colInvoiceAbstract");
            this.colInvoiceAbstract.FieldName = "InvoiceAbstract";
            this.colInvoiceAbstract.Name = "colInvoiceAbstract";
            this.colInvoiceAbstract.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceAbstract.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceAbstract.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceNote
            // 
            this.colInvoiceNote.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceNote, "colInvoiceNote");
            this.colInvoiceNote.FieldName = "InvoiceNote";
            this.colInvoiceNote.Name = "colInvoiceNote";
            this.colInvoiceNote.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceNote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceNote.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceTotal
            // 
            this.colInvoiceTotal.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceTotal, "colInvoiceTotal");
            this.colInvoiceTotal.FieldName = "InvoiceTotal";
            this.colInvoiceTotal.Name = "colInvoiceTotal";
            this.colInvoiceTotal.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn1.FieldName = "IsClose";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // repositoryItemCheckEdit1
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Tag = "ToCG";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barBtn_ListPrint
            // 
            resources.ApplyResources(this.barBtn_ListPrint, "barBtn_ListPrint");
            this.barBtn_ListPrint.Id = 12;
            this.barBtn_ListPrint.Name = "barBtn_ListPrint";
            this.barBtn_ListPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ListPrint_ItemClick);
            // 
            // barBtnPrintList
            // 
            resources.ApplyResources(this.barBtnPrintList, "barBtnPrintList");
            this.barBtnPrintList.Id = 12;
            this.barBtnPrintList.Name = "barBtnPrintList";
            this.barBtnPrintList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ListPrint_ItemClick);
            // 
            // checkEditAll
            // 
            resources.ApplyResources(this.checkEditAll, "checkEditAll");
            this.checkEditAll.MenuManager = this.barManager1;
            this.checkEditAll.Name = "checkEditAll";
            this.checkEditAll.Properties.Caption = resources.GetString("checkEditAll.Properties.Caption");
            this.checkEditAll.CheckedChanged += new System.EventHandler(this.checkEditAll_CheckedChanged);
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkEditAll);
            this.Controls.Add(this.gridControl1);
            this.Name = "ListForm";
            this.Load += new System.EventHandler(this.ListForm_Load);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            this.Controls.SetChildIndex(this.checkEditAll, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceId;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee0Id;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceAbstract;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNote;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceTotal;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarButtonItem barBtn_ListPrint;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraBars.BarButtonItem barBtnPrintList;
        private DevExpress.XtraEditors.CheckEdit checkEditAll;
    }
}