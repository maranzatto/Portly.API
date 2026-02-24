namespace Portly.Application.Ports.Input.Resident;

public interface IDeleteResidentUseCase
{
    Task ExecuteAsync(Guid residentId);
}
