//------------------------------------------------------------------------------
//
// file name：AtCashAccountManager.cs
// author: mayanjun
// create date：2014/8/11 19:50:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    public partial class AtCashAccountManager
    {
        private static readonly DA.IAtCashAccountDetailAccessor detailaccessor = (DA.IAtCashAccountDetailAccessor)Accessors.Get("AtCashAccountDetailAccessor");

        /// <summary>
        /// Delete AtCashAccount by primary key.
        /// </summary>
        public void Delete(string atCashAccountId)
        {
            try
            {
                BL.V.BeginTransaction();
                detailaccessor.DeleteByHeaderId(atCashAccountId);
                accessor.Delete(atCashAccountId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a AtCashAccount.
        /// </summary>
        public void Insert(Model.AtCashAccount atCashAccount)
        {
            ValiDate(atCashAccount);

            try
            {
                BL.V.BeginTransaction();
                atCashAccount.InsertTime = DateTime.Now;
                atCashAccount.UpdateTime = DateTime.Now;
                accessor.Insert(atCashAccount);
                foreach (var item in atCashAccount.Detail)
                {
                    item.AtCashAccountId = atCashAccount.AtCashAccountId;
                    detailaccessor.Insert(item);
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
        /// Update a AtCashAccount.
        /// </summary>
        public void Update(Model.AtCashAccount atCashAccount)
        {
            ValiDate(atCashAccount);
            try
            {
                BL.V.BeginTransaction();
                atCashAccount.UpdateTime = DateTime.Now;
                accessor.Update(atCashAccount);

                detailaccessor.DeleteByHeaderId(atCashAccount.AtCashAccountId);
                foreach (var item in atCashAccount.Detail)
                {
                    item.AtCashAccountId = atCashAccount.AtCashAccountId;
                    detailaccessor.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void ValiDate(Book.Model.AtCashAccount atCashAccount)
        {
            if (atCashAccount.AtCashAccountMonth == null)
                throw new Helper.InvalidValueException(Model.AtCashAccount.PRO_AtCashAccountMonth);
            if (this.ISExistsMonth(atCashAccount))
                throw new Helper.MessageValueException("當前月份 " + atCashAccount.AtCashAccountMonth.Value.Month.ToString() + " 月已存在記錄！");
            //foreach (var item in atCashAccount.Detail)
            //{
            //    if (item.SubjectId == null)
            //        throw new Helper.InvalidValueException(Model.AtCashAccountDetail.PRO_SubjectId);
            //}
        }

        public Model.AtCashAccount GetDetail(string Id)
        {
            Model.AtCashAccount model = this.Get(Id);
            if (model != null)
                model.Detail = new BL.AtCashAccountDetailManager().SelectByHeaderId(Id);
            return model;
        }

        public bool ISExistsMonth(Model.AtCashAccount model)
        {
            return accessor.ISExistsMonth(model);
        }

        public decimal GetBalance()
        {
            return accessor.GetBalance();
        }

        public decimal GetBalanceByDate(DateTime startdate, DateTime enddate)
        {
            return accessor.GetBalanceByDate(startdate, enddate);
        }

        public DataTable CountIncomeAndPay(DateTime startdate, DateTime enddate)
        {
            return accessor.CountIncomeAndPay(startdate, enddate);
        }
    }
}
