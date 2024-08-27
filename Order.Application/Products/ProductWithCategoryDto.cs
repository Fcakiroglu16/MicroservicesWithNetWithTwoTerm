namespace Order.Application.Products
{
    public record ProductWithCategoryDto(string Id, string Name, int Quantity, decimal Price, string CategoryName);
}