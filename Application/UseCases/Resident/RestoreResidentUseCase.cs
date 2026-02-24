using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Resident;
using Portly.Application.Ports.Input.Resident;
using Portly.Domain.Exceptions;

namespace Portly.Application.UseCases.Resident;

public sealed class RestoreResidentUseCase : IRestoreResidentUseCase
{
    private readonly IResidentRepository _repository;
    private readonly ILogger<RestoreResidentUseCase> _logger;

    public RestoreResidentUseCase(IResidentRepository repository, ILogger<RestoreResidentUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ResidentOutput> ExecuteAsync(Guid residentId)
    {
        _logger.LogInformation("Iniciando restauração do resident ID: {Id}", residentId);
        
        var resident = await _repository.GetByIdAsync(residentId)
            ?? throw new ResidentNotFoundException();

        if (!resident.IsDeleted)
        {
            _logger.LogWarning("Tentativa de restaurar resident que não está excluído. ID: {Id}", residentId);
            throw new ResidentAlreadyRestoredException();
        }

        resident.Restore();

        await _repository.UpdateAsync(resident);

        _logger.LogInformation("Resident restaurado com sucesso. ID: {Id}", residentId);
        return ResidentOutput.FromEntity(resident);
    }
}
