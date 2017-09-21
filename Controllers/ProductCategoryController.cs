using System;
using SampleAPI.Repositories;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SampleAPI.Controllers
{

    [Route("/api/v1/[controller]")]
    public class ProductCategoryController: ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepo;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepo)
        {
            _productCategoryRepo = productCategoryRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
            var productCategories = _productCategoryRepo.GetProductCategories();
            
            return new OkObjectResult(productCategories);
            }
            catch(Exception e)
            {                
                return new ContentResult(){ Content = e.Message ?? "Naughty" , StatusCode = 500 };
            }
        }
    }
}