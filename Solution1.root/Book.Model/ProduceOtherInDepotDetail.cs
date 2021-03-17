//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepotDetail.cs
// author: peidun
// create date：2010-1-8 13:43:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 外包入库详细
    /// </summary>
    [Serializable]
    public partial class ProduceOtherInDepotDetail
    {
        public string ProductDescription
        {
            get { return this.Product == null ? null : this.Product.ProductDescription; }
        }


        private bool _Checked;

        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
            }
        }

        public DateTime? ProduceOtherInDepotDate { get; set; }
        public string ProductName { get; set; }
        public string SupplierShortName { get; set; }
        public DateTime? JiaoQi { get; set; }

        public bool IsCheck { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public double? OrderQuantity { get; set; }

        private double? _notArriveQuantity;

        /// <summary>
        /// 未到货数量
        /// </summary>
        public double? NotArriveQuantity
        {
            get
            {
                return _notArriveQuantity < 0 ? 0 : _notArriveQuantity;
            }
            set
            {
                _notArriveQuantity = value;
            }
        }
    }
}
