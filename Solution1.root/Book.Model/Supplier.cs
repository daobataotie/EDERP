//------------------------------------------------------------------------------
//
// file name：Supplier.cs
// author: peidun
// create date：2009-08-03 9:37:30
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 供应商
    /// </summary>
    [Serializable]
    public partial class Supplier
    {
        private System.Collections.Generic.IList<Model.SupplierContact> contacts = new System.Collections.Generic.List<Model.SupplierContact>();

        public System.Collections.Generic.IList<Model.SupplierContact> Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        public override string ToString()
        {
            return this._supplierShortName;
        }
        private bool _customerCheck;
        public bool customerCheck
        {
            get { return _customerCheck; }
            set
            {
                _customerCheck = value;
            }

        }

        public string InvoiceID1 { get; set; }

        public DateTime InvoiceDate1 { get; set; }

        public string InvoiceID2 { get; set; }

        public DateTime InvoiceDate2 { get; set; }

        public string SupplierCategoryName { get; set; }

        //延迟交货次数，延迟为1，否则为0
        public int OverTime { get; set; }

        //延迟交货天数
        public int OverDay { get; set; }

        //总采购次数
        public int TotalTime { get; set; }
    }
}
