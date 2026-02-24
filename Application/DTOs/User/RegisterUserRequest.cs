using Portly.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Portly.Application.DTOs.User
{
    public record RegisterUserRequest(
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        string Email,

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(8, ErrorMessage = "Senha deve ter pelo menos 8 caracteres")]
        string Password,

        UserRole? Role
    );
}
