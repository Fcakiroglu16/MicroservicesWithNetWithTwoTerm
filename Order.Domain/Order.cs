namespace Order.Repository
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}