//------------------------------------------------------------------------------
//
// file name：PCOpticalMachineAccessor.cs
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
    /// Data accessor of PCOpticalMachine
    /// </summary>
    public partial class PCOpticalMachineAccessor : EntityAccessor, IPCOpticalMachineAccessor
    {
        public IList<Model.PCOpticalMachine> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.PCOpticalMachine>("PCOpticalMachine.SelectByHeaderId", id);
        }
    }
}
