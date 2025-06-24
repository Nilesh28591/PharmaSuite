using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteWebAPI.Model
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        public string? Category { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}