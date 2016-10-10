using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.BankBill
{
    public partial class BillCollectionForm : DevExpress.XtraEditors.XtraForm
    {
        BL.AtBankAccountManager bankAccountManager = new Book.BL.AtBankAccountManager();
        BL.AtBillsIncomeManager billIncomeManager = new Book.BL.AtBillsIncomeManager();
        IList<Model.AtBillsIncome> bList = null;
        public BillCollectionForm()
        {
            InitializeComponent();
            this.bindingSourceAtBankAccount.DataSource = bankAccountManager.Select();
        }

        private void lookUpEditCollectionAccount_EditValueChanged(object sender, EventArgs e)
        {
            string collectionAccount = "";
            if (this.lookUpEditCollectionAccount.EditValue != null)
            {
                collectionAccount = this.lookUpEditCollectionAccount.EditValue.ToString();
            }
            IList<Model.AtBillsIncome> billList= billIncomeManager.SelectAtBillsIncomeByBillsOften("尚未兌現", collectionAccount);
            bList=new List<Model.AtBillsIncome>();
            if(billList!=null)
            {
                foreach (Model.AtBillsIncome i in billList)
                {
                    Model.AtBankAccount at = bankAccountManager.Get(i.CollectionAccount);
                    Model.Customer cu = new BL.CustomerManager().Get(i.PassingObject);
                    if (at != null)
                    {
                        i.C = at.BankAccountName;
                    }
                    if (cu != null)
                    {
                        i.A = cu.Id;
                        i.B = cu.CustomerFullName;
                    }
                    bList.Add(i);
                }
            }
            this.bindingSourcestringbillsOften.DataSource = bList;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (bList != null)
            {
                foreach (Model.AtBillsIncome ab in bList)
                {
                    if (ab.Up == true)
                    {
                        ab.MoveDay = this.dateEditMoveDay.EditValue == null ? global::Helper.DateTimeParse.NullDate :DateTime.Parse(this.dateEditMoveDay.EditValue.ToString());
                        ab.BillsOften = "托收中";
                        billIncomeManager.Update(ab);
                    }
                }
                MessageBox.Show("托收成功..");
            }
            this.gridControl1.RefreshDataSource();
        }

        private void simpleButtonControl_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}