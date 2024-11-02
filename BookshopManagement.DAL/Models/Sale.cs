using System.ComponentModel.DataAnnotations;

namespace BookshopManagement.DAL.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a non-negative value.")]
        public decimal TotalPrice { get; set; }


        public virtual Book Book { get; set; }
    }
}
