namespace Observability.API.Services
{
    public class StockService(HttpClient client)
    {
        public async Task<string> GetStock()
        {
            var response = await client.GetAsync("/api/Stock");

            return await response.Content.ReadAsStringAsync();
        }
    }
}