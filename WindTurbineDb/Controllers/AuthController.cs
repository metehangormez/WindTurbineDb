using Microsoft.AspNetCore.Mvc;
using WindTurbine.Business.Abstract;
using WindTurbine.DTOs.Auth;

namespace WindTurbineDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            if (_authService.UserExists(userRegisterDto.Email))
                return BadRequest("Bu e-posta adresi zaten kayıtlı.");

            var user = _authService.Register(userRegisterDto, userRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var user = _authService.Login(userLoginDto);
            if (user == null)
                return Unauthorized("E-posta veya şifre hatalı.");

            // Başarılı girişte basit bir mesaj veya kullanıcı bilgisi dönüyoruz
            return Ok(new { Message = "Giriş Başarılı", UserId = user.UserId, FullName = user.FullName });
        }
    }
}