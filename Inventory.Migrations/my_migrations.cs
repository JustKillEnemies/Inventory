using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Inventory.Migrations
{
    // Фабрика для создания экземпляров контекста базы данных во время разработки
    public class AppDbContextFactory : IDesignTimeDbContextFactory<Inventory.Entities.InventoryDbContext>
    {
        // Метод, который создает и возвращает новый экземпляр InventoryDbContext
        public Inventory.Entities.InventoryDbContext CreateDbContext(string[] args)
        {
            // Создаем билдера для параметров контекста базы данных
            var optionsBuilder = new DbContextOptionsBuilder<Inventory.Entities.InventoryDbContext>();

            // Настраиваем подключение к базе данных PostgreSQL
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=inventory_db;Username=postgres;Password=postgres",
                options => options.MigrationsAssembly("Inventory.Migrations"));

            // Возвращаем новый экземпляр InventoryDbContext с заданными параметрами
            return new Inventory.Entities.InventoryDbContext(optionsBuilder.Options);
        }
    }
}
