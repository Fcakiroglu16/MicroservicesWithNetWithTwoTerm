namespace Order.Application.Products
{
    public interface IProductReadRepository
    {
        Task<List<ProductWithCategoryDto>> GetAll();
    }
}