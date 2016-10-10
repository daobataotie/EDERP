using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class OutDepotForm : ConditionChooseForm
    {
        private OutDepot condition;

        public override Condition Condition
        {
            get
            {
                return condition;
            }
            set
            {
                condition = value as OutDepot;
            }
        }

        public OutDepotForm()
        {
            InitializeComponent();

            this.bindingSourceDepot.DataSource = (new BL.DepotManager()).Select();
            this.date_Start.DateTime = DateTime.Now.Date.AddDays(-7);
            this.date_End.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new OutDepot();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_Start.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.date_Start.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_End.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.date_End.DateTime;
            }

            this.condition.OutDepotIdStart = this.txt_DepotOutIdStart.Text;
            this.condition.OutDepotIdEnd = this.txt_DepotOutIdEnd.Text;
            this.condition.DepotEnd = this.lookUpEditDepotEnd.EditValue == null ? null : this.lookUpEditDepotEnd.EditValue.ToString();
            this.condition.DepotStart = this.lookUpEditDepotStar.EditValue == null ? null : this.lookUpEditDepotStar.EditValue.ToString();
            if ((this.buttonEditProduct.EditValue as Model.Product) != null)
                this.condition.ProductId = (this.buttonEditProduct.EditValue as Model.Product).ProductId;
            else
                this.condition.ProductId = null;
            this.condition.InvoiceCusId = this.txt_InvoiceCusId.Text;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEditProduct_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void buttonEditProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    this.buttonEditProduct.EditValue = f.SelectedItem as Model.Product;
            //}
            //f.Dispose();
            //GC.Collect();
        }

        private void buttonEditProduct_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                this.buttonEditProduct.EditValue = null;
            else
            {
                Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    this.buttonEditProduct.EditValue = f.SelectedItem as Model.Product;
                }
                f.Dispose();
                GC.Collect();
            }
        }
    }
}