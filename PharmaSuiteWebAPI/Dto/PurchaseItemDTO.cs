namespace PharmaSuiteWebAPI.Dto
{
    public class PurchaseItemDTO
    {
        public int MedicineId { get; set; }
        public string BatchNo { get; set; }
        public DateTime MfgDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public string CreatedBy { get; set; }
    }
}
