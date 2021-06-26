using EStore.Service.DBContext;
using EStore.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EStore.Service.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _dbContext;

        public ProductService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            var entry = _dbContext.Entry(product);
            entry.State = EntityState.Modified;
          await  _dbContext.SaveChangesAsync();
        }
    }
}
