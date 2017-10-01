using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using System.Xml;
using System.Linq;

namespace Book.UI.produceManager.PCDoubleImpactCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        Model.PCDoubleImpactCheck _pcdic = null;
        BL.PCDoubleImpactCheckManager _pcdicManage = new Book.BL.PCDoubleImpactCheckManager();
        int pcType = -1;
        IList<Model.Setting> CJLD = new List<Model.Setting>();
        IList<Model.Setting> ZQZL = new List<Model.Setting>();
        IList<Model.Setting> LYBTXX = new List<Model.Setting>();
        IList<Model.Setting> JIARE = new List<Model.Setting>();
        IList<Model.Setting> LENGDONG = new List<Model.Setting>();

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.PCDoubleImpactCheck.PRO_PCDoubleImpactCheckID, new AA(Properties.Resources.NumsIsNotNull, this.txtPCDoubleImpactCheckID));
            this.requireValueExceptions.Add(Model.PCDoubleImpactCheck.PRO_PCDoubleImpactCheckDate, new AA(Properties.Resources.DateNotNull, this.DE_PCDoubleImpactCheckDate));
            this.requireValueExceptions.Add(Model.PCDoubleImpactCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.BEProduct));
            this.requireValueExceptions.Add(Model.PCDoubleImpactCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));
            this.action = "view";
            this.nccEmployee0.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
        }

        public EditForm(int pcFlag)
            : this()
        {
            this.pcType = pcFlag;
        }

        int LastFlag = 0;
        public EditForm(string invoiceId)
            : this()
        {
            this._pcdic = this._pcdicManage.Get(invoiceId);
            if (this._pcdic == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCDoubleImpactCheck mpcdic, int pcFlag)
            : this()
        {
            if (pcFlag == 0)
            {
                this.Text = "CSA衝擊測試單";
            }
            else if (pcFlag == 1)
            {
                this.Text = "BS/EN衝擊測試單";
            }
            else if (pcFlag == 2)
            {
                this.Text = "AS/NZS衝擊測試單";
            }
            this.pcType = pcFlag;
            if (mpcdic == null)
                throw new ArithmeticException("invoiceid");
            this._pcdic = mpcdic;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCDoubleImpactCheck mpcdic, string action)
            : this()
        {
            this._pcdic = mpcdic;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pcdic = new Book.Model.PCDoubleImpactCheck();
            this._pcdic.PCDoubleImpactCheckID = this._pcdicManage.GetId();
            this._pcdic.PCDoubleImpactCheckDate = DateTime.Now.Date;
            this._pcdic.PCDoubleImpactCheckType = 0;
            this._pcdic.PCDoubleImpactCheckCount = 1;   //默认抽检数量为1
            this._pcdic.Employee = BL.V.ActiveOperator.Employee;
            this._pcdic.EmployeeId = BL.V.ActiveOperator.EmployeeId;
            //初始化一条详细
            this._pcdic.Detail = new List<Model.PCDoubleImpactCheckDetail>();
            this.AddDataRows();
        }

        protected override void Delete()
        {

            if (this._pcdic == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcdicManage.Delete(this._pcdic);

            this._pcdic = this._pcdicManage.GetNext(this._pcdic);
            if (this._pcdic == null)
            {
                //this._pcdic = this._pcdicManage.GetLast(this._pcdic.PCDoubleImpactCheckType.Value);
                this._pcdic = this._pcdicManage.GetLast(this.pcType);
            }
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._pcdic = this._pcdicManage.Get(this._pcdicManage.GetLast(this.pcType) == null ? "" : this._pcdicManage.GetLast(this.pcType).PCDoubleImpactCheckID);
        }

        protected override void MoveFirst()
        {
            this._pcdic = this._pcdicManage.Get(this._pcdicManage.GetFirst(this._pcdic.PCDoubleImpactCheckType.HasValue ? this._pcdic.PCDoubleImpactCheckType.Value : -1) == null ? "" : this._pcdicManage.GetFirst(this._pcdic.PCDoubleImpactCheckType.HasValue ? this._pcdic.PCDoubleImpactCheckType.Value : -1).PCDoubleImpactCheckID);
        }

        protected override void MovePrev()
        {
            Model.PCDoubleImpactCheck p = this._pcdicManage.GetPrev(this._pcdic);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcdic = this._pcdicManage.Get(p.PCDoubleImpactCheckID);
        }

        protected override void MoveNext()
        {
            Model.PCDoubleImpactCheck p = this._pcdicManage.GetNext(this._pcdic);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcdic = this._pcdicManage.Get(p.PCDoubleImpactCheckID);
        }

        protected override bool HasRows()
        {
            return this._pcdicManage.HasRows(this._pcdic.PCDoubleImpactCheckType.Value);
        }

        protected override bool HasRowsNext()
        {
            return this._pcdicManage.HasRowsAfter(this._pcdic);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcdicManage.HasRowsBefore(this._pcdic);
        }

        public override void Refresh()
        {
            if (this._pcdic == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._pcdic = this._pcdicManage.GetDetail(this._pcdic);
                }
            }
            //初始化控件
            this.txtPCDoubleImpactCheckID.Text = this._pcdic.PCDoubleImpactCheckID;
            //this.txtInvoiceCusXOId.Text = this._pcdic.InvoiceCusXOId;
            if (!string.IsNullOrEmpty(this._pcdic.PronoteHeaderId))
            {
                Model.PronoteHeader ph = new BL.PronoteHeaderManager().Get(this._pcdic.PronoteHeaderId);
                if (ph != null)
                    this.txtInvoiceCusXOId.Text = (ph.InvoiceXO == null ? null : ph.InvoiceXO.CustomerInvoiceXOId);
            }
            this.txtPronoteHeaderId.Text = this._pcdic.PronoteHeaderId;
            this.txtPCDoubleImpactCheckDesc.Text = this._pcdic.PCDoubleImpactCheckDesc;
            this.DE_PCDoubleImpactCheckDate.EditValue = this._pcdic.PCDoubleImpactCheckDate.Value;
            this.BEProduct.EditValue = this._pcdic.Product;
            this.nccEmployee0.EditValue = this._pcdic.Employee;
            this.txtCheckedStandard.Text = this._pcdic.CheckStandard;
            this.calcPCDoubleImpactCheckCount.EditValue = this._pcdic.PCDoubleImpactCheckCount.HasValue ? this._pcdic.PCDoubleImpactCheckCount.Value : 0;
            this.ceInvoiceXOCount.EditValue = this._pcdic.InvoiceXOQuantity.HasValue ? this._pcdic.InvoiceXOQuantity.Value : 0;
            this.newChooseContorlAuditEmp.EditValue = this._pcdic.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcdic.AuditState);
            this.coBoxCJLD.Text = string.IsNullOrEmpty(this._pcdic.PowerImpact) ? this.coBoxCJLD.Text : this._pcdic.PowerImpact;
            this.comBox_ChongJICeShiJiaRe.Text = string.IsNullOrEmpty(this._pcdic.JiaReWenDu) ? this.comBox_ChongJICeShiJiaRe.Text : this._pcdic.JiaReWenDu;
            this.comBox_ChongJICeShiLengDong.Text = string.IsNullOrEmpty(this._pcdic.LengDongWenDu) ? this.comBox_ChongJICeShiLengDong.Text : this._pcdic.LengDongWenDu;
            this.coBoxZQZL.Text = string.IsNullOrEmpty(this._pcdic.ZhuiQiuKG) ? this.coBoxZQZL.Text : this._pcdic.ZhuiQiuKG;
            this.coBoxLYBTXX.Text = string.IsNullOrEmpty(this._pcdic.PrintHeader) ? this.coBoxLYBTXX.Text : this._pcdic.PrintHeader;
            this.lookUpEditUnit.EditValue = this._pcdic.ProductUnitId;

            #region LookUpEditor

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("id", typeof(string));
            DataColumn dc1 = new DataColumn("name", typeof(string));
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            DataRow dr;
            dr = dt.NewRow();
            dr[0] = "-1";
            dr[1] = string.Empty;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "△";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "X";
            dt.Rows.Add(dr);

            for (int i = 0; i < this.gridView1.Columns.Count; i++)
            {
                if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit && this.gridView1.Columns[i].Name != "Employee")
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name",25, "标识"),
                     });
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "name";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "id";
                }
            }

            #endregion

            this.GirdViewLookUpEditEmployee.DataSource = this.bsLUEemployees;

            this.bindingSource1.DataSource = this._pcdic.Detail;

            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }

            //this.ceInvoiceXOCount.Enabled = false;
            this.txtPCDoubleImpactCheckID.Properties.ReadOnly = true;
            //this.nccEmployee0.Enabled = false;
        }

        protected override void Save()
        {
            this._pcdic.PCDoubleImpactCheckID = this.txtPCDoubleImpactCheckID.Text;
            this._pcdic.PCDoubleImpactCheckDate = this.DE_PCDoubleImpactCheckDate.DateTime;
            this._pcdic.PCDoubleImpactCheckDesc = this.txtPCDoubleImpactCheckDesc.Text;
            this._pcdic.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._pcdic.PronoteHeaderId = this.txtPronoteHeaderId.Text;
            this._pcdic.PCDoubleImpactCheckType = this.pcType;
            this._pcdic.PowerImpact = this.coBoxCJLD.Text;
            this._pcdic.JiaReWenDu = this.comBox_ChongJICeShiJiaRe.Text;
            this._pcdic.LengDongWenDu = this.comBox_ChongJICeShiLengDong.Text;
            this._pcdic.CheckStandard = this.txtCheckedStandard.Text;
            this._pcdic.PCDoubleImpactCheckCount = int.Parse(this.calcPCDoubleImpactCheckCount.EditValue != null ? calcPCDoubleImpactCheckCount.EditValue.ToString() : "0");
            this._pcdic.InvoiceXOQuantity = Convert.ToDouble(this.ceInvoiceXOCount.Value);

            this._pcdic.Employee = (this.nccEmployee0.EditValue as Model.Employee);
            if (this._pcdic.Employee != null)
            {
                this._pcdic.EmployeeId = this._pcdic.Employee.EmployeeId;
            }

            this._pcdic.Product = (this.BEProduct.EditValue as Model.Product);
            if (this._pcdic.Product != null)
            {
                this._pcdic.ProductId = this._pcdic.Product.ProductId;
            }
            this._pcdic.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();
            this._pcdic.ZhuiQiuKG = this.coBoxZQZL.Text;
            this._pcdic.PrintHeader = this.coBoxLYBTXX.Text;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this._pcdicManage.Insert(this._pcdic);
                    break;
                case "update":
                    this._pcdicManage.Update(this._pcdic);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            switch (this._pcdic.PCDoubleImpactCheckType)
            {
                case 0:     //CSA
                    return new RO1(this._pcdic);
                    break;
                case 1:    //BS-EN
                    return new RO(this._pcdic);
                    break;
                case 2:   //AS-NZS
                    return new RO2(this._pcdic);
                    break;
                default:
                    return null;
                    break;
            }
            //return new RO(this._pcdic);
        }

        //添加行
        private void AddDataRows()
        {
            Model.PCDoubleImpactCheckDetail pcdicDetail = new Book.Model.PCDoubleImpactCheckDetail();
            pcdicDetail.PCDoubleImpactCheckDetailID = Guid.NewGuid().ToString();
            pcdicDetail.PCDoubleImpactCheckID = this._pcdic.PCDoubleImpactCheckID;
            pcdicDetail.PCDoubleImpactCheckDetailDate = DateTime.Now;
            this._pcdic.Detail.Add(pcdicDetail);

            this.bindingSource1.Position = this.bindingSource1.IndexOf(pcdicDetail);
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bsLUEemployees.DataSource = new BL.EmployeeManager().SelectOnActive();
            switch (this.pcType)
            {
                case 0:     //CSA
                    this.attrHotL.Visible = false;
                    this.attrHotR.Visible = false;
                    this.attrCoolL.Visible = false;
                    this.attrCoolR.Visible = false;

                    this.attrHeat.Visible = false;
                    this.attrZhuiQiu68gL.Visible = false;
                    this.attrZhuiQiu68gR.Visible = false;
                    this.attrChuanTou44_2gL.Visible = false;
                    this.attrChuanTou44_2gR.Visible = false;

                    this.attr30L.Caption = "眼球1.5CM(左)";
                    this.attr30R.Caption = "眼球1.5Cm(右)";

                    this.attr60L.Caption = "90度後1CM(左)";
                    this.attr60R.Caption = "90度後1CM(右)";
                    this.attr75L.Caption = "90度後1CM的上1CM(左)";
                    this.attr75R.Caption = "90度後1CM的上1CM(右)";
                    this.attr90L.Caption = "90度後1CM的下1CM(左)";
                    this.attr90R.Caption = "90度後1CM的下1CM(右)";
                    this.attr60L.Width = 100;
                    this.attr60R.Width = 100;
                    this.attr75L.Width = 140;
                    this.attr75R.Width = 140;
                    this.attr90L.Width = 140;
                    this.attr90R.Width = 140;

                    break;
                case 1:     //BS_EN
                    this.attrJiaoLianL.Visible = false;
                    this.attrJiaoLianR.Visible = false;
                    this.attrHotL.Visible = true;
                    this.attrHotR.Visible = true;
                    this.attrCoolL.Visible = true;
                    this.attrCoolR.Visible = true;

                    this.attrHeat.Visible = false;
                    this.attrZhuiQiu68gL.Visible = false;
                    this.attrZhuiQiu68gR.Visible = false;
                    this.attrChuanTou44_2gL.Visible = false;
                    this.attrChuanTou44_2gR.Visible = false;

                    this.attr30L.Caption = "加熱";
                    this.attr30R.Caption = "冰凍";
                    break;
                case 2:     //AS_NZS
                    this.attrJiaoLianL.Visible = false;
                    this.attrJiaoLianR.Visible = false;
                    this.attrHotL.Visible = false;
                    this.attrHotR.Visible = false;
                    this.attrCoolL.Visible = false;
                    this.attrCoolR.Visible = false;
                    this.attr75L.Visible = false;
                    this.attr75R.Visible = false;
                    this.attr90L.Visible = false;
                    this.attr90R.Visible = false;


                    this.attr30L.Caption = "上框2Cm(左)";
                    this.attr30R.Caption = "上框2CM(右)";
                    break;
                default: break;
            }

            //XmlDocument xDoc = new XmlDocument();
            //string filePath = Application.StartupPath + "\\QualityCheck.config";
            //xDoc.Load(filePath);

            //Hashtable ansiHT = new Hashtable();
            //XmlNodeList ANSINodeList = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck").ChildNodes;
            ////冲击力度
            //ansiHT.Add(this.coBoxCJLD, ANSINodeList.Item(0).ChildNodes);
            //BindAllListBox(ansiHT);

            foreach (Model.Setting SET in new BL.SettingManager().SelectByName("ChongJiLiDao"))
            {
                this.coBoxCJLD.Properties.Items.Add(SET.SettingCurrentValue);
                if (!string.IsNullOrEmpty(SET.SettingDescription))
                    this.CJLD.Add(SET);
            }

            foreach (Model.Setting item in new BL.SettingManager().SelectByName("ChongJICeShiJiaRe"))
            {
                this.comBox_ChongJICeShiJiaRe.Properties.Items.Add(item.SettingCurrentValue);
                if (!string.IsNullOrEmpty(item.SettingDescription))
                    this.JIARE.Add(item);
            }

            foreach (Model.Setting item in new BL.SettingManager().SelectByName("ChongJiCeShiLengDong"))
            {
                this.comBox_ChongJICeShiLengDong.Properties.Items.Add(item.SettingCurrentValue);
                if (!string.IsNullOrEmpty(item.SettingDescription))
                    this.LENGDONG.Add(item);
            }

            foreach (Model.Setting SET in new BL.SettingManager().SelectByName("LieYinBiaoTouXinXi"))
            {
                this.coBoxLYBTXX.Properties.Items.Add(SET.SettingCurrentValue);
                if (!string.IsNullOrEmpty(SET.SettingDescription))
                    this.LYBTXX.Add(SET);
            }
            foreach (Model.Setting SET in new BL.SettingManager().SelectByName("ZhuiQiuZhongLiang"))
            {
                this.coBoxZQZL.Properties.Items.Add(SET.SettingCurrentValue);
                if (!string.IsNullOrEmpty(SET.SettingDescription))
                    this.ZQZL.Add(SET);
            }
        }

        private void BindAllListBox(Hashtable mHt)
        {
            foreach (DictionaryEntry de in mHt)
            {
                DevExpress.XtraEditors.ComboBoxEdit mcoBox = de.Key as DevExpress.XtraEditors.ComboBoxEdit;
                XmlNodeList mNodes = de.Value as XmlNodeList;
                mcoBox.Properties.Items.Clear();
                foreach (XmlNode nd in mNodes)
                {
                    mcoBox.Properties.Items.Add(nd.Attributes["value"].Value);
                }
                mcoBox.SelectedIndex = 0;
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action != "view")
            {
                switch (e.KeyValue)
                {
                    case 13:
                        this.AddDataRows();
                        break;
                    case 46:
                        if (this.bindingSource1.Current != null)
                        {
                            this._pcdic.Detail.Remove(this.bindingSource1.Current as Model.PCDoubleImpactCheckDetail);
                            if (this._pcdic.Detail.Count == 0)
                            {
                                this.AddDataRows();
                            }
                        }
                        break;
                    default:
                        break;
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        //选择加工单据
        private void btnGetPronoteHeader_Click(object sender, EventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(null, 0);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader currentModel = pronoForm.SelectItem;

                if (currentModel != null)
                {
                    this._pcdic.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._pcdic.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._pcdic.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._pcdic.ProductId = this._pcdic.Product.ProductId;
                    this._pcdic.CheckStandard = currentModel.CustomerCheckStandard;
                    this._pcdic.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity;
                    //this._pcdic.PCDoubleImpactCheckCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(currentModel.InvoiceXODetailQuantity) / 500));
                    if (pcType == 0)
                        this._pcdic.PCDoubleImpactCheckCount = Common.AutoCalculation.Calculation("csa", Convert.ToInt32(currentModel.InvoiceXODetailQuantity));
                    else if (pcType == 1)
                        this._pcdic.PCDoubleImpactCheckCount = Common.AutoCalculation.Calculation("en", Convert.ToInt32(currentModel.InvoiceXODetailQuantity));
                    else if (pcType == 2)
                        this._pcdic.PCDoubleImpactCheckCount = Common.AutoCalculation.Calculation("as", Convert.ToInt32(currentModel.InvoiceXODetailQuantity));

                    this._pcdic.ProductUnitId = this._pcdic.Product.QualityTestUnitId;
                    this._pcdic.ProductUnit = this._pcdic.Product.QualityTestUnit;


                    if (!string.IsNullOrEmpty(this._pcdic.CheckStandard))
                    {
                        if (this.CJLD.Any(d => d.SettingDescription == this._pcdic.CheckStandard))
                            this.coBoxCJLD.EditValue = this.CJLD.First(d => d.SettingDescription == this._pcdic.CheckStandard).SettingCurrentValue;
                        if (this.ZQZL.Any(d => d.SettingDescription == this._pcdic.CheckStandard))
                            this.coBoxZQZL.EditValue = this.ZQZL.First(d => d.SettingDescription == this._pcdic.CheckStandard).SettingCurrentValue;
                        if (this.LYBTXX.Any(d => d.SettingDescription == this._pcdic.CheckStandard))
                            this.coBoxLYBTXX.EditValue = this.LYBTXX.First(d => d.SettingDescription == this._pcdic.CheckStandard).SettingCurrentValue;
                        if (this.JIARE.Any(d => d.SettingDescription == this._pcdic.CheckStandard))
                            this.comBox_ChongJICeShiJiaRe.EditValue = this.JIARE.First(d => d.SettingDescription == this._pcdic.CheckStandard).SettingCurrentValue;
                        if (this.LENGDONG.Any(d => d.SettingDescription == this._pcdic.CheckStandard))
                            this.comBox_ChongJICeShiLengDong.EditValue = this.LENGDONG.First(d => d.SettingDescription == this._pcdic.CheckStandard).SettingCurrentValue;
                    }
                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        //搜索测试单据
        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm(this.pcType, this.Text);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCDoubleImpactCheck currentModel = form.SelectItem as Model.PCDoubleImpactCheck;
                if (currentModel != null)
                {
                    this._pcdic = currentModel;
                    this._pcdic = this._pcdicManage.GetDetail(this._pcdic);
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            this.AddDataRows();
            this.gridControl1.RefreshDataSource();
        }

        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this._pcdic.Detail.Remove(this.bindingSource1.Current as Model.PCDoubleImpactCheckDetail);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void txtPCDoubleImpactCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.txtPCDoubleImpactCheckDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCDoubleImpactCheck.PRO_PCDoubleImpactCheckID;
        }

        protected override int AuditState()
        {
            return this._pcdic.AuditState.HasValue ? this._pcdic.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCDoubleImpactCheck" + "," + this._pcdic.PCDoubleImpactCheckID;
        }

        #endregion

        //更改加热温度
        private void comBox_ChongJICeShiJiaRe_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.attrGridCol_JiaReWenDu.Caption = this.comBox_ChongJICeShiJiaRe.SelectedItem.ToString();
            this.attrHotL.Caption = this.attrHotL.Caption + this.comBox_ChongJICeShiJiaRe.SelectedItem.ToString();
            this.attrHotR.Caption = this.attrHotR.Caption + this.comBox_ChongJICeShiJiaRe.SelectedItem.ToString();
        }

        //更改冷冻温度
        private void comBox_ChongJICeShiLengDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.attrCoolL.Caption = this.attrCoolL.Caption + this.comBox_ChongJICeShiLengDong.SelectedItem.ToString();
            this.attrCoolL.Caption = this.attrCoolR.Caption + this.comBox_ChongJICeShiLengDong.SelectedItem.ToString();
        }

        private void coBoxZQZL_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(coBoxZQZL.Text))
            {
                this.attrZhuiQiu68gL.Caption = this.coBoxZQZL.SelectedItem.ToString() + "(左)";
                this.attrZhuiQiu68gR.Caption = this.coBoxZQZL.SelectedItem.ToString() + "(右)";
            }
        }

        private void repositoryItemLookUpEdit44_EditValueChanged(object sender, EventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();
            Model.PCDoubleImpactCheckDetail model = this.bindingSource1.Current as Model.PCDoubleImpactCheckDetail;
            if (model != null)
            {
                model.attrBiZhong = model.attrCoolL = model.attrCoolR = model.attrHeat30m = model.attrHeat60 = model.attrHotL = model.attrHotR = model.attrJiaoLianL = model.attrJiaoLianR = model.attrJPDownL = model.attrJPDownR = model.attrJPLeftL = model.attrJPLeftR = model.attrJPRightL = model.attrJPRightR = model.attrJPUpL = model.attrJPUpR = model.attrJPZYL = model.attrJPZYR = model.attrS_SShangL = model.attrS_SShangR = model.attrS_SXiaL = model.attrS_SXiaR = model.attrS_SZhongL = model.attrS_SZhongR = model.attrShangLiangL = model.attrShangLiangR = model.CheckAll;
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}
