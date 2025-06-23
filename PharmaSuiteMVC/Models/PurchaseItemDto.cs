using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteMVC.Models
{
    public class PurchaseItemDto
    {
        public int MedicineId { get; set; }
        public string Name { get; set; } // Medicine Name
        public string BatchNo { get; set; }
        public DateTime MfgDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public string CreatedBy { get; set; }
    }
}
