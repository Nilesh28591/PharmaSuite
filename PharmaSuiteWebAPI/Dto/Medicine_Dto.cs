namespace PharmaSuiteWebAPI.Dto
{
    public class Medicine_Dto
    {
        public int MedicineId { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Manufacturer { get; set; }

        public double PricePerUnit { get; set; }

        public DateTime ExpiryDate { get; set; }
        public string BatchNo { get; set; }
    }
}
