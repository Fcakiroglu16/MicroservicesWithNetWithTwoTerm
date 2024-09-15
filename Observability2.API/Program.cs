using MassTransit;
using MassTransit.Logging;
using Observability2.API.Consumers;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductAddedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("observability2-product.created.event-queue",
            e => { e.ConfigureConsumer<ProductAddedEventConsumer>(context); });
    });
});
builder.Services.AddOpenTelemetry().WithTracing(tracing =>
{
    tracing.AddSource(DiagnosticHeaders.DefaultListenerName);
    tracing.AddSource("Observability2.API.ActivitySource");
    tracing.ConfigureResource(rb => rb.AddService("Observability2.API", serviceVersion: "1.0"));
    tracing.AddOtlpExporter();
    tracing.AddAspNetCoreInstrumentation();
}).WithLogging(logging =>
{
    logging.AddOtlpExporter();
    logging.ConfigureResource(rb => rb.AddService("Observability2.API", serviceVersion: "1.0"));
}).WithMetrics(metrics =>
{
    metrics.AddProcessInstrumentation();
    metrics.AddRuntimeInstrumentation();

    metrics.ConfigureResource(rb => rb.AddService("Observability2.API", serviceVersion: "1.0"));
    metrics.AddOtlpExporter();
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