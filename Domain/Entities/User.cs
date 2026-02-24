using Portly.Domain.Exceptions;
using Portly.Domain.ValueObjects;

namespace Portly.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public UserRole Role { get; private set; } = UserRole.Porteiro;
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected User()
        {
        }

        public User(
            Guid id,
            string email,
            string passwordHash,
            UserRole role = UserRole.Porteiro)
        {
            if (id == Guid.Empty)
                throw new InvalidIdException(nameof(User));

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new InvalidUserEmailException();

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new InvalidUserPasswordException();

            if (!IsValidRole(role))
                throw new ArgumentException("Role inválida");

            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        private static bool IsValidRole(UserRole role)
        {
            return role == UserRole.Sindico || 
                   role == UserRole.Porteiro;
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new InvalidUserPasswordException();

            PasswordHash = newPasswordHash;
            Touch();
        }

        public void ChangeRole(UserRole newRole)
        {
            if (!IsValidRole(newRole))
                throw new ArgumentException("Role inválida");

            Role = newRole;
            Touch();
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new UserAlreadyInactiveException();

            IsActive = false;
            Touch();
        }

        public void Activate()
        {
            if (IsActive)
                throw new UserAlreadyActiveException();

            IsActive = true;
            Touch();
        }

        private void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}