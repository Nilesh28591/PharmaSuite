namespace PharmaSuiteWebAPI.Dto
{
    public class Manifacturer_Dto_F
    {
        public int ManId { get; set; }
        public string MName { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime? ModifyAt { get; set; }
    }
}
