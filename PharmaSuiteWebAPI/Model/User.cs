namespace PharmaSuiteWebAPI.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
//p
