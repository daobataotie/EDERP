//------------------------------------------------------------------------------
//
// file name：PCEuropeOpticalManager.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEuropeOptical.
    /// </summary>
    public partial class PCEuropeOpticalManager
    {

        /// <summary>
        /// Delete PCEuropeOptical by primary key.
        /// </summary>
        public void Delete(string pCEuropeOpticalId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCEuropeOpticalId);
        }

        /// <summary>
        /// Insert a PCEuropeOptical.
        /// </summary>
        public void Insert(Model.PCEuropeOptical pCEuropeOptical)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCEuropeOptical);
        }

        /// <summary>
        /// Update a PCEuropeOptical.
        /// </summary>
        public void Update(Model.PCEuropeOptical pCEuropeOptical)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCEuropeOptical);
        }

        public IList<Model.PCEuropeOptical> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }
    }
}
