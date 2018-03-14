//------------------------------------------------------------------------------
//
// file name：ProduceInDepotDetail.cs
// author: peidun
// create date：2010-1-8 13:43:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 生产入库详细
    /// </summary>
    [Serializable]
    public partial class ProduceInDepotDetail
    {
        private string productDescription;

        public string ProductDesc
        {
            get
            {
                return this._product == null ? null : this._product.ProductDescription;
            }
        }

        public DateTime? ProduceInDepotDate
        {
            get
            {
                return this._produceInDepot == null ? null : this._produceInDepot.ProduceInDepotDate;
            }
        }

        public Model.WorkHouse WorkHouseHeader
        {
            get { return this._produceInDepot.WorkHouse; }
        }

        private bool _mChecked;
        public bool mChecked
        {
            get { return _mChecked; }
            set { _mChecked = value; }
        }

        private bool? _pisClose;
        public bool? PIsClose
        {
            get { return _pisClose; }
            set { _pisClose = value; }
        }


        public double? NoHegeQuantity
        {
            get
            {
                return (this.ProceduresSum.HasValue && this.CheckOutSum.HasValue) ? (this.ProceduresSum - this.CheckOutSum) : 0;
            }
        }


        private string _rejectionRate_1;
        public string RejectionRate_1
        {
            set { this._rejectionRate_1 = value; }
            get
            {
                return this._rejectionRate_1;
            }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int INumber { get; set; }

        public string ProInDepotDetailDate { get; set; }

        public string mWorkHouseName
        {
            get { return this.WorkHouseHeader == null ? "" : this.WorkHouseHeader.ToString(); }
        }

        public string PID_ProductType
        {
            get
            {
                string result = string.Empty;
                if (this._product == null)
                    result = "";
                else
                    if (this._product.ProductType.HasValue)
                        result = this._product.ProductType.Value == 0 ? "常態" : "特殊";
                    else
                        result = "";
                return result;
            }
        }

        public string PID_ProductWDQHua
        {
            get
            {
                string result = "";
                if (this._product.IsQiangHua.HasValue && this._product.IsQiangHua.Value)
                    result += " 強化 ";
                if (this._product.IsFangWu.HasValue && this._product.IsFangWu.Value)
                    result += " 防霧 ";
                if (this._product.IsNoQiangFang.HasValue && this._product.IsNoQiangFang.Value)
                    result += " 無強化防霧 ";

                return result;
            }
        }

        private string _productName;
        public string ProductName
        {
            set { this._productName = value; }
            get
            {
                return this._productName;
            }
        }

        private string _workHousename;
        public string WorkHousename
        {
            set { this._workHousename = value; }
            get
            {
                return this._workHousename;
            }
        }

        private DateTime? _mproduceInDepotDate;
        public DateTime? mProduceInDepotDate
        {
            set { this._mproduceInDepotDate = value; }
            get
            {
                return this._mproduceInDepotDate;
            }
        }

        private string _customerShortName;
        public string CustomerShortName
        {
            set { this._customerShortName = value; }
            get
            {
                return this._customerShortName;
            }
        }

        /// <summary>
        /// 客户订单编号
        /// </summary>
        public string CusXOId { get; set; }

        public string Machine { get; set; }

        public DateTime? JieAnDate { get; set; }

        //价格区间
        public string PriceRange
        {
            set;
            get;
        }

        public double? BadTotal
        {
            get { return (mYuanliaowenti == null ? 0 : mYuanliaowenti) + (mChouliaowenti == null ? 0 : mChouliaowenti) + (mPaoguanwenti == null ? 0 : mPaoguanwenti) + (mJingdiangudingdian == null ? 0 : mJingdiangudingdian) + (mChapiancashang == null ? 0 : mChapiancashang) + (mWanMocashang == null ? 0 : mWanMocashang) + (mGuaiShouZhuangShang == null ? 0 : mGuaiShouZhuangShang) + (mHuabancashang == null ? 0 : mHuabancashang) + (mGuohuojizhua == null ? 0 : mGuohuojizhua) + (mBaiyanHeiYan == null ? 0 : mBaiyanHeiYan) + (mJieHeXianHuiwen == null ? 0 : mJieHeXianHuiwen) + (mSuoShui == null ? 0 : mSuoShui) + (mQiPao == null ? 0 : mQiPao) + (mShechuqita == null ? 0 : mShechuqita) + (mCaMoSunHua == null ? 0 : mCaMoSunHua) + (mChaipiancashang == null ? 0 : mChaipiancashang) + (mHeidianzazhi == null ? 0 : mHeidianzazhi) + (mQianghuaqiancashang == null ? 0 : mQianghuaqiancashang) + (mQianghuahoucashang == null ? 0 : mQianghuahoucashang) + (mHanyao == null ? 0 : mHanyao) + (mKeLimianxu == null ? 0 : mKeLimianxu) + (mLiuheng == null ? 0 : mLiuheng) + (mPengYaodiyao == null ? 0 : mPengYaodiyao) + (mQianghuafangwuxian == null ? 0 : mQianghuafangwuxian) + (mYoudian == null ? 0 : mYoudian) + (mQianghuaQiTa == null ? 0 : mQianghuaQiTa) + (mChangshangbuliang == null ? 0 : mChangshangbuliang) + (mZuzhuangcashang == null ? 0 : mZuzhuangcashang) + (mCashang == null ? 0 : mCashang) + (mPinjianqita == null ? 0 : mPinjianqita) + (mPinguanqita == null ? 0 : mPinguanqita) + (mPodong == null ? 0 : mPodong) + (mBowen == null ? 0 : mBowen); }
        }

        //2017年10月27日23:49:05
        public string DepotName { get; set; }
        public DateTime? HeaderDate { get; set; }

        //2018年3月14日20:32:57,生产入库要导出的Excel里面包含 型号，该型号取入库商品对应的母件型号
        public string CustomerProductName { get; set; }

        public readonly static string PRO_ProInDepotDetailDate = "ProInDepotDetailDate";

        public readonly static string PRO_RejectionRate_1 = "RejectionRate_1";

        public readonly static string PRO_NoHegeQuantity = "NoHegeQuantity";

        public readonly static string PRO_PID_ProductType = "PID_ProductType";

        public readonly static string PRO_PID_ProductWDQHua = "PID_ProductWDQHua";

        public readonly static string PRO_ProductName = "ProductName";
    }
}
