namespace Order.Domain.Write
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}