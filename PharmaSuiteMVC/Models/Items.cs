namespace PharmaSuiteMVC.Models
{
    public class Items
    {
        public int ItemId { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
    }
}
