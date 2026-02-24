using Portly.Domain.Exceptions;
using Portly.Domain.ValueObjects;

namespace Portly.Domain.Entities;

public class Resident
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = default!;
    public Document Document { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Apartment { get; private set; } = default!;
    public string Block { get; private set; } = default!;
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public virtual ICollection<Visitor> Visitors { get; private set; } = new List<Visitor>();

    protected Resident() { }

    private Resident(
        Guid id,
        string fullName,
        Document document,
        string phone,
        string email,
        string apartment,
        string block)
    {
        Id = id;
        ApplyValidation(fullName, document, phone, email, apartment, block);

        FullName = fullName.Trim();
        Document = document;
        Phone = phone.Trim();
        Email = email.Trim();
        Apartment = apartment.Trim();
        Block = block.Trim();

        CreatedAt = UpdatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public static Resident Create(
        Guid id,
        string fullName,
        Document document,
        string phone,
        string email,
        string apartment,
        string block)
    {
        if (id == Guid.Empty)
            throw new BusinessRuleException("ID de resident inválido");

        return new Resident(id, fullName, document, phone, email, apartment, block);
    }

    public void Update(
        string fullName,
        Document document,
        string phone,
        string email,
        string apartment,
        string block)
    {
        if (IsDeleted)
            throw new BusinessRuleException("Resident foi excluído e não pode ser alterado");

        ApplyValidation(fullName, document, phone, email, apartment, block);

        FullName = fullName.Trim();
        Document = document;
        Phone = phone.Trim();
        Email = email.Trim();
        Apartment = apartment.Trim();
        Block = block.Trim();

        Touch();
    }

    public void Delete()
    {
        if (IsDeleted)
            return;

        IsDeleted = true;
        Touch();
    }

    public void Restore()
    {
        if (!IsDeleted)
            return;

        IsDeleted = false;
        Touch();
    }

    private void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ApplyValidation(
        string fullName,
        Document document,
        string phone,
        string email,
        string apartment,
        string block)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BusinessRuleException("O nome completo é obrigatório");
        if (fullName.Length < 3)
            throw new BusinessRuleException("O nome completo deve ter pelo menos 3 caracteres");
        if (document is null)
            throw new BusinessRuleException("O documento é obrigatório");
        if (string.IsNullOrWhiteSpace(phone))
            throw new BusinessRuleException("O telefone é obrigatório");
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new BusinessRuleException("O e-mail informado é inválido");
        if (string.IsNullOrWhiteSpace(apartment))
            throw new BusinessRuleException("O apartamento é obrigatório");
        if (string.IsNullOrWhiteSpace(block))
            throw new BusinessRuleException("O bloco é obrigatório");
    }
}
