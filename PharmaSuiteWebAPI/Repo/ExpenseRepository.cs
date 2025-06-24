using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuite.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly PharmaSuiteDBContext _context;

        public ExpenseRepository(PharmaSuiteDBContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllExpensesAsync()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<decimal> GetTotalExpensesAsync()
        {
            return await _context.Expenses.SumAsync(e => e.Amount);
        }
    }
}