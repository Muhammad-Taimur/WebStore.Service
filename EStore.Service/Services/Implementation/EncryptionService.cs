using EStore.Service.DBContext;
using EStore.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EStore.Service.Services.Implementation
{
    public class EncryptionService : IEncryptionTest
    {
        private readonly ProductDbContext _dbContext;

        public EncryptionService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddProduct(EncryptionTest encryption)
        {
            _dbContext.EncryptionTests.Add(encryption);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EncryptionTest>> GetProducts()
        {
            return await _dbContext.EncryptionTests.ToListAsync();
        }
    }
}
