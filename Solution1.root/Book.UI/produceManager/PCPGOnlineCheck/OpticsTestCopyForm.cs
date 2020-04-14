using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class OpticsTestCopyForm : DevExpress.XtraEditors.XtraForm
    {
        BL.PCPGOnlineCheckDetailManager pCPGOnlineCheckDetailManager = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.OpticsTestManager opticsTestManager = new Book.BL.OpticsTestManager();
        string _pCPGOnlineCheckDetailId;

        public OpticsTestCopyForm(Model.PCPGOnlineCheckDetail pCPGOnlineCheckDetail)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this._pCPGOnlineCheckDetailId = pCPGOnlineCheckDetail.PCPGOnlineCheckDetailId;
            DataTable dt = new DataTable();
            if (pCPGOnlineCheckDetail != null && pCPGOnlineCheckDetail.FromInvoiceId.StartsWith("PNT"))
            {
                dt = pCPGOnlineCheckDetailManager.SelectOpticsTestByFromInvoiceId(pCPGOnlineCheckDetail.FromInvoiceId);
            }


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
                    IList<Model.OpticsTest> otList = opticsTestManager.mSelect(dr["PCPGOnlineCheckDetailId"].ToString());
                    foreach (var item in otList)
                    {
                        item.OpticsTestId = opticsTestManager.GetId();
                        item.PCPGOnlineCheckDetailId = this._pCPGOnlineCheckDetailId;
                        item.OptiscTestDate = DateTime.Now;
                        item.EmployeeId = BL.V.ActiveOperator.EmployeeId;

                        opticsTestManager.Insert(item);
                    }

                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}