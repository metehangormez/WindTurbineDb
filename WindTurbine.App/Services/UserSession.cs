namespace WindTurbine.App.Services
{
    // Bu sınıf, uygulama açık kaldığı sürece
    // giriş yapan kullanıcının bilgilerini hafızada tutar.
    public class UserSession
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "Saha Mühendisi";
    }
}