//------------------------------------------------------------------------------
//
// file name：IPCEarplugsResilienceConditionSetAccessor.cs
// author: mayanjun
// create date：2019/6/15 19:29:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCEarplugsResilienceConditionSet
    /// </summary>
    public partial interface IPCEarplugsResilienceConditionSetAccessor : IAccessor
    {
        bool mHasRows(string id);

        Model.PCEarplugsResilienceConditionSet mGetLast(string id);
    }
}
