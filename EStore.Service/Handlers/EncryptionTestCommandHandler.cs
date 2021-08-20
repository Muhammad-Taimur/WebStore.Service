//using EStore.Contracts;
//using EStore.Service.Services;
//using Microsoft.Extensions.Logging;
//using NServiceBus;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EStore.Service.Handlers
//{
//    public class EncryptionTestCommandHandler : IHandleMessages<EncryptionTestCommand>
//    {
//        private readonly Func<IMessageHandlerContext, IEncryptionTest> _encryptionService;
//        private readonly ILogger<EncryptionTestCommand> _logger;

//        public EncryptionTestCommandHandler(ILogger<EncryptionTestCommand> logger, Func<IMessageHandlerContext, IEncryptionTest> encryptionService)
//        {
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _encryptionService =encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
//        }

//        public Task Handle(EncryptionTestCommand message, IMessageHandlerContext context)
        
//        {
//            _encryptionService(context).AddProduct(message);
            
//            throw new NotImplementedException();
//        }
//    }
//}
