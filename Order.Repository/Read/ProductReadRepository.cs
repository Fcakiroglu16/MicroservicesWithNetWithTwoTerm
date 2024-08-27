using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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