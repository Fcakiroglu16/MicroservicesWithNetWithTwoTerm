using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceBus;

namespace Stock.Service.Consumers
{
    public class OrderCreatedEventConsumerBackgroundService(IBus bus) : BackgroundService
    {
        private IModel? Channel { get; set; }

        // hook methods
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Channel = bus.GetChannel();

            //create queue
            Channel.QueueDeclare(queue: BusConst.StockOrderCreatedEventQueue,
                durable: true,
                exclusive: true,
                autoDelete: false,
                arguments: null);


            Channel.QueueBind(BusConst.StockOrderCreatedEventQueue, BusConst.OrderCreatedEventExchange, "", null);

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(Channel);


            Channel.BasicConsume(BusConst.StockOrderCreatedEventQueue, false, consumer);

            Channel!.CallbackException += Channel_CallbackException;


            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageAsJson = Encoding.UTF8.GetString(body);
                var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(messageAsJson);


                Console.WriteLine($"Gelen Event:{orderCreatedEvent.OrderId}");

                // process order
                // update stock
                // send payment

                // payment
                //


                Channel!.BasicAck(ea.DeliveryTag, false);
            };

            return Task.CompletedTask;
        }

        private void Channel_CallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}