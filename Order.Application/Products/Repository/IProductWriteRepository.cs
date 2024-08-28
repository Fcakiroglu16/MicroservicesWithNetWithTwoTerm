using Order.Domain.Write;

namespace Order.Application.Products.Repository;

public interface IProductWriteRepository
{
    ValueTask<Category> GetCategory(int id);
    Task<int> AddCategory(Category category);
    Task<string> AddProduct(Product product);

    Task UpdateProduct(Product product);
    Task DeleteProduct(Product product);
}