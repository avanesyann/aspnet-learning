using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repository
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        void Update(Expense expense);
    }
}
