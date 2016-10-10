//------------------------------------------------------------------------------
//
// file name：PCPerspectiveManager.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCPerspective.
    /// </summary>
    public partial class PCPerspectiveManager
    {

        /// <summary>
        /// Delete PCPerspective by primary key.
        /// </summary>
        public void Delete(string pCPerspectiveId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCPerspectiveId);
        }

        /// <summary>
        /// Insert a PCPerspective.
        /// </summary>
        public void Insert(Model.PCPerspective pCPerspective)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCPerspective);
        }

        /// <summary>
        /// Update a PCPerspective.
        /// </summary>
        public void Update(Model.PCPerspective pCPerspective)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCPerspective);
        }

        public IList<Model.PCPerspective> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }
    }
}
