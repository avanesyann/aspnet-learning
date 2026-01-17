using Microsoft.EntityFrameworkCore;
using ToDoList.API.Model;

namespace ToDoList.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; }
    }
}
