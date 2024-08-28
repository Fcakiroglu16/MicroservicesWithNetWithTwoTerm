using MediatR;

namespace Order.Application.Categories;

public record CreateCategoryCommand(string Name) : IRequest<int>;