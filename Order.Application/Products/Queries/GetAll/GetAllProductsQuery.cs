using MediatR;
using Order.Application.Products.Dto;

namespace Order.Application.Products.Queries.GetAll;

public record GetAllProductsQuery : IRequest<List<ProductWithCategoryDto>>;