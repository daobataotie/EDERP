using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class ROList : DevExpress.XtraReports.UI.XtraReport
    {
        public ROList()
        {
            InitializeComponent();
        }

        public ROList(IList<Model.ProduceInDepotDetail> details)
            : this()
        {
            //Controls
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.ProduceInDepot + "ап╠М";
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            var cusid = from n in details
                        group n by new { n.CusXOId } into nn
                        select new { id = nn.Key.CusXOId };

            foreach (var item in cusid)
            {
                this.lblInvoiceCusId.Text += item.id + ",";
            }
            if (this.lblInvoiceCusId.Text.Length > 0)
                this.lblInvoiceCusId.Text = this.lblInvoiceCusId.Text.Substring(0, this.lblInvoiceCusId.Text.Length - 1);
            //Details
            this.DataSource = details;

            this.TCRiQi.DataBindings.Add("Text", this.DataSource, "mProduceInDepotDate", "{0:yyyy-MM-dd}");
            this.TCPinMing.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProductName);
            this.TCRuKuDanHao.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceInDepotId);
            this.TCJiaGongDanHao.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_PronoteHeaderId);
            this.TCBuMeng.DataBindings.Add("Text", this.DataSource, "WorkHousename");
            this.TCShenChanShuLiang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProceduresSum);
            this.TCHeGeShuLiang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_CheckOutSum);
            this.TCZhuanShenChanShuLiang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceTransferQuantity);
            this.TCRuKuShuLiang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceQuantity);
            this.TCInvoiceQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_PronoteHeaderSum);
        }
    }
}
