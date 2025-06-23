using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteMVC.Models
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
