using BookshopManagement.BL.DTO;

namespace BookshopManagement.BL.Interface
{
    public interface ISalesService
    {
        int AddSales(IEnumerable<CartItemDTO> cartItems);
    }
}
