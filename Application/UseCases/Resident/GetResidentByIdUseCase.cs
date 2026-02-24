using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Resident;
using Portly.Application.Ports.Input.Resident;
using Portly.Domain.Exceptions;

namespace Portly.Application.UseCases.Resident;

public sealed class GetResidentByIdUseCase : IGetResidentByIdUseCase
{
    private readonly IResidentRepository _repository;
    private readonly ILogger<GetResidentByIdUseCase> _logger;

    public GetResidentByIdUseCase(IResidentRepository repository, ILogger<GetResidentByIdUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ResidentOutput> ExecuteAsync(Guid residentId)
    {
        _logger.LogInformation("Buscando resident por ID: {Id}", residentId);
        
        var resident = await _repository.GetByIdAsync(residentId)
            ?? throw new ResidentNotFoundException();

        _logger.LogInformation("Resident encontrado. ID: {Id}, Email: {Email}", resident.Id, resident.Email);
        return ResidentOutput.FromEntity(resident);
    }
}
