using BookshopManagement.BL.DTO;
using BookshopManagement.BL.Interface;
using BookshopManagement.DAL.Data;
using BookshopManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookshopManagement.BL.Services
{
    public class SalesService : ISalesService
    {
        private readonly BookshopContext _context;

        public SalesService(BookshopContext context)
        {
            _context = context;
        }

        public int AddSales(IEnumerable<CartItemDTO> cartItems)
        {
            foreach (var item in cartItems)
            {
                // Create a new Sale record
                var sale = new Sale
                {
                    BookId = item.BookId,         
                    Quantity = item.Quantity,
                    SaleDate = DateTime.Now,
                    TotalPrice = item.TotalPrice
                };
                _context.Sales.Add(sale);
            }

            _context.SaveChanges();

            // Return the count of items processed as a confirmation (or transaction count)
            return cartItems.Count();
        }

        public IEnumerable<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            // Retrieve all sales in the date range, including deleted books
            var sales = _context.Sales
                .Where(sale => sale.SaleDate >= startDate && sale.SaleDate <= endDate)
                .Include(sale => sale.Book) // Include book to access related data if available
                .AsEnumerable() 
                .Select(sale => new Sale
                {
                    Id = sale.Id,
                    BookId = sale.BookId,
                    Quantity = sale.Quantity,
                    SaleDate = sale.SaleDate,
                    TotalPrice = sale.TotalPrice,
                    Book = sale.Book ?? new Book { Title = "Book no longer available" }
                })
                .ToList();

            return sales;
        }

    }
}
