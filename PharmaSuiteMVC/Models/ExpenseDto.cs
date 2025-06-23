namespace PharmaSuiteMVC.Models
{
    public class ExpenseDto
    {
        public string? Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}