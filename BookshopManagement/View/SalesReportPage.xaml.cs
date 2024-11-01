using BookshopManagement.PL.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace BookshopManagement.PL.View
{
    /// <summary>
    /// Interaction logic for SalesReportPage.xaml
    /// </summary>
    public partial class SalesReportPage : Page
    {
        public SalesReportPage()
        {
            InitializeComponent();
            DataContext = App._serviceProvider.GetRequiredService<SalesReportViewModel>();
        }
    }
}
