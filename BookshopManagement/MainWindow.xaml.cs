using BookshopManagement.PL.View;
using System.Windows;

namespace BookshopManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }
    }
}
