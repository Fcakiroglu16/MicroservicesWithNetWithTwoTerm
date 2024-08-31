using Docker.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(Docker2Service dockerService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var product = await dockerService.GetProduct();
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}