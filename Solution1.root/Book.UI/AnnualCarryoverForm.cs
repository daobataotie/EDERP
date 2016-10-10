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
using System.IO;
using System.Xml;

namespace Book.UI
{
    public partial class AnnualCarryoverForm : DevExpress.XtraEditors.XtraForm
    {
        public AnnualCarryoverForm()
        {
            InitializeComponent();
        }

        BL.StockManager manager = new Book.BL.StockManager();
        string mess;
        string DBname;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要進行年度結轉嗎？", "警告", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show("請選擇需結轉年！", this.Text, MessageBoxButtons.OK);
                return;
            }
            string startDate = this.dateEdit1.DateTime.ToString("yyyy") + "-1-1";
            string endDate = this.dateEdit1.DateTime.ToString("yyyy") + "-12-31 23:59:59";
            try
            {

                if (!Directory.Exists("c:\\ERP Data")) //如果不存在就创建文件夹
                {
                    Directory.CreateDirectory("c:\\ERP Data");
                }

                string dbName = "ERP" + this.dateEdit1.DateTime.ToString("yyyy");
                DBname = dbName;
                string dataName = dbName + "_Data";
                string dataFileName = "c:\\ERP Data\\" + dataName + ".mdf";
                string logName = dbName + "_Log";
                string logFileName = "c:\\ERP Data\\" + logName + ".ldf";
                SqlConnection coon = new SqlConnection(DbHelperSQL.connectionString);

                string dbExists = "use master select * from sys.databases where name='" + dbName + "'";
                string createDatabse = "create  database " + dbName + " on( name='" + dataName + "', filename='" + dataFileName + "', size=5mb, filegrowth=5mb)log on( name='" + logName + "', filename='" + logFileName + "',size=5mb, filegrowth=5mb) ";
                string reductionDatabase = "backup database " + coon.Database + " to disk = 'D:\\UpFiles\\data' RESTORE DATABASE " + dbName + " from disk='D:\\UpFiles\\data' with replace,move 'ErpGeneralCode7' to '" + dataFileName + "',move 'ErpGeneralCode7_log' to '" + logFileName + "'";

                if (File.Exists("D:\\UpFiles\\data"))
                    File.Delete("D:\\UpFiles\\data");

                if (DbHelperSQL.DBExists(dbExists))                      //如果数据库存在说明已经结转过
                {
                    MessageBox.Show("已經做過該年度的結轉！", this.Text, MessageBoxButtons.OK);
                    return;
                }

                try
                {

                    DbHelperSQL.ExecuteSql(createDatabse);                    //创建数据库
                    DbHelperSQL.ExecuteSql(reductionDatabase);                //备份原数据库还原新数据库
                }
                catch
                {
                    throw;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_"))
                    mess += ex.Message + "\r";
                else
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK);
                    return;
                }
            }
            //删除新数据库结转年之外的资料
            try
            {
                Sqlhelp(DBname, startDate, endDate);
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message, this.Text, MessageBoxButtons.OK);
                return;

            }
            //删除本数据库结转年的资料,本数据库资料不能删除，商品的历史记录等会受到影响
            //try
            //{
            //    manager.AnnualknotturnForm(startDate, endDate);
            //}
            //catch (Exception ex3)
            //{
            //    if (ex3.Message.Contains("FK_"))
            //        mess += ex3.Message + "\r";
            //    else
            //    {
            //        MessageBox.Show(ex3.Message, this.Text, MessageBoxButtons.OK);
            //        return;
            //    }
            //}

            String configFile = Application.ExecutablePath + ".config";
            XmlDocument document = new XmlDocument();
            document.Load(configFile);

            XmlNodeList nodes = document.SelectNodes("/configuration/userSettings/Book.UI.Properties.Settings/setting");
            foreach (XmlNode node in nodes)
            {
                switch (node.Attributes["name"].Value)
                {
                    case "AnnualCarryoverForm":
                        node.FirstChild.InnerText = mess;
                        break;
                }
            }
            document.Save(configFile);
            MessageBox.Show("結轉成功！", this.Text, MessageBoxButtons.OK);
        }

        private void Sqlhelp(string dbName, string startDate, string endDate)
        {
            string conString = "Data Source =.;Initial Catalog =" + dbName + ";Trusted_Connection=true";
            SqlConnection sqlcon = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("CopyCarryoverYear", sqlcon);
            sqlcon.Open();
            SqlTransaction tran = sqlcon.BeginTransaction();

            SqlParameter[] par = {                                  
                                  new SqlParameter ("@startDate",DbType.DateTime),
                                  new SqlParameter("@endDate",DbType.DateTime)
                                 };

            par[0].Value = startDate;
            par[1].Value = endDate;

            cmd.Transaction = tran;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(par);

            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                sqlcon.Close();
            }
            catch (Exception ex2)
            {
                if (ex2.Message.Contains("FK_"))
                {
                    mess += ex2.Message + "\r";
                    tran.Commit();
                    sqlcon.Close();
                }
                else
                {
                    tran.Rollback();
                    sqlcon.Close();
                    throw;

                }
            }
            finally
            {

            }
        }


    }
}