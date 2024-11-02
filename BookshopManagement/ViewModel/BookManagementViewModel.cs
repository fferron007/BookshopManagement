using BookshopManagement.BL.Interface;
using BookshopManagement.Common;
using BookshopManagement.Common.Logger;
using BookshopManagement.DAL.Models;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BookshopManagement.PL.ViewModel
{
    public class BookManagementViewModel : INotifyPropertyChanged
    {
        #region Properties
        private readonly IBookService _bookService;
        public ObservableCollection<Book> Books { get; set; }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
                LoadBookDetails(_selectedBook);
            }
        }

        private string _title, _author, _isbn;
        private decimal _price;
        private int _stockQuantity;

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                if (_author != value)
                {
                    _author = value;
                    OnPropertyChanged(nameof(Author));
                }
            }
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                if (_isbn != value)
                {
                    _isbn = value;
                    OnPropertyChanged(nameof(ISBN));
                }
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                if (_stockQuantity != value)
                {
                    _stockQuantity = value;
                    OnPropertyChanged(nameof(StockQuantity));
                }
            }
        }

        // Commands
        public ICommand AddBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        #endregion

        #region Public Methods
        public BookManagementViewModel(IBookService bookService)
        {
            _bookService = bookService;
            Books = new ObservableCollection<Book>(_bookService.GetAllBooks());

            AddBookCommand = new RelayCommand(AddBook);
            EditBookCommand = new RelayCommand(EditBook);
            DeleteBookCommand = new RelayCommand(DeleteBook);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #region Private Methods
        private void AddBook()
        {
            try
            {
                var newBook = new Book { Title = Title, Author = Author, ISBN = ISBN, Price = Price, StockQuantity = StockQuantity };
                _bookService.AddBook(newBook);
                Books.Add(newBook);

                // Show success message
                MessageBox.Show("Book added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear input fields
                ClearFields();
            }
            catch (Exception ex)
            {
                LoggingService.Logger.LogError(ex, "Error adding book.");
                throw ex;
            }
        }

        private void EditBook()
        {
            try
            {
                if (SelectedBook == null) return;

                // Update properties of the selected book
                SelectedBook.Title = Title;
                SelectedBook.Author = Author;
                SelectedBook.ISBN = ISBN;
                SelectedBook.Price = Price;
                SelectedBook.StockQuantity = StockQuantity;

                _bookService.UpdateBook(SelectedBook);

                // Refresh the Books collection by re-fetching data from the service
                RefreshBooksCollection();

                // Show success message
                MessageBox.Show("Book updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                LoggingService.Logger.LogError(ex, "Error editing book.");
                throw ex;
            }
        }

        private void DeleteBook()
        {
            try
            {
                if (SelectedBook == null) return;

                _bookService.DeleteBook(SelectedBook);
                Books.Remove(SelectedBook);

                // Show success message
                MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear input fields after deletion
                ClearFields();
            }
            catch (Exception ex)
            {
                LoggingService.Logger.LogError(ex, "Error deliting book.");
                throw ex;
            }
        }

        private void RefreshBooksCollection()
        {
            Books.Clear();
            foreach (var book in _bookService.GetAllBooks())
            {
                Books.Add(book);
            }
        }

        private void LoadBookDetails(Book book)
        {
            if (book == null) return;

            Title = book.Title;
            Author = book.Author;
            ISBN = book.ISBN;
            Price = book.Price;
            StockQuantity = book.StockQuantity;
        }

        private void ClearFields()
        {
            Title = Author = ISBN = string.Empty;
            Price = 0.0m; 
            StockQuantity = 0;
        }
        #endregion
    }
}