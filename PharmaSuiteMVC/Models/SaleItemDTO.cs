namespace PharmaSuiteMVC.Models
{
    public class SaleItemDTO
    {
        public int MedicineId { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double? Discount { get; set; }
    }
}
