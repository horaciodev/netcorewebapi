using System.Data;

using SampleAPI.Models;

namespace SampleAPI.DataUtils.MappingExtensions
{
    public class ProductCategoryModelDataMapping
    {
        public static ProductCategoryModel GetFromReader(IDataReader reader)
        {
            var prodCatModel = new ProductCategoryModel{
                ProductCategoryId = reader.GetInt32(0),
                CategoryDescr = reader.GetString(1)
            };
            return prodCatModel;
        }
    }
}