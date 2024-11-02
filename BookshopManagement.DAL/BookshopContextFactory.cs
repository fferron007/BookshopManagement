using BookshopManagement.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace BookshopManagement.DAL
{
    public class BookshopContextFactory : IDesignTimeDbContextFactory<BookshopContext>
    {
        public BookshopContext CreateDbContext(string[] args)
        {
            // Set the path manually
            var basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "BookshopManagement.DAL");

            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)  // Startup project directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            // Get connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("BookshopDatabase");

            // Set up DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<BookshopContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BookshopContext(optionsBuilder.Options);
        }
    }
}
