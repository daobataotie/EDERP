//------------------------------------------------------------------------------
//
// file name：IPCAssemblyInspectionDetailAccessor.cs
// author: mayanjun
// create date：2017-11-05 16:28:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCAssemblyInspectionDetail
    /// </summary>
    public partial interface IPCAssemblyInspectionDetailAccessor : IAccessor
    {
        IList<Model.PCAssemblyInspectionDetail> SelectByHeaderId(string id);

        void DeleteByHeaderId(string id);

        IList<Model.PCAssemblyInspectionDetail> SelectByCondition(DateTime startDate, DateTime endDate, string startPId, string endPId, string invoiceCusId);
    }
}
