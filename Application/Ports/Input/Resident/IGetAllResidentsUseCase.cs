using Portly.Application.DTOs.Resident;

namespace Portly.Application.Ports.Input.Resident;

public interface IGetAllResidentsUseCase
{
    Task<IEnumerable<ResidentOutput>> ExecuteAsync();
}
