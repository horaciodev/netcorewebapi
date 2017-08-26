using System.Collections.Generic;
using SampleAPI.Models;

namespace SampleAPI.Repositories
{
    public interface IProductRepository
    {
        IList<ProductModel> GetProductsByCategory(int categoryId);

        ProductModel GetProductById(int productId);        
    }
}