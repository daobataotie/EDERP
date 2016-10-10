//------------------------------------------------------------------------------
//
// file name：IBillIdDeletedAccessor.cs
// author: mayanjun
// create date：2015/11/20 18:56:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BillIdDeleted
    /// </summary>
    public partial interface IBillIdDeletedAccessor : IAccessor
    {
        string SelectBillIdByBillIdSetId(string id);
    }
}
