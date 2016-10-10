//------------------------------------------------------------------------------
//
// file name：PCDefinitionManager.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCDefinition.
    /// </summary>
    public partial class PCDefinitionManager
    {

        /// <summary>
        /// Delete PCDefinition by primary key.
        /// </summary>
        public void Delete(string pCDefinitionId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCDefinitionId);
        }

        /// <summary>
        /// Insert a PCDefinition.
        /// </summary>
        public void Insert(Model.PCDefinition pCDefinition)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCDefinition);
        }

        /// <summary>
        /// Update a PCDefinition.
        /// </summary>
        public void Update(Model.PCDefinition pCDefinition)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCDefinition);
        }

        public IList<Model.PCDefinition> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }
    }
}
