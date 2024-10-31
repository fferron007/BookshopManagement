using BookshopManagement.DAL.Models;
using System.ComponentModel;

namespace BookshopManagement.PL.ViewModel
{
    public class CartItem : INotifyPropertyChanged
    {
        private int _quantity;
        public Book Book { get; set; }
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(TotalPrice)); // Update TotalPrice when Quantity changes
            }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
