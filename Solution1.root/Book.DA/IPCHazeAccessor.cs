//------------------------------------------------------------------------------
//
// file name：IPCHazeAccessor.cs
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
    /// Interface of data accessor of dbo.PCHaze
    /// </summary>
    public partial interface IPCHazeAccessor : IAccessor
    {
        IList<Model.PCHaze> SelectByHeaderId(string id);
    }
}
