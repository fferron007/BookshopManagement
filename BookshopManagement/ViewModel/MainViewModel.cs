using BookshopManagement.BL.Services;
using BookshopManagement.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BookshopManagement.PL.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly BookService _bookService;

        public ICommand AddBookCommand { get; }
        public ICommand SellBookCommand { get; }

        public MainViewModel(BookService bookService)
        {
            _bookService = bookService;
            AddBookCommand = new RelayCommand(async () => await AddBook());
            SellBookCommand = new RelayCommand(async () => await SellBook());
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Example of property with change notification
        private string _exampleProperty;
        public string ExampleProperty
        {
            get => _exampleProperty;
            set
            {
                if (_exampleProperty != value)
                {
                    _exampleProperty = value;
                    OnPropertyChanged(); // Notifies the UI of the change
                }
            }
        }

        private async Task AddBook() { /* Logic for adding a book */ }
        private async Task SellBook() { /* Logic for selling a book */ }
    }
}
