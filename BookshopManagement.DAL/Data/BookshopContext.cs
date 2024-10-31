using BookshopManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookshopManagement.DAL.Data
{
    public class BookshopContext : DbContext
    {
        public BookshopContext(DbContextOptions<BookshopContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}