namespace PharmaSuiteWebAPI.Dto
{
    public class AvailableMedicineDTO
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public double PricePerUnit { get; set; }

        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public double CostPrice { get; set; }
    }
}
