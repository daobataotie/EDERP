using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.IO;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class JISOutPutFile : DevExpress.XtraEditors.XtraForm
    {
        Model.PCExportReportANSIDetail detail = new Book.Model.PCExportReportANSIDetail();
        BL.PCExportReportANSIDetailManager manager = new Book.BL.PCExportReportANSIDetailManager();
        DataTable dt = new DataTable();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        BL.InvoiceCOManager invoiceCOManager = new Book.BL.InvoiceCOManager();
        BL.MRSHeaderManager mrsHeaderManager = new Book.BL.MRSHeaderManager();
        BL.ProduceOtherCompactManager produceOtherCompactManager = new Book.BL.ProduceOtherCompactManager();
        BL.ProduceOtherMaterialManager produceOtherMaterialManager = new Book.BL.ProduceOtherMaterialManager();
        BL.ProduceMaterialManager produceMaterialManager = new Book.BL.ProduceMaterialManager();
        BL.DepotOutManager depotOutManager = new Book.BL.DepotOutManager();
        BL.InvoiceXSManager invoiceXSManager = new Book.BL.InvoiceXSManager();
        BL.ProduceInDepotDetailManager produceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();
        BL.ProductOnlineCheckManager productOnlineCheckManager = new Book.BL.ProductOnlineCheckManager();
        BL.PCFinishCheckManager pCFinishCheckManager = new Book.BL.PCFinishCheckManager();
        BL.PCPGOnlineCheckDetailManager pCPGOnlineCheckDetailManager = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.PCOpticsCheckManager pCOpticsCheckManager = new Book.BL.PCOpticsCheckManager();
        BL.ANSIPCImpactCheckManager aNSIPCImpactCheckManager = new Book.BL.ANSIPCImpactCheckManager();
        BL.PCOtherCheckDetailManager pCOtherCheckDetailManager = new Book.BL.PCOtherCheckDetailManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();

        public string ServerSavePath
        {
            get
            {
                string s = string.Empty;
                //取得服务器附件存储地址
                if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["AllAttachment"] != null)
                {
                    s = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["AllAttachment"].Value;
                }
                return s;
            }
        }

        public JISOutPutFile()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.gridView1.GroupPanelText = "雙擊查看對應單據";
            dt.Columns.Add("Type", typeof(string));   //单据类型
            dt.Columns.Add("Id", typeof(string));     //单据编号
            dt.Columns.Add("Annex", typeof(string));  //附件
            dt.Columns.Add("Address", typeof(string)); //附件地址

            DataRow dr;
            dr = dt.NewRow();
            dr[0] = "訂單通知";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "採購單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "生產加工單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "領料單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "物料採購訂單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "入料檢驗單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "模具上線檢查表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "生產日報表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "委外加工單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "光學/厚度表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "光譜測試";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "強化/防霧工作日報表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "衝擊測試表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "品檢日報表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "委外加工檢驗單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "組裝半成品日報表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "成品組裝日報表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "組裝檢驗日報表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "成品檢驗單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "出貨報告";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "出貨通知單";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "出庫單";
            dt.Rows.Add(dr);
        }

        //private string ISExistsAnnex(string invoiceid, string invoiceType)
        //{
        //    string[] str = new string[] { };
        //    if (invoiceid.Contains(" "))
        //    {
        //        str = invoiceid.Split(' ');
        //    }
        //    if (str.Length > 0)
        //    {
        //        foreach (var item in str)
        //        {
        //            if (Directory.Exists(this.ServerSavePath + "\\" + invoiceType + "\\" + item.Trim()))
        //            {
        //                string[] filenames = Directory.GetFiles(this.ServerSavePath + "\\" + invoiceType + "\\" + item.Trim());
        //                if (filenames != null)
        //                    return "有";
        //                else
        //                    return "無";
        //            }
        //        }
        //        return "無";
        //    }
        //    invoiceid = invoiceid.Trim();
        //    if (Directory.Exists(this.ServerSavePath + "\\" + invoiceType + "\\" + invoiceid))
        //    {
        //        string[] filenames = Directory.GetFiles(this.ServerSavePath + "\\" + invoiceType + "\\" + invoiceid);
        //        if (filenames != null)
        //            return "有";
        //        else
        //            return "無";
        //    }
        //    else
        //        return "無";
        //}

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            //string invoiceCusId = this.txt_InvoiceCusXoId.Text;
            //if (this.bindingSource1.Current != null)
            //{
            //    DataRowView dr = this.bindingSource1.Current as DataRowView;
            //    if (dr != null)
            //    {
            //        string id = dr[1].ToString();
            //        //string[] ids = new string[] { };
            //        //if (!string.IsNullOrEmpty(id))
            //        //{
            //        //    if (id.Contains(" "))
            //        //        ids = id.Split(' ');
            //        //    if (ids.Length > 1)
            //        //    {
            //        //        IList<HelpClass> idList = new List<HelpClass>();
            //        //        foreach (var item in ids)
            //        //        {
            //        //            HelpClass d = new HelpClass();
            //        //            d.Id = item.Trim();
            //        //            idList.Add(d);
            //        //        }
            //        //        JISOutPutFileHelp j = new JISOutPutFileHelp(idList, dr[0].ToString());
            //        //        j.ShowDialog(this);
            //        //    }
            //        //else
            //        //{
            //        string str = id.Trim();
            //        if (str.Contains("XD"))
            //        {
            //            //Invoices.XO.EditForm xo = new Book.UI.Invoices.XO.EditForm(str);
            //            //xo.Show(this);
            //            Invoices.XO.ListForm listform = new Book.UI.Invoices.XO.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("PNT"))
            //        {
            //            //PronoteHeader.EditForm pnt = new Book.UI.produceManager.PronoteHeader.EditForm(str);
            //            //pnt.Show(this);
            //            PronoteHeader.ListForm listform;
            //            if (dr[0].ToString().Contains("組裝加工單"))
            //                listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId, 4);
            //            else if (dr[0].ToString().Contains("生產加工單"))
            //                listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId, 0);
            //            else
            //                listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId, 5);

            //            listform.Show(this);
            //        }
            //        else if (str.Contains("CD"))
            //        {
            //            //Invoices.CO.EditForm co = new Book.UI.Invoices.CO.EditForm(str);
            //            //co.Show(this);
            //            Invoices.CO.ListForm listform = new Book.UI.Invoices.CO.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        //else if (str.Contains("MRP"))
            //        //{
            //        //    MRSHeader.EditForm mrp = new Book.UI.produceManager.MRSHeader.EditForm(str);
            //        //    mrp.Show(this);
            //        //}
            //        else if (str.Contains("POM"))
            //        {
            //            //ProduceOtherMaterial.EditForm pom = new Book.UI.produceManager.ProduceOtherMaterial.EditForm(str);
            //            //pom.Show(this);
            //            ProduceOtherMaterial.ListForm listform = new Book.UI.produceManager.ProduceOtherMaterial.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("PDM"))
            //        {
            //            //ProduceMaterial.EditForm pdm = new Book.UI.produceManager.ProduceMaterial.EditForm(str);
            //            //pdm.Show(this);
            //            ProduceMaterial.ListForm listform = new Book.UI.produceManager.ProduceMaterial.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("CC") && !str.Contains("QCC"))
            //        {
            //            //Settings.StockLimitations.OutStockEditForm cc = new Book.UI.Settings.StockLimitations.OutStockEditForm(str);
            //            //cc.Show(this);
            //            Settings.StockLimitations.ChooseOutStockDepot f = new Book.UI.Settings.StockLimitations.ChooseOutStockDepot(invoiceCusId);
            //            f.Show(this);
            //        }
            //        else if (str.Contains("XC"))
            //        {
            //            //Invoices.XS.EditForm xc = new Book.UI.Invoices.XS.EditForm(str);
            //            //xc.Show(this);
            //            Invoices.XS.ListForm listform = new Book.UI.Invoices.XS.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("PID"))
            //        {
            //            //ProduceInDepot.EditForm pid = new Book.UI.produceManager.ProduceInDepot.EditForm(str);
            //            //pid.Show(this);
            //            ProduceInDepot.ListForm listform = new Book.UI.produceManager.ProduceInDepot.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("POC"))            //委外合同或者产品上线检验单
            //        {
            //            if (dr[0].ToString().Contains("委外合同單"))
            //            {
            //                //ProduceOtherCompact.EditForm p = new Book.UI.produceManager.ProduceOtherCompact.EditForm(str);
            //                //p.Show(this);
            //                ProduceOtherCompact.ListForm listform = new Book.UI.produceManager.ProduceOtherCompact.ListForm(invoiceCusId);
            //                listform.Show(this);
            //            }
            //            else
            //            {
            //                //ProductOnlineCheck.Editform poc = new Book.UI.produceManager.ProductOnlineCheck.Editform(str);
            //                //poc.Show(this);
            //                ProductOnlineCheck.List listform = new Book.UI.produceManager.ProductOnlineCheck.List(invoiceCusId);
            //                listform.Show(this);
            //            }
            //        }
            //        else if (str.Contains("QCC"))
            //        {
            //            //PCFinishCheck.EditForm qcc = new Book.UI.produceManager.PCFinishCheck.EditForm(str);
            //            //qcc.Show(this);
            //            PCFinishCheck.ListForm listform = new Book.UI.produceManager.PCFinishCheck.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("QC") && !str.Contains("QCC"))
            //        {
            //            //PCPGOnlineCheck.EditForm qc = new Book.UI.produceManager.PCPGOnlineCheck.EditForm(str);
            //            //qc.Show(this);
            //            PCPGOnlineCheck.ListForm listform = new Book.UI.produceManager.PCPGOnlineCheck.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("pcOptics"))
            //        {
            //            //PCOpticsCheck.EditForm pcOptics = new Book.UI.produceManager.PCOpticsCheck.EditForm(str);
            //            //pcOptics.Show(this);
            //            PCOpticsCheck.ListForm listform = new Book.UI.produceManager.PCOpticsCheck.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("QAN"))
            //        {
            //            //ANSIPCImpactCheck.EditForm qan = new Book.UI.produceManager.ANSIPCImpactCheck.EditForm(str);
            //            //qan.Show(this);
            //            ANSIPCImpactCheck.ListForm listform = new Book.UI.produceManager.ANSIPCImpactCheck.ListForm(invoiceCusId, 1);
            //            listform.Show(this);
            //        }
            //        else if (str.Contains("PCOC"))
            //        {
            //            //PCOtherCheck.EditForm pcoc = new Book.UI.produceManager.PCOtherCheck.EditForm(str);
            //            //pcoc.Show(this);
            //            PCOtherCheck.ListForm listform = new Book.UI.produceManager.PCOtherCheck.ListForm(invoiceCusId);
            //            listform.Show(this);
            //        }

            //        //}
            //        //}
            //    }
            //}
        }

        private void JISOutPutFile_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = dt;
            this.gridControl1.RefreshDataSource();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            //if (this.lookUpEdit1.EditValue == null)
            //{
            //    MessageBox.Show("請選擇商品！", this.Text, MessageBoxButtons.OK);
            //    return;
            //}
            if (string.IsNullOrEmpty(this.txt_InvoiceCusXoId.Text))
            {
                MessageBox.Show("客戶訂單編號為空！", this.Text, MessageBoxButtons.OK);
                return;
            }
            string invoiceCusID = this.txt_InvoiceCusXoId.Text;
            //string productId = this.lookUpEdit1.EditValue.ToString();

            dt.Rows[0][1] = this.invoiceXOManager.SelectByInvoiceCusID(invoiceCusID);              //客户订单 扫描件
            //dt.Rows[1][1] = this.invoiceXOManager.SelectByInvoiceCusID(invoiceCusID);              //客户订单 扫描件
            dt.Rows[1][1] = dt.Rows[0][1];
            //dt.Rows[2][1] = this.mrsHeaderManager.SelectByInvoiceCusIdAndType(invoiceCusID, "4");
            //if (!string.IsNullOrEmpty(dt.Rows[2][1].ToString()))
            //    dt.Rows[2][1] = dt.Rows[2][1].ToString().Substring(0, dt.Rows[2][1].ToString().LastIndexOf(' '));    //物料需求自製(組裝)
            dt.Rows[2][1] = this.pronoteHeaderManager.SelectByInvoiceCusID(invoiceCusID, "1");
            if (!string.IsNullOrEmpty(dt.Rows[2][1].ToString()))
                dt.Rows[2][1] = dt.Rows[2][1].ToString().Substring(0, dt.Rows[2][1].ToString().LastIndexOf(' '));    //组装加工
            //dt.Rows[3][1] = this.mrsHeaderManager.SelectByInvoiceCusIdAndType(invoiceCusID, "0");
            //if (!string.IsNullOrEmpty(dt.Rows[3][1].ToString()))
            //    dt.Rows[3][1] = dt.Rows[3][1].ToString().Substring(0, dt.Rows[3][1].ToString().LastIndexOf(' '));    //物料需求自製
            dt.Rows[3][1] = this.pronoteHeaderManager.SelectByInvoiceCusID(invoiceCusID, "0");
            if (!string.IsNullOrEmpty(dt.Rows[3][1].ToString()))
                dt.Rows[3][1] = dt.Rows[3][1].ToString().Substring(0, dt.Rows[3][1].ToString().LastIndexOf(' '));    //生产加工
            //dt.Rows[4][1] = this.mrsHeaderManager.SelectByInvoiceCusIdAndType(invoiceCusID, "1");
            //if (!string.IsNullOrEmpty(dt.Rows[4][1].ToString()))
            //    dt.Rows[4][1] = dt.Rows[4][1].ToString().Substring(0, dt.Rows[4][1].ToString().LastIndexOf(' '));    //物料需求外購
            dt.Rows[4][1] = this.invoiceCOManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[4][1].ToString()))
                dt.Rows[4][1] = dt.Rows[4][1].ToString().Substring(0, dt.Rows[4][1].ToString().LastIndexOf(' '));    //采购订单
            //dt.Rows[5][1] = this.mrsHeaderManager.SelectByInvoiceCusIdAndType(invoiceCusID, "3") + this.produceOtherMaterialManager.SelectByInvoiceCusID(invoiceCusID);
            //if (!string.IsNullOrEmpty(dt.Rows[5][1].ToString()))
            //    dt.Rows[5][1] = dt.Rows[5][1].ToString().Substring(0, dt.Rows[5][1].ToString().LastIndexOf(' '));    //物料需求委外&委外發料
            dt.Rows[5][1] = this.produceOtherCompactManager.SelectByInvoiceCusID(invoiceCusID) + this.produceOtherMaterialManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[5][1].ToString()))
                dt.Rows[5][1] = dt.Rows[5][1].ToString().Substring(0, dt.Rows[5][1].ToString().LastIndexOf(' '));    //委外合同&委外發料
            dt.Rows[6][1] = this.produceMaterialManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[6][1].ToString()))
                dt.Rows[6][1] = dt.Rows[6][1].ToString().Substring(0, dt.Rows[6][1].ToString().LastIndexOf(' '));    //生产领料
            dt.Rows[7][1] = this.depotOutManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[7][1].ToString()))
                dt.Rows[7][1] = dt.Rows[7][1].ToString().Substring(0, dt.Rows[7][1].ToString().LastIndexOf(' '));    //出仓单
            dt.Rows[8][1] = this.invoiceXSManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[8][1].ToString()))
                dt.Rows[8][1] = dt.Rows[8][1].ToString().Substring(0, dt.Rows[8][1].ToString().LastIndexOf(' '));    //出货单 扫描件
            dt.Rows[9][1] = this.produceInDepotDetailManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[9][1].ToString()))
                dt.Rows[9][1] = dt.Rows[9][1].ToString().Substring(0, dt.Rows[9][1].ToString().LastIndexOf(' '));    //生产入库
            dt.Rows[10][1] = dt.Rows[9][1];
            dt.Rows[11][1] = dt.Rows[9][1];
            dt.Rows[12][1] = this.productOnlineCheckManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[12][1].ToString()))
                dt.Rows[12][1] = dt.Rows[12][1].ToString().Substring(0, dt.Rows[12][1].ToString().LastIndexOf(' ')); //模具上线检验单
            dt.Rows[13][1] = this.pronoteHeaderManager.SelectByInvoiceCusID(invoiceCusID, "2");
            if (!string.IsNullOrEmpty(dt.Rows[13][1].ToString()))
                dt.Rows[13][1] = dt.Rows[13][1].ToString().Substring(0, dt.Rows[13][1].ToString().LastIndexOf(' ')); //加工指示 扫描件
            dt.Rows[14][1] = dt.Rows[9][1];
            dt.Rows[15][1] = this.pCFinishCheckManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[15][1].ToString()))
                dt.Rows[15][1] = dt.Rows[15][1].ToString().Substring(0, dt.Rows[15][1].ToString().LastIndexOf(' ')); //组装成品检验单 扫描件
            //dt.Rows[15][1] = dt.Rows[13][1];
            dt.Rows[17][1] = this.pCPGOnlineCheckDetailManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[17][1].ToString()))
                dt.Rows[17][1] = dt.Rows[17][1].ToString().Substring(0, dt.Rows[17][1].ToString().LastIndexOf(' '));  //光學/厚度表
            //dt.Rows[18][1] = this.pCOpticsCheckManager.SelectByInvoiceCusID(invoiceCusID);
            //if (!string.IsNullOrEmpty(dt.Rows[18][1].ToString()))
            //    dt.Rows[18][1] = dt.Rows[18][1].ToString().Substring(0, dt.Rows[18][1].ToString().LastIndexOf(' '));  //紫外線測試單 PDF件
            dt.Rows[18][1] = dt.Rows[15][1];                                                                          //组装成品检验单PDF件
            dt.Rows[19][1] = this.aNSIPCImpactCheckManager.SelectByInvoiceCusID(invoiceCusID);
            if (!string.IsNullOrEmpty(dt.Rows[19][1].ToString()))
                dt.Rows[19][1] = dt.Rows[19][1].ToString().Substring(0, dt.Rows[19][1].ToString().LastIndexOf(' '));  //JIS衝擊測試單
            //if (this.pCPGOnlineCheckDetailManager.SelectByInvoiceCusID(invoiceCusID) != null)
            //dt.Rows[20][1] = this.pCOtherCheckDetailManager.SelectByInvoiceCusID(invoiceCusID) + this.pCPGOnlineCheckDetailManager.SelectByInvoiceCusID(invoiceCusID).Substring(0, this.pCPGOnlineCheckDetailManager.SelectByInvoiceCusID(invoiceCusID).LastIndexOf(' '));
            dt.Rows[20][1] = this.pCOtherCheckDetailManager.SelectByInvoiceCusID(invoiceCusID) + dt.Rows[17][1].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[20][1].ToString()) && string.IsNullOrEmpty(dt.Rows[17][1].ToString()))
                dt.Rows[20][1] = dt.Rows[20][1].ToString().Substring(0, dt.Rows[20][1].ToString().LastIndexOf(' ')).ToString();//進料檢驗單&光學/厚度表
            //if (this.pCFinishCheckManager.SelectByInvoiceCusID(invoiceCusID) != null)
            //    dt.Rows[21][1] = this.pCFinishCheckManager.SelectByInvoiceCusID(invoiceCusID).Substring(0, this.pCFinishCheckManager.SelectByInvoiceCusID(invoiceCusID).LastIndexOf(' '));                                              //组装成品检验单
            dt.Rows[21][1] = dt.Rows[15][1];
            //if (this.pCFinishCheckManager.SelectByInvoiceCusID(invoiceCusID) != null)
            //    dt.Rows[22][1] = this.pCFinishCheckManager.SelectByInvoiceCusID(invoiceCusID).Substring(0, this.pCFinishCheckManager.SelectByInvoiceCusID(invoiceCusID).LastIndexOf(' '));         //组装成品检验单 PDF件
            dt.Rows[22][1] = dt.Rows[15][1];

            //附件
            //if (!string.IsNullOrEmpty(dt.Rows[0][1].ToString()))
            //{
            //    dt.Rows[0][2] = this.ISExistsAnnex(dt.Rows[0][1].ToString(), "Book.UI.Invoices.XO.EditForm");
            //    dt.Rows[0][3] = "Book.UI.Invoices.XO.EditForm";
            //}
            //dt.Rows[1][2] = dt.Rows[0][2];
            //dt.Rows[1][3] = dt.Rows[0][3];
            ////if (!string.IsNullOrEmpty(dt.Rows[2][1].ToString()))
            ////    dt.Rows[2][2] = this.ISExistsAnnex(dt.Rows[2][1].ToString(), "Book.UI.produceManager.MRSHeader.EditForm");
            ////if (!string.IsNullOrEmpty(dt.Rows[3][1].ToString()))
            ////    dt.Rows[3][2] = this.ISExistsAnnex(dt.Rows[3][1].ToString(), "Book.UI.produceManager.MRSHeader.EditForm");
            ////if (!string.IsNullOrEmpty(dt.Rows[4][1].ToString()))
            ////    dt.Rows[4][2] = this.ISExistsAnnex(dt.Rows[4][1].ToString(), "Book.UI.produceManager.MRSHeader.EditForm");
            ////if (!string.IsNullOrEmpty(dt.Rows[5][1].ToString()))
            ////{
            ////    string[] str = dt.Rows[5][1].ToString().Split(' ');
            ////    foreach (var item in str)
            ////    {
            ////        if (item.Contains("MRP"))
            ////        {
            ////            if (this.ISExistsAnnex(item, "Book.UI.produceManager.MRSHeader.EditForm") == "有")
            ////            {
            ////                dt.Rows[5][2] = "有";
            ////                break;
            ////            }
            ////        }
            ////        else if (item.Contains("POM"))
            ////        {
            ////            if (this.ISExistsAnnex(item, "Book.UI.produceManager.ProduceOtherMaterial.EditForm") == "有")
            ////            {
            ////                dt.Rows[5][2] = "有";
            ////                break;
            ////            }
            ////        }
            ////        dt.Rows[5][2] = "無";
            ////    }
            ////}
            ////if (!string.IsNullOrEmpty(dt.Rows[6][1].ToString()))
            ////    dt.Rows[6][2] = this.ISExistsAnnex(dt.Rows[6][1].ToString(), "Book.UI.produceManager.ProduceMaterial.EditForm");
            ////if (!string.IsNullOrEmpty(dt.Rows[7][1].ToString()))
            ////    dt.Rows[7][2] = this.ISExistsAnnex(dt.Rows[7][1].ToString(), "Book.UI.Settings.StockLimitations.OutStockEditForm");
            //if (!string.IsNullOrEmpty(dt.Rows[8][1].ToString()))
            //{
            //    dt.Rows[8][2] = this.ISExistsAnnex(dt.Rows[8][1].ToString(), "Book.UI.Invoices.XS.EditForm");
            //    dt.Rows[8][3] = "Book.UI.Invoices.XS.EditForm";
            //}
            ////if (!string.IsNullOrEmpty(dt.Rows[9][1].ToString()))
            ////    dt.Rows[9][2] = this.ISExistsAnnex(dt.Rows[9][1].ToString(), "Book.UI.produceManager.ProduceInDepot.EditForm");
            ////dt.Rows[10][2] = dt.Rows[9][2];
            ////dt.Rows[11][2] = dt.Rows[9][2];
            ////if (!string.IsNullOrEmpty(dt.Rows[12][1].ToString()))
            ////    dt.Rows[12][2] = this.ISExistsAnnex(dt.Rows[12][1].ToString(), "Book.UI.produceManager.ProductOnlineCheck.Editform");
            //if (!string.IsNullOrEmpty(dt.Rows[13][1].ToString()))
            //{
            //    dt.Rows[13][2] = this.ISExistsAnnex(dt.Rows[13][1].ToString(), "Book.UI.produceManager.PronoteHeader.EditForm");
            //    dt.Rows[13][3] = "Book.UI.produceManager.PronoteHeader.EditForm";
            //}
            ////dt.Rows[14][2] = dt.Rows[9][2];
            //if (!string.IsNullOrEmpty(dt.Rows[15][1].ToString()))
            //{
            //    dt.Rows[15][2] = this.ISExistsAnnex(dt.Rows[15][1].ToString(), "Book.UI.produceManager.PCFinishCheck.EditForm");
            //    dt.Rows[15][3] = "Book.UI.produceManager.PCFinishCheck.EditForm";
            //}
            ////if (!string.IsNullOrEmpty(dt.Rows[17][1].ToString()))
            ////    dt.Rows[17][2] = this.ISExistsAnnex(dt.Rows[17][1].ToString(), "Book.UI.produceManager.PCPGOnlineCheck.EditForm");
            ////if (!string.IsNullOrEmpty(dt.Rows[18][1].ToString()))
            ////    dt.Rows[18][2] = this.ISExistsAnnex(dt.Rows[18][1].ToString(), "Book.UI.produceManager.PCOpticsCheck.EditForm");
            //dt.Rows[18][2] = dt.Rows[15][2];
            //dt.Rows[18][3] = dt.Rows[15][3];
            ////if (!string.IsNullOrEmpty(dt.Rows[19][1].ToString()))
            ////    dt.Rows[19][2] = this.ISExistsAnnex(dt.Rows[19][1].ToString(), "Book.UI.produceManager.ANSIPCImpactCheck.EditForm");
            ////if (!string.IsNullOrEmpty(dt.Rows[20][1].ToString()))
            ////{
            ////    string[] str = dt.Rows[20][1].ToString().Split(' ');
            ////    foreach (var item in str)
            ////    {
            ////        if (item.Contains("PCOC"))
            ////        {
            ////            if (this.ISExistsAnnex(item, "Book.UI.produceManager.PCOtherCheck.EditForm") == "有")
            ////            {
            ////                dt.Rows[20][2] = "有";
            ////                break;
            ////            }
            ////        }
            ////        else if (item.Contains("QC"))
            ////        {
            ////            if (this.ISExistsAnnex(item, "Book.UI.produceManager.PCPGOnlineCheck.EditForm") == "有")
            ////            {
            ////                dt.Rows[20][2] = "有";
            ////                break;
            ////            }
            ////        }
            ////        dt.Rows[20][2] = "無";
            ////    }

            ////}
            ////dt.Rows[21][2] = dt.Rows[15][2];
            //dt.Rows[22][2] = dt.Rows[15][2];
            //dt.Rows[22][3] = dt.Rows[15][3];

            this.bindingSource1.DataSource = dt;
            this.gridControl1.RefreshDataSource();
        }

        private void txt_InvoiceCusXoId_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.txt_InvoiceCusXoId.Text != "")
            //    this.bindingSourceProduct.DataSource = this.productManager.SelectByInvoiceCusID(this.txt_InvoiceCusXoId.Text);
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            string invoiceCusId = this.txt_InvoiceCusXoId.Text;
            if (string.IsNullOrEmpty(invoiceCusId))
            {
                MessageBox.Show("客戶訂單編號為空！", this.Text, MessageBoxButtons.OK);
                return;
            }
            if (this.bindingSource1.Current != null)
            {
                DataRowView dr = this.bindingSource1.Current as DataRowView;
                if (dr != null)
                {
                    //string id = dr[1].ToString();
                    string name = dr[0].ToString();
                    //string[] ids = new string[] { };
                    //if (!string.IsNullOrEmpty(id))
                    //{
                    //    if (id.Contains(" "))
                    //        ids = id.Split(' ');
                    //    if (ids.Length > 1)
                    //    {
                    //        IList<HelpClass> idList = new List<HelpClass>();
                    //        foreach (var item in ids)
                    //        {
                    //            HelpClass d = new HelpClass();
                    //            d.Id = item.Trim();
                    //            idList.Add(d);
                    //        }
                    //        JISOutPutFileHelp j = new JISOutPutFileHelp(idList, dr[0].ToString());
                    //        j.ShowDialog(this);
                    //    }
                    //else
                    //{
                    //string str = id.Trim();
                    if (name.Contains("訂單通知") || name.Contains("採購單"))
                    {
                        //Invoices.XO.EditForm xo = new Book.UI.Invoices.XO.EditForm(str);
                        //xo.Show(this);
                        Invoices.XO.ListForm listform = new Book.UI.Invoices.XO.ListForm(invoiceCusId);
                        listform.Show(this);
                    }
                    else if (name.Contains("生產加工單") || name.Contains("組裝半成品日報表"))
                    {
                        //PronoteHeader.EditForm pnt = new Book.UI.produceManager.PronoteHeader.EditForm(str);
                        //pnt.Show(this);
                        PronoteHeader.ListForm listform = new Book.UI.produceManager.PronoteHeader.ListForm();
                        if (name.Contains("組裝加工單"))
                            listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId, 4);
                        else if (name.Contains("生產加工單"))
                            listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId, 0);
                        else if (name.Contains("組裝半成品日報表"))
                        {
                            listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId, 5);
                            ProduceInDepot.ListForm listform2 = new Book.UI.produceManager.ProduceInDepot.ListForm(invoiceCusId);
                            listform2.Show(this);
                        }
                        listform.Show(this);
                    }
                    else if (name.Contains("物料採購訂單"))
                    {
                        //Invoices.CO.EditForm co = new Book.UI.Invoices.CO.EditForm(str);
                        //co.Show(this);
                        Invoices.CO.ListForm listform = new Book.UI.Invoices.CO.ListForm(invoiceCusId);
                        listform.Show(this);
                        PCOtherCheck.ListForm listform2 = new Book.UI.produceManager.PCOtherCheck.ListForm(invoiceCusId);
                        listform2.Show(this);
                    }
                    else if (name.Contains("委外加工單"))            //委外合同或者产品上线检验单
                    {
                        //ProduceOtherCompact.EditForm p = new Book.UI.produceManager.ProduceOtherCompact.EditForm(str);
                        //p.Show(this);
                        ProduceOtherCompact.ListForm listform = new Book.UI.produceManager.ProduceOtherCompact.ListForm(invoiceCusId);
                        listform.Show(this);

                    }
                    //else if (str.Contains("MRP"))
                    //{
                    //    MRSHeader.EditForm mrp = new Book.UI.produceManager.MRSHeader.EditForm(str);
                    //    mrp.Show(this);
                    //}
                    //else if (str.Contains("POM"))
                    //{
                    //    //ProduceOtherMaterial.EditForm pom = new Book.UI.produceManager.ProduceOtherMaterial.EditForm(str);
                    //    //pom.Show(this);
                    //    ProduceOtherMaterial.ListForm listform = new Book.UI.produceManager.ProduceOtherMaterial.ListForm(invoiceCusId);
                    //    listform.Show(this);
                    //}
                    else if (name.Contains("領料單"))
                    {
                        //ProduceMaterial.EditForm pdm = new Book.UI.produceManager.ProduceMaterial.EditForm(str);
                        //pdm.Show(this);
                        ProduceMaterial.ListForm listform = new Book.UI.produceManager.ProduceMaterial.ListForm(invoiceCusId);
                        listform.Show(this);

                        ProduceOtherMaterial.ListForm listform2 = new Book.UI.produceManager.ProduceOtherMaterial.ListForm(invoiceCusId);
                        listform2.Show(this);

                        Settings.StockLimitations.ChooseOutStockDepot f = new Book.UI.Settings.StockLimitations.ChooseOutStockDepot(invoiceCusId);
                        f.Show(this);

                    }
                    //else if (name.Contains("出庫單"))
                    //{
                    //    //Settings.StockLimitations.OutStockEditForm cc = new Book.UI.Settings.StockLimitations.OutStockEditForm(str);
                    //    //cc.Show(this);
                    //    Settings.StockLimitations.ChooseOutStockDepot f = new Book.UI.Settings.StockLimitations.ChooseOutStockDepot(invoiceCusId);
                    //    f.Show(this);
                    //}
                    else if (name.Contains("出貨通知單") || name.Contains("出庫單"))
                    {
                        //Invoices.XS.EditForm xc = new Book.UI.Invoices.XS.EditForm(str);
                        //xc.Show(this);
                        Invoices.XS.ListForm listform = new Book.UI.Invoices.XS.ListForm(invoiceCusId);
                        listform.Show(this);
                    }
                    else if (name.Contains("生產日報表") || name.Contains("強化/防霧工作日報表") || name.Contains("品檢日報表") || name.Contains("成品組裝日報表"))
                    {
                        //ProduceInDepot.EditForm pid = new Book.UI.produceManager.ProduceInDepot.EditForm(str);
                        //pid.Show(this);
                        ProduceInDepot.ListForm listform = new Book.UI.produceManager.ProduceInDepot.ListForm(invoiceCusId);
                        listform.Show(this);
                    }
                    else if (name.Contains("模具上線檢查表"))
                    {
                        //ProductOnlineCheck.Editform poc = new Book.UI.produceManager.ProductOnlineCheck.Editform(str);
                        //poc.Show(this);
                        PCMouldOnlineCheck.ListFrom listform = new Book.UI.produceManager.PCMouldOnlineCheck.ListFrom(invoiceCusId);
                        listform.Show(this);
                    }
                    else if (name.Contains("組裝檢驗日報表"))
                    {
                        PCAssemblyInspection.ListForm listform = new Book.UI.produceManager.PCAssemblyInspection.ListForm(invoiceCusId);
                        listform.Show(this);
                    }
                    else if (name.Contains("光譜測試") || name.Contains("成品檢驗單") || name.Contains("出貨報告"))
                    {
                        //PCFinishCheck.EditForm qcc = new Book.UI.produceManager.PCFinishCheck.EditForm(str);
                        //qcc.Show(this);
                        PCFinishCheck.ListForm listform = new Book.UI.produceManager.PCFinishCheck.ListForm(invoiceCusId);
                        listform.Show(this);
                    }
                    else if (name.Contains("光學/厚度表"))
                    {
                        //PCPGOnlineCheck.EditForm qc = new Book.UI.produceManager.PCPGOnlineCheck.EditForm(str);
                        //qc.Show(this);
                        PCPGOnlineCheck.ListForm listform = new Book.UI.produceManager.PCPGOnlineCheck.ListForm(invoiceCusId);
                        listform.Show(this);
                    }
                    else if (name.Contains("衝擊測試表"))
                    {
                        //ANSIPCImpactCheck.EditForm qan = new Book.UI.produceManager.ANSIPCImpactCheck.EditForm(str);
                        //qan.Show(this);
                        ANSIPCImpactCheck.ListForm listform = new Book.UI.produceManager.ANSIPCImpactCheck.ListForm(invoiceCusId, 1);
                        listform.Show(this);

                        PCImpactCheck.ListForm listform2 = new Book.UI.produceManager.PCImpactCheck.ListForm(invoiceCusId);
                        listform2.Show(this);
                    }
                    //else if (str.Contains("pcOptics"))
                    //{
                    //    //PCOpticsCheck.EditForm pcOptics = new Book.UI.produceManager.PCOpticsCheck.EditForm(str);
                    //    //pcOptics.Show(this);
                    //    PCOpticsCheck.ListForm listform = new Book.UI.produceManager.PCOpticsCheck.ListForm(invoiceCusId);
                    //    listform.Show(this);
                    //}
                    else if (name.Contains("委外加工檢驗單"))
                    {
                        //PCOtherCheck.EditForm pcoc = new Book.UI.produceManager.PCOtherCheck.EditForm(str);
                        //pcoc.Show(this);
                        PCOtherCheck.ListForm listform = new Book.UI.produceManager.PCOtherCheck.ListForm(invoiceCusId);
                        listform.Show(this);

                        PCPGOnlineCheck.ListForm listform2 = new Book.UI.produceManager.PCPGOnlineCheck.ListForm(invoiceCusId);
                        listform2.Show(this);

                        PCImpactCheck.ListForm listform3 = new Book.UI.produceManager.PCImpactCheck.ListForm(invoiceCusId);
                        listform3.Show(this);
                    }
                    else if (name.Contains("入料檢驗單"))
                    {
                        //PCInputCheck.ListForm listform = new Book.UI.produceManager.PCInputCheck.ListForm(invoiceCusId);
                        //listform.Show(this);
                        PCInputCheck.RelationXOForm f = new Book.UI.produceManager.PCInputCheck.RelationXOForm(invoiceCusId);
                        if (f._relationXO == null)
                        {
                            MessageBox.Show("無記錄！", this.Text, MessageBoxButtons.OK);
                            return;
                        }
                        else
                            f.Show(this);
                    }

                    //else if (name.Contains("出貨報告"))
                    //{
                    //    PCExportReportANSI.ListForm listform = new ListForm(invoiceCusId, "JIS");
                    //    listform.Show(this);
                    //}
                    //}
                    //}
                }
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == this.gridColumn2)
                e.DisplayText = "查詢";
        }

        //private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        //{
        //    DataRowView dr = this.bindingSource1.Current as DataRowView;
        //    if (dr[3].ToString() == "無")
        //        return;
        //    string tableKeyId = dr[1].ToString();
        //    string InvoiceType = dr[3].ToString();
        //    if (tableKeyId != null)
        //    {
        //        string[] ids = new string[] { };
        //        if (tableKeyId.Contains(" "))
        //            ids = tableKeyId.Split(' ');
        //        if (ids.Length > 0)
        //        {

        //        }
        //        else
        //        {
        //            Settings.BasicData.BaseAttachmentView bav = new Settings.BasicData.BaseAttachmentView(tableKeyId, InvoiceType);
        //            bav.ShowDialog(this);
        //        }
        //    }
        //}
    }

    public class HelpClass
    {
        public string Id { get; set; }
    }
}