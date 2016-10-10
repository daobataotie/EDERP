namespace Book.UI.produceManager.PCPGOnlineCheck
{
    partial class ListForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.barBtnSearch = new DevExpress.XtraBars.BarButtonItem();
            this.PCPGOnlineCheckId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PCPGOnlineCheckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.InvoiceCusXOId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.attrCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.che_HomeMade = new DevExpress.XtraEditors.CheckEdit();
            this.che_Out = new DevExpress.XtraEditors.CheckEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.che_HomeMade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.che_Out.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnSearch});
            this.barManager1.MaxItemId = 14;
            // 
            // bar1
            // 
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBtnSearch, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.PCPGOnlineCheckId,
            this.PCPGOnlineCheckDate,
            this.InvoiceCusXOId,
            this.attrCustomer,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // barBtnSearch
            // 
            resources.ApplyResources(this.barBtnSearch, "barBtnSearch");
            this.barBtnSearch.Id = 13;
            this.barBtnSearch.ImageIndex = 3;
            this.barBtnSearch.Name = "barBtnSearch";
            this.barBtnSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSearch_ItemClick);
            // 
            // PCPGOnlineCheckId
            // 
            resources.ApplyResources(this.PCPGOnlineCheckId, "PCPGOnlineCheckId");
            this.PCPGOnlineCheckId.FieldName = "PCPGOnlineCheckId";
            this.PCPGOnlineCheckId.Name = "PCPGOnlineCheckId";
            this.PCPGOnlineCheckId.OptionsColumn.AllowEdit = false;
            // 
            // PCPGOnlineCheckDate
            // 
            resources.ApplyResources(this.PCPGOnlineCheckDate, "PCPGOnlineCheckDate");
            this.PCPGOnlineCheckDate.FieldName = "PCPGOnlineCheckDate";
            this.PCPGOnlineCheckDate.Name = "PCPGOnlineCheckDate";
            this.PCPGOnlineCheckDate.OptionsColumn.AllowEdit = false;
            // 
            // InvoiceCusXOId
            // 
            resources.ApplyResources(this.InvoiceCusXOId, "InvoiceCusXOId");
            this.InvoiceCusXOId.FieldName = "InvoiceCusXOId2";
            this.InvoiceCusXOId.Name = "InvoiceCusXOId";
            this.InvoiceCusXOId.OptionsColumn.AllowEdit = false;
            // 
            // attrCustomer
            // 
            resources.ApplyResources(this.attrCustomer, "attrCustomer");
            this.attrCustomer.FieldName = "CustomerShortName";
            this.attrCustomer.Name = "attrCustomer";
            this.attrCustomer.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "PCPGOnlineCheckDate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "ProductName";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "EmployeeName";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "DescTime";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // che_HomeMade
            // 
            resources.ApplyResources(this.che_HomeMade, "che_HomeMade");
            this.che_HomeMade.MenuManager = this.barManager1;
            this.che_HomeMade.Name = "che_HomeMade";
            this.che_HomeMade.Properties.Caption = resources.GetString("che_HomeMade.Properties.Caption");
            this.che_HomeMade.CheckedChanged += new System.EventHandler(this.che_HomeMade_CheckedChanged);
            // 
            // che_Out
            // 
            resources.ApplyResources(this.che_Out, "che_Out");
            this.che_Out.MenuManager = this.barManager1;
            this.che_Out.Name = "che_Out";
            this.che_Out.Properties.Caption = resources.GetString("che_Out.Properties.Caption");
            this.che_Out.CheckedChanged += new System.EventHandler(this.che_Out_CheckedChanged);
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "BusinessHoursName";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.FieldName = "FromId";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.che_Out);
            this.Controls.Add(this.che_HomeMade);
            this.Name = "ListForm";
            this.Controls.SetChildIndex(this.che_HomeMade, 0);
            this.Controls.SetChildIndex(this.che_Out, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.che_HomeMade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.che_Out.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem barBtnSearch;
        private DevExpress.XtraGrid.Columns.GridColumn PCPGOnlineCheckId;
        private DevExpress.XtraGrid.Columns.GridColumn PCPGOnlineCheckDate;
        private DevExpress.XtraGrid.Columns.GridColumn InvoiceCusXOId;
        private DevExpress.XtraGrid.Columns.GridColumn attrCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.CheckEdit che_HomeMade;
        private DevExpress.XtraEditors.CheckEdit che_Out;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}
