using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.ProduceManager.Workhouselog;

namespace Book.UI.produceManager.PCFinishCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        BL.PCFinishCheckManager _PCFCManager = new Book.BL.PCFinishCheckManager();
        BL.ProduceOtherInDepotManager _poim = new Book.BL.ProduceOtherInDepotManager();
        BL.OpticsTestManager _OpticsTestManager = new Book.BL.OpticsTestManager();
        Model.PCFinishCheck _PCFC = null;
        IList<Model.OpticsTest> ListOpticsTest;
        int Def_select = 2;

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.PCFinishCheck.PRO_PCFinishCheckID, new AA(Properties.Resources.NumsIsNotNull, this.txtPCFinishCheckID));
            this.requireValueExceptions.Add(Model.PCFinishCheck.PRO_PCFinishCheckDate, new AA(Properties.Resources.DateIsNull, this.DE_JYDRQ));
            this.requireValueExceptions.Add(Model.PCFinishCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.txtProduct));
            this.requireValueExceptions.Add(Model.PCFinishCheck.PRO_Employee0Id, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));

            this.nccWorkHouse.Choose = new ChooseWorkHouse();
            this.nccEmployee0.Choose = new ChooseEmployee();
            this.nccEmployee1.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
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
                this.SetDefRadioGroup();
            }

            this.txtPCFinishCheckID.Text = this._PCFC.PCFinishCheckID;
            this.txtInvoiceCusXOId.Text = this._PCFC.InvoiceCusXOId;
            this.txtPCFinishCheckDesc.Text = this._PCFC.PCFinishCheckDesc;
            this.DE_JYDRQ.EditValue = this._PCFC.PCFinishCheckDate;
            this.txtProduct.Text = this._PCFC.Product == null ? "" : this._PCFC.Product.ToString();
            this.CE_Count.EditValue = this._PCFC.PCFinishCheckCount.HasValue ? this._PCFC.PCFinishCheckCount : 0;
            this.CE_InCount.EditValue = this._PCFC.PCFinishCheckInCoiunt.HasValue ? this._PCFC.PCFinishCheckInCoiunt : 0;
            this.txtCustomerProductName.Text = this._PCFC.CustomerProductName;
            this.txtPronoteHeaderId.Text = this._PCFC.PronoteHeaderID;
            //this.lblCustomerType.Text = this._PCFC.CustomerType;
            this.nccEmployee0.EditValue = this._PCFC.Employee0;
            this.nccEmployee1.EditValue = this._PCFC.Employee1;
            this.nccWorkHouse.EditValue = this._PCFC.WorkHouse;

            this.chkMuShiJianYan.Checked = this._PCFC.IsMuShiJianYan.HasValue ? this._PCFC.IsMuShiJianYan.Value : false;

            this.newChooseContorlAuditEmp.EditValue = this._PCFC.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCFC.AuditState);
            this.lookUpEditUnit.EditValue = this._PCFC.ProductUnitId;
            this.txt_AnnualRing.Text = this._PCFC.AnnualRing;
            this.txt_Pihao.EditValue = this._PCFC.Pihao;

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
            return new RO(this._PCFC);
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
            this._PCFC.PCFinishCheckCount = int.Parse(this.CE_Count.Value.ToString());
            this._PCFC.PCFinishCheckInCoiunt = int.Parse(this.CE_InCount.Value.ToString());
            this._PCFC.PCFinishCheckDesc = this.txtPCFinishCheckDesc.Text;
            if (this.nccEmployee0.EditValue != null)
            {
                this._PCFC.Employee0Id = (this.nccEmployee0.EditValue as Model.Employee).EmployeeId;
            }
            if (this.nccEmployee1.EditValue != null)
            {
                this._PCFC.Employee1Id = (this.nccEmployee1.EditValue as Model.Employee).EmployeeId;
            }
            if (this.nccWorkHouse.EditValue != null)
            {
                this._PCFC.WorkHouseId = (this.nccWorkHouse.EditValue as Model.WorkHouse).WorkHouseId;
            }

            this._PCFC.IsMuShiJianYan = this.chkMuShiJianYan.Checked;
            this._PCFC.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();
            this._PCFC.AnnualRing = this.txt_AnnualRing.Text;
            this._PCFC.Pihao = this.txt_Pihao.Text;

            #region Radio
            this._PCFC.AttrCJBZ = this.radio_AttrCJBZ.SelectedIndex;
            this._PCFC.AttrDZDWQDW = this.radio_AttrDZDWQDW.SelectedIndex;
            this._PCFC.AttrGX = this.radio_AttrGX.SelectedIndex;
            this._PCFC.AttrGZBKYRL = this.radio_AttrGZBKYRL.SelectedIndex;
            this._PCFC.AttrJDZRFS = this.radio_AttrJDZRFS.SelectedIndex;
            this._PCFC.AttrJJSFTSYH = this.radio_AttrJJSFTSYH.SelectedIndex;
            this._PCFC.AttrJPBKGS = this.radio_AttrJPBKGS.SelectedIndex;
            this._PCFC.AttrJPJHZQ = this.radio_AttrJPJHZQ.SelectedIndex;
            this._PCFC.AttrJPSX = this.radio_AttrJPSX.SelectedIndex;
            this._PCFC.AttrJSSFZQ = this.radio_AttrJSSFZQ.SelectedIndex;
            this._PCFC.AttrJWYHWRL = this.radio_AttrJWYHWRL.SelectedIndex;
            this._PCFC.AttrNHDQSFZQ = this.radio_AttrNHDQSFZQ.SelectedIndex;
            this._PCFC.AttrNHTB = this.radio_AttrNHTB.SelectedIndex;
            this._PCFC.AttrPKZRFS = this.radio_AttrPKZRFS.SelectedIndex;
            this._PCFC.AttrSLDNHWXTMSFZQ = this.radio_AttrSLDNHWXTMSFZQ.SelectedIndex;
            this._PCFC.AttrSLDSFMF = this.radio_AttrSLDSFMF.SelectedIndex;
            this._PCFC.AttrTSL = this.radio_AttrTSL.SelectedIndex;
            this._PCFC.AttrWXTB = this.radio_AttrWXTB.SelectedIndex;
            this._PCFC.AttrZMCM = this.radio_AttrZMCM.SelectedIndex;
            this._PCFC.AttrZZWBXGJ = this.radio_AttrZZWBXGJ.SelectedIndex;
            this._PCFC.AttrDGBLTest = this.radio_AttrDGBLTest.SelectedIndex;
            #endregion

            string strCusXoId = this.txtInvoiceCusXOId.Text;
            string sqlJudge = string.Empty;
            this._PCFC.CustomerProductName = this.txtCustomerProductName.Text;
            switch (this.action)
            {
                case "insert":

                    sqlJudge = "SELECT InvoiceCusXOId FROM PCFinishCheck WHERE InvoiceCusXOId = '" + strCusXoId + "'";
                    this._PCFCManager.Insert(this._PCFC);
                    break;
                case "update":
                    sqlJudge = "SELECT p1.InvoiceCusXOId FROM PCFinishCheck p1 WHERE p1.InvoiceCusXOId = '" + strCusXoId + "' AND p1.PCFinishCheckID NOT IN ( SELECT TOP 1 p2.PCFinishCheckID FROM PCFinishCheck p2)";
                    this._PCFCManager.Update(this._PCFC);
                    break;
            }

            if (ListOpticsTest != null)
            {
                foreach (var item in ListOpticsTest)
                {
                    item.OpticsTestId = this._OpticsTestManager.GetId();
                    item.OptiscTestDate = DateTime.Now;
                    item.PCFinishCheckId = _PCFC.PCFinishCheckID;
                    this._OpticsTestManager.Insert(item);
                }
                ListOpticsTest.Clear();
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
                    this._PCFC.PronoteHeaderID = currentModel.PronoteHeaderID;
                    this._PCFC.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._PCFC.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._PCFC.ProductId = this._PCFC.Product.ProductId;
                    this._PCFC.CustomerProductName = currentModel.CustomerProductName;      //客户型号
                    this._PCFC.PCFinishCheckInCoiunt = currentModel.InvoiceXODetailQuantity;
                    this._PCFC.PCFinishCheckCount = Math.Ceiling(Convert.ToDouble(this._PCFC.PCFinishCheckInCoiunt) / 500);

                    this._PCFC.ProductUnit = this._PCFC.Product.QualityTestUnit;
                    this._PCFC.ProductUnitId = this._PCFC.Product.QualityTestUnitId;

                    if (currentModel.InvoiceXOId != null)
                    {
                        Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(currentModel.InvoiceXOId);
                        if (xo != null)
                            this._PCFC.Pihao = xo.CustomerLotNumber;
                    }

                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        //设置选择详细
        private void SetDefRadioGroup()
        {
            this.radio_AttrDZDWQDW.SelectedIndex = this._PCFC.AttrDZDWQDW.HasValue ? this._PCFC.AttrDZDWQDW.Value : Def_select;
            this.radio_AttrWXTB.SelectedIndex = this._PCFC.AttrWXTB.HasValue ? this._PCFC.AttrWXTB.Value : Def_select;
            this.radio_AttrJWYHWRL.SelectedIndex = this._PCFC.AttrJWYHWRL.HasValue ? this._PCFC.AttrJWYHWRL.Value : Def_select;
            this.radio_AttrZMCM.SelectedIndex = this._PCFC.AttrZMCM.HasValue ? this._PCFC.AttrZMCM.Value : Def_select;
            this.radio_AttrGZBKYRL.SelectedIndex = this._PCFC.AttrGZBKYRL.HasValue ? this._PCFC.AttrGZBKYRL.Value : Def_select;
            this.radio_AttrSLDSFMF.SelectedIndex = this._PCFC.AttrSLDSFMF.HasValue ? this._PCFC.AttrSLDSFMF.Value : Def_select;
            this.radio_AttrZZWBXGJ.SelectedIndex = this._PCFC.AttrZZWBXGJ.HasValue ? this._PCFC.AttrZZWBXGJ.Value : Def_select;
            this.radio_AttrNHDQSFZQ.SelectedIndex = this._PCFC.AttrNHDQSFZQ.HasValue ? this._PCFC.AttrNHDQSFZQ.Value : Def_select;
            this.radio_AttrJPBKGS.SelectedIndex = this._PCFC.AttrJPBKGS.HasValue ? this._PCFC.AttrJPBKGS.Value : Def_select;
            this.radio_AttrNHTB.SelectedIndex = this._PCFC.AttrNHTB.HasValue ? this._PCFC.AttrNHTB.Value : Def_select;
            this.radio_AttrJPJHZQ.SelectedIndex = this._PCFC.AttrJPJHZQ.HasValue ? this._PCFC.AttrJPJHZQ.Value : Def_select;
            this.radio_AttrJSSFZQ.SelectedIndex = this._PCFC.AttrJSSFZQ.HasValue ? this._PCFC.AttrJSSFZQ.Value : Def_select;
            this.radio_AttrJPSX.SelectedIndex = this._PCFC.AttrJPSX.HasValue ? this._PCFC.AttrJPSX.Value : Def_select;
            this.radio_AttrJDZRFS.SelectedIndex = this._PCFC.AttrJDZRFS.HasValue ? this._PCFC.AttrJDZRFS.Value : Def_select;
            this.radio_AttrJJSFTSYH.SelectedIndex = this._PCFC.AttrJJSFTSYH.HasValue ? this._PCFC.AttrJJSFTSYH.Value : Def_select;
            this.radio_AttrPKZRFS.SelectedIndex = this._PCFC.AttrPKZRFS.HasValue ? this._PCFC.AttrPKZRFS.Value : Def_select;
            this.radio_AttrGX.SelectedIndex = this._PCFC.AttrGX.HasValue ? this._PCFC.AttrGX.Value : Def_select;
            this.radio_AttrSLDNHWXTMSFZQ.SelectedIndex = this._PCFC.AttrSLDNHWXTMSFZQ.HasValue ? this._PCFC.AttrSLDNHWXTMSFZQ.Value : Def_select;
            this.radio_AttrTSL.SelectedIndex = this._PCFC.AttrTSL.HasValue ? this._PCFC.AttrTSL.Value : Def_select;
            this.radio_AttrCJBZ.SelectedIndex = this._PCFC.AttrCJBZ.HasValue ? this._PCFC.AttrCJBZ.Value : Def_select;
            this.radio_AttrDGBLTest.SelectedIndex = this._PCFC.AttrDGBLTest.HasValue ? this._PCFC.AttrDGBLTest.Value : Def_select;
        }

        private void txtPCFinishCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.txtPCFinishCheckDesc.Text += setParam.SettingCurrentValue;
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
            if (this.action != "view")
                this.CE_Count.EditValue = Math.Ceiling(Convert.ToDouble(this.CE_InCount.EditValue) / 500);
        }

        /// <summary>
        /// 複製
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Model.PCFinishCheck model = new Book.Model.PCFinishCheck();
            model.PCFinishCheckID = this._PCFCManager.GetId();
            model.PCFinishCheckDate = DateTime.Now.Date;

            model.Employee0 = this._PCFC.Employee0;
            model.Employee1 = this._PCFC.Employee1;
            model.WorkHouse = this._PCFC.WorkHouse;
            model.IsMuShiJianYan = this._PCFC.IsMuShiJianYan.HasValue ? this._PCFC.IsMuShiJianYan.Value : false;
            model.AnnualRing = this._PCFC.AnnualRing;
            model.Pihao = this._PCFC.Pihao;

            model.AttrDZDWQDW = this._PCFC.AttrDZDWQDW.HasValue ? this._PCFC.AttrDZDWQDW.Value : Def_select;
            model.AttrWXTB = this._PCFC.AttrWXTB.HasValue ? this._PCFC.AttrWXTB.Value : Def_select;
            model.AttrJWYHWRL = this._PCFC.AttrJWYHWRL.HasValue ? this._PCFC.AttrJWYHWRL.Value : Def_select;
            model.AttrZMCM = this._PCFC.AttrZMCM.HasValue ? this._PCFC.AttrZMCM.Value : Def_select;
            model.AttrGZBKYRL = this._PCFC.AttrGZBKYRL.HasValue ? this._PCFC.AttrGZBKYRL.Value : Def_select;
            model.AttrSLDSFMF = this._PCFC.AttrSLDSFMF.HasValue ? this._PCFC.AttrSLDSFMF.Value : Def_select;
            model.AttrZZWBXGJ = this._PCFC.AttrZZWBXGJ.HasValue ? this._PCFC.AttrZZWBXGJ.Value : Def_select;
            model.AttrNHDQSFZQ = this._PCFC.AttrNHDQSFZQ.HasValue ? this._PCFC.AttrNHDQSFZQ.Value : Def_select;
            model.AttrJPBKGS = this._PCFC.AttrJPBKGS.HasValue ? this._PCFC.AttrJPBKGS.Value : Def_select;
            model.AttrNHTB = this._PCFC.AttrNHTB.HasValue ? this._PCFC.AttrNHTB.Value : Def_select;
            model.AttrJPJHZQ = this._PCFC.AttrJPJHZQ.HasValue ? this._PCFC.AttrJPJHZQ.Value : Def_select;
            model.AttrJSSFZQ = this._PCFC.AttrJSSFZQ.HasValue ? this._PCFC.AttrJSSFZQ.Value : Def_select;
            model.AttrJPSX = this._PCFC.AttrJPSX.HasValue ? this._PCFC.AttrJPSX.Value : Def_select;
            model.AttrJDZRFS = this._PCFC.AttrJDZRFS.HasValue ? this._PCFC.AttrJDZRFS.Value : Def_select;
            model.AttrJJSFTSYH = this._PCFC.AttrJJSFTSYH.HasValue ? this._PCFC.AttrJJSFTSYH.Value : Def_select;
            model.AttrPKZRFS = this._PCFC.AttrPKZRFS.HasValue ? this._PCFC.AttrPKZRFS.Value : Def_select;
            model.AttrGX = this._PCFC.AttrGX.HasValue ? this._PCFC.AttrGX.Value : Def_select;
            model.AttrSLDNHWXTMSFZQ = this._PCFC.AttrSLDNHWXTMSFZQ.HasValue ? this._PCFC.AttrSLDNHWXTMSFZQ.Value : Def_select;
            model.AttrTSL = this._PCFC.AttrTSL.HasValue ? this._PCFC.AttrTSL.Value : Def_select;
            model.AttrCJBZ = this._PCFC.AttrCJBZ.HasValue ? this._PCFC.AttrCJBZ.Value : Def_select;
            model.AttrDGBLTest = this._PCFC.AttrDGBLTest.HasValue ? this._PCFC.AttrDGBLTest.Value : Def_select;

            ListOpticsTest = this._OpticsTestManager.FSelect(this._PCFC.PCFinishCheckID);

            this._PCFC = model;
            this.action = "insert";
            this.Refresh();
        }
    }
}
