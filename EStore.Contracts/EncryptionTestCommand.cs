using NServiceBus;
namespace EStore.Contracts
{
    public class EncryptionTestCommand : ICommand
    {
        public int EncID { get; set; }
        public string Name { get; set; }
    }
}
