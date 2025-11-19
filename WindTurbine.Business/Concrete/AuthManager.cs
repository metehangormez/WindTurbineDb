using System.Security.Cryptography;
using System.Text;
using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DTOs.Auth;
using WindTurbine.Entities;

namespace WindTurbine.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Register(UserRegisterDto registerDto, string password)
        {
            // Şifreyi Hashle
            string passwordHash = CreateHash(password);

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Role = "User" // Varsayılan rol
            };

            _userRepository.Add(user);
            return user;
        }

        public User Login(UserLoginDto loginDto)
        {
            var user = _userRepository.GetByEmail(loginDto.Email);
            if (user == null) return null; // Kullanıcı yok

            // Şifre kontrolü
            string girilenSifreHash = CreateHash(loginDto.Password);
            if (user.PasswordHash != girilenSifreHash) return null; // Şifre yanlış

            return user;
        }

        public bool UserExists(string email)
        {
            return _userRepository.GetByEmail(email) != null;
        }

        // Basit Hash Fonksiyonu (SHA256)
        private string CreateHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes) builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}