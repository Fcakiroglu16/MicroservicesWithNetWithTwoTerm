using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Observability.API.Models;

namespace Observability.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(
        AppDbContext context,
        ILogger<ProductsController> logger,
        ILoggerFactory loggerFactory,
        IDistributedCache distributedCache) : ControllerBase
    {
        private static HttpClient client = new HttpClient();


        [HttpGet]
        public IActionResult Get()
        {
            //var orderLogger = loggerFactory.CreateLogger("Order.API.ProductController");

            //orderLogger.LogInformation("Get Metodu çağrıldı(loggerFactory)");


            //logger.LogInformation("Get Methodu çağrıldı(logger)");


            return Ok("Products");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Product {id}");
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            context.Products.Add(new Product() { Name = "kalem 1", Price = 300 });

            await context.SaveChangesAsync();


            await client.GetAsync("https://www.google.com");


            await distributedCache.SetStringAsync("userId", "123");

            return Ok();
            //var orderCode = "123";
            //var userId = "5";

            //logger.LogInformation($"Sipariş oluştu.(OrderCode={orderCode},UserId={userId})");
            //logger.LogInformation("Sipariş oluştu.(OrderCode={0},UserId={1}", orderCode, userId);

            ////ne?
            //logger.LogError("kullanıcı giriş yapamadı");
            ////niçin?
            //logger.LogError("kullanıcı giriş yapamadı. twoFactor flag=false olduğundan dolayı  ");


            //logger.LogInformation("Sipariş oluştu.(OrderCode={orderCode},UserId={userId}", orderCode, userId);


            //// userId= 5
            //var text = string.Format("Merhaba {0}", "Dünya");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok($"Product {id} updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Product {id} deleted");
        }
    }
}