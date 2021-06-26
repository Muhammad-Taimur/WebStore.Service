namespace EStore.Service.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int ProductForeignKey { get; set; }

        public Product product { get; set; }

    }
}
