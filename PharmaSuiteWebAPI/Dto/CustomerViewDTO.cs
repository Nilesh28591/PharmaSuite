namespace PharmaSuiteWebAPI.DTO
{
    public class CustomerViewDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }         // Added Email
        public bool IsOnline { get; set; }        // Added IsOnline

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
