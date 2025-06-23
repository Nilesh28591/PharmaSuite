namespace PharmaSuiteMVC.Models
{
    public class InvoiceItemDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double? Discount { get; set; }
    }
}
