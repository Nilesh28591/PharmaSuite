using AutoMapper;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Services
{
    public interface IExpenseService
    {
        Task AddExpenseAsync(ExpenseDto dto);
        Task<List<ExpenseDto>> GetAllExpensesAsync();
        Task<ProfitDto> CalculateProfitAsync(decimal totalSales, decimal totalCostOfGoods);
    }

    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repo;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddExpenseAsync(ExpenseDto dto)
        {
            var expense = _mapper.Map<Expense>(dto);
            await _repo.AddExpenseAsync(expense);
        }

        public async Task<List<ExpenseDto>> GetAllExpensesAsync()
        {
            var expenses = await _repo.GetAllExpensesAsync();
            return _mapper.Map<List<ExpenseDto>>(expenses);
        }

        public async Task<ProfitDto> CalculateProfitAsync(decimal totalSales, decimal totalCostOfGoods)
        {
            var totalExpenses = await _repo.GetTotalExpensesAsync();

            return new ProfitDto
            {
                TotalSales = totalSales,
                TotalCostOfGoods = totalCostOfGoods,
                TotalExpenses = totalExpenses
            };
        }
    }
}