﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class ThicknessTestCopyForm : DevExpress.XtraEditors.XtraForm
    {
        //BL.PCPGOnlineCheckDetailManager pCPGOnlineCheckDetailManager = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.ThicknessTestManager thicknessTestManager = new Book.BL.ThicknessTestManager();
        BL.ThicknessTestDetailsManager thicknessTestDetailsManager = new Book.BL.ThicknessTestDetailsManager();
        string _pCFirstOnlineCheckDetailId;
        string _pCPGOnlineCheckDetailId;

        /// <summary>
        /// 来源单据的类型(0,光学厚度表；1，首件上线检查表)
        /// </summary>
        int _invoiceType;

        public ThicknessTestCopyForm(string fromId, string pronoteHeaderId, int invoiceType)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            this._invoiceType = invoiceType;
            if (invoiceType == 0)
                this._pCPGOnlineCheckDetailId = fromId;
            else
                this._pCFirstOnlineCheckDetailId = fromId;

            DataTable dt = new DataTable();
            dt = thicknessTestManager.SelectByPronoteHeaderId(pronoteHeaderId);

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
                    IList<Model.ThicknessTest> otList = null;

                    if (dr["InvoiceType"].ToString() == "0")
                        otList = thicknessTestManager.mSelect(dr["DetailId"].ToString());
                    else
                        otList = thicknessTestManager.PFCSelect(dr["DetailId"].ToString());


                    foreach (var item in otList)
                    {
                        item.Details = thicknessTestDetailsManager.SelectByHeaderId(item.ThicknessTestId);

                        item.ThicknessTestId = thicknessTestManager.GetId();
                        item.PCPGOnlineCheckDetailId = this._pCPGOnlineCheckDetailId;
                        item.PCFirstOnlineCheckDetailId = this._pCFirstOnlineCheckDetailId;
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