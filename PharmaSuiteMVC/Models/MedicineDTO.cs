using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteMVC.Models
{
    public class MedicineDTO
    {
        public int MedicineId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        [MaxLength(100)]
        public string Manufacturer { get; set; }

        public double PricePerUnit { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
