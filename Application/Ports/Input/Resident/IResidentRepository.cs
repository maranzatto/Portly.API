using Portly.Domain.Entities;

namespace Portly.Application.Ports.Input.Resident;

public interface IResidentRepository
{
    Task<Portly.Domain.Entities.Resident?> GetByIdAsync(Guid id);
    Task<Portly.Domain.Entities.Resident?> GetByDocumentAsync(string document);
    Task<bool> ExistsByDocumentAsync(string document);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(Portly.Domain.Entities.Resident resident);
    Task UpdateAsync(Portly.Domain.Entities.Resident resident);
    Task<IEnumerable<Portly.Domain.Entities.Resident>> GetAllAsync();
    Task<IEnumerable<Portly.Domain.Entities.Resident>> GetActiveAsync();
}
