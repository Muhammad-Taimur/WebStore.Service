using AutoFixture;
using EStore.Service.DBContext;
using EStore.Service.Entities;
using EStore.Service.Services.Implementation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace EStore.Service.Test
{
    public class ServiceTest
    {
        private Fixture _fixture;
        private ProductDbContext _db;
        private ProductService _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _db = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            _sut = new ProductService(_db);
        }
        [TearDown]
        public void TearDown()
        {
            _db?.Dispose();
        }

        [Test]
        public async Task GetNote_ReturnValue()
        {
            //Arrange
            var product = _fixture.Build<Product>()
                .With(n => n.ProductId, 1)
                .With(n => n.Name, "1  Product")
                .With(n => n.Description, "1 Description")
                .Create();

             await   _sut.AddProduct(product);
             _db.SaveChangesAsync();

            //Act
            var result = await _sut.GetProducts();

            //Assert
            result.Should().BeEquivalentTo(product);
        }
        [Test]
        public async Task AddNote_ReturnAccepted()
        {
            //Arrange
            var product = _fixture.Build<Product>()
                .With(n => n.ProductId, 1)
                .With(n => n.Name, "1  Product")
                .With(n => n.Description, "1 Description")
                .Create();
            
            //Act
            //Saving Prodct in DB
           await _sut.AddProduct(product);
            _db.SaveChangesAsync();

            //Getting product and compare with added Product.
            var result = await _sut.GetProducts();

            //Assert
            result.Should().BeEquivalentTo(product);
        }
    }
}