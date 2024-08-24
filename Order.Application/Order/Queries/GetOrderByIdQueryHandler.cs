using System.Security.Cryptography.X509Certificates;
using MediatR;

namespace Order.Application.Order.Queries
{
    internal class GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        : IRequestHandler<GetOrderByIdQuery, GetOrderByIdQueryResponse>
    {
        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdAsync(request.Id);

            return new GetOrderByIdQueryResponse(order.Id, order.Name, order.Quantity, order.Price);
        }
    }
}