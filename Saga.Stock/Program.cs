using MassTransit;
using Saga.Stock.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<StockReserveStartMessageConsumer>();

    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost");

        cfg.ReceiveEndpoint("stock-microservice.stock-reserve-start-message.queue",
            e => { e.ConfigureConsumer<StockReserveStartMessageConsumer>(context); });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();