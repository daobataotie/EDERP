using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Model
{
    /// <summary>
    /// 商品成本分析
    /// </summary>
    public class ProductCost
    {
        public decimal Price { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string ProductId { get; set; }
    }
}
