using Caching;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Application;
using Order.Application.Consumers;
using Order.Application.Products;
using Order.Application.Products.Create;
using Repository;
using Repository.Read;
using Repository.Write;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
builder.Services.AddSingleton<IProductReadRepository, ProductReadRepository>();
builder.Services.AddSingleton<ISyncWriteRepository, SyncWriteRepository>();

builder.Services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("OrderDb"); });
builder.Services.AddMemoryCache();

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<ProductCreatedEventConsumer>();
    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("amqps://dfvzgnhz:sfg6HpUKbJd5eJxYntrPYCm1dfjcnXxG@toad.rmq.cloudamqp.com/dfvzgnhz"));

        //endpoints
        cfg.ReceiveEndpoint("product-created-event",
            e => { e.ConfigureConsumer<ProductCreatedEventConsumer>(context); });
    });
});

builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssemblyContaining<CreateProductCommand>());

//builder.Services.Configure<BusOption>(builder.Configuration.GetSection(nameof(BusOption)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();