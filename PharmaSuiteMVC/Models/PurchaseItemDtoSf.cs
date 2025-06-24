namespace PharmaSuiteMVC.Models
{
    public class PurchaseItemDtoSf
    {
        public int PurchaseItemId { get; set; }
        public int MedicineId { get; set; }
        public string? BatchNo { get; set; }
        public string? MedicineName { get; set; }

        
        public DateTime MfgDate { get; set; }
       
        public DateTime ExpiryDate { get; set; }
       
        public int Quantity { get; set; }
     
        public decimal CostPrice { get; set; }
        public int MinQuantity { get; set; }
    }
}
