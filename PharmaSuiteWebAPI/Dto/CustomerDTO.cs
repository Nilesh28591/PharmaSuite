using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteWebAPI.Dto
{
    public class CustomerDTO
    {
        public string Name { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }  // Use Mobile to match your model

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
