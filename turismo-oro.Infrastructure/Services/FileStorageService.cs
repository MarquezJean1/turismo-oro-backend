using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using turismo_oro.Application.Dtos;
using turismo_oro.Application.Interfaces;
using turismo_oro.Domain.Entities;
using turismo_oro.Domain.Enums;
using turismo_oro.Infrastructure.Data;

namespace turismo_oro.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private static readonly HashSet<string> ExtensionesPermitidas = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".gif"
    };

    private readonly TurismoDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _uploadFolder;
    private readonly long _maxBytes;

    public FileStorageService(
        TurismoDbContext context,
        IWebHostEnvironment environment,
        IConfiguration configuration)
    {
        _context = context;
        _environment = environment;
        _uploadFolder = configuration["FileStorage:UploadFolder"] ?? "uploads/turismo";
        _maxBytes = configuration.GetValue<long?>("FileStorage:MaxBytes") ?? 5 * 1024 * 1024;
    }

    public async Task<ArchivoDto> GuardarImagenAsync(
        Stream contenido,
        string nombreOriginal,
        CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(nombreOriginal);
        if (string.IsNullOrWhiteSpace(extension) || !ExtensionesPermitidas.Contains(extension))
            throw new InvalidOperationException("Formato de imagen no permitido. Use JPG, PNG, WEBP o GIF.");

        if (contenido.CanSeek)
        {
            if (contenido.Length > _maxBytes)
                throw new InvalidOperationException($"La imagen supera el tamaño máximo de {_maxBytes / (1024 * 1024)} MB.");
        }

        var archivoId = Guid.NewGuid();
        var nombreAlmacenado = $"{archivoId}{extension.ToLowerInvariant()}";
        var relativePath = Path.Combine(_uploadFolder, nombreAlmacenado).Replace('\\', '/');
        var absolutePath = Path.Combine(_environment.WebRootPath, _uploadFolder, nombreAlmacenado);

        Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);

        await using (var fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write))
        {
            await contenido.CopyToAsync(fileStream, cancellationToken);
        }

        var fileInfo = new FileInfo(absolutePath);
        var url = $"/{relativePath}";

        var archivo = new Archivo
        {
            Id = archivoId,
            Url = url,
            TamanioBytes = fileInfo.Length,
            Nombre = Path.GetFileNameWithoutExtension(nombreOriginal),
            Extension = extension.ToLowerInvariant(),
            TipoArchivo = TipoArchivo.Imagen,
            FechaCreacion = DateTime.UtcNow
        };

        _context.Archivos.Add(archivo);
        await _context.SaveChangesAsync(cancellationToken);

        return new ArchivoDto
        {
            Id = archivo.Id,
            Url = archivo.Url,
            TamanioBytes = archivo.TamanioBytes,
            Nombre = archivo.Nombre,
            Extension = archivo.Extension,
            TipoArchivo = archivo.TipoArchivo.ToString()
        };
    }
}
