using ExpenseTracker.DataAccess.Data;
using ExpenseTracker.DataAccess.Repository;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IActionResult Index()
        {
            List<Expense> expenses = _expenseRepository.GetAll().ToList();

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
                _expenseRepository.Add(expense);
                _expenseRepository.Save();

                TempData["success"] = "Expense created successfully!";

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            Expense? dbExpense = _expenseRepository.Get(u => u.Id == id);
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
                _expenseRepository.Update(expense);
                _expenseRepository.Save();

                TempData["success"] = "Expense edited successfully!";

                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            Expense? dbExpense = _expenseRepository.Get(u => u.Id == id);

            if (dbExpense == null)
                return NotFound();


            return View(dbExpense);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Expense? expense = _expenseRepository.Get(u => u.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            _expenseRepository.Remove(expense);
            _expenseRepository.Save();

            TempData["success"] = "Expense deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}
