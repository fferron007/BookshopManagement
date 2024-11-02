using BookshopManagement.BL.DTO;
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
    public class SalesViewModel : INotifyPropertyChanged
    {
        #region Properties 

        private readonly IBookService _bookService;
        private readonly ISalesService _salesService;

        public ObservableCollection<Book> AvailableBooks { get; set; }
        public ObservableCollection<CartItem> Cart { get; set; } = new ObservableCollection<CartItem>();

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
                ((RelayCommand)AddToCartCommand).RaiseCanExecuteChanged();
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                ((RelayCommand)AddToCartCommand).RaiseCanExecuteChanged();
            }
        }

        private CartItem _selectedCartItem;
        public CartItem SelectedCartItem
        {
            get => _selectedCartItem;
            set
            {
                _selectedCartItem = value;
                OnPropertyChanged(nameof(SelectedCartItem));
                ((RelayCommand)RemoveFromCartCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddToCartCommand { get; }
        public ICommand SellCommand { get; }
        public ICommand RemoveFromCartCommand { get; }

        #endregion

        #region Public Methods
        public SalesViewModel(IBookService bookService, ISalesService salesService)
        {
            _bookService = bookService;
            _salesService = salesService;

            AvailableBooks = new ObservableCollection<Book>(_bookService.GetAllBooks().Where(b => b.StockQuantity > 0 && b.IsActive));
            Cart = new ObservableCollection<CartItem>();

            // Subscribe to CollectionChanged to update CanSell state
            Cart.CollectionChanged += (s, e) => ((RelayCommand)SellCommand).RaiseCanExecuteChanged();

            AddToCartCommand = new RelayCommand(AddToCart, CanAddToCart);
            SellCommand = new RelayCommand(Sell, CanSell);
            RemoveFromCartCommand = new RelayCommand(RemoveFromCart, CanRemoveFromCart);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #region Private Methods
        private bool CanAddToCart() => true;

        private void AddToCart()
        {
            try
            {
                if (SelectedBook != null && Quantity > 0)
                {
                    // Check if the requested quantity exceeds available stock
                    if (Quantity > SelectedBook.StockQuantity)
                    {
                        MessageBox.Show("Insufficient stock. Please reduce the quantity.", "Stock Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return; // Exit the method to prevent adding to the cart
                    }

                    // Proceed with adding to cart if stock is sufficient
                    var existingItem = Cart.FirstOrDefault(item => item.Book.Id == SelectedBook.Id);

                    if (existingItem != null)
                    {
                        // Update quantity and total price for the existing item
                        existingItem.Quantity += Quantity;
                        existingItem.TotalPrice = existingItem.Quantity * SelectedBook.Price;
                    }
                    else
                    {
                        // Add a new item to the cart
                        Cart.Add(new CartItem
                        {
                            Book = SelectedBook,
                            Quantity = Quantity,
                            TotalPrice = Quantity * SelectedBook.Price
                        });
                    }

                    // Reduce the available stock quantity
                    SelectedBook.StockQuantity -= Quantity;

                    // Clear the quantity input after adding to the cart
                    Quantity = 0;

                    // Refresh the AvailableBooks collection to update the Stock Quantity in the UI
                    RefreshAvailableBooks();
                }
                else
                {
                    MessageBox.Show("Enter a quantity greater than zero.", "Cart Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                LoggingService.Logger.LogError(ex, "Error adding book to cart.");
                throw ex;
            }
        }

        private void RefreshAvailableBooks()
        {
            var books = AvailableBooks.Where(b => b.StockQuantity > 0).ToList();

            AvailableBooks.Clear();

            foreach (var book in books)
            {
                AvailableBooks.Add(book);
            }
        }


        private bool CanRemoveFromCart() => SelectedCartItem != null;

        private void RemoveFromCart()
        {
            if (SelectedCartItem != null)
            {
                // Find the corresponding book in AvailableBooks
                var bookToUpdate = _bookService.GetAllBooks().Where(b => b.IsActive).FirstOrDefault(b => b.Id == SelectedCartItem.Book.Id);

                if (bookToUpdate != null)
                {
                    // Recalculate stock by adding the quantity back
                    bookToUpdate.StockQuantity += SelectedCartItem.Quantity;
                }

                // Refresh AvailableBooks collection to update the UI
                var existingBook = AvailableBooks.FirstOrDefault(b => b.Id == bookToUpdate.Id);

                if (existingBook != null)
                {
                    RefreshAvailableBooks();
                } else {
                    AvailableBooks.Add(bookToUpdate);
                }

                // Remove the item from the cart
                Cart.Remove(SelectedCartItem);
                SelectedCartItem = null;

              
            }
        }

        private bool CanSell() => Cart.Any();

        private void Sell()
        {
            if (Cart.Any())
            {
                // Map each CartItem to CartItemDTO for the service layer
                var cartItemsDto = Cart.Select(item => new CartItemDTO
                {
                    BookId = item.Book.Id,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                }).ToList();

                // Save the sales using the service and get the sales count
                int salesCount = _salesService.AddSales(cartItemsDto);

                // Show success message
                MessageBox.Show($"{salesCount} item(s) have been successfully ordered.", "Sales Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear the cart and refresh the available books
                Cart.Clear();
                RefreshAvailableBooks();
            }
        }
        #endregion
    }
}
