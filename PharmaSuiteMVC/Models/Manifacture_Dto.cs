namespace PharmaSuiteMVC.Models
{
    public class Manifacture_Dto
    {
        public int manId { get; set; }
        public string mName { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public DateTime createdAt { get; set; }
        public string? modifyBy { get; set; }
        public DateTime? modifyAt { get; set; }
    }
}
