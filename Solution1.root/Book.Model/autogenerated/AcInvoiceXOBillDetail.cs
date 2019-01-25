﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcInvoiceXOBillDetail.autogenerated.cs
// author: mayanjun
// create date：2012-2-23 10:21:51
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class AcInvoiceXOBillDetail
	{
		#region Data

		/// <summary>
		/// 销售发票详细编号
		/// </summary>
		private string _acInvoiceXOBillDetailId;
		
		/// <summary>
		/// 销售发票主键
		/// </summary>
		private string _acInvoiceXOBillId;
		
		/// <summary>
		/// 单据编号
		/// </summary>
		private string _invoiceId;
		
		/// <summary>
		/// 折让
		/// </summary>
		private decimal? _invoiceAllowance;
		
		/// <summary>
		/// 单价
		/// </summary>
		private decimal? _invoiceXODetailPrice;
		
		/// <summary>
		/// 金额
		/// </summary>
		private decimal? _invoiceXODetailMoney;
		
		/// <summary>
		/// 含税单价
		/// </summary>
		private decimal? _invoiceXODetailTaxPrice;
		
		/// <summary>
		/// 税价合计
		/// </summary>
		private decimal? _invoiceXODetailTaxMoney;
		
		/// <summary>
		/// 税额
		/// </summary>
		private decimal? _invoiceXODetailTax;
		
		/// <summary>
		/// 
		/// </summary>
		private string _productId;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _invoiceXODetaiInQuantity;
		
		/// <summary>
		/// 
		/// </summary>
		private string _invoiceXODetailId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _invoiceProductUnit;
		
		/// <summary>
		/// 产品
		/// </summary>
		private Product _product;
		/// <summary>
		/// 销售发票
		/// </summary>
		private AcInvoiceXOBill _acInvoiceXOBill;
		/// <summary>
		/// 出库单
		/// </summary>
		private InvoiceXS _invoice;

        private InvoiceXSDetail _invoiceXODetail;

        public InvoiceXSDetail InvoiceXODetail
        {
            get { return _invoiceXODetail; }
            set { _invoiceXODetail = value; }
        }
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 销售发票详细编号
		/// </summary>
		public string AcInvoiceXOBillDetailId
		{
			get 
			{
				return this._acInvoiceXOBillDetailId;
			}
			set 
			{
				this._acInvoiceXOBillDetailId = value;
			}
		}

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
		/// 单据编号
		/// </summary>
		public string InvoiceId
		{
			get 
			{
				return this._invoiceId;
			}
			set 
			{
				this._invoiceId = value;
			}
		}

		/// <summary>
		/// 折让
		/// </summary>
		public decimal? InvoiceAllowance
		{
			get 
			{
				return this._invoiceAllowance;
			}
			set 
			{
				this._invoiceAllowance = value;
			}
		}

		/// <summary>
		/// 单价
		/// </summary>
		public decimal? InvoiceXODetailPrice
		{
			get 
			{
				return this._invoiceXODetailPrice;
			}
			set 
			{
				this._invoiceXODetailPrice = value;
			}
		}

		/// <summary>
		/// 金额
		/// </summary>
		public decimal? InvoiceXODetailMoney
		{
			get 
			{
				return this._invoiceXODetailMoney;
			}
			set 
			{
				this._invoiceXODetailMoney = value;
			}
		}

		/// <summary>
		/// 含税单价
		/// </summary>
		public decimal? InvoiceXODetailTaxPrice
		{
			get 
			{
				return this._invoiceXODetailTaxPrice;
			}
			set 
			{
				this._invoiceXODetailTaxPrice = value;
			}
		}

		/// <summary>
		/// 税价合计
		/// </summary>
		public decimal? InvoiceXODetailTaxMoney
		{
			get 
			{
				return this._invoiceXODetailTaxMoney;
			}
			set 
			{
				this._invoiceXODetailTaxMoney = value;
			}
		}

		/// <summary>
		/// 税额
		/// </summary>
		public decimal? InvoiceXODetailTax
		{
			get 
			{
				return this._invoiceXODetailTax;
			}
			set 
			{
				this._invoiceXODetailTax = value;
			}
		}

		/// <summary>
		/// 
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
		/// 
		/// </summary>
		public double? InvoiceXODetaiInQuantity
		{
			get 
			{
				return this._invoiceXODetaiInQuantity;
			}
			set 
			{
				this._invoiceXODetaiInQuantity = value;
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
		public string InvoiceProductUnit
		{
			get 
			{
				return this._invoiceProductUnit;
			}
			set 
			{
				this._invoiceProductUnit = value;
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
		/// 销售发票
		/// </summary>
		public virtual AcInvoiceXOBill AcInvoiceXOBill
		{
			get
			{
				return this._acInvoiceXOBill;
			}
			set
			{
				this._acInvoiceXOBill = value;
			}
			
		}
		/// <summary>
		/// 出库单
		/// </summary>
		public virtual InvoiceXS Invoice
		{
			get
			{
				return this._invoice;
			}
			set
			{
				this._invoice = value;
			}
			
		}
		/// <summary>
		/// 销售发票详细编号
		/// </summary>
		public readonly static string PRO_AcInvoiceXOBillDetailId = "AcInvoiceXOBillDetailId";
		
		/// <summary>
		/// 销售发票主键
		/// </summary>
		public readonly static string PRO_AcInvoiceXOBillId = "AcInvoiceXOBillId";
		
		/// <summary>
		/// 单据编号
		/// </summary>
		public readonly static string PRO_InvoiceId = "InvoiceId";
		
		/// <summary>
		/// 折让
		/// </summary>
		public readonly static string PRO_InvoiceAllowance = "InvoiceAllowance";
		
		/// <summary>
		/// 单价
		/// </summary>
		public readonly static string PRO_InvoiceXODetailPrice = "InvoiceXODetailPrice";
		
		/// <summary>
		/// 金额
		/// </summary>
		public readonly static string PRO_InvoiceXODetailMoney = "InvoiceXODetailMoney";
		
		/// <summary>
		/// 含税单价
		/// </summary>
		public readonly static string PRO_InvoiceXODetailTaxPrice = "InvoiceXODetailTaxPrice";
		
		/// <summary>
		/// 税价合计
		/// </summary>
		public readonly static string PRO_InvoiceXODetailTaxMoney = "InvoiceXODetailTaxMoney";
		
		/// <summary>
		/// 税额
		/// </summary>
		public readonly static string PRO_InvoiceXODetailTax = "InvoiceXODetailTax";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProductId = "ProductId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InvoiceXODetaiInQuantity = "InvoiceXODetaiInQuantity";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InvoiceXODetailId = "InvoiceXODetailId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InvoiceProductUnit = "InvoiceProductUnit";
		

		#endregion

        private string _InvoiceCOIdNote;

        public string InvoiceCOIdNote
        {
            get { return _InvoiceCOIdNote; }
            set { _InvoiceCOIdNote = value; }
        }

        private decimal _price2;

        public decimal Price2
        {
            get { return _price2; }
            set { _price2 = value; }
        }

        private decimal _quantity2;

        public decimal Quantity2
        {
            get { return _quantity2; }
            set { _quantity2 = value; }
        }

        private string _productUnit2;

        public string ProductUnit2
        {
            get { return _productUnit2; }
            set { _productUnit2 = value; }
        }
        
        public readonly static string PRO_InvoiceCOIdNote = "InvoiceCOIdNote";

        public readonly static string PRO_Price2 = "Price2";

        public readonly static string PRO_Quantity2 = "Quantity2";

        public readonly static string PRO_ProductUnit2 = "ProductUnit2";
	}
}
