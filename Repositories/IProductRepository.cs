using System.Collections.Generic;
using SampleAPI.Models;

namespace SampleAPI.Repositories
{
    public interface IProductRepository
    {
        IList<ProductModel> GetProductsByCategory(int categoryId);

        IList<ProductModel> GetMultipleByIds(int[] productIds);

        ProductModel GetProductById(int productId);        
    }
}