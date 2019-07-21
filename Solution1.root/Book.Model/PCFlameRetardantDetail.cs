//------------------------------------------------------------------------------
//
// file name：PCFlameRetardantDetail.cs
// author: mayanjun
// create date：2019/1/4 10:22:35
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class PCFlameRetardantDetail
    {
        public string ProductName { get; set; }

        public string EmployeeName { get; set; }

        public string CustomerInvoiceXOId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public bool IsChecked { get; set; }

        public string FromId
        {
            get
            {
                return this.InvoiceCOId + this.PronoteHeaderId;
            }
        }

        public readonly static string PRO_FromId = "FromId";
    }
}