using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteWebAPI.Model
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;

        [MaxLength(200)]
        public string CustomerName { get; set; }

        public double TotalAmount { get; set; }

        public List<SaleItem> SaleItems { get; set; }
    }
}
