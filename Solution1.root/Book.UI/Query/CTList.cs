using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data;

namespace Book.UI.Query
{
    public partial class CTList : DevExpress.XtraReports.UI.XtraReport
    {
        BL.InvoiceCTDetailManager manager = new Book.BL.InvoiceCTDetailManager();
        public CTList()
        {
            InitializeComponent();
        }

        public CTList(Invoices.CT.Condition condition)
            : this()
        {
            this.lblReportName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            DataTable dt = manager.SelectByCondition(condition.StartDate, condition.EndDate, condition.StartCTId, condition.EndCTId, condition.StartCOId, condition.EndCOId, condition.CusId, condition.SupplierId);

            if (dt == null || dt.Rows.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.DataSource = dt;

            TCInvoiceId.DataBindings.Add("Text",this.DataSource,Model.InvoiceCTDetail.PRO_InvoiceId);
            TCInvoiceDate.DataBindings.Add("Text", this.DataSource, "InvoiceDate","{0:yyyy-MM-dd}");
            TCSupplier.DataBindings.Add("Text", this.DataSource, "SupplierFullName");
            TCProduct.DataBindings.Add("Text", this.DataSource, "ProductName");
            TCQuantity.DataBindings.Add("Text",this.DataSource,Model.InvoiceCTDetail.PRO_InvoiceCTDetailQuantity,"{0:0.00}");
            TCUnit.DataBindings.Add("Text",this.DataSource,Model.InvoiceCTDetail.PRO_InvoiceProductUnit);
            TCPrice.DataBindings.Add("Text",this.DataSource,Model.InvoiceCTDetail.PRO_InvoiceCTDetailPrice,"{0:0.00}");
            TCAmount.DataBindings.Add("Text",this.DataSource,Model.InvoiceCTDetail.PRO_InvoiceCTDetailMoney0,"{0:0.00}");
        }
    }
}
