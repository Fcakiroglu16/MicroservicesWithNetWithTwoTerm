using MediatR;
using Order.Application.Products.Repository;
using Order.Domain.Write;

namespace Order.Application.Categories;

internal class CreateCategoryCommandHandler(IProductWriteRepository productWriteRepository)
    : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await productWriteRepository.AddCategory(new Category { Name = request.Name });
    }
}