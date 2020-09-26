﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcInvoiceXOBill.autogenerated.cs
// author: mayanjun
// create date：2012-2-13 15:05:17
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    public partial class AcInvoiceXOBill
    {
        #region Data

        /// <summary>
        /// 销售发票主键
        /// </summary>
        private string _acInvoiceXOBillId;

        /// <summary>
        /// 操作人
        /// </summary>
        private string _employeeId;

        /// <summary>
        /// 经办人
        /// </summary>
        private string _employee0Id;

        /// <summary>
        /// 审核人
        /// </summary>
        private string _employee1Id;

        /// <summary>
        /// 客户编号
        /// </summary>
        private string _customerId;

        /// <summary>
        /// 审核状态
        /// </summary>
        private int? _auditingState;

        /// <summary>
        /// 单据状态
        /// </summary>
        private int? _invoiceStatus;

        /// <summary>
        /// 审核日期
        /// </summary>
        private DateTime? _auditingStateDate;

        /// <summary>
        /// 开票日期
        /// </summary>
        private DateTime? _acInvoiceXOBillDate;

        /// <summary>
        /// 发票类型
        /// </summary>
        private int? _acInvoiceXOBillType;

        /// <summary>
        /// 发票号码
        /// </summary>
        private string _id;

        /// <summary>
        /// 备注
        /// </summary>
        private string _acInvoiceXOBillDesc;

        /// <summary>
        /// 税率
        /// </summary>
        private double? _taxRate;

        /// <summary>
        /// 税别
        /// </summary>
        private int? _taxRateType;

        /// <summary>
        /// 税额
        /// </summary>
        private decimal? _taxRateMoney;

        /// <summary>
        /// 合计
        /// </summary>
        private decimal? _heJiMoney;

        /// <summary>
        /// 总额
        /// </summary>
        private decimal? _zongMoney;

        /// <summary>
        /// 
        /// </summary>
        private DateTime? _insertTime;

        /// <summary>
        /// 
        /// </summary>
        private DateTime? _updateTime;

        /// <summary>
        /// 已核销金额
        /// </summary>
        private decimal? _mHeXiaoJingE;

        /// <summary>
        /// 
        /// </summary>
        private DateTime? _ySDate;

        /// <summary>
        /// 
        /// </summary>
        private decimal? _invoiceAllowanceTotal;

        /// <summary>
        /// 
        /// </summary>
        private decimal? _noHeXiaoTotal;

        /// <summary>
        /// 
        /// </summary>
        private string _customerShouPiaoId;

        private int? _auditState;

        private string _auditEmpId;

        private Employee _auditEmp;

        /// <summary>
        /// 客户
        /// </summary>
        private Customer _customerShouPiao;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee1;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee0;
        /// <summary>
        /// 客户
        /// </summary>
        private Customer _customer;

        #endregion

        #region Properties

        /// <summary>
        /// 销售发票主键
        /// </summary>
        public string AcInvoiceXOBillId
        {
            get
            {
                return this._acInvoiceXOBillId;
            }
            set
            {
                this._acInvoiceXOBillId = value;
            }
        }

        /// <summary>
        /// 操作人
        /// </summary>
        public string EmployeeId
        {
            get
            {
                return this._employeeId;
            }
            set
            {
                this._employeeId = value;
            }
        }

        /// <summary>
        /// 经办人
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
        /// 审核人
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
        /// 客户编号
        /// </summary>
        public string CustomerId
        {
            get
            {
                return this._customerId;
            }
            set
            {
                this._customerId = value;
            }
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int? AuditingState
        {
            get
            {
                return this._auditingState;
            }
            set
            {
                this._auditingState = value;
            }
        }

        /// <summary>
        /// 单据状态
        /// </summary>
        public int? InvoiceStatus
        {
            get
            {
                return this._invoiceStatus;
            }
            set
            {
                this._invoiceStatus = value;
            }
        }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditingStateDate
        {
            get
            {
                return this._auditingStateDate;
            }
            set
            {
                this._auditingStateDate = value;
            }
        }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime? AcInvoiceXOBillDate
        {
            get
            {
                return this._acInvoiceXOBillDate;
            }
            set
            {
                this._acInvoiceXOBillDate = value;
            }
        }

        /// <summary>
        /// 发票类型
        /// </summary>
        public int? AcInvoiceXOBillType
        {
            get
            {
                return this._acInvoiceXOBillType;
            }
            set
            {
                this._acInvoiceXOBillType = value;
            }
        }

        /// <summary>
        /// 发票号码
        /// </summary>
        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string AcInvoiceXOBillDesc
        {
            get
            {
                return this._acInvoiceXOBillDesc;
            }
            set
            {
                this._acInvoiceXOBillDesc = value;
            }
        }

        /// <summary>
        /// 税率
        /// </summary>
        public double? TaxRate
        {
            get
            {
                return this._taxRate;
            }
            set
            {
                this._taxRate = value;
            }
        }

        /// <summary>
        /// 税别
        /// </summary>
        public int? TaxRateType
        {
            get
            {
                return this._taxRateType;
            }
            set
            {
                this._taxRateType = value;
            }
        }

        /// <summary>
        /// 税额
        /// </summary>
        public decimal? TaxRateMoney
        {
            get
            {
                return this._taxRateMoney;
            }
            set
            {
                this._taxRateMoney = value;
            }
        }

        /// <summary>
        /// 合计
        /// </summary>
        public decimal? HeJiMoney
        {
            get
            {
                return this._heJiMoney;
            }
            set
            {
                this._heJiMoney = value;
            }
        }

        /// <summary>
        /// 总额
        /// </summary>
        public decimal? ZongMoney
        {
            get
            {
                return this._zongMoney;
            }
            set
            {
                this._zongMoney = value;
            }
        }

        /// <summary>
        /// 
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
        /// 
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
        /// 已核销金额
        /// </summary>
        public decimal? mHeXiaoJingE
        {
            get
            {
                return this._mHeXiaoJingE;
            }
            set
            {
                this._mHeXiaoJingE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? YSDate
        {
            get
            {
                return this._ySDate;
            }
            set
            {
                this._ySDate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? InvoiceAllowanceTotal
        {
            get
            {
                return this._invoiceAllowanceTotal;
            }
            set
            {
                this._invoiceAllowanceTotal = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? NoHeXiaoTotal
        {
            get
            {
                return this._noHeXiaoTotal;
            }
            set
            {
                this._noHeXiaoTotal = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustomerShouPiaoId
        {
            get
            {
                return this._customerShouPiaoId;
            }
            set
            {
                this._customerShouPiaoId = value;
            }
        }
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

        public virtual string AuditEmpId
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
        /// 客户
        /// </summary>
        public virtual Customer CustomerShouPiao
        {
            get
            {
                return this._customerShouPiao;
            }
            set
            {
                this._customerShouPiao = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee
        {
            get
            {
                return this._employee;
            }
            set
            {
                this._employee = value;
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
        /// 客户
        /// </summary>
        public virtual Customer Customer
        {
            get
            {
                return this._customer;
            }
            set
            {
                this._customer = value;
            }

        }

        /// <summary>
        /// 销售发票主键
        /// </summary>
        public readonly static string PRO_AcInvoiceXOBillId = "AcInvoiceXOBillId";

        /// <summary>
        /// 操作人
        /// </summary>
        public readonly static string PRO_EmployeeId = "EmployeeId";

        /// <summary>
        /// 经办人
        /// </summary>
        public readonly static string PRO_Employee0Id = "Employee0Id";

        /// <summary>
        /// 审核人
        /// </summary>
        public readonly static string PRO_Employee1Id = "Employee1Id";

        /// <summary>
        /// 客户编号
        /// </summary>
        public readonly static string PRO_CustomerId = "CustomerId";

        /// <summary>
        /// 审核状态
        /// </summary>
        public readonly static string PRO_AuditingState = "AuditingState";

        /// <summary>
        /// 单据状态
        /// </summary>
        public readonly static string PRO_InvoiceStatus = "InvoiceStatus";

        /// <summary>
        /// 审核日期
        /// </summary>
        public readonly static string PRO_AuditingStateDate = "AuditingStateDate";

        /// <summary>
        /// 开票日期
        /// </summary>
        public readonly static string PRO_AcInvoiceXOBillDate = "AcInvoiceXOBillDate";

        /// <summary>
        /// 发票类型
        /// </summary>
        public readonly static string PRO_AcInvoiceXOBillType = "AcInvoiceXOBillType";

        /// <summary>
        /// 发票号码
        /// </summary>
        public readonly static string PRO_Id = "Id";

        /// <summary>
        /// 备注
        /// </summary>
        public readonly static string PRO_AcInvoiceXOBillDesc = "AcInvoiceXOBillDesc";

        /// <summary>
        /// 税率
        /// </summary>
        public readonly static string PRO_TaxRate = "TaxRate";

        /// <summary>
        /// 税别
        /// </summary>
        public readonly static string PRO_TaxRateType = "TaxRateType";

        /// <summary>
        /// 税额
        /// </summary>
        public readonly static string PRO_TaxRateMoney = "TaxRateMoney";

        /// <summary>
        /// 合计
        /// </summary>
        public readonly static string PRO_HeJiMoney = "HeJiMoney";

        /// <summary>
        /// 总额
        /// </summary>
        public readonly static string PRO_ZongMoney = "ZongMoney";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_InsertTime = "InsertTime";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_UpdateTime = "UpdateTime";

        /// <summary>
        /// 已核销金额
        /// </summary>
        public readonly static string PRO_mHeXiaoJingE = "mHeXiaoJingE";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_YSDate = "YSDate";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_InvoiceAllowanceTotal = "InvoiceAllowanceTotal";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_NoHeXiaoTotal = "NoHeXiaoTotal";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_CustomerShouPiaoId = "CustomerShouPiaoId";

        public readonly static string PRO_AuditState = "AuditState";

        public readonly static string PRO_AuditEmpId = "AuditEmpId";
        #endregion

        private int _IsCancel;

        public int IsCancel
        {
            get { return _IsCancel; }
            set { _IsCancel = value; }
        }

        private string _invoiceType;

        /// <summary>
        /// 发票类别
        /// </summary>
        public string InvoiceType
        {
            get { return _invoiceType; }
            set { _invoiceType = value; }
        }

        private string _clearanceType;

        /// <summary>
        /// 通关方式注记
        /// </summary>
        public string ClearanceType
        {
            get { return _clearanceType; }
            set { _clearanceType = value; }
        }

        private decimal _exchangeRate;

        /// <summary>
        /// 汇率
        /// </summary>
        public decimal ExchangeRate
        {
            get { return _exchangeRate; }
            set { _exchangeRate = value; }
        }

        private string _currency;

        /// <summary>
        /// 币别
        /// </summary>
        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        private bool _huikaiNote;

        public bool HuikaiNote
        {
            get { return _huikaiNote; }
            set { _huikaiNote = value; }
        }

        private string _salesType;

        /// <summary>
        /// 销售类别
        /// </summary>
        public string SalesType
        {
            get { return _salesType; }
            set { _salesType = value; }
        }

        private string _relatedNumbers;

        /// <summary>
        /// 相关号码
        /// </summary>
        public string RelatedNumbers
        {
            get { return _relatedNumbers; }
            set { _relatedNumbers = value; }
        }

        private string _invoiceXSId;

        public string InvoiceXSId
        {
            get { return _invoiceXSId == null ? null : _invoiceXSId.TrimEnd(','); }
            set { _invoiceXSId = value; }
        }


        public readonly static string PRO_IsCancel = "IsCancel";

        public readonly static string PRO_InvoiceType = "InvoiceType";

        public readonly static string PRO_ClearanceType = "ClearanceType";

        public readonly static string PRO_ExchangeRate = "ExchangeRate";

        public readonly static string PRO_Currency = "Currency";

        public readonly static string PRO_HuikaiNote = "HuikaiNote";

        public readonly static string PRO_SalesType = "SalesType";

        public readonly static string PRO_RelatedNumbers = "RelatedNumbers";
    }
}
