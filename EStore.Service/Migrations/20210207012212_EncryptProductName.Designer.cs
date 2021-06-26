﻿// <auto-generated />
using EStore.Service.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EStore.Service.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    [Migration("20210207012212_EncryptProductName")]
    partial class EncryptProductName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Product")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EStore.Service.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ProductForeignKey")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("ProductForeignKey");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("EStore.Service.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("EStore.Service.Entities.Order", b =>
                {
                    b.HasOne("EStore.Service.Entities.Product", "product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("EStore.Service.Entities.Product", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
