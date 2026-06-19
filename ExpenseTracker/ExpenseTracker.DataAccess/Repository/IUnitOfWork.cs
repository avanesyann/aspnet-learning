using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        IExpenseRepository ExpenseRepository { get; }

        void Save();
    }
}
