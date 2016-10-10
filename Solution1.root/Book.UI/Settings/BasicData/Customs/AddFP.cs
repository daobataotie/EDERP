using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class AddFP : DevExpress.XtraEditors.XtraForm
    {
        Model.Setting setting = new Book.Model.Setting();
        BL.SettingManager settingManager = new Book.BL.SettingManager();
        IList<Model.Setting> list = new List<Model.Setting>();
        public AddFP()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Refresh();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.setting == null)
                this.setting = new Book.Model.Setting();
            setting.SettingId = Guid.NewGuid().ToString();
            setting.SettingCurrentValue = this.textBox1.Text;
            setting.SettingTags = "FP";
            setting.PictrueLogo = new Byte[] { };
            settingManager.Insert(setting);
            this.Refresh();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
                this.settingManager.Delete((this.bindingSource1.Current as Model.Setting).SettingId);
            }
            this.Refresh();
        }

        private void Refresh()
        {
            this.list = settingManager.SelectTagOrderDefault("FP");
            this.bindingSource1.DataSource = list;
            this.gridControl1.RefreshDataSource();
        }
    }
}