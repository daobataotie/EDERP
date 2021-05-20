using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Threading;

namespace Book.UI.Settings.StockLimitations
{
    /// <summary>
    /// 库存成本分析
    /// </summary>
    public partial class InventoryCostForm : DevExpress.XtraEditors.XtraForm
    {
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.StockManager stockManager = new Book.BL.StockManager();

        public InventoryCostForm()
        {
            InitializeComponent();

            date_Search.DateTime = DateTime.Now;
            bindingSourceCategory.DataSource = new BL.ProductCategoryManager().Select();
            ncc_Depot.Choose = new Book.UI.Invoices.ChooseDepot();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string depotId = string.Empty;
                if (ncc_Depot.EditValue != null)
                {
                    depotId = (ncc_Depot.EditValue as Model.Depot).DepotId;
                }


                #region 目前只查当前库存，历史库存太耗时，暂不处理
                //if (date_Search.EditValue == null)
                //{
                //    MessageBox.Show("請先選擇查詢日期", "提示", MessageBoxButtons.OK);
                //    return;
                //}

                //第一步先查詢對應日期的 商品庫存。若日期<當天日期則查詢對應日期的 即時庫存,否則,直接用現在的商品庫存
                IList<Model.Product> listPro = productManager.SelectQtyAndCost(lue_CategoryStart.EditValue == null ? "" : lue_CategoryStart.EditValue.ToString(), lue_CategoryEnd.EditValue == null ? "" : lue_CategoryEnd.EditValue.ToString(), depotId);
                DateTime date = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);

                //if (date_Search.DateTime < DateTime.Now.Date)
                //{
                //     date = this.date_Search.DateTime.Date.AddDays(1);

                //    #region 多线程版，有问题
                //    ////每1000条数据开一个线程处理
                //    //int count = (int)Math.Ceiling(listPro.Count / 1000D);

                //    //List<ManualResetEvent> listMre = new List<ManualResetEvent>();
                //    //for (int i = 0; i < count; i++)
                //    //{
                //    //    var subList = listPro.Skip(i * 1000).Take(1000);

                //    //    ManualResetEvent mre = new ManualResetEvent(false);
                //    //    Thread t = new Thread((obj) =>
                //    //    {
                //    //        ManualResetEvent manualResetEvent = (obj as object[])[0] as ManualResetEvent;
                //    //        IEnumerable<Model.Product> list = (obj as object[])[1] as IEnumerable<Model.Product>;

                //    //        CalcHistoryStock(list, date);
                //    //        manualResetEvent.Set();
                //    //    });

                //    //    t.SetApartmentState(ApartmentState.MTA);
                //    //    t.IsBackground = true;
                //    //    listMre.Add(mre);
                //    //    t.Start(new object[2] { mre, subList });
                //    //}

                //    //好像是多线程和数据库查询冲突，执行到此会报 WaitAll for multiple handles on a STA thread is not supported，但是吧这句话注释掉就不会报错了，但是sqlhelper会报连接池错误，大概是因为在线程中开启了多个数据库链接吧
                //    //以后有时间了试试在计算每个商品历史库存的循环内部，查出历史数据后启动线程看看效率如何
                //    //WaitHandle.WaitAll(listMre.ToArray()); 
                //    #endregion

                //    CalcHistoryStock2(listPro, date);
                //}

                #endregion

                //IList<Model.Product> listPro = productManager.SelectQtyAndCost(lue_CategoryStart.EditValue == null ? "" : lue_CategoryStart.EditValue.ToString(), lue_CategoryEnd.EditValue == null ? "" : lue_CategoryEnd.EditValue.ToString(), depotId);

                /* 計算成本，目前之分自製，採購，委外
                 * ProductType，原指商品 常態，特殊，耳塞。此處為了查詢時不添加新字段用來當做商品生產類型：0 自製；1 外購；2，委外
                 * 0 自制：直接参考成本 (理论上还需要计算生产线上的库存，这个后期再考虑)
                 * 1 外购：拿该商品去进库单查找最近一个月平均采购进库的单价
                 * 2 委外：原理同采购单
                 */
                var zizhiList = listPro.Where(l => l.ProductType == 0 && l.StocksQuantity != 0);
                var caigouList = listPro.Where(l => l.ProductType == 1 && l.StocksQuantity != 0);
                var weiwaiList = listPro.Where(l => l.ProductType == 2 && l.StocksQuantity != 0);

                //按照月份计算商品的平均单价并按照月份倒序
                IList<Model.ProductCost> listCaigou = productManager.SelectCGPriceByMonth();
                IList<Model.ProductCost> listWeiwai = productManager.SelectOtherInDepotPriceByMonth();

                //DateTime date = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);

                foreach (var item in zizhiList)
                {
                    item.TotalCost = Convert.ToDecimal(item.StocksQuantity.Value) * item.ReferenceCost.Value;
                }

                foreach (var item in caigouList)
                {
                    //找到离查询月份最近月份的平均单价
                    Model.ProductCost pc = listCaigou.Where(c => (date - c.InvoiceDate).Days >= 0 && c.ProductId == item.ProductId).FirstOrDefault();
                    if (pc != null)
                    {
                        item.ReferenceCost = pc.Price;
                        item.TotalCost = Convert.ToDecimal(item.StocksQuantity.Value) * pc.Price;
                    }
                    else
                        item.ReferenceCost = 0;
                }

                foreach (var item in weiwaiList)
                {
                    Model.ProductCost pc = listWeiwai.Where(w => (date - w.InvoiceDate).Days >= 0 && w.ProductId == item.ProductId).FirstOrDefault();
                    if (pc != null)
                    {
                        item.ReferenceCost = pc.Price;
                        item.TotalCost = Convert.ToDecimal(item.StocksQuantity.Value) * pc.Price;
                    }
                    else
                        item.ReferenceCost = 0;
                }


                bindingSourceDetail.DataSource = listPro;
                gridControl1.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK);
                return;
            }
        }

        private void CalcHistoryStock(IEnumerable<Model.Product> listPro, DateTime date)
        {
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
                                item.StocksQuantity = Convert.ToDouble(item.StocksQuantity) + Convert.ToDouble(stock.InvoiceQuantity);

                            }
                            if (stock.InvoiceTypeIndex == 1)
                            {
                                item.StocksQuantity = Convert.ToDouble(item.StocksQuantity) - Convert.ToDouble(stock.InvoiceQuantity);

                            }
                        }

                    }
                }

            }
        }

        private void CalcHistoryStock2(IList<Model.Product> listPro, DateTime date)
        {
            List<ManualResetEvent> listMre = new List<ManualResetEvent>();
            foreach (var item in listPro)
            {
                var stockList = this.stockManager.SelectJiShi(item.ProductId, date, DateTime.Now).OrderByDescending(o => o.InvoiceDate.Value.Date);

                ManualResetEvent mre = new ManualResetEvent(false);
                Thread t = new Thread((obj) =>
                {
                    ManualResetEvent manualResetEvent = (obj as object[])[0] as ManualResetEvent;
                    var list = (obj as object[])[1] as IList<Model.StockSeach>;

                    //因為調撥後總庫存不變，暫不處理
                    if (list != null && list.Count() > 0)
                    {
                        //若有盘点，以盘点后库存为准                        
                        Model.StockSeach seach = list.Where(s => s.InvoiceTypeIndex == 3).OrderByDescending(o => o.InvoiceDate).ThenByDescending(d => d.InsertTime).FirstOrDefault();
                        if (seach != null)
                        {
                            list = list.Where(l => l.InvoiceDate.Value.Date <= seach.InvoiceDate.Value.Date && l.InsertTime.Value < seach.InsertTime.Value)
                                 .OrderByDescending(o => o.InvoiceDate.Value.Date).ToList();

                            item.StocksQuantity = seach.StockCheckBookQuantity;
                        }

                        if (list != null && list.Count() > 0)
                        {
                            foreach (Model.StockSeach stock in list)
                            {

                                if (stock.InvoiceTypeIndex == 0)
                                {
                                    item.StocksQuantity = Convert.ToDouble(item.StocksQuantity) + Convert.ToDouble(stock.InvoiceQuantity);

                                }
                                if (stock.InvoiceTypeIndex == 1)
                                {
                                    item.StocksQuantity = Convert.ToDouble(item.StocksQuantity) - Convert.ToDouble(stock.InvoiceQuantity);

                                }
                            }

                        }
                    }

                    manualResetEvent.Set();
                });

                t.SetApartmentState(ApartmentState.MTA);
                t.IsBackground = true;
                listMre.Add(mre);
                t.Start(new object[2] { mre, stockList });

            }

            WaitHandle.WaitAll(listMre.ToArray());
        }

    }
}