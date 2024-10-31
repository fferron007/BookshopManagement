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

    //public async Task SellBookAsync(int bookId, int quantity)
    //{
    //    var book = await _unitOfWork.Books.GetByIdAsync(bookId);
    //    if (book == null) throw new Exception("Book not found.");
    //    if (book.StockQuantity < quantity) throw new Exception("Insufficient stock.");

    //    book.StockQuantity -= quantity;
    //    _unitOfWork.Books.Update(book);

    //    var sale = new Sale
    //    {
    //        BookId = bookId,
    //        Quantity = quantity,
    //        SaleDate = DateTime.Now,
    //        TotalPrice = book.Price * quantity
    //    };

    //    await _unitOfWork.Sales.AddAsync(sale);
    //    await _unitOfWork.SaveAsync();
    //}

}
