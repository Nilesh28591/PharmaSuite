namespace PharmaSuiteMVC.Models
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public string? CustomerName { get; set; }
        public double TotalAmount { get; set; }
        public List<SaleItemDTO> SaleItems { get; set; }
    }
}
