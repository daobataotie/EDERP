namespace Book.UI.Accounting.YPFP
{
    partial class EditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.spe_TotalMoney = new DevExpress.XtraEditors.SpinEdit();
            this.spe_Tax = new DevExpress.XtraEditors.SpinEdit();
            this.spe_Money = new DevExpress.XtraEditors.SpinEdit();
            this.nccSupplier = new Book.UI.Invoices.NewChooseContorl();
            this.date_YFFP = new DevExpress.XtraEditors.DateEdit();
            this.txt_FPId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bindingSourceFPHeader = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spe_TotalMoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spe_Tax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spe_Money.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_YFFP.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_YFFP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FPId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFPHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1});
            this.barManager1.MaxItemId = 16;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // bar1
            // 
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar1.OptionsBar.UseWholeRow = true;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spe_TotalMoney);
            this.layoutControl1.Controls.Add(this.spe_Tax);
            this.layoutControl1.Controls.Add(this.spe_Money);
            this.layoutControl1.Controls.Add(this.nccSupplier);
            this.layoutControl1.Controls.Add(this.date_YFFP);
            this.layoutControl1.Controls.Add(this.txt_FPId);
            this.layoutControl1.Controls.Add(this.lookUpEdit1);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // spe_TotalMoney
            // 
            resources.ApplyResources(this.spe_TotalMoney, "spe_TotalMoney");
            this.spe_TotalMoney.MenuManager = this.barManager1;
            this.spe_TotalMoney.Name = "spe_TotalMoney";
            this.spe_TotalMoney.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spe_TotalMoney.Properties.ReadOnly = true;
            this.spe_TotalMoney.StyleController = this.layoutControl1;
            // 
            // spe_Tax
            // 
            resources.ApplyResources(this.spe_Tax, "spe_Tax");
            this.spe_Tax.MenuManager = this.barManager1;
            this.spe_Tax.Name = "spe_Tax";
            this.spe_Tax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spe_Tax.StyleController = this.layoutControl1;
            this.spe_Tax.EditValueChanged += new System.EventHandler(this.spe_Tax_EditValueChanged);
            // 
            // spe_Money
            // 
            resources.ApplyResources(this.spe_Money, "spe_Money");
            this.spe_Money.MenuManager = this.barManager1;
            this.spe_Money.Name = "spe_Money";
            this.spe_Money.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spe_Money.StyleController = this.layoutControl1;
            this.spe_Money.EditValueChanged += new System.EventHandler(this.spe_Money_EditValueChanged);
            // 
            // nccSupplier
            // 
            this.nccSupplier.EditValue = null;
            resources.ApplyResources(this.nccSupplier, "nccSupplier");
            this.nccSupplier.Name = "nccSupplier";
            // 
            // date_YFFP
            // 
            this.date_YFFP.EditValue = null;
            resources.ApplyResources(this.date_YFFP, "date_YFFP");
            this.date_YFFP.MenuManager = this.barManager1;
            this.date_YFFP.Name = "date_YFFP";
            this.date_YFFP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("date_YFFP.Properties.Buttons"))))});
            this.date_YFFP.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.date_YFFP.StyleController = this.layoutControl1;
            // 
            // txt_FPId
            // 
            resources.ApplyResources(this.txt_FPId, "txt_FPId");
            this.txt_FPId.MenuManager = this.barManager1;
            this.txt_FPId.Name = "txt_FPId";
            this.txt_FPId.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(837, 130);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.date_YFFP;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(408, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txt_FPId;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(408, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(409, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.nccSupplier;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(408, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spe_Money;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(408, 25);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.spe_Tax;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(408, 50);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(409, 25);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.spe_TotalMoney;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 75);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(408, 25);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(408, 75);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(409, 25);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 100);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(817, 10);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 15;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // lookUpEdit1
            // 
            resources.ApplyResources(this.lookUpEdit1, "lookUpEdit1");
            this.lookUpEdit1.MenuManager = this.barManager1;
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lookUpEdit1.Properties.Buttons"))))});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEdit1.Properties.Columns"), resources.GetString("lookUpEdit1.Properties.Columns1"))});
            this.lookUpEdit1.Properties.DataSource = this.bindingSourceFPHeader;
            this.lookUpEdit1.Properties.DisplayMember = "CompanyName";
            this.lookUpEdit1.Properties.NullText = resources.GetString("lookUpEdit1.Properties.NullText");
            this.lookUpEdit1.Properties.ValueMember = "CompanyId";
            this.lookUpEdit1.StyleController = this.layoutControl1;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lookUpEdit1;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(408, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(409, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // EditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "EditForm";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spe_TotalMoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spe_Tax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spe_Money.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_YFFP.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_YFFP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FPId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFPHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SpinEdit spe_TotalMoney;
        private DevExpress.XtraEditors.SpinEdit spe_Tax;
        private DevExpress.XtraEditors.SpinEdit spe_Money;
        private Book.UI.Invoices.NewChooseContorl nccSupplier;
        private DevExpress.XtraEditors.DateEdit date_YFFP;
        private DevExpress.XtraEditors.TextEdit txt_FPId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private System.Windows.Forms.BindingSource bindingSourceFPHeader;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}