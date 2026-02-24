using Portly.Application.Ports.Input;
using Portly.Application.Interfaces.Security;
using Portly.Domain.Entities;

namespace Portly.Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<string> ExecuteAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email é obrigatório.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Senha é obrigatória.");

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            if (!_passwordHasher.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            if (!user.IsActive)
                throw new InvalidOperationException("Usuário inativo.");

            return _tokenService.GenerateToken(user.Id, user.Email, user.Role.ToString());
        }
    }
}

