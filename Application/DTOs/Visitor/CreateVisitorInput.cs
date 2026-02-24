using System.ComponentModel.DataAnnotations;

namespace Portly.Application.DTOs.Visitor;

public sealed class CreateVisitorInput
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
}

