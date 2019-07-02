//------------------------------------------------------------------------------
//
// file name：PCEarplugsResilienceConditionSetAccessor.cs
// author: mayanjun
// create date：2019/6/15 19:29:48
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
    /// Data accessor of PCEarplugsResilienceConditionSet
    /// </summary>
    public partial class PCEarplugsResilienceConditionSetAccessor : EntityAccessor, IPCEarplugsResilienceConditionSetAccessor
    {
        public bool mHasRows(string id)
        {
            return sqlmapper.QueryForObject<bool>("PCEarplugsResilienceConditionSet.mHasRows", id);
        }

        public Model.PCEarplugsResilienceConditionSet mGetLast(string id)
        {
            return sqlmapper.QueryForObject<Model.PCEarplugsResilienceConditionSet>("PCEarplugsResilienceConditionSet.mGetLast", id);
        }
    }
}
