namespace PharmaSuiteWebAPI.Dto
{
    public class PurchaseDTO
    {
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string CreatedBy { get; set; }
        public List<PurchaseItemDTO> Items { get; set; }
    }
}
