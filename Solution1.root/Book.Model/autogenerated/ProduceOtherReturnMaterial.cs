﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceOtherReturnMaterial.autogenerated.cs
// author: mayanjun
// create date：2012-2-4 11:36:08
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    public partial class ProduceOtherReturnMaterial
    {
        #region Data

        /// <summary>
        /// 头编号
        /// </summary>
        private string _produceOtherReturnMaterialId;

        /// <summary>
        /// 操作员
        /// </summary>
        private string _employee0Id;

        /// <summary>
        /// 经办人
        /// </summary>
        private string _employee1Id;

        /// <summary>
        /// 审核人
        /// </summary>
        private string _employee2Id;

        /// <summary>
        /// 库房编号
        /// </summary>
        private string _depotId;

        /// <summary>
        /// 插入时间
        /// </summary>
        private DateTime? _insertTime;

        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime? _updateTime;

        /// <summary>
        /// 日期
        /// </summary>
        private DateTime? _produceOtherReturnMaterialDate;

        /// <summary>
        /// 说明
        /// </summary>
        private string _produceOtherReturnMaterialDesc;

        /// <summary>
        /// 
        /// </summary>
        private string _supplierId;

        /// <summary>
        /// 
        /// </summary>
        private int? _auditState;

        /// <summary>
        /// 
        /// </summary>
        private string _auditEmpId;

        /// <summary>
        /// 金额
        /// </summary>
        private decimal? _amountMoney;

        /// <summary>
        /// 税额
        /// </summary>
        private decimal? _tax;

        /// <summary>
        /// 总额
        /// </summary>
        private decimal? _totalMoney;

        /// <summary>
        /// 付款日期
        /// </summary>
        private DateTime? _payDate;

        private int _invoiceTaxrate;

        /// <summary>
        /// 员工
        /// </summary>
        private Employee _auditEmp;
        /// <summary>
        /// 库房
        /// </summary>
        private Depot _depot;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee0;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee1;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee2;
        /// <summary>
        /// 供应商
        /// </summary>
        private Supplier _supplier;

        #endregion

        #region Properties

        /// <summary>
        /// 头编号
        /// </summary>
        public string ProduceOtherReturnMaterialId
        {
            get
            {
                return this._produceOtherReturnMaterialId;
            }
            set
            {
                this._produceOtherReturnMaterialId = value;
            }
        }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Employee0Id
        {
            get
            {
                return this._employee0Id;
            }
            set
            {
                this._employee0Id = value;
            }
        }

        /// <summary>
        /// 经办人
        /// </summary>
        public string Employee1Id
        {
            get
            {
                return this._employee1Id;
            }
            set
            {
                this._employee1Id = value;
            }
        }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Employee2Id
        {
            get
            {
                return this._employee2Id;
            }
            set
            {
                this._employee2Id = value;
            }
        }

        /// <summary>
        /// 库房编号
        /// </summary>
        public string DepotId
        {
            get
            {
                return this._depotId;
            }
            set
            {
                this._depotId = value;
            }
        }

        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime? InsertTime
        {
            get
            {
                return this._insertTime;
            }
            set
            {
                this._insertTime = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            get
            {
                return this._updateTime;
            }
            set
            {
                this._updateTime = value;
            }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? ProduceOtherReturnMaterialDate
        {
            get
            {
                return this._produceOtherReturnMaterialDate;
            }
            set
            {
                this._produceOtherReturnMaterialDate = value;
            }
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string ProduceOtherReturnMaterialDesc
        {
            get
            {
                return this._produceOtherReturnMaterialDesc;
            }
            set
            {
                this._produceOtherReturnMaterialDesc = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SupplierId
        {
            get
            {
                return this._supplierId;
            }
            set
            {
                this._supplierId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? AuditState
        {
            get
            {
                return this._auditState;
            }
            set
            {
                this._auditState = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AuditEmpId
        {
            get
            {
                return this._auditEmpId;
            }
            set
            {
                this._auditEmpId = value;
            }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal? AmountMoney
        {
            get { return _amountMoney; }
            set { _amountMoney = value; }
        }

        /// <summary>
        /// 税额
        /// </summary>
        public decimal? Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        /// <summary>
        /// 总额
        /// </summary> 
        public decimal? TotalMoney
        {
            get { return _totalMoney; }
            set { _totalMoney = value; }
        }

        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime? PayDate
        {
            get { return _payDate; }
            set { _payDate = value; }
        }

        /// <summary>
        /// 稅率
        /// </summary>
        public int InvoiceTaxrate
        {
            get { return _invoiceTaxrate; }
            set { _invoiceTaxrate = value; }
        }

        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee AuditEmp
        {
            get
            {
                return this._auditEmp;
            }
            set
            {
                this._auditEmp = value;
            }

        }
        /// <summary>
        /// 库房
        /// </summary>
        public virtual Depot Depot
        {
            get
            {
                return this._depot;
            }
            set
            {
                this._depot = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee0
        {
            get
            {
                return this._employee0;
            }
            set
            {
                this._employee0 = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee1
        {
            get
            {
                return this._employee1;
            }
            set
            {
                this._employee1 = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee2
        {
            get
            {
                return this._employee2;
            }
            set
            {
                this._employee2 = value;
            }

        }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual Supplier Supplier
        {
            get
            {
                return this._supplier;
            }
            set
            {
                this._supplier = value;
            }

        }
        /// <summary>
        /// 头编号
        /// </summary>
        public readonly static string PRO_ProduceOtherReturnMaterialId = "ProduceOtherReturnMaterialId";

        /// <summary>
        /// 操作员
        /// </summary>
        public readonly static string PRO_Employee0Id = "Employee0Id";

        /// <summary>
        /// 经办人
        /// </summary>
        public readonly static string PRO_Employee1Id = "Employee1Id";

        /// <summary>
        /// 审核人
        /// </summary>
        public readonly static string PRO_Employee2Id = "Employee2Id";

        /// <summary>
        /// 库房编号
        /// </summary>
        public readonly static string PRO_DepotId = "DepotId";

        /// <summary>
        /// 插入时间
        /// </summary>
        public readonly static string PRO_InsertTime = "InsertTime";

        /// <summary>
        /// 修改时间
        /// </summary>
        public readonly static string PRO_UpdateTime = "UpdateTime";

        /// <summary>
        /// 日期
        /// </summary>
        public readonly static string PRO_ProduceOtherReturnMaterialDate = "ProduceOtherReturnMaterialDate";

        /// <summary>
        /// 说明
        /// </summary>
        public readonly static string PRO_ProduceOtherReturnMaterialDesc = "ProduceOtherReturnMaterialDesc";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_SupplierId = "SupplierId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_AuditState = "AuditState";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_AuditEmpId = "AuditEmpId";

        /// <summary>
        /// 金额
        /// </summary>
        public readonly static string PRO_AmountMoney = "AmountMoney";

        /// <summary>
        /// 税额
        /// </summary>
        public readonly static string PRO_Tax = "Tax";

        /// <summary>
        /// 总额
        /// </summary>
        public readonly static string PRO_TotalMoney = "TotalMoney";

        /// <summary>
        /// 付款日期
        /// </summary>
        public readonly static string PRO_PayDate = "PayDate";

        public readonly static string PRO_InvoiceTaxrate="InvoiceTaxrate";

        #endregion
    }
}
