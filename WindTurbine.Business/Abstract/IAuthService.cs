using WindTurbine.DTOs.Auth;
using WindTurbine.Entities;

namespace WindTurbine.Business.Abstract
{
    public interface IAuthService
    {
        User Register(UserRegisterDto registerDto, string password);
        User Login(UserLoginDto loginDto);
        bool UserExists(string email);
    }
}