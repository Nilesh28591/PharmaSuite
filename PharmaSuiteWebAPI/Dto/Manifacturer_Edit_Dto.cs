namespace PharmaSuiteWebAPI.Dto
{
    public class Manifacturer_Edit_Dto
    {
        public int ManId { get; set; }
        public string MName { get; set; }
        public string Status { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime? ModifyAt { get; set; }
    }
}
