//------------------------------------------------------------------------------
//
// file name：IPCEarplugsStayWireCheckDetailAccessor.cs
// author: mayanjun
// create date：2019/5/14 09:57:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCEarplugsStayWireCheckDetail
    /// </summary>
    public partial interface IPCEarplugsStayWireCheckDetailAccessor : IAccessor
    {
        IList<Model.PCEarplugsStayWireCheckDetail> SelectByHeaderId(string headerId);

        void DeleteByHeaderId(string headerId);

        IList<Model.PCEarplugsStayWireCheckDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productId, string cusXOId);
    }
}
