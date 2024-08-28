using MediatR;
using Order.Application.Products.Dto;
using Order.Application.Products.Repository;

namespace Order.Application.Products.Queries.GetAll
{
    internal class GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        : IRequestHandler<GetAllProductsQuery, List<ProductWithCategoryDto>>
    {
        public async Task<List<ProductWithCategoryDto>> Handle(GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var products = await productReadRepository.GetAll();


            return products.Select(p => new ProductWithCategoryDto(p.Id, p.Name, p.Quantity, p.Price, p.CategoryName))
                .ToList();
        }
    }
}