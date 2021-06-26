using EStore.Service.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EStore.Service.Services
{
    public interface IEncryptionTest
    {
        Task<IEnumerable<EncryptionTest>> GetProducts();
        Task AddProduct(EncryptionTest encryption);
    }
}
