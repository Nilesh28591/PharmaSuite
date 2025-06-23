namespace PharmaSuiteMVC.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public string Name { get; set; } // Supplier Name
        public string Email { get; set; } // Supplier Email
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string CreatedBy { get; set; }
        public List<PurchaseItemDto> Items { get; set; }
    }
}
