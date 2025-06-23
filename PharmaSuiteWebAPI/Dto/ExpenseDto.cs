namespace PharmaSuiteWebAPI.Dto
{
    public class ExpenseDto
    {
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class ProfitDto
    {
        public decimal TotalSales { get; set; }
        public decimal TotalCostOfGoods { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit => TotalSales - TotalCostOfGoods - TotalExpenses;
    }
}