//------------------------------------------------------------------------------
//
// file name：IPCDataInputAccessor.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCDataInput
    /// </summary>
    public partial interface IPCDataInputAccessor : IAccessor
    {
        IList<Model.PCDataInput> SelectByCondition(DateTime startDate, DateTime endDate, string PNTId, string CusXOId, string ProductId);
    }
}
