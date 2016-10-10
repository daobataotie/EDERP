//------------------------------------------------------------------------------
//
// file name：PCDefinitionAccessor.cs
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
    /// Data accessor of PCDefinition
    /// </summary>
    public partial class PCDefinitionAccessor : EntityAccessor, IPCDefinitionAccessor
    {
        public IList<Model.PCDefinition> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.PCDefinition>("PCDefinition.SelectByHeaderId", id);
        }
    }
}
