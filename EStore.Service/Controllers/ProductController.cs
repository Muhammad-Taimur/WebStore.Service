using EStore.Contracts;
using EStore.Service.Entities;
using EStore.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Service.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IEndpointInstance _eventBus;
        private readonly IProductService _productService;
        private readonly ILogger <ProductController>_logger;
        public ProductController(IProductService productService, ILogger <ProductController> logger, IEndpointInstance eventBus)
        {
            //this means keep finding until not nullable throw excpetion if can find any not nullable
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [Route("api/[controller]")]
        [HttpGet]
        public  async Task<ActionResult> Getproduct()
        {
            var result = await _productService.GetProducts();
            _logger.LogInformation($"Product is been fetched {result.Select(a=>a.Name.ToList())}");
           
            return Ok(result);
        }
        [Route("api/[controller]/{id}")]
        [HttpGet]
        public async Task<ActionResult> Getproduct(int id)
        {
            var result = await _productService.GetProductById(id);
            _logger.LogInformation($"Product is been fetched {result.Name}");
            return Ok(result);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> PostProdcut([FromBody] Product product)
        {
            await _productService.AddProduct(product);
            await _eventBus.SendLocal(new ProductCommand
            {
                ProductId = product.ProductId,
                Name=product.Name,
                Description=product.Description,
               // Orders=product.Orders
            });
            _logger.LogInformation($"Product Message has been sent {product.Name}");
            _logger.LogInformation($"Product Posted with Product Name {product.Name}");
            return Accepted();
        }

        [HttpPut]
        [Route("api/[controller]")]
        public async Task<ActionResult> UpdateProdcut([FromBody] Product product)
        {
            await _productService.UpdateProduct(product);
            _logger.LogInformation($"Product Updated with Product ID {product.ProductId} and Product Name {product.Name}");
            return Accepted();
        }
        

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult> Deleteproduct(int id)
        {
            await _productService.DeleteProduct(id);
            _logger.LogInformation($"Product Deleted with ID {id}");
            return Ok();
        }
    }
}
