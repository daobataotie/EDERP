//------------------------------------------------------------------------------
//
// file name：AcInvoiceXOBillDetail.cs
// author: mayanjun
// create date：2011-09-28 08:45:17
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 销售发票详细
    /// </summary>
    [Serializable]
    public partial class AcInvoiceXOBillDetail
    {

        public decimal? ReportShuiE
        {
            get
            {
                decimal? a = null;
                if (this._invoiceXODetailMoney.HasValue)
                    a = this._invoiceXODetailMoney.Value * (decimal)0.05;
                if (a != null)
                    a = Math.Round(a.Value, MidpointRounding.AwayFromZero);
                return a;
            }
        }
    }
}
