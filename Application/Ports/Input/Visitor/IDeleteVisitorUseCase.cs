namespace Portly.Application.Ports.Input.Visitor;

public interface IDeleteVisitorUseCase
{
    Task ExecuteAsync(Guid visitorId);
}

