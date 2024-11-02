using BookshopManagement.PL.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace BookshopManagement.PL.View
{
    /// <summary>
    /// Interaction logic for SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        public SalesPage()
        {
            InitializeComponent();
            DataContext = App._serviceProvider.GetRequiredService<SalesViewModel>();
            Loaded += SalesPage_Loaded;
        }
        private void SalesPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Cast DataContext to SalesViewModel and call RefreshAvailableBooks
            if (DataContext is SalesViewModel viewModel)
            {
                viewModel.RefreshAvailableBooks();
            }
        }
    }
}
