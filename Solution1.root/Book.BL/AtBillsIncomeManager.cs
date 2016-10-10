//------------------------------------------------------------------------------
//
// file name：AtBillsIncomeManager.cs
// author: mayanjun
// create date：2010-11-22 14:21:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtBillsIncome.
    /// </summary>
    public partial class AtBillsIncomeManager
    {

        /// <summary>
        /// Delete AtBillsIncome by primary key.
        /// </summary>
        public void Delete(string billsId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(billsId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a AtBillsIncome.
        /// </summary>
        public void Insert(Model.AtBillsIncome atBillsIncome)
        {
            //
            // todo:add other logic here
            //
            try
            {
                if (atBillsIncome.BillsId == null)
                    atBillsIncome.BillsId = Guid.NewGuid().ToString();
                BL.V.BeginTransaction();
                Validate(atBillsIncome);
                atBillsIncome.InsertTime = DateTime.Now;
                atBillsIncome.UpdateTime = DateTime.Now;
                accessor.Insert(atBillsIncome);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Insert(IList<Model.AtBillsIncome> atBillsIncomelist)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                foreach (var item in atBillsIncomelist)
                {

                    if (item.BillsId == null)
                        item.BillsId = Guid.NewGuid().ToString();
                    Validate(item);
                    item.InsertTime = DateTime.Now.AddSeconds(atBillsIncomelist.IndexOf(item));
                    item.UpdateTime = DateTime.Now.AddSeconds(atBillsIncomelist.IndexOf(item));
                    accessor.Insert(item);
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
        /// Update a AtBillsIncome.
        /// </summary>
        public void Update(Model.AtBillsIncome atBillsIncome)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                Validate(atBillsIncome);
                atBillsIncome.UpdateTime = DateTime.Now;
                accessor.Update(atBillsIncome);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }


        public void Update(IList<Model.AtBillsIncome> atBillsIncomeList, string ShouldPayAccountId)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                //删除旧资料
                this.DeleteByShouldPayAccountId(ShouldPayAccountId);
                foreach (var item in atBillsIncomeList)
                {

                    if (item.BillsId == null)
                        item.BillsId = Guid.NewGuid().ToString();
                    Validate(item);
                    item.InsertTime = DateTime.Now.AddSeconds(atBillsIncomeList.IndexOf(item));
                    item.UpdateTime = DateTime.Now.AddSeconds(atBillsIncomeList.IndexOf(item));
                    accessor.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void Validate(Model.AtBillsIncome atBillsIncome)
        {
            if (string.IsNullOrEmpty(atBillsIncome.Id))
            {
                throw new Helper.RequireValueException(Model.AtBillsIncome.PRO_Id);
            }
        }
        public IList<Book.Model.AtBillsIncome> SelectAtBillsIncomeByBillsOften(string billsOften, string collectionAccount)
        {
            return accessor.SelectAtBillsIncomeByBillsOften(billsOften, collectionAccount);
        }
        public IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate)
        {
            return accessor.Select(startDate, endDate);
        }
        public IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate, string startCollectionAccount, string endCollectionAccount)
        {
            return accessor.Select(startDate, endDate, startCollectionAccount, endCollectionAccount);
        }
        public IList<Book.Model.AtBillsIncome> SelectOferAndDate(DateTime startDate, DateTime endDate, string billsOften)
        {
            return accessor.SelectOferAndDate(startDate, endDate, billsOften);
        }
        public IList<Book.Model.AtBillsIncome> SelectDuiXianByDate(DateTime startDate, DateTime endDate, DateTime daoQiDate1, DateTime daoQiDate2, string billsOften, string IncomeCategory)
        {
            return accessor.SelectDuiXianByDate(startDate, endDate, daoQiDate1, daoQiDate2, billsOften, IncomeCategory);
        }

        public IList<Model.AtBillsIncome> SelectByCondition(DateTime? kpStart, DateTime? kpEnd, DateTime? dqStart, DateTime? dqEnd, DateTime? ydStart, DateTime? ydEnd, string IdStart, string IdEnd, string Category, string InvoiceState, string bankStart, string bankEnd, string SupplierStart, string SupplierEnd, string CustomerStart, string CustomerEnd)
        {
            return accessor.SelectByCondition(kpStart, kpEnd, dqStart, dqEnd, ydStart, ydEnd, IdStart, IdEnd, Category, InvoiceState, bankStart, bankEnd, SupplierStart, SupplierEnd, CustomerStart, CustomerEnd);
        }

        public IList<Model.AtBillsIncome> SelectByShouldPayAccountId(string id)
        {
            return accessor.SelectByShouldPayAccountId(id);
        }

        public void DeleteByShouldPayAccountId(string id)
        {
            accessor.DeleteByShouldPayAccountId(id);
        }

        public bool IsExistsIdInsert(string id)
        {
            return accessor.IsExistsIdInsert(id);
        }

        public bool IsExistsIdUpdate(string id, string billsId)
        {
            return accessor.IsExistsIdUpdate(id, billsId);
        }
    }
}

