using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Data;
using NotesApp.Models;

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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
                return NotFound();

            Note? note = _context.Notes.Find(id);

            if (note == null)
                return NotFound();

            return View(note);
        }

        [HttpPost]
        public IActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Notes.Update(note);
                _context.SaveChanges();
                return RedirectToAction("Index", "Notes");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
                return NotFound();

            Note? note = _context.Notes.Find(id);

            if (note == null)
                return NotFound();

            return View(note);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            if (id == 0 || id == null)
                return NotFound();

            Note? note = _context.Notes.Find(id);

            if (note == null)
                return NotFound();

            _context.Notes.Remove(note);
            _context.SaveChanges();

            return RedirectToAction("Index", "Notes");
        }
    }
}
