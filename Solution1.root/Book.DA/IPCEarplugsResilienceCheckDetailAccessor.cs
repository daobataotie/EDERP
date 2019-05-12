//------------------------------------------------------------------------------
//
// file name：IPCEarplugsResilienceCheckDetailAccessor.cs
// author: mayanjun
// create date：2019/5/10 10:49:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCEarplugsResilienceCheckDetail
    /// </summary>
    public partial interface IPCEarplugsResilienceCheckDetailAccessor : IAccessor
    {
        IList<Model.PCEarplugsResilienceCheckDetail> SelectByHeaderId(string primaryId);

        void DeleteByHeaderId(string headerId);

        IList<Model.PCEarplugsResilienceCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId);
    }
}
