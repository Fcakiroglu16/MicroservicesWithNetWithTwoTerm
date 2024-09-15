using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Logging;
using Observability.API;

namespace Observability2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController(ILogger<StockController> logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            logger.LogInformation("Stock Get Methodu Çalıştı");
            var headers = HttpContext.Request.Headers;


            using (var activity = ActivitySourceProvider.ActivitySource.StartActivity("File Operation(write)"))
            {
                System.IO.File.WriteAllText("log.txt", "Hello World");
            }


            return Ok(10);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Stock {id}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Stock Added");
        }
    }
}