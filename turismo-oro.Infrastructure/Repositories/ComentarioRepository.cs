using turismo_oro.Application.Interfaces;
using turismo_oro.Domain.Entities;
using turismo_oro.Infrastructure.Data;

namespace turismo_oro.Infrastructure.Repositories;

public class ComentarioRepository : IComentarioRepository
{
    private readonly SqlDbContext _context;

    public ComentarioRepository(SqlDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ComentarioTurismo comentario, CancellationToken cancellationToken = default) =>
        await _context.ComentariosTurismo.AddAsync(comentario, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
