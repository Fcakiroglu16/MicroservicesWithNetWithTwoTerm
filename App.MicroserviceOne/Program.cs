using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Authority = "http://localhost:8080/realms/ACompanyTenant";
    options.Audience = "app.microservice.one";

    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:8080/realms/ACompanyTenant"
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdvancedPolicy", x => { x.RequireClaim("scope", "advanced"); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// [Authorize]
app.MapGet("/exchange", (IHttpContextAccessor httpContextAccessor) =>
    {
        var claim = httpContextAccessor.HttpContext.User.Claims;

        return Results.Ok(new { From = "Usd", To = "Turkish lira", Rate = 1.2m });
    })
    .RequireAuthorization();


app.MapPost("/exchangeAsAdvanced", () => Results.Ok(new { From = "Usd", To = "Turkish lira", Rate = 1.2m }))
    .RequireAuthorization(policyNames: "AdvancedPolicy");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}