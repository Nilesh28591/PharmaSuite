namespace PharmaSuiteMVC.Models
{
    public class User
    {
        public int UserId { get; set; }     // Returned on successful login
        public string Username { get; set; }
        public string Password { get; set; } // Plain password for Login/Register
        public string Role { get; set; }
    }
}
