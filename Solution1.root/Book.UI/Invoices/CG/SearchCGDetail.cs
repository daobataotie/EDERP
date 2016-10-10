using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
namespace Book.UI.Invoices.CG
{
    public partial class SearchCGDetail : Book.UI.Settings.BasicData.BaseChooseForm
    {
        BL.InvoiceCGManager _mInvoiceCGManager = new Book.BL.InvoiceCGManager();
        BL.InvoiceCGDetailManager _mInvoiceCGDetailManager = new Book.BL.InvoiceCGDetailManager();
        public IList<Model.InvoiceCGDetail> selectItems = new List<Model.InvoiceCGDetail>();

        public SearchCGDetail()
        {
            InitializeComponent();
            this.manager = new BL.InvoiceCGDetailManager();
            this.dateEditStart.DateTime = DateTime.Now.Date.AddDays(-15);
            this.dateEditEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.newChooseSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
        }

        protected override void LoadData()
        {
            this.bsHeader.DataSource = this._mInvoiceCGManager.Select(null, null, null, null, this.dateEditStart.DateTime, this.dateEditEnd.DateTime, null, null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null);//非2 显示全部
            if (this.bsHeader.Current != null)
            {
                Model.InvoiceCG icg = this.bsHeader.Current as Model.InvoiceCG;
                IList<Model.InvoiceCGDetail> icgd = _mInvoiceCGDetailManager.Select(icg);
                (this.bsHeader.Current as Model.InvoiceCG).Details = icgd;
                //this.SelectAll((this.bsHeader.Current as Model.InvoiceCG).Details);
                this.bindingSource1.DataSource = (this.bsHeader.Current as Model.InvoiceCG).Details;
            }
        }

        public override void simpleButtonOK_Click(object sender, EventArgs e)
        {
            //if ((this.bsHeader.Current as Model.InvoiceCG) != null)
            //    this.selectItems = (from i in (this.bsHeader.Current as Model.InvoiceCG).Details
            //                        where i.Checked == true
            //                        select i).ToList<Model.InvoiceCGDetail>();
            foreach (var h in this.bsHeader.DataSource as List<Model.InvoiceCG>)
            {
                foreach (var d in h.Details.Where(w => w.Checked == true))
                {
                    if (!selectItems.Any(a => a.InvoiceCGDetailId == d.InvoiceCGDetailId))
                        selectItems.Add(d);
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceCGDetail> details = this.bindingSource1.DataSource as IList<Model.InvoiceCGDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            Model.InvoiceCODetail codetail = details[e.ListSourceRowIndex].InvoiceCODetail;
            switch (e.Column.Name)
            {
                case "colProductId":
                    if (detail == null) return;
                    e.DisplayText = detail.Id;
                    break;
                case "gridColumn18":
                    if (codetail != null)
                        e.DisplayText = codetail.NoArrivalQuantity.Value.ToString("0.####");
                    break;
                case "gridColumn17":
                    if (codetail != null)
                        e.DisplayText = codetail.OrderQuantity.Value.ToString();
                    break;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //this.LoadData();
            this.bsHeader.DataSource = this._mInvoiceCGManager.Select(null, null, this.newChooseSupplier.EditValue as Model.Supplier, this.newChooseSupplier.EditValue as Model.Supplier, this.dateEditStart.DateTime, this.dateEditEnd.DateTime, null, null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null);//非2 显示全部
            if (this.bsHeader.Current != null)
            {
                Model.InvoiceCG icg = this.bsHeader.Current as Model.InvoiceCG;
                IList<Model.InvoiceCGDetail> icgd = _mInvoiceCGDetailManager.Select(icg);
                (this.bsHeader.Current as Model.InvoiceCG).Details = icgd;
                //this.SelectAll((this.bsHeader.Current as Model.InvoiceCG).Details);
                this.bindingSource1.DataSource = (this.bsHeader.Current as Model.InvoiceCG).Details;
            }
        }

        //更改查询周期
        private void btnChangeSearch_Click(object sender, EventArgs e)
        {
            Query.ConditionCOChooseForm f = new Query.ConditionCOChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionCO con = f.Condition as Query.ConditionCO;
                this.dateEditStart.DateTime = Convert.ToDateTime(con.StartInvoiceDate);
                this.dateEditEnd.DateTime = Convert.ToDateTime(con.EndInvoiceDate);

                this.bsHeader.DataSource = this._mInvoiceCGManager.Select(con.COStartId, con.COEndId, con.SupplierStart, con.SupplierEnd, Convert.ToDateTime(con.StartInvoiceDate), Convert.ToDateTime(con.EndInvoiceDate), con.ProductStart, con.ProductEnd, con.CusXOId, con.StartJHDate, con.EndJHDate, con.InvoiceCGIdStart, con.InvoiceCGIdEnd);
                if (this.bsHeader.Current != null)
                {
                    Model.InvoiceCG icg = this.bsHeader.Current as Model.InvoiceCG;
                    IList<Model.InvoiceCGDetail> icgd = _mInvoiceCGDetailManager.Select(icg);
                    (this.bsHeader.Current as Model.InvoiceCG).Details = icgd;
                    //this.SelectAll((this.bsHeader.Current as Model.InvoiceCG).Details);
                    this.bindingSource1.DataSource = (this.bsHeader.Current as Model.InvoiceCG).Details;
                }
            }
            f.Dispose();
            GC.Collect();
        }

        private void bsHeader_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bsHeader.Current != null)
            {
                if ((this.bsHeader.Current as Model.InvoiceCG).Details.Count == 0)
                {
                    Model.InvoiceCG icg = this.bsHeader.Current as Model.InvoiceCG;
                    IList<Model.InvoiceCGDetail> icgd = _mInvoiceCGDetailManager.Select(icg);
                    (this.bsHeader.Current as Model.InvoiceCG).Details = icgd;
                }
                //this.SelectAll((this.bsHeader.Current as Model.InvoiceCG).Details);
                this.bindingSource1.DataSource = (this.bsHeader.Current as Model.InvoiceCG).Details;
            }
        }

        //全选
        private void SelectAll(IList<Model.InvoiceCGDetail> detail)
        {
            foreach (Model.InvoiceCGDetail icgd in detail)
            {
                icgd.Checked = true;
            }
        }
    }
}
