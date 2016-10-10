//------------------------------------------------------------------------------
//
// file name：ProduceMaterialdetails.cs
// author: peidun
// create date：2009-12-30 16:33:32
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 加工领料单明细
    /// </summary>
    [Serializable]
    public partial class ProduceMaterialdetails
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
        private string productDescription;

        public string ProductDescription
        {
            get
            {
                return this._product == null ? null : this._product.ProductDescription;
            }
        }

        private string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        private string _CustomerProductName;

        public string CustomerProductName
        {
            get { return _CustomerProductName; }
            set { _CustomerProductName = value; }
        }

        public string InvoiceId { get { return string.IsNullOrEmpty(this.MRSHeaderId) ? this.PronoteHeaderID : this.MRSHeaderId; } }

        public double NoMaterialprocesedsum { get { return Convert.ToDouble(this.Materialprocessum) - Convert.ToDouble(this.Materialprocesedsum); } }

        public DateTime ProduceMaterialDate { get; set; }

        public string PID { get; set; }

        public string ProduceMaterialdesc { get; set; }

        public string WorkhouseName { get; set; }

        public string CusXOId { get; set; }

        public static readonly string PRO_ProductName = "ProductName";
        public static readonly string PRO_CustomerProductName = "CustomerProductName";
    }
}
