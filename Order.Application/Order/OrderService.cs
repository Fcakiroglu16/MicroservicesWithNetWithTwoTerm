using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Order.CreateOrderUseCase;

namespace Order.Application.Order
{
    public interface IOrderService
    {
        Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request);
    }

    public class OrderService(IOrderRepository orderRepository, IBusService busService) : IOrderService
    {
        public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
        {
            var order = new Repository.Order
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price
            };


            var orderId = orderRepository.CreateOrder(order);

            await busService.PublishAsync(new OrderCreatedEvent(orderId, order.Quantity));

            return new CreateOrderResponse(orderId);
        }
    }
}