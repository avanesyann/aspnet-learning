using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasData(
                new Note() { Id = 1, Title = "First Note", Content = "This is this app's first note.", CreatedAt = new DateTime(2025, 11, 14) },
                new Note() { Id = 2, Title = "Some other note", Content = "This is some other note created while seeding data.", CreatedAt = new DateTime(2025, 11, 14) }
                );
        }
    }
}
