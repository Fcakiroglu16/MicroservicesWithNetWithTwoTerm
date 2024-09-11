using MassTransit;
using Saga.Order.Consumers;
using Shared.Events.Order;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<OrderStatusMessageConsumer>();

    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost");


        cfg.ReceiveEndpoint("order-microservice.order-status-message.queue",
            e => { e.ConfigureConsumer<OrderStatusMessageConsumer>(context); });
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


app.MapPost("/order/create", async (ISendEndpointProvider sendEndpointProvider) =>
{
    // order created(db)


    // send order created event
    var sendEndpoint =
        await sendEndpointProvider.GetSendEndpoint(new Uri("queue:saga-state-machine.order-created-event.queue"));


    await sendEndpoint.Send(new OrderCreatedEvent($"abc-{Random.Shared.Next(1, 1000)}", "123", "xyz", "ahmet yıldız",
        300, new Dictionary<int, int>()
        {
            { 1, 5 },
            { 10, 3 }
        }));

    return Results.Ok();
});

app.Run();