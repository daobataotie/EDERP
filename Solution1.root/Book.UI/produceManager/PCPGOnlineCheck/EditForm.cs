using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Customs;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
using System.Reflection;
using DevExpress.XtraEditors.Controls;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        BL.PCPGOnlineCheckManager _pcpgocManager = new Book.BL.PCPGOnlineCheckManager();
        BL.PCPGOnlineCheckDetailManager _pcpgocDetailManager = new Book.BL.PCPGOnlineCheckDetailManager();

        Model.PCPGOnlineCheck _pcpgoc = null;
        int LastFlag = 0;

        List<ColumnHelper> listColumn = new List<ColumnHelper>();

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCPGOnlineCheck.PRO_PCPGOnlineCheckId, new AA(Properties.Resources.NumsIsNotNull, this.txtPCPGOnlineCheckId));
            this.requireValueExceptions.Add(Model.PCPGOnlineCheck.PRO_PCPGOnlineCheckDate, new AA(Properties.Resources.DateIsNull, this.DE_PCPGOnlineCheckDate));
            this.requireValueExceptions.Add(Model.PCPGOnlineCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));

            this.action = "view";
            this.nccEmployee0.Choose = new ChooseEmployee();
            //this.nccCHCustomer.Choose = new Book.UI.Settings.BasicData.Customs.ChooseCustoms();
            this.bindingSourceBusinessHours.DataSource = new BL.BusinessHoursManager().Select();
            this.nccWorkHouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();


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
                if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
                {
                    //if (this.gridView1.Columns[i].Name == "colattrUVChengFen" || this.gridView1.Columns[i].Name == "attrTouShiLv" || this.gridView1.Columns[i].Name == "attrFangWuMoYingDu" || this.gridView1.Columns[i].Name == "colattrQiangHuaMo" || this.gridView1.Columns[i].Name == "colattrZhePian" || this.gridView1.Columns[i].Name == "colattrMaoBian" || this.gridView1.Columns[i].Name == "ImpactCheck" || this.gridView1.Columns[i].Name == "attrExterior" || this.gridView1.Columns[i].Name == "attrDianDuPDSLv" || this.gridView1.Columns[i].Name == "attrDianDuBOLiTest" || this.gridView1.Columns[i].Name == "attrGaoDiJiaoL" || this.gridView1.Columns[i].Name == "attrGaoDiJiaoR" || this.gridView1.Columns[i].Name == "gridColumn1" || this.gridView1.Columns[i].Name == "colattrFangWuMoYingDu" || this.gridView1.Columns[i].Name == "colattrTouShiLv")
                    if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit && this.gridView1.Columns[i].Visible == true)
                    {
                        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).NullText = "";
                        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name",25, "標誌"),
                     });
                        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "name";
                        ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "name";

                        //获取此类型列的集合
                        listColumn.Add(new ColumnHelper
                        {
                            ColumnName = this.gridView1.Columns[i].Name,
                            ColumnCaption = this.gridView1.Columns[i].Caption,
                            ColumnFieldName = this.gridView1.Columns[i].FieldName
                        });
                    }
                }
            }

            #endregion

            this.ccob_AutoFillColumn.Properties.DataSource = listColumn;
            this.ccob_AutoFillColumn.Properties.DisplayMember = "ColumnCaption";
            this.ccob_AutoFillColumn.Properties.ValueMember = "ColumnFieldName";
        }

        public EditForm(string invoiceId)
            : this()
        {
            this._pcpgoc = this._pcpgocManager.GetDetail(invoiceId);
            if (this._pcpgoc == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCPGOnlineCheck pcpgoc)
            : this()
        {
            if (pcpgoc == null)
                throw new ArithmeticException("invoiceid");
            this._pcpgoc = pcpgoc;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCPGOnlineCheck pcpgoc, string action)
            : this()
        {
            this._pcpgoc = pcpgoc;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pcpgoc = new Book.Model.PCPGOnlineCheck();
            this._pcpgoc.PCPGOnlineCheckId = this._pcpgocManager.GetId();
            this._pcpgoc.PCPGOnlineCheckDate = DateTime.Now.Date;
            this._pcpgoc.PCPGOnlineCheckType = -1;
            this._pcpgoc.Employee = BL.V.ActiveOperator.Employee;
            this._pcpgoc.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            //初始化一条详细
            this._pcpgoc.Details = new List<Model.PCPGOnlineCheckDetail>();
            //this.AddDataRows();
        }

        protected override void Delete()
        {
            if (this._pcpgoc == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcpgocManager.Delete(this._pcpgoc);
            this._pcpgoc = this._pcpgocManager.GetNext(this._pcpgoc);
            if (this._pcpgoc == null)
            {
                this._pcpgoc = this._pcpgocManager.GetLast();
            }
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._pcpgoc = this._pcpgocManager.Get(this._pcpgocManager.GetLast() == null ? "" : this._pcpgocManager.GetLast().PCPGOnlineCheckId);
        }

        protected override void MoveFirst()
        {

            this._pcpgoc = this._pcpgocManager.Get(this._pcpgocManager.GetFirst() == null ? "" : this._pcpgocManager.GetFirst().PCPGOnlineCheckId);
        }

        protected override void MovePrev()
        {
            Model.PCPGOnlineCheck pcpgoc = this._pcpgocManager.GetPrev(this._pcpgoc);
            if (pcpgoc == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcpgoc = this._pcpgocManager.Get(pcpgoc.PCPGOnlineCheckId);
        }

        protected override void MoveNext()
        {
            Model.PCPGOnlineCheck pcpgoc = this._pcpgocManager.GetNext(this._pcpgoc);
            if (pcpgoc == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcpgoc = this._pcpgocManager.Get(pcpgoc.PCPGOnlineCheckId);
        }

        protected override bool HasRows()
        {
            return this._pcpgocManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pcpgocManager.HasRowsAfter(this._pcpgoc);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcpgocManager.HasRowsBefore(this._pcpgoc);
        }

        protected override void Save()
        {
            this._pcpgoc.PCPGOnlineCheckId = this.txtPCPGOnlineCheckId.Text;
            this._pcpgoc.PCPGOnlineCheckDesc = this.txtPCImpactCheckDesc.Text;
            this._pcpgoc.PCPGOnlineCheckDate = this.DE_PCPGOnlineCheckDate.DateTime;
            this._pcpgoc.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._pcpgoc.FromPCId = this.txtDatnJuBianHao.Text;
            this._pcpgoc.BusinessHoursId = this.lookUpEditBusinessHours.EditValue == null ? null : this.lookUpEditBusinessHours.EditValue.ToString();

            this._pcpgoc.Employee = (this.nccEmployee0.EditValue as Model.Employee);
            if (this._pcpgoc.Employee != null)
            {
                this._pcpgoc.EmployeeId = this._pcpgoc.Employee.EmployeeId;
            }
            //this._pcpgoc.Customer = (this.nccCHCustomer.EditValue as Model.Customer);
            //if (this._pcpgoc.Customer != null)
            //{
            //    this._pcpgoc.CustomerId = this._pcpgoc.Customer.CustomerId;
            //}
            this._pcpgoc.WorkHouse = (this.nccWorkHouse.EditValue as Model.WorkHouse);
            if (this._pcpgoc.WorkHouse != null)
            {
                this._pcpgoc.WorkHouseId = this._pcpgoc.WorkHouse.WorkHouseId;
            }

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this._pcpgocManager.Insert(this._pcpgoc);
                    break;
                case "update":
                    this._pcpgocManager.Update(this._pcpgoc);
                    break;
            }

        }

        public override void Refresh()
        {
            if (this._pcpgoc == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._pcpgoc = this._pcpgocManager.GetDetail(this._pcpgoc.PCPGOnlineCheckId);
                }
            }
            this.txtPCPGOnlineCheckId.Text = this._pcpgoc.PCPGOnlineCheckId;
            this.txtPCImpactCheckDesc.Text = this._pcpgoc.PCPGOnlineCheckDesc;
            this.DE_PCPGOnlineCheckDate.EditValue = this._pcpgoc.PCPGOnlineCheckDate.Value;
            this.txtInvoiceCusXOId.Text = this._pcpgoc.InvoiceCusXOId;
            //this.layoutDanJuBianHao.Text = this._pcpgoc.PCPGOnlineCheckType < 0 ? "加工單編號:" : "委外單編號:";
            this.txtDatnJuBianHao.Text = this._pcpgoc.FromPCId;
            //this.nccCHCustomer.EditValue = this._pcpgoc.Customer;
            this.nccWorkHouse.EditValue = this._pcpgoc.WorkHouse;
            //this.txtCheckStandard.Text = this._pcpgoc.Customer == null ? "" : this._pcpgoc.Customer.CheckedStandard;
            this.nccEmployee0.EditValue = this._pcpgoc.Employee;
            //this.txtProductName.Text = this._pcpgoc.Product == null ? "" : this._pcpgoc.Product.ToString();
            //this.txtProductDescription.Rtf = this._pcpgoc.Product == null ? "" : this._pcpgoc.Product.ProductDescription;
            this.calcInvoiceXOQuantity.EditValue = this._pcpgoc.InvoiceXOQuantity;

            this.newChooseContorlAuditEmp.EditValue = this._pcpgoc.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcpgoc.AuditState);
            this.lookUpEditBusinessHours.EditValue = this._pcpgoc.BusinessHoursId;

            this.bsPCPGOnlineCheckDetail.DataSource = this._pcpgoc.Details;

            base.Refresh();
            //switch (this.action)
            //{
            //    case "insert":
            //        this.gridView1.OptionsBehavior.Editable = true;
            //        break;
            //    case "update":
            //        this.gridView1.OptionsBehavior.Editable = true;
            //        break;
            //    case "view":
            //        this.gridView1.OptionsBehavior.Editable = false;
            //        break;
            //}

            this.txtPCPGOnlineCheckId.Properties.ReadOnly = true;
            this.calcInvoiceXOQuantity.Enabled = false;
            this.txtProductDescription.Enabled = false;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            if (!string.IsNullOrEmpty(this.gridView1.RowFilter))
            {
                return new Ro(this._pcpgoc, this.gridView1.RowFilter);
            }
            else
                return new Ro(this._pcpgoc);
        }

        private void AddDataRows()
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    Model.PCPGOnlineCheckDetail detail = null;
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.PCPGOnlineCheckDetail();
                        detail.PCPGOnlineCheckDetailId = Guid.NewGuid().ToString();
                        detail.PCPGOnlineCheckId = this._pcpgoc.PCPGOnlineCheckId;
                        detail.Product = new BL.ProductManager().Get(product.ProductId);
                        detail.ProductId = product.ProductId;
                        detail.PCPGOnlineCheckDetailDate = DateTime.Now;
                        detail.PCPGOnlineCheckDetailTime = DateTime.Now;

                        this._pcpgoc.Details.Add(detail);
                    }
                    this.bsPCPGOnlineCheckDetail.Position = this.bsPCPGOnlineCheckDetail.IndexOf(detail);
                    this.gridControl1.RefreshDataSource();
                }
            }
            f.Dispose();
            System.GC.Collect();
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            this.AddDataRows();
        }

        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (this.bsPCPGOnlineCheckDetail.Current != null)
            {
                if (this._pcpgoc.Details.Count > 0)
                {
                    this._pcpgoc.Details.Remove(this.bsPCPGOnlineCheckDetail.Current as Model.PCPGOnlineCheckDetail);
                    this.gridControl1.RefreshDataSource();
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ErrorNoMoreRows);
                }
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
                        if (this.bsPCPGOnlineCheckDetail.Current != null)
                        {
                            this._pcpgoc.Details.Remove(this.bsPCPGOnlineCheckDetail.Current as Model.PCPGOnlineCheckDetail);
                        }
                        else
                        {
                            MessageBox.Show(Properties.Resources.ErrorNoMoreRows);
                        }
                        break;
                    default:
                        break;
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        //加工单
        private void btnGetPronoteHeader_Click(object sender, EventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(this.nccWorkHouse.EditValue as Model.WorkHouse, 0);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                //this._pcpgoc.Details.Clear();
                foreach (var SelectModel in PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList)
                {
                    if (SelectModel != null)
                    {
                        this._pcpgoc.PCPGOnlineCheckType = -1;
                        Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(SelectModel.InvoiceXOId);
                        if (xo != null)
                        {
                            this._pcpgoc.InvoiceCusXOId = xo.CustomerInvoiceXOId;
                            //this._pcpgoc.FromPCId = SelectModel.PronoteHeaderID;
                            if (SelectModel.InvoiceXOId != null)
                                this._pcpgoc.Customer = xo.xocustomer;
                        }
                        //this._pcpgoc.CustomerId = this._pcpgoc.Customer == null ? "" : this._pcpgoc.CustomerId;
                        //this._pcpgoc.Product = new BL.ProductManager().Get(SelectModel.ProductId);
                        //this._pcpgoc.ProductId = SelectModel.ProductId;
                        //this._pcpgoc.InvoiceXOQuantity = SelectModel.InvoiceXODetailQuantity;

                        //Controls
                        this.txtInvoiceCusXOId.Text = this._pcpgoc.InvoiceCusXOId;
                        //this.nccCHCustomer.EditValue = this._pcpgoc.Customer;
                        //this.txtDatnJuBianHao.Text = this._pcpgoc.FromPCId;
                        //this.txtCheckStandard.Text = this._pcpgoc.Customer == null ? "" : this._pcpgoc.Customer.CheckedStandard;
                        //this.txtProductName.Text = this._pcpgoc.Product == null ? "" : this._pcpgoc.Product.ToString();
                        //this.calcInvoiceXOQuantity.EditValue = this._pcpgoc.InvoiceXOQuantity;
                        //this.txtProductDescription.Rtf = this._pcpgoc.Product == null ? "" : this._pcpgoc.Product.ProductDescription;
                        //Detail
                        Model.PCPGOnlineCheckDetail d = new Book.Model.PCPGOnlineCheckDetail();
                        d.PCPGOnlineCheckDetailId = Guid.NewGuid().ToString();
                        d.PCPGOnlineCheckId = this._pcpgoc.PCPGOnlineCheckId;
                        d.InvoiceCusXOId = xo == null ? "" : xo.CustomerInvoiceXOId;
                        d.PCPGOnlineCheckDetailDate = DateTime.Now;
                        //d.PCPGOnlineCheckDetailTime = DateTime.Now;
                        d.ProductId = SelectModel.ProductId;
                        d.Product = new BL.ProductManager().Get(d.ProductId);
                        d.CheckQuantity = Convert.ToInt32(SelectModel.DetailsSum);
                        d.FromInvoiceId = SelectModel.PronoteHeaderID;
                        if (this._pcpgoc.Customer != null)
                            d.CheckedStandard = this._pcpgoc.Customer.CheckedStandard;

                        this._pcpgoc.Details.Add(d);
                    }
                }
                this.gridControl1.RefreshDataSource();

            }
            pronoForm.Dispose();
            GC.Collect();
        }

        //委外单
        private void btnGetOtherPacmt_Click(object sender, EventArgs e)
        {
            ProduceOtherCompact.ChooseOutContract f = new ProduceOtherCompact.ChooseOutContract();
            if (f.ShowDialog(this) != DialogResult.OK) return;

            Model.ProduceOtherCompact OtherCompact = f.SelectItem as Model.ProduceOtherCompact;
            if (OtherCompact != null)
            {
                //this._pcpgoc.Details.Clear();
                foreach (var item in f.key)
                {
                    this._pcpgoc.PCPGOnlineCheckType = 1;
                    //this.layoutDanJuBianHao.Text = "委外單編號:";
                    this._pcpgoc.FromPCId = OtherCompact.ProduceOtherCompactId;
                    this._pcpgoc.Customer = OtherCompact.Customer;
                    //this._pcpgoc.CustomerId = OtherCompact.CustomerId;
                    this._pcpgoc.InvoiceCusXOId = OtherCompact.InvoiceXO == null ? "" : OtherCompact.InvoiceXO.CustomerInvoiceXOId;
                    //this._pcpgoc.Product = f.key[0].Product;
                    //this._pcpgoc.ProductId = f.key[0].ProductId;

                    //对控件进行赋值
                    this.txtInvoiceCusXOId.Text = OtherCompact.InvoiceXOId;
                    //this.nccCHCustomer.EditValue = this._pcpgoc.Customer;
                    //this.txtDatnJuBianHao.Text = OtherCompact.ProduceOtherCompactId;
                    //this.txtCheckStandard.Text = this._pcpgoc.Customer == null ? "" : this._pcpgoc.Customer.CheckedStandard;
                    //this.txtProductName.Text = this._pcpgoc.Product == null ? "" : this._pcpgoc.Product.ToString();
                    //this.txtProductDescription.Rtf = this._pcpgoc.Product == null ? "" : this._pcpgoc.Product.ProductDescription;

                    //foreach (Model.ProduceOtherCompactDetail item in f.key)
                    //{
                    Model.PCPGOnlineCheckDetail d = new Book.Model.PCPGOnlineCheckDetail();
                    d.PCPGOnlineCheckDetailId = Guid.NewGuid().ToString();
                    d.PCPGOnlineCheckId = this._pcpgoc.PCPGOnlineCheckId;
                    d.InvoiceCusXOId = OtherCompact.InvoiceXO == null ? "" : OtherCompact.InvoiceXO.CustomerInvoiceXOId;
                    d.PCPGOnlineCheckDetailDate = DateTime.Now;
                    d.PCPGOnlineCheckDetailTime = DateTime.Now;
                    d.ProductId = item.ProductId;
                    d.Product = new BL.ProductManager().Get(d.ProductId);
                    //d.ProductDesc = item.Product.ProductDescription;
                    //d.ProductXingHao = item.Product.ProductSpecification;
                    d.CheckQuantity = Convert.ToInt32(item.OtherCompactCount);
                    d.FromInvoiceId = item.ProduceOtherCompactId;
                    d.CheckedStandard = this._pcpgoc.Customer == null ? "" : this._pcpgoc.Customer.CheckedStandard;
                    this._pcpgoc.Details.Add(d);
                    //}
                }
                this.gridControl1.RefreshDataSource();

            }
            f.Dispose();
            GC.Collect();
            #region 委外入库
            //ProduceOtherInDepot.ChooseProduceOtherInDepotForPCO f = new Book.UI.produceManager.ProduceOtherInDepot.ChooseProduceOtherInDepotForPCO();

            //if (f.ShowDialog(this) != DialogResult.OK) return;

            //Model.ProduceOtherInDepotDetail detail = f.SelectItems.Count > 0 ? f.SelectItems[0] : null;
            //if (detail != null)
            //{
            //    //清空数据
            //    //if (this._pcpgoc.PCPGOnlineCheckType < 0)
            //    //{
            //    this._pcpgoc.Details.Clear();
            //    //}
            //    this._pcpgoc.PCPGOnlineCheckType = 1;
            //    this.layoutDanJuBianHao.Text = "委外入庫單編號:";

            //    this._pcpgoc.FromPCId = detail.ProduceOtherInDepotId;       //来源单据
            //    this._pcpgoc.Customer = detail.Customer;
            //    this._pcpgoc.CustomerId = detail.CustomerId;
            //    this._pcpgoc.InvoiceCusXOId = detail.InvoiceCusId;  //取得头里的客户订单编号
            //    this._pcpgoc.Product = detail.Product;
            //    this._pcpgoc.ProductId = detail.ProductId;

            //    //对控件进行赋值
            //    this.txtInvoiceCusXOId.Text = this._pcpgoc.InvoiceCusXOId;
            //    this.nccCHCustomer.EditValue = this._pcpgoc.Customer;
            //    this.txtDatnJuBianHao.Text = this._pcpgoc.FromPCId;     //来源单据编号.委外入库详细头编号
            //    this.txtCheckStandard.Text = this._pcpgoc.Customer == null ? "" : this._pcpgoc.Customer.CheckedStandard;
            //    this.txtProductName.Text = this._pcpgoc.Product == null ? "" : this._pcpgoc.Product.ToString();

            //    //赋值详细
            //    Model.PCPGOnlineCheckDetail d = new Book.Model.PCPGOnlineCheckDetail();
            //    d.PCPGOnlineCheckDetailId = Guid.NewGuid().ToString();
            //    d.PCPGOnlineCheckId = this._pcpgoc.PCPGOnlineCheckId;
            //    d.InvoiceCusXOId = detail.InvoiceCusId;
            //    d.PCPGOnlineCheckDetailDate = DateTime.Now;
            //    d.PCPGOnlineCheckDetailTime = DateTime.Now;
            //    //d.Product = detail.Product;
            //    //d.ProductId = detail.ProductId;
            //    //d.ProductDesc = item.Product.ProductDescription;
            //    //d.ProductXingHao = item.Product.ProductSpecification;
            //    d.CheckQuantity = detail.ProduceInDepotQuantity.HasValue ? int.Parse(detail.ProduceInDepotQuantity.Value.ToString()) : 0;
            //    d.FromInvoiceId = detail.ProduceOtherInDepotId;



            //    this._pcpgoc.Details.Add(d);

            //    this.gridControl1.RefreshDataSource();
            //}
            //f.Dispose();
            //GC.Collect();
            #endregion
        }

        //采购单
        private void btn_invoiceCO_Click(object sender, EventArgs e)
        {
            Invoices.CG.CGForm f = new Book.UI.Invoices.CG.CGForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //this._pcpgoc.Details.Clear();
                foreach (var SelectModel in f.key)
                {
                    if (SelectModel != null)
                    {
                        this._pcpgoc.PCPGOnlineCheckType = 0;

                        Model.InvoiceXO xo = null;
                        if (SelectModel.Invoice.InvoiceXOId != null)
                        {
                            xo = new BL.InvoiceXOManager().Get(SelectModel.Invoice.InvoiceXOId);
                            this._pcpgoc.Customer = xo == null ? null : xo.xocustomer;
                            this._pcpgoc.InvoiceCusXOId = xo == null ? "" : xo.CustomerInvoiceXOId;
                        }

                        this.txtInvoiceCusXOId.Text = this._pcpgoc.InvoiceCusXOId;

                        //Detail
                        Model.PCPGOnlineCheckDetail d = new Book.Model.PCPGOnlineCheckDetail();
                        d.PCPGOnlineCheckDetailId = Guid.NewGuid().ToString();
                        d.PCPGOnlineCheckId = this._pcpgoc.PCPGOnlineCheckId;
                        if (SelectModel.Invoice.InvoiceXOId != null)
                            d.InvoiceCusXOId = xo == null ? "" : xo.CustomerInvoiceXOId;
                        d.PCPGOnlineCheckDetailDate = DateTime.Now;
                        //d.PCPGOnlineCheckDetailTime = DateTime.Now;
                        d.ProductId = SelectModel.ProductId;
                        d.Product = SelectModel.Product;
                        d.CheckQuantity = Convert.ToInt32(SelectModel.OrderQuantity);
                        d.FromInvoiceId = SelectModel.InvoiceId;
                        if (this._pcpgoc.Customer != null)
                            d.CheckedStandard = this._pcpgoc.Customer.CheckedStandard;

                        this._pcpgoc.Details.Add(d);
                    }
                }
                this.gridControl1.RefreshDataSource();

            }
            f.Dispose();
            GC.Collect();
        }

        //查询
        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                //Model.PCPGOnlineCheck currentModel = form.SelectItem as Model.PCPGOnlineCheck;
                DataRowView dv = form.SelectItem as DataRowView;
                if (dv != null)
                {
                    //this._pcpgoc = currentModel;
                    this._pcpgoc = this._pcpgocManager.GetDetail(dv.Row[0].ToString());
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            foreach (Model.Setting set in new BL.SettingManager().SelectByName("waiguan"))
            {
                this.GVCattrExterior.Items.Add(set.SettingCurrentValue);
            }
            foreach (Model.Setting set in new BL.SettingManager().SelectByName("boli"))
            {
                this.GVCattrDianDuBOLiTest.Items.Add(set.SettingCurrentValue);
            }
            foreach (Model.Setting set in new BL.SettingManager().SelectByName("jinsong"))
            {
                this.GVCattrZhuangJiaoSJD.Items.Add(set.SettingCurrentValue);
            }
            foreach (Model.Setting set in new BL.SettingManager().SelectByName("toushi"))
            {
                this.GVCattrDianDuPDSLv.Items.Add(set.SettingCurrentValue);
            }
            foreach (Model.Setting set in new BL.SettingManager().SelectByName("gaojiaor"))
            {
                this.GVCattrGaoDiJiaoL.Items.Add(set.SettingCurrentValue);
            }
            foreach (Model.Setting set in new BL.SettingManager().SelectByName("gaojiaol"))
            {
                this.GVCattrGaoDiJiaoR.Items.Add(set.SettingCurrentValue);
            }
        }

        private void txtPCImpactCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.txtPCImpactCheckDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        private void ItemHyperLinkGuangXue_Click(object sender, EventArgs e)
        {
            //Model.PCPGOnlineCheckDetail d = (this.bsPCPGOnlineCheckDetail.Current as Model.PCPGOnlineCheckDetail);
            //if (d != null)
            //{
            //    OpticsTest f = new OpticsTest(d.PCPGOnlineCheckDetailId);
            //    f.ShowDialog();
            //}
        }

        private void ItemHyperLinkHouDu_Click(object sender, EventArgs e)
        {

        }

        //条件打印
        private void barBtnContionPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                IList<Model.PCPGOnlineCheck> list = this._pcpgocManager.SelectByDateRage(condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd);
                if (list.Count < 1)
                {
                    MessageBox.Show("無數據！", this.Text, MessageBoxButtons.OK);
                    return;
                }
                RoList ro = new RoList(list);
                ro.ShowPreviewDialog();
            }
            f.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCPGOnlineCheck.PRO_PCPGOnlineCheckId;
        }

        protected override int AuditState()
        {
            return this._pcpgoc.AuditState.HasValue ? this._pcpgoc.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCPGOnlineCheck" + "," + this._pcpgoc.PCPGOnlineCheckId;
        }

        #endregion

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
        }

        //光学
        private void ItemHyperLinkGuangXue_Click_1(object sender, EventArgs e)
        {
            Model.PCPGOnlineCheckDetail d = (this.bsPCPGOnlineCheckDetail.Current as Model.PCPGOnlineCheckDetail);
            if (d != null)
            {
                OpticsTest f = new OpticsTest(d.PCPGOnlineCheckDetailId);
                f.ShowDialog();
            }
        }

        //厚度
        private void ItemHyperLinkHouDu_Click_1(object sender, EventArgs e)
        {
            Model.PCPGOnlineCheckDetail d = (this.bsPCPGOnlineCheckDetail.Current as Model.PCPGOnlineCheckDetail);
            if (d != null)
            {
                ThicknessTest f = new ThicknessTest(d.PCPGOnlineCheckDetailId);
                f.ShowDialog();
            }
        }

        private void repositoryItemHyperLinkEdit1_Click_1(object sender, EventArgs e)
        {

            Model.PCPGOnlineCheckDetail d = (this.bsPCPGOnlineCheckDetail.Current as Model.PCPGOnlineCheckDetail);
            if (d != null)
            {
                ProduceOtherCompact.EditForm f;
                PronoteHeader.EditForm f1;
                Invoices.CO.EditForm f2;
                if (this._pcpgoc.PCPGOnlineCheckType == 1)
                {
                    if (new BL.ProduceOtherCompactManager().Get(d.FromInvoiceId) != null)
                    {
                        f = new Book.UI.produceManager.ProduceOtherCompact.EditForm(new BL.ProduceOtherCompactManager().Get(d.FromInvoiceId));
                        f.ShowDialog();
                    }
                }
                else if (this._pcpgoc.PCPGOnlineCheckType == -1)
                {
                    if (new BL.PronoteHeaderManager().Get(d.FromInvoiceId) != null)
                    {
                        f1 = new Book.UI.produceManager.PronoteHeader.EditForm(new BL.PronoteHeaderManager().Get(d.FromInvoiceId));
                        f1.ShowDialog();
                    }
                }
                else if (this._pcpgoc.PCPGOnlineCheckType == 0)
                {
                    if (new BL.InvoiceCOManager().Get(d.FromInvoiceId) != null)
                    {
                        f2 = new Book.UI.Invoices.CO.EditForm(new BL.InvoiceCOManager().Get(d.FromInvoiceId));
                        f2.ShowDialog();
                    }
                }

            }
        }

        private void btn_AutoFill_Click(object sender, EventArgs e)
        {
            List<PropertyInfo> listProInfo = new List<PropertyInfo>();

            foreach (CheckedListBoxItem item in this.ccob_AutoFillColumn.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    PropertyInfo pi = new Book.Model.PCPGOnlineCheckDetail().GetType().GetProperty(item.Value.ToString());
                    if (pi != null)
                        listProInfo.Add(pi);
                }
            }
            var detailList = this.bsPCPGOnlineCheckDetail.DataSource as IList<Model.PCPGOnlineCheckDetail>;
            if (detailList != null && detailList.Count > 0)
            {
                foreach (var detail in detailList)
                {
                    foreach (var item in listProInfo)
                    {
                        item.SetValue(detail, "√", null);
                    }
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void btn_AutoClean_Click(object sender, EventArgs e)
        {
            List<PropertyInfo> listProInfo = new List<PropertyInfo>();

            foreach (CheckedListBoxItem item in this.ccob_AutoFillColumn.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    PropertyInfo pi = new Book.Model.PCPGOnlineCheckDetail().GetType().GetProperty(item.Value.ToString());
                    if (pi != null)
                        listProInfo.Add(pi);
                }
            }
            var detailList = this.bsPCPGOnlineCheckDetail.DataSource as IList<Model.PCPGOnlineCheckDetail>;
            if (detailList != null && detailList.Count > 0)
            {
                foreach (var detail in detailList)
                {
                    foreach (var item in listProInfo)
                    {
                        item.SetValue(detail, null, null);
                    }
                }

                this.gridControl1.RefreshDataSource();
            }
        }

    }

    public class ColumnHelper
    {
        public string ColumnName { get; set; }

        public string ColumnCaption { get; set; }

        public string ColumnFieldName { get; set; }
    }
}
