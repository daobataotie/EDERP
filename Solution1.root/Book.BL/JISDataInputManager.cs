//------------------------------------------------------------------------------
//
// file name：JISDataInputManager.cs
// author: mayanjun
// create date：2018-07-23 20:14:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.JISDataInput.
    /// </summary>
    public partial class JISDataInputManager
    {
		
		/// <summary>
		/// Delete JISDataInput by primary key.
		/// </summary>
		public void Delete(string jISDataInputId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(jISDataInputId);
		}

		/// <summary>
		/// Insert a JISDataInput.
		/// </summary>
        public void Insert(Model.JISDataInput jISDataInput)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(jISDataInput);
        }
		
		/// <summary>
		/// Update a JISDataInput.
		/// </summary>
        public void Update(Model.JISDataInput jISDataInput)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(jISDataInput);
        }
    }
}
