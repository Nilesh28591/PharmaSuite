using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaSuiteWebAPI.Model
{
    public class SaleItem
    {
        [Key]
        public int ItemId { get; set; }

        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        public int MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine_Management Medicine { get; set; }

        public int Quantity { get; set; }

        public double PricePerUnit { get; set; }

        public double? Discount { get; set; }
    }
}
