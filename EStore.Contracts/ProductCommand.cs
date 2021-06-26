using NServiceBus;
using System.Collections.Generic;

namespace EStore.Contracts
{
    public class ProductCommand : ICommand
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<OrderCommand> Orders { get; set; }
    }
}
