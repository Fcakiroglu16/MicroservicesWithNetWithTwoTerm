using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using ServiceBus;

namespace Order.Service
{
    public class OrderService(ServiceBus.IBus bus, IPublishEndpoint publishEndpoint) : IOrderService
    {
        public async Task Create()
        {
            // Create order
            // sent to queue
            var orderCreatedEvent = new OrderCreatedEvent(10, new Dictionary<int, int>()
            {
                { 1, 5 }, { 2, 5 }
            });

            // await bus.Send(orderCreatedEvent, BusConst.OrderCreatedEventExchange);


            CancellationTokenSource cancellationTokenSource = new();

            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(60));
            await publishEndpoint.Publish(orderCreatedEvent, pipeline =>
            {
                pipeline.SetAwaitAck(true);
                pipeline.Durable = true;
            }, cancellationTokenSource.Token);
        }
    }
}