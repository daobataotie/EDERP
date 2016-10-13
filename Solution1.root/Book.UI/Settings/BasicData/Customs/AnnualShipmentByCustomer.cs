using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using CrystalDecisions.Shared;
using System.Reflection;
using Microsoft.CSharp;
using Microsoft.Office.Core;


namespace Book.UI.Settings.BasicData.Customs
{
    public partial class AnnualShipmentByCustomer : DevExpress.XtraEditors.XtraForm
    {
        BL.InvoiceXSDetailManager invoiceXSDetailManager = new Book.BL.InvoiceXSDetailManager();
        IList<Model.Product> customerProductList;
        List<ProductShipment> productShipmentList = new List<ProductShipment>();

        public AnnualShipmentByCustomer()
        {
            InitializeComponent();
            this.bindingSourceProduct.DataSource = new BL.ProductManager().GetProductBaseInfo();

            this.nccCustomer.Choose = new Customs.ChooseCustoms();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            productShipmentList.Clear();
            if (this.customerProductList == null || this.customerProductList.Count == 0)
            {
                MessageBox.Show("客戶商品不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }
            if (this.date_Start.EditValue == null)
            {
                MessageBox.Show("起始日期不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }
            if (this.date_End.EditValue == null)
            {
                MessageBox.Show("結束日期不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }
            int showType = this.radioGroup1.SelectedIndex;

            //this.bindingSourceHeader.DataSource = invoiceXSDetailManager.SelectAnnualShipment((this.but_Product.EditValue as Model.Product).ProductId, this.date_Start.DateTime, this.date_End.DateTime, (this.nccCustomer.EditValue == null ? null : (this.nccCustomer.EditValue as Model.Customer).CustomerId));
            //this.gridControl1.RefreshDataSource();

            System.Data.DataTable dt;
            foreach (var item in customerProductList)
            {
                dt = invoiceXSDetailManager.SelectAnnualShipment(item.ProductId, this.date_Start.DateTime, this.date_End.DateTime, null, showType);

                ProductShipment ps;

                if (productShipmentList.Any(d => d.CustomerProductName == item.CustomerProductName))
                {
                    ps = productShipmentList.First(d => d.CustomerProductName == item.CustomerProductName);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ShipmentDetail sd = ps.ShipmentDetail.First(d => d.Year == dr["ShipmentYear"].ToString());
                            sd.Quantity = (Convert.ToDecimal(dr["ShipmentQuantity"].ToString()) + Convert.ToDecimal(sd.Quantity)).ToString();
                        }
                    }
                }
                else
                {
                    ps = new ProductShipment();
                    ps.ProductName = item.ProductName;
                    ps.CustomerProductName = item.CustomerProductName;
                    ps.ShipmentDetail = new List<ShipmentDetail>();

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ShipmentDetail sd = new ShipmentDetail();
                            sd.Year = dr["ShipmentYear"].ToString();
                            sd.Quantity = dr["ShipmentQuantity"].ToString();
                            ps.ShipmentDetail.Add(sd);
                        }
                    }

                    productShipmentList.Add(ps);
                }
                if (showType == 0)
                {
                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        if (!ps.ShipmentDetail.Any(d => d.Year == i.ToString()))
                        {
                            ShipmentDetail sd = new ShipmentDetail();
                            sd.Year = i.ToString();
                            sd.Quantity = "0";
                            ps.ShipmentDetail.Add(sd);
                        }
                    }
                }
                else
                {
                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            if (!ps.ShipmentDetail.Any(d => d.Year == i.ToString() + "." + j.ToString()))
                            {
                                ShipmentDetail sd = new ShipmentDetail();
                                sd.Year = i.ToString() + "." + j.ToString();
                                sd.Quantity = "0";
                                ps.ShipmentDetail.Add(sd);
                            }
                        }
                    }
                }
                //ps.ShipmentDetail = ps.ShipmentDetail.OrderBy(d => d.Year).ToList();
            }
            if (productShipmentList.Count == 0)
            {
                MessageBox.Show("無數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
            try
            {
                Type objClassType = null;
                objClassType = Type.GetTypeFromProgID("Excel.Application");
                if (objClassType == null)
                {
                    MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                    return;
                }

                //productShipmentList = new List<ProductShipment> { new ProductShipment { CustomerProductName = "", ProductName = "", ShipmentDetail = new List<ShipmentDetail> { new ShipmentDetail { Year = "", Quantity = "" } } } };
                //object objExcel = Activator.CreateInstance(objClassType);
                //objExcel.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null, objExcel, new object[] { true });
                //object wookBook = objExcel.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, objExcel, null);
                //wookBook.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, wookBook, new object[] { true });
                //object rangeMerge = objExcel.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objExcel, new object[] { string.Format("A1:A{0}", productShipmentList.Count) });
                //rangeMerge.GetType().InvokeMember("MergeCells", BindingFlags.SetProperty, null, rangeMerge, new object[] { true });
                //object cells = objExcel.GetType().InvokeMember("Cells", BindingFlags.GetProperty, null, objExcel, null);
                //cells.GetType().InvokeMember("ColumnWidth", BindingFlags.SetProperty, null, cells, new object[] { 25 });
                //object cell11 = objExcel.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objExcel, new object[] { "A1:A1" });
                //cell11.GetType().InvokeMember("Value2", BindingFlags.SetProperty, null, cell11, new object[] { (this.nccCustomer.EditValue as Model.Customer).CustomerShortName });
                //cell11.GetType().InvokeMember("RowHeight", BindingFlags.SetProperty, null, cell11, new object[] { 25 });
                //object font = cell11.GetType().InvokeMember("Font", BindingFlags.GetProperty, null, cell11, null);
                //font.GetType().InvokeMember("Size", BindingFlags.SetProperty, null, font, new object[] { 20 });
                //object cellDate = objExcel.GetType().InvokeMember("Range",BindingFlags.GetProperty,null,objExcel,new object[]{string."A:"});

                //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                dynamic excel = Activator.CreateInstance(objClassType);
                excel.Application.Workbooks.Add(true);

                dynamic r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, productShipmentList.Count]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 25;
                excel.Cells[1, 1] = (this.nccCustomer.EditValue as Model.Customer).CustomerShortName;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, productShipmentList.Count + 1], excel.Cells[1, productShipmentList.Count + 1]).HorizontalAlignment = XlVAlign.xlVAlignCenter;

                int rowCount = this.date_End.DateTime.Year - this.date_Start.DateTime.Year + 1;
                if (showType != 0)
                    rowCount = rowCount * 12;

                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).BorderAround(LineStyle.SingleLine, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, productShipmentList.Count + 1]).Interior.ColorIndex = 15;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[rowCount + 2, productShipmentList.Count + 1]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[rowCount + 2, productShipmentList.Count + 1]).WrapText = true;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[rowCount + 2, productShipmentList.Count + 1]).EntireRow.AutoFit();
                excel.get_Range(excel.Cells[3, 1], excel.Cells[rowCount + 2, productShipmentList.Count + 1]).RowHeight = 20;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[rowCount + 2, productShipmentList.Count + 1]).Font.Size = 13;
                //excel.get_Range(excel.Cells[2, 1], excel.Cells[this.date_End.DateTime.Year - this.date_Start.DateTime.Year + 1 + 2, productShipmentList.Count + 1]).BorderAround(LineStyle.SingleLine, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");

                for (int j = 0; j < productShipmentList.Count; j++)
                {
                    excel.Cells[2, j + 2] = productShipmentList[j].CustomerProductName;
                    excel.get_Range(excel.Cells[2, j + 2], excel.Cells[2, j + 2]).BorderAround(LineStyle.SingleLine, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");
                }

                int rows = 3;
                if (showType == 0)
                {
                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        excel.Cells[rows, 1] = i.ToString() + "年";
                        excel.get_Range(excel.Cells[rows, 1], excel.Cells[rows, 1]).BorderAround(LineStyle.SingleLine, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");

                        for (int j = 0; j < productShipmentList.Count; j++)
                        {
                            excel.Cells[rows, j + 2] = productShipmentList[j].ShipmentDetail.First(d => d.Year == i.ToString()).Quantity;
                            excel.get_Range(excel.Cells[rows, j + 2], excel.Cells[rows, j + 2]).BorderAround(LineStyle.SingleLine, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");
                        }

                        rows++;
                    }
                }
                else
                {
                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        for (int l = 1; l <= 12; l++)
                        {
                            excel.Cells[rows, 1] = i.ToString() + "年" + l.ToString() + "月";
                            excel.get_Range(excel.Cells[rows, 1], excel.Cells[rows, 1]).BorderAround(LineStyle.SingleLine, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");

                            for (int j = 0; j < productShipmentList.Count; j++)
                            {
                                excel.Cells[rows, j + 2] = productShipmentList[j].ShipmentDetail.First(d => d.Year == i.ToString() + "." + l.ToString()).Quantity;
                                excel.get_Range(excel.Cells[rows, j + 2], excel.Cells[rows, j + 2]).BorderAround(LineStyle.SingleLine, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");
                            }

                            rows++;
                        }
                    }
                }


                excel.Visible = true;//是否打开该Excel文件
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void btn_ChooseCustomerProduct_Click(object sender, EventArgs e)
        {
            if (this.nccCustomer.EditValue != null)
            {
                CustomerProduct f;
                if (this.customerProductList != null && this.customerProductList.Count > 0)
                    f = new CustomerProduct(this.nccCustomer.EditValue as Model.Customer, this.customerProductList);
                else
                    f = new CustomerProduct(this.nccCustomer.EditValue as Model.Customer);
                f.ShowDialog(this);
                this.customerProductList = f.SelectProduct;
            }
            else
            {
                MessageBox.Show("請先選擇客戶！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

    }

    internal class ProductShipment
    {
        public string ProductName { get; set; }

        public string CustomerProductName { get; set; }

        public List<ShipmentDetail> ShipmentDetail { get; set; }
    }

    internal class ShipmentDetail
    {
        public string Year { get; set; }

        public string Quantity { get; set; }
    }

}