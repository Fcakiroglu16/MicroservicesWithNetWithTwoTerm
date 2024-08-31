using Docker.API.Models;
using Docker.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Docker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(Docker2Service dockerService, AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await context.Products.ToListAsync());
        }


        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var product = await dockerService.GetProduct();
        //    if (product is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(product);
        //}
    }
}