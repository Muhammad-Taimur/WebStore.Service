using EStore.Service.Entities;
using EStore.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Service.Controllers
{
    public class EncryptionController : ControllerBase
    {

        private readonly IEncryptionTest _encryptiontest;
        private readonly ILogger <EncryptionController> _logger;


        public EncryptionController(IEncryptionTest encryptionTest, ILogger<EncryptionController> logger)
        {
            //this means keep finding until not nullable throw excpetion if can find any not nullable
            _encryptiontest = encryptionTest ?? throw new ArgumentNullException(nameof(encryptionTest));
            _logger = logger;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult> Getproduct()
        {
            var result = await _encryptiontest.GetProducts();
            _logger.LogInformation($"List of Encryption {result.Select(a=>a.Name.ToList())}");
            return Ok(result);
            
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> PostProdcut([FromBody] EncryptionTest encryptionTest)
        {
            await _encryptiontest.AddProduct(encryptionTest);
            _logger.LogInformation($"Encryption {@encryptionTest} Posted");
            return Accepted();
        }

    }
}
