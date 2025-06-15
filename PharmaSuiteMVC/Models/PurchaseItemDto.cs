using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteMVC.Models
{
    public class PurchaseItemDto
    {
        public int PurchaseItemId { get; set; }
        public int PurchaseId { get; set; }
        public int MedicineId { get; set; }
        public string? BatchNo { get; set; }
        public string? MedicineName { get; set; }

        public DateTime MfgDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public int MinQuantity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
