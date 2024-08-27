using MongoDB.Bson.Serialization.Attributes;

namespace Order.Domain.Read
{
    public class ProductWithCategory
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;

        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}