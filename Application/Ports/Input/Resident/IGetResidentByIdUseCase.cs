using Portly.Application.DTOs.Resident;

namespace Portly.Application.Ports.Input.Resident;

public interface IGetResidentByIdUseCase
{
    Task<ResidentOutput> ExecuteAsync(Guid residentId);
}
