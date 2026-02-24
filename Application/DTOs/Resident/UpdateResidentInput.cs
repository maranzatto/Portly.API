using System.ComponentModel.DataAnnotations;

namespace Portly.Application.DTOs.Resident;

public sealed class UpdateResidentInput
{
    [Required(ErrorMessage = "Nome completo é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
    public string FullName { get; init; } = default!;

    [Required(ErrorMessage = "Documento é obrigatório")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Documento deve ter entre 5 e 20 caracteres")]
    public string Document { get; init; } = default!;

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [Phone(ErrorMessage = "Telefone inválido")]
    public string Phone { get; init; } = default!;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; init; } = default!;

    [Required(ErrorMessage = "Apartamento é obrigatório")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "Apartamento deve ter entre 1 e 10 caracteres")]
    public string Apartment { get; init; } = default!;

    [Required(ErrorMessage = "Bloco é obrigatório")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "Bloco deve ter entre 1 e 10 caracteres")]
    public string Block { get; init; } = default!;
}
