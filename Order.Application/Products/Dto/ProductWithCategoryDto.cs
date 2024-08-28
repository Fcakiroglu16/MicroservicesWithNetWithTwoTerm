namespace Order.Application.Products.Dto
{
    public record ProductWithCategoryDto(string Id, string Name, int Quantity, decimal Price, string CategoryName);
}