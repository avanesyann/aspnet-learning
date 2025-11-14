using Microsoft.AspNetCore.Mvc;
using NotesApp.Data;

namespace NotesApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var notes = _context.Notes.ToList();
            return View(notes);
        }
    }
}
