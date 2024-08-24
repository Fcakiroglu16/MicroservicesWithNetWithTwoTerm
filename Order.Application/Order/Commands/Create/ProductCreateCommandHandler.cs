using MediatR;
using Order.Application.Order.CreateOrderUseCase;

namespace Order.Application.Order.Commands.Create
{
    public class ProductCreateCommandHandler(IOrderRepository orderRepository, IBusService busService)
        : IRequestHandler<OrderCreateCommand, OrderCreateCommandResponse>
    {
        public async Task<OrderCreateCommandResponse> Handle(OrderCreateCommand request,
            CancellationToken cancellationToken)
        {
            // localhost:5000/api/order/pagedList/1/10
            var order = new Repository.Order
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price
            };


            var orderId = orderRepository.CreateOrder(order);

            await busService.PublishAsync(new OrderCreatedEvent(orderId, order.Quantity));

            return new OrderCreateCommandResponse(orderId);
        }
    }
}