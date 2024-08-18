using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceBus;

namespace Order.Service
{
    public class OrderService(IBus bus) : IOrderService
    {
        public async Task Create()
        {
            // Create order
            // sent to queue
            var orderCreatedEvent = new OrderCreatedEvent(10, new Dictionary<int, int>()
            {
                { 1, 5 }, { 2, 5 }
            });

            await bus.Send(orderCreatedEvent, BusConst.OrderCreatedEventExchange);
        }
    }
}