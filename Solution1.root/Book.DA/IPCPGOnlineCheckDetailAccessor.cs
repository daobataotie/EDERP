//------------------------------------------------------------------------------
//
// file name：IPCPGOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-12-6 14:34:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCPGOnlineCheckDetail
    /// </summary>
    public partial interface IPCPGOnlineCheckDetailAccessor : IAccessor
    {
        IList<Book.Model.PCPGOnlineCheckDetail> Select(string pcpgocId);
        void DeleteByPCPGOnlineCheckId(string IPCPGOnlineCheckId);
        IList<Model.PCPGOnlineCheckDetail> SelectByFromInvoiceId(string id);
        string GetTimerListString(string PCPGOnlineCheckId);
        string SelectByInvoiceCusID(string ID);
        DataTable SelectByHeaderId(string id);

        DataTable SelectOpticsTestByFromInvoiceId(string fromInvoiceId);
        DataTable SelectThicknessTestByFromInvoiceId(string fromInvoiceId);
    }
}

