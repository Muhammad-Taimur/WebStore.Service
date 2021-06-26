using EStore.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EStore.Service.DBContext
{
    public class ProductDbContext : DbContext
    {

        public ProductDbContext()
        {
        }
        public const string Schema = "Product";
            public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet <Product>Products {get; set;}
        public DbSet <Order>Orders{get; set;}
        public DbSet <EncryptionTest> EncryptionTests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);

            //Creating Foreign Key This is Fluent API of EF Core
            modelBuilder.Entity<Order>()
                .HasOne(p => p.product)
                .WithMany(o => o.Orders)
                .HasForeignKey(fk => fk.ProductForeignKey);

            //modelBuilder.Entity<Product>()
            //    .Property(p => p.Name).HasMaxLength(250);

            //This will Singularize all the tables name
            foreach(var entityType  in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

    }
}
