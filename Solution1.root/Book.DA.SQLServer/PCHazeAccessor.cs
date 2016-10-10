//------------------------------------------------------------------------------
//
// file name：PCHazeAccessor.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of PCHaze
    /// </summary>
    public partial class PCHazeAccessor : EntityAccessor, IPCHazeAccessor
    {
        public IList<Model.PCHaze> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.PCHaze>("PCHaze.SelectByHeaderId", id);
        }
    }
}
