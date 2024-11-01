using BookshopManagement.DAL.Models;

namespace BookshopManagement.BL.Interface
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}
