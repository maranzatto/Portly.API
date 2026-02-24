using Microsoft.Extensions.Logging;
using Portly.Application.Exceptions;
using Portly.Application.Ports.Input.Visitor;
using Portly.Application.Ports.Output;

namespace Portly.Application.UseCases.Visitor
{
    public sealed class DeleteVisitorUseCase : IDeleteVisitorUseCase
    {
        private readonly IVisitorRepository _repository;
        private readonly ILogger<DeleteVisitorUseCase> _logger;

        public DeleteVisitorUseCase(IVisitorRepository repository, ILogger<DeleteVisitorUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task ExecuteAsync(Guid id)
        {
            _logger.LogInformation("Iniciando exclus�o do visitante. ID: {Id}", id);

            var visitor = await _repository.GetByIdAsync(id)
                ?? throw new VisitorNotFoundException(id);

            visitor.Delete();

            await _repository.UpdateAsync(visitor);

            _logger.LogInformation("Visitante exclu�do com sucesso. ID: {Id}", id);
        }
    }
}

