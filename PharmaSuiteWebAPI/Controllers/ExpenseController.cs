using Microsoft.AspNetCore.Mvc;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Services;
using System.Threading.Tasks;

namespace PharmaSuiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseDto dto)
        {
            await _expenseService.AddExpenseAsync(dto);
            return Ok("Expense added successfully");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _expenseService.GetAllExpensesAsync();
            return Ok(data);
        }

        [HttpGet("profit")]
        public async Task<IActionResult> GetProfit([FromQuery] decimal totalSales, [FromQuery] decimal totalCostOfGoods)
        {
            var profit = await _expenseService.CalculateProfitAsync(totalSales, totalCostOfGoods);
            return Ok(profit);
        }
    }
}