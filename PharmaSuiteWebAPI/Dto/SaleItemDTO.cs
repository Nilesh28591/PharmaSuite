namespace PharmaSuiteWebAPI.Dto
{
    public class SaleItemDTO
    {
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double? Discount { get; set; }
    }
}
