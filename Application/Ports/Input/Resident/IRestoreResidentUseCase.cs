using Portly.Application.DTOs.Resident;

namespace Portly.Application.Ports.Input.Resident;

public interface IRestoreResidentUseCase
{
    Task<ResidentOutput> ExecuteAsync(Guid residentId);
}
