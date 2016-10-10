//------------------------------------------------------------------------------
//
// file name：InformationManager.cs
// author: mayanjun
// create date：2015/8/20 16:32:58
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Information.
    /// </summary>
    public partial class InformationManager
    {
		
		/// <summary>
		/// Delete Information by primary key.
		/// </summary>
		public void Delete(string informationId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(informationId);
		}

		/// <summary>
		/// Insert a Information.
		/// </summary>
        public void Insert(Model.Information information)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(information);
        }
		
		/// <summary>
		/// Update a Information.
		/// </summary>
        public void Update(Model.Information information)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(information);
        }
    }
}
