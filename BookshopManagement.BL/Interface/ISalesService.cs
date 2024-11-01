using BookshopManagement.BL.DTO;
using BookshopManagement.DAL.Models;

namespace BookshopManagement.BL.Interface
{
    public interface ISalesService
    {
        int AddSales(IEnumerable<CartItemDTO> cartItems);
        IEnumerable<Sale> GetSalesByDate(DateTime date);
    }
}
