namespace PharmaSuiteMVC.Models
{
    public class InvoiceDTO
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public string? CustomerName { get; set; }
        public double TotalAmount { get; set; }
        public List<InvoiceItemDTO> SaleItems { get; set; }
    }
}
