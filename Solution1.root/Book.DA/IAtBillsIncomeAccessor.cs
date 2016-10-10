//------------------------------------------------------------------------------
//
// file name：IAtBillsIncomeAccessor.cs
// author: mayanjun
// create date：2010-11-22 14:21:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtBillsIncome
    /// </summary>
    public partial interface IAtBillsIncomeAccessor : IAccessor
    {
        IList<Book.Model.AtBillsIncome> SelectAtBillsIncomeByBillsOften(string billsOften, string collectionAccount);
        IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate);
        IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate, string startCollectionAccount, string endCollectionAccount);
        IList<Book.Model.AtBillsIncome> SelectOferAndDate(DateTime startDate, DateTime endDate, string billsOften);
        IList<Book.Model.AtBillsIncome> SelectDuiXianByDate(DateTime startDate, DateTime endDate, DateTime daoQiDate1, DateTime daoQiDate2, string billsOften, string IncomeCategory);
        IList<Model.AtBillsIncome> SelectByCondition(DateTime? kpStart, DateTime? kpEnd, DateTime? dqStart, DateTime? dqEnd, DateTime? ydStart, DateTime? ydEnd, string IdStart, string IdEnd, string Category, string InvoiceState, string bankStart, string bankEnd, string SupplierStart, string SupplierEnd, string CustomerStart, string CustomerEnd);
        IList<Model.AtBillsIncome> SelectByShouldPayAccountId(string id);
        void DeleteByShouldPayAccountId(string id);
        bool IsExistsIdInsert(string id);
        bool IsExistsIdUpdate(string id, string billsId);
    }
}

