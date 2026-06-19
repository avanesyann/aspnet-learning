using ExpenseTracker.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IExpenseRepository ExpenseRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ExpenseRepository = new ExpenseRepository(context);
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
