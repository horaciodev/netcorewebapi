using Microsoft.SqlServer.Server;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using SampleAPI.Models;

namespace SampleAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private string _connStr;

        public ProductRepository(string connStr)
        {
            _connStr = connStr;
        }

        public IList<ProductModel> GetProductsByCategory(int categoryId)
        {
            var productList = new List<ProductModel>();

            using(var cn = new SqlConnection(_connStr))
            {
                using(var cmd = new SqlCommand("uprocGetProductsByCategory", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductCategoryId", categoryId);

                    cn.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var product = new ProductModel{
                                ProductId = reader.GetInt32(0),
                                ProductCategoryId = reader.GetInt32(1),
                                CategoryDescr = reader.GetString(2),
                                IsShippable = reader.GetBoolean(3),
                                IsVisible = reader.GetBoolean(4),
                                Price = reader.GetDecimal(5),
                                ProductName = reader.GetString(6),
                                ProductDescr = reader[7] == System.DBNull.Value ? string.Empty : reader.GetString(7)
                            };

                            productList.Add(product);
                        }
                    }
                }
            }

            return productList;
        }

        public ProductModel GetProductById(int productId)
        {
            ProductModel retProduct = null;
            using(var cn = new SqlConnection(_connStr))
            {
                using(var cmd = new SqlCommand("uprocGetProductById", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    cn.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            retProduct = new ProductModel{
                                ProductId = reader.GetInt32(0),
                                ProductCategoryId = reader.GetInt32(1),
                                CategoryDescr = reader.GetString(2),
                                IsShippable = reader.GetBoolean(3),
                                IsVisible = reader.GetBoolean(4),
                                Price = reader.GetDecimal(5),
                                ProductName = reader.GetString(6),
                                ProductDescr = reader[7] == System.DBNull.Value ? string.Empty : reader.GetString(7)
                            };

                        }
                    }
                }
            }

            return retProduct;
        }

        public IList<ProductModel> GetMultipleByIds(int[] productIds)
        {
            var productList = new List<ProductModel>();

            using(var cn = new SqlConnection(_connStr))
            {
                using(var cmd = new SqlCommand("uprocGetMultipleProductsById", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dataTableParam = CreateSqlTableParam(productIds);
                    var tableSqlParam = cmd.Parameters.AddWithValue("@ProductIdsTable", dataTableParam);
                    tableSqlParam.SqlDbType = SqlDbType.Structured;

                    cn.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var product = new ProductModel{
                                ProductId = reader.GetInt32(0),
                                ProductCategoryId = reader.GetInt32(1),
                                CategoryDescr = reader.GetString(2),
                                IsShippable = reader.GetBoolean(3),
                                IsVisible = reader.GetBoolean(4),
                                Price = reader.GetDecimal(5),
                                ProductName = reader.GetString(6),
                                ProductDescr = reader[7] == System.DBNull.Value ? string.Empty : reader.GetString(7)
                            };

                            productList.Add(product);
                        }
                    }
                }
            }

             return productList;
        }

        private static List<SqlDataRecord> CreateSqlTableParam(int[] ids)
        {
            var sqlTableParam = new List<SqlDataRecord>();
            SqlMetaData intRowKey = new SqlMetaData("RowKey", SqlDbType.Int);
            for(int i=0;i<ids.Length;i++)
            {
                var paramRecord = new SqlDataRecord(new []{intRowKey});
                paramRecord.SetInt32(0,ids[i]); 
                sqlTableParam.Add(paramRecord);       
            }

            return sqlTableParam;
        }
    }
    
}