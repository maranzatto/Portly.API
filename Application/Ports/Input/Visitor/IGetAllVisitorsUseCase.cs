using Portly.Application.DTOs.Visitor;

namespace Portly.Application.Ports.Input.Visitor;

public interface IGetAllVisitorsUseCase
{
    Task<IEnumerable<VisitorOutput>> ExecuteAsync();
}

