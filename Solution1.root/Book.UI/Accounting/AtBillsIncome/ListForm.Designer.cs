namespace Book.UI.Accounting.AtBillsIncome
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bindingSourceSupplier = new System.Windows.Forms.BindingSource(this.components);
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bindingSourceCustomer = new System.Windows.Forms.BindingSource(this.components);
            this.repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSearch,
            this.barStaticItem2});
            this.barManager1.MaxItemId = 15;
            // 
            // bar1
            // 
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSearch)});
            this.bar1.OptionsBar.UseWholeRow = true;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // layoutControl1
            // 
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Controls.SetChildIndex(this.gridControl1, 0);
            // 
            // gridControl1
            // 
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemLookUpEdit1,
            this.repositoryItemTrackBar1,
            this.repositoryItemLookUpEdit2});
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // barSearch
            // 
            resources.ApplyResources(this.barSearch, "barSearch");
            this.barSearch.Id = 13;
            this.barSearch.Name = "barSearch";
            this.barSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSearch_ItemClick);
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn2.FieldName = "IncomeCategory";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemComboBox1
            // 
            resources.ApplyResources(this.repositoryItemComboBox1, "repositoryItemComboBox1");
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemComboBox1.Buttons"))))});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            resources.GetString("repositoryItemComboBox1.Items"),
            resources.GetString("repositoryItemComboBox1.Items1")});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "TheOpenDate";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "Maturity";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "NotesMoney";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.FieldName = "Bank";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn7
            // 
            resources.ApplyResources(this.gridColumn7, "gridColumn7");
            this.gridColumn7.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.gridColumn7.FieldName = "PassingObject";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemLookUpEdit1
            // 
            resources.ApplyResources(this.repositoryItemLookUpEdit1, "repositoryItemLookUpEdit1");
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemLookUpEdit1.Buttons"))))});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("repositoryItemLookUpEdit1.Columns"), resources.GetString("repositoryItemLookUpEdit1.Columns1")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("repositoryItemLookUpEdit1.Columns2"), resources.GetString("repositoryItemLookUpEdit1.Columns3"))});
            this.repositoryItemLookUpEdit1.DataSource = this.bindingSourceSupplier;
            this.repositoryItemLookUpEdit1.DisplayMember = "SupplierFullName";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.ValueMember = "SupplierId";
            // 
            // gridColumn8
            // 
            resources.ApplyResources(this.gridColumn8, "gridColumn8");
            this.gridColumn8.ColumnEdit = this.repositoryItemLookUpEdit2;
            this.gridColumn8.FieldName = "PassingObject";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemLookUpEdit2
            // 
            resources.ApplyResources(this.repositoryItemLookUpEdit2, "repositoryItemLookUpEdit2");
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemLookUpEdit2.Buttons"))))});
            this.repositoryItemLookUpEdit2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("repositoryItemLookUpEdit2.Columns"), resources.GetString("repositoryItemLookUpEdit2.Columns1")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("repositoryItemLookUpEdit2.Columns2"), resources.GetString("repositoryItemLookUpEdit2.Columns3"))});
            this.repositoryItemLookUpEdit2.DataSource = this.bindingSourceCustomer;
            this.repositoryItemLookUpEdit2.DisplayMember = "CustomerFullName";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.ValueMember = "CustomerId";
            // 
            // repositoryItemTrackBar1
            // 
            this.repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
            // 
            // barStaticItem2
            // 
            resources.ApplyResources(this.barStaticItem2, "barStaticItem2");
            this.barStaticItem2.Id = 14;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("labelControl1.Appearance.BackColor")));
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl1);
            this.Name = "ListForm";
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem barSearch;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.BindingSource bindingSourceSupplier;
        private System.Windows.Forms.BindingSource bindingSourceCustomer;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}