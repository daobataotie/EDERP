//------------------------------------------------------------------------------
//
// file name：AtCashAccountDetailManager.cs
// author: mayanjun
// create date：2014/8/11 19:50:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtCashAccountDetail.
    /// </summary>
    public partial class AtCashAccountDetailManager
    {

        /// <summary>
        /// Delete AtCashAccountDetail by primary key.
        /// </summary>
        public void Delete(string atCashAccountDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(atCashAccountDetailId);
        }

        /// <summary>
        /// Insert a AtCashAccountDetail.
        /// </summary>
        public void Insert(Model.AtCashAccountDetail atCashAccountDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(atCashAccountDetail);
        }

        /// <summary>
        /// Update a AtCashAccountDetail.
        /// </summary>
        public void Update(Model.AtCashAccountDetail atCashAccountDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(atCashAccountDetail);
        }

        public IList<Model.AtCashAccountDetail> SelectByHeaderId(string Id)
        {
            return accessor.SelectByHeaderId(Id);
        }

        public void DeleteByHeaderId(string Id)
        {
            accessor.DeleteByHeaderId(Id);
        }

        public IList<Model.AtCashAccountDetail> SelectByDate(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDate(startdate, enddate);
        }
    }
}
