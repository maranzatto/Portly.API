using Portly.Application.DTOs.Resident;

namespace Portly.Application.Ports.Input.Resident;

public interface ICreateResidentUseCase
{
    Task<ResidentOutput> ExecuteAsync(CreateResidentInput input);
}
