using NServiceBus;

namespace EStore.Contracts
{
    public class OrderCommand :ICommand
    {
        public int OrderId { get; set; }

        public int ProductForeignKey { get; set; }

        public ProductCommand product { get; set; }

    }
}
