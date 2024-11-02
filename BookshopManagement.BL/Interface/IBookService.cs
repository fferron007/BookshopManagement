using BookshopManagement.DAL.Models;

namespace BookshopManagement.BL.Interface
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int bookId);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}
