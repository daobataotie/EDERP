//------------------------------------------------------------------------------
//
// file name:InvoiceCO.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 采购订单
    /// </summary>
    [Serializable]
    public partial class InvoiceCO : Invoice
    {
        private System.Collections.Generic.IList<Model.InvoiceCODetail> details;

        public System.Collections.Generic.IList<Model.InvoiceCODetail> Details
        {
            get { return details; }
            set { details = value; }
        }
        private string _supplierFullName;
        public string SupplierFullName
        {
            get { return _supplierFullName; }
            set { _supplierFullName = value; }
        }

        /// <summary>
        /// 採購訂單是否被選中
        /// </summary>
        private bool isChecked = false;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public string Lbl_JIS
        {
            get
            {
                if (this.Customer != null && !string.IsNullOrEmpty(this.Customer.CheckedStandard))
                {
                    if (this.Customer.CheckedStandard.ToLower().Contains("jis") && this.Customer.CustomerFullName.ToUpper().Contains("MIDORI"))
                    {
                        return "JIS";
                    }
                    else if (this.Customer.CheckedStandard.ToLower().Contains("as"))
                    {
                        return "AS";
                    }
                }

                return "";
            }
        }
    }
}
