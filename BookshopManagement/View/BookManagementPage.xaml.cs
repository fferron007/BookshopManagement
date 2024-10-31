using BookshopManagement.PL.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace BookshopManagement.PL.View
{
    /// <summary>
    /// Interaction logic for BookManagementPage.xaml
    /// </summary>
    public partial class BookManagementPage : Page
    {
        public BookManagementPage()
        {
            InitializeComponent();
            DataContext = App._serviceProvider.GetRequiredService<BookManagementViewModel>();
        }
    }
}
