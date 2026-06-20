using ExpenseTracker.DataAccess.Data;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repository
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        private ApplicationDbContext _context;
        public ExpenseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Expense expense)
        {
            _context.Update(expense);
        }
    }
}
