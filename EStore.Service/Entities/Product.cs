using System.Collections.Generic;

namespace EStore.Service.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
