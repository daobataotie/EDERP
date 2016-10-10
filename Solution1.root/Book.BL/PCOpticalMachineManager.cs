//------------------------------------------------------------------------------
//
// file name：PCOpticalMachineManager.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCOpticalMachine.
    /// </summary>
    public partial class PCOpticalMachineManager
    {

        /// <summary>
        /// Delete PCOpticalMachine by primary key.
        /// </summary>
        public void Delete(string pCOpticalMachineId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCOpticalMachineId);
        }

        /// <summary>
        /// Insert a PCOpticalMachine.
        /// </summary>
        public void Insert(Model.PCOpticalMachine pCOpticalMachine)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCOpticalMachine);
        }

        /// <summary>
        /// Update a PCOpticalMachine.
        /// </summary>
        public void Update(Model.PCOpticalMachine pCOpticalMachine)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCOpticalMachine);
        }

        public IList<Model.PCOpticalMachine> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }
    }
}
