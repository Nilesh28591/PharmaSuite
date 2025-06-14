using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaSuiteWebAPI.Model
{
    public class PurchaseItem
    {
        [Key]
        public int PurchaseItemId { get; set; }
        [ForeignKey("Purchase")]
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public Medicine_Management Medicine { get; set; }
        [Required]
        public string? BatchNo { get; set; }
        [Required]
        public DateTime MfgDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal CostPrice { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
