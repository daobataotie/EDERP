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
    public partial class DataInputListForm : Settings.BasicData.BaseListForm
    {
        public DataInputListForm()
        {
            InitializeComponent();
            BL.PCDataInputManager pcDataInputManager = new BL.PCDataInputManager();
        }

        protected override void RefreshData()
        {

         
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new DataInputForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(DataInputForm);
            return (DataInputForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }
    }
}