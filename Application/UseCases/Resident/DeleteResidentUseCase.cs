using Microsoft.Extensions.Logging;
using Portly.Application.Ports.Input.Resident;
using Portly.Domain.Exceptions;

namespace Portly.Application.UseCases.Resident;

public sealed class DeleteResidentUseCase : IDeleteResidentUseCase
{
    private readonly IResidentRepository _repository;
    private readonly ILogger<DeleteResidentUseCase> _logger;

    public DeleteResidentUseCase(IResidentRepository repository, ILogger<DeleteResidentUseCase> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task ExecuteAsync(Guid residentId)
    {
        _logger.LogInformation("Iniciando exclusão do resident ID: {Id}", residentId);
        
        var resident = await _repository.GetByIdAsync(residentId)
            ?? throw new ResidentNotFoundException();

        resident.Delete();

        await _repository.UpdateAsync(resident);

        _logger.LogInformation("Resident excluído com sucesso. ID: {Id}", residentId);
    }
}
