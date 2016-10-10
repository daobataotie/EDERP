using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class FPRO : DevExpress.XtraReports.UI.XtraReport
    {
        public FPRO()
        {
            InitializeComponent();
        }
        IList<Model.AcInvoiceXOBillDetail> list;
        public FPRO(Model.AcInvoiceXOBill model)
            : this()
        {
            this.DataSource = list = model.Details;
            if (model.AcInvoiceXOBillDate.HasValue)
            {
                this.lblYear.Text = (model.AcInvoiceXOBillDate.Value.Year - 1911).ToString();
                this.lblMonth.Text = model.AcInvoiceXOBillDate.Value.Month.ToString();
                this.lblDay.Text = model.AcInvoiceXOBillDate.Value.Day.ToString();
            }

            this.lblFPNo.Text = model.Id;
            //¼ì²éÂë
            if (!string.IsNullOrEmpty(model.Id))
            {
                string englishid1 = model.Id.Substring(0, 1);
                string englishid2 = model.Id.Substring(1, 1);

                decimal x = Math.Truncate((decimal)(((int)System.Text.Encoding.ASCII.GetBytes(englishid1)[0] * (int)System.Text.Encoding.ASCII.GetBytes(englishid2)[0]) / 2)) % 10;

                string str = model.Id.Substring(2);
                decimal y = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    y += Convert.ToInt32(str.Substring(i, 1)) * (i + 1);
                }
                y = y % 10;

                this.lblCheckNo.Text = x.ToString() + y.ToString();
            }
            //if (!string.IsNullOrEmpty(model.Id))
            //{
            //    int x = Convert.ToInt32((Convert.ToInt32(model.Id.Substring(2)) + 84562911).ToString().Substring((Convert.ToInt32(model.Id.Substring(2)) + 84562911).ToString().Length - 8));
            //    string str = x.ToString();
            //    int y = Convert.ToInt32(str.Substring(0, 2)) + Convert.ToInt32(str.Substring(2, 2)) + Convert.ToInt32(str.Substring(4, 2)) + Convert.ToInt32(str.Substring(6, 2));
            //    this.lblCheckNo.Text = (100 - y % 100).ToString();
            //}
            if (model.CustomerShouPiao != null)
            {
                this.lblBuyer.Text = model.CustomerShouPiao.CustomerFullName;
                this.lblUniteNo.Text = model.CustomerShouPiao.CustomerNumber;
                this.lblAddress.Text = model.CustomerShouPiao.CustomerAddress;
            }

            string s = "";
            foreach (var item in model.Details)
            {
                if (!string.IsNullOrEmpty(item.InvoiceCOIdNote))
                {
                    //string[] str = item.InvoiceCOIdNote.Split(',');
                    //foreach (var id in str)
                    //{
                    if (!s.Contains(item.InvoiceCOIdNote))
                        s += item.InvoiceCOIdNote + ",";
                    //}
                }
            }
            if (s.Length >= 1)
                this.lblNote.Text = s.Substring(0, s.Length - 1);

            lblHeji.Text = model.HeJiMoney.HasValue ? model.HeJiMoney.Value.ToString("0.##") : "";
            lblTax.Text = model.TaxRateMoney.HasValue ? model.TaxRateMoney.Value.ToString("0.##") : "";
            lblTotal.Text = model.ZongMoney.HasValue ? model.ZongMoney.Value.ToString("0.##") : "";
            switch (model.TaxRateType)
            {
                case 0:
                    this.lblNoTax.Text = "¡Ì";
                    break;
                case 1:
                    this.lblShouldTax.Text = "¡Ì";
                    break;
                case 2:
                    this.lblZeroTax.Text = "¡Ì";
                    break;
            }
            string totalmoney = (model.ZongMoney.HasValue ? model.ZongMoney.Value : 0).ToString("##########");
            string ge = "", shi = "", bai = "", qian = "", wan = "", shiwan = "", baiwan = "", qianwan = "";
            //if (totalmoney.Length > 8)
            //{
            //    ge = totalmoney.Substring(totalmoney.Length - 1);
            //    shi = totalmoney.Substring(totalmoney.Length - 2, 1);
            //    bai = totalmoney.Substring(totalmoney.Length - 3, 1);
            //    qian = totalmoney.Substring(totalmoney.Length - 4, 1);
            //    wan = totalmoney.Substring(totalmoney.Length - 5, 1);
            //    shiwan = totalmoney.Substring(totalmoney.Length - 6, 1);
            //    baiwan = totalmoney.Substring(totalmoney.Length - 7, 1);
            //    qianwan = totalmoney.Substring(0, totalmoney.Length - 8 + 1);
            //}
            if (totalmoney.Length == 8)
            {
                ge = totalmoney.Substring(totalmoney.Length - 1);
                shi = totalmoney.Substring(totalmoney.Length - 2, 1);
                bai = totalmoney.Substring(totalmoney.Length - 3, 1);
                qian = totalmoney.Substring(totalmoney.Length - 4, 1);
                wan = totalmoney.Substring(totalmoney.Length - 5, 1);
                shiwan = totalmoney.Substring(totalmoney.Length - 6, 1);
                baiwan = totalmoney.Substring(totalmoney.Length - 7, 1);
                qianwan = totalmoney.Substring(totalmoney.Length - 8, 1);
            }
            else if (totalmoney.Length == 7)
            {
                ge = totalmoney.Substring(totalmoney.Length - 1);
                shi = totalmoney.Substring(totalmoney.Length - 2, 1);
                bai = totalmoney.Substring(totalmoney.Length - 3, 1);
                qian = totalmoney.Substring(totalmoney.Length - 4, 1);
                wan = totalmoney.Substring(totalmoney.Length - 5, 1);
                shiwan = totalmoney.Substring(totalmoney.Length - 6, 1);
                baiwan = totalmoney.Substring(totalmoney.Length - 7, 1);
            }
            else if (totalmoney.Length == 6)
            {
                ge = totalmoney.Substring(totalmoney.Length - 1);
                shi = totalmoney.Substring(totalmoney.Length - 2, 1);
                bai = totalmoney.Substring(totalmoney.Length - 3, 1);
                qian = totalmoney.Substring(totalmoney.Length - 4, 1);
                wan = totalmoney.Substring(totalmoney.Length - 5, 1);
                shiwan = totalmoney.Substring(totalmoney.Length - 6, 1);
            }
            else if (totalmoney.Length == 5)
            {
                ge = totalmoney.Substring(totalmoney.Length - 1);
                shi = totalmoney.Substring(totalmoney.Length - 2, 1);
                bai = totalmoney.Substring(totalmoney.Length - 3, 1);
                qian = totalmoney.Substring(totalmoney.Length - 4, 1);
                wan = totalmoney.Substring(totalmoney.Length - 5, 1);
            }
            else if (totalmoney.Length == 4)
            {
                ge = totalmoney.Substring(totalmoney.Length - 1);
                shi = totalmoney.Substring(totalmoney.Length - 2, 1);
                bai = totalmoney.Substring(totalmoney.Length - 3, 1);
                qian = totalmoney.Substring(totalmoney.Length - 4, 1);
            }
            else if (totalmoney.Length == 3)
            {
                ge = totalmoney.Substring(totalmoney.Length - 1);
                shi = totalmoney.Substring(totalmoney.Length - 2, 1);
                bai = totalmoney.Substring(totalmoney.Length - 3, 1);
            }
            else if (totalmoney.Length == 2)
            {
                ge = totalmoney.Substring(totalmoney.Length - 1);
                shi = totalmoney.Substring(totalmoney.Length - 2, 1);
            }
            else if (totalmoney.Length == 1)
            {
                ge = totalmoney.Substring(totalmoney.Length - 1);
            }

            this.lblGe.Text = this.ConvertData(ge);
            this.lblShi.Text = this.ConvertData(shi);
            this.lblBai.Text = this.ConvertData(bai);
            this.lblQian.Text = this.ConvertData(qian);
            this.lblWan.Text = this.ConvertData(wan);
            this.lblShiwan.Text = this.ConvertData(shiwan);
            this.lblBaiwan.Text = this.ConvertData(baiwan);
            this.lblQianwan.Text = this.ConvertData(qianwan);

            //TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //TCQuantity.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetaiInQuantity, "{0:0.##}");
            //TCPrice.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailPrice, "{0:0.##}");
            //TCMoney.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailMoney, "{0:0.##}");
        }
        private string ConvertData(string s)
        {
            string str = "";
            if (!string.IsNullOrEmpty(s))
            {
                int i = Convert.ToInt32(s);
                switch (i)
                {
                    case 0:
                        str = "Áã";
                        break;
                    case 1:
                        str = "Ò¼";
                        break;
                    case 2:
                        str = "·¡";
                        break;
                    case 3:
                        str = "Èþ";
                        break;
                    case 4:
                        str = "ËÁ";
                        break;
                    case 5:
                        str = "Îé";
                        break;
                    case 6:
                        str = "ê‘";
                        break;
                    case 7:
                        str = "Æâ";
                        break;
                    case 8:
                        str = "°Æ";
                        break;
                    case 9:
                        str = "¾Á";
                        break;
                    default:
                        str = "¡Á";
                        break;
                }
            }
            else
                str = "¡Á";
            return str;
        }

        private void XtraReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.xrSubreport1.ReportSource = new FPRO1(this.list);
        }
    }
}
