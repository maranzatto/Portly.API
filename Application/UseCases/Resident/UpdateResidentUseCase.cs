using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Resident;
using Portly.Application.Ports.Input.Resident;
using Portly.Domain.Entities;
using Portly.Domain.Exceptions;
using Portly.Domain.ValueObjects;

namespace Portly.Application.UseCases.Resident;

public sealed class UpdateResidentUseCase : IUpdateResidentUseCase
{
    private readonly IResidentRepository _repository;
    private readonly ILogger<UpdateResidentUseCase> _logger;

    public UpdateResidentUseCase(IResidentRepository repository, ILogger<UpdateResidentUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ResidentOutput> ExecuteAsync(Guid id, UpdateResidentInput input)
    {
        _logger.LogInformation("Iniciando atualização do resident ID: {Id}", id);
        
        var resident = await _repository.GetByIdAsync(id)
            ?? throw new ResidentNotFoundException();

        var document = Document.Create(input.Document);

        var existingResidentByDocument = await _repository.GetByDocumentAsync(document.Value);
        if (existingResidentByDocument != null && existingResidentByDocument.Id != id)
        {
            _logger.LogWarning("Tentativa de atualizar resident com documento duplicado: {Document}", document.Value);
            throw new BusinessRuleException("Já existe outro resident cadastrado com este documento");
        }

        var existingResidentByEmail = await _repository.GetByIdAsync(id);
        if (existingResidentByEmail != null && existingResidentByEmail.Email != input.Email && 
            await _repository.ExistsByEmailAsync(input.Email))
        {
            _logger.LogWarning("Tentativa de atualizar resident com email duplicado: {Email}", input.Email);
            throw new BusinessRuleException("Já existe outro resident cadastrado com este e-mail");
        }

        resident.Update(
            input.FullName,
            document,
            input.Phone,
            input.Email,
            input.Apartment,
            input.Block
        );

        await _repository.UpdateAsync(resident);

        _logger.LogInformation("Resident atualizado com sucesso. ID: {Id}, Email: {Email}", resident.Id, resident.Email);
        return ResidentOutput.FromEntity(resident);
    }
}
