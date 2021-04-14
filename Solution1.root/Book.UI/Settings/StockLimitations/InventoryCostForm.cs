using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Settings.StockLimitations
{
    public partial class InventoryCostForm : DevExpress.XtraEditors.XtraForm
    {
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.StockManager stockManager = new Book.BL.StockManager();

        public InventoryCostForm()
        {
            InitializeComponent();

            date_Search.DateTime = DateTime.Now;
            bindingSourceCategory.DataSource = new BL.ProductCategoryManager().Select();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (date_Search.EditValue == null)
            {
                MessageBox.Show("請先選擇查詢日期", "提示", MessageBoxButtons.OK);
                return;
            }

            //第一步先查詢對應日期的 商品庫存。若日期<當天日期則查詢對應日期的 即時庫存,否則,直接用現在的商品庫存
            IList<Model.Product> listPro = productManager.SelectQtyAndCost(lue_CategoryStart.EditValue == null ? "" : lue_CategoryStart.EditValue.ToString(), lue_CategoryEnd.EditValue == null ? "" : lue_CategoryEnd.EditValue.ToString());

            if (date_Search.DateTime < DateTime.Now.Date)
            {
                DateTime date = this.date_Search.DateTime.Date.AddDays(1);

                foreach (var item in listPro)
                {
                    var stockList = this.stockManager.SelectJiShi(item.ProductId, date, DateTime.Now).OrderByDescending(o => o.InvoiceDate.Value.Date);

                    //因為調撥後總庫存不變，暫不處理
                    if (stockList != null && stockList.Count() > 0)
                    {
                        //若有盘点，以盘点后库存为准                        
                        Model.StockSeach seach = stockList.Where(s => s.InvoiceTypeIndex == 3).OrderByDescending(o => o.InvoiceDate).ThenByDescending(d => d.InsertTime).FirstOrDefault();
                        if (seach != null)
                        {
                            stockList = stockList.Where(l => l.InvoiceDate.Value.Date <= seach.InvoiceDate.Value.Date && l.InsertTime.Value < seach.InsertTime.Value)
                                 .OrderByDescending(o => o.InvoiceDate.Value.Date);

                            item.StocksQuantity = seach.StockCheckBookQuantity;
                        }

                        if (stockList != null && stockList.Count() > 0)
                        {
                            foreach (Model.StockSeach stock in stockList)
                            {

                                if (stock.InvoiceTypeIndex == 0)
                                {
                                    item.StocksQuantity = Convert.ToDouble(item.StocksQuantity) + stock.InvoiceQuantity.Value;

                                }
                                if (stock.InvoiceTypeIndex == 1)
                                {
                                    item.StocksQuantity = Convert.ToDouble(item.StocksQuantity) - stock.InvoiceQuantity.Value;

                                }
                            }


                        }
                    }
                }

            }

            //計算成本，目前之分自製，採購
            //ProductType，原指商品 常態，特殊，耳塞。此處為了查詢時不添加新字段用來當做商品生產類型：0 自製；1 外購；2，委外
            var zizhiList = listPro.Where(l => l.ProductType == 0);
            var caigouList = listPro.Where(l => l.ProductType != 0);

            foreach (var item in zizhiList)
            {
                item.TotalCost = Convert.ToDecimal(item.StocksQuantity.Value) * item.ReferenceCost.Value;
            }

            foreach (var item in caigouList)
            {

            }

            bindingSourceDetail.DataSource = listPro;
            gridControl1.RefreshDataSource();
        }


    }
}