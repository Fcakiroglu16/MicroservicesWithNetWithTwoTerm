using Order.Application.Consumers;
using Order.Domain.Read;

namespace Repository.Read
{
    public class SyncWriteRepository(MongoDbContext context) : ISyncWriteRepository
    {
        public async Task Create(ProductWithCategory productWithCategory)
        {
            await context.Products.InsertOneAsync(productWithCategory);
        }
    }
}