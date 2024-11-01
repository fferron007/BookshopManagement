namespace BookshopManagement.BL.DTO
{
    public class CartItemDTO
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
