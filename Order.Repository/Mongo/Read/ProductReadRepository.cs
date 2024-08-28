using MongoDB.Driver;
using Order.Application.Products.Repository;
using Order.Domain.Read;

namespace Repository.Mongo.Read;

public class ProductReadRepository(MongoDbContext client) : IProductReadRepository
{
    public async Task<List<ProductWithCategory>> GetAll()
    {
        return await client.Products.Find(f => true).ToListAsync();
    }
}