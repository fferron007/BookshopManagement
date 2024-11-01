using BookshopManagement.BL.Interface;
using BookshopManagement.BL.Services;
using BookshopManagement.Common;
using BookshopManagement.DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace BookshopManagement.PL.ViewModel
{
    public class SalesReportViewModel : INotifyPropertyChanged
    {
        private DateTime? _startDate;
        private DateTime? _endDate;
        public ObservableCollection<Sale> SalesReport { get; set; }
        private readonly ISalesService _salesService;
        private readonly IConfiguration _configuration;
        private readonly string _reportDirectoryPath;


        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public ICommand GenerateReportCommand { get; }
        public ICommand ExportToCsvCommand { get; }

        public SalesReportViewModel(ISalesService salesService, IConfiguration configuration)
        {
            _salesService = salesService;
            _configuration = configuration;

            SalesReport = new ObservableCollection<Sale>();
            GenerateReportCommand = new RelayCommand(GenerateReport);
            ExportToCsvCommand = new RelayCommand(ExportToCsv, CanExportToCsv);

            // Retrieve the directory path from configuration
            _reportDirectoryPath = _configuration["ReportSettings:ReportDirectoryPath"];

            EnsureReportDirectoryExists();
        }

        private void EnsureReportDirectoryExists()
        {
            // Check if directory exists and create if not
            if (!Directory.Exists(_reportDirectoryPath))
            {
                Directory.CreateDirectory(_reportDirectoryPath);
            }
        }

        private void GenerateReport()
        {
            if (StartDate == null || EndDate == null)
            {
                MessageBox.Show("Please, enter a valid date range.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime start = StartDate.Value.Date;
            DateTime end = EndDate.Value.Date.AddDays(1).AddTicks(-1);

            SalesReport.Clear();
            var reportData = GetSalesDataForDateRange(start, end);

            if (reportData.Count() > 0)
            {
                foreach (var sale in reportData)
                {
                    SalesReport.Add(sale);
                }

                // Notify that CanExecute should be re-evaluated
                ((RelayCommand)ExportToCsvCommand).RaiseCanExecuteChanged();
            }
            else
            {
                MessageBox.Show("There is no sales in this period.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanExportToCsv() => SalesReport.Any();

        private void ExportToCsv()
        {
            var csvLines = SalesReport.Select(sale =>
                $"{sale.Book.Title},{sale.Quantity},{sale.TotalPrice},{sale.SaleDate}");

            var csvContent = "Book Title,Quantity Sold,Total Price,Sale Date\n" + string.Join("\n", csvLines);
            var filePath = Path.Combine(_reportDirectoryPath, $"SalesReport_{DateTime.Now:yyyy-MM-dd}.csv");

            File.WriteAllText(filePath, csvContent);
            MessageBox.Show($"Sales report exported to {filePath}", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private IEnumerable<Sale> GetSalesDataForDateRange(DateTime startDate, DateTime endDate)
        {
            return _salesService.GetSalesByDateRange(startDate, endDate);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
