using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCBoxFootCheck
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PCBoxFootCheck _pcBoxFootCheck;
        BL.PCBoxFootCheckManager _pcBoxFootCheckManager = new Book.BL.PCBoxFootCheckManager();
        public EditForm()
        {
            InitializeComponent();

            this.newChooseEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseContorlSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.action = "view";
        }

        int LastFlag = 0; //页面载 入时是否执行 last方法
        public EditForm(string PCBoxFootCheckId)
            : this()
        {
            this._pcBoxFootCheck = this._pcBoxFootCheckManager.Get(PCBoxFootCheckId);
            if (this._pcBoxFootCheck == null)
                throw new ArithmeticException("PCBoxFootCheckId");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCBoxFootCheck PCBoxFootCheck)
            : this()
        {
            if (PCBoxFootCheck == null)
                throw new ArithmeticException("PCBoxFootCheckId");
            this._pcBoxFootCheck = PCBoxFootCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pcBoxFootCheck = new Book.Model.PCBoxFootCheck();
            this._pcBoxFootCheck.PCBoxFootCheckId = this._pcBoxFootCheckManager.GetId();
            this._pcBoxFootCheckManager.TiGuiExists(this._pcBoxFootCheck);
            this._pcBoxFootCheck.CheckNum = 1;   //检测数量默认为一
            this._pcBoxFootCheck.CheckDate = DateTime.Now;
            this._pcBoxFootCheck.Employee = BL.V.ActiveOperator.Employee;
            if (this._pcBoxFootCheck.Employee != null)
                this._pcBoxFootCheck.EmployeeId = this._pcBoxFootCheck.Employee.EmployeeId;
            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this._pcBoxFootCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pcBoxFootCheckManager.HasRowsAfter(this._pcBoxFootCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcBoxFootCheckManager.HasRowsBefore(this._pcBoxFootCheck);
        }

        protected override void MoveFirst()
        {
            this._pcBoxFootCheck = this._pcBoxFootCheckManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._pcBoxFootCheck = this._pcBoxFootCheckManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.PCBoxFootCheck pcBoxFootCheck = this._pcBoxFootCheckManager.GetNext(this._pcBoxFootCheck);
            if (pcBoxFootCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcBoxFootCheck = pcBoxFootCheck;
        }

        protected override void MovePrev()
        {
            Model.PCBoxFootCheck pcBoxFootCheck = this._pcBoxFootCheckManager.GetPrev(this._pcBoxFootCheck);
            if (pcBoxFootCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcBoxFootCheck = pcBoxFootCheck;
        }

        public override void Refresh()
        {
            if (this._pcBoxFootCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._pcBoxFootCheck = this._pcBoxFootCheckManager.Get(this._pcBoxFootCheck.PCBoxFootCheckId);
            }

            this.txt_Id.EditValue = this._pcBoxFootCheck.PCBoxFootCheckId;
            this.date_Check.EditValue = this._pcBoxFootCheck.CheckDate;
            this.newChooseEmployee.EditValue = this._pcBoxFootCheck.Employee;
            this.txt_Product.EditValue = this._pcBoxFootCheck.Product == null ? null : this._pcBoxFootCheck.Product.ToString();
            this.txt_InvoiceXO.EditValue = this._pcBoxFootCheck.InvoiceXO == null ? null : this._pcBoxFootCheck.InvoiceXO.CustomerInvoiceXOId;
            this.txt_PronoteHeader.EditValue = this._pcBoxFootCheck.PronoteHeaderId;
            this.richTextNote.Rtf = this._pcBoxFootCheck.Note;
            this.spinEditGetNum.EditValue = this._pcBoxFootCheck.GetNum;
            this.spinEditCheckNum.EditValue = this._pcBoxFootCheck.CheckNum;
            this.spinEditPassNum.EditValue = this._pcBoxFootCheck.PassNum;
            this.spinEditNoPassNum.EditValue = this._pcBoxFootCheck.NoPassNum;
            this.newChooseContorlAuditEmp.EditValue = this._pcBoxFootCheck.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcBoxFootCheck.AuditState);
            this.txt_CheckStandard.EditValue = this._pcBoxFootCheck.CheckStandard;
            this.newChooseContorlCustomer.EditValue = this._pcBoxFootCheck.Customer;
            this.newChooseContorlSupplier.EditValue = this._pcBoxFootCheck.Supplier;
            this.richTextBox1.Rtf = this._pcBoxFootCheck.Product == null ? null : this._pcBoxFootCheck.Product.ProductDescription;

            GetRadioGroup();
            base.Refresh();

            this.txt_Id.Enabled = true;
            this.txt_Id.Properties.ReadOnly = true;
        }

        private void GetRadioGroup()
        {
            this.radioFlap.EditValue = this._pcBoxFootCheck.Flap == null ? 0 : this._pcBoxFootCheck.Flap;
            this.radioExterior.EditValue = this._pcBoxFootCheck.Exterior == null ? 0 : this._pcBoxFootCheck.Exterior;
            this.radioOfColor.EditValue = this._pcBoxFootCheck.OfColor == null ? 0 : this._pcBoxFootCheck.OfColor; ;
            this.radioFootElasticL.EditValue = this._pcBoxFootCheck.FootElasticL == null ? 0 : this._pcBoxFootCheck.FootElasticL;
            this.radioFootElasticR.EditValue = this._pcBoxFootCheck.FootElasticR == null ? 0 : this._pcBoxFootCheck.FootElasticR;
            this.radioHeightFootL.EditValue = this._pcBoxFootCheck.HeightFootL == null ? 0 : this._pcBoxFootCheck.HeightFootL;
            this.radioHeightFootR.EditValue = this._pcBoxFootCheck.HeightFootR == null ? 0 : this._pcBoxFootCheck.HeightFootR;
            this.radioImpactTest.EditValue = this._pcBoxFootCheck.ImpactTest == null ? 0 : this._pcBoxFootCheck.ImpactTest;
            this.radioAceticacidTest.EditValue = this._pcBoxFootCheck.AceticacidTest == null ? 0 : this._pcBoxFootCheck.AceticacidTest;
        }

        protected override void Save()
        {
            this._pcBoxFootCheck.CheckDate = this.date_Check.EditValue == null ? DateTime.Now : this.date_Check.DateTime;
            this._pcBoxFootCheck.EmployeeId = this.newChooseEmployee.EditValue == null ? null : (this.newChooseEmployee.EditValue as Model.Employee).EmployeeId;
            this._pcBoxFootCheck.Flap = Convert.ToInt32(this.radioFlap.EditValue.ToString());
            this._pcBoxFootCheck.Exterior = Convert.ToInt32(this.radioExterior.EditValue.ToString());
            this._pcBoxFootCheck.OfColor = Convert.ToInt32(this.radioOfColor.EditValue.ToString());
            this._pcBoxFootCheck.FootElasticL = Convert.ToInt32(this.radioFootElasticL.EditValue.ToString());
            this._pcBoxFootCheck.FootElasticR = Convert.ToInt32(this.radioFootElasticR.EditValue.ToString());
            this._pcBoxFootCheck.HeightFootL = Convert.ToInt32(this.radioHeightFootL.EditValue.ToString());
            this._pcBoxFootCheck.HeightFootR = Convert.ToInt32(this.radioHeightFootR.EditValue.ToString());
            this._pcBoxFootCheck.ImpactTest = Convert.ToInt32(this.radioImpactTest.EditValue.ToString());
            this._pcBoxFootCheck.AceticacidTest = Convert.ToInt32(this.radioAceticacidTest.EditValue.ToString());
            this._pcBoxFootCheck.Note = this.richTextNote.Rtf;
            this._pcBoxFootCheck.GetNum = Convert.ToDouble(this.spinEditGetNum.EditValue == null ? null : this.spinEditGetNum.EditValue.ToString());
            this._pcBoxFootCheck.CheckNum = Convert.ToDouble(this.spinEditCheckNum.EditValue == null ? null : this.spinEditCheckNum.EditValue.ToString());
            this._pcBoxFootCheck.PassNum = Convert.ToDouble(this.spinEditPassNum.EditValue == null ? null : this.spinEditPassNum.EditValue.ToString());
            this._pcBoxFootCheck.NoPassNum = Convert.ToDouble(this.spinEditNoPassNum.EditValue == null ? null : this.spinEditNoPassNum.EditValue.ToString());
            this._pcBoxFootCheck.CheckStandard = this.txt_CheckStandard.Text;
            this._pcBoxFootCheck.CustomerId = this.newChooseContorlCustomer.EditValue == null ? null : (this.newChooseContorlCustomer.EditValue as Model.Customer).CustomerId;
            this._pcBoxFootCheck.SupplierId = this.newChooseContorlSupplier.EditValue == null ? null : (this.newChooseContorlSupplier.EditValue as Model.Supplier).SupplierId;

            switch (this.action)
            {
                case "insert":
                    this._pcBoxFootCheckManager.Insert(this._pcBoxFootCheck);
                    break;
                case "update":
                    this._pcBoxFootCheckManager.Update(this._pcBoxFootCheck);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._pcBoxFootCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcBoxFootCheckManager.Delete(this._pcBoxFootCheck.PCBoxFootCheckId);
            this._pcBoxFootCheck = this._pcBoxFootCheckManager.GetNext(this._pcBoxFootCheck);
            if (this._pcBoxFootCheck == null)
            {
                this._pcBoxFootCheck = this._pcBoxFootCheckManager.GetLast();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._pcBoxFootCheck);
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._pcBoxFootCheck = f.SelectItem as Model.PCBoxFootCheck;
                this.action = "view";
                this.Refresh();
            }
        }

        private void spinEditPassNum_EditValueChanged(object sender, EventArgs e)
        {
            this.spinEditNoPassNum.EditValue = Convert.ToDouble(this.spinEditCheckNum.EditValue) - Convert.ToDouble(this.spinEditPassNum.EditValue);
        }

        private void spinEditCheckNum_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(this.spinEditPassNum.EditValue) > 0)
            this.spinEditNoPassNum.EditValue = Convert.ToDouble(this.spinEditCheckNum.EditValue) - Convert.ToDouble(this.spinEditPassNum.EditValue);

        }

        private void btn_ProduceOtherCompact_Click(object sender, EventArgs e)
        {
            #region 选取客户订单(弃用2013年5月17日11:40:06 CN)
            //createProduce.EditForm f = new Book.UI.produceManager.createProduce.EditForm();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    if (f.SelectList != null && f.SelectList.Count > 0)
            //    {
            //        Model.InvoiceXODetail model = f.SelectList[0];
            //        this._pcBoxFootCheck.ProductId = model.ProductId;
            //        this.txt_Product.EditValue = model.Product == null ? null : model.Product.ToString();
            //        this._pcBoxFootCheck.InvoiceXOId = model.InvoiceId;
            //        this.txt_InvoiceXO.EditValue = model.InvoiceId;
            //        this.spinEditGetNum.EditValue = model.InvoiceXODetailQuantity;
            //    }
            //}
            #endregion

            #region 选取委外合同 2013年5月17日11:40:23 CN
            ProduceOtherCompact.ChooseOutContract f = new Book.UI.produceManager.ProduceOtherCompact.ChooseOutContract();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProduceOtherCompact currentModel = f.SelectItem as Model.ProduceOtherCompact;
                if (currentModel != null)
                {
                    this._pcBoxFootCheck.PronoteHeaderId = currentModel.PronoteHeaderId;          //加工单据编号
                    this._pcBoxFootCheck.InvoiceXOId = currentModel.InvoiceXOId;                  //客户订单编号
                    this._pcBoxFootCheck.InvoiceXO = currentModel.InvoiceXO;
                    if (currentModel.InvoiceXO != null)
                    {
                        this._pcBoxFootCheck.Customer = currentModel.InvoiceXO.Customer;
                        this._pcBoxFootCheck.CheckStandard = currentModel.InvoiceXO.xocustomer.CheckedStandard;
                        this.txt_InvoiceXO.EditValue = currentModel.InvoiceXO.CustomerInvoiceXOId;
                    }

                    Model.ProduceOtherCompactDetail pocd = new Book.BL.ProduceOtherCompactDetailManager().Select(currentModel)[0];
                    if (pocd != null)
                    {
                        //if (this._pcBoxFootCheck.Product != null)
                        //{
                        this._pcBoxFootCheck.Product = pocd.Product;
                        this._pcBoxFootCheck.ProductId = this._pcBoxFootCheck.Product.ProductId;
                        //}

                        if (pocd.ProduceOtherCompact != null)
                            this._pcBoxFootCheck.PronoteHeaderId = pocd.ProduceOtherCompact.PronoteHeaderId;
                        this._pcBoxFootCheck.GetNum = pocd.OtherCompactCount;
                    }
                    this.Refresh();
                }
            }
            #endregion
        }

        private void btn_PronoteHeader_Click(object sender, EventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectItem != null)
                {
                    this._pcBoxFootCheck.PronoteHeaderId = f.SelectItem.PronoteHeaderID;
                    this.txt_PronoteHeader.EditValue = f.SelectItem.PronoteHeaderID;
                    this.spinEditGetNum.EditValue = f.SelectItem.DetailsSum;
                    this._pcBoxFootCheck.ProductId = f.SelectItem.ProductId;
                    this.txt_Product.EditValue = f.SelectItem.ProductName;
                    this.richTextBox1.Rtf = f.SelectItem.ProductDesc;
                    this._pcBoxFootCheck.InvoiceXOId = f.SelectItem.InvoiceXOId;
                    this._pcBoxFootCheck.InvoiceXO = f.SelectItem.InvoiceXO;
                    if (f.SelectItem.InvoiceXO != null)
                    {
                        this.txt_CheckStandard.EditValue = f.SelectItem.InvoiceXO.xocustomer.CheckedStandard;
                        this.txt_InvoiceXO.EditValue = f.SelectItem.InvoiceXO.CustomerInvoiceXOId;
                        this.newChooseContorlCustomer.EditValue = f.SelectItem.InvoiceXO.Customer;
                    }
                }
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCBoxFootCheck.PRO_PCBoxFootCheckId;
        }

        protected override int AuditState()
        {
            return this._pcBoxFootCheck.AuditState.HasValue ? this._pcBoxFootCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCBoxFootCheck" + "," + this._pcBoxFootCheck.PCBoxFootCheckId;
        }

        #endregion
    }
}