//------------------------------------------------------------------------------
//
// file name：AtBillsIncomeAccessor.cs
// author: mayanjun
// create date：2010-11-22 14:21:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of AtBillsIncome
    /// </summary>
    public partial class AtBillsIncomeAccessor : EntityAccessor, IAtBillsIncomeAccessor
    {
        public IList<Book.Model.AtBillsIncome> SelectAtBillsIncomeByBillsOften(string billsOften, string collectionAccount)
        {
            Hashtable pars = new Hashtable();
            pars.Add("BillsOften", billsOften);
            pars.Add("CollectionAccount", collectionAccount);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_AtBillsIncomeBillsOften", pars);
        }
        public IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_byDdate", ht);
        }
        public IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate, string startCollectionAccount, string endCollectionAccount)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("startCollectionAccount", startCollectionAccount);
            ht.Add("endCollectionAccount", endCollectionAccount);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_byDdateAndBank", ht);
        }
        public IList<Book.Model.AtBillsIncome> SelectOferAndDate(DateTime startDate, DateTime endDate, string billsOften)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("BillsOften", billsOften);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_byDdate2", ht);
        }
        public IList<Book.Model.AtBillsIncome> SelectDuiXianByDate(DateTime startDate, DateTime endDate, DateTime daoQiDate1, DateTime daoQiDate2, string billsOften, string IncomeCategory)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("daoQiDate1", daoQiDate1);
            ht.Add("daoQiDate2", daoQiDate2);
            ht.Add("BillsOften", billsOften);
            ht.Add("IncomeCategory", IncomeCategory);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_DuiXianByDate", ht);
        }

        public IList<Model.AtBillsIncome> SelectByCondition(DateTime? kpStart, DateTime? kpEnd, DateTime? dqStart, DateTime? dqEnd, DateTime? ydStart, DateTime? ydEnd, string IdStart, string IdEnd, string Category, string InvoiceState, string bankStart, string bankEnd, string SupplierStart, string SupplierEnd, string CustomerStart, string CustomerEnd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" and TheOpenDate between '" + kpStart.Value.ToString("yyyy-MM-dd") + "' and '" + kpEnd.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sb.Append(" and (Maturity between '" + dqStart.Value.ToString("yyyy-MM-dd") + "' and '" + dqEnd.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' or Maturity is null)");
            sb.Append(" and (TheJpy between '" + ydStart.Value.ToString("yyyy-MM-dd") + "' and '" + ydEnd.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' or TheJpy is null)");
            if (!string.IsNullOrEmpty(IdStart) || !string.IsNullOrEmpty(IdEnd))
            {
                if (!string.IsNullOrEmpty(IdStart) && !string.IsNullOrEmpty(IdEnd))
                    sb.Append(" and Id between '" + IdStart + "' and '" + IdEnd + "'");
                else
                    sb.Append(" and Id='" + (string.IsNullOrEmpty(IdStart) ? IdEnd : IdStart) + "'");
            }
            if (Category == "0")
            {
                sb.Append(" and IncomeCategory='0'");
                if (!string.IsNullOrEmpty(CustomerStart) || !string.IsNullOrEmpty(CustomerEnd))
                {
                    if (!string.IsNullOrEmpty(CustomerStart) && !string.IsNullOrEmpty(CustomerEnd))
                        sb.Append(" and PassingObject in (select CustomerId from Customer where Id between '" + CustomerStart + "' and '" + CustomerEnd + "')");
                    else
                        sb.Append(" and PassingObject in (select CustomerId from Customer='" + (string.IsNullOrEmpty(CustomerStart) ? CustomerEnd : CustomerStart) + "')");
                }
            }
            else if (Category == "1")
            {
                sb.Append(" and IncomeCategory='1'");
                if (!string.IsNullOrEmpty(SupplierStart) || !string.IsNullOrEmpty(SupplierEnd))
                {
                    if (!string.IsNullOrEmpty(SupplierStart) && !string.IsNullOrEmpty(SupplierEnd))
                        sb.Append(" and PassingObject in (select supplierid from Supplier where Id between '" + SupplierStart + "' and '" + SupplierEnd + "')");
                    else
                        sb.Append(" and PassingObject in (select supplierid from Supplier='" + (string.IsNullOrEmpty(SupplierStart) ? SupplierEnd : SupplierStart) + "')");
                }
            }
            if (InvoiceState == "0")
                sb.Append(" and BillsOften='0'");
            else if (InvoiceState == "1")
                sb.Append(" and BillsOften='1'");
            else if (InvoiceState == "2")
                sb.Append(" and BillsOften='2'");
            if (!string.IsNullOrEmpty(bankStart) || !string.IsNullOrEmpty(bankEnd))
            {
                if (!string.IsNullOrEmpty(bankStart) && !string.IsNullOrEmpty(bankEnd))
                    sb.Append(" and BankId in (select BankId from Bank where BankName between '" + bankStart + "' and '" + bankEnd + "')");
                else
                    sb.Append(" and BankId in (select BankId from Bank where BankName ='" + (string.IsNullOrEmpty(bankStart) ? bankEnd : bankStart) + "')");
            }

            sb.Append(" order by case when ChuanPiaoId is null then 1 else 0 end ,ChuanPiaoId,BankId asc ");
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.SelectByCondition", sb.ToString());
        }

        public IList<Model.AtBillsIncome> SelectByShouldPayAccountId(string id)
        {
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.SelectByShouldPayAccountId", id);
        }

        public void DeleteByShouldPayAccountId(string id)
        {
            sqlmapper.Delete("AtBillsIncome.DeleteByShouldPayAccountId", id);
        }

        public bool IsExistsIdInsert(string id)
        {
            return sqlmapper.QueryForObject<bool>("AtBillsIncome.IsExistsIdInsert", id);
        }

        public bool IsExistsIdUpdate(string id, string billsId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Id", id);
            ht.Add("BillsId", billsId);
            return sqlmapper.QueryForObject<bool>("AtBillsIncome.IsExistsIdUpdate", ht);
        }
    }
}
