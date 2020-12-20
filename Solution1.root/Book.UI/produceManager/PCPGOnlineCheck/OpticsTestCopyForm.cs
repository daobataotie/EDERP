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
        //BL.PCPGOnlineCheckDetailManager pCPGOnlineCheckDetailManager = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.OpticsTestManager opticsTestManager = new Book.BL.OpticsTestManager();
        string _pCPGOnlineCheckDetailId;
        string _pCFirstOnlineCheckDetailId;

        /// <summary>
        /// 来源单据的类型(0,光学厚度表；1，首件上线检查表)
        /// </summary>
        int _invoiceType;

        /// <summary>
        /// 光学表复制
        /// </summary>
        /// <param name="fromId">来源Id</param>
        /// <param name="pronoteHeaderId">加工单Id</param>
        /// <param name="invoiceType">来源单据的类型(0,光学厚度表；1，首件上线检查表)</param>
        public OpticsTestCopyForm(string fromId, string pronoteHeaderId, int invoiceType)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            this._invoiceType = invoiceType;
            if (invoiceType == 0)
                this._pCPGOnlineCheckDetailId = fromId;
            else
                this._pCFirstOnlineCheckDetailId = fromId;

            DataTable dt = new DataTable();
            //dt = pCPGOnlineCheckDetailManager.SelectOpticsTestByFromInvoiceId(pronoteHeaderId);  //老版，只支持光学/厚度表

            dt = opticsTestManager.SelectByPronoteHeaderId(pronoteHeaderId);

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
                    IList<Model.OpticsTest> otList = null;

                    if (dr["InvoiceType"].ToString() == "0")
                        otList = opticsTestManager.mSelect(dr["DetailId"].ToString());
                    else
                        otList = opticsTestManager.PFCSelect(dr["DetailId"].ToString());

                    foreach (var item in otList)
                    {
                        item.OpticsTestId = opticsTestManager.GetId();
                        item.PCPGOnlineCheckDetailId = this._pCPGOnlineCheckDetailId;
                        item.PCFirstOnlineCheckDetailId = this._pCFirstOnlineCheckDetailId;
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