namespace Book.UI.Accounting.Report
{
    partial class ConditionGeneralAccountChooseForm
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditDepositSubject = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepositSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(109, 43);
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            // 
            // dateEditEndDate
            // 
            this.dateEditEndDate.EditValue = new System.DateTime(2011, 2, 19, 0, 0, 0, 0);
            this.dateEditEndDate.Location = new System.Drawing.Point(397, 40);
            this.dateEditEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditEndDate.Size = new System.Drawing.Size(100, 21);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(322, 43);
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            // 
            // dateEditStartDate
            // 
            this.dateEditStartDate.EditValue = new System.DateTime(2011, 2, 19, 0, 0, 0, 0);
            this.dateEditStartDate.Location = new System.Drawing.Point(213, 40);
            this.dateEditStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditStartDate.Size = new System.Drawing.Size(100, 21);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(332, 151);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(425, 151);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(109, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "总分类账日期区间";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(109, 77);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "科目编号区间";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(140, 103);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(16, 14);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "自:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(354, 103);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = "至：";
            // 
            // lookUpEditDepositSubject
            // 
            this.lookUpEditDepositSubject.Location = new System.Drawing.Point(213, 100);
            this.lookUpEditDepositSubject.Name = "lookUpEditDepositSubject";
            this.lookUpEditDepositSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditDepositSubject.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubjectId", "科目編號"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubjectName", "科目名稱")});
            this.lookUpEditDepositSubject.Properties.DataSource = this.bindingSource1;
            this.lookUpEditDepositSubject.Properties.DisplayMember = "SubjectId";
            this.lookUpEditDepositSubject.Properties.NullText = "";
            this.lookUpEditDepositSubject.Properties.ValueMember = "SubjectId";
            this.lookUpEditDepositSubject.Size = new System.Drawing.Size(100, 21);
            this.lookUpEditDepositSubject.TabIndex = 21;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(397, 100);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubjectId", "科目編號"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubjectName", "科目名稱")});
            this.lookUpEdit1.Properties.DataSource = this.bindingSource1;
            this.lookUpEdit1.Properties.DisplayMember = "SubjectId";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ValueMember = "SubjectId";
            this.lookUpEdit1.Size = new System.Drawing.Size(100, 21);
            this.lookUpEdit1.TabIndex = 22;
            // 
            // ConditionGeneralAccountChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 189);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.lookUpEditDepositSubject);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Name = "ConditionGeneralAccountChooseForm";
            this.Text = "总分类账";
            this.Controls.SetChildIndex(this.labelControl3, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.dateEditStartDate, 0);
            this.Controls.SetChildIndex(this.dateEditEndDate, 0);
            this.Controls.SetChildIndex(this.simpleButtonCancel, 0);
            this.Controls.SetChildIndex(this.simpleButtonOK, 0);
            this.Controls.SetChildIndex(this.labelControl4, 0);
            this.Controls.SetChildIndex(this.labelControl5, 0);
            this.Controls.SetChildIndex(this.labelControl6, 0);
            this.Controls.SetChildIndex(this.lookUpEditDepositSubject, 0);
            this.Controls.SetChildIndex(this.lookUpEdit1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepositSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditDepositSubject;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
    }
}