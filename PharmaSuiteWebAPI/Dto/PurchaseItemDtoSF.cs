using PharmaSuiteWebAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteWebAPI.Dto
{
    public class PurchaseItemDtoSF
    {
        public int PurchaseItemId { get; set; }
        public int MedicineId { get; set; }
        public string? BatchNo { get; set; }
        public string? MedicineName { get; set; }

        [Required]
        public DateTime MfgDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal CostPrice { get; set; }
        public int MinQuantity { get; set; }
    }
}
