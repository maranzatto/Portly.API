using Portly.Domain.Entities;
using Portly.Domain.ValueObjects;

namespace Portly.Application.Ports.Output
{
    public interface IVisitorRepository
    {
        Task AddAsync(Visitor visitor);
        Task UpdateAsync(Visitor visitor);

        Task<Visitor?> GetByIdAsync(Guid id);
        Task<Visitor?> GetByIdIncludingDeletedAsync(Guid id);
        Task<IReadOnlyList<Visitor>> GetAllAsync();

        Task<bool> ExistsByDocumentAsync(Document document);
        Task<bool> ExistsByEmailAsync(string email);
    }
}

