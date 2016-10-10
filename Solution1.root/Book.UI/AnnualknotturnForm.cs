using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.DA.SQLServer.SQLDB;
using System.Data.SqlClient;

namespace Book.UI
{
    public partial class AnnualknotturnForm : DevExpress.XtraEditors.XtraForm
    {
        public AnnualknotturnForm()
        {
            InitializeComponent();
        }

        BL.StockManager manager = new Book.BL.StockManager();

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show("請選擇需結轉年！", this.Text, MessageBoxButtons.OK);
                return;
            }
            string startDate = this.dateEdit1.DateTime.ToString("yyyy") + "-1-1";
            string endDate = this.dateEdit1.DateTime.ToString("yyyy") + "-12-31 23:59:59";
            try
            {
                BL.V.BeginTransaction();
                string dbName = "ERP" + this.dateEdit1.DateTime.ToString("yyyy");
                string dataName = dbName + "_Data";
                string dataFileName = "c:\\" + dataName + ".mdf";
                string logName = dbName + "_Log";
                string logFileName = "c:\\" + logName + ".ldf";
                SqlConnection coon = new SqlConnection(DbHelperSQL.connectionString);

                string createDatabse = "use master go  if db_id( '" + dbName + "') is not null  drop database " + dbName + " go create  database " + dbName + " on( name='" + dataName + "', filename='" + dataFileName + "', size=5mb, filegrowth=5mb)log on( name='" + logName + "', filename='" + logFileName + "',size=5mb, filegrowth=5mb) go";
                string reductionDatabase = "backup database " + coon.Database + " to disk = 'C:\\data' RESTORE FILELISTONLY from disk='C:\\data' RESTORE DATABASE " + dbName + " from disk='C:\\data' with replace,move 'ErpGeneralCode7' to '" + dataFileName + "',move 'ErpGeneralCode7_log' to '" + logFileName + "'";
                
                DbHelperSQL.ExecuteSql(createDatabse);                    //创建数据库
                DbHelperSQL.ExecuteSql(reductionDatabase);                //备份原数据库还原新数据库

                manager.AnnualknotturnForm(startDate, endDate);

                BL.V.CommitTransaction();
            }
            catch
            {

            }
        }
    }
}