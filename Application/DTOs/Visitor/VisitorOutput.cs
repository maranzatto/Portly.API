namespace Portly.Application.DTOs.Visitor;

public sealed class VisitorOutput
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = default!;
    public string Document { get; init; } = default!;
    public string Phone { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool IsDeleted { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public static VisitorOutput FromEntity(Portly.Domain.Entities.Visitor visitor)
    {
        return new VisitorOutput
        {
            Id = visitor.Id,
            FullName = visitor.FullName,
            Document = visitor.Document.Value,
            Phone = visitor.Phone,
            Email = visitor.Email,
            IsDeleted = visitor.IsDeleted,
            CreatedAt = visitor.CreatedAt,
            UpdatedAt = visitor.UpdatedAt
        };
    }
}

