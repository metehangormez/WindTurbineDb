namespace WindTurbine.App.Services
{
    
    public class UserSession
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "Saha Mühendisi";
    }
}