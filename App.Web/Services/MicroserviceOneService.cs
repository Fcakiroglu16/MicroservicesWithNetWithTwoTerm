using System.Net.Http.Headers;
using IdentityModel.Client;

namespace App.Web.Services
{
    public class MicroserviceOneService(HttpClient client, IConfiguration configuration)
    {
        public async Task<ExchangeResponse> GetExchange()
        {
            var clientId = configuration.GetSection("Client")["Id"]!;
            var clientSecret = configuration.GetSection("Client")["Secret"]!;
            var authority = configuration.GetSection("Client")["Authority"]!;


            var discoveryResult =
                await client.GetDiscoveryDocumentAsync($"{authority}/.well-known/openid-configuration");

            if (discoveryResult.IsError)
            {
                // logging
                // return model
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResult.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret
            });


            if (tokenResponse.IsError)
            {
                // logging
                // return model
            }


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            client.SetBearerToken(tokenResponse.AccessToken!);


            var response = await client.GetAsync("/exchange");


            if (response.IsSuccessStatusCode)
            {
            }

            var responseContent = await response.Content.ReadFromJsonAsync<ExchangeResponse>();


            return responseContent;
        }
    }
}