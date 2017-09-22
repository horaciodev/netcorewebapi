using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SampleAPI.Models;
using SampleAPI.Repositories;
using SampleAPI.Utils;

namespace SampleAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [Authorize(Roles="Clerk, Poseidon")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _proudctRepository;
        public ProductController(IProductRepository productRepo)
        {
            _proudctRepository = productRepo;
        }

        [HttpGet]
        [Route("category/{id:int}",Name ="GetByCategory")]
        public IActionResult Get(int id)
        {
            var prodList = _proudctRepository.GetProductsByCategory(id);

            return new OkObjectResult(prodList);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int}",Name ="GetById")]        
        public IActionResult GetById(int id)
        {
            var product = _proudctRepository.GetProductById(id);

            if(product!=null)
                return new OkObjectResult(product);

            return new NotFoundObjectResult(id);
        }

        [HttpGet]
        [Route("multi/{ids}", Name="GetMultipleById")]
        public IActionResult GetMultipleById(string ids)
        {            
            var prodIds = ValidationExtensions.ParseProductIdsFromString(ids);

            if(prodIds == null)
                return new BadRequestResult();

            var products = _proudctRepository.GetMultipleByIds(prodIds);

            if(products != null)
                return new OkObjectResult(products);

            return new NotFoundObjectResult(ids);

        }
    }
}