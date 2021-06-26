using EStore.Service.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EStore.Service.Services
{
    public interface IProductService
    {
        Task <IEnumerable<Product>>GetProducts();
        Task AddProduct(Product product);
        Task <Product>GetProductById(int id);

        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
