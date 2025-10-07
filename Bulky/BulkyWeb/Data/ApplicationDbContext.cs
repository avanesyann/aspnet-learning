using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category() { Id = 2, Name = "Thriller", DisplayOrder = 2 },
                new Category() { Id = 3, Name = "Fantasy", DisplayOrder = 3 },
                new Category() { Id = 4, Name = "Drama", DisplayOrder = 4 },
                new Category() { Id = 5, Name = "History", DisplayOrder = 5 }
                );
        }
    }
}
