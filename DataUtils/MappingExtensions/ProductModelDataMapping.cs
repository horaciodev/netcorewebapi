using System.Data;

using SampleAPI.Models;

namespace SampleAPI.DataUtils.MappingExtensions
{
    public class ProductModelDataMapping
    {
        public static ProductModel GetFromReader(IDataReader reader)
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
            return product;
        }
    }

}