//------------------------------------------------------------------------------
//
// file name：ShouldPayAccountManager.cs
// author: mayanjun
// create date：2014/7/16 22:02:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ShouldPayAccount.
    /// </summary>
    public partial class ShouldPayAccountManager
    {
        BL.ShouldPayAccountDetailManager detailmanager = new ShouldPayAccountDetailManager();
        /// <summary>
        /// Delete ShouldPayAccount by primary key.
        /// </summary>
        public void Delete(string shouldPayAccountId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                detailmanager.DeleteByHeaderId(shouldPayAccountId);
                accessor.Delete(shouldPayAccountId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a ShouldPayAccount.
        /// </summary>
        public void Insert(Model.ShouldPayAccount shouldPayAccount)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                shouldPayAccount.InsertTime = DateTime.Now;
                shouldPayAccount.UpdateTime = DateTime.Now;
                accessor.Insert(shouldPayAccount);
                foreach (var item in shouldPayAccount.Detail)
                {
                    //2017年6月24日20:06:45  改為可以先建立發票，打應付賬款明細表時自動拉出對應的發票，對已這部分已存在的要修改不是增加
                    if (detailmanager.ExistsPrimary(item.ShouldPayAccountDetailId))
                        detailmanager.Update(item);
                    else
                        detailmanager.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a ShouldPayAccount.
        /// </summary>
        public void Update(Model.ShouldPayAccount shouldPayAccount)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                shouldPayAccount.UpdateTime = DateTime.Now;
                accessor.Update(shouldPayAccount);
                detailmanager.DeleteByHeaderId(shouldPayAccount.ShouldPayAccountId);
                foreach (var item in shouldPayAccount.Detail)
                {
                    detailmanager.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public Model.ShouldPayAccount GetDetail(string id)
        {
            Model.ShouldPayAccount model = accessor.Get(id);
            if (model != null)
                model.Detail = this.detailmanager.GetByHeaderId(id);
            return model;
        }

        public IList<Model.ShouldPayAccount> SelectByCondition(DateTime startdate, DateTime enddate, string supplierid)
        {
            return accessor.SelectByCondition(startdate, enddate, supplierid);
        }
    }
}
