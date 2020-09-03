//------------------------------------------------------------------------------
//
// file name：ShouldPayAccountManager.cs
// author: mayanjun
// create date：2014/7/16 22:02:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ShouldPayAccount.
    /// </summary>
    public partial class ShouldPayAccountManager
    {
        BL.ShouldPayAccountDetailManager detailmanager = new ShouldPayAccountDetailManager();
        BL.AtSummonManager atSummonManager = new Book.BL.AtSummonManager();
        BL.AtAccountSubjectManager atAccountSubjectManager = new Book.BL.AtAccountSubjectManager();
        BL.AtSummonDetailManager atSummonDetailManager = new Book.BL.AtSummonDetailManager();

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


        public void Delete(Model.ShouldPayAccount shouldPayAccount)
        {
            try
            {
                //删除应付应收票据
                var bills = new BL.AtBillsIncomeManager().SelectByShouldPayAccountId(shouldPayAccount.ShouldPayAccountId);
                if (bills != null && bills.Count > 0)
                {
                    foreach (var item in bills)
                    {
                        new BL.AtBillsIncomeManager().Delete(item.BillsId);
                    }
                }

                //删除本身
                Delete(shouldPayAccount.ShouldPayAccountId);

                //删除应收账款明细表的查询条件
                new BL.ShouldPayAccountConditionManager().Delete(shouldPayAccount.ShouldPayAccountConditionId);

                //删除会计传票
                new BL.AtSummonManager().Delete(shouldPayAccount.AtSummon);

                DeleteAtSummon(shouldPayAccount);
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

        #region 生成修改删除對應的會計傳票

        public void InsertAtSummon(Model.ShouldPayAccount shouldPayAccount, string subjectId, string summary)
        {
            //2020年8月27日22:02:24，保存时，多保存一份会计传票的信息

            Model.AtSummon newAtSummon = new Book.Model.AtSummon();
            newAtSummon.SummonId = Guid.NewGuid().ToString();
            newAtSummon.SummonDate = DateTime.Now;
            newAtSummon.SummonCategory = "轉帳傳票";
            newAtSummon.InsertTime = DateTime.Now;
            newAtSummon.UpdateTime = DateTime.Now;
            newAtSummon.Id = this.atSummonManager.GetId();
            newAtSummon.ShouldPayAccountId = shouldPayAccount.ShouldPayAccountId;
            newAtSummon.Details = new List<Model.AtSummonDetail>();

            Model.AtSummonDetail detail1 = new Model.AtSummonDetail();
            detail1.SummonDetailId = Guid.NewGuid().ToString();
            detail1.SummonCatetory = newAtSummon.SummonCategory;
            detail1.Lending = "借";
            detail1.AMoney = shouldPayAccount.Total;
            detail1.InsertTime = DateTime.Now;
            detail1.UpdateTime = DateTime.Now;
            detail1.SubjectId = subjectId;
            newAtSummon.Details.Add(detail1);

            Model.AtSummonDetail detail2 = new Model.AtSummonDetail();
            detail2.SummonDetailId = Guid.NewGuid().ToString();
            detail2.SummonCatetory = newAtSummon.SummonCategory;
            detail2.Lending = "貸";
            detail2.AMoney = shouldPayAccount.Total;
            detail2.InsertTime = DateTime.Now;
            detail2.UpdateTime = DateTime.Now;
            detail2.SubjectId = "6389198a-ab4d-401c-8ec9-2865a727d0e6";    //台幣銀行存款
            if (!string.IsNullOrEmpty(summary))
                detail2.Summary = summary;
            newAtSummon.Details.Add(detail2);

            newAtSummon.TotalDebits = newAtSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
            newAtSummon.CreditTotal = newAtSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

            foreach (var item in newAtSummon.Details)
            {
                if (item.Lending == "借")
                    item.Id = "A" + newAtSummon.Details.IndexOf(item);
                else
                    item.Id = "B" + newAtSummon.Details.IndexOf(item);
            }

            this.atSummonManager.Insert(newAtSummon);
        }

        public void UpdateAtSummon(Model.ShouldPayAccount shouldPayAccount, string subjectId)
        {
            Model.AtSummon atSummon = atSummonManager.GetByShouldPayAccountId(shouldPayAccount.ShouldPayAccountId);
            if (atSummon != null)
            {
                atSummon.UpdateTime = DateTime.Now;

                atSummon.Details = atSummonDetailManager.Select(atSummon);

                foreach (var item in atSummon.Details)
                {
                    if (item.SubjectId == subjectId)   //應付票據-厂商
                    {
                        item.AMoney = shouldPayAccount.Total;
                        item.UpdateTime = DateTime.Now;
                    }
                    else if (item.SubjectId == "6389198a-ab4d-401c-8ec9-2865a727d0e6")    //台幣銀行存款
                    {
                        item.AMoney = shouldPayAccount.Total;
                        item.UpdateTime = DateTime.Now;
                    }                    
                }

                atSummon.TotalDebits = atSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
                atSummon.CreditTotal = atSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

                atSummonManager.Update(atSummon);
            }
        }

        public void DeleteAtSummon(Model.ShouldPayAccount shouldPayAccount)
        {
            Model.AtSummon atSummon = atSummonManager.GetByShouldPayAccountId(shouldPayAccount.ShouldPayAccountId);
            if (atSummon != null)
            {
                atSummonManager.Delete(atSummon);
            }
        }

        #endregion
    }
}
