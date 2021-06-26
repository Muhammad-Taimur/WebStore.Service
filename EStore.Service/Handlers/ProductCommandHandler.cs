using EStore.Contracts;
using EStore.Service.Services;
using Microsoft.Extensions.Logging;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Service.Handlers
{
    public class ProductCommandHandler :IHandleMessages<ProductCommand>
    {
        private readonly ILogger<ProductCommandHandler> _logger;
        private readonly Func<IMessageHandlerContext, IProductService> _productFactory;

        public ProductCommandHandler(ILogger<ProductCommandHandler> logger, Func<IMessageHandlerContext, IProductService> productServiceFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productFactory = productServiceFactory ?? throw new ArgumentNullException(nameof(productServiceFactory));
        }

        public async Task Handle(ProductCommand message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Product Message has been sent {message}");

            //await _productFactory(context).AddProduct(message);
            throw new NotImplementedException();
        }
    }
}
