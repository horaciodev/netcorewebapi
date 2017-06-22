using System.Collections.Generic;
using System.Linq;
//using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SampleAPI.Controllers
{
    [Route("/api/v1/[controller]")]
    [Authorize(Roles="Clerk")]
    public class IdentityController: ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
        }
    }
}