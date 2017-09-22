using Microsoft.SqlServer.Server;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

using SampleAPI.DataUtils.Sql;
using SampleAPI.DataUtils.MappingExtensions;
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
            var dataGrunt = new DataGrunt<ProductModel>(_connStr);
            var productList = dataGrunt
                                    .GetObjectListFromProc("uprocGetProductsByCategory",
                                    new List<SqlParameter>(){ new SqlParameter("@ProductCategoryId",categoryId)},
                                    ProductModelDataMapping.GetFromReader
                                    );

            return productList;                                    
        }

        public ProductModel GetProductById(int productId)
        {
            var dataGrunt = new DataGrunt<ProductModel>(_connStr);
            var productList = dataGrunt
                                    .GetObjectListFromProc("uprocGetProductById",
                                    new List<SqlParameter>(){ new SqlParameter("@ProductId",productId)},
                                    ProductModelDataMapping.GetFromReader
                                    );

            if (!productList.Any()) return null;

            return productList[0];
        }

        public IList<ProductModel> GetMultipleByIds(int[] productIds)
        {
            var dataGrunt = new DataGrunt<ProductModel>(_connStr);
            var dataTableParam = CreateSqlTableParam(productIds);
            var sqlDataTableParam = new SqlParameter("@ProductIdsTable",dataTableParam);
            sqlDataTableParam.SqlDbType = SqlDbType.Structured;

            var productList = dataGrunt
                                    .GetObjectListFromProc("uprocGetMultipleProductsById",
                                    new List<SqlParameter>(){ sqlDataTableParam},
                                    ProductModelDataMapping.GetFromReader
                                    );

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