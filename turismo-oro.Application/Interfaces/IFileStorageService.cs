using turismo_oro.Application.Dtos;

namespace turismo_oro.Application.Interfaces;

public interface IFileStorageService
{
    Task<ArchivoDto> GuardarImagenAsync(Stream contenido, string nombreOriginal, CancellationToken cancellationToken = default);
}
