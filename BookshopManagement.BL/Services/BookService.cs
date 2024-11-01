using BookshopManagement.BL.Interface;
using BookshopManagement.DAL.Interfaces;
using BookshopManagement.DAL.Models;

namespace BookshopManagement.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks() => _bookRepository.GetAll();

        public void AddBook(Book book) => _bookRepository.Add(book);

        public void UpdateBook(Book book) => _bookRepository.Update(book);

        public void DeleteBook(Book book) => _bookRepository.Delete(book);
    }
}
