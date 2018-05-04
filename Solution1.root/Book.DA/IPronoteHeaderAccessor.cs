//------------------------------------------------------------------------------
//
// file name：IPronoteHeaderAccessor.cs
// author: peidun
// create date：2009-12-29 11:58:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronoteHeader
    /// </summary>
    public partial interface IPronoteHeaderAccessor : IAccessor
    {
        IList<Book.Model.PronoteHeader> GetByDate(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5, bool sourcetype7, DateTime JiaohuoDateStart, DateTime JiaohuoDateEnd, string MachineName);

        IList<Book.Model.PronoteHeader> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd, string CusXOId);

        IList<Book.Model.PronoteHeader> Select(Model.MRSHeader mrsheader);
        //void UpdatePronoteIsClose(string pronoteheaderid, double? indepotquantity);
        void UpdateHeaderIsClse(string pronoteheaderid, bool isclose);

        IList<Book.Model.PronoteHeader> GetByDateMa(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5, bool sourcetype7);

        IList<Book.Model.PronoteHeader> GetByDateZJ(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, string sign, bool sourcetype0, bool sourcetype4, bool sourcetype5, bool sourcetype7);

        IList<Book.Model.PronoteHeader> GetByDateDI(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5, bool sourcetype7);

        void UpdateHeaderIsClseByXOId(string InvoiceXOId, bool isclose);
        IList<Book.Model.PronoteHeader> Select(IList<string> ids);
        IList<Model.PronoteHeader> SelectByHeaderId(string id);
        string SelectByInvoiceCusID(string ID, string invoiceType);
        Model.PronoteHeader GetLast(string InvoiceType);
        Model.PronoteHeader mGetFirst(string InvoiceType);
        bool mHasRows(string InvoiceType);
        bool mHasRowsAfter(Model.PronoteHeader p);
        bool mHasRowsBefore(Model.PronoteHeader p);
        Model.PronoteHeader mGetPrev(Model.PronoteHeader p);
        Model.PronoteHeader mGetNext(Model.PronoteHeader p);

        DataTable GetExcelData(DateTime startDate, DateTime endDate, string workHouseId);
    }
}
