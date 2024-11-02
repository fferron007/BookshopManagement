using BookshopManagement.BL.Services;
using BookshopManagement.DAL.Interfaces;
using BookshopManagement.DAL.Models;
using Moq;
using Xunit;

namespace BookshopManagement.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            // Arrange
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnBooks_WhenBooksExist()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", StockQuantity = 5 },
                new Book { Id = 2, Title = "Book 2", StockQuantity = 3 }
            };
            _bookRepositoryMock.Setup(repo => repo.GetAll()).Returns(books);

            // Act
            var result = _bookService.GetAllBooks();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, b => b.Title == "Book 1");
        }

        [Fact]
        public void GetBookById_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book 1" };
            _bookRepositoryMock.Setup(repo => repo.GetBookById(1)).Returns(book);

            // Act
            var result = _bookService.GetBookById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Book 1", result.Title);
        }

        [Fact]
        public void GetBookById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock.Setup(repo => repo.GetBookById(99)).Returns((Book)null);

            // Act
            var result = _bookService.GetBookById(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddBook_ShouldCallAddMethodOnRepository()
        {
            // Arrange
            var newBook = new Book { Title = "New Book" };

            // Act
            _bookService.AddBook(newBook);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.Add(newBook), Times.Once);
        }

        [Fact]
        public void UpdateBook_ShouldCallUpdateMethodOnRepository()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book" };

            // Act
            _bookService.UpdateBook(book);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.Update(book), Times.Once);
        }

        [Fact]
        public void DeleteBook_ShouldCallDeleteMethodOnRepository()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book to Delete" };

            // Act
            _bookService.DeleteBook(book);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.Delete(book), Times.Once);
        }
    }
}
