using turismo_oro.Application.Dtos;
using turismo_oro.Application.Interfaces;
using turismo_oro.Application.Mappings;
using turismo_oro.Domain.Entities;

namespace turismo_oro.Application.Services;

public class TurismoLugarService
{
    public const int MaxImagenesPorLugar = 4;

    private readonly ITurismoLugarRepository _turismoRepository;
    private readonly IArchivoRepository _archivoRepository;
    private readonly IComentarioRepository _comentarioRepository;
    private readonly IFileStorageService _fileStorageService;

    public TurismoLugarService(
        ITurismoLugarRepository turismoRepository,
        IArchivoRepository archivoRepository,
        IComentarioRepository comentarioRepository,
        IFileStorageService fileStorageService)
    {
        _turismoRepository = turismoRepository;
        _archivoRepository = archivoRepository;
        _comentarioRepository = comentarioRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<IReadOnlyList<TurismoLugarListDto>> ListarAsync(
        string? busqueda,
        string? categoria,
        CancellationToken cancellationToken = default)
    {
        var lugares = await _turismoRepository.GetAllAsync(busqueda, categoria, cancellationToken);
        return lugares.Select(TurismoMapper.ToListDto).ToList();
    }

    public async Task<TurismoLugarDto?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var lugar = await _turismoRepository.GetByIdAsync(id, cancellationToken);
        return lugar is null ? null : TurismoMapper.ToDto(lugar);
    }

    public async Task<TurismoLugarDto> CrearAsync(
        TurismoLugarCreateDto dto,
        IReadOnlyList<(Stream Contenido, string NombreOriginal)> imagenes,
        CancellationToken cancellationToken = default)
    {
        if (imagenes.Count > MaxImagenesPorLugar)
            throw new InvalidOperationException($"Un lugar turístico puede tener como máximo {MaxImagenesPorLugar} imágenes.");

        var lugar = new TurismoLugar
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre.Trim(),
            Categoria = dto.Categoria.Trim(),
            Latitud = dto.Latitud,
            Longitud = dto.Longitud,
            Direccion = dto.Direccion.Trim(),
            EtiquetaPrecio = string.IsNullOrWhiteSpace(dto.EtiquetaPrecio) ? "Gratis" : dto.EtiquetaPrecio.Trim(),
            Descripcion = dto.Descripcion.Trim(),
            Destacados = TurismoMapper.SerializarDestacados(dto.Destacados),
            FechaCreacion = DateTime.UtcNow
        };

        var orden = 0;
        foreach (var imagen in imagenes)
        {
            var archivoDto = await _fileStorageService.GuardarImagenAsync(
                imagen.Contenido,
                imagen.NombreOriginal,
                cancellationToken);

            var archivo = await _archivoRepository.GetByIdAsync(archivoDto.Id, cancellationToken)
                ?? throw new InvalidOperationException("No se pudo recuperar el archivo guardado.");

            lugar.TurismoArchivos.Add(new TurismoArchivo
            {
                TurismoLugarId = lugar.Id,
                ArchivoId = archivo.Id,
                Orden = orden++
            });
        }

        await _turismoRepository.AddAsync(lugar, cancellationToken);
        await _turismoRepository.SaveChangesAsync(cancellationToken);

        var creado = await _turismoRepository.GetByIdAsync(lugar.Id, cancellationToken);
        return TurismoMapper.ToDto(creado!);
    }

    public async Task<ComentarioDto> AgregarComentarioAsync(
        Guid turismoLugarId,
        ComentarioCreateDto dto,
        CancellationToken cancellationToken = default)
    {
        var lugar = await _turismoRepository.GetByIdAsync(turismoLugarId, cancellationToken)
            ?? throw new KeyNotFoundException("Lugar turístico no encontrado.");

        if (dto.Calificacion is < 1 or > 5)
            throw new ArgumentOutOfRangeException(nameof(dto.Calificacion), "La calificación debe estar entre 1 y 5.");

        var comentario = new ComentarioTurismo
        {
            Id = Guid.NewGuid(),
            TurismoLugarId = lugar.Id,
            Autor = dto.Autor.Trim(),
            Calificacion = dto.Calificacion,
            Texto = dto.Texto.Trim(),
            FechaCreacion = DateTime.UtcNow
        };

        await _comentarioRepository.AddAsync(comentario, cancellationToken);
        await _comentarioRepository.SaveChangesAsync(cancellationToken);

        return new ComentarioDto
        {
            Id = comentario.Id,
            Autor = comentario.Autor,
            Calificacion = comentario.Calificacion,
            Texto = comentario.Texto,
            FechaCreacion = comentario.FechaCreacion
        };
    }
}
