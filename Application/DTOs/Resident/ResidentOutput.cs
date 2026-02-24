using Portly.Domain.Entities;

namespace Portly.Application.DTOs.Resident;

public sealed class ResidentOutput
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = default!;
    public string Document { get; init; } = default!;
    public string Phone { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Apartment { get; init; } = default!;
    public string Block { get; init; } = default!;
    public bool IsDeleted { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public static ResidentOutput FromEntity(Portly.Domain.Entities.Resident resident)
    {
        return new ResidentOutput
        {
            Id = resident.Id,
            FullName = resident.FullName,
            Document = resident.Document.Value,
            Phone = resident.Phone,
            Email = resident.Email,
            Apartment = resident.Apartment,
            Block = resident.Block,
            IsDeleted = resident.IsDeleted,
            CreatedAt = resident.CreatedAt,
            UpdatedAt = resident.UpdatedAt
        };
    }
}
