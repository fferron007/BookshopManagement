using BookshopManagement.DAL.Models;

namespace BookshopManagement.BL.Interface
{
    public interface ISalesService
    {
        void AddSale(Book book, int quantity);
    }
}
