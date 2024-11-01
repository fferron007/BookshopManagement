using BookshopManagement.DAL.Data;
using BookshopManagement.DAL.Interfaces;
using BookshopManagement.DAL.Models;
using BookshopManagement.Common.Logger;
using Microsoft.Extensions.Logging;

namespace BookshopManagement.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookshopContext _context;

        public BookRepository(BookshopContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll() => _context.Books.ToList();

        public void Add(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();

                LoggingService.Logger.LogInformation($"Book ID: {book.Id} added successfully.");
            }
            catch (Exception ex)
            {
                LoggingService.Logger.LogError(ex, "Error adding book to the database.");
                throw ex;
            }
        }

        public void Update(Book book)
        {
            try
            {
                var existing = _context.Books.Find(book.Id);
                if (existing != null)
                {
                    existing.Title = book.Title;
                    existing.Author = book.Author;
                    existing.ISBN = book.ISBN;
                    existing.Price = book.Price;
                    existing.StockQuantity = book.StockQuantity;
                    _context.SaveChanges();

                    LoggingService.Logger.LogInformation($"Book ID: {existing.Id} updated successfully.");
                }
            }
            catch (Exception ex)
            {
                LoggingService.Logger.LogError(ex, "Error updating book to the database.");
                throw ex;
            }
        }

        public void Delete(Book book)
        {
            try
            {
                var existing = _context.Books.Find(book.Id);
                if (existing != null)
                {
                    _context.Books.Remove(existing);
                    _context.SaveChanges();

                    LoggingService.Logger.LogInformation($"Book ID: {existing.Id} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                LoggingService.Logger.LogError(ex, "Error deleting book to the database.");
                throw ex;
            }
        }
    }

}
