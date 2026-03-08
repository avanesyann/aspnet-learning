using ContactMangerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactMangerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = Guid.Parse("b5f63192-2abd-451c-98f9-07231039f542"),
                    Name = "Aven",
                    Phone = "01418296585",
                },
                new Contact
                {
                    Id = Guid.Parse("46cbd7fa-ce78-4622-b006-4fa99f289e63"),
                    Name = "Yaffa",
                    Phone = "09718103868"
                },
                new Contact
                {
                    Id = Guid.Parse("726aaad1-5355-4fe3-8113-a162f8edd6ee"),
                    Name = "Iskander",
                    Phone = "09784990682"
                }
                );
        }
    }
}
