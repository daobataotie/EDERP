//------------------------------------------------------------------------------
//
// file name：PCEarplugsResilienceWeightAccessor.cs
// author: mayanjun
// create date：2019/8/25 22:18:53
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
    /// Data accessor of PCEarplugsResilienceWeight
    /// </summary>
    public partial class PCEarplugsResilienceWeightAccessor : EntityAccessor, IPCEarplugsResilienceWeightAccessor
    {
        public bool mHasRows(string id)
        {
            return sqlmapper.QueryForObject<bool>("PCEarplugsResilienceWeight.mHasRows", id);
        }

        public Model.PCEarplugsResilienceWeight mGetLast(string id)
        {
            return sqlmapper.QueryForObject<Model.PCEarplugsResilienceWeight>("PCEarplugsResilienceWeight.mGetLast", id);
        }
    }
}
