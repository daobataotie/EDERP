using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Settings.BasicData.Products
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：List1Form
   // 编 码 人: 茍波濤                   完成时间:2009-10-30
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class List1Form : BaseListForm
    {
        DataTable dt = new DataTable();
        BL.CustomerProductPriceManager cpp = new Book.BL.CustomerProductPriceManager();
        BL.SupplierProductManager spp = new Book.BL.SupplierProductManager();

        public List1Form()
        {

            InitializeComponent();
            // this.barManager1.Bars[0].Visible = false ;

            this.manager = new BL.ProductManager();
        }
        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string id = dt.Rows[this.bindingSource1.IndexOf(this.bindingSource1.Current)][0].ToString();
            EditForm._product = ((BL.ProductManager)this.manager).Get(id);
            //this.DialogResult = DialogResult.OK;
            EditForm f = new EditForm(EditForm._product, "view");
            f.ShowDialog(this);
        }

        private void List1Form_Load(object sender, EventArgs e)
        {


        }
        protected override void RefreshData()
        {
            //为了查询速度，暂将ProductSpecification，ProductDescription去掉
            this.bindingSource1.DataSource = this.dt = ((BL.ProductManager)this.manager).Query("SELECT product.ProductId,product.Id,ProductName,ProductCategoryName,c.CustomerFullName ,CustomerProductName,isnull(StocksQuantity,0) StocksQuantity,ProductVersion,'' as Price FROM Product left join ProductCategory ca  on ca.ProductCategoryId=Product.ProductCategoryId left join Customer c on c.CustomerId=Product.CustomerId order by  ProductName", 300, "pro").Tables[0];

            IList<Model.CustomerProductPrice> cppList = cpp.SelectAll();
            IList<Model.SupplierProduct> sppList = spp.SelectAll();

            #region 方案一
            foreach (DataRow item in this.dt.Rows)
            {
                string productId = item["ProductId"].ToString();
                string PriceRange = string.Empty;

                Model.CustomerProductPrice cPrice = cppList.FirstOrDefault(C => C.ProductId == productId);
                if (cPrice != null)
                {
                    PriceRange = cPrice.CustomerProductPriceRage;
                    cppList.Remove(cPrice);
                }
                else
                {
                    Model.SupplierProduct sPrice = sppList.FirstOrDefault(S => S.ProductId == productId);
                    if (sPrice != null)
                    {
                        PriceRange = sPrice.SupplierProductPriceRange;
                        sppList.Remove(sPrice);
                    }
                }
                string[] PriAndRange = string.IsNullOrEmpty(PriceRange) ? null : PriceRange.Split(',');

                if (PriAndRange != null)
                {
                    item["Price"] = (string.IsNullOrEmpty(PriAndRange[0].Split('/')[2]) ? null : PriAndRange[0].Split('/')[2]);
                }
            }
            #endregion

            #region 方案二 更慢
            //EnumerableRowCollection<DataRow> dtListAll = dt.AsEnumerable();
            //EnumerableRowCollection<DataRow> dtListCustomer = dtListAll.Where(D => cppList.Any(C => C.ProductId == D.Field<string>("ProductId")));
            //EnumerableRowCollection<DataRow> dtListSuppiler = dtListAll.Where(D => sppList.Any(C => C.ProductId == D.Field<string>("ProductId")));
            //foreach (DataRow item in dtListCustomer)
            //{
            //    string productId = item["ProductId"].ToString();
            //    string PriceRange = string.Empty;

            //    Model.CustomerProductPrice cPrice = cppList.First(C => C.ProductId == productId);
            //    PriceRange = cPrice.CustomerProductPriceRage;
            //    cppList.Remove(cPrice);

            //    string[] PriAndRange = string.IsNullOrEmpty(PriceRange) ? null : PriceRange.Split(',');

            //    if (PriAndRange != null)
            //    {
            //        item["Price"] = (string.IsNullOrEmpty(PriAndRange[0].Split('/')[2]) ? null : PriAndRange[0].Split('/')[2]);
            //    }
            //}
            //foreach (DataRow item in dtListSuppiler)
            //{
            //    string productId = item["ProductId"].ToString();
            //    string PriceRange = string.Empty;

            //    Model.SupplierProduct sPrice = sppList.First(S => S.ProductId == productId);
            //    PriceRange = sPrice.SupplierProductPriceRange;
            //    sppList.Remove(sPrice);

            //    string[] PriAndRange = string.IsNullOrEmpty(PriceRange) ? null : PriceRange.Split(',');

            //    if (PriAndRange != null)
            //    {
            //        item["Price"] = (string.IsNullOrEmpty(PriAndRange[0].Split('/')[2]) ? null : PriAndRange[0].Split('/')[2]);
            //    }
            //} 
            #endregion
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ROList1FormReport ro = new ROList1FormReport(this.dt);
            ro.ShowPreviewDialog();
        }
        //protected override BaseEditForm GetEditForm()
        //{
        //    return new EditForm(this.productCate);
        //}

        //protected override BaseEditForm GetEditForm(object[] args)
        //{
        //    Type type = typeof(EditForm);
        //    return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        //}
    }
}