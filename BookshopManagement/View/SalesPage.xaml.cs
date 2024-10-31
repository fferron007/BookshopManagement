using BookshopManagement.PL.ViewModel;
using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
