using BookshopManagement.BL.Interface;
using BookshopManagement.Common;
using BookshopManagement.DAL.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BookshopManagement.PL.ViewModel
{
    public class BookManagementViewModel : INotifyPropertyChanged
    {
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

        // Properties for binding to UI fields
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

        public BookManagementViewModel(IBookService bookService)
        {
            _bookService = bookService;
            Books = new ObservableCollection<Book>(_bookService.GetAllBooks());

            AddBookCommand = new RelayCommand(AddBook);
            EditBookCommand = new RelayCommand(EditBook);
            DeleteBookCommand = new RelayCommand(DeleteBook);
        }

        private void AddBook()
        {
            var newBook = new Book { Title = Title, Author = Author, ISBN = ISBN, Price = Price, StockQuantity = StockQuantity };
            _bookService.AddBook(newBook);
            Books.Add(newBook);
            ClearFields();
        }

        private void EditBook()
        {
            if (SelectedBook == null) return;

            SelectedBook.Title = Title;
            SelectedBook.Author = Author;
            SelectedBook.ISBN = ISBN;
            SelectedBook.Price = Price;
            SelectedBook.StockQuantity = StockQuantity;

            _bookService.UpdateBook(SelectedBook);
            Books[Books.IndexOf(SelectedBook)] = SelectedBook;
        }

        private void DeleteBook()
        {
            if (SelectedBook == null) return;

            _bookService.DeleteBook(SelectedBook);
            Books.Remove(SelectedBook);
            ClearFields();
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
            Price = 0;
            StockQuantity = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}