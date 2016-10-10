namespace Book.UI.Accounting.AtSummon
{
    partial class ConditionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.nccEndSubject = new Book.UI.Invoices.NewChooseContorl();
            this.nccStartSubject = new Book.UI.Invoices.NewChooseContorl();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.textEditEndId = new DevExpress.XtraEditors.TextEdit();
            this.textEditStartId = new DevExpress.XtraEditors.TextEdit();
            this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
            this.dateEditStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditEndId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditStartId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.nccEndSubject);
            this.layoutControl1.Controls.Add(this.nccStartSubject);
            this.layoutControl1.Controls.Add(this.simpleButtonCancel);
            this.layoutControl1.Controls.Add(this.simpleButtonOK);
            this.layoutControl1.Controls.Add(this.textEditEndId);
            this.layoutControl1.Controls.Add(this.textEditStartId);
            this.layoutControl1.Controls.Add(this.dateEditEnd);
            this.layoutControl1.Controls.Add(this.dateEditStart);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // nccEndSubject
            // 
            this.nccEndSubject.EditValue = null;
            resources.ApplyResources(this.nccEndSubject, "nccEndSubject");
            this.nccEndSubject.Name = "nccEndSubject";
            // 
            // nccStartSubject
            // 
            this.nccStartSubject.EditValue = null;
            resources.ApplyResources(this.nccStartSubject, "nccStartSubject");
            this.nccStartSubject.Name = "nccStartSubject";
            this.nccStartSubject.EditValueChanged += new System.EventHandler(this.nccStartSubject_EditValueChanged);
            // 
            // simpleButtonCancel
            // 
            resources.ApplyResources(this.simpleButtonCancel, "simpleButtonCancel");
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.StyleController = this.layoutControl1;
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonOK
            // 
            resources.ApplyResources(this.simpleButtonOK, "simpleButtonOK");
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.StyleController = this.layoutControl1;
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // textEditEndId
            // 
            resources.ApplyResources(this.textEditEndId, "textEditEndId");
            this.textEditEndId.Name = "textEditEndId";
            this.textEditEndId.StyleController = this.layoutControl1;
            // 
            // textEditStartId
            // 
            resources.ApplyResources(this.textEditStartId, "textEditStartId");
            this.textEditStartId.Name = "textEditStartId";
            this.textEditStartId.StyleController = this.layoutControl1;
            // 
            // dateEditEnd
            // 
            this.dateEditEnd.EditValue = null;
            resources.ApplyResources(this.dateEditEnd, "dateEditEnd");
            this.dateEditEnd.Name = "dateEditEnd";
            this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditEnd.Properties.Buttons"))))});
            this.dateEditEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditEnd.StyleController = this.layoutControl1;
            // 
            // dateEditStart
            // 
            this.dateEditStart.EditValue = null;
            resources.ApplyResources(this.dateEditStart, "dateEditStart");
            this.dateEditStart.Name = "dateEditStart";
            this.dateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditStart.Properties.Buttons"))))});
            this.dateEditStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditStart.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(494, 139);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateEditStart;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(238, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEditEnd;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(238, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(236, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditStartId;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(238, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEditEndId;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(238, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(236, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonOK;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 93);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(237, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButtonCancel;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(237, 93);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(237, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 75);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(474, 18);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.nccStartSubject;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(238, 25);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.nccEndSubject;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(238, 50);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(236, 25);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(60, 14);
            // 
            // ConditionForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ConditionForm";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditEndId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditStartId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit textEditEndId;
        private DevExpress.XtraEditors.TextEdit textEditStartId;
        private DevExpress.XtraEditors.DateEdit dateEditEnd;
        private DevExpress.XtraEditors.DateEdit dateEditStart;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Book.UI.Invoices.NewChooseContorl nccEndSubject;
        private Book.UI.Invoices.NewChooseContorl nccStartSubject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}