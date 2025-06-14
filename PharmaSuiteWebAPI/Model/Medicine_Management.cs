using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteWebAPI.Model
{
    public class Medicine_Management
    {
        [Key]
        public int MedicineId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public double PricePerUnit { get; set; }


        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string BatchNo { get; set; }
    }
}
