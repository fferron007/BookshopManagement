using BookshopManagement.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BookshopManagement.DAL
{
    public class BookshopContextFactory : IDesignTimeDbContextFactory<BookshopContext>
    {
        public BookshopContext CreateDbContext(string[] args)
        {
            // Build configuration from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Retrieve the connection string
            var connectionString = configuration.GetConnectionString("BookshopDatabase");

            // Configure DbContextOptionsBuilder with the connection string
            var optionsBuilder = new DbContextOptionsBuilder<BookshopContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BookshopContext(optionsBuilder.Options);
        }
    }
}
