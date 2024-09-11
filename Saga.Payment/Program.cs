using MassTransit;
using Saga.Payment.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<PaymentStartMessageConsumer>();

    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost");

        cfg.ReceiveEndpoint("payment-microservice.payment-start-message.queue",
            e => { e.ConfigureConsumer<PaymentStartMessageConsumer>(context); });
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