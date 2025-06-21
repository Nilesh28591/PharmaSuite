namespace PharmaSuiteMVC.Models
{
    public class Category_Dto
    {
        public int catId { get; set; }
        public string categoryName { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string? modifyBy { get; set; }
        public DateTime? modifyAt { get; set; }
        public DateTime createdAt { get; set; }
    }
}
