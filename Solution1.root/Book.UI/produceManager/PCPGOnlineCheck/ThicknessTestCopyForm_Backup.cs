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
    public partial class ThicknessTestCopyForm_Backup : DevExpress.XtraEditors.XtraForm
    {
        BL.PCPGOnlineCheckDetailManager pCPGOnlineCheckDetailManager = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.ThicknessTestManager thicknessTestManager = new Book.BL.ThicknessTestManager();
        BL.ThicknessTestDetailsManager thicknessTestDetailsManager = new Book.BL.ThicknessTestDetailsManager();

        string _pCPGOnlineCheckDetailId;

        public ThicknessTestCopyForm_Backup(Model.PCPGOnlineCheckDetail pCPGOnlineCheckDetail)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            this._pCPGOnlineCheckDetailId = pCPGOnlineCheckDetail.PCPGOnlineCheckDetailId;
            DataTable dt = new DataTable();
            if (pCPGOnlineCheckDetail != null && pCPGOnlineCheckDetail.FromInvoiceId.StartsWith("PNT"))
            {
                dt = pCPGOnlineCheckDetailManager.SelectThicknessTestByFromInvoiceId(pCPGOnlineCheckDetail.FromInvoiceId);
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
                    IList<Model.ThicknessTest> otList = thicknessTestManager.mSelect(dr["PCPGOnlineCheckDetailId"].ToString());
                    foreach (var item in otList)
                    {

                        item.Details = thicknessTestDetailsManager.SelectByHeaderId(item.ThicknessTestId);

                        item.ThicknessTestId = thicknessTestManager.GetId();
                        item.PCPGOnlineCheckDetailId = this._pCPGOnlineCheckDetailId;
                        item.ThicknessTestDate = DateTime.Now;
                        item.EmployeeId = BL.V.ActiveOperator.EmployeeId;

                        foreach (var detail in item.Details)
                        {
                            detail.ThicknessTestDetailsId = Guid.NewGuid().ToString();
                            detail.ThicknessTestId = item.ThicknessTestId;
                        }

                        thicknessTestManager.Insert(item);
                    }

                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}