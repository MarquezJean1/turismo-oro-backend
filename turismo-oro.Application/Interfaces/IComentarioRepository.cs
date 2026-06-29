using turismo_oro.Domain.Entities;

namespace turismo_oro.Application.Interfaces;

public interface IComentarioRepository
{
    Task AddAsync(ComentarioTurismo comentario, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
