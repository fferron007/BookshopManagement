using BookshopManagement.DAL.Data;
using BookshopManagement.DAL.Interfaces;
using BookshopManagement.DAL.Models;

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
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
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
            }
        }

        public void Delete(Book book)
        {
            var existing = _context.Books.Find(book.Id);
            if (existing != null)
            {
                _context.Books.Remove(existing);
                _context.SaveChanges();
            }
        }
    }

}
