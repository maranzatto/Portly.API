using Microsoft.AspNetCore.Mvc;
using Portly.Application.DTOs.User;
using Portly.Application.UseCases.Auth;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Portly.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterUserUseCase _registerUserUseCase;

        public AuthController(LoginUseCase loginUseCase, RegisterUserUseCase registerUserUseCase)
        {
            _loginUseCase = loginUseCase ?? throw new ArgumentNullException(nameof(loginUseCase));
            _registerUserUseCase = registerUserUseCase ?? throw new ArgumentNullException(nameof(registerUserUseCase));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _loginUseCase.ExecuteAsync(
                request.Email,
                request.Password
            );

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Email),
                new Claim("Token", token)
            };

            if (!string.IsNullOrWhiteSpace(role))
                claims.Add(new Claim(ClaimTypes.Role, role));

            if (!string.IsNullOrWhiteSpace(userId))
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok(new { 
                Success = true,
                Message = "Login realizado com sucesso"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            await _registerUserUseCase.ExecuteAsync(request);
            
            return Ok(new { 
                Success = true,
                Message = "Usuário criado com sucesso"
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return Ok(new { 
                Success = true,
                Message = "Logout realizado com sucesso"
            });
        }
    }

    public record LoginRequest(
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        string Email,

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(8, ErrorMessage = "Senha deve ter pelo menos 8 caracteres")]
        string Password
    );
}
