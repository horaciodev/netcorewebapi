using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SampleAPI.Models;
using SampleAPI.Repositories;

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
        [Route("{id:int}",Name ="GetById")]        
        public IActionResult GetById(int id)
        {
            var product = _proudctRepository.GetProductById(id);

            if(product!=null)
            return new OkObjectResult(product);

            return new NotFoundObjectResult(id);
        }
    }
}