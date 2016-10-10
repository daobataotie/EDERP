using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class JISOutPutFileHelp : DevExpress.XtraEditors.XtraForm
    {
        string invoiceType = string.Empty;
        public JISOutPutFileHelp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.gridView1.GroupPanelText = "單擊編號打開對應單據";
        }

        public JISOutPutFileHelp(IList<HelpClass> list, string type)
            : this()
        {
            this.bindingSource1.DataSource = list;
            this.invoiceType = type;
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            string id = (this.bindingSource1.Current as HelpClass) == null ? null : (this.bindingSource1.Current as HelpClass).Id;
            if (!string.IsNullOrEmpty(id))
            {
                if (id.Contains("XD"))
                {
                    Invoices.XO.EditForm xo = new Book.UI.Invoices.XO.EditForm(id);
                    xo.Show(this);
                }
                else if (id.Contains("CD"))
                {
                    Invoices.CO.EditForm co = new Book.UI.Invoices.CO.EditForm(id);
                    co.Show(this);
                }
                else if (id.Contains("MRP"))
                {
                    MRSHeader.EditForm mrp = new Book.UI.produceManager.MRSHeader.EditForm(id);
                    mrp.Show(this);
                }
                else if (id.Contains("POM"))
                {
                    ProduceOtherMaterial.EditForm pom = new Book.UI.produceManager.ProduceOtherMaterial.EditForm(id);
                    pom.Show(this);
                }
                else if (id.Contains("PDM"))
                {
                    ProduceMaterial.EditForm pdm = new Book.UI.produceManager.ProduceMaterial.EditForm(id);
                    pdm.Show(this);
                }
                else if (id.Contains("CC") && !id.Contains("QCC"))
                {
                    Settings.StockLimitations.OutStockEditForm cc = new Book.UI.Settings.StockLimitations.OutStockEditForm(id);
                    cc.Show(this);
                }
                else if (id.Contains("XC"))
                {
                    Invoices.XS.EditForm xc = new Book.UI.Invoices.XS.EditForm(id);
                    xc.Show(this);
                }
                else if (id.Contains("PID"))
                {
                    ProduceInDepot.EditForm pid = new Book.UI.produceManager.ProduceInDepot.EditForm(id);
                    pid.Show(this);
                }
                else if (id.Contains("POC"))            //委外合同或者产品上线检验单
                {
                    if (this.invoiceType.Contains("委外合同單"))
                    {
                        ProduceOtherCompact.EditForm p = new Book.UI.produceManager.ProduceOtherCompact.EditForm(id);
                        p.Show(this);
                    }
                    else
                    {
                        ProductOnlineCheck.Editform poc = new Book.UI.produceManager.ProductOnlineCheck.Editform(id);
                        poc.Show(this);
                    }
                }
                else if (id.Contains("PNT"))
                {
                    PronoteHeader.EditForm pnt = new Book.UI.produceManager.PronoteHeader.EditForm(id);
                    pnt.Show(this);
                }
                else if (id.Contains("QCC"))
                {
                    PCFinishCheck.EditForm qcc = new Book.UI.produceManager.PCFinishCheck.EditForm(id);
                    qcc.Show(this);
                }
                else if (id.Contains("QC") && !id.Contains("QCC"))
                {
                    PCPGOnlineCheck.EditForm qc = new Book.UI.produceManager.PCPGOnlineCheck.EditForm(id);
                    qc.Show(this);
                }
                else if (id.Contains("pcOptics"))
                {
                    PCOpticsCheck.EditForm pcOptics = new Book.UI.produceManager.PCOpticsCheck.EditForm(id);
                    pcOptics.Show(this);
                }
                else if (id.Contains("QAN"))
                {
                    ANSIPCImpactCheck.EditForm qan = new Book.UI.produceManager.ANSIPCImpactCheck.EditForm(id);
                    qan.Show(this);
                }
                else if (id.Contains("PCOC"))
                {
                    PCOtherCheck.EditForm pcoc = new Book.UI.produceManager.PCOtherCheck.EditForm(id);
                    pcoc.Show(this);
                }

            }
        }
    }
}