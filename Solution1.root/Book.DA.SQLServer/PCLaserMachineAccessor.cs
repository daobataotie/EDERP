//------------------------------------------------------------------------------
//
// file name：PCLaserMachineAccessor.cs
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
    /// Data accessor of PCLaserMachine
    /// </summary>
    public partial class PCLaserMachineAccessor : EntityAccessor, IPCLaserMachineAccessor
    {
        public IList<Model.PCLaserMachine> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.PCLaserMachine>("PCLaserMachine.SelectByHeaderId", id);
        }
    }
}
