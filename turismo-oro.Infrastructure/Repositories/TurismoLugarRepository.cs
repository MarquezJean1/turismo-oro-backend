using Microsoft.EntityFrameworkCore;
using turismo_oro.Application.Interfaces;
using turismo_oro.Domain.Entities;
using turismo_oro.Infrastructure.Data;

namespace turismo_oro.Infrastructure.Repositories;

public class TurismoLugarRepository : ITurismoLugarRepository
{
    private readonly SqlDbContext _context;

    public TurismoLugarRepository(SqlDbContext context)
    {
        _context = context;
    }

    public async Task<List<TurismoLugar>> GetAllAsync(
        string? busqueda,
        string? categoria,
        CancellationToken cancellationToken = default)
    {
        var query = _context.TurismoLugares
            .AsNoTracking()
            .Include(t => t.TurismoArchivos)
                .ThenInclude(ta => ta.Archivo)
            .Include(t => t.Comentarios)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            var termino = busqueda.Trim().ToLower();
            query = query.Where(t =>
                t.Nombre.ToLower().Contains(termino) ||
                t.Categoria.ToLower().Contains(termino) ||
                t.Descripcion.ToLower().Contains(termino) ||
                t.Direccion.ToLower().Contains(termino));
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            var cat = categoria.Trim().ToLower();
            query = query.Where(t => t.Categoria.ToLower().Contains(cat));
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<TurismoLugar?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.TurismoLugares
            .Include(t => t.TurismoArchivos)
                .ThenInclude(ta => ta.Archivo)
            .Include(t => t.Comentarios)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task AddAsync(TurismoLugar lugar, CancellationToken cancellationToken = default) =>
        await _context.TurismoLugares.AddAsync(lugar, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
