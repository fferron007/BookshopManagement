using System.ComponentModel.DataAnnotations;

namespace BookshopManagement.DAL.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(100)]
        public string Author { get; set; }

        [Required, MaxLength(13)]
        public string ISBN { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}
