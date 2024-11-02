using BookshopManagement.BL.DTO;
using BookshopManagement.BL.Services;
using BookshopManagement.DAL.Data;
using BookshopManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookshopManagement.Tests
{
    public class SalesServiceTests
    {
        private readonly SalesService _salesService;
        private readonly BookshopContext _context;

        public SalesServiceTests()
        {
            // Set up an in-memory database context
            var options = new DbContextOptionsBuilder<BookshopContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new BookshopContext(options);
            _salesService = new SalesService(_context);

            // Seed the database with a book for foreign key constraints
            _context.Books.Add(new Book { Id = 1, Title = "Sample Book", Author = "James Jones", ISBN = "4567-1234", Price = 10.0m, StockQuantity = 100 });
            _context.SaveChanges();
        }

        
        [Fact]
        public void GetSalesByDateRange_ShouldReturnSalesWithinSpecifiedRange()
        {
            // Arrange
            // Seed sales data with varying dates
            _context.Sales.AddRange(new List<Sale>
            {
                new Sale { BookId = 1, Quantity = 1, SaleDate = new DateTime(2023, 1, 1), TotalPrice = 10.0m },
                new Sale { BookId = 1, Quantity = 2, SaleDate = new DateTime(2023, 1, 15), TotalPrice = 20.0m },
                new Sale { BookId = 1, Quantity = 3, SaleDate = new DateTime(2023, 2, 1), TotalPrice = 30.0m }
            });
            _context.SaveChanges();

            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 1, 31);

            // Act
            var sales = _salesService.GetSalesByDateRange(startDate, endDate);

            // Assert
            Assert.Equal(2, sales.Count()); // Only the sales within January 2023 should be returned
            Assert.All(sales, sale => Assert.True(sale.SaleDate >= startDate && sale.SaleDate <= endDate));
        }
    }
}
