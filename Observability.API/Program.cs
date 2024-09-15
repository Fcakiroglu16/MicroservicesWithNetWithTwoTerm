using MassTransit;
using MassTransit.Logging;
using Microsoft.EntityFrameworkCore;
using Observability.API;
using Observability.API.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;
using System.Diagnostics;
using Observability.API.Services;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);


//massransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});


builder.Services.AddOpenTelemetry().WithTracing(x =>
{
    x.AddSource("Observability.API.ActivitySource");
    x.AddSource(DiagnosticHeaders.DefaultListenerName);


    x.ConfigureResource(rb => rb.AddService("Observability.API", serviceVersion: "1.0"));


    x.AddAspNetCoreInstrumentation(x =>
    {
        x.Filter = (httpContext) => httpContext.Request.Path.Value!.Contains("api");
    });
    x.AddEntityFrameworkCoreInstrumentation(x =>
    {
        x.SetDbStatementForText = true;
        x.SetDbStatementForStoredProcedure = true;
    });
    x.AddHttpClientInstrumentation();
    x.AddRedisInstrumentation(x => { x.SetVerboseDatabaseStatements = true; });


    x.AddConsoleExporter();
    x.AddOtlpExporter(); // 4317 via gprc;
}).WithLogging(logging =>
{
    logging.AddOtlpExporter();
    logging.ConfigureResource(rb => rb.AddService("Observability.API", serviceVersion: "1.0"));
}).WithMetrics(metrics =>
{
    metrics.AddProcessInstrumentation();
    metrics.AddRuntimeInstrumentation();

    metrics.ConfigureResource(rb => rb.AddService("Observability.API", serviceVersion: "1.0"));
    metrics.AddOtlpExporter();
});


builder.Services.AddHttpClient<StockService>(x => { x.BaseAddress = new Uri("https://localhost:7001"); });


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