using Portly.Application.DTOs.Visitor;

namespace Portly.Application.Ports.Input.Visitor;

public interface IGetVisitorByIdUseCase
{
    Task<VisitorOutput> ExecuteAsync(Guid visitorId);
}

