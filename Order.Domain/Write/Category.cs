namespace Order.Domain.Write
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public Category()
        {
            var x = Enumerable.Empty<Product>().ToList();
        }

        public List<Product>? Products { get; set; }
    }
}