//------------------------------------------------------------------------------
//
// file name:ProductAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Book.DA.SQLServer.SQLDB;
namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of Product
    /// </summary>
    public partial class ProductAccessor : EntityAccessor, IProductAccessor
    {
        #region IProductAccessor 成员
        private const string SQL_SELECT_IdName = "SELECT ProductId,Id ,ProductName,productDescription FROM product";
        public IList<Book.Model.Product> SelectProduct()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_product", null);
        }

        public IList<Model.Product> SelectByProductIds(string productIds)
        {
            productIds = " productId in (" + productIds + ")";
            return sqlmapper.QueryForList<Model.Product>("Product.select_WhereSQL", productIds);
        }

        public System.Data.DataTable SelectDataTable()
        {
            string sql = "select * from product";
            System.Data.SqlClient.SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;

        }
        public void UpdateBeginCost(DataTable dt)
        {
            string sql = "UPDATE Product SET ProductId = @ProductId,ProductCategoryId = @ProductCategoryId,ProductName = @ProductName,ProductBaseUnit = @ProductBaseUnit,ProductBarCode = @ProductBarCode,ProductSpecification = @ProductSpecification,ProductModel = @ProductModel,ProductPriceA = @ProductPriceA,ProductPriceB = @ProductPriceB,ProductPriceC = @ProductPriceC,ProductRetailPrice = @ProductRetailPrice,ProductBeginCost = @ProductBeginCost,ProductStandardCost = @ProductStandardCost,ProductDescription = @ProductDescription WHERE ProductId=@ProductId";
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.UpdateCommand = new SqlCommand(sql, conn);
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.VarChar, 50, "ProductId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductCategoryId", SqlDbType.VarChar, 50, "ProductCategoryId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 50, "ProductName"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductBaseUnit", SqlDbType.VarChar, 50, "ProductBaseUnit"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductBarCode", SqlDbType.VarChar, 50, "ProductBarCode"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductSpecification", SqlDbType.VarChar, 50, "ProductSpecification"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductModel", SqlDbType.VarChar, 50, "ProductModel"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceA", SqlDbType.Money, 8, "ProductPriceA"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceB", SqlDbType.Money, 8, "ProductPriceB"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceC", SqlDbType.Money, 8, "ProductPriceC"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductRetailPrice", SqlDbType.Money, 8, "ProductRetailPrice"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductBeginCost", SqlDbType.Money, 8, "ProductBeginCost"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductStandardCost", SqlDbType.Money, 8, "ProductStandardCost"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductDescription", SqlDbType.Text, 16, "ProductDescription"));
            dataAdapter.Update(dt);
        }

        public void UpdateCost1(Book.Model.Product product, decimal? price, double? quantity)
        {
            Hashtable table = new Hashtable();
            table.Add("quantity", quantity == null ? 0 : quantity.Value);
            table.Add("unitprice", price == null ? decimal.Zero : price.Value);
            table.Add("productid", product.ProductId);

            sqlmapper.Update("Product.update_cost1", table);
        }

        public IList<Book.Model.Product> Select(Book.Model.ProductCategory Category)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_byCategory", Category.ProductCategoryId);
        }
        public IList<Book.Model.Product> SelectProductByProductCategoryId(Book.Model.ProductCategory Category)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_byCategoryTo", Category.ProductCategoryId);
        }
        public IList<Book.Model.Product> Select(Book.Model.Depot depot)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_byDepot", depot.DepotId);
        }

        public bool ExistsNameInsert(string productName)
        {
            return sqlmapper.QueryForObject<bool>("Product.ExistsNameInsert", productName);
        }

        public bool ExistsNameUpdate(Model.Product product)
        {
            Hashtable pars = new Hashtable();
            pars.Add("newName", product.ProductName);
            pars.Add("oldName", Get(product.ProductId).ProductName);
            string sql = string.Empty;
            if (product.IsCustomerProduct == null || !product.IsCustomerProduct.Value)
                sql = " and (IsCustomerProduct=0 or IsCustomerProduct is null) ";
            pars.Add("sql", sql);
            return sqlmapper.QueryForObject<bool>("Product.ExistsNameUpdate", pars);
        }
        //查询指定客户的货品和公司货品
        public IList<Model.Product> Select(Model.Customer customer)
        {

            return sqlmapper.QueryForList<Model.Product>("Product.SelectByCustomer", customer == null ? null : customer.CustomerId);
        }
        //查询指定类和客户的货品和公司货品
        public IList<Model.Product> Select(Model.Customer customer, Model.ProductCategory cate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("customerid", customer == null ? "" : customer.CustomerId);
            ht.Add("productcategoryid", cate == null ? "" : cate.ProductCategoryId);
            return sqlmapper.QueryForList<Model.Product>("Product.SelectByCategoryAndCustomer", ht);
        }

        //  //查询指定客户的货品
        public IList<Model.Product> SelectProductByCustomer(Model.Customer customer)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CustomerId", customer.CustomerId);
            ht.Add("DeadDate", DateTime.Now.Date.AddDays(1).AddSeconds(-1));
            return sqlmapper.QueryForList<Model.Product>("Product.SelectProductByCustomer", ht);
        }
        public IList<Model.Product> SelectAllProductByCustomers(string customerIds, bool isShowUnuseProduct)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CustomerIds", customerIds);
            if (!isShowUnuseProduct)
                ht.Add("sql", " and ProductId in (select CustomerProductProceName from CustomerProducts where (VersionDate IS NULL OR (year(VersionDate) = '1900' AND month(VersionDate) = '01' AND day(VersionDate) = '01') OR VersionDate > GETDATE()) and CustomerId in (" + customerIds + "))");

            return sqlmapper.QueryForList<Model.Product>("Product.SelectAllProductByCustomers", ht);
        }

        public void Delete(Book.Model.Product product, Model.Customer customer)
        {
            Hashtable table = new Hashtable();
            table.Add("productid", product == null ? null : product.ProductId);
            table.Add("customerid", customer == null ? null : customer.CustomerId);
            sqlmapper.Delete("Product.deleteByCustomPro", table);
        }
        //查询指定类和客户的货品和公司货品
        public Model.Product Get(Model.Customer customer, Model.Product product)
        {
            Hashtable ht = new Hashtable();
            ht.Add("customerid", customer == null ? "" : customer.CustomerId);
            ht.Add("productid", product == null ? "" : product.ProductId);
            return sqlmapper.QueryForObject<Model.Product>("Product.selectByProductAndCustomer", ht);
        }

        public IList<Model.Product> GetProduct()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.GetProduct", null);
        }

        public IList<Model.Product> GetProductByCondition(string ProductCategoryName, string pt)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProductCategoryName", ProductCategoryName);
            ht.Add("pt", pt);
            return sqlmapper.QueryForList<Model.Product>("Product.GetProductByCondition", ht);
        }

        public IList<Model.Product> SelectNotCustomer()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_notcustomer", null);
        }
        public IList<Model.Product> SelectNotCustomer1()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_notcustomer1", null);
        }
        public IList<Model.Product> SelectNotCustomerByCate(string productCate)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_notcustomerByCate", productCate);
        }
        public IList<Model.Product> SelectByIdOrNameKey(string id, string productName, string customerProductName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", id);
            ht.Add("name", productName);
            ht.Add("customerProductName", customerProductName);
            return sqlmapper.QueryForList<Model.Product>("Product.select_byIdOrNameKey", ht);
        }
        public IList<Model.Product> SelectALLIdOrNameKey(string id, string productName, string customerProductName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", id);
            ht.Add("name", productName);
            ht.Add("customerProductName", customerProductName);
            return sqlmapper.QueryForList<Model.Product>("Product.select_ALLIdOrNameKey", ht);
        }
        public IList<Model.Product> GetProductReader()
        {
            IList<Model.Product> productList = new List<Model.Product>();
            using (SqlDataReader rdr = SQLDB.SqlHelper.ExecuteReader(SQLDB.SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, SQL_SELECT_IdName, null))
            {
                Model.Product temp = new Model.Product();
                //temp.Id = rdr["id"].ToString();

                temp.ProductName = rdr[Model.Product.PRO_ProductName].ToString();
                temp.ProductId = rdr[Model.Product.PRO_ProductId].ToString();
                while (rdr.Read())
                {
                    Model.Product product = new Model.Product(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3));
                    productList.Add(product);
                }

            }
            return productList;
        }
        //根据组装前半成品 查询 裸片加工后商品
        public IList<Model.Product> SelectProceProduct(Model.Product product)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProceebeforeProductId", product.ProceebeforeProductId);
            ht.Add("productId", product.ProductId);

            return sqlmapper.QueryForList<Model.Product>("Product.select_ProceProduct", ht);
        }
        //根据裸片 或加工商品  查询 裸片及加工 等相关商品
        public IList<Model.Product> SelectProceByProduct(Model.Product product)
        {
            string sql = "";
            if (product.IsProcee == true)
                sql = "productId='" + product.ProceebeforeProductId + "' or ProceebeforeProductId ='" + product.ProceebeforeProductId + "'";
            else
                sql = "productId='" + product.ProductId + "' or ProceebeforeProductId ='" + product.ProductId + "'";
            return this.SelectByWhereSQL(sql);
        }
        public IList<Model.Product> SelectByWhereSQL(string sql)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_WhereSQL", sql);
        }
        public double? getStockByProduct(string productid)
        {
            return sqlmapper.QueryForObject<double>("Product.select_StockByProduct", productid);
        }
        public Model.Product getStockYFPByProduct(string productid)
        {
            DataSet ds = DbHelperSQL.Query("select (ISNULL(ProduceMaterialDistributioned,0)+isnull( OtherMaterialDistributioned,0)) AS ProduceMaterialDistributioned,ISNULL(StocksQuantity,0) as  StocksQuantity  from product where productid='" + productid + "'");
            Model.Product product = new Book.Model.Product();
            product.StocksQuantity = double.Parse(ds.Tables[0].Rows[0][1].ToString());
            product.ProduceMaterialDistributioned = double.Parse(ds.Tables[0].Rows[0][0].ToString());
            return product;

        }

        public IList<Model.Product> StockPrompt()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.StockPrompt", null);
        }

        public void UpdateSimple(Model.Product product)
        {
            sqlmapper.Update("Product.Update_SimpleProduct", product);
        }

        public Model.Product SelectByIdAndName(string id, string productName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", id);
            ht.Add("productName", productName);
            return sqlmapper.QueryForObject<Model.Product>("Product.SelectByIdAndName", ht);
        }

        public IList<Model.Product> SelectByInvoiceCusID(string id)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.SelectByInvoiceCusID", id);
        }

        public IList<Model.Product> GetProductBaseInfo()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.GetProductBaseInfo", null);
        }

        public double SelectStocksQuantityByStock(string productId)
        {
            return sqlmapper.QueryForObject<double>("Product.SelectStocksQuantityByStock", productId);
        }
        #endregion


        #region /*CdmiN--2011年9月29日16:05:38*/ 更新product表,使其与stock表中数据对应
        public void UpdateProduct_Stock(Book.Model.Product pro)
        {
            sqlmapper.Update("Product.update_stock", pro.ProductId);
        }
        #endregion

        public string SelectCustomerProductNameByProductIds(string productIds)
        {
            string sql = "select CustomerProductName+',' from Product where ProductId in (" + productIds + ") for xml path('')";
            object value = this.QueryObject(sql, 60);

            return (value == null ? "" : value.ToString());
        }


        public IList<Model.Product> SelectQtyAndCost(string startCategory_Id, string endCategory_Id, string depotId)
        {
            string sql = "";
            if (!string.IsNullOrEmpty(startCategory_Id) || !string.IsNullOrEmpty(endCategory_Id))
            {
                if (!string.IsNullOrEmpty(startCategory_Id) && !string.IsNullOrEmpty(endCategory_Id))
                    sql = " and ProductCategoryId in (select ProductCategoryId from ProductCategory where Id between '" + startCategory_Id + "' and '" + endCategory_Id + "')";
                else
                    sql = " and ProductCategoryId in (select ProductCategoryId from ProductCategory where Id = '" + (string.IsNullOrEmpty(startCategory_Id) ? endCategory_Id : startCategory_Id) + "')";
            }
            Hashtable ht = new Hashtable();
            ht.Add("sql", sql);

            //若仓库为空，直接查商品库存，否则查对应仓库的库存
            if (string.IsNullOrEmpty(depotId))
                return sqlmapper.QueryForList<Model.Product>("Product.SelectQtyAndCost", ht);
            else
            {
                ht.Add("DepotId", depotId);
                return sqlmapper.QueryForList<Model.Product>("Product.SelectQtyAndCostByDepot", ht);
            }
        }


        //按月份查询所有商品平均采购单价
        public IList<Model.ProductCost> SelectCGPriceByMonth()
        {
            string sql = "select * from (select Avg(ISNULL(cgd.InvoiceCGDetailPrice,0)) as Price,Cast((CONVERT(varchar(7),cg.InvoiceDate,120)+'-01') as datetime) as InvoiceDate,cgd.ProductId from InvoiceCGDetail cgd left join InvoiceCG cg on cgd.InvoiceId=cg.InvoiceId where cgd.InvoiceCGDetailPrice<>0 group by Cast((CONVERT(varchar(7),cg.InvoiceDate,120)+'-01') as datetime),cgd.ProductId) a order by a.InvoiceDate desc ";

            return DataReaderBind<Model.ProductCost>(sql, null, CommandType.Text);
        }

        //按月份查询所有商品平均委外入库单价
        public IList<Model.ProductCost> SelectOtherInDepotPriceByMonth()
        {
            string sql = "select * from (select AVG(ISNULL(poid.ProcessPrice,0)) as Price,Cast((CONVERT(varchar(7),poi.ProduceOtherInDepotDate,120)+'-01') as datetime) as InvoiceDate ,poid.ProductId from ProduceOtherInDepotDetail poid left join ProduceOtherInDepot poi on poid.ProduceOtherInDepotId=poi.ProduceOtherInDepotId where poid.ProcessPrice<>0 group by Cast((CONVERT(varchar(7),poi.ProduceOtherInDepotDate,120)+'-01') as datetime),poid.ProductId) a order by a.InvoiceDate desc ";

            return DataReaderBind<Model.ProductCost>(sql, null, CommandType.Text);
        }

        //获取所有商品的参考成本
        public IList<Model.Product> GetAllProductReferenceCost()
        {
            //查询“参考成本”=0 的所有商品，然后去“客户商品价格”和“厂商商品价格”中查询其对应的 销售/采购 价格，若销售/采购同时存在，以 采购 价格为准当做参考成本 ；一个商品可能对不同的客户/厂商有不同的价格，所以查询结果中，一个商品可能会对应多个价格，后续计算中以日期最近的价格为准
            string sql = "select a.ProductId,a.ProductName,a.PriceAndRange,a.InsertTime,COUNT(*) from (select ReferenceCost,p.ProductId,p.ProductName,cpp.CustomerProductPriceRage,sp.SupplierProductPriceRange,ISNULL(sp.SupplierProductPriceRange,cpp.CustomerProductPriceRage) as PriceAndRange,ISNULL(sp.InsertTime,cpp.InsertTime) as InsertTime from Product p left join CustomerProductPrice cpp on p.ProductId=cpp.ProductId left join SupplierProduct sp on p.ProductId=sp.ProductId where (p. ReferenceCost =0 or p.ReferenceCost is null) and( cpp.CustomerProductPriceRage is not null or sp.SupplierProductPriceRange is not null) ) a group by a.ProductId,a.ProductName,a.PriceAndRange,a.InsertTime ";

            return DataReaderBind<Model.Product>(sql, null, CommandType.Text);
        }

        //批量更新商品的 参考成本
        public void BatchUpdateReferenceCost(DataTable dt)
        {
            using (SqlConnection connection = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("", connection))
                {
                    //创建临时表，因为SqlBulkCopy只能批量插入，所以先批量插入到临时表中，再从临时表中更新到商品表，然后删除临时表
                    //由于临时表关闭连接自动删除，所以保持在一个链接内
                    string createTempTableSql = "Create Table #ProHelper (ProductId varchar(50),ReferenceCost money)";

                    try
                    {
                        connection.Open();
                        cmd.CommandText = createTempTableSql;
                        int row = cmd.ExecuteNonQuery();

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.BatchSize = 100;
                            bulkCopy.BulkCopyTimeout = 300;

                            bulkCopy.DestinationTableName = "#ProHelper";
                            bulkCopy.ColumnMappings.Add("ProductId", "ProductId");
                            bulkCopy.ColumnMappings.Add("ReferenceCost", "ReferenceCost");

                            bulkCopy.WriteToServer(dt);
                        }

                        //从临时表中更新到商品表
                        string updateSql = "update Product set ReferenceCost=p.ReferenceCost  from #ProHelper p  where Product.ProductId=p.ProductId  and p.ReferenceCost>0";
                        cmd.CommandText = updateSql;
                        row = cmd.ExecuteNonQuery();

                        //删除临时表
                        string dropTempTableSql = "drop table #ProHelper";
                        cmd.CommandText = dropTempTableSql;
                        row = cmd.ExecuteNonQuery();

                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }

        }
    }
}
