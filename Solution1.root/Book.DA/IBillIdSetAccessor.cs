//------------------------------------------------------------------------------
//
// file name：IBillIdSetAccessor.cs
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
    /// Interface of data accessor of dbo.BillIdSet
    /// </summary>
    public partial interface IBillIdSetAccessor : IAccessor
    {
        IList<Model.BillIdSet> SelectAll();

        void DisableAll();

        Model.BillIdSet SelectEnable();
    }
}
