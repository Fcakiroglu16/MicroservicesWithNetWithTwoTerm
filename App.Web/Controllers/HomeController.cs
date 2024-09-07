using App.Web.Models;
using App.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Web.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger,
        IConfiguration configuration,
        MicroserviceOneService microserviceOneService)
        : Controller
    {
        static HttpClient client = new HttpClient();
        private readonly ILogger<HomeController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsPost()
        {
            var userName = Request.Form["username"];
            var password = Request.Form["password"];


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


            var requestPasswordResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryResult.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret,
                UserName = userName,
                Password = password
            });


            if (requestPasswordResponse.IsError)
            {
            }


            var accessToken = requestPasswordResponse.AccessToken;
            var refreshToken = requestPasswordResponse.RefreshToken;
            var idToken = requestPasswordResponse.IdentityToken;


            var userInfoResponse = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = discoveryResult.UserInfoEndpoint,
                Token = accessToken
            });


            if (userInfoResponse.IsError)
            {
            }

            var tokenList = new List<AuthenticationToken>()
            {
                new AuthenticationToken()
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = accessToken
                },
                new AuthenticationToken()
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = refreshToken
                },
            };
            AuthenticationProperties properties = new AuthenticationProperties();
            properties.StoreTokens(tokenList);


            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(accessToken);


            var realm_access = jwtSecurityToken.Claims.First(x => x.Type == "realm_access");


            var claims = new List<Claim>();
            if (realm_access is not null)
            {
                var roleAsClaim = JsonSerializer.Deserialize<RoleAsClaim>(realm_access.Value);

                foreach (var role in roleAsClaim.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }


            foreach (var claim in jwtSecurityToken.Claims)
            {
                claims.Add(new Claim(claim.Type, claim.Value));
            }

            foreach (var claim in userInfoResponse.Claims)
            {
                if (claims.Any(x => x.Type == claim.Type)) continue;

                claims.Add(new Claim(claim.Type, claim.Value));
            }


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
                "preferred_username", ClaimTypes.Role);


            var principal = new ClaimsPrincipal(identity);


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> ClientCredentialRequest()
        {
            var exchange = await microserviceOneService.GetExchange();


            return View();
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Secured()
        {
            var AccessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var RefreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            var IdToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);


            var claims = User.Claims;


            var userName = User.FindFirst(x => x.Type == "preferred_username")!.Value;


            var shortUserName = User.Identity!.Name;
            //var userId = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            // kullanıcı id
            // kullanıcı userName

            //var email = User.FindFirst(x =>
            //    x.Type == ClaimTypes.Email);


            //var x = email.Value;
            return View();
        }


        [Authorize(Roles = "adminx")]
        public async Task<IActionResult> SecuredAsAdmin()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index), "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}