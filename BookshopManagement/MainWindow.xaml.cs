using BookshopManagement.Common.Logger;
using BookshopManagement.PL.View;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace BookshopManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoggingService.Logger.LogInformation("System initialized successfully.");
        }

        private void BookManagement_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to BookManagementPage
            MainFrame.Navigate(new BookManagementPage()); 
        }

        private void SalesPage_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to SalesPage
            MainFrame.Navigate(new SalesPage());
        }

        private void SalesReport_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SalesReportPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoggingService.Logger.LogInformation("Closing system.");

            // Close the application
            Application.Current.Shutdown();
        }
    }
}
