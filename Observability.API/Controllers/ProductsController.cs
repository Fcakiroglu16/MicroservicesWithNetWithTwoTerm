using System.Diagnostics;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Observability.API.Models;
using Observability.API.Services;
using Shared.Bus;

namespace Observability.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(
        AppDbContext context,
        ILogger<ProductsController> logger,
        ILoggerFactory loggerFactory,
        IDistributedCache distributedCache,
        IPublishEndpoint publishEndpoint,
        StockService stockService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await publishEndpoint.Publish(
                new ProductAddedEvent(1, "kalem 1", 100, Activity.Current!.TraceId.ToString()));

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
            logger.LogInformation("Produts >Post Methodu başladı");

            context.Products.Add(new Product() { Name = "kalem 1", Price = 300 });

            await context.SaveChangesAsync();


            await distributedCache.SetStringAsync("userId", "123");

            using (var activity = ActivitySourceProvider.ActivitySource.StartActivity("File(app.txt)"))
            {
                activity!.SetTag("userId", "123");
                await System.IO.File.WriteAllTextAsync("app.txt", "Merhaba Dünya");
            }

            await publishEndpoint.Publish(
                new ProductAddedEvent(1, "kalem 1", 100, Activity.Current!.TraceId.ToString()));


            var result = await stockService.GetStock();
            logger.LogInformation("Produts >Post Methodu bitti");

            logger.LogInformation("Sipariş oluştu(OrderCode={orderCode})(UserId={userId})", "abc", "123");

            return Ok(result);


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