using Microsoft.EntityFrameworkCore;
using Observability.API.Models;
using OpenTelemetry.Trace;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenTelemetry().WithTracing(x =>
{
    x.AddSource("Observability.API.ActivitySource");


    x.AddAspNetCoreInstrumentation();
    x.AddEntityFrameworkCoreInstrumentation(x =>
    {
        x.SetDbStatementForText = true;
        x.SetDbStatementForStoredProcedure = true;
    });
    x.AddHttpClientInstrumentation();
    x.AddRedisInstrumentation(x => { x.SetVerboseDatabaseStatements = true; });
    x.AddConsoleExporter();
    x.AddOtlpExporter(); // 4317 via gprc;
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConnectionMultiplexer redisConnectionMultiplexer = await ConnectionMultiplexer.ConnectAsync("localhost");
builder.Services.AddSingleton(redisConnectionMultiplexer);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "SampleInstance";

    options.ConnectionMultiplexerFactory = () => Task.FromResult(redisConnectionMultiplexer);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();