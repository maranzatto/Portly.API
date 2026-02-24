using Portly.Application.DTOs.Resident;

namespace Portly.Application.Ports.Input.Resident;

public interface IUpdateResidentUseCase
{
    Task<ResidentOutput> ExecuteAsync(Guid id, UpdateResidentInput input);
}
