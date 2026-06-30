using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using turismo_oro.Application.Dtos;
using turismo_oro.Application.Services;

namespace turismo_oro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurismoController(TurismoLugarService turismoService) : ControllerBase
{
    private readonly TurismoLugarService _turismoService = turismoService;

    /// <summary>Lista lugares turísticos con búsqueda y filtro opcional por categoría.</summary>
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] string? busqueda,
        [FromQuery] string? categoria,
        CancellationToken cancellationToken)
    {
        var lugares = await _turismoService.ListarAsync(busqueda, categoria, cancellationToken);
        return Ok(lugares);
    }

    /// <summary>Obtiene el detalle de un lugar turístico con fotos y comentarios.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TurismoLugarDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TurismoLugarDto>> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var lugar = await _turismoService.ObtenerPorIdAsync(id, cancellationToken);
        return lugar is null ? NotFound() : Ok(lugar);
    }

    /// <summary>Crea un lugar turístico. Acepta hasta 4 imágenes en el campo <c>imagenes</c>.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TurismoLugarDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TurismoLugarDto>> Crear(
        [FromForm] TurismoLugarCreateRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Descripcion))
            return BadRequest(new { mensaje = "Nombre y descripción son obligatorios." });

        if (string.IsNullOrWhiteSpace(request.Direccion))
            return BadRequest(new { mensaje = "La dirección es obligatoria." });

        var imagenes = (request.Imagenes ?? [])
            .Where(f => f.Length > 0)
            .Take(TurismoLugarService.MaxImagenesPorLugar)
            .Select(f => ((Stream)f.OpenReadStream(), f.FileName))
            .ToList();

        if ((request.Imagenes?.Count ?? 0) > TurismoLugarService.MaxImagenesPorLugar)
            return BadRequest(new { mensaje = $"Máximo {TurismoLugarService.MaxImagenesPorLugar} imágenes por lugar." });

        if (!TryParseCoordenada(request.Latitud, out var latitud) ||
            !TryParseCoordenada(request.Longitud, out var longitud))
            return BadRequest(new { mensaje = "Latitud y longitud deben ser números decimales válidos (use punto como separador)." });

        if (latitud is < -90 or > 90)
            return BadRequest(new { mensaje = "La latitud debe estar entre -90 y 90." });

        if (longitud is < -180 or > 180)
            return BadRequest(new { mensaje = "La longitud debe estar entre -180 y 180." });

        var dto = new TurismoLugarCreateDto
        {
            Nombre = request.Nombre,
            Categoria = request.Categoria,
            Latitud = latitud,
            Longitud = longitud,
            Direccion = request.Direccion,
            EtiquetaPrecio = request.EtiquetaPrecio,
            Descripcion = request.Descripcion,
            Destacados = ParseDestacados(request.Destacados)
        };

        try
        {
            var creado = await _turismoService.CrearAsync(dto, imagenes, cancellationToken);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>Agrega un comentario a un lugar turístico.</summary>
    [HttpPost("{id:guid}/comentarios")]
    [ProducesResponseType(typeof(ComentarioDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ComentarioDto>> AgregarComentario(
        Guid id,
        [FromBody] ComentarioCreateDto dto,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dto.Autor) || string.IsNullOrWhiteSpace(dto.Texto))
            return BadRequest(new { mensaje = "Autor y comentario son obligatorios." });

        try
        {
            var comentario = await _turismoService.AgregarComentarioAsync(id, dto, cancellationToken);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, comentario);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { mensaje = "Lugar turístico no encontrado." });
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    private static IReadOnlyList<string> ParseDestacados(string? destacados) =>
        string.IsNullOrWhiteSpace(destacados)
            ? []
            : destacados.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    private static bool TryParseCoordenada(string? value, out decimal result) =>
        decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
}

public class TurismoLugarCreateRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Latitud { get; set; } = string.Empty;
    public string Longitud { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string EtiquetaPrecio { get; set; } = "Gratis";
    public string Descripcion { get; set; } = string.Empty;
    public string? Destacados { get; set; }
    public List<IFormFile>? Imagenes { get; set; }
}
