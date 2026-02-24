using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Visitor;
using Portly.Application.Exceptions;
using Portly.Application.Ports.Input.Visitor;
using Portly.Application.Ports.Output;
using Portly.Domain.Exceptions;

namespace Portly.Application.UseCases.Visitor;

public sealed class RestoreVisitorUseCase : IRestoreVisitorUseCase
{
    private readonly IVisitorRepository _repository;
    private readonly ILogger<RestoreVisitorUseCase> _logger;

    public RestoreVisitorUseCase(IVisitorRepository repository, ILogger<RestoreVisitorUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task ExecuteAsync(Guid visitorId)
    {
        _logger.LogInformation("Iniciando restaura��o do visitante. ID: {Id}", visitorId);

        var visitor = await _repository.GetByIdAsync(visitorId)
            ?? throw new VisitorNotFoundException(visitorId);

        visitor.Restore();

        await _repository.UpdateAsync(visitor);

        _logger.LogInformation("Visitante restaurado com sucesso. ID: {Id}", visitorId);
    }
}

