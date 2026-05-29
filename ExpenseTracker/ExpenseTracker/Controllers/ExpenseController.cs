using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Expense> expenses = _context.Expenses.ToList();

            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            if (expense.Description == expense.Amount.ToString())
            {
                ModelState.AddModelError("description", "Both fields cannot match.");
            }
            if (ModelState.IsValid)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();

                TempData["success"] = "Expense successfully created!";

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            Expense? dbExpense = _context.Expenses.FirstOrDefault(u => u.Id == id);
            // Expense? dbExpense2 = _context.Expenses.Find(id);
            // Expense? dbExpense3 = _context.Expenses.Where(u => u.Id == id).FirstOrDefault();
            
            if (dbExpense == null)
                return NotFound();

            
            return View(dbExpense);
        }

        [HttpPost]
        public IActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Update(expense);
                _context.SaveChanges();

                TempData["success"] = "Expense successfully edited!";

                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            Expense? dbExpense = _context.Expenses.FirstOrDefault(u => u.Id == id);

            if (dbExpense == null)
                return NotFound();


            return View(dbExpense);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Expense? expense = _context.Expenses.FirstOrDefault(u => u.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            _context.SaveChanges();

            TempData["success"] = "Expense successfully deleted!";

            return RedirectToAction("Index");
        }
    }
}
