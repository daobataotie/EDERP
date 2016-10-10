//------------------------------------------------------------------------------
//
// file name：BGHandbookDetail1Manager.cs
// author: mayanjun
// create date：2013-4-16 11:58:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookDetail1.
    /// </summary>
    public partial class BGHandbookDetail1Manager
    {
		
		/// <summary>
		/// Delete BGHandbookDetail1 by primary key.
		/// </summary>
		public void Delete(string bGHandbookDetail1Id)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(bGHandbookDetail1Id);
		}

		/// <summary>
		/// Insert a BGHandbookDetail1.
		/// </summary>
        public void Insert(Model.BGHandbookDetail1 bGHandbookDetail1)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(bGHandbookDetail1);
        }
		
		/// <summary>
		/// Update a BGHandbookDetail1.
		/// </summary>
        public void Update(Model.BGHandbookDetail1 bGHandbookDetail1)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(bGHandbookDetail1);
        }
        public IList<Book.Model.BGHandbookDetail1> Select(string pac)
        {
           return   accessor.Select(pac);
        }
    }
}

