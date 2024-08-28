using Order.Domain.Read;

namespace Order.Application.Products.Repository;

public interface IProductReadRepository
{
    Task<List<ProductWithCategory>> GetAll();
}