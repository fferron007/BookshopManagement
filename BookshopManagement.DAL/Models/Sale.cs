namespace BookshopManagement.DAL.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Book Book { get; set; }
    }
}
