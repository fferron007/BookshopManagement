using BookshopManagement.Common.Logger;
using BookshopManagement.DAL.Data;
using BookshopManagement.DAL.Interfaces;
using BookshopManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookshopManagement.DAL.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly BookshopContext _context;

        public SalesRepository(BookshopContext context)
        {
            _context = context;
        }

        public void AddSale(Sale sale)
        {
            try
            {
                _context.Sales.Add(sale);
                _context.SaveChanges();

                LoggingService.Logger.LogInformation($"Sale #ID:{sale.Id} successfully.");
            }
            catch (Exception ex)
            {
                LoggingService.Logger.LogError("Error adding sale to the database.");
                throw ex;
            }
        }

        public IEnumerable<Sale> GetAllSales()
        {
            return _context.Sales.Include(s => s.Book).ToList();
        }

        public IEnumerable<Sale> GetSalesByBookId(int bookId)
        {
            return _context.Sales
                           .Where(s => s.BookId == bookId)
                           .Include(s => s.Book)
                           .ToList();
        }

        public IEnumerable<Sale> GetSalesByDate(DateTime date)
        {
            return _context.Sales
                           .Where(s => s.SaleDate.Date == date.Date)
                           .Include(s => s.Book)
                           .ToList();
        }
    }
}
