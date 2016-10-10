//------------------------------------------------------------------------------
//
// file name：PCLaserMachineManager.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCLaserMachine.
    /// </summary>
    public partial class PCLaserMachineManager
    {

        /// <summary>
        /// Delete PCLaserMachine by primary key.
        /// </summary>
        public void Delete(string pCLaserMachineId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCLaserMachineId);
        }

        /// <summary>
        /// Insert a PCLaserMachine.
        /// </summary>
        public void Insert(Model.PCLaserMachine pCLaserMachine)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCLaserMachine);
        }

        /// <summary>
        /// Update a PCLaserMachine.
        /// </summary>
        public void Update(Model.PCLaserMachine pCLaserMachine)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCLaserMachine);
        }

        public IList<Model.PCLaserMachine> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }
    }
}
