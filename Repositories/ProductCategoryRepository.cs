using System.Collections.Generic;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using SampleAPI.Models;

namespace SampleAPI.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private string _connStr;
        public ProductCategoryRepository(string connStr)
        {
            _connStr = connStr;
        }

        public IList<ProductCategoryModel> GetProductCategories()
        {
            var productCategoryList = new List<ProductCategoryModel>();

            using(var cn= new SqlConnection(_connStr))
            {
                using(var cmd = new SqlCommand("uprocGetProductCategories", cn) )
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var productCategory = new ProductCategoryModel{
                                ProductCategoryId = reader.GetInt32(0),
                                CategoryDescr = reader.GetString(1)
                            };

                            productCategoryList.Add(productCategory);
                        }
                    }
                }
            }

            return productCategoryList;
        }
    }
}