//------------------------------------------------------------------------------
//
// file name：IShouldPayAccountAccessor.cs
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
    /// Interface of data accessor of dbo.ShouldPayAccount
    /// </summary>
    public partial interface IShouldPayAccountAccessor : IAccessor
    {
        IList<Model.ShouldPayAccount> SelectByCondition(DateTime startdate, DateTime enddate, string supplierid);
    }
}
