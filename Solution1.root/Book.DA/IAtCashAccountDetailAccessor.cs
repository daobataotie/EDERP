//------------------------------------------------------------------------------
//
// file name：IAtCashAccountDetailAccessor.cs
// author: mayanjun
// create date：2014/8/11 19:50:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtCashAccountDetail
    /// </summary>
    public partial interface IAtCashAccountDetailAccessor : IAccessor
    {
        IList<Model.AtCashAccountDetail> SelectByHeaderId(string Id);

        void DeleteByHeaderId(string Id);

        IList<Model.AtCashAccountDetail> SelectByDate(DateTime startdate, DateTime enddate);
    }
}
