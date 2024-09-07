using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace App.MicroserviceOne
{
    internal sealed class CustomClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var scopes = principal.Claims.FirstOrDefault(claim => claim.Type == "scope");

            if (scopes is null) return Task.FromResult(principal);


            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            var scopeAsList = scopes.Value.Split(" ");
            foreach (var scope in scopeAsList)
            {
                claimsIdentity.AddClaim(new Claim("scope", scope));
            }


            principal.AddIdentity(claimsIdentity);


            return Task.FromResult(principal);
        }
    }
}