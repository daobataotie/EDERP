//------------------------------------------------------------------------------
//
// file name：IPCImpactCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-15 14:09:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCImpactCheckDetail
    /// </summary>
    public partial interface IPCImpactCheckDetailAccessor : IAccessor
    {
        IList<Book.Model.PCImpactCheckDetail> Select(string PCImpactCheckId);
        void DeleteByPCImpactCheckId(string PCImpactCheckId);
        IList<Book.Model.PCImpactCheckDetail> SelectByDateRage(DateTime StartDate, DateTime EndDate, Book.Model.Product product, string CusXOId);
        string SelectChecker(string id);

         DataTable SelectByHeaderId(string Id);
    }
}

