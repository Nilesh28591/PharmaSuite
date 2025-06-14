using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteWebAPI.Model
{
    public class Manifacturer_Medicine
    {
        [Key]
        public int ManId { get; set; }
        public string MName { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime? ModifyAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
