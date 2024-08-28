namespace Order.Domain.Write;

public class Category
{
    public Category()
    {
        var x = Enumerable.Empty<Product>().ToList();
    }

    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public List<Product>? Products { get; set; }
}