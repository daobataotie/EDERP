//------------------------------------------------------------------------------
//
// file name：IAtCashAccountAccessor.cs
// author: mayanjun
// create date：2014/8/11 19:50:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtCashAccount
    /// </summary>
    public partial interface IAtCashAccountAccessor : IAccessor
    {
        bool ISExistsMonth(Model.AtCashAccount model);

        decimal GetBalance();

        decimal GetBalanceByDate(DateTime startdate, DateTime enddate);

        System.Data.DataTable CountIncomeAndPay(DateTime startdate, DateTime enddate);
    }
}
