using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Portly.Application.Ports.Input.Resident;
using Portly.Domain.Entities;
using Portly.Domain.ValueObjects;
using Portly.Infrastructure.Data;

namespace Portly.Infrastructure.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly PortlyDbContext _context;
        private readonly ILogger<ResidentRepository> _logger;

        public ResidentRepository(PortlyDbContext context, ILogger<ResidentRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task AddAsync(Portly.Domain.Entities.Resident resident)
        {
            _logger.LogDebug("Adicionando novo resident. ID: {Id}, Email: {Email}", resident.Id, resident.Email);
            await _context.Residents.AddAsync(resident);
            await _context.SaveChangesAsync();

            _logger.LogDebug("Resident adicionado com sucesso. ID: {Id}", resident.Id);
        }

        public async Task UpdateAsync(Portly.Domain.Entities.Resident resident)
        {
            _logger.LogDebug("Atualizando resident. ID: {Id}", resident.Id);
            _context.Residents.Update(resident);
            await _context.SaveChangesAsync();

            _logger.LogDebug("Resident atualizado com sucesso. ID: {Id}", resident.Id);
        }

        public async Task<Portly.Domain.Entities.Resident?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Buscando resident por ID: {Id}", id);

            var resident = await _context.Residents
                .FirstOrDefaultAsync(r => r.Id == id);

            _logger.LogDebug("Resident {Found} para ID: {Id}", resident != null ? "encontrado" : "não encontrado", id);
            return resident;
        }

        public async Task<Portly.Domain.Entities.Resident?> GetByDocumentAsync(string document)
        {
            _logger.LogDebug("Buscando resident por documento: {Document}", document);

            var documentVo = Document.Create(document);

            var resident = await _context.Residents
                .FirstOrDefaultAsync(r => r.Document == documentVo);

            _logger.LogDebug("Resident {Found} para documento: {Document}", resident != null ? "encontrado" : "não encontrado", document);
            return resident;
        }

        public async Task<IEnumerable<Portly.Domain.Entities.Resident>> GetAllAsync()
        {
            _logger.LogDebug("Buscando todos os residents");

            var residents = await _context.Residents
                .AsNoTracking()
                .OrderBy(r => r.FullName)
                .ToListAsync();

            _logger.LogDebug("Encontrados {Count} residents no total", residents.Count);
            return residents;
        }

        public async Task<IEnumerable<Portly.Domain.Entities.Resident>> GetActiveAsync()
        {
            _logger.LogDebug("Buscando residents ativos");

            var residents = await _context.Residents
                .AsNoTracking()
                .Where(r => !r.IsDeleted)
                .OrderBy(r => r.FullName)
                .ToListAsync();

            _logger.LogDebug("Encontrados {Count} residents ativos", residents.Count);
            return residents;
        }

        public async Task<bool> ExistsByDocumentAsync(string document)
        {
            _logger.LogDebug("Verificando existência de documento: {Document}", document);

            var documentVo = Document.Create(document);
            var exists = await _context.Residents
                .AnyAsync(r => r.Document == documentVo);

            _logger.LogDebug("Documento {Document} {Exists}", document, exists ? "existe" : "não existe");
            return exists;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            _logger.LogDebug("Verificando existência de email: {Email}", email);
            var exists = await _context.Residents
                .AnyAsync(r => r.Email == email);

            _logger.LogDebug("Email {Email} {Exists}", email, exists ? "existe" : "não existe");
            return exists;
        }
    }
}
