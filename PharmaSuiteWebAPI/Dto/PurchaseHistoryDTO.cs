namespace PharmaSuiteWebAPI.Dto
{
    public class PurchaseHistoryDTO
    {
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string MedicineName { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

    }
}
