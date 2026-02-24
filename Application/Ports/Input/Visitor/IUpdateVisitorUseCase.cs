using Portly.Application.DTOs.Visitor;

namespace Portly.Application.Ports.Input.Visitor;

public interface IUpdateVisitorUseCase
{
    Task<VisitorOutput> ExecuteAsync(Guid id, UpdateVisitorInput input);
}

