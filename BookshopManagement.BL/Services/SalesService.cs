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

                // Update the stock of the book in the database
                var book = _context.Books.FirstOrDefault(b => b.Id == item.BookId);
                if (book != null)
                {
                    book.StockQuantity -= item.Quantity;
                }
            }

            _context.SaveChanges();

            // Return the count of items processed as a confirmation (or transaction count)
            return cartItems.Count();
        }

        public IEnumerable<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Sales
                .Where(sale => sale.SaleDate >= startDate && sale.SaleDate <= endDate)
                .Include(sale => sale.Book) 
                .ToList();
        }
    }
}
