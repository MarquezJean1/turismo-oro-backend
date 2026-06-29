using turismo_oro.Domain.Entities;

namespace turismo_oro.Application.Interfaces;

public interface IArchivoRepository
{
    Task AddAsync(Archivo archivo, CancellationToken cancellationToken = default);
    Task<Archivo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
