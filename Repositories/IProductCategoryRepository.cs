using System.Collections.Generic;
using SampleAPI.Models;

namespace SampleAPI.Repositories
{
    public interface IProductCategoryRepository
    {
        IList<ProductCategoryModel> GetProductCategories();
    }
}