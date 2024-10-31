﻿using BookshopManagement.BL.Interface;
using BookshopManagement.Common;
using BookshopManagement.DAL.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BookshopManagement.PL.ViewModel
{
    public class SalesViewModel : INotifyPropertyChanged
    {
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

        public SalesViewModel(IBookService bookService, ISalesService salesService)
        {
            _bookService = bookService;
            _salesService = salesService;

            AvailableBooks = new ObservableCollection<Book>(_bookService.GetAllBooks());
            Cart = new ObservableCollection<CartItem>();

            AddToCartCommand = new RelayCommand(AddToCart, CanAddToCart);
            SellCommand = new RelayCommand(Sell, CanSell);
            RemoveFromCartCommand = new RelayCommand(RemoveFromCart, CanRemoveFromCart);
        }

        private bool CanAddToCart() => true;

        private void AddToCart()
        {
            if (SelectedBook != null)
            {
                // Check if the requested quantity exceeds available stock
                if (Quantity > SelectedBook.StockQuantity)
                {
                    // Display a message to the user (for example, using MessageBox or a toast notification)
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
            }
        }

        private bool CanRemoveFromCart() => SelectedCartItem != null;

        private void RemoveFromCart()
        {
            if (SelectedCartItem != null)
            {
                Cart.Remove(SelectedCartItem);
                SelectedCartItem = null;
            }
        }

        private bool CanSell() => Cart.Any();

        private void Sell()
        {
            foreach (var cartItem in Cart)
            {
                _salesService.AddSale(cartItem.Book, cartItem.Quantity);
            }

            Cart.Clear();
            AvailableBooks = new ObservableCollection<Book>(_bookService.GetAllBooks()); // Refresh available books
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
