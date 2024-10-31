using BookshopManagement.DAL.Data;
using BookshopManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookshopManagement.DAL.Interfaces
{
    public interface ISalesRepository
    {
        void AddSale(Sale sale);
        IEnumerable<Sale> GetAllSales();
        IEnumerable<Sale> GetSalesByBookId(int bookId);
        IEnumerable<Sale> GetSalesByDate(DateTime date);
    }
}
