using System;
using System.Collections.Generic;
using System.Text;

namespace Book.BL
{
    public class Settings
    {
        #region Helpers
        private static SettingManager settingsManager = new SettingManager();
        private static readonly DA.ISettingAccessor accessor = (DA.ISettingAccessor)Accessors.Get("SettingAccessor");
        static BL.OperationRoleManager operationRoleManager = new OperationRoleManager();
        
        /// <summary>
        /// 生產入庫.委外加工單.委外入庫.採購單.進庫單.商品.單價只有會計1.會計.老闆娘.業務.我.經理助理.業務會計.倉庫主管.可以看的到.其他人不可以看到.
        /// </summary>
        //public static IList<string> authorityOperationId
        //{
        //    get { return operationRoleManager.GetAuthorityOperetionId(); }
        //}

        #endregion

        #region Properties

        public static string CompanyChineseName
        {
            get
            {
                return Get("CompanyChineseName");
            }
            set
            {
                settingsManager.Update("CompanyChineseName", value);
            }
        }

        public static string CompanyEnglishName
        {
            get
            {
                return Get("CompanyEnglishName");
            }
            set
            {
                settingsManager.Update("CompanyEnglishName", value);
            }
        }

        public static string CompanyPrinciple
        {
            get
            {
                return Get("CompanyPrinciple");
            }
            set
            {
                settingsManager.Update("CompanyPrinciple", value);
            }
        }

        public static string CompanyPhone
        {
            get
            {
                return Get("CompanyPhone");
            }
            set
            {
                settingsManager.Update("CompanyPhone", value);
            }
        }

        public static string CompanyFax
        {
            get
            {
                return Get("CompanyFax");
            }
            set
            {
                settingsManager.Update("CompanyFax", value);
            }
        }

        public static string CompanyEMail
        {
            get
            {
                return Get("CompanyEMail");
            }
            set
            {
                settingsManager.Update("CompanyEMail", value);
            }
        }

        public static string CompanyWebUrl
        {
            get
            {
                return Get("CompanyWebUrl");
            }
            set
            {
                settingsManager.Update("CompanyWebUrl", value);
            }
        }

        public static string CompanyAddress1
        {
            get
            {
                return Get("CompanyAddress1");
            }
            set
            {
                settingsManager.Update("CompanyAddress1", value);
            }
        }

        public static string CompanyAddress2
        {
            get
            {
                return Get("CompanyAddress2");
            }
            set
            {
                settingsManager.Update("CompanyAddress2", value);
            }
        }
        public static string CompanyAddress3
        {
            get
            {
                return Get("CompanyAddress3");
            }
            set
            {
                settingsManager.Update("CompanyAddress3", value);
            }
        }
        public static string CompanyNotes
        {
            get
            {
                return Get("CompanyNotes");
            }
            set
            {
                settingsManager.Update("CompanyNotes", value);
            }
        }
        public static string FactoryAddress
        {
            get
            {
                return Get("FactoryAddress");
            }
            set
            {
                settingsManager.Update("FactoryAddress", value);
            }
        }
        public static byte[] PictrueLogo
        {
            get
            {
                return GetImage("PictrueLogo");
            }
            set
            {
                settingsManager.UpdateImage("PictrueLogo", value);
            }
        }

        /// <summary>
        /// 库存预警
        /// </summary>
        public static string StockPromptFlag
        {
            get { return Get("StockPromptFlag"); }
            set { settingsManager.Update("StockPromptFlag", value); }
        }

        /// <summary>
        /// 长期未出仓商品
        /// </summary>
        public static string NoDepotOutProducts
        {
            get { return Get("NoDepotOutProducts"); }
            set { settingsManager.Update("NoDepotOutProducts", value); }
        }
        #endregion

        #region Methods

        public static string Get(string settingId)
        {
            Model.Setting setting = settingsManager.Get(settingId);
            if (setting != null)
                return setting.SettingCurrentValue;
            return null;
        }
        public static byte[] GetImage(string settingId)
        {
            Model.Setting setting = settingsManager.selectimage(settingId);
            byte[] image = null;
            if (setting != null)
                image = setting.PictrueLogo;
            return image;
        }



        #endregion

    }
}
