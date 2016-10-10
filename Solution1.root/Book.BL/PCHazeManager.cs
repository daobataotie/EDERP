//------------------------------------------------------------------------------
//
// file name：PCHazeManager.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCHaze.
    /// </summary>
    public partial class PCHazeManager
    {

        /// <summary>
        /// Delete PCHaze by primary key.
        /// </summary>
        public void Delete(string pCHazeId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCHazeId);
        }

        /// <summary>
        /// Insert a PCHaze.
        /// </summary>
        public void Insert(Model.PCHaze pCHaze)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCHaze);
        }

        /// <summary>
        /// Update a PCHaze.
        /// </summary>
        public void Update(Model.PCHaze pCHaze)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCHaze);
        }

        public IList<Model.PCHaze> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }
    }
}
