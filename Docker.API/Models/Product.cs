using Microsoft.EntityFrameworkCore;

namespace Docker.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int Stock { get; set; }

        [Precision(9, 2)] public decimal Price { get; set; }

        public string CategoryName { get; set; }
    }
}