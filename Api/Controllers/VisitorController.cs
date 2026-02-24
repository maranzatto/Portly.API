using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portly.Application.DTOs.Visitor;
using Portly.Application.Ports.Input.Visitor;

namespace Portly.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/visitor")]
    public class VisitorController : ControllerBase
    {
        private readonly ICreateVisitorUseCase _createVisitorUseCase;
        private readonly IUpdateVisitorUseCase _updateVisitorUseCase;
        private readonly IGetVisitorByIdUseCase _getVisitorByIdUse;
        private readonly IGetAllVisitorsUseCase _getAllVisitorsUseCase;
        private readonly IDeleteVisitorUseCase _deleteVisitorUseCase;
        private readonly IRestoreVisitorUseCase _restoreVisitorUseCase;
        private readonly ILogger<VisitorController> _logger;

        public VisitorController(ICreateVisitorUseCase createVisitorUseCase, IUpdateVisitorUseCase updateVisitorUseCase, IGetVisitorByIdUseCase getVisitorByIdUse, IGetAllVisitorsUseCase getAllVisitorsUseCase, IDeleteVisitorUseCase deleteVisitorUseCase, IRestoreVisitorUseCase restoreVisitorUseCase, ILogger<VisitorController> logger)
        {
            _createVisitorUseCase = createVisitorUseCase ?? throw new ArgumentNullException(nameof(createVisitorUseCase));
            _updateVisitorUseCase = updateVisitorUseCase ?? throw new ArgumentNullException(nameof(updateVisitorUseCase));
            _getVisitorByIdUse = getVisitorByIdUse ?? throw new ArgumentNullException(nameof(getVisitorByIdUse));
            _getAllVisitorsUseCase = getAllVisitorsUseCase ?? throw new ArgumentNullException(nameof(getAllVisitorsUseCase));
            _deleteVisitorUseCase = deleteVisitorUseCase ?? throw new ArgumentNullException(nameof(deleteVisitorUseCase));
            _restoreVisitorUseCase = restoreVisitorUseCase ?? throw new ArgumentNullException(nameof(restoreVisitorUseCase));
            _logger = logger;
        }

        [HttpPost]
public async Task<IActionResult> Create([FromBody] CreateVisitorInput input)
        {
            _logger.LogInformation("Recebida requisição POST para criar visitante. Email: {Email}", input.Email);
            
            var result = await _createVisitorUseCase.ExecuteAsync(input);
            
            _logger.LogInformation("Visitante criado com sucesso. ID: {Id}", result.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVisitorInput input)
        {
            _logger.LogInformation("Recebida requisição PUT para atualizar visitante. ID: {Id}", id);
            
            var visitor = await _updateVisitorUseCase.ExecuteAsync(id, input);
            
            _logger.LogInformation("Visitante atualizado com sucesso. ID: {Id}", id);
            return Ok(visitor);
        }

        [HttpGet("{id:guid}")]
public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Recebida requisição GET para obter visitante. ID: {Id}", id);
            
            var visitor = await _getVisitorByIdUse.ExecuteAsync(id);
            
            _logger.LogInformation("Visitante encontrado com sucesso. ID: {Id}", id);
            return Ok(visitor);
        }

        [HttpGet]
public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Recebida requisição GET para listar visitantes");
            
            var visitors = await _getAllVisitorsUseCase.ExecuteAsync();
            
            _logger.LogInformation("Lista de visitantes retornada com sucesso");
            return Ok(visitors);
        }

        [HttpDelete("{id:guid}")]
public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Recebida requisição DELETE para excluir visitante. ID: {Id}", id);
            
            await _deleteVisitorUseCase.ExecuteAsync(id);
            
            _logger.LogInformation("Visitante excluído com sucesso. ID: {Id}", id);
            return NoContent();
        }

        [HttpPost("{id:guid}/restore")]
public async Task<IActionResult> Restore(Guid id)
        {
            _logger.LogInformation("Recebida requisição POST para restaurar visitante. ID: {Id}", id);
            
            await _restoreVisitorUseCase.ExecuteAsync(id);
            
            _logger.LogInformation("Visitante restaurado com sucesso. ID: {Id}", id);
            return NoContent();
        }
    }
}
