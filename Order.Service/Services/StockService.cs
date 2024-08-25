using System.Net.Http.Json;
using System.Text.Json;
using Polly;

namespace Order.Service.Services
{
    public record CheckStockResponse(bool Status);


    public class StockService(HttpClient client)
    {
        public async Task<bool> CheckStockAsync(int productId, int quantity)
        {
            var fallbackPolicy = Policy<HttpResponseMessage>
                .Handle<Exception>()
                .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(new CheckStockResponse(true)))
                });


            var response =
                await fallbackPolicy.ExecuteAsync(() => client.GetAsync($"api/stocks/{productId}/{quantity}"));


            if (!response.IsSuccessStatusCode)
            {
                // logging
                return false;
            }


            var content = await response.Content.ReadFromJsonAsync<CheckStockResponse>();


            return content!.Status;
        }
    }
}