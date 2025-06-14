using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteWebAPI.Model
{
    public class Category
    {

        [Key]
        public int CatId { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime? ModifyAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
