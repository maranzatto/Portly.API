using Portly.Application.DTOs.User;
using Portly.Application.Ports.Input;
using Portly.Application.Interfaces.Security;
using Portly.Domain.Entities;
using Portly.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Portly.Application.UseCases.Auth
{
    public class RegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task ExecuteAsync(RegisterUserRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email é obrigatório.");

            if (!IsValidEmail(request.Email))
                throw new ArgumentException("Email inválido.");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Senha é obrigatória.");

            if (request.Password.Length < 8)
                throw new ArgumentException("Senha deve ter pelo menos 8 caracteres.");

            var exists = await _userRepository.ExistsByEmailAsync(request.Email);

            if (exists)
                throw new InvalidOperationException("Email já cadastrado.");

            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = new User(
                Guid.NewGuid(),
                request.Email,
                passwordHash,
                request.Role.HasValue ? request.Role.Value : UserRole.Porteiro
            );

            await _userRepository.AddAsync(user);
        }

        private static bool IsValidEmail(string email)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }
    }
}
