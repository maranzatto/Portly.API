using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Visitor;
using Portly.Application.Exceptions;
using Portly.Application.Ports.Input.Visitor;
using Portly.Application.Ports.Output;
using Portly.Domain.Exceptions;
using Portly.Domain.ValueObjects;

namespace Portly.Application.UseCases.Visitor;

public sealed class UpdateVisitorUseCase : IUpdateVisitorUseCase
{
    private readonly IVisitorRepository _repository;
    private readonly ILogger<UpdateVisitorUseCase> _logger;

    public UpdateVisitorUseCase(IVisitorRepository repository, ILogger<UpdateVisitorUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<VisitorOutput> ExecuteAsync(Guid id, UpdateVisitorInput input)
    {
        _logger.LogInformation("Iniciando atualização do visitante. ID: {Id}, NovoEmail: {Email}", id, input.Email);

        var visitor = await _repository.GetByIdAsync(id)
            ?? throw new VisitorNotFoundException(id);

        var document = Document.Create(input.Document);

        if (visitor.Document.Value != document.Value)
        {
            if (await _repository.ExistsByDocumentAsync(document))
            {
                _logger.LogWarning("Tentativa de atualizar visitante com documento duplicado: {Document}", document.Value); throw new BusinessRuleException("Já existe um visitante cadastrado com este documento");
            }
        }

        if (visitor.Email != input.Email)
        {
            if (await _repository.ExistsByEmailAsync(input.Email))
            {
                _logger.LogWarning("Tentativa de atualizar visitante com email duplicado: {Email}", input.Email); throw new BusinessRuleException("Já existe um visitante cadastrado com este e-mail");
            }
        }

        visitor.Update(
            input.FullName,
            document,
            input.Phone,
            input.Email
        );

        await _repository.UpdateAsync(visitor);

        _logger.LogInformation("Visitante atualizado com sucesso. ID: {Id}", id);

        return VisitorOutput.FromEntity(visitor);
    }
}
