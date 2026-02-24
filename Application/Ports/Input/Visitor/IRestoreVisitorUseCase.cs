namespace Portly.Application.Ports.Input.Visitor;

public interface IRestoreVisitorUseCase
{
    Task ExecuteAsync(Guid visitorId);
}

