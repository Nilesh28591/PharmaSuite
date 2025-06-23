using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Repo
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllExpensesAsync();
        Task<Expense> AddExpenseAsync(Expense expense);
        Task<decimal> GetTotalExpensesAsync();
    }
}