//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactDetail.cs
// author: peidun
// create date：2010-1-4 15:32:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 生产外包合同详细
    /// </summary>
    [Serializable]
    public partial class ProduceOtherCompactDetail
    {
        private string _productSpecification;
        /// <summary>
        /// 规格型号
        /// </summary>
        public string ProductSpecification
        {
            get
            {
                return this._productSpecification;
            }
            set
            {
                this._productSpecification = value;
            }
        }

        private double noInDepotCount = 0;
        /// <summary>
        /// 未入库的委外产品数量
        /// </summary>
        public double NoInDepotCount
        {
            get
            {
                if (this._otherCompactCount == null)
                    this._otherCompactCount = 0;
                if (this._arrivalInQuantity == null)
                    this._arrivalInQuantity = 0;
                return this._otherCompactCount.Value - this._arrivalInQuantity.Value;
            }
        }

        public string ProductDesc
        {
            get { return this.Product == null ? null : this.Product.ProductDescription; }
        }

        //private string _checkedStandard;
        ///// <summary>
        ///// 检测标准
        ///// </summary>
        //public string CheckedStandard
        //{
        //    get
        //    {
        //        return this._checkedStandard;
        //    }
        //    set
        //    {
        //        this._checkedStandard = value;
        //    }
        //}

        //public static readonly string PRO_CheckedStandard = "CheckedStandard";
        private bool _checked = false;
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        public Supplier Supplier { get { return this.ProduceOtherCompact.Supplier; } }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime? OtherCompactDate { get { return this.ProduceOtherCompact.ProduceOtherCompactDate; } }

        /// <summary>
        /// 客户订单号
        /// </summary>
        public string InvoiceCusId
        {
            get
            {
                string s = string.Empty;
                if (this.ProduceOtherCompact.InvoiceXO != null)
                    s = this.ProduceOtherCompact.InvoiceXO.CustomerInvoiceXOId;
                return s;
            }
        }

        #region 报表绑定虚拟
        public string ProductName { get; set; }

        public string CustomerProductName { get; set; }

        public readonly static string PRO_RPProductName = "ProductName";
        public readonly static string PRO_RPCustomerProductName = "CustomerProductName";
        #endregion
    }
}
