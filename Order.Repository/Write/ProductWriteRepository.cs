using Order.Application.Products;
using Order.Domain.Write;

namespace Repository.Write
{
    public class ProductWriteRepository(AppDbContext context) : IProductWriteRepository
    {
        public ValueTask<Category> GetCategory(int id)
        {
            return context.Categories.FindAsync(id);
        }

        public async Task<int> AddCategory(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return category.Id;
        }


        public async Task<string> AddProduct(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product.Id;
        }

        public async Task UpdateProduct(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}