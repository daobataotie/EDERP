//------------------------------------------------------------------------------
//
// file name：IShouldPayAccountDetailAccessor.cs
// author: mayanjun
// create date：2014/7/16 22:02:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ShouldPayAccountDetail
    /// </summary>
    public partial interface IShouldPayAccountDetailAccessor : IAccessor
    {
        IList<Model.ShouldPayAccountDetail> GetByDateRangeAndSupplier(DateTime startDate, DateTime endDate, string supplierID);
        IList<Model.ShouldPayAccountDetail> GetByHeaderId(string Id);
        void DeleteByHeaderId(string id);
    }
}
