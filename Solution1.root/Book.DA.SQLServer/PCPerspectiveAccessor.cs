//------------------------------------------------------------------------------
//
// file name：PCPerspectiveAccessor.cs
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
    /// Data accessor of PCPerspective
    /// </summary>
    public partial class PCPerspectiveAccessor : EntityAccessor, IPCPerspectiveAccessor
    {
        public IList<Model.PCPerspective> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.PCPerspective>("PCPerspective.SelectByHeaderId", id);
        }
    }
}
