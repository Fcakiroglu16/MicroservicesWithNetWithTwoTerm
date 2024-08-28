using Order.Application.Consumers;
using Order.Domain.Read;
using Repository.Mongo.Read;

namespace Repository.Mongo.Write;

public class SyncWriteRepository(MongoDbContext context) : ISyncWriteRepository
{
    public async Task Create(ProductWithCategory productWithCategory)
    {
        await context.Products.InsertOneAsync(productWithCategory);
    }
}