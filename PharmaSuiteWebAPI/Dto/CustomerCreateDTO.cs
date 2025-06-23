namespace PharmaSuiteWebAPI.Dto
{
    public class CustomerCreateDTO
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }         // Added Email
        public bool IsOnline { get; set; }        // Added IsOnline

        public string? CreatedBy { get; set; }
    }
}
