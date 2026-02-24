using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Resident;
using Portly.Application.Ports.Input.Resident;
using Portly.Domain.Entities;
using Portly.Domain.Exceptions;
using Portly.Domain.ValueObjects;

namespace Portly.Application.UseCases.Resident;

public sealed class CreateResidentUseCase : ICreateResidentUseCase
{
    private readonly IResidentRepository _repository;
    private readonly ILogger<CreateResidentUseCase> _logger;

    public CreateResidentUseCase(IResidentRepository repository, ILogger<CreateResidentUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ResidentOutput> ExecuteAsync(CreateResidentInput input)
    {
        _logger.LogInformation("Iniciando criação de resident com email: {Email}", input.Email);
        
        var document = Document.Create(input.Document);

        if (await _repository.ExistsByDocumentAsync(document.Value))
        {
            _logger.LogWarning("Tentativa de criar resident com documento duplicado: {Document}", document.Value);
            throw new BusinessRuleException("Já existe um resident cadastrado com este documento");
        }

        if (await _repository.ExistsByEmailAsync(input.Email))
        {
            _logger.LogWarning("Tentativa de criar resident com email duplicado: {Email}", input.Email);
            throw new BusinessRuleException("Já existe um resident cadastrado com este e-mail");
        }

        var resident = Portly.Domain.Entities.Resident.Create(
            Guid.NewGuid(),
            input.FullName,
            document,
            input.Phone,
            input.Email,
            input.Apartment,
            input.Block
        );

        await _repository.AddAsync(resident);

        _logger.LogInformation("Resident criado com sucesso. ID: {Id}, Email: {Email}", resident.Id, resident.Email);
        return ResidentOutput.FromEntity(resident);
    }
}
