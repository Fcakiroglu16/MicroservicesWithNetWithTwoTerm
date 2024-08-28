using MongoDB.Driver;
using Order.Application.Products;

namespace Repository.Read
{
    public class ProductReadRepository(MongoDbContext client) : IProductReadRepository
    {
        public async Task<List<ProductWithCategoryDto>> GetAll()
        {
            // get all products
            var products = await client.Products.Find(f => true).ToListAsync();


            return products.Select(p => new ProductWithCategoryDto(p.Id, p.Name, p.Quantity, p.Price, p.CategoryName))
                .ToList();
        }
    }
}