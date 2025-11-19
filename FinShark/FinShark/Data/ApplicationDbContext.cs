using FinShark.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Stock> Stocks { get; set; }        // using DbSet, we are manipulating the whole table.
        public DbSet<Comment> Comments { get; set; }
    }
}
