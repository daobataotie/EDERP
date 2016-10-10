//------------------------------------------------------------------------------
//
// file name：InformationStateManager.cs
// author: mayanjun
// create date：2015/8/20 16:32:58
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InformationState.
    /// </summary>
    public partial class InformationStateManager
    {
		
		/// <summary>
		/// Delete InformationState by primary key.
		/// </summary>
		public void Delete(string informationStateId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(informationStateId);
		}

		/// <summary>
		/// Insert a InformationState.
		/// </summary>
        public void Insert(Model.InformationState informationState)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(informationState);
        }
		
		/// <summary>
		/// Update a InformationState.
		/// </summary>
        public void Update(Model.InformationState informationState)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(informationState);
        }
    }
}
