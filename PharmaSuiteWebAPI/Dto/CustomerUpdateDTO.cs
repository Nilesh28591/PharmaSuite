namespace PharmaSuiteWebAPI.DTO
{
    public class CustomerUpdateDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }       // Add Email
        public bool IsOnline { get; set; }      // Add IsOnline

        public string? UpdatedBy { get; set; }
    }
}
