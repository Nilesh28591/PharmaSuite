namespace PharmaSuiteMVC.Models
{
    public class Medicine_Management
    {
        public int medicineId { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string manufacturer { get; set; }
        public int pricePerUnit { get; set; }
        public string BatchNo { get; set; }
        public DateTime expiryDate { get; set; }
    }
}
