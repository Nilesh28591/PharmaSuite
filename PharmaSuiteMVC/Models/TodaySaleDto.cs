namespace PharmaSuiteMVC.Models
{
    public class TodaySaleDto
    {
        public DateTime SaleDate { get; set; }
        public string? CustomerName { get; set; }
        public double TotalAmount { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
    }
}
