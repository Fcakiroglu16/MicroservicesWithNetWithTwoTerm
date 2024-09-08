using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaStateMachine;
using SagaStateMachine.Models;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


builder.Services.AddMassTransit(configure =>
{
    configure.AddSagaStateMachine<AppSagaStateMachine, SagaEntity>().EntityFrameworkRepository(efcoreConfigure =>
    {
        efcoreConfigure.AddDbContext<DbContext, AppSagaDbContext>((provider, contextbuilder) =>
        {
            contextbuilder.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
        });
    });


    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost");


        cfg.ReceiveEndpoint("saga-state-machine.order-created-event.queue",
            e => { e.ConfigureSaga<SagaEntity>(context); });
    });
});

var host = builder.Build();
host.Run();