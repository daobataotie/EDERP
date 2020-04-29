using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCClarityCheck
{
    public partial class CopyForm : DevExpress.XtraEditors.XtraForm
    {
        BL.PCClarityCheckManager pCClarityCheckManager = new Book.BL.PCClarityCheckManager();
        public Model.PCClarityCheck PCClarityCheck { get; set; }

        public CopyForm(string productname)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            DataTable dt = new DataTable();

            productname = productname.Contains("-") ? productname.Split('-')[0].Trim() : productname;
            dt = pCClarityCheckManager.SelectByProductName(productname);

            if (dt.Rows.Count > 0)
            {
                this.bindingSource1.DataSource = dt;
            }
            else
            {
                throw new Exception("無數據！");
            }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                DataRowView dr = bindingSource1.Current as DataRowView;
                if (dr != null)
                {
                    PCClarityCheck = pCClarityCheckManager.GetDetail(dr["PCClarityCheckId"].ToString());
                    
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}