using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Visitor;
using Portly.Application.Exceptions;
using Portly.Application.Ports.Input.Visitor;
using Portly.Application.Ports.Output;
using Portly.Domain.Exceptions;

namespace Portly.Application.UseCases.Visitor;

public sealed class GetVisitorByIdUseCase : IGetVisitorByIdUseCase
{
    private readonly IVisitorRepository _repository;
    private readonly ILogger<GetVisitorByIdUseCase> _logger;

    public GetVisitorByIdUseCase(IVisitorRepository repository, ILogger<GetVisitorByIdUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<VisitorOutput> ExecuteAsync(Guid visitorId)
    {
        _logger.LogInformation("Buscando visitante por ID: {Id}", visitorId);

        var visitor = await _repository.GetByIdAsync(visitorId)
            ?? throw new VisitorNotFoundException(visitorId);

        _logger.LogInformation("Visitante encontrado. ID: {Id}, Email: {Email}", visitor.Id, visitor.Email);

        return VisitorOutput.FromEntity(visitor);
    }
}

