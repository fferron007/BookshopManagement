using BookshopManagement.BL.Interface;
using BookshopManagement.DAL.Data;
using BookshopManagement.DAL.Models;

namespace BookshopManagement.BL.Services
{
    public class SalesService : ISalesService
    {
        private readonly BookshopContext _context;

        public SalesService(BookshopContext context)
        {
            _context = context;
        }

        public void AddSale(Book book, int quantity)
        {
            var sale = new Sale
            {
                BookId = book.Id,
                Quantity = quantity,
                SaleDate = DateTime.Now
            };

            book.StockQuantity -= quantity;
            _context.Sales.Add(sale);
            _context.SaveChanges();
        }
    }
}
