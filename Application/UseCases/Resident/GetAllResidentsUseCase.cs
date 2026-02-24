using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Resident;
using Portly.Application.Ports.Input.Resident;

namespace Portly.Application.UseCases.Resident;

public sealed class GetAllResidentsUseCase : IGetAllResidentsUseCase
{
    private readonly IResidentRepository _repository;
    private readonly ILogger<GetAllResidentsUseCase> _logger;

    public GetAllResidentsUseCase(IResidentRepository repository, ILogger<GetAllResidentsUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<ResidentOutput>> ExecuteAsync()
    {
        _logger.LogInformation("Buscando todos os residents");
        
        var residents = await _repository.GetAllAsync();
        
        _logger.LogInformation("Encontrados {Count} residents", residents.Count());
        return residents.Select(ResidentOutput.FromEntity);
    }
}
