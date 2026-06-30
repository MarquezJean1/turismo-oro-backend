using turismo_oro.Application.Dtos;
using turismo_oro.Domain.Entities;

namespace turismo_oro.Application.Interfaces;

public interface ITurismoLugarRepository
{
    Task<List<TurismoLugar>> GetAllAsync(string? busqueda, string? categoria, CancellationToken cancellationToken = default);
    Task<TurismoLugar?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(TurismoLugar lugar, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
