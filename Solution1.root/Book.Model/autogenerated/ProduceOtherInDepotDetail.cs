﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceOtherInDepotDetail.autogenerated.cs
// author: mayanjun
// create date：2012-4-16 16:25:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    public partial class ProduceOtherInDepotDetail
    {
        #region Data

        /// <summary>
        /// 编号
        /// </summary>
        private string _produceOtherInDepotDetailId;

        /// <summary>
        /// 商品编号
        /// </summary>
        private string _productId;

        /// <summary>
        /// 位置编号
        /// </summary>
        private string _depotPositionId;

        /// <summary>
        /// 外包入库编号
        /// </summary>
        private string _produceOtherInDepotId;

        /// <summary>
        /// 规格
        /// </summary>
        private string _productGuige;

        /// <summary>
        /// 单位
        /// </summary>
        private string _productUnit;

        /// <summary>
        /// 数量
        /// </summary>
        private double? _produceQuantity;

        /// <summary>
        /// 单价
        /// </summary>
        private decimal? _producePrice;

        /// <summary>
        /// 金额
        /// </summary>
        private decimal? _produceMoney;

        /// <summary>
        /// 加工价格
        /// </summary>
        private decimal? _processPrice;

        /// <summary>
        /// 生产是否结束
        /// </summary>
        private bool? _produceIsEnd;

        /// <summary>
        /// 
        /// </summary>
        private string _invoiceXOId;

        /// <summary>
        /// 
        /// </summary>
        private string _invoiceXODetailId;

        /// <summary>
        /// 
        /// </summary>
        private string _primaryKeyId;

        /// <summary>
        /// 
        /// </summary>
        private string _produceOtherCompactDetailId;

        /// <summary>
        /// 
        /// </summary>
        private string _description;

        /// <summary>
        /// 
        /// </summary>
        private double? _produceTransferQuantity;

        /// <summary>
        /// 
        /// </summary>
        private double? _produceInDepotQuantity;

        /// <summary>
        /// 
        /// </summary>
        private string _customerId;

        /// <summary>
        /// 
        /// </summary>
        private string _invoiceCusId;

        /// <summary>
        /// 
        /// </summary>
        private string _produceOtherCompactId;

        private decimal? _produceTaxMoney;

        /// <summary>
        /// 客户
        /// </summary>
        private Customer _customer;
        /// <summary>
        /// 客户产品
        /// </summary>
        private CustomerProducts _primaryKey;
        /// <summary>
        /// 库库货位
        /// </summary>
        private DepotPosition _depotPosition;
        /// <summary>
        /// 外包入库
        /// </summary>
        private ProduceOtherInDepot _produceOtherInDepot;
        /// <summary>
        /// 产品
        /// </summary>
        private Product _product;

        #endregion

        #region Properties

        /// <summary>
        /// 编号
        /// </summary>
        public string ProduceOtherInDepotDetailId
        {
            get
            {
                return this._produceOtherInDepotDetailId;
            }
            set
            {
                this._produceOtherInDepotDetailId = value;
            }
        }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }

        /// <summary>
        /// 位置编号
        /// </summary>
        public string DepotPositionId
        {
            get
            {
                return this._depotPositionId;
            }
            set
            {
                this._depotPositionId = value;
            }
        }

        /// <summary>
        /// 外包入库编号
        /// </summary>
        public string ProduceOtherInDepotId
        {
            get
            {
                return this._produceOtherInDepotId;
            }
            set
            {
                this._produceOtherInDepotId = value;
            }
        }

        /// <summary>
        /// 规格
        /// </summary>
        public string ProductGuige
        {
            get
            {
                return this._productGuige;
            }
            set
            {
                this._productGuige = value;
            }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string ProductUnit
        {
            get
            {
                return this._productUnit;
            }
            set
            {
                this._productUnit = value;
            }
        }

        /// <summary>
        /// 进货数量
        /// </summary>
        public double? ProduceQuantity
        {
            get
            {
                return this._produceQuantity;
            }
            set
            {
                this._produceQuantity = value;
            }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? ProducePrice
        {
            get
            {
                return this._producePrice;
            }
            set
            {
                this._producePrice = value;
            }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal? ProduceMoney
        {
            get
            {
                return this._produceMoney;
            }
            set
            {
                this._produceMoney = value;
            }
        }

        /// <summary>
        /// 加工价格
        /// </summary>
        public decimal? ProcessPrice
        {
            get
            {
                return this._processPrice;
            }
            set
            {
                this._processPrice = value;
            }
        }

        /// <summary>
        /// 生产是否结束
        /// </summary>
        public bool? ProduceIsEnd
        {
            get
            {
                return this._produceIsEnd;
            }
            set
            {
                this._produceIsEnd = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string InvoiceXOId
        {
            get
            {
                return this._invoiceXOId;
            }
            set
            {
                this._invoiceXOId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string InvoiceXODetailId
        {
            get
            {
                return this._invoiceXODetailId;
            }
            set
            {
                this._invoiceXODetailId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PrimaryKeyId
        {
            get
            {
                return this._primaryKeyId;
            }
            set
            {
                this._primaryKeyId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProduceOtherCompactDetailId
        {
            get
            {
                return this._produceOtherCompactDetailId;
            }
            set
            {
                this._produceOtherCompactDetailId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        /// <summary>
        /// 转生产数量
        /// </summary>
        public double? ProduceTransferQuantity
        {
            get
            {
                return this._produceTransferQuantity;
            }
            set
            {
                this._produceTransferQuantity = value;
            }
        }

        /// <summary>
        /// 入库数量
        /// </summary>
        public double? ProduceInDepotQuantity
        {
            get
            {
                return this._produceInDepotQuantity;
            }
            set
            {
                this._produceInDepotQuantity = value;
            }
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public string InvoiceCusId
        {
            get
            {
                return this._invoiceCusId;
            }
            set
            {
                this._invoiceCusId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProduceOtherCompactId
        {
            get
            {
                return this._produceOtherCompactId;
            }
            set
            {
                this._produceOtherCompactId = value;
            }
        }

        /// <summary>
        /// 稅額
        /// </summary>
        public decimal? ProduceTaxMoney
        {
            get { return _produceTaxMoney; }
            set { _produceTaxMoney = value; }
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
        /// 客户产品
        /// </summary>
        public virtual CustomerProducts PrimaryKey
        {
            get
            {
                return this._primaryKey;
            }
            set
            {
                this._primaryKey = value;
            }

        }
        /// <summary>
        /// 库库货位
        /// </summary>
        public virtual DepotPosition DepotPosition
        {
            get
            {
                return this._depotPosition;
            }
            set
            {
                this._depotPosition = value;
            }

        }
        /// <summary>
        /// 外包入库
        /// </summary>
        public virtual ProduceOtherInDepot ProduceOtherInDepot
        {
            get
            {
                return this._produceOtherInDepot;
            }
            set
            {
                this._produceOtherInDepot = value;
            }

        }
        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product
        {
            get
            {
                return this._product;
            }
            set
            {
                this._product = value;
            }

        }
        /// <summary>
        /// 编号
        /// </summary>
        public readonly static string PRO_ProduceOtherInDepotDetailId = "ProduceOtherInDepotDetailId";

        /// <summary>
        /// 商品编号
        /// </summary>
        public readonly static string PRO_ProductId = "ProductId";

        /// <summary>
        /// 位置编号
        /// </summary>
        public readonly static string PRO_DepotPositionId = "DepotPositionId";

        /// <summary>
        /// 外包入库编号
        /// </summary>
        public readonly static string PRO_ProduceOtherInDepotId = "ProduceOtherInDepotId";

        /// <summary>
        /// 规格
        /// </summary>
        public readonly static string PRO_ProductGuige = "ProductGuige";

        /// <summary>
        /// 单位
        /// </summary>
        public readonly static string PRO_ProductUnit = "ProductUnit";

        /// <summary>
        /// 数量
        /// </summary>
        public readonly static string PRO_ProduceQuantity = "ProduceQuantity";

        /// <summary>
        /// 单价
        /// </summary>
        public readonly static string PRO_ProducePrice = "ProducePrice";

        /// <summary>
        /// 金额
        /// </summary>
        public readonly static string PRO_ProduceMoney = "ProduceMoney";

        /// <summary>
        /// 加工价格
        /// </summary>
        public readonly static string PRO_ProcessPrice = "ProcessPrice";

        /// <summary>
        /// 生产是否结束
        /// </summary>
        public readonly static string PRO_ProduceIsEnd = "ProduceIsEnd";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_InvoiceXOId = "InvoiceXOId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_InvoiceXODetailId = "InvoiceXODetailId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_PrimaryKeyId = "PrimaryKeyId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_ProduceOtherCompactDetailId = "ProduceOtherCompactDetailId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Description = "Description";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_ProduceTransferQuantity = "ProduceTransferQuantity";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_ProduceInDepotQuantity = "ProduceInDepotQuantity";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_CustomerId = "CustomerId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_InvoiceCusId = "InvoiceCusId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_ProduceOtherCompactId = "ProduceOtherCompactId";

        public readonly static string PRO_ProduceTaxMoney = "ProduceTaxMoney";

        #endregion

        private string _generateInputCheck;

        public string GenerateInputCheck
        {
            get { return _generateInputCheck; }
            set { _generateInputCheck = value; }
        }

        public readonly static string PRO_GenerateInputCheck = "GenerateInputCheck";
    }
}
