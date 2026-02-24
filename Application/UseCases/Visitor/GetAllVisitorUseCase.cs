using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Visitor;
using Portly.Application.Ports.Input.Visitor;
using Portly.Application.Ports.Output;

namespace Portly.Application.UseCases.Visitor;

public sealed class GetAllVisitorsUseCase : IGetAllVisitorsUseCase
{
    private readonly IVisitorRepository _repository;
    private readonly ILogger<GetAllVisitorsUseCase> _logger;

    public GetAllVisitorsUseCase(IVisitorRepository repository, ILogger<GetAllVisitorsUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<VisitorOutput>> ExecuteAsync()
    {
        _logger.LogInformation("Buscando todos os visitantes");

        var visitors = await _repository.GetAllAsync();

        var result = visitors
            .Select(VisitorOutput.FromEntity)
            .ToList();

        _logger.LogInformation("Encontrados {Count} visitantes (ativos e excluídos)", result.Count);

        return result;
    }
}

