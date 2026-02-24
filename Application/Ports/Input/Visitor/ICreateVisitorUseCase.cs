using Portly.Application.DTOs.Visitor;

namespace Portly.Application.Ports.Input.Visitor;

public interface ICreateVisitorUseCase
{
    Task<VisitorOutput> ExecuteAsync(CreateVisitorInput input);
}

