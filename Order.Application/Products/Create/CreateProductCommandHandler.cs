﻿using MassTransit;
using MediatR;
using Order.Domain.Events;

namespace Order.Application.Products.Create
{
    public class CreateProductCommandHandler(
        IProductWriteRepository productWriteRepository,
        IPublishEndpoint publishEndpoint)
        : IRequestHandler<CreateProductCommand, string>
    {
        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Write.Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            var newProductId = await productWriteRepository.AddProduct(product);

            var category = await productWriteRepository.GetCategory(request.CategoryId);


            await publishEndpoint.Publish(new ProductCreatedEvent(newProductId, request.Name, request.Quantity,
                request.Price, category.Name), cancellationToken);

            return newProductId;
        }
    }
}