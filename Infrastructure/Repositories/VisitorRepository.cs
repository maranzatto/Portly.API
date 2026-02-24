using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Portly.Application.Ports.Output;
using Portly.Domain.Entities;
using Portly.Domain.ValueObjects;
using Portly.Infrastructure.Data;

namespace Portly.Infrastructure.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly PortlyDbContext _context;
        private readonly ILogger<VisitorRepository> _logger;

        public VisitorRepository(PortlyDbContext context, ILogger<VisitorRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task AddAsync(Visitor visitor)
        {
            _logger.LogDebug("Adicionando novo visitante. ID: {Id}, Email: {Email}", visitor.Id, visitor.Email);
            
            await _context.Visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
            
            _logger.LogDebug("Visitante adicionado com sucesso. ID: {Id}", visitor.Id);
        }

        public async Task UpdateAsync(Visitor visitor)
        {
            _logger.LogDebug("Atualizando visitante. ID: {Id}", visitor.Id);
            
            _context.Visitors.Update(visitor);
            await _context.SaveChangesAsync();
            
            _logger.LogDebug("Visitante atualizado com sucesso. ID: {Id}", visitor.Id);
        }

        public async Task<Visitor?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Buscando visitante por ID: {Id}", id);
            
            var visitor = await _context.Visitors
                .FirstOrDefaultAsync(v => v.Id == id);
                
            _logger.LogDebug("Visitante {Found} para ID: {Id}", visitor != null ? "encontrado" : "não encontrado", id);
            
            return visitor;
        }

        public async Task<Visitor?> GetByIdIncludingDeletedAsync(Guid id)
        {
            _logger.LogDebug("Buscando visitante por ID incluindo excluídos: {Id}", id);
            
            var visitor = await _context.Visitors
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(v => v.Id == id);
                
            _logger.LogDebug("Visitante {Found} para ID: {Id} (incluindo excluídos)", visitor != null ? "encontrado" : "não encontrado", id);
            
            return visitor;
        }

        public async Task<IReadOnlyList<Visitor>> GetAllAsync()
        {
            _logger.LogDebug("Buscando todos os visitantes");
            
            var visitors = await _context.Visitors
                .AsNoTracking()
                .OrderBy(v => v.FullName)
                .ToListAsync();
                
            _logger.LogDebug("Encontrados {Count} visitantes no total", visitors.Count);
            
            return visitors;
        }

        public async Task<bool> ExistsByDocumentAsync(Document document)
        {
            _logger.LogDebug("Verificando existência de documento: {Document}", document.Value);
            
            var exists = await _context.Visitors
                .AnyAsync(v => v.Document == document);
                
            _logger.LogDebug("Documento {Document} {Exists}", document.Value, exists ? "existe" : "não existe");
            
            return exists;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            _logger.LogDebug("Verificando existência de email: {Email}", email);
            
            var exists = await _context.Visitors
                .AnyAsync(v => v.Email == email);
                
            _logger.LogDebug("Email {Email} {Exists}", email, exists ? "existe" : "não existe");
            
            return exists;
        }
    }
}

