using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Inventory.Entities
{
    // Контекст базы данных, управляющий подключением и операциями с объектами
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        // Таблицы базы данных
        public DbSet<User> users { get; set; } // Таблица пользователей
        public DbSet<Product> products { get; set; } // Таблица продуктов
        public DbSet<Category> categories { get; set; } // Таблица категорий
        public DbSet<Order> orders { get; set; } // Таблица заказов
        public DbSet<Customer> customers { get; set; } // Таблица клиентов
        public DbSet<OrderProduct> orderproducts { get; set; } // Таблица связей между заказами и продуктами (многие ко многим)

        // Настройка отношений между сущностями
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.customer)
                .WithMany(c => c.orders)
                .HasForeignKey(o => o.customerid);


            // Связь "один ко многим" между Product и Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.category)
                .WithMany(c => c.products)
                .HasForeignKey(p => p.categoryid);

            // Настройка связи "многие ко многим" между Order и Product через промежуточную таблицу OrderProduct
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.orderid, op.productid }); //  ключ для связи

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.order)
                .WithMany(o => o.orderproducts)
                .HasForeignKey(op => op.orderid); // Связь между OrderProduct и Order

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.product)
                .WithMany(p => p.orderproducts)
                .HasForeignKey(op => op.productid); // Связь между OrderProduct и Product

            // Связь "один ко многим" между Order и User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.user)
                .WithMany(u => u.orders)
                .HasForeignKey(o => o.userId);
        }
    }

    // Модель пользователя
    public class User
    {
        public int userId { get; set; } // Первичный ключ пользователя
        public string username { get; set; } // Имя пользователя
        public string name { get; set; } // Имя
        public string surname { get; set; } // Фамилия
        public string password { get; set; } // Пароль
        public ICollection<Order> orders { get; set; } // Список заказов пользователя
    }

    // Модель продукта
    public class Product
    {
        public int productid { get; set; } // Первичный ключ продукта
        public string prodname { get; set; } // Название продукта
        public int quantity { get; set; } // Количество на складе
        public decimal price { get; set; } // Цена
        public string description { get; set; } // Описание

        public int categoryid { get; set; } // Внешний ключ, связывающий продукт с категорией
        public Category category { get; set; } // Объект категории, к которой относится продукт

        public ICollection<OrderProduct> orderproducts { get; set; } // Связь многие ко многим с заказами
    }

    // Модель категории
    public class Category
    {
        public int categoryid { get; set; } // Первичный ключ категории
        public string catname { get; set; } // Название категории

        public ICollection<Product> products { get; set; } // Список продуктов в данной категории
    }

    // Модель заказа
    public class Order
    {
        public int orderid { get; set; } // Первичный ключ заказа
        public decimal total { get; set; } // Общая стоимость заказа
        public int customerid { get; set; } // Внешний ключ, связывающий заказ с клиентом
        public Customer customer { get; set; } // Объект клиента, сделавшего заказ
        public int userId { get; set; } // Внешний ключ на пользователя
        public User user { get; set; } // Объект пользователя, связанного с заказом


        public ICollection<OrderProduct> orderproducts { get; set; } // Связь многие ко многим с продуктами
    }

    // Модель клиента
    public class Customer
    {
        public int customerid { get; set; } // Первичный ключ клиента
        public string c_username { get; set; } // Имя пользователя клиента
        public string c_name { get; set; } // Имя клиента
        public string c_surname { get; set; } // Фамилия клиента
        public string c_phone { get; set; } // Телефон клиента

        public ICollection<Order> orders { get; set; } // Список заказов клиента
    }

    // Промежуточная модель для связи "многие ко многим" между Order и Product
    public class OrderProduct
    {
        public int orderid { get; set; } // Внешний ключ на заказ
        public Order order { get; set; } // Объект заказа

        public int productid { get; set; } // Внешний ключ на продукт
        public Product product { get; set; } // Объект продукта

        public int quantity { get; set; } // Количество данного продукта в заказе
    }
}
