using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Visitor;
using Portly.Application.Ports.Input.Visitor;
using Portly.Application.Ports.Output;
using Portly.Domain.Entities;
using Portly.Domain.Exceptions;
using Portly.Domain.ValueObjects;

namespace Portly.Application.UseCases.Visitor;

public sealed class CreateVisitorUseCase : ICreateVisitorUseCase
{
    private readonly IVisitorRepository _repository;
    private readonly ILogger<CreateVisitorUseCase> _logger;

    public CreateVisitorUseCase(IVisitorRepository repository, ILogger<CreateVisitorUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<VisitorOutput> ExecuteAsync(CreateVisitorInput input)
    {
        _logger.LogInformation("Iniciando cria��o de visitante com email: {Email}", input.Email);

        var document = Document.Create(input.Document);

        if (await _repository.ExistsByDocumentAsync(document))
        {
            _logger.LogWarning("Tentativa de criar visitante com documento duplicado: {Document}", document.Value);
            throw new BusinessRuleException("J� existe um visitante cadastrado com este documento");
        }

        if (await _repository.ExistsByEmailAsync(input.Email))
        {
            _logger.LogWarning("Tentativa de criar visitante com email duplicado: {Email}", input.Email);
            throw new BusinessRuleException("J� existe um visitante cadastrado com este e-mail");
        }

        var visitor = Portly.Domain.Entities.Visitor.Create(
            Guid.NewGuid(),
            input.FullName,
            document,
            input.Phone,
            input.Email
        );

        await _repository.AddAsync(visitor);

        _logger.LogInformation("Visitante criado com sucesso. ID: {Id}, Email: {Email}", visitor.Id, visitor.Email);

        return VisitorOutput.FromEntity(visitor);
    }
}

