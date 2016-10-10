using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class FPRO1 : DevExpress.XtraReports.UI.XtraReport
    {
        public FPRO1()
        {
            InitializeComponent();
        }

        public FPRO1(IList<Model.AcInvoiceXOBillDetail> list)
            : this()
        {
            this.DataSource = list;
            TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            TCQuantity.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetaiInQuantity, "{0:0.##}");
            lblUnit.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceProductUnit);
            TCPrice.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailPrice, "{0:0.###}");
            TCMoney.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailMoney, "{0:0.###}");
        }
    }
}
