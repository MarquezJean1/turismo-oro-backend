using Microsoft.EntityFrameworkCore;
using turismo_oro.Application.Interfaces;
using turismo_oro.Domain.Entities;
using turismo_oro.Infrastructure.Data;

namespace turismo_oro.Infrastructure.Repositories;

public class ArchivoRepository : IArchivoRepository
{
    private readonly SqlDbContext _context;

    public ArchivoRepository(SqlDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Archivo archivo, CancellationToken cancellationToken = default) =>
        await _context.Archivos.AddAsync(archivo, cancellationToken);

    public async Task<Archivo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Archivos.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
