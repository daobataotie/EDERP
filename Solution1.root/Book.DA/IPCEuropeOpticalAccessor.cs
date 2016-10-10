//------------------------------------------------------------------------------
//
// file name：IPCEuropeOpticalAccessor.cs
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
    /// Interface of data accessor of dbo.PCEuropeOptical
    /// </summary>
    public partial interface IPCEuropeOpticalAccessor : IAccessor
    {
        IList<Model.PCEuropeOptical> SelectByHeaderId(string id);
    }
}
