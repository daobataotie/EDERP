//------------------------------------------------------------------------------
//
// file name：IPCEarplugsDecibelCheckDetailAccessor.cs
// author: mayanjun
// create date：2019/5/12 14:43:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCEarplugsDecibelCheckDetail
    /// </summary>
    public partial interface IPCEarplugsDecibelCheckDetailAccessor : IAccessor
    {
        IList<Model.PCEarplugsDecibelCheckDetail> SelectByHeaderId(string headerId);

        void DeleteByHeaderId(string headerId);

        IList<Model.PCEarplugsDecibelCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId);
    }
}
