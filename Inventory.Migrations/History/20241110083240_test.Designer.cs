﻿// <auto-generated />
using Inventory.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Inventory.Migrations.History
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20241110083240_test")]
    partial class test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Inventory.Entities.Category", b =>
                {
                    b.Property<int>("categoryid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("categoryid"));

                    b.Property<string>("catname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("categoryid");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Inventory.Entities.Customer", b =>
                {
                    b.Property<int>("customerid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("customerid"));

                    b.Property<string>("c_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("c_phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("c_surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("c_username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("customerid");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("Inventory.Entities.Order", b =>
                {
                    b.Property<int>("orderid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("orderid"));

                    b.Property<int>("customerid")
                        .HasColumnType("integer");

                    b.Property<decimal>("total")
                        .HasColumnType("numeric");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("orderid");

                    b.HasIndex("customerid");

                    b.HasIndex("userId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Inventory.Entities.OrderProduct", b =>
                {
                    b.Property<int>("orderid")
                        .HasColumnType("integer");

                    b.Property<int>("productid")
                        .HasColumnType("integer");

                    b.Property<int>("quantity")
                        .HasColumnType("integer");

                    b.HasKey("orderid", "productid");

                    b.HasIndex("productid");

                    b.ToTable("orderproducts");
                });

            modelBuilder.Entity("Inventory.Entities.Product", b =>
                {
                    b.Property<int>("productid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("productid"));

                    b.Property<int>("categoryid")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("price")
                        .HasColumnType("numeric");

                    b.Property<string>("prodname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("quantity")
                        .HasColumnType("integer");

                    b.HasKey("productid");

                    b.HasIndex("categoryid");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Inventory.Entities.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("userId"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("userId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Inventory.Entities.Order", b =>
                {
                    b.HasOne("Inventory.Entities.Customer", "customer")
                        .WithMany("orders")
                        .HasForeignKey("customerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Inventory.Entities.User", "user")
                        .WithMany("orders")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Inventory.Entities.OrderProduct", b =>
                {
                    b.HasOne("Inventory.Entities.Order", "order")
                        .WithMany("orderproducts")
                        .HasForeignKey("orderid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Inventory.Entities.Product", "product")
                        .WithMany("orderproducts")
                        .HasForeignKey("productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("Inventory.Entities.Product", b =>
                {
                    b.HasOne("Inventory.Entities.Category", "category")
                        .WithMany("products")
                        .HasForeignKey("categoryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("Inventory.Entities.Category", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("Inventory.Entities.Customer", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("Inventory.Entities.Order", b =>
                {
                    b.Navigation("orderproducts");
                });

            modelBuilder.Entity("Inventory.Entities.Product", b =>
                {
                    b.Navigation("orderproducts");
                });

            modelBuilder.Entity("Inventory.Entities.User", b =>
                {
                    b.Navigation("orders");
                });
#pragma warning restore 612, 618
        }
    }
}
