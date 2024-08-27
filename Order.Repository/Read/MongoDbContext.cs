using MongoDB.Driver;
using Order.Domain.Read;

namespace Repository.Read
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("productdb");
        }

        public IMongoCollection<ProductWithCategory> Products =>
            _database.GetCollection<ProductWithCategory>("products");
    }
}