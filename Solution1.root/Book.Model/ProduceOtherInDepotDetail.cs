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

        public double? OrderQuantity { get; set; }

        public double? NotArriveQuantity { get; set; }

        public double? NotArrive
        {
            get
            {
                return NotArriveQuantity <= 0 ? 0 : NotArriveQuantity;
            }
        }
    }
}
