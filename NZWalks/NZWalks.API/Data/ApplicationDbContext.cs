using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty() { Id = Guid.Parse("b9ffd6d6-0054-420f-a7a8-a3ae46355d1f"), Name = "Easy" },
                new Difficulty() { Id = Guid.Parse("1670a8c4-367b-41e0-bc3b-2978e15d860c"), Name = "Medium" },
                new Difficulty() { Id = Guid.Parse("519a7515-27b5-4e98-87ed-5292fbffcbb5"), Name = "Hard" },
            };

            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }
    }
}
