using AutoFixture;
using EStore.Service.Controllers;
using EStore.Service.Entities;
using EStore.Service.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EStore.Service.Test.Controller
{
    internal class ProductControllerTest
    {
        private Fixture _fixture;
        private ProductController _sut;
        private Mock<IProductService> _productService;
        private ILogger<ProductController> _logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [SetUp]
        public void SetUp()
        {
            _logger = Mock.Of<ILogger<ProductController>>();
            _productService = new Mock<IProductService>();
            _sut  = new ProductController (_productService.Object,_logger);
        }
        [Test]
        public async Task GetProduct_ReturnsOk()
        {
            //Arrange
            var product = _fixture.Build<Product>()
                .With(n => n.ProductId, 1)
                .With(n => n.Name, "1  Product")
                .With(n => n.Description, "1 Description")
                .Create();

            //Posting product in DB
            // _productService.Setup(x => x.GetProducts());

            var postProduct = await _sut.PostProdcut(product);

            //var productResponse = 
            //_productService.Setup(a => a.GetProducts())
                //.ReturnsAsync(product);

            //Act
            var response = await _sut.Getproduct() as OkObjectResult;

            //Assert 
            response.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeOfType<Product>();

            //response.Value.Should().BeOfType<Product[]>();
            //var ps = response.Value.As<Product[]>();
            //ps.Should().BeNullOrEmpty();

            //var pm = response.Value.As<Product>();
            //pm.Name.Should().Be("6  Product");
            //response.Value.Should().BeNull();

            //var productModel = response.Value.As<Product>();
            //productModel.Name.Should().Be("6  Product");
        }
        [Test]
        public async Task GetProduct_IfNoData_ReturnsEmptyArrayAndReturnOk() {

            Product[] emptyProduct = new Product[] { };

            var product = _fixture.Create<Product>();
            var response = await _sut.Getproduct() as OkObjectResult;

            response.Should().BeOfType<OkObjectResult>();
            response.Value.Should().Equals(emptyProduct);
        }

        [Test]
        public async Task PostProduct()
        {
            //Arrange
            var product = _fixture.Build<Product>()
               .With(n => n.ProductId, 7)
               .With(n => n.Name, " 7 Product")
               .With(n => n.Description, "7 Description")
               .Create();

            //Act
            var response = await  _sut.PostProdcut(product);

            //Assert
            response.Should().BeOfType<AcceptedResult>();
        }

    }
}
