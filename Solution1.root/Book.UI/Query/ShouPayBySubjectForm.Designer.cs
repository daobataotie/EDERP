namespace Book.UI.Query
{
    partial class ShouPayBySubjectForm
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.lue_Supplier = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSourceSupplier = new System.Windows.Forms.BindingSource(this.components);
            this.date_End = new DevExpress.XtraEditors.DateEdit();
            this.date_Start = new DevExpress.XtraEditors.DateEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmb_Subject = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bindingSourceSubject = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lue_Supplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_End.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Start.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Start.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_Subject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSubject)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Search);
            this.layoutControl1.Controls.Add(this.lue_Supplier);
            this.layoutControl1.Controls.Add(this.date_End);
            this.layoutControl1.Controls.Add(this.date_Start);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.cmb_Subject);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(590, 368);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(489, 12);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(89, 22);
            this.btn_Search.StyleController = this.layoutControl1;
            this.btn_Search.TabIndex = 9;
            this.btn_Search.Text = "查詢";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // lue_Supplier
            // 
            this.lue_Supplier.Location = new System.Drawing.Point(315, 38);
            this.lue_Supplier.Name = "lue_Supplier";
            this.lue_Supplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.lue_Supplier.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierFullName", "供應商")});
            this.lue_Supplier.Properties.DataSource = this.bindingSourceSupplier;
            this.lue_Supplier.Properties.NullText = "";
            this.lue_Supplier.Properties.ValueMember = "SupplierId";
            this.lue_Supplier.Size = new System.Drawing.Size(263, 21);
            this.lue_Supplier.StyleController = this.layoutControl1;
            this.lue_Supplier.TabIndex = 8;
            this.lue_Supplier.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lue_Supplier_ButtonClick);
            // 
            // date_End
            // 
            this.date_End.EditValue = null;
            this.date_End.Location = new System.Drawing.Point(315, 12);
            this.date_End.Name = "date_End";
            this.date_End.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_End.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.date_End.Size = new System.Drawing.Size(170, 21);
            this.date_End.StyleController = this.layoutControl1;
            this.date_End.TabIndex = 6;
            // 
            // date_Start
            // 
            this.date_Start.EditValue = null;
            this.date_Start.Location = new System.Drawing.Point(76, 12);
            this.date_Start.Name = "date_Start";
            this.date_Start.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_Start.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.date_Start.Size = new System.Drawing.Size(171, 21);
            this.date_Start.StyleController = this.layoutControl1;
            this.date_Start.TabIndex = 5;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Location = new System.Drawing.Point(12, 63);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1});
            this.gridControl1.Size = new System.Drawing.Size(566, 293);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "採購進庫單號";
            this.gridColumn1.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.gridColumn1.FieldName = "InvoiceId";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.SummaryItem.DisplayFormat = "汇总：";
            this.gridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 122;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.Click += new System.EventHandler(this.repositoryItemHyperLinkEdit1_Click);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "單據日期";
            this.gridColumn2.FieldName = "InvoiceDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 93;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "金額";
            this.gridColumn3.DisplayFormat.FormatString = "0.###";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "InvoiceHeji";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.SummaryItem.DisplayFormat = "{0:0.###}";
            this.gridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 95;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "稅額";
            this.gridColumn4.DisplayFormat.FormatString = "0.###";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "InvoiceTax";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.SummaryItem.DisplayFormat = "{0:0.###}";
            this.gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "折讓/折扣";
            this.gridColumn5.DisplayFormat.FormatString = "0.###";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "InvoiceAllowance";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.SummaryItem.DisplayFormat = "{0:0.###}";
            this.gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 66;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "合計";
            this.gridColumn6.DisplayFormat.FormatString = "0.###";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "InvoiceTotal";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.SummaryItem.DisplayFormat = "{0:0.###}";
            this.gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 97;
            // 
            // cmb_Subject
            // 
            this.cmb_Subject.Location = new System.Drawing.Point(76, 38);
            this.cmb_Subject.Name = "cmb_Subject";
            this.cmb_Subject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_Subject.Properties.PopupSizeable = true;
            this.cmb_Subject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_Subject.Size = new System.Drawing.Size(171, 21);
            this.cmb_Subject.StyleController = this.layoutControl1;
            this.cmb_Subject.TabIndex = 7;
            this.cmb_Subject.SelectedIndexChanged += new System.EventHandler(this.cmb_Subject_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(590, 368);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 51);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(570, 297);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.date_Start;
            this.layoutControlItem2.CustomizationFormText = "開始日期：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(239, 26);
            this.layoutControlItem2.Text = "開始日期：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.date_End;
            this.layoutControlItem3.CustomizationFormText = "結束日期：";
            this.layoutControlItem3.Location = new System.Drawing.Point(239, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(238, 26);
            this.layoutControlItem3.Text = "結束日期：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmb_Subject;
            this.layoutControlItem4.CustomizationFormText = "科目：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(239, 25);
            this.layoutControlItem4.Text = "科目：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lue_Supplier;
            this.layoutControlItem5.CustomizationFormText = "供應商：";
            this.layoutControlItem5.Location = new System.Drawing.Point(239, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(331, 25);
            this.layoutControlItem5.Text = "供應商：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_Search;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(477, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // ShouPayBySubjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 368);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ShouPayBySubjectForm";
            this.Text = "應付帳款(按科目)";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lue_Supplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_End.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_End.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Start.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Start.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_Subject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSubject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LookUpEdit lue_Supplier;
        private DevExpress.XtraEditors.DateEdit date_End;
        private DevExpress.XtraEditors.DateEdit date_Start;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.BindingSource bindingSourceSubject;
        private System.Windows.Forms.BindingSource bindingSourceSupplier;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.SimpleButton btn_Search;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_Subject;

    }
}