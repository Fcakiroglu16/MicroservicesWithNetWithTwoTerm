using System.Text.Json;

namespace Docker.API.Services
{
    public record ProductResponse(int Id, string Name, decimal Price);

    public class Docker2Service(HttpClient client)
    {
        public async Task<ProductResponse?> GetProduct()
        {
            var response = await client.GetAsync("/api/products");


            return await response.Content.ReadFromJsonAsync<ProductResponse>();
        }
    }
}