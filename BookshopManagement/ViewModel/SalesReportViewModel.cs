using BookshopManagement.BL.Interface;
using BookshopManagement.Common;
using BookshopManagement.DAL.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace BookshopManagement.PL.ViewModel
{
    public class SalesReportViewModel : INotifyPropertyChanged
    {
        private readonly ISalesService _salesService;

        public ObservableCollection<Sale> SalesReport { get; set; } = new ObservableCollection<Sale>();

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public ICommand GenerateReportCommand { get; }
        public ICommand ExportToCsvCommand { get; }

        public SalesReportViewModel(ISalesService salesService)
        {
            _salesService = salesService;

            GenerateReportCommand = new RelayCommand(GenerateReport);
            ExportToCsvCommand = new RelayCommand(ExportToCsv, CanExportToCsv);
        }

        private void GenerateReport()
        {
            if (SelectedDate == null)
            {
                MessageBox.Show("Please select a date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Clear current report and load sales data for the selected date
            SalesReport.Clear();
            var sales = _salesService.GetSalesByDate(SelectedDate.Value);
            foreach (var sale in sales)
            {
                SalesReport.Add(sale);
            }
        }

        private bool CanExportToCsv() => SalesReport.Any();

        private void ExportToCsv()
        {
            var csvLines = SalesReport.Select(sale =>
                $"{sale.Book.Title},{sale.Quantity},{sale.TotalPrice},{sale.SaleDate}");

            var csvContent = "Book Title,Quantity Sold,Total Price,Sale Date\n" + string.Join("\n", csvLines);
            var filePath = $"SalesReport_{SelectedDate:yyyy-MM-dd}.csv";

            File.WriteAllText(filePath, csvContent);
            MessageBox.Show($"Sales report exported to {filePath}", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
