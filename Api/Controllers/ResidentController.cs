using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portly.Application.Attributes;
using Portly.Application.DTOs.Resident;
using Portly.Application.Ports.Input.Resident;
using Portly.Domain.ValueObjects;

namespace Portly.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/resident")]
public class ResidentController : ControllerBase
{
    private readonly ICreateResidentUseCase _createResidentUseCase;
    private readonly IUpdateResidentUseCase _updateResidentUseCase;
    private readonly IGetResidentByIdUseCase _getResidentByIdUse;
    private readonly IGetAllResidentsUseCase _getAllResidentsUseCase;
    private readonly IDeleteResidentUseCase _deleteResidentUseCase;
    private readonly IRestoreResidentUseCase _restoreResidentUseCase;
    private readonly ILogger<ResidentController> _logger;

    public ResidentController(
        ICreateResidentUseCase createResidentUseCase,
        IUpdateResidentUseCase updateResidentUseCase,
        IGetResidentByIdUseCase getResidentByIdUse,
        IGetAllResidentsUseCase getAllResidentsUseCase,
        IDeleteResidentUseCase deleteResidentUseCase,
        IRestoreResidentUseCase restoreResidentUseCase,
        ILogger<ResidentController> logger)
    {
        _createResidentUseCase = createResidentUseCase ?? throw new ArgumentNullException(nameof(createResidentUseCase));
        _updateResidentUseCase = updateResidentUseCase ?? throw new ArgumentNullException(nameof(updateResidentUseCase));
        _getResidentByIdUse = getResidentByIdUse ?? throw new ArgumentNullException(nameof(getResidentByIdUse));
        _getAllResidentsUseCase = getAllResidentsUseCase ?? throw new ArgumentNullException(nameof(getAllResidentsUseCase));
        _deleteResidentUseCase = deleteResidentUseCase ?? throw new ArgumentNullException(nameof(deleteResidentUseCase));
        _restoreResidentUseCase = restoreResidentUseCase ?? throw new ArgumentNullException(nameof(restoreResidentUseCase));
        _logger = logger;
    }

    [HttpPost]
    [AuthorizeRole(UserRole.Sindico)]
    public async Task<IActionResult> Create([FromBody] CreateResidentInput input)
    {
        _logger.LogInformation("Recebida requisição POST para criar resident. Email: {Email}", input.Email);
        var result = await _createResidentUseCase.ExecuteAsync(input);

        _logger.LogInformation("Resident criado com sucesso. ID: {Id}", result.Id);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [AuthorizeRole(UserRole.Sindico)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResidentInput input)
    {
        _logger.LogInformation("Recebida requisição PUT para atualizar resident. ID: {Id}", id);
        var resident = await _updateResidentUseCase.ExecuteAsync(id, input);

        _logger.LogInformation("Resident atualizado com sucesso. ID: {Id}", id);
        return Ok(resident);
    }

    [HttpGet]
    [AuthorizeRole(UserRole.Sindico, UserRole.Porteiro)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Recebida requisição GET para listar residents");
        var residents = await _getAllResidentsUseCase.ExecuteAsync();

        _logger.LogInformation("Lista de residents retornada com sucesso");
        return Ok(residents);
    }

    [HttpGet("{id:guid}")]
    [AuthorizeRole(UserRole.Sindico, UserRole.Porteiro)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Recebida requisição GET para obter resident. ID: {Id}", id);
        var resident = await _getResidentByIdUse.ExecuteAsync(id);

        _logger.LogInformation("Resident encontrado com sucesso. ID: {Id}", id);
        return Ok(resident);
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeRole(UserRole.Sindico)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Recebida requisição DELETE para excluir resident. ID: {Id}", id);
        await _deleteResidentUseCase.ExecuteAsync(id);

        _logger.LogInformation("Resident excluído com sucesso. ID: {Id}", id);
        return NoContent();
    }

    [HttpPost("{id:guid}/restore")]
    [AuthorizeRole(UserRole.Sindico)]
    public async Task<IActionResult> Restore(Guid id)
    {
        _logger.LogInformation("Recebida requisição POST para restaurar resident. ID: {Id}", id);
        await _restoreResidentUseCase.ExecuteAsync(id);

        _logger.LogInformation("Resident restaurado com sucesso. ID: {Id}", id);
        return NoContent();
    }
}
