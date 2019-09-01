//------------------------------------------------------------------------------
//
// file name：IPCEarplugsResilienceWeightAccessor.cs
// author: mayanjun
// create date：2019/8/25 22:18:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCEarplugsResilienceWeight
    /// </summary>
    public partial interface IPCEarplugsResilienceWeightAccessor : IAccessor
    {
        bool mHasRows(string id);

        Model.PCEarplugsResilienceWeight mGetLast(string id);
    }
}
