using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.ProduceManager.Workhouselog;

namespace Book.UI.produceManager.JISDataInput
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        BL.PCFinishCheckManager _PCFCManager = new Book.BL.PCFinishCheckManager();
        Model.PCFinishCheck _PCFC = null;
        IList<Model.OpticsTest> ListOpticsTest;
        int Def_select = 2;

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.PCFinishCheck.PRO_PCFinishCheckDate, new AA(Properties.Resources.DateIsNull, this.DE_JYDRQ));
            this.requireValueExceptions.Add(Model.PCFinishCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.txtProduct));

            this.action = "view";
        }

        int LastFlag = 0;
        public EditForm(string invoiceId)
            : this()
        {
            this._PCFC = this._PCFCManager.Get(invoiceId);
            if (this._PCFC == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCFinishCheck mPCFC)
            : this()
        {
            if (mPCFC == null)
                throw new ArithmeticException("invoiceid");
            this._PCFC = mPCFC;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCFinishCheck mPCFC, string action)
            : this()
        {
            this._PCFC = mPCFC;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        public override void Refresh()
        {
            if (this._PCFC == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._PCFC = this._PCFCManager.Get(this._PCFC.PCFinishCheckID);
                }
            }

            this.txtPCFinishCheckID.Text = this._PCFC.PCFinishCheckID;
            //this.txtInvoiceCusXOId.Text = this._PCFC.InvoiceCusXOId;
            if (this._PCFC.PronoteHeader != null)
                this.txtInvoiceCusXOId.Text = (this._PCFC.PronoteHeader.InvoiceXO == null ? null : this._PCFC.PronoteHeader.InvoiceXO.CustomerInvoiceXOId);
            else
                this.txtInvoiceCusXOId.Text = null;

            base.Refresh();

            this.txtPCFinishCheckID.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.PCFinishCheck pcfc = this._PCFCManager.GetNext(this._PCFC);
            if (pcfc == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCFC = this._PCFCManager.Get(pcfc.PCFinishCheckID);
        }

        protected override void MovePrev()
        {
            Model.PCFinishCheck pcfc = this._PCFCManager.GetPrev(this._PCFC);
            if (pcfc == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCFC = this._PCFCManager.Get(pcfc.PCFinishCheckID);
        }

        protected override void MoveFirst()
        {
            this._PCFC = this._PCFCManager.Get(this._PCFCManager.GetFirst() == null ? "" : this._PCFCManager.GetFirst().PCFinishCheckID);
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._PCFC = this._PCFCManager.Get(this._PCFCManager.GetLast() == null ? "" : this._PCFCManager.GetLast().PCFinishCheckID);
        }

        protected override bool HasRows()
        {
            return this._PCFCManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._PCFCManager.HasRowsAfter(this._PCFC);
        }

        protected override bool HasRowsPrev()
        {
            return this._PCFCManager.HasRowsBefore(this._PCFC);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            //return new RO(this._PCFC);
            return null;
        }

        protected override void AddNew()
        {
            this._PCFC = new Book.Model.PCFinishCheck();
            this._PCFC.PCFinishCheckID = this._PCFCManager.GetId();
            this._PCFC.PCFinishCheckDate = DateTime.Now.Date;
            this._PCFC.PCFinishCheckCount = 1;  //默认抽检数量为1
            this._PCFC.Employee0 = BL.V.ActiveOperator.Employee;
            this._PCFC.Employee0Id = BL.V.ActiveOperator.EmployeeId;

            //清空複製的光學測試信息
            if (ListOpticsTest != null)
                ListOpticsTest.Clear();
        }

        protected override void Save()
        {
            this._PCFC.PCFinishCheckID = this.txtPCFinishCheckID.Text;
            this._PCFC.PCFinishCheckDate = this.DE_JYDRQ.DateTime;
            this._PCFC.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            switch (this.action)
            {
                case "insert":
                    this._PCFCManager.Insert(this._PCFC);
                    break;
                case "update":
                    this._PCFCManager.Update(this._PCFC);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._PCFC == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCFCManager.Delete(this._PCFC.PCFinishCheckID);
            this._PCFC = this._PCFCManager.GetNext(this._PCFC);
            if (this._PCFC == null)
            {
                this._PCFC = this._PCFCManager.GetLast();
            }
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm lf = new ListForm();
            if (lf.ShowDialog(this) == DialogResult.OK)
            {
                this._PCFC = lf.SelectItem as Model.PCFinishCheck;
                this.action = "view";
                this.Refresh();
            }
            lf.Dispose();
            GC.Collect();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
        }

        //选择加工单据
        private void btnGetPronoteHeader_Click(object sender, EventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(0, "Check");
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader currentModel = pronoForm.SelectItem;

                if (currentModel != null)
                {
                    this._PCFC.PronoteHeader = currentModel;
                    this._PCFC.PronoteHeaderID = currentModel.PronoteHeaderID;
                    this._PCFC.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._PCFC.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._PCFC.ProductId = this._PCFC.Product.ProductId;
                    this._PCFC.CustomerProductName = currentModel.CustomerProductName;      //客户型号
                    this._PCFC.PCFinishCheckInCoiunt = currentModel.InvoiceXODetailQuantity;
                    //this._PCFC.PCFinishCheckCount = Math.Ceiling(Convert.ToDouble(this._PCFC.PCFinishCheckInCoiunt) / 500);
                    this._PCFC.PCFinishCheckCount = Common.AutoCalculation.Calculation(currentModel.CustomerCheckStandard, Convert.ToInt32(this._PCFC.PCFinishCheckInCoiunt));

                    this._PCFC.ProductUnit = this._PCFC.Product.QualityTestUnit;
                    this._PCFC.ProductUnitId = this._PCFC.Product.QualityTestUnitId;

                    if (currentModel.InvoiceXOId != null)
                    {
                        Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(currentModel.InvoiceXOId);
                        if (xo != null)
                        {
                            this._PCFC.Pihao = xo.CustomerLotNumber;
                            this._PCFC.PronoteHeader.InvoiceXO = xo;
                        }
                    }

                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        private void txtPCFinishCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
            }
            cp.Dispose();
            GC.Collect();
        }

        private void linkLabelGuangxue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PCPGOnlineCheck.OpticsTest f = new Book.UI.produceManager.PCPGOnlineCheck.OpticsTest(this._PCFC.PCFinishCheckID, 1);
            f.ShowDialog();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCFinishCheck.PRO_PCFinishCheckID;
        }

        protected override int AuditState()
        {
            return this._PCFC.AuditState.HasValue ? this._PCFC.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCFinishCheck" + "," + this._PCFC.PCFinishCheckID;
        }

        #endregion

        private void CE_InCount_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.action != "view")
            //    this.CE_Count.EditValue = Math.Ceiling(Convert.ToDouble(this.CE_InCount.EditValue) / 500);
        }
    }
}
