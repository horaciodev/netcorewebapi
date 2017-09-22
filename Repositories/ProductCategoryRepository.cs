using System.Collections.Generic;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using SampleAPI.Models;
using SampleAPI.DataUtils.Sql;
using SampleAPI.DataUtils.MappingExtensions;

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
            var dataGrunt = new DataGrunt<ProductCategoryModel>(_connStr);
            var productCategoryList = dataGrunt
                                    .GetObjectListFromProc("uprocGetProductCategories", 
                                                            null,
                                                            ProductCategoryModelDataMapping.GetFromReader);

            return productCategoryList;                                                           
        }
    }
}