using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Docker2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Id = 1, Name = "kalem 1", Price = 100 });
        }
    }
}